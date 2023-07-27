using HtmlAgilityPack;
using Jt.Common.Tool.Extension;
using System.Net.Http;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Tcmsp
{
    public partial class MainFrm : Form
    {
        HttpClient httpClient;

        public MainFrm()
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
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id='grid']");
            if (node != null)
            {
                
            }


        }
    }
}