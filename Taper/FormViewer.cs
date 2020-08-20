using System;
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
            title = block.FileTitle;
            data = block.FileData;
        }

        private void FormViewer_Shown(object sender, EventArgs e)
        {
            //Пытаемся автоматически понять что за файл просматривается, если непонятно - открываем в виде кода
            tabControl.SelectedIndex = 5;
            if (data == null)
            {
                Program.Error("В этом блоке нет данных"); //Тут ащще всё надо переделать
                Close();
                return;
            }
            if (data.Count() == 6914) tabControl.SelectedIndex = 2;
            if (data.Count() == 770) tabControl.SelectedIndex = 3;
            if (title != null && title[1] == 0) tabControl.SelectedIndex = 1;
            tabChange(null, null);
        }

        private void tabChange(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0: break;
                case 1: ViewBasic(); break;
                case 2: ViewScreen(); break;
                case 3: ViewFont(); break;
                case 4:
                    numericUpDownASM.Value = title != null ? title[14] + title[15] * 256 : 16384;
                    textBoxASM.Text = Assembler.Disassembler(data, (int)numericUpDownASM.Value, 16);
                    break;
                case 5: ViewText(); break;
                case 6: tabControl2_SelectedIndexChanged(null, null); break; //Коды
            }
        }

        //Просмотр в виде Basic-программы
        void ViewBasic()
        {
            textBoxProgram.Text = Basic.Program(data, checkBoxSpaces.Checked);
        }

        //Просмотр в виде текста
        void ViewText()
        {
            textBoxText.Text = Data.Text(data);
        }

        //Вкладка просмотра кодов
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxBytes.Text = Data.Bytes(data, 10);
        }

        //Просмотр в виде картинки
        void ViewScreen()
        {
            pictureBoxScreen.Image = Graphics.Screen(data, (int)numericUpDownScreen.Value, 0);
        }

        //Просмотр в виде шрифта
        void ViewFont()
        {
            pictureBoxFont.Image = Graphics.Font(data, (int)numericUpDownFont.Value);
        }

        //Поиск шрифта
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

        private void RefreshASM(object sender, EventArgs e)
        {
            textBoxASM.Text = "Секунду...";
            
            
            Refresh();
            textBoxASM.Text = Assembler.Disassembler(data, (int)numericUpDownASM.Value, 16); 
        }

        //Меню сохранение картинки или шрифта
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

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) { ViewScreen(); }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { ViewBasic(); }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e) { ViewFont(); }
    }
}//1131, 348, 188, 129