using Microsoft.Playwright;
using Newtonsoft.Json;
using Tcmsp.Core.Interface;
using Tcmsp.Domain.SpiderDomain;
using static Tcmsp.Program;

namespace Tcmsp.Core.Impl;

public class TcmspPlaywright : ISpider
{
    private static IBrowserContext _context;
    private static readonly object LockObject = new();

    public TcmspPlaywright()
   {
       if (_context != null) return;
       lock (LockObject)
       {
           if (_context != null) return;
           var playwright = Playwright.CreateAsync().Result;
           var browser = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
           {
               Headless = true,
               ExecutablePath = ConfigJson["edgePath"]?.ToString()
           }).Result;
           _context = browser.NewContextAsync(new BrowserNewContextOptions
           {
               IgnoreHTTPSErrors = true
           }).Result;
       }
   }
    
    private async Task<(List<Ingredients>, List<RelatedTargets>)> GetIngredientsAndRelatedTargets(string name)
    {
       
        var page = await _context.NewPageAsync();
        await page.GotoAsync(
            $"https://old.tcmsp-e.com/tcmspsearch.php?qs=herb_all_name&q={name}&token={ConfigJson["token"]}");
        // 获取#kendoResult标签的文字是否包含Error querying database...
        var errorText = await page.TextContentAsync("#kendoResult");
        if (errorText != null && errorText.Contains("Error"))
        {
            var tokenTable = "input[name='token'][type='hidden']";
            var tokenElementHandle = await page.QuerySelectorAsync(tokenTable);
            var token = await tokenElementHandle.GetAttributeAsync("value");
            ConfigJson["token"] = token;
            await File.WriteAllTextAsync(ConfigFilePath, ConfigJson.ToString());
            await page.GotoAsync(
                $"https://old.tcmsp-e.com/tcmspsearch.php?qs=herb_all_name&q={name}&token={ConfigJson["token"]}");
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded,
                new PageWaitForLoadStateOptions { Timeout = 60000 });
        }

        // 点击链接
        await page.ClickAsync("td[role=gridcell]:nth-child(3) a");

        // 等待页面跳转完成，指定加载状态为"domcontentloaded"，增加等待时间为60秒
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded,
            new PageWaitForLoadStateOptions { Timeout = 60000 });
        // 获取dataSource数据
        var ingredientJson =
            await page.EvaluateAsync<string>("JSON.stringify($('#grid').data('kendoGrid').dataSource.data())");
        var ingredients = JsonConvert.DeserializeObject<List<Ingredients>>(ingredientJson);
        var relatedTargetJson =
            await page.EvaluateAsync<string>("JSON.stringify($('#grid2').data('kendoGrid').dataSource.data())");
        var relatedTargets = JsonConvert.DeserializeObject<List<RelatedTargets>>(relatedTargetJson);
        return (ingredients, relatedTargets);
    }

    public async Task<(List<Ingredients> Ingredients, List<RelatedTargets> RelatedTargets)> GetIngredientsAndTargets(
        string name, decimal ob, decimal dl, string token = "")
    {
        var (ingredientsList, relatedTargetsList) = await GetIngredientsAndRelatedTargets(name); // 使用await来等待异步方法完成
        var ings = ingredientsList.Where(x => x.Ob >= ob && x.Dl >= dl).ToList();
        var target = relatedTargetsList.Where(x => ings.Any(o => o.MoleculeID == x.MoleculeID)).ToList();
        return (ings, target);
    }
}