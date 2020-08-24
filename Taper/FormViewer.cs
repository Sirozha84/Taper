using System;
using System.Linq;
using System.Windows.Forms;

namespace Taper
{
    public partial class FormViewer : Form
    {
        Block block;
        byte[] title;
        byte[] data;
        
        public FormViewer(Block block)
        {
            InitializeComponent();
            this.block = block;
            title = block.FileTitle;
            data = block.FileData;
        }

        private void FormViewer_Shown(object sender, EventArgs e)
        {
            //Пытаемся автоматически понять что за файл просматривается, если непонятно - открываем в виде кода
            int tab = 6;
            
            if (title == null)
            {
                tabPageTitle.Enabled = false;
                labelTitle.Text = Lang.dataBlock;
                labelStart.Text = Lang.start + ":";
                labelLenght.Text = Lang.lenght + ":";
            }
            else
            {
                labelTitle.Text = block.FileType + ": " + block.FileName;
                labelStart.Text = Lang.start + ": " + block.Start.ToString();
                labelLenght.Text = Lang.lenght + ": " + block.Len.ToString();
            }

            if (data == null)
            {
                tabPageProgram.Enabled = false;
                tabPageScreen.Enabled = false;
                tabPageFont.Enabled = false;
                tabPageAssembler.Enabled = false;
                tabPageText.Enabled = false;
                tabPageBytes.Enabled = false;
                tab = 0;
            }
            else
            {
                if (data.Count() == 6914) tab = 2;
                if (data.Count() == 770) tab = 3;
                if (title != null && title[1] == 0) tab = 1;
            }

            tabControl.SelectedIndex = tab;
            tabChange(null, null);
        }

        private void tabChange(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 1: ViewBasic(); break;
                case 2: ViewScreen(); break;
                case 3: ViewFont(); break;
                case 4: ViewAssembler(); break;
                case 5: ViewText(); break;
                case 6: ViewBytes(); break;
            }
        }

        #region Basic
        void ViewBasic()
        {
            textBoxProgram.Text = Basic.Program(data, checkBoxSpaces.Checked);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { ViewBasic(); }
        #endregion

        #region Screen
        void ViewScreen()
        {
            pictureBoxScreen.Image = Graphics.Screen(data, (int)numericUpDownScreen.Value, 0);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) { ViewScreen(); }
        #endregion

        #region Font
        void ViewFont()
        {
            pictureBoxFont.Image = Graphics.Font(data, (int)numericUpDownFont.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e) { ViewFont(); }
        
        private void FontPgUp(object sender, EventArgs e)
        {
            int i = (int)numericUpDownFont.Value - 768;
            if (i >= 0)
                numericUpDownFont.Value = i;
            else
                numericUpDownFont.Value = 0;
        }
        private void FontPgDown(object sender, EventArgs e)
        {
            int i = (int)numericUpDownFont.Value + 768;
            if (i <= numericUpDownFont.Maximum)
                numericUpDownFont.Value = i;
            else
                numericUpDownFont.Value = numericUpDownFont.Maximum;
        }
        #endregion

        #region Assembler
        void ViewAssembler()
        {
            numericUpDownASM.Value = title != null ? title [14] + title [15] * 256 : 16384;
            textBoxASM.Text = Assembler.Disassembler(data, (int) numericUpDownASM.Value, 16);
        }
        private void RefreshASM(object sender, EventArgs e)
        {
            textBoxASM.Text = "Секунду...";
            Refresh();
            textBoxASM.Text = Assembler.Disassembler(data, (int)numericUpDownASM.Value, 16);
        }
        #endregion
        
        #region Text
        void ViewText()
        {
            textBoxText.Text = Data.Text(data);
        }
        #endregion

        #region Bytes
        //Вкладка просмотра кодов
        private void ViewBytes()
        {
            textBoxBytes.Text = "Секунду...";
            Refresh();
            textBoxBytes.Text = Data.Bytes(data, (int)numericUpDown1.Value, radioButtonHEX.Checked ? (byte)16 : (byte)10);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { ViewBytes(); }
        private void radioButtonHEX_CheckedChanged(object sender, EventArgs e) { ViewBytes(); }
        #endregion

        private void SaveBitmap(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog() { Filter = Lang.FilterBMP };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            string file = dialog.FileName;
            try
            {
                if (tabControl.SelectedIndex == 2)
                    pictureBoxScreen.Image.Save(file);
                if (tabControl.SelectedIndex == 3)
                    pictureBoxFont.Image.Save(file);
            }
            catch { Program.Error("Произошла ошибка при сохранении файла. Файл не сохранён."); }
        }

    }
}