using HtmlAgilityPack;
using Jt.Common.Tool.Extension;
using System.Net.Http;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Tcmsp
{
    public partial class Form1 : Form
    {
        HttpClient httpClient;

        public Form1()
        {
            httpClient = new HttpClient();
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
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

            // string url = $"https://old.tcmsp-e.com/tcmspsearch.php?qs=herb_all_name&q={this.tbName.Text}&token={this.tbToken.Text}";
            string url = $"https://old.tcmsp-e.com/tcmspsearch.php?qr={this.tbName.Text}&qsr=herb_en_name&token={this.tbToken.Text}";

            // 初始化网络请求客户端
            HtmlWeb webClient = new HtmlWeb();
            //初始化文档
            HtmlDocument doc = webClient.Load(url);
            //查找节点
            HtmlNodeCollection titleNodes = doc.DocumentNode.SelectNodes("//div[@id='kendoResult']");
            if (titleNodes != null)
            {
                foreach (var item in titleNodes)
                {
                    var subNodes = item.SelectNodes("script");
                    if (subNodes != null)
                    {
                        var js = subNodes[0].f.InnerText;
                        Console.WriteLine(js);
                    }

                }
            }


        }
    }
}