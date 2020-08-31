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
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmSaveBitmap = new System.Windows.Forms.ToolStripMenuItem();
            this.numericLoadTo = new System.Windows.Forms.NumericUpDown();
            this.labelLoadTo = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.numericFind = new System.Windows.Forms.NumericUpDown();
            this.labelFind = new System.Windows.Forms.Label();
            this.comboBoxViewAs = new System.Windows.Forms.ComboBox();
            this.comboBoxModes = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLoadTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFind)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.BackColor = System.Drawing.Color.LightGray;
            this.labelTitle.Font = new System.Drawing.Font("Lucida Console", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(624, 37);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Title";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BackColor = System.Drawing.SystemColors.Window;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox.Location = new System.Drawing.Point(0, 71);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(624, 468);
            this.textBox.TabIndex = 1;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.ContextMenuStrip = this.contextMenu;
            this.pictureBox.Location = new System.Drawing.Point(0, 71);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(624, 468);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
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
            this.cmSaveBitmap.Image = global::Taper.Properties.Resources.save;
            this.cmSaveBitmap.Name = "cmSaveBitmap";
            this.cmSaveBitmap.Size = new System.Drawing.Size(209, 22);
            this.cmSaveBitmap.Text = "Сохранить изображение";
            this.cmSaveBitmap.Click += new System.EventHandler(this.SaveBitmap);
            // 
            // numericLoadTo
            // 
            this.numericLoadTo.Location = new System.Drawing.Point(397, 47);
            this.numericLoadTo.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericLoadTo.Name = "numericLoadTo";
            this.numericLoadTo.Size = new System.Drawing.Size(68, 20);
            this.numericLoadTo.TabIndex = 2;
            this.numericLoadTo.ValueChanged += new System.EventHandler(this.viewChange);
            // 
            // labelLoadTo
            // 
            this.labelLoadTo.Location = new System.Drawing.Point(224, 49);
            this.labelLoadTo.Name = "labelLoadTo";
            this.labelLoadTo.Size = new System.Drawing.Size(167, 13);
            this.labelLoadTo.TabIndex = 4;
            this.labelLoadTo.Text = "Адрес загрузки:";
            this.labelLoadTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(537, 0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Visible = false;
            // 
            // numericFind
            // 
            this.numericFind.Location = new System.Drawing.Point(544, 47);
            this.numericFind.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericFind.Name = "numericFind";
            this.numericFind.Size = new System.Drawing.Size(68, 20);
            this.numericFind.TabIndex = 3;
            this.numericFind.ValueChanged += new System.EventHandler(this.viewChange);
            // 
            // labelFind
            // 
            this.labelFind.Location = new System.Drawing.Point(471, 49);
            this.labelFind.Name = "labelFind";
            this.labelFind.Size = new System.Drawing.Size(67, 13);
            this.labelFind.TabIndex = 11;
            this.labelFind.Text = "Поиск:";
            this.labelFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxViewAs
            // 
            this.comboBoxViewAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViewAs.FormattingEnabled = true;
            this.comboBoxViewAs.Location = new System.Drawing.Point(12, 44);
            this.comboBoxViewAs.Name = "comboBoxViewAs";
            this.comboBoxViewAs.Size = new System.Drawing.Size(100, 21);
            this.comboBoxViewAs.TabIndex = 0;
            this.comboBoxViewAs.SelectedIndexChanged += new System.EventHandler(this.viewChange);
            // 
            // comboBoxModes
            // 
            this.comboBoxModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModes.FormattingEnabled = true;
            this.comboBoxModes.Location = new System.Drawing.Point(118, 44);
            this.comboBoxModes.Name = "comboBoxModes";
            this.comboBoxModes.Size = new System.Drawing.Size(100, 21);
            this.comboBoxModes.TabIndex = 1;
            this.comboBoxModes.SelectedIndexChanged += new System.EventHandler(this.modeChange);
            // 
            // FormViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(624, 539);
            this.Controls.Add(this.comboBoxModes);
            this.Controls.Add(this.comboBoxViewAs);
            this.Controls.Add(this.numericFind);
            this.Controls.Add(this.labelFind);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.numericLoadTo);
            this.Controls.Add(this.labelLoadTo);
            this.MinimumSize = new System.Drawing.Size(640, 400);
            this.Name = "FormViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Просмотр файла";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericLoadTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFind)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem cmSaveBitmap;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.NumericUpDown numericLoadTo;
        private System.Windows.Forms.Label labelLoadTo;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.NumericUpDown numericFind;
        private System.Windows.Forms.Label labelFind;
        private System.Windows.Forms.ComboBox comboBoxViewAs;
        private System.Windows.Forms.ComboBox comboBoxModes;
    }
}