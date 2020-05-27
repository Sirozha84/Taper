using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Taper
{
    public partial class FormMain : Form
    {
        public FormMain() { InitializeComponent(); }

        #region Меню Файл
        /// <summary>
        /// Новый файл
        /// </summary>
        void FileNew()
        {
            if (!SaveQuestion()) return;
            Project.New();
            DrawProject();
        }
        private void menunNew_Click(object sender, EventArgs e) { FileNew(); }
        private void toolNew_Click(object sender, EventArgs e) { FileNew(); }

        /// <summary>
        /// Открыть файл
        /// </summary>
        private void FileOpen()
        {
            if (!SaveQuestion()) return;
            OpenFileDialog dialog = new OpenFileDialog() { Filter = Program.FilterAll };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            Project.Open(dialog.FileName, false);
            DrawProject();
        }
        private void menuOpen_Click(object sender, EventArgs e) { FileOpen(); }
        private void toolOpen_Click(object sender, EventArgs e) { FileOpen(); }

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="saveAs"></param>
        void FileSave(bool saveAs)
        {
            if (Project.name == Program.FileUnnamed | saveAs)
            {
                SaveFileDialog dialog = new SaveFileDialog() { Filter = Program.FilterSel };
                if (dialog.ShowDialog() == DialogResult.OK) Project.Save(dialog.FileName);
                else return;
            }
            else Project.Save(Project.name);
        }

        private void menuSave_Click(object sender, EventArgs e) { FileSave(false); }
        private void toolSave_Click(object sender, EventArgs e) { FileSave(false); }
        private void menuSaveAs_Click(object sender, EventArgs e) { FileSave(true); }

        /// <summary>
        /// Добавление блоков
        /// </summary>
        void importTAP()
        {
            OpenFileDialog dialog = new OpenFileDialog() { Title = "Импорт блоков из TAP-файла", Filter = Program.FilterAll };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            Project.Change();
            Project.Open(dialog.FileName, true);
            DrawProject();
        }
        private void menuImportTap_Click(object sender, EventArgs e) { importTAP(); }

        #endregion

        #region Меню Вид
        private void menuListFiles_Click(object sender, EventArgs e) { menuListFiles.Checked = true; menuListBlocks.Checked = false ; DrawProject(); }
        private void menuListBlocks_Click(object sender, EventArgs e) { menuListFiles.Checked = false; menuListBlocks.Checked = true; DrawProject(); }
        #endregion

        #region Меню Правка
        private void menuUndo_Click(object sender, EventArgs e) { Project.Undo(); DrawProject(); }
        private void toolUndo_Click(object sender, EventArgs e) { Project.Undo(); DrawProject(); }
        private void menuRedo_Click(object sender, EventArgs e) { Project.Redo(); DrawProject(); }
        private void toolRedo_Click(object sender, EventArgs e) { Project.Redo(); DrawProject(); }
        private void menuCut_Click(object sender, EventArgs e) { Project.Cut(listViewTAP.SelectedIndices); DrawProject(); }
        private void toolCut_Click(object sender, EventArgs e) { Project.Cut(listViewTAP.SelectedIndices); DrawProject(); }
        private void cmenuCut_Click(object sender, EventArgs e) { Project.Cut(listViewTAP.SelectedIndices); DrawProject(); }
        private void menuCopy_Click(object sender, EventArgs e) { Project.Copy(listViewTAP.SelectedIndices); }
        private void toolCopy_Click(object sender, EventArgs e) { Project.Copy(listViewTAP.SelectedIndices); }
        private void cmenuCopy_Click(object sender, EventArgs e) { Project.Copy(listViewTAP.SelectedIndices); }
        private void menuPaste_Click(object sender, EventArgs e) { Project.Paste(listViewTAP.SelectedIndices); DrawProject(); }
        private void toolPaste_Click(object sender, EventArgs e) { Project.Paste(listViewTAP.SelectedIndices); DrawProject(); }
        private void cmenuPaste_Click(object sender, EventArgs e) { Project.Paste(listViewTAP.SelectedIndices); DrawProject(); }
        private void menuDelete_Click(object sender, EventArgs e) { Project.Delete(listViewTAP.SelectedIndices); DrawProject(); }
        private void toolMenu_Click(object sender, EventArgs e) { Project.Delete(listViewTAP.SelectedIndices); DrawProject(); }
        private void cmenuDelete_Click(object sender, EventArgs e) { Project.Delete(listViewTAP.SelectedIndices); DrawProject(); }
        private void menuRename_Click(object sender, EventArgs e) { Project.Rename(listViewTAP.SelectedIndices); DrawProject(); }
        private void cmenuRename_Click(object sender, EventArgs e) { Project.Rename(listViewTAP.SelectedIndices); DrawProject(); }
        #endregion

        #region Меню Блоки
        private void menuMoveUp_Click(object sender, EventArgs e) { if (Project.MoveUp(listViewTAP.SelectedIndices)) { DrawProject(); Project.RestroreSelection(listViewTAP, -1); } }
        private void toolMoveUp_Click(object sender, EventArgs e) { if (Project.MoveUp(listViewTAP.SelectedIndices)) { DrawProject(); Project.RestroreSelection(listViewTAP, -1); } }
        private void menuMoveDown_Click(object sender, EventArgs e) { if (Project.MoveDown(listViewTAP.SelectedIndices)) { DrawProject(); Project.RestroreSelection(listViewTAP, 1); } }
        private void toolMoveDown_Click(object sender, EventArgs e) { if (Project.MoveDown(listViewTAP.SelectedIndices)) { DrawProject(); Project.RestroreSelection(listViewTAP, 1); } }
        #endregion

        #region Меню Инструменты
        /// <summary>
        /// Просмотр файла
        /// </summary>
        private void View()
        {
            if (listViewTAP.SelectedIndices.Count < 1) return;
            Project.view = Project.TAP[listViewTAP.SelectedIndices[0]];
            FormViewer form = new FormViewer();
            form.ShowDialog();
        }
        private void menuViewFile_Click(object sender, EventArgs e) { View(); }
        private void listViewTAP_DoubleClick(object sender, EventArgs e) { View(); }
        private void cmenuView_Click(object sender, EventArgs e) { View(); }

        private void menuFixCRCs_Click(object sender, EventArgs e)
        {
            if (Project.FixCRCs()) Program.Message("Контрольные суммы исправлены.");
            else Program.Message("Все контрольные суммы в порядке.");
        }
        private void menuFindDuplicates_Click(object sender, EventArgs e) { Project.FindDuplicates(listViewTAP); }
        #endregion

        #region Меню Справка
        private void menuPage_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("http://www.sg-software.ru/windows/programs/taper"); }
        private void menuAbout_Click(object sender, EventArgs e) { FormAbout form = new FormAbout(); form.ShowDialog(); }
        #endregion
        
        /// <summary>
        /// Обновление заголовка
        /// </summary>
        void SetFormText()
        {
            string star = ""; if (Project.changed) star = "*";
            Text = System.IO.Path.GetFileNameWithoutExtension(Project.name) + star + " - " + Program.Name;
        }
        
        /// <summary>
        /// Задание вопроса перед уничтожением файла
        /// </summary>
        bool SaveQuestion()
        {
            if (!Project.changed) return true;
            switch (MessageBox.Show("Сохранить изменения в файле \"" + System.IO.Path.GetFileNameWithoutExtension(Project.name) + "\"?", "Файл изменён", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes: FileSave(false); return true;
                case DialogResult.No: return true;
                case DialogResult.Cancel: return false;
            }
            return false;
        }
        
        /// <summary>
        /// Выход из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveQuestion()) e.Cancel = true;
            Properties.Settings.Default.Left = Left;
            Properties.Settings.Default.Top = Top;
            Properties.Settings.Default.Width = Width;
            Properties.Settings.Default.Height = Height;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Вывод проекта на экран
        /// </summary>
        void DrawProject()
        {
            if (menuListFiles.Checked) Project.ListFiles(); else Project.ListBlocks();
            string NullString = "- - - - -";
            listViewTAP.Items.Clear();
            int files = 0;
            int bytes = 0;
            int fullbytes = 0;
            foreach (Block block in Project.TAP)
            {
                bool nm = block.FileTitle != null;
                bool dt = block.FileData != null;
                ListViewItem item = new ListViewItem(nm? block.FileType : "    Блок данных");
                item.SubItems.Add(nm ? block.FileName : NullString);
                item.SubItems.Add(nm ? block.Start : NullString);
                item.SubItems.Add(nm ? block.Len : NullString);
                item.SubItems.Add(dt ? (block.FileData.Length - 2).ToString() : NullString);
                item.SubItems.Add(block.CRCview());
                listViewTAP.Items.Add(item);
            }
            //Подсчёт количества блоков
            toolStripStatusLabel2.Text = "Файлов в проекте: " + files;
            toolStripStatusLabel3.Text = "Объём: " + bytes + " байт";
            toolStripStatusLabel4.Text = "Полный объём: " + fullbytes + " байт";
            SetFormText();
        }


        private void изWAVфайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTapeLoad form = new FormTapeLoad();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Project.Change(); //Переместить перед добавлением файлов в проект
                DrawProject();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            изWAVфайлаToolStripMenuItem_Click(null, null);
        }

        private void listViewTAP_KeyDown(object sender, KeyEventArgs e) { /*if (e.KeyData == Keys.Enter) View();*/ }

        //Добавление бита в выборку: 0, 1
        void AddBitToWav(ref List<byte> wav, int bit)
        {
            if (bit == 0)
            {
                //0
                for (int j = 0; j < 10; j++) wav.Add(127);
                for (int j = 0; j < 10; j++) wav.Add(143);
                wav.Add(135);
            }
            else 
            {
                //1
                for (int j = 0; j < 21; j++) wav.Add(127);
                for (int j = 0; j < 21; j++) wav.Add(143);
            } 
        }
        //Добавление блока в выборку: 0 - заголовок, 1 - блок
        void AddBlockToWav(List<byte> wav,  byte[] block, byte Type)
        {
            //Пишем пилот-тон
            int ii = 0;
            if (Type == 0) ii = 3000; else ii = 1500;
            for (int i = 0; i < ii; i++)
            {
                for (int j = 0; j < 27; j++) wav.Add(127);
                for (int j = 0; j < 27; j++) wav.Add(143);
            }
            //Пишем подготовительный сигнал
            for (int j = 0; j < 8; j++) wav.Add(127);
            for (int j = 0; j < 8; j++) wav.Add(143);
            //Пишем блок
            foreach (byte b in block)
            {
                AddBitToWav(ref wav, b & 128);
                AddBitToWav(ref wav, b & 64);
                AddBitToWav(ref wav, b & 32);
                AddBitToWav(ref wav, b & 16);
                AddBitToWav(ref wav, b & 8);
                AddBitToWav(ref wav, b & 4);
                AddBitToWav(ref wav, b & 2);
                AddBitToWav(ref wav, b & 1);
            }
            //Пишем тишину после
            for (int i = 0; i < 30000; i++) wav.Add(127);
        }
        //Сохранение "тапа" в "вавку"
        private void вWAVфайлтожеПокаНеРаботаетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog() { Title = "Экспорт в WAV-файл", Filter = Program.FilterWAV };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            //Подготовка выборки 54 42 21
            List<byte> Data = new List<byte>();
            foreach (Block block in Project.TAP)
            {
                if (block.FileTitle != null) AddBlockToWav(Data, block.FileTitle, 0);
                if (block.FileData != null) AddBlockToWav(Data, block.FileData, 1);
            }
            //Запись файла
            try
            {
                System.IO.BinaryWriter file = new System.IO.BinaryWriter(new System.IO.FileStream(dialog.FileName, System.IO.FileMode.Create));
                file.Write('R');
                file.Write('I');
                file.Write('F');
                file.Write('F');
                file.Write(Data.Count() + 26); //Длина файла (остатка)
                file.Write('W');
                file.Write('A');
                file.Write('V');
                file.Write('E');
                file.Write('f');
                file.Write('m');
                file.Write('t');
                file.Write(' ');
                file.Write(16); //Длина этого кусочка (не знаю зачем, если одинаковая)
                file.Write((ushort)1); //Формат (1 - это видимо PCM)
                file.Write((ushort)1); //Количество каналов
                file.Write(44100); //Дискретизация
                file.Write(44100); //Выдача байтов (для 8-и битного выглядит так же как частота)
                file.Write((ushort)2); //Какое-то выравнивание
                file.Write((ushort)8); //Битность
                file.Write('d');
                file.Write('a');
                file.Write('t');
                file.Write('a');
                file.Write(Data.Count()); //Длина выборки
                file.Write(Data.ToArray());
                file.Close();
            }
            catch { Program.Error("Произошла ошибка при сохранении файла. Файл не сохранён."); }
        }

        private void listViewTAP_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }

        private void listViewTAP_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string file = files[0];
            string ext = System.IO.Path.GetExtension(file).ToLower();

            if (ext == ".tap" | ext == ".tzx")
            {
                if (MessageBox.Show("Добавить файлы в проект? Нет - открыть файл.", Program.Name, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Project.Open(file, true);
                else
                {
                    SaveQuestion();
                    Project.Open(file, false);
                }
                DrawProject();
            }
            else MessageBox.Show("Файл не поддерживается", "Taper");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Left = Properties.Settings.Default.Left;
            Top = Properties.Settings.Default.Top;
            Width = Properties.Settings.Default.Width;
            Height = Properties.Settings.Default.Height;
            FileNew();
            
            //Загружаем файл, если он был передан через аргумент
            string[] args = Environment.GetCommandLineArgs();
            if (args.Count() == 1) return;
            string file = args[1];
            string ext = System.IO.Path.GetExtension(file).ToLower();
            if (ext == ".tap" | ext == ".tzx")
            {
                Project.Open(file, false);
                DrawProject();
            }
            else MessageBox.Show("Файл не поддерживается", "Taper");
        }

    }
} //846 -> 401, 399