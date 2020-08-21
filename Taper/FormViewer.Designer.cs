namespace Taper
{
    partial class FormViewer
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageTitle = new System.Windows.Forms.TabPage();
            this.tabPageProgram = new System.Windows.Forms.TabPage();
            this.checkBoxSpaces = new System.Windows.Forms.CheckBox();
            this.textBoxProgram = new System.Windows.Forms.TextBox();
            this.tabPageScreen = new System.Windows.Forms.TabPage();
            this.pictureBoxScreen = new System.Windows.Forms.PictureBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmSaveBitmap = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDownScreen = new System.Windows.Forms.NumericUpDown();
            this.labelStartAdressScreen = new System.Windows.Forms.Label();
            this.tabPageFont = new System.Windows.Forms.TabPage();
            this.buttonFontPgDown = new System.Windows.Forms.Button();
            this.numericUpDownFont = new System.Windows.Forms.NumericUpDown();
            this.labelStartAdressFont = new System.Windows.Forms.Label();
            this.buttonFontPgUp = new System.Windows.Forms.Button();
            this.pictureBoxFont = new System.Windows.Forms.PictureBox();
            this.tabPageAssembler = new System.Windows.Forms.TabPage();
            this.numericUpDownASM = new System.Windows.Forms.NumericUpDown();
            this.labelStartAdressASM = new System.Windows.Forms.Label();
            this.buttonRefreshASM = new System.Windows.Forms.Button();
            this.textBoxASM = new System.Windows.Forms.TextBox();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.tabPageBytes = new System.Windows.Forms.TabPage();
            this.textBoxBytes = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelStart = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageTitle.SuspendLayout();
            this.tabPageProgram.SuspendLayout();
            this.tabPageScreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen)).BeginInit();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreen)).BeginInit();
            this.tabPageFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFont)).BeginInit();
            this.tabPageAssembler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownASM)).BeginInit();
            this.tabPageText.SuspendLayout();
            this.tabPageBytes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageTitle);
            this.tabControl.Controls.Add(this.tabPageProgram);
            this.tabControl.Controls.Add(this.tabPageScreen);
            this.tabControl.Controls.Add(this.tabPageFont);
            this.tabControl.Controls.Add(this.tabPageAssembler);
            this.tabControl.Controls.Add(this.tabPageText);
            this.tabControl.Controls.Add(this.tabPageBytes);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(558, 491);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabChange);
            // 
            // tabPageTitle
            // 
            this.tabPageTitle.Controls.Add(this.labelStart);
            this.tabPageTitle.Controls.Add(this.labelTitle);
            this.tabPageTitle.Location = new System.Drawing.Point(4, 22);
            this.tabPageTitle.Name = "tabPageTitle";
            this.tabPageTitle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTitle.Size = new System.Drawing.Size(550, 465);
            this.tabPageTitle.TabIndex = 7;
            this.tabPageTitle.Text = "Заголовок";
            this.tabPageTitle.UseVisualStyleBackColor = true;
            // 
            // tabPageProgram
            // 
            this.tabPageProgram.Controls.Add(this.checkBoxSpaces);
            this.tabPageProgram.Controls.Add(this.textBoxProgram);
            this.tabPageProgram.Location = new System.Drawing.Point(4, 22);
            this.tabPageProgram.Name = "tabPageProgram";
            this.tabPageProgram.Size = new System.Drawing.Size(550, 465);
            this.tabPageProgram.TabIndex = 5;
            this.tabPageProgram.Text = "Program";
            this.tabPageProgram.UseVisualStyleBackColor = true;
            // 
            // checkBoxSpaces
            // 
            this.checkBoxSpaces.AutoSize = true;
            this.checkBoxSpaces.Location = new System.Drawing.Point(12, 15);
            this.checkBoxSpaces.Name = "checkBoxSpaces";
            this.checkBoxSpaces.Size = new System.Drawing.Size(158, 17);
            this.checkBoxSpaces.TabIndex = 2;
            this.checkBoxSpaces.Text = "Промежутки между строк";
            this.checkBoxSpaces.UseVisualStyleBackColor = true;
            this.checkBoxSpaces.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBoxProgram
            // 
            this.textBoxProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProgram.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxProgram.Location = new System.Drawing.Point(0, 41);
            this.textBoxProgram.Multiline = true;
            this.textBoxProgram.Name = "textBoxProgram";
            this.textBoxProgram.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxProgram.Size = new System.Drawing.Size(550, 424);
            this.textBoxProgram.TabIndex = 1;
            // 
            // tabPageScreen
            // 
            this.tabPageScreen.Controls.Add(this.pictureBoxScreen);
            this.tabPageScreen.Controls.Add(this.numericUpDownScreen);
            this.tabPageScreen.Controls.Add(this.labelStartAdressScreen);
            this.tabPageScreen.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreen.Name = "tabPageScreen";
            this.tabPageScreen.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreen.Size = new System.Drawing.Size(550, 465);
            this.tabPageScreen.TabIndex = 1;
            this.tabPageScreen.Text = "SCREEN$";
            this.tabPageScreen.UseVisualStyleBackColor = true;
            // 
            // pictureBoxScreen
            // 
            this.pictureBoxScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxScreen.ContextMenuStrip = this.contextMenu;
            this.pictureBoxScreen.Location = new System.Drawing.Point(3, 32);
            this.pictureBoxScreen.Name = "pictureBoxScreen";
            this.pictureBoxScreen.Size = new System.Drawing.Size(544, 430);
            this.pictureBoxScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxScreen.TabIndex = 3;
            this.pictureBoxScreen.TabStop = false;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmSaveBitmap});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(210, 26);
            // 
            // cmSaveBitmap
            // 
            this.cmSaveBitmap.Image = global::Taper.Properties.Resources.Сохранить;
            this.cmSaveBitmap.Name = "cmSaveBitmap";
            this.cmSaveBitmap.Size = new System.Drawing.Size(209, 22);
            this.cmSaveBitmap.Text = "Сохранить изображение";
            this.cmSaveBitmap.Click += new System.EventHandler(this.SaveBitmap);
            // 
            // numericUpDownScreen
            // 
            this.numericUpDownScreen.Location = new System.Drawing.Point(114, 6);
            this.numericUpDownScreen.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownScreen.Name = "numericUpDownScreen";
            this.numericUpDownScreen.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownScreen.TabIndex = 2;
            this.numericUpDownScreen.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // labelStartAdressScreen
            // 
            this.labelStartAdressScreen.AutoSize = true;
            this.labelStartAdressScreen.Location = new System.Drawing.Point(8, 8);
            this.labelStartAdressScreen.Name = "labelStartAdressScreen";
            this.labelStartAdressScreen.Size = new System.Drawing.Size(100, 13);
            this.labelStartAdressScreen.TabIndex = 1;
            this.labelStartAdressScreen.Text = "Начальный адрес:";
            // 
            // tabPageFont
            // 
            this.tabPageFont.Controls.Add(this.buttonFontPgDown);
            this.tabPageFont.Controls.Add(this.numericUpDownFont);
            this.tabPageFont.Controls.Add(this.labelStartAdressFont);
            this.tabPageFont.Controls.Add(this.buttonFontPgUp);
            this.tabPageFont.Controls.Add(this.pictureBoxFont);
            this.tabPageFont.Location = new System.Drawing.Point(4, 22);
            this.tabPageFont.Name = "tabPageFont";
            this.tabPageFont.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFont.Size = new System.Drawing.Size(550, 465);
            this.tabPageFont.TabIndex = 6;
            this.tabPageFont.Text = "Шрифт";
            this.tabPageFont.UseVisualStyleBackColor = true;
            // 
            // buttonFontPgDown
            // 
            this.buttonFontPgDown.Location = new System.Drawing.Point(243, 10);
            this.buttonFontPgDown.Name = "buttonFontPgDown";
            this.buttonFontPgDown.Size = new System.Drawing.Size(49, 25);
            this.buttonFontPgDown.TabIndex = 7;
            this.buttonFontPgDown.Text = ">>>";
            this.buttonFontPgDown.UseVisualStyleBackColor = true;
            this.buttonFontPgDown.Click += new System.EventHandler(this.FontPgDown);
            // 
            // numericUpDownFont
            // 
            this.numericUpDownFont.Location = new System.Drawing.Point(114, 14);
            this.numericUpDownFont.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownFont.Name = "numericUpDownFont";
            this.numericUpDownFont.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownFont.TabIndex = 6;
            this.numericUpDownFont.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // labelStartAdressFont
            // 
            this.labelStartAdressFont.AutoSize = true;
            this.labelStartAdressFont.Location = new System.Drawing.Point(8, 16);
            this.labelStartAdressFont.Name = "labelStartAdressFont";
            this.labelStartAdressFont.Size = new System.Drawing.Size(100, 13);
            this.labelStartAdressFont.TabIndex = 5;
            this.labelStartAdressFont.Text = "Начальный адрес:";
            // 
            // buttonFontPgUp
            // 
            this.buttonFontPgUp.Location = new System.Drawing.Point(188, 10);
            this.buttonFontPgUp.Name = "buttonFontPgUp";
            this.buttonFontPgUp.Size = new System.Drawing.Size(49, 25);
            this.buttonFontPgUp.TabIndex = 4;
            this.buttonFontPgUp.Text = "<<<";
            this.buttonFontPgUp.UseVisualStyleBackColor = true;
            this.buttonFontPgUp.Click += new System.EventHandler(this.FontPgUp);
            // 
            // pictureBoxFont
            // 
            this.pictureBoxFont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxFont.ContextMenuStrip = this.contextMenu;
            this.pictureBoxFont.Location = new System.Drawing.Point(3, 46);
            this.pictureBoxFont.Name = "pictureBoxFont";
            this.pictureBoxFont.Size = new System.Drawing.Size(544, 416);
            this.pictureBoxFont.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxFont.TabIndex = 2;
            this.pictureBoxFont.TabStop = false;
            // 
            // tabPageAssembler
            // 
            this.tabPageAssembler.Controls.Add(this.numericUpDownASM);
            this.tabPageAssembler.Controls.Add(this.labelStartAdressASM);
            this.tabPageAssembler.Controls.Add(this.buttonRefreshASM);
            this.tabPageAssembler.Controls.Add(this.textBoxASM);
            this.tabPageAssembler.Location = new System.Drawing.Point(4, 22);
            this.tabPageAssembler.Name = "tabPageAssembler";
            this.tabPageAssembler.Size = new System.Drawing.Size(550, 465);
            this.tabPageAssembler.TabIndex = 2;
            this.tabPageAssembler.Text = "Ассемблер";
            this.tabPageAssembler.UseVisualStyleBackColor = true;
            // 
            // numericUpDownASM
            // 
            this.numericUpDownASM.Location = new System.Drawing.Point(114, 14);
            this.numericUpDownASM.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownASM.Name = "numericUpDownASM";
            this.numericUpDownASM.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownASM.TabIndex = 3;
            // 
            // labelStartAdressASM
            // 
            this.labelStartAdressASM.AutoSize = true;
            this.labelStartAdressASM.Location = new System.Drawing.Point(8, 16);
            this.labelStartAdressASM.Name = "labelStartAdressASM";
            this.labelStartAdressASM.Size = new System.Drawing.Size(100, 13);
            this.labelStartAdressASM.TabIndex = 2;
            this.labelStartAdressASM.Text = "Начальный адрес:";
            // 
            // buttonRefreshASM
            // 
            this.buttonRefreshASM.Location = new System.Drawing.Point(188, 10);
            this.buttonRefreshASM.Name = "buttonRefreshASM";
            this.buttonRefreshASM.Size = new System.Drawing.Size(122, 25);
            this.buttonRefreshASM.TabIndex = 1;
            this.buttonRefreshASM.Text = "Обновить";
            this.buttonRefreshASM.UseVisualStyleBackColor = true;
            this.buttonRefreshASM.Click += new System.EventHandler(this.RefreshASM);
            // 
            // textBoxASM
            // 
            this.textBoxASM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxASM.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxASM.Location = new System.Drawing.Point(0, 41);
            this.textBoxASM.Multiline = true;
            this.textBoxASM.Name = "textBoxASM";
            this.textBoxASM.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxASM.Size = new System.Drawing.Size(550, 424);
            this.textBoxASM.TabIndex = 0;
            // 
            // tabPageText
            // 
            this.tabPageText.Controls.Add(this.textBoxText);
            this.tabPageText.Location = new System.Drawing.Point(4, 22);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Size = new System.Drawing.Size(550, 465);
            this.tabPageText.TabIndex = 3;
            this.tabPageText.Text = "Текст";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // textBoxText
            // 
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxText.Location = new System.Drawing.Point(3, 3);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxText.Size = new System.Drawing.Size(544, 459);
            this.textBoxText.TabIndex = 0;
            // 
            // tabPageBytes
            // 
            this.tabPageBytes.Controls.Add(this.textBoxBytes);
            this.tabPageBytes.Location = new System.Drawing.Point(4, 22);
            this.tabPageBytes.Name = "tabPageBytes";
            this.tabPageBytes.Size = new System.Drawing.Size(550, 465);
            this.tabPageBytes.TabIndex = 4;
            this.tabPageBytes.Text = "Bytes";
            this.tabPageBytes.UseVisualStyleBackColor = true;
            // 
            // textBoxBytes
            // 
            this.textBoxBytes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBytes.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBytes.Location = new System.Drawing.Point(3, 3);
            this.textBoxBytes.Multiline = true;
            this.textBoxBytes.Name = "textBoxBytes";
            this.textBoxBytes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBytes.Size = new System.Drawing.Size(544, 459);
            this.textBoxBytes.TabIndex = 1;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(8, 106);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(77, 37);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Title";
            // 
            // labelStart
            // 
            this.labelStart.AutoSize = true;
            this.labelStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStart.Location = new System.Drawing.Point(8, 157);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(85, 37);
            this.labelStart.TabIndex = 1;
            this.labelStart.Text = "Start";
            // 
            // FormViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 491);
            this.Controls.Add(this.tabControl);
            this.Name = "FormViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Просмотр файла";
            this.Shown += new System.EventHandler(this.FormViewer_Shown);
            this.tabControl.ResumeLayout(false);
            this.tabPageTitle.ResumeLayout(false);
            this.tabPageTitle.PerformLayout();
            this.tabPageProgram.ResumeLayout(false);
            this.tabPageProgram.PerformLayout();
            this.tabPageScreen.ResumeLayout(false);
            this.tabPageScreen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen)).EndInit();
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreen)).EndInit();
            this.tabPageFont.ResumeLayout(false);
            this.tabPageFont.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFont)).EndInit();
            this.tabPageAssembler.ResumeLayout(false);
            this.tabPageAssembler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownASM)).EndInit();
            this.tabPageText.ResumeLayout(false);
            this.tabPageText.PerformLayout();
            this.tabPageBytes.ResumeLayout(false);
            this.tabPageBytes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageScreen;
        private System.Windows.Forms.TabPage tabPageAssembler;
        private System.Windows.Forms.TabPage tabPageText;
        private System.Windows.Forms.TabPage tabPageBytes;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.TabPage tabPageProgram;
        private System.Windows.Forms.TextBox textBoxProgram;
        private System.Windows.Forms.TextBox textBoxBytes;
        private System.Windows.Forms.TextBox textBoxASM;
        private System.Windows.Forms.NumericUpDown numericUpDownASM;
        private System.Windows.Forms.Label labelStartAdressASM;
        private System.Windows.Forms.Button buttonRefreshASM;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem cmSaveBitmap;
        private System.Windows.Forms.NumericUpDown numericUpDownScreen;
        private System.Windows.Forms.Label labelStartAdressScreen;
        private System.Windows.Forms.CheckBox checkBoxSpaces;
        private System.Windows.Forms.TabPage tabPageFont;
        private System.Windows.Forms.PictureBox pictureBoxFont;
        private System.Windows.Forms.NumericUpDown numericUpDownFont;
        private System.Windows.Forms.Label labelStartAdressFont;
        private System.Windows.Forms.Button buttonFontPgUp;
        private System.Windows.Forms.Button buttonFontPgDown;
        private System.Windows.Forms.TabPage tabPageTitle;
        private System.Windows.Forms.PictureBox pictureBoxScreen;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelStart;
    }
}