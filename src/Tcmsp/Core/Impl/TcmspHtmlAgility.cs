using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tcmsp.Core.Interface;
using Tcmsp.Domain.SpiderDomain;
using static Tcmsp.Program;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Tcmsp.Core.Impl;

public class TcmspHtmlAgility : ISpider
{
    private static HttpClient HttpClient;
    private readonly HtmlDocument _doc = new();
    private string _token;

    private const string TcmspBaseUrl = "https://old.tcmsp-e.com/tcmspsearch.php";
    private const string TokenQueryParam = "token";

    public TcmspHtmlAgility()
    {
        HttpClient ??= new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        });
        _token = ConfigJson["token"]?.ToString();
    }

    private async Task<bool> CheckToken()
    {
        var kendoResult = _doc.DocumentNode.SelectSingleNode("//div[@id='kendoResult']").InnerText;
        if (!kendoResult.Contains("Error querying database"))
        {
            return false;
        }

        await UpdateToken();
        return true;
    }

    public async Task<(List<Ingredients> Ingredients, List<RelatedTargets> RelatedTargets)> GetIngredientsAndTargets(string name, decimal ob, decimal dl, string token = "")
    {
        await UpdateDocument($"{TcmspBaseUrl}?qs=herb_all_name&q={name}&token={_token}");
        if (await CheckToken())
        {
            await UpdateDocument($"{TcmspBaseUrl}?qs=herb_all_name&q={name}&token={_token}");
        }

        await GetHerbEnName();
        return await GetValue();
    }

    private async Task GetHerbEnName()
    {
        try
        {
            var script = _doc.DocumentNode.SelectSingleNode("//div[@id='kendoResult']/script").InnerHtml;
            const string pattern = @"data*:\s(\[[^\]]*\])";
            var match = Regex.Match(script, pattern);
            if (match.Success)
            {
                var dataJson = match.Groups[1].Value;
                var jsonObject = JArray.Parse(dataJson).First;
                if (jsonObject != null)
                {
                    var herbEnName = (string)jsonObject["herb_en_name"];
                    await UpdateDocument($"{TcmspBaseUrl}?qr={herbEnName}&qsr=herb_en_name&token={_token}");
                }
            }
            else
            {
                Console.WriteLine(@"未在 JavaScript 代码中找到数据。");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private Task<(List<Ingredients> Ingredients, List<RelatedTargets> RelatedTargets)> GetValue()
    {
        var script = _doc.DocumentNode.SelectSingleNode("//*[@id='kendoResult']//*[@id='tabstrip']/script[2]")
            .InnerHtml;
        var jsonDataList = new List<string>();
        const string pattern = @"data:\s*(\[.*?\])\,";
        var matches = Regex.Matches(script, pattern, RegexOptions.Singleline);
        foreach (Match match in matches)
        {
            jsonDataList.Add(match.Groups[1].Value);
        }

        var ingredients = JsonConvert.DeserializeObject<List<Ingredients>>(jsonDataList[0]);
        var relatedTargets = JsonConvert.DeserializeObject<List<RelatedTargets>>(jsonDataList[1]);
        return Task.FromResult((ingredients, relatedTargets));
    }

    private async Task UpdateToken()
    {
        var tokenInput = _doc.DocumentNode.SelectSingleNode("//input[@name='token'][@type='hidden']");
        _token = tokenInput.GetAttributeValue("value", "");
        ConfigJson["token"] = _token;
        await File.WriteAllTextAsync(ConfigFilePath, ConfigJson.ToString());
    }

    private async Task UpdateDocument(string url)
    {
        var httpResponseMessage = await HttpClient.GetAsync(url);
        var docText = await httpResponseMessage.Content.ReadAsStringAsync();
        _doc.LoadHtml(docText);
    }
}
