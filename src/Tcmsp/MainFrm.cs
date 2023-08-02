using Jt.Common.Tool.Extension;
using Tcmsp.Core.Impl;
using Tcmsp.Core.Interface;

namespace Tcmsp
{
    public partial class MainFrm : Form
    {
        ISpider _spider;
        public MainFrm()
        {
            InitializeComponent();
            _spider = new HtmlAgilityPackSpider();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            try
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

                if (!decimal.TryParse(this.tbObVal.Text, out decimal obval))
                {
                    MessageBox.Show("Obֵ����");
                    return;
                }

                if (!decimal.TryParse(this.tbDlVal.Text, out decimal dlval))
                {
                    MessageBox.Show("DLֵ����");
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                this.dgvIng.DataSource = null;
                this.dgvTarget.DataSource = null;
                this.tssIng.Text = null;
                this.tssTarget.Text = null;

                var data = _spider.GetIngredientsAndTargets(this.tbName.Text, obval, dlval, this.tbToken.Text);
                if (data.Ingredients == null)
                {
                    MessageBox.Show("û��ȡ������");
                    return;
                }

                this.dgvIng.DataSource = data.Ingredients;
                this.dgvTarget.DataSource = data.RelatedTargets;
                this.tssIng.Text = data.Ingredients.Count.ToString();
                this.tssTarget.Text = data.RelatedTargets.Count.ToString();
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
    }
}