using HtmlAgilityPack;
using Jt.Common.Tool.Extension;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Tcmsp
{
    public partial class MainFrm : Form
    {
        HttpClient httpClient;
        private const string pattern1 = "(?<=\"herb_en_name\":\").*?(?=\",)";
        private const string pattern2 = "(?<=data: ).*?(?=}],)";


        public MainFrm()
        {
            httpClient = new HttpClient();
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tbToken.Text.IsNullOrWhiteSpace())
                {
                    MessageBox.Show("请填写token");
                    return;
                }

                if (this.tbName.Text.IsNullOrWhiteSpace())
                {
                    MessageBox.Show("请填写名称");
                    return;
                }

                if (!decimal.TryParse(this.tbObVal.Text, out decimal obval))
                {
                    MessageBox.Show("Ob值不对");
                    return;
                }

                if (!decimal.TryParse(this.tbDlVal.Text, out decimal dlval))
                {
                    MessageBox.Show("DL值不对");
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                var enName = GetEnName(this.tbName.Text, this.tbToken.Text);
                var data = GetTargets(enName, this.tbToken.Text);
                var ings = data.Ingredients.Where(x => x.ob >= obval && x.dl >= dlval).ToList();
                var target = data.RelatedTargets.Where(x => ings.Any(o => o.molecule_ID == x.molecule_ID)).ToList();
                this.dgvIng.DataSource = ings;
                this.dgvTarget.DataSource = target;
                this.tssIng.Text = ings.Count.ToString();
                this.tssTarget.Text = target.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private string GetEnName(string name, string token)
        {
            string url = $"https://old.tcmsp-e.com/tcmspsearch.php?qs=herb_all_name&q={name}&token={token}";

            // 初始化网络请求客户端
            HtmlWeb webClient = new HtmlWeb();
            //初始化文档
            HtmlDocument doc = webClient.Load(url);
            //查找节点
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id='grid']");
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
            // 初始化网络请求客户端
            HtmlWeb webClient = new HtmlWeb();
            //初始化文档
            HtmlDocument doc = webClient.Load(url);
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