namespace Tcmsp
{
    partial class MainFrm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbName = new TextBox();
            label1 = new Label();
            btnGet = new Button();
            tbToken = new TextBox();
            label2 = new Label();
            groupBox1 = new GroupBox();
            tbDlVal = new TextBox();
            label4 = new Label();
            tbObVal = new TextBox();
            label3 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            dgvIng = new DataGridView();
            statusStrip1 = new StatusStrip();
            tssIng = new ToolStripStatusLabel();
            tabPage2 = new TabPage();
            dgvTarget = new DataGridView();
            statusStrip2 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            tssTarget = new ToolStripStatusLabel();
            groupBox1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIng).BeginInit();
            statusStrip1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTarget).BeginInit();
            statusStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // tbName
            // 
            tbName.Location = new Point(103, 50);
            tbName.Name = "tbName";
            tbName.Size = new Size(318, 23);
            tbName.TabIndex = 0;
            tbName.Text = "党参";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(54, 53);
            label1.Name = "label1";
            label1.Size = new Size(32, 17);
            label1.TabIndex = 1;
            label1.Text = "名称";
            // 
            // btnGet
            // 
            btnGet.Location = new Point(437, 51);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(75, 23);
            btnGet.TabIndex = 2;
            btnGet.Text = "获取";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // tbToken
            // 
            tbToken.Location = new Point(103, 21);
            tbToken.Name = "tbToken";
            tbToken.Size = new Size(318, 23);
            tbToken.TabIndex = 3;
            tbToken.Text = "93032b3fd6e9f3c12b74007fe1b19f8a";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(54, 24);
            label2.Name = "label2";
            label2.Size = new Size(41, 17);
            label2.TabIndex = 4;
            label2.Text = "token";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tbDlVal);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(tbObVal);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(728, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(367, 145);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "筛选";
            // 
            // tbDlVal
            // 
            tbDlVal.Location = new Point(23, 109);
            tbDlVal.Name = "tbDlVal";
            tbDlVal.Size = new Size(318, 23);
            tbDlVal.TabIndex = 5;
            tbDlVal.Text = "0.18";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 89);
            label4.Name = "label4";
            label4.Size = new Size(71, 17);
            label4.TabIndex = 4;
            label4.Text = "DL大于等于";
            // 
            // tbObVal
            // 
            tbObVal.Location = new Point(23, 51);
            tbObVal.Name = "tbObVal";
            tbObVal.Size = new Size(318, 23);
            tbObVal.TabIndex = 3;
            tbObVal.Text = "30";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 31);
            label3.Name = "label3";
            label3.Size = new Size(74, 17);
            label3.TabIndex = 2;
            label3.Text = "OB大于等于";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Bottom;
            tabControl1.Location = new Point(0, 163);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1107, 610);
            tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dgvIng);
            tabPage1.Controls.Add(statusStrip1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1099, 580);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Ingredients";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvIng
            // 
            dgvIng.AllowUserToAddRows = false;
            dgvIng.AllowUserToDeleteRows = false;
            dgvIng.BackgroundColor = SystemColors.Control;
            dgvIng.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIng.Dock = DockStyle.Fill;
            dgvIng.Location = new Point(3, 3);
            dgvIng.Name = "dgvIng";
            dgvIng.ReadOnly = true;
            dgvIng.RowTemplate.Height = 25;
            dgvIng.Size = new Size(1093, 552);
            dgvIng.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tssIng });
            statusStrip1.Location = new Point(3, 555);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1093, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // tssIng
            // 
            tssIng.Name = "tssIng";
            tssIng.Size = new Size(0, 17);
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dgvTarget);
            tabPage2.Controls.Add(statusStrip2);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1099, 580);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Related Targets";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvTarget
            // 
            dgvTarget.AllowUserToAddRows = false;
            dgvTarget.AllowUserToDeleteRows = false;
            dgvTarget.BackgroundColor = SystemColors.Control;
            dgvTarget.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTarget.Dock = DockStyle.Fill;
            dgvTarget.Location = new Point(3, 3);
            dgvTarget.Name = "dgvTarget";
            dgvTarget.ReadOnly = true;
            dgvTarget.RowTemplate.Height = 25;
            dgvTarget.Size = new Size(1093, 552);
            dgvTarget.TabIndex = 3;
            // 
            // statusStrip2
            // 
            statusStrip2.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, tssTarget });
            statusStrip2.Location = new Point(3, 555);
            statusStrip2.Name = "statusStrip2";
            statusStrip2.Size = new Size(1093, 22);
            statusStrip2.TabIndex = 2;
            statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // tssTarget
            // 
            tssTarget.Name = "tssTarget";
            tssTarget.Size = new Size(0, 17);
            // 
            // MainFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1107, 773);
            Controls.Add(tabControl1);
            Controls.Add(groupBox1);
            Controls.Add(label2);
            Controls.Add(tbToken);
            Controls.Add(btnGet);
            Controls.Add(label1);
            Controls.Add(tbName);
            Name = "MainFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tcmsp";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIng).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTarget).EndInit();
            statusStrip2.ResumeLayout(false);
            statusStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbName;
        private Label label1;
        private Button btnGet;
        private TextBox tbToken;
        private Label label2;
        private GroupBox groupBox1;
        private TextBox tbObVal;
        private Label label3;
        private TextBox tbDlVal;
        private Label label4;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dgvIng;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tssIng;
        private DataGridView dgvTarget;
        private StatusStrip statusStrip2;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel tssTarget;
    }
}