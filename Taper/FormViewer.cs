﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace Taper
{
    public partial class FormViewer : Form
    {
        byte[] title;
        byte[] data;
        public FormViewer(Block block)
        {
            InitializeComponent();
            Text = Lang.fileView;
            labelLoadTo.Text = Lang.loadTo + ":";
            labelFind.Text = Lang.find + ":";
            cmSaveBitmap.Text = Lang.saveImage;

            title = block.FileTitle;
            data = block.FileData;

            if (title != null)
                labelTitle.Text = block.FileType + ": " + block.FileName + " (" + block.Start.ToString() + ", " + block.Len.ToString() + ")";
            else
                labelTitle.Text = Lang.dataBlock;

            if (data != null)
            {
                comboBoxViewAs.Items.Add("Basic");
                comboBoxViewAs.Items.Add("Bytes");
                comboBoxViewAs.Items.Add("Screen");
                comboBoxViewAs.Items.Add("Font");
                comboBoxViewAs.Items.Add("Assembler");
                comboBoxViewAs.Items.Add("Text");
                int i = 1;
                if (data.Count() == 6914) i = 2;
                if (data.Count() == 770) i = 3;
                if (title != null && title[1] == 0) i = 0;
                comboBoxViewAs.SelectedIndex = i;

                numericLoadTo.Value = title != null ? title[14] + title[15] * 256 : 16384;
                if (i == 0) numericLoadTo.Value = 0;
            }
        }

        private void viewChange(object sender, EventArgs e)
        {
            switch (comboBoxViewAs.SelectedIndex)
            {
                case 0:
                    comboBoxModes.Enabled = false;
                    comboBoxModes.Items.Clear();
                    numericLoadTo.Enabled = false;
                    numericFind.Enabled = false;
                    ViewBasic();
                    break;
                case 1:
                    comboBoxModes.Enabled = true;
                    numericLoadTo.Enabled = true;
                    numericFind.Enabled = false;
                    comboBoxModes.Items.Clear();
                    comboBoxModes.Items.Add("HEX");
                    comboBoxModes.Items.Add("DEC");
                    comboBoxModes.SelectedIndex = 0;
                    break;
                case 2:
                    comboBoxModes.Enabled = true;
                    numericLoadTo.Enabled = false;
                    numericFind.Enabled = true;
                    comboBoxModes.Items.Clear();
                    comboBoxModes.Items.Add(Lang.colored);
                    comboBoxModes.Items.Add("B/W");
                    comboBoxModes.Items.Add("W/B");
                    comboBoxModes.SelectedIndex = 0;
                    break;
                case 3:
                    comboBoxModes.Enabled = false;
                    numericLoadTo.Enabled = false;
                    numericFind.Enabled = true;
                    comboBoxModes.Items.Clear();
                    ViewFont();
                    break;
                case 4:
                    comboBoxModes.Enabled = false;
                    numericLoadTo.Enabled = true;
                    numericFind.Enabled = false;
                    comboBoxModes.Items.Clear();
                    ViewAssembler();
                    break;
                case 5:
                    comboBoxModes.Enabled = false;
                    numericLoadTo.Enabled = false;
                    numericFind.Enabled = false;
                    comboBoxModes.Items.Clear();
                    ViewText();
                    break;
            }
        }

        private void modeChange(object sender, EventArgs e)
        {
            switch (comboBoxViewAs.SelectedIndex)
            {
                case 1: ViewBytes(); break;
                case 2: ViewScreen(); break;
            }
        }

        #region Обновление просмотрщиков
        void ViewBasic()
        {
            textBox.Visible = true;
            pictureBox.Visible = false;
            textBox.Text = Basic.Program(data);
        }

        private void ViewBytes()
        {
            textBox.Visible = true;
            pictureBox.Visible = false;
            textBox.Text = Lang.msgPleaseWait;
            Refresh();
            textBox.Text = Data.Bytes(data, (int)numericLoadTo.Value, comboBoxModes.SelectedIndex == 0 ? (byte)16 : (byte)10);
        }

        void ViewScreen()
        {
            pictureBox.Visible = true;
            textBox.Visible = false;
            pictureBox.Image = Graphics.Screen(data, (int)numericFind.Value, comboBoxModes.SelectedIndex);
        }

        void ViewFont()
        {
            pictureBox.Visible = true;
            textBox.Visible = false;
            pictureBox.Image = Graphics.Font(data, (int)numericFind.Value);
        }

        void ViewAssembler()
        {
            textBox.Visible = true;
            pictureBox.Visible = false;
            textBox.Text = Lang.msgPleaseWait;
            Refresh();
            textBox.Text = Assembler.Disassembler(data, (int)numericLoadTo.Value, 16);
        }

        void ViewText()
        {
            textBox.Visible = true;
            pictureBox.Visible = false;
            textBox.Text = Data.Text(data);
        }
        #endregion

        private void SaveBitmap(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog() { Filter = Lang.FilterBMP };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            string file = dialog.FileName;
            try { pictureBox.Image.Save(file); }
            catch { Program.Error(Lang.errorSave); }
        }
    }
}