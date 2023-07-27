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
                MessageBox.Show("����дtoken");
                return;
            }

            if (this.tbName.Text.IsNullOrWhiteSpace())
            {
                MessageBox.Show("����д����");
                return;
            }

            // string url = $"https://old.tcmsp-e.com/tcmspsearch.php?qs=herb_all_name&q={this.tbName.Text}&token={this.tbToken.Text}";
            string url = $"https://old.tcmsp-e.com/tcmspsearch.php?qr={this.tbName.Text}&qsr=herb_en_name&token={this.tbToken.Text}";

            // ��ʼ����������ͻ���
            HtmlWeb webClient = new HtmlWeb();
            //��ʼ���ĵ�
            HtmlDocument doc = webClient.Load(url);
            //���ҽڵ�
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