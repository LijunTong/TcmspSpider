namespace Tcmsp
{
    partial class Form1
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
            SuspendLayout();
            // 
            // tbName
            // 
            tbName.Location = new Point(91, 29);
            tbName.Name = "tbName";
            tbName.Size = new Size(318, 23);
            tbName.TabIndex = 0;
            tbName.Text = "党参";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 32);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 1;
            label1.Text = "英文名称";
            // 
            // btnGet
            // 
            btnGet.Location = new Point(415, 29);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(75, 23);
            btnGet.TabIndex = 2;
            btnGet.Text = "获取";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // tbToken
            // 
            tbToken.Location = new Point(91, 0);
            tbToken.Name = "tbToken";
            tbToken.Size = new Size(318, 23);
            tbToken.TabIndex = 3;
            tbToken.Text = "4597a89c4da253e9909a22da9d18c9ec";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 3);
            label2.Name = "label2";
            label2.Size = new Size(41, 17);
            label2.TabIndex = 4;
            label2.Text = "token";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(tbToken);
            Controls.Add(btnGet);
            Controls.Add(label1);
            Controls.Add(tbName);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbName;
        private Label label1;
        private Button btnGet;
        private TextBox tbToken;
        private Label label2;
    }
}