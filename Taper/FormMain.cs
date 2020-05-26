using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Taper
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

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
            OpenFileDialog dialog = new OpenFileDialog() { Filter = Program.FilterTAP };
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
                SaveFileDialog dialog = new SaveFileDialog() { Filter = Program.FilterTAP };
                if (dialog.ShowDialog() == DialogResult.OK) Project.Save(dialog.FileName);
                else return;
            }
            else Project.Save(Project.name);
        }

        private void menuSave_Click(object sender, EventArgs e) { FileSave(false); }
        private void toolSave_Click(object sender, EventArgs e) { FileSave(false); }
        private void menuSaveAs_Click(object sender, EventArgs e) { FileSave(true); }

        /// <summary>
        /// Импорт блоков из TAP-файла
        /// </summary>
        void importTAP()
        {
            OpenFileDialog dialog = new OpenFileDialog() { Title = "Импорт блоков из TAP-файла", Filter = Program.FilterTAP };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            Project.Change();
            Project.Open(dialog.FileName, true);
            DrawProject();
        }
        private void menuImportTap_Click(object sender, EventArgs e) { importTAP(); }

        /// <summary>
        /// Импорт блоков из TZX-файла
        /// </summary>
        void importTZX()
        {
            OpenFileDialog dialog = new OpenFileDialog() { Title = "Импорт блоков из TZX-файла", Filter = Program.FilterTZX };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            Project.Change();
            TZXload(dialog.FileName);
            DrawProject();
        }
        private void menuImportTZX_Click(object sender, EventArgs e) { importTZX(); }
        private void menuexit_Click(object sender, EventArgs e) { Close(); }

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
        #endregion

        #region Меню Инструменты
        #endregion

        #region Меню Справка
        private void menuabout_Click(object sender, EventArgs e) { FormAbout form = new FormAbout(); form.ShowDialog(); }
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
        
        //Движение блоков вверх
        private void подвинутьВверхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.Change();
            //Сначала проверим, нормально ли выделено
            if (!NormalSelection()) return;
            if (listViewTAP.SelectedIndices[0] == 0) return; //Двигаться некуда...
            //Теперь мы уверены, что выделено всё правильно, можно двигать
            //Запоминаем временный блок, который потом появится "снизу"
            Block temp = Project.TAP[listViewTAP.SelectedIndices[0] - 1];
            for (int i = 0; i < listViewTAP.SelectedIndices.Count; i++)
                Project.TAP[listViewTAP.SelectedIndices[i] - 1] = Project.TAP[listViewTAP.SelectedIndices[i]];
            Project.TAP[listViewTAP.SelectedIndices[listViewTAP.SelectedIndices.Count - 1]] = temp;
            //Теперь нвдо запомнить какие строчки были выделены
            List<int> sel = new List<int>();
            foreach (int i in listViewTAP.SelectedIndices)
                sel.Add(i);
            //Делаем изменения на экране (и в мозгах)
            DrawProject();
            //Теперь восстанавливаем выделение, но на строчку выше
            foreach (int i in sel)
                listViewTAP.Items[i - 1].Selected = true;
        }
        //Проверка на "Нормальность" выделения, чтобы, например, избежать "дыр" //Проверить где это надо, и для чего
        bool NormalSelection()
        {
            if (listViewTAP.SelectedIndices.Count == 0) { Program.Message("Ничего не выделено"); return false; }
            if (listViewTAP.SelectedIndices.Count == 1) return true; //Если выделен только один - уже хорошо
            bool Normal = true;
            for (int i = 0; i < listViewTAP.SelectedIndices.Count - 1; i++)
                if (listViewTAP.SelectedIndices[i] + 1 != listViewTAP.SelectedIndices[i + 1]) Normal = false;
            if (!Normal) { Program.Message("Для этого блоки должны быть выделены последовательно"); return false; }
            return true;
        }

        private void подвинутьВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.Change();
            //Сначала проверим, нормально ли выделено
            if (!NormalSelection()) return;
            if (listViewTAP.SelectedIndices[listViewTAP.SelectedIndices.Count - 1] == listViewTAP.Items.Count - 1) return; //Двигаться некуда...
            //Теперь мы уверены, что выделено всё правильно, можно двигать
            //Запоминаем временный блок, который потом появится "снизу"
            Block temp = Project.TAP[listViewTAP.SelectedIndices[listViewTAP.SelectedIndices.Count - 1] + 1];
            for (int i = listViewTAP.SelectedIndices.Count - 1; i >= 0 ; i--)
                Project.TAP[listViewTAP.SelectedIndices[i] + 1] = Project.TAP[listViewTAP.SelectedIndices[i]];
            Project.TAP[listViewTAP.SelectedIndices[0]] = temp;
            //Теперь нвдо запомнить какие строчки были выделены
            List<int> sel = new List<int>();
            foreach (int i in listViewTAP.SelectedIndices)
                sel.Add(i);
            //Делаем изменения на экране (и в мозгах)
            DrawProject();
            //Теперь восстанавливаем выделение, но на строчку выше
            foreach (int i in sel)
                listViewTAP.Items[i + 1].Selected = true;
        }

        private void изWAVфайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTapeLoad form = new FormTapeLoad();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Project.Change();
                DrawProject();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            изWAVфайлаToolStripMenuItem_Click(null, null);
        }

        private void просмотрФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewTAP.SelectedIndices.Count != 1)
            {
                Program.Message("Должен быть выделен один блок.");
                return;
            }
            if (Project.TAP[listViewTAP.SelectedIndices[0]].FileData == null)
            {
                Program.Message("В этом блоке только заголовок.");
                return;
            }
            Project.view = Project.TAP[listViewTAP.SelectedIndices[0]];
            FormViewer form = new FormViewer();
            form.ShowDialog();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            просмотрФайлаToolStripMenuItem_Click(null, null);
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) listView1_DoubleClick(null, null);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.sg-software.ru");
        }
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
        //Исправление контрольных сумм в блоке
        bool FixCRC(byte[] block)
        {
            byte crc=0;
            for (int i = 0; i < block.Count() - 1; i++) crc = (byte)(crc ^ block[i]);
            if (block[block.Count() - 1] != crc)
            {
                block[block.Count() - 1] = crc;
                return true;
            }
            return false;
        }

        void TZXload(string filename)
        {
            try
            {
                System.IO.BinaryReader file = new System.IO.BinaryReader(new System.IO.FileStream(filename, System.IO.FileMode.Open));
                file.ReadBytes(10); //10 байт какой-то херни в начале файла
                while (file.BaseStream.Position < file.BaseStream.Length)
                {
                    file.ReadBytes(3); //3 байта какой-то херни в начале каждого блока
                    int LEN = file.ReadUInt16();
                    byte[] Bytes = file.ReadBytes(LEN); //Собственно загрузка, в C# она простая и быстрая
                    //Добавляем загруженную коллекцию байтов в проект
                    Project.Add(Bytes);
                }
                file.Close();
            }
            catch { Program.Error("Произошла ошибка при открытии файла."); }
        }

        //Экспорт в TZX
        private void вTZXфайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog() { Filter = Program.FilterTZX };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                System.IO.BinaryWriter file = new System.IO.BinaryWriter(new System.IO.FileStream(dialog.FileName, System.IO.FileMode.Create));
                file.Write('Z');
                file.Write('X');
                file.Write('T');
                file.Write('a');
                file.Write('p');
                file.Write('e');
                file.Write('!');
                file.Write((byte)26);
                file.Write((byte)1);
                file.Write((byte)0);
                foreach (Block block in Project.TAP)
                {
                    if (block.FileTitle != null)
                    {
                        file.Write((byte)16);
                        file.Write((Int16)1000);
                        file.Write((Int16)block.FileTitle.Count());
                        file.Write(block.FileTitle);
                    }
                    if (block.FileData != null)
                    {
                        file.Write((byte)16);
                        file.Write((Int16)1000);
                        file.Write((Int16)block.FileData.Count());
                        file.Write(block.FileData);
                    }
                }
                file.Close();
            }
            catch { Program.Error("Произошла ошибка при сохранении файла. Файл не сохранён."); }
        }
        //Исправление контрольных сумм
        private void проверкаКонтрольныхСуммToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool action = false;
            foreach (Block block in Project.TAP)
            {
                if (block.FileTitle != null) if (FixCRC(block.FileTitle)) action = true;
                if (block.FileData != null) if (FixCRC(block.FileData)) action = true;
                block.CRCData = true;
            }
            Project.Change();
            DrawProject();
            if (action) Program.Message("Контрольные суммы исправлены.");
            else Program.Message("Все контрольные суммы и так в норме.");
        }
        //Поиск дубликатов блоков
        private void поискДубликатовToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //Хрен знает зачем, но Стас попросил сделать такую возможность
            bool find = false;
            //УБираем выделения (тупой способ, но не знаю как проще)
            for (int i = 0; i < listViewTAP.Items.Count; i++) listViewTAP.Items[i].Selected = false;
            for (int i = 0; i < Project.TAP.Count() - 1; i++)
            {
                if (Project.TAP[i].FileData != null)
                {
                    //То, с чем будем сравнивать всё остальное
                    byte[] block = Project.TAP[i].FileData;
                    for (int j = i + 1; j < Project.TAP.Count(); j++)
                        if (Project.TAP[j].FileData != null)
                            if (Project.TAP[i].FileData == Project.TAP[j].FileData)
                            {
                                find = true;
                                listViewTAP.Items[i].Selected = true;
                                listViewTAP.Items[j].Selected = true;
                            }
                    if (find) break;
                }
            }
            if (find) Program.Message("Найдены дубликаты файлов. Они отмечены выделением.");
            else Program.Message("Дубликаты не обнаружены.");
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) { просмотрФайлаToolStripMenuItem_Click(null, null); }
        private void toolStripButton4_Click(object sender, EventArgs e) { подвинутьВверхToolStripMenuItem_Click(null, null); }
        private void toolStripButton6_Click(object sender, EventArgs e) { подвинутьВнизToolStripMenuItem_Click(null, null); }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string file = files[0];
            string ext = System.IO.Path.GetExtension(file).ToLower();
            if (ext == ".tap" & SaveQuestion())
            {
                Project.Open(file, false); //Здесь будет зависить от будущего диалога (добавить или открыть)
                DrawProject();
                return;
            }
            if (ext == ".tzx" & SaveQuestion())
            {
                Project.name = Program.FileUnnamed;
                SetFormText();
                Project.New();
                TZXload(file);
                DrawProject();
                return;
            }
            //А здесь добавим файл импортом (которого пока нет) если он не больше 65535 байт
            MessageBox.Show("Файл не поддерживается", "Taper");
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
            if (ext == ".tap" & SaveQuestion())
            {
                Project.Open(file, false);
                DrawProject();
                return;
            }
            if (ext == ".tzx" & SaveQuestion())
            {
                Project.name = Program.FileUnnamed;
                SetFormText();
                Project.New();
                TZXload(file);
                DrawProject();
                return;
            }
            MessageBox.Show("Файл не поддерживается", "Taper");
        }

    }
} //846, 820, 759, 734, 696, 642, 598, 577

