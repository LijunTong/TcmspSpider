using Microsoft.Playwright;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tcmsp.Core.Interface;
using Tcmsp.Domain.SpiderDomain;

namespace Tcmsp.Core.Impl;

public class TcmspPlaywright : ISpider
{
    private async Task<(List<Ingredients>, List<RelatedTargets>)> GetIngredientsAndRelatedTargets(string name)
    {
        var filePath = Path.Combine(Application.StartupPath, "Config.json");
        // 获取Config.json的token
        var configJson = JObject.Parse(await File.ReadAllTextAsync(filePath));
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            ExecutablePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe" // Edge浏览器的安装路径
        });
        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true
        });
        var page = await context.NewPageAsync();
        await page.GotoAsync(
            $"https://old.tcmsp-e.com/tcmspsearch.php?qs=herb_all_name&q={name}&token={configJson["token"]}");
        // 获取#kendoResult标签的文字是否包含Error querying database...，如果包含则抛出异常
        var errorText = await page.TextContentAsync("#kendoResult");
        if (errorText != null && errorText.Contains("Error"))
        {
            var tokenTable = "input[name='token'][type='hidden']";
            var tokenElementHandle = await page.QuerySelectorAsync(tokenTable);
            var token = await tokenElementHandle.GetAttributeAsync("value");
            configJson["token"] = token;
            await File.WriteAllTextAsync(filePath, configJson.ToString());
            await page.GotoAsync(
                $"https://old.tcmsp-e.com/tcmspsearch.php?qs=herb_all_name&q={name}&token={configJson["token"]}");
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
        await browser.CloseAsync();
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