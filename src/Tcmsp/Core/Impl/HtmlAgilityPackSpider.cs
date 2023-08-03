using HtmlAgilityPack;
using Jt.Common.Tool.Extension;
using System.Text.RegularExpressions;
using Tcmsp.Core.Interface;
using Tcmsp.Domain.SpiderDomain;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Tcmsp.Core.Impl
{
    public class HtmlAgilityPackSpider : ISpider
    {
        private const string pattern1 = "(?<=\"herb_en_name\":\").*?(?=\",)";
        private const string pattern2 = "(?<=data: ).*?(?=}],)";
        HttpClient _httpClient;

        public HtmlAgilityPackSpider()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            clientHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13 | System.Security.Authentication.SslProtocols.Tls11;
            _httpClient = new HttpClient(clientHandler);
        }

        public (List<Ingredients> Ingredients, List<RelatedTargets> RelatedTargets) GetIngredientsAndTargets(string name, decimal ob, decimal dl, string token = "")
        {
            if (token.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("_token");
            }

            var enName = GetEnName(name, token);
            if (enName.IsNullOrWhiteSpace())
            {
                return (null, null);
            }

            var data = GetTargets(enName, token);
            var ings = data.Ingredients.Where(x => x.Ob >= ob && x.Dl >= dl).ToList();
            var target = data.RelatedTargets.Where(x => ings.Any(o => o.MoleculeID == x.MoleculeID)).ToList();
            return (ings, target);
        }

        private string GetEnName(string name, string token)
        {
            string url = $"https://old.tcmsp-e.com/tcmspsearch.php?qs=herb_all_name&q={name}&token={token}";

            string docText = _httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            if (docText.Contains("Error querying database"))
            {
                MessageBox.Show("请更换Token");
                return "";
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(docText);
            //查找节点
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id='kendoResult']");
            if (node != null)
            {
                var subNodes = node.SelectNodes("script");
                if (subNodes != null && subNodes.Count > 0)
                {
                    var text = subNodes[0].InnerHtml;
                    Regex regex = new Regex(pattern1);
                    var matchs = regex.Matches(text);
                    foreach (var match in matchs)
                    {
                        Console.WriteLine(match.ToString());
                        return match.ToString();
                    }
                }

            }
            return "";
        }

        private (List<Ingredients> Ingredients, List<RelatedTargets> RelatedTargets) GetTargets(string enName, string token)
        {
            string url = $"https://old.tcmsp-e.com/tcmspsearch.php?qr={enName}&qsr=herb_en_name&token={token}";
            string docText = _httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            //初始化文档
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(docText);
            //查找节点
            HtmlNodeCollection titleNodes = doc.DocumentNode.SelectNodes("//div[@id='tabstrip']");
            if (titleNodes != null)
            {
                foreach (var item in titleNodes)
                {
                    var subNodes = item.SelectNodes("script");
                    if (subNodes != null && subNodes.Count > 0)
                    {
                        var text = subNodes[1].InnerHtml;
                        Regex regex = new Regex(pattern2);
                        var matchs = regex.Matches(text);
                        string ingredientsJson = matchs[0].Value + "}]";
                        List<Ingredients> ingredients = ingredientsJson.ToObj<List<Ingredients>>();
                        string relatedTargetsJson = matchs[1].Value + "}]";
                        List<RelatedTargets> relatedTargets = relatedTargetsJson.ToObj<List<RelatedTargets>>();
                        return (ingredients, relatedTargets);
                    }
                }
            }
            return (null, null);
        }
    }
}
