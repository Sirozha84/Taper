namespace Taper
{
    partial class FormRec
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderBlock = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCRC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxDevices = new System.Windows.Forms.ComboBox();
            this.labelSound = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.pictureBorder = new System.Windows.Forms.PictureBox();
            this.pictureWave = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWave)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(297, 326);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(216, 326);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(68, 110);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(120, 13);
            this.labelInfo.TabIndex = 6;
            this.labelInfo.Text = "Загруженные данные:";
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTime,
            this.columnHeaderBlock,
            this.columnHeaderCRC});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(68, 126);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(304, 194);
            this.listView.TabIndex = 5;
            this.listView.TabStop = false;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Время";
            // 
            // columnHeaderBlock
            // 
            this.columnHeaderBlock.Text = "Блок";
            this.columnHeaderBlock.Width = 150;
            // 
            // columnHeaderCRC
            // 
            this.columnHeaderCRC.Text = "CRC";
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevices.FormattingEnabled = true;
            this.comboBoxDevices.Location = new System.Drawing.Point(164, 12);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Size = new System.Drawing.Size(208, 21);
            this.comboBoxDevices.TabIndex = 0;
            this.comboBoxDevices.SelectedIndexChanged += new System.EventHandler(this.comboBoxDevices_SelectedIndexChanged);
            // 
            // labelSound
            // 
            this.labelSound.AutoSize = true;
            this.labelSound.Location = new System.Drawing.Point(68, 15);
            this.labelSound.Name = "labelSound";
            this.labelSound.Size = new System.Drawing.Size(90, 13);
            this.labelSound.TabIndex = 11;
            this.labelSound.Text = "Источник звука:";
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClear.Location = new System.Drawing.Point(68, 326);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // pictureBorder
            // 
            this.pictureBorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBorder.Location = new System.Drawing.Point(12, 12);
            this.pictureBorder.Name = "pictureBorder";
            this.pictureBorder.Size = new System.Drawing.Size(50, 337);
            this.pictureBorder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBorder.TabIndex = 9;
            this.pictureBorder.TabStop = false;
            // 
            // pictureWave
            // 
            this.pictureWave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureWave.Location = new System.Drawing.Point(68, 39);
            this.pictureWave.Name = "pictureWave";
            this.pictureWave.Size = new System.Drawing.Size(304, 64);
            this.pictureWave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureWave.TabIndex = 20;
            this.pictureWave.TabStop = false;
            // 
            // FormRec
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.pictureWave);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.labelSound);
            this.Controls.Add(this.comboBoxDevices);
            this.Controls.Add(this.pictureBorder);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.listView);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "FormRec";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Запись";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTapeLoader_FormClosing);
            this.Load += new System.EventHandler(this.FormTapeLoader_Load);
            this.Shown += new System.EventHandler(this.FormRec_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.ColumnHeader columnHeaderBlock;
        private System.Windows.Forms.ColumnHeader columnHeaderCRC;
        private System.Windows.Forms.PictureBox pictureBorder;
        private System.Windows.Forms.ComboBox comboBoxDevices;
        private System.Windows.Forms.Label labelSound;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.PictureBox pictureWave;
    }
}