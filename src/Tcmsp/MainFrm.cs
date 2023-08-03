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
            _spider = new TcmspPlaywright();
        }

        private async void btnGet_Click(object sender, EventArgs e)
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
                this.dgvIng.DataSource = null;
                this.dgvTarget.DataSource = null;
                this.tssIng.Text = null;
                this.tssTarget.Text = null;

                var data = await _spider.GetIngredientsAndTargets(this.tbName.Text, obval, dlval, this.tbToken.Text);
                if (data.Ingredients == null)
                {
                    MessageBox.Show("没获取到数据");
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