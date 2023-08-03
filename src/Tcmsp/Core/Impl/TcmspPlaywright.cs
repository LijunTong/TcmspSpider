using Microsoft.Playwright;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tcmsp.Core.Interface;
using Tcmsp.Domain.SpiderDomain;

namespace Tcmsp.Core.Impl
{
    public class TcmspPlaywright : ISpider
    {
        private static IBrowserContext _context;
        private static readonly object LockObject = new();

        private static readonly string ConfigFilePath = Path.Combine(Application.StartupPath, "Config.json");
        private static JObject ConfigJson;

        static TcmspPlaywright()
        {
            LoadConfig();
        }

        private static void LoadConfig()
        {
            ConfigJson = JObject.Parse(File.ReadAllText(ConfigFilePath));
        }

        public TcmspPlaywright()
        {
            if (_context != null) return;
            lock (LockObject)
            {
                if (_context == null)
                {
                    InitializeContextAsync().GetAwaiter().GetResult();
                }
            }
        }

        private async Task InitializeContextAsync()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true,
                ExecutablePath = ConfigJson["edgePath"]?.ToString()
            });
            _context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                IgnoreHTTPSErrors = true
            });
        }

        public async Task<(List<Ingredients>, List<RelatedTargets>)> GetIngredientsAndRelatedTargets(string name)
        {
            var page = await _context.NewPageAsync();
            await page.GotoAsync($"https://old.tcmsp-e.com/tcmspsearch.php?qs=herb_all_name&q={name}&token={ConfigJson["token"]}");
            
            var errorText = await page.TextContentAsync("#kendoResult");
            if (errorText != null && errorText.Contains("Error"))
            {
                var tokenTable = "input[name='token'][type='hidden']";
                var tokenElementHandle = await page.QuerySelectorAsync(tokenTable);
                var token = await tokenElementHandle.GetAttributeAsync("value");
                ConfigJson["token"] = token;
                await File.WriteAllTextAsync(ConfigFilePath, ConfigJson.ToString());
                await page.GotoAsync($"https://old.tcmsp-e.com/tcmspsearch.php?qs=herb_all_name&q={name}&token={ConfigJson["token"]}");
                await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 60000 });
            }

            await page.ClickAsync("td[role=gridcell]:nth-child(3) a");

            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 60000 });

            var ingredientJson = await page.EvaluateAsync<string>("JSON.stringify($('#grid').data('kendoGrid').dataSource.data())");
            var ingredients = JsonConvert.DeserializeObject<List<Ingredients>>(ingredientJson);
            var relatedTargetJson = await page.EvaluateAsync<string>("JSON.stringify($('#grid2').data('kendoGrid').dataSource.data())");
            var relatedTargets = JsonConvert.DeserializeObject<List<RelatedTargets>>(relatedTargetJson);
            
            return (ingredients, relatedTargets);
        }

        public async Task<(List<Ingredients>, List<RelatedTargets>)> GetIngredientsAndTargets(string name, decimal ob, decimal dl, string token = "")
        {
            var (ingredientsList, relatedTargetsList) = await GetIngredientsAndRelatedTargets(name);
            var ings = ingredientsList.Where(x => x.Ob >= ob && x.Dl >= dl).ToList();
            var target = relatedTargetsList.Where(x => ings.Any(o => o.MoleculeID == x.MoleculeID)).ToList();
            return (ings, target);
        }

        public static async Task DisposeAsync()
        {
            if (_context != null)
            {
                await _context.CloseAsync();
                _context = null;
            }
        }
    }
}
