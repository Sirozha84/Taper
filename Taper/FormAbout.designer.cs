namespace Taper
{
    partial class FormAbout
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logo = new System.Windows.Forms.PictureBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.linkLabelSite = new System.Windows.Forms.LinkLabel();
            this.labelNewVersions = new System.Windows.Forms.Label();
            this.labelAutor = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.tabPageHistory = new System.Windows.Forms.TabPage();
            this.history = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.tabPageHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // logo
            // 
            this.logo.Image = global::Taper.Properties.Resources.Logo;
            this.logo.Location = new System.Drawing.Point(32, 32);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(128, 128);
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageAbout);
            this.tabControl.Controls.Add(this.tabPageHistory);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(460, 208);
            this.tabControl.TabIndex = 1;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.linkLabelSite);
            this.tabPageAbout.Controls.Add(this.labelNewVersions);
            this.tabPageAbout.Controls.Add(this.labelAutor);
            this.tabPageAbout.Controls.Add(this.labelVersion);
            this.tabPageAbout.Controls.Add(this.labelName);
            this.tabPageAbout.Controls.Add(this.logo);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(452, 182);
            this.tabPageAbout.TabIndex = 0;
            this.tabPageAbout.Text = "О программе";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // linkLabelSite
            // 
            this.linkLabelSite.AutoSize = true;
            this.linkLabelSite.Location = new System.Drawing.Point(182, 144);
            this.linkLabelSite.Name = "linkLabelSite";
            this.linkLabelSite.Size = new System.Drawing.Size(100, 13);
            this.linkLabelSite.TabIndex = 6;
            this.linkLabelSite.TabStop = true;
            this.linkLabelSite.Text = "www.sg-software.ru";
            this.linkLabelSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSite_LinkClicked_1);
            // 
            // labelNewVersions
            // 
            this.labelNewVersions.Location = new System.Drawing.Point(182, 118);
            this.labelNewVersions.Name = "labelNewVersions";
            this.labelNewVersions.Size = new System.Drawing.Size(226, 30);
            this.labelNewVersions.TabIndex = 5;
            this.labelNewVersions.Text = "Новую версию этой и других моих программ Вы можете загрузить на сайте";
            // 
            // labelAutor
            // 
            this.labelAutor.AutoSize = true;
            this.labelAutor.Location = new System.Drawing.Point(182, 92);
            this.labelAutor.Name = "labelAutor";
            this.labelAutor.Size = new System.Drawing.Size(124, 13);
            this.labelAutor.TabIndex = 4;
            this.labelAutor.Text = "Автор: Сергей Гордеев";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(182, 67);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(44, 13);
            this.labelVersion.TabIndex = 3;
            this.labelVersion.Text = "Версия";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(180, 32);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(68, 25);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Name";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(397, 226);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.Controls.Add(this.history);
            this.tabPageHistory.Location = new System.Drawing.Point(4, 22);
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHistory.Size = new System.Drawing.Size(452, 182);
            this.tabPageHistory.TabIndex = 1;
            this.tabPageHistory.Text = "История версий";
            this.tabPageHistory.UseVisualStyleBackColor = true;
            // 
            // history
            // 
            this.history.BackColor = System.Drawing.Color.White;
            this.history.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.history.Dock = System.Windows.Forms.DockStyle.Fill;
            this.history.Location = new System.Drawing.Point(3, 3);
            this.history.Name = "history";
            this.history.ReadOnly = true;
            this.history.Size = new System.Drawing.Size(446, 176);
            this.history.TabIndex = 0;
            this.history.Text = "";
            // 
            // FormAbout
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            this.tabPageHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TabPage tabPageHistory;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.LinkLabel linkLabelSite;
        private System.Windows.Forms.Label labelNewVersions;
        private System.Windows.Forms.Label labelAutor;
        private System.Windows.Forms.RichTextBox history;

    }
}
