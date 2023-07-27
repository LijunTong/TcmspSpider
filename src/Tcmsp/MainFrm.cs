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
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id='grid']");
            if (node != null)
            {
                
            }


        }
    }
}