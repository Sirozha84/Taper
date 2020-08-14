namespace Taper
{
    partial class FormProperties
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.groupBoxPosition = new System.Windows.Forms.GroupBox();
            this.radioButtonRem = new System.Windows.Forms.RadioButton();
            this.radioButtonCenter = new System.Windows.Forms.RadioButton();
            this.tabPageAudio = new System.Windows.Forms.TabPage();
            this.groupBoxAudio = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxRec = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPlay = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxLang = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxLanguages = new System.Windows.Forms.ComboBox();
            this.tabControl.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.groupBoxPosition.SuspendLayout();
            this.tabPageAudio.SuspendLayout();
            this.groupBoxAudio.SuspendLayout();
            this.groupBoxLang.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageMain);
            this.tabControl.Controls.Add(this.tabPageAudio);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(360, 184);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.groupBoxLang);
            this.tabPageMain.Controls.Add(this.groupBoxPosition);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(352, 158);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Основные";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // groupBoxPosition
            // 
            this.groupBoxPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPosition.Controls.Add(this.radioButtonRem);
            this.groupBoxPosition.Controls.Add(this.radioButtonCenter);
            this.groupBoxPosition.Location = new System.Drawing.Point(6, 68);
            this.groupBoxPosition.Name = "groupBoxPosition";
            this.groupBoxPosition.Size = new System.Drawing.Size(340, 69);
            this.groupBoxPosition.TabIndex = 2;
            this.groupBoxPosition.TabStop = false;
            this.groupBoxPosition.Text = "Начальная позиция окна";
            // 
            // radioButtonRem
            // 
            this.radioButtonRem.AutoSize = true;
            this.radioButtonRem.Location = new System.Drawing.Point(6, 19);
            this.radioButtonRem.Name = "radioButtonRem";
            this.radioButtonRem.Size = new System.Drawing.Size(203, 17);
            this.radioButtonRem.TabIndex = 0;
            this.radioButtonRem.TabStop = true;
            this.radioButtonRem.Text = "Запоминать последнее положение";
            this.radioButtonRem.UseVisualStyleBackColor = true;
            // 
            // radioButtonCenter
            // 
            this.radioButtonCenter.AutoSize = true;
            this.radioButtonCenter.Location = new System.Drawing.Point(6, 42);
            this.radioButtonCenter.Name = "radioButtonCenter";
            this.radioButtonCenter.Size = new System.Drawing.Size(147, 17);
            this.radioButtonCenter.TabIndex = 1;
            this.radioButtonCenter.TabStop = true;
            this.radioButtonCenter.Text = "Всегда в центре экрана";
            this.radioButtonCenter.UseVisualStyleBackColor = true;
            // 
            // tabPageAudio
            // 
            this.tabPageAudio.Controls.Add(this.groupBoxAudio);
            this.tabPageAudio.Location = new System.Drawing.Point(4, 22);
            this.tabPageAudio.Name = "tabPageAudio";
            this.tabPageAudio.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAudio.Size = new System.Drawing.Size(352, 158);
            this.tabPageAudio.TabIndex = 1;
            this.tabPageAudio.Text = "Аудио";
            this.tabPageAudio.UseVisualStyleBackColor = true;
            // 
            // groupBoxAudio
            // 
            this.groupBoxAudio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAudio.Controls.Add(this.label2);
            this.groupBoxAudio.Controls.Add(this.comboBoxRec);
            this.groupBoxAudio.Controls.Add(this.label1);
            this.groupBoxAudio.Controls.Add(this.comboBoxPlay);
            this.groupBoxAudio.Location = new System.Drawing.Point(6, 6);
            this.groupBoxAudio.Name = "groupBoxAudio";
            this.groupBoxAudio.Size = new System.Drawing.Size(340, 79);
            this.groupBoxAudio.TabIndex = 0;
            this.groupBoxAudio.TabStop = false;
            this.groupBoxAudio.Text = "Устройства по умолчанию";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 21);
            this.label2.TabIndex = 15;
            this.label2.Text = "Запись:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxRec
            // 
            this.comboBoxRec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRec.FormattingEnabled = true;
            this.comboBoxRec.Location = new System.Drawing.Point(117, 46);
            this.comboBoxRec.Name = "comboBoxRec";
            this.comboBoxRec.Size = new System.Drawing.Size(200, 21);
            this.comboBoxRec.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "Вопроизведение:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxPlay
            // 
            this.comboBoxPlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPlay.FormattingEnabled = true;
            this.comboBoxPlay.Location = new System.Drawing.Point(117, 19);
            this.comboBoxPlay.Name = "comboBoxPlay";
            this.comboBoxPlay.Size = new System.Drawing.Size(200, 21);
            this.comboBoxPlay.TabIndex = 12;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(216, 202);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(297, 202);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBoxLang
            // 
            this.groupBoxLang.Controls.Add(this.comboBoxLanguages);
            this.groupBoxLang.Controls.Add(this.label3);
            this.groupBoxLang.Location = new System.Drawing.Point(6, 6);
            this.groupBoxLang.Name = "groupBoxLang";
            this.groupBoxLang.Size = new System.Drawing.Size(340, 55);
            this.groupBoxLang.TabIndex = 3;
            this.groupBoxLang.TabStop = false;
            this.groupBoxLang.Text = "Язык (Language)";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Язык:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLanguages
            // 
            this.comboBoxLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguages.FormattingEnabled = true;
            this.comboBoxLanguages.Location = new System.Drawing.Point(117, 19);
            this.comboBoxLanguages.Name = "comboBoxLanguages";
            this.comboBoxLanguages.Size = new System.Drawing.Size(200, 21);
            this.comboBoxLanguages.TabIndex = 13;
            // 
            // FormProperties
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(384, 237);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormProperties";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Параметры";
            this.Load += new System.EventHandler(this.FormProperties_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.groupBoxPosition.ResumeLayout(false);
            this.groupBoxPosition.PerformLayout();
            this.tabPageAudio.ResumeLayout(false);
            this.groupBoxAudio.ResumeLayout(false);
            this.groupBoxLang.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageAudio;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxPosition;
        private System.Windows.Forms.RadioButton radioButtonRem;
        private System.Windows.Forms.RadioButton radioButtonCenter;
        private System.Windows.Forms.GroupBox groupBoxAudio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxRec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPlay;
        private System.Windows.Forms.GroupBox groupBoxLang;
        private System.Windows.Forms.ComboBox comboBoxLanguages;
        private System.Windows.Forms.Label label3;
    }
}