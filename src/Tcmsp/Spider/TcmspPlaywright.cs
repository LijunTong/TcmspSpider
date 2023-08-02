namespace Tcmsp.Spider;
using Newtonsoft.Json;
using Microsoft.Playwright;
using Domain.SpiderDomain;
public class TcmspPlaywright
{
    public async Task<(List<Ingredients>, List<RelatedTargets>)> getIngredientsAndRequireds(string name)
    {
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
            "https://old.tcmsp-e.com/tcmspsearch.php");
        var searchInputSelector = "#inputVarTcm"; // 替换为搜索框的选择器
        var searchButtonSelector = "#searchBtTcm"; // 替换为搜索按钮的选择器
        var searchValue = name; // 替换为要搜索的值
        await page.FillAsync(searchInputSelector, searchValue);
        await page.ClickAsync(searchButtonSelector);
        // 等待页面跳转完成，指定加载状态为"domcontentloaded"，增加等待时间为60秒
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 60000 });
        
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
}