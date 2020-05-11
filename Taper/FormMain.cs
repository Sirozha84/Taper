﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Taper
{
    public partial class FormMain : Form
    {
        List<Block> Buffer = new List<Block>(); //Буфер обмена

        public FormMain()
        {
            InitializeComponent();
            Left = Properties.Settings.Default.Left;
            Top = Properties.Settings.Default.Top;
            Width = Properties.Settings.Default.Width;
            Height = Properties.Settings.Default.Height;
            Project.New();
            DrawTap();
        }

        //Сохранение файла как
        bool FileSaveAs()
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = Program.FileTypeTAP;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) { Project.name = saveFileDialog1.FileName; ChangeReset(); return true; }
            return false;
        }
        void SetFormText()
        {
            string star = ""; if (Project.changed) star = "*";
            Text = System.IO.Path.GetFileNameWithoutExtension(Project.name) + star + " - " + Program.Name;
        }
        //Регистрация сохранений в файле
        void ChangeReset()
        {
            Project.changed = false;
            SetFormText();
        }
        //Задание вопроса перед уничтожением файла
        bool SaveQuestion()
        {
            if (!Project.changed) return true;
            switch (MessageBox.Show("Сохранить изменения в файле \"" + System.IO.Path.GetFileNameWithoutExtension(Project.name) + "\"?", "Файл изменён", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes: return FileSave();
                case DialogResult.No: return true;
                case DialogResult.Cancel: return false;
            }
            return false;
        }
        //Закрытие программы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Left = Left;
            Properties.Settings.Default.Top = Top;
            Properties.Settings.Default.Width = Width;
            Properties.Settings.Default.Height = Height;
            Properties.Settings.Default.Save();
        }
        private void menuabout_Click(object sender, EventArgs e) { FormAbout form = new FormAbout(); form.ShowDialog(); }
        private void menusave_Click(object sender, EventArgs e) { FileSave(); }
        private void menusaveas_Click(object sender, EventArgs e) { if (FileSaveAs()) FileSave(); }
        private void toolnew_Click(object sender, EventArgs e) { menunew_Click(null, null); }
        private void toolopen_Click(object sender, EventArgs e) { menuopen_Click(null, null); }
        private void toolsave_Click(object sender, EventArgs e) { menusave_Click(null, null); }
        private void toolcut_Click(object sender, EventArgs e) { menucut_Click(null, null); }
        private void toolcopy_Click(object sender, EventArgs e) { menucopy_Click(null, null); }
        private void toolpaste_Click(object sender, EventArgs e) { menupaste_Click(null, null); }
        private void menunew_Click(object sender, EventArgs e) { if (SaveQuestion()) { Project.New(); DrawTap(); }
        }

        //Сохранение файла
        bool FileSave()
        {
            if (Project.name == Program.FileUnnamed && !FileSaveAs()) return false;
            try
            {
                System.IO.BinaryWriter file = new System.IO.BinaryWriter(new System.IO.FileStream(Project.name, System.IO.FileMode.Create));
                foreach (Block block in Project.TAP)
                {
                    //Сохраняем заголовок
                    if (block.FileTitle != null)
                    {
                        file.Write((UInt16)19);
                        file.Write(block.FileTitle);
                    }
                    //Сохраняем блок данных
                    if (block.FileData != null)
                    {
                        file.Write((UInt16)block.FileData.Count());
                        file.Write(block.FileData);
                    }
                }
                file.Close();
                ChangeReset(); return true;
            }
            catch { Program.Error("Произошла ошибка во время сохранения файла. Файл не сохранён."); return false; }
        }
        private void menuopen_Click(object sender, EventArgs e)
        {
            if (!SaveQuestion()) return;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = Program.FileTypeTAP;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            Project.New();
            Project.name = openFileDialog1.FileName;
            LoadTapFile(Project.name);
            DrawTap();
            Project.changed = false;
            SetFormText();
            
        }
        //Чтение TAP и добавление файлов в проект
        private void LoadTapFile(string filename)
        {
            try
            {
                System.IO.BinaryReader file = new System.IO.BinaryReader(new System.IO.FileStream(filename,System.IO.FileMode.Open));
                while (file.BaseStream.Position<file.BaseStream.Length)
                {
                    int LEN = file.ReadUInt16();
                    byte[] Bytes = file.ReadBytes(LEN);
                    //Добавляем загруженную коллекцию байтов в проект
                    Project.Add(Bytes);
                }
                file.Close();
            }
            catch { Program.Error("Произошла ошибка при открытии файла."); }
        }
        //Копирование
        private void menucopy_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) { Program.Message("Ничего не выделено"); return; }
            Buffer.Clear();
            for (int i = 0; i < listView1.SelectedIndices.Count; i++)
                Buffer.Add(Project.TAP[listView1.SelectedIndices[i]]);
        }
        //Вырезание
        private void menucut_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) { Program.Message("Ничего не выделено"); return; }
            Buffer.Clear();
            for (int i = 0; i < listView1.SelectedIndices.Count; i++)
                Buffer.Add(Project.TAP[listView1.SelectedIndices[i]]);
            for (int i = listView1.SelectedIndices.Count - 1; i >= 0; i--)
                Project.TAP.RemoveAt(listView1.SelectedIndices[i]);
            Project.Change();
            DrawTap();
        }
        //Вставка
        private void menupaste_Click(object sender, EventArgs e)
        {
            if (Buffer.Count == 0) return;
            if (listView1.SelectedIndices.Count == 0)
            {
                foreach (Block block in Buffer)
                    Project.TAP.Add(block);
            }
            else
            {
                //Там-то было легко... только добавить к концу, здесь же сначала надо раздвинуть блоки
                for (int i = 0; i < Buffer.Count; i++)
                    Project.TAP.Add(null);
                for (int i = Project.TAP.Count - 1; i >= listView1.SelectedIndices[0] + Buffer.Count; i--)
                    Project.TAP[i] = Project.TAP[i - Buffer.Count];
                for (int i = 0; i < Buffer.Count; i++)
                    Project.TAP[listView1.SelectedIndices[0] + i] = Buffer[i];
            }
            Project.Change();
            DrawTap();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------
        //Отображение содержимого файла в листвью
        void DrawTap()
        {
            string NullString = "- - - - -";
            listView1.Items.Clear();
            int files = 0;
            int bytes = 0;
            int fullbytes = 0;
            foreach (Block block in Project.TAP)
            {
                if (block.FileTitle != null)
                {
                    //Нормальный файл с именем (но может быть и без самого блока)
                    listView1.Items.Add(block.FileType);
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(block.FileName);
                    if (block.FileTitle[1] == 0)
                        if (block.FileTitle[14] + block.FileTitle[15] * 256 < 10000)
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add("Basic line " + (block.FileTitle[14] + block.FileTitle[15] * 256).ToString()); //Адрес
                        else
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add("No run");
                    if (block.FileTitle[1] == 3)
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add((block.FileTitle[14] + block.FileTitle[15] * 256).ToString()); //Адрес
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add((block.FileTitle[12] + block.FileTitle[13] * 256).ToString()); //Размер
                    if (block.FileData != null)
                    {
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add((block.FileData.Count() - 2).ToString());
                        AddCRC(block.CRCOK);
                    }
                    else
                    {
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(NullString);
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(NullString);
                    }
                }
                else
                {
                    //Блок без имени
                    if (block.FileData[0] == 0)
                        listView1.Items.Add(block.FileData[0].ToString());
                    else
                        listView1.Items.Add("    Блок данных");
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(NullString);
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(NullString);
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(NullString);
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add((block.FileData.Count() - 2).ToString());
                    AddCRC(block.CRCOK);
                }
                if (block.FileTitle != null) fullbytes += 17;
                if (block.FileData != null)
                {
                    files++;
                    bytes += block.FileData.Count() - 2;
                    fullbytes += block.FileData.Count() - 2;
                }
            }
            //Подсчёт количества блоков
            toolStripStatusLabel2.Text = "Файлов в проекте: " + files;
            toolStripStatusLabel3.Text = "Объём: " + bytes + " байт";
            toolStripStatusLabel4.Text = "Полный объём: " + fullbytes + " байт";
            SetFormText();
        }
        //Добавление строки со статусом CRC
        void AddCRC(bool OK)
        {
            if (OK)
                listView1.Items[listView1.Items.Count - 1].SubItems.Add("OK");
            else
                listView1.Items[listView1.Items.Count - 1].SubItems.Add("Failed");
        }
        //Возврат строки с числом из заголовка с адресом
        string NumInFileTitle(Block block, int adress)
        {
            int x = block.FileTitle[adress] + block.FileTitle[adress + 1] * 256;
            return x.ToString();
        }
        //Разбивание файлов на отдельные блоки
        private void разбитьНаБлокиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Block> tempfile = new List<Block>();
            foreach (Block block in Project.TAP)
                tempfile.Add(block);
            Project.TAP.Clear();
            foreach (Block block in tempfile)
            {
                if (block.FileTitle != null)
                    Project.TAP.Add(new Block(block.FileTitle));
                if (block.FileData != null)
                    Project.TAP.Add(new Block(block.FileData));
            }
            DrawTap();
        }
        //Собирание отдельных блоков в файлы
        private void объединитьБлокиВФайлыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Block> tempfile = new List<Block>();
            foreach (Block block in Project.TAP)
                tempfile.Add(block);
            Project.TAP.Clear();
            for (int i = 0; i < tempfile.Count(); i++)
            {
                if (tempfile[i].FileTitle != null & tempfile[i].FileData == null & i < tempfile.Count() - 1 &&
                    tempfile[i + 1].FileTitle == null & tempfile[i + 1].FileData != null)
                {
                    Project.TAP.Add(new Block(tempfile[i].FileTitle));
                    Project.TAP[Project.TAP.Count() - 1].AddBlock(tempfile[i + 1].FileData);
                    i++;
                }
                else
                    Project.TAP.Add(tempfile[i]);
            }
            DrawTap();
        }
        //Удаление выделенного блока
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) { Program.Message("Ничего не выделено"); return; }
            for (int i = listView1.SelectedIndices.Count - 1; i >= 0; i--)
                Project.TAP.RemoveAt(listView1.SelectedIndices[i]);
            Project.Change();
            DrawTap();
        }
        //Добавление файла в проект
        private void tAPфайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = Program.FileTypeTAP;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            LoadTapFile(openFileDialog1.FileName);
            Project.Change();
            DrawTap();
        }
        //Движение блоков вверх
        private void подвинутьВверхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Сначала проверим, нормально ли выделено
            if (!NormalSelection()) return;
            if (listView1.SelectedIndices[0] == 0) return; //Двигаться некуда...
            //Теперь мы уверены, что выделено всё правильно, можно двигать
            //Запоминаем временный блок, который потом появится "снизу"
            Block temp = Project.TAP[listView1.SelectedIndices[0] - 1];
            for (int i = 0; i < listView1.SelectedIndices.Count; i++)
                Project.TAP[listView1.SelectedIndices[i] - 1] = Project.TAP[listView1.SelectedIndices[i]];
            Project.TAP[listView1.SelectedIndices[listView1.SelectedIndices.Count - 1]] = temp;
            //Теперь нвдо запомнить какие строчки были выделены
            List<int> sel = new List<int>();
            foreach (int i in listView1.SelectedIndices)
                sel.Add(i);
            //Делаем изменения на экране (и в мозгах)
            Project.Change();
            DrawTap();
            //Теперь восстанавливаем выделение, но на строчку выше
            foreach (int i in sel)
                listView1.Items[i - 1].Selected = true;
        }
        //Проверка на "Нормальность" выделения, чтобы, например, избежать "дыр"
        bool NormalSelection()
        {
            if (listView1.SelectedIndices.Count == 0) { Program.Message("Ничего не выделено"); return false; }
            if (listView1.SelectedIndices.Count == 1) return true; //Если выделен только один - уже хорошо
            bool Normal = true;
            for (int i = 0; i < listView1.SelectedIndices.Count - 1; i++)
                if (listView1.SelectedIndices[i] + 1 != listView1.SelectedIndices[i + 1]) Normal = false;
            if (!Normal) { Program.Message("Для этого блоки должны быть выделены последовательно"); return false; }
            return true;
        }

        private void подвинутьВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Сначала проверим, нормально ли выделено
            if (!NormalSelection()) return;
            if (listView1.SelectedIndices[listView1.SelectedIndices.Count - 1] == listView1.Items.Count - 1) return; //Двигаться некуда...
            //Теперь мы уверены, что выделено всё правильно, можно двигать
            //Запоминаем временный блок, который потом появится "снизу"
            Block temp = Project.TAP[listView1.SelectedIndices[listView1.SelectedIndices.Count - 1] + 1];
            for (int i = listView1.SelectedIndices.Count - 1; i >= 0 ; i--)
                Project.TAP[listView1.SelectedIndices[i] + 1] = Project.TAP[listView1.SelectedIndices[i]];
            Project.TAP[listView1.SelectedIndices[0]] = temp;
            //Теперь нвдо запомнить какие строчки были выделены
            List<int> sel = new List<int>();
            foreach (int i in listView1.SelectedIndices)
                sel.Add(i);
            //Делаем изменения на экране (и в мозгах)
            Project.Change();
            DrawTap();
            //Теперь восстанавливаем выделение, но на строчку выше
            foreach (int i in sel)
                listView1.Items[i + 1].Selected = true;
        }

        private void menuexit_Click(object sender, EventArgs e) { this.Close(); }

        //Отменить
        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Project.hIndex < 2) return;
            Project.hIndex--;
            Project.TAP.Clear();
            foreach (Block block in Project.history[Project.hIndex - 1])
                Project.TAP.Add(block);
            Project.changed = true;
            DrawTap();
        }
        //Повторить
        private void вернутьтожеПокаНеРаботаетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Project.hIndex == Project.history.Count) return;
            Project.hIndex++;
            Project.TAP.Clear();
            foreach (Block block in Project.history[Project.hIndex - 1])
                Project.TAP.Add(block);
            Project.changed = true;
            DrawTap();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) { отменитьToolStripMenuItem_Click(null, null); }
        private void toolStripButton2_Click(object sender, EventArgs e) { вернутьтожеПокаНеРаботаетToolStripMenuItem_Click(null, null); }

        private void изWAVфайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTapeLoad form = new FormTapeLoad();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Project.Change();
                DrawTap();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            изWAVфайлаToolStripMenuItem_Click(null, null);
        }

        private void просмотрФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 1)
            {
                Program.Message("Должен быть выделен один блок.");
                return;
            }
            if (Project.TAP[listView1.SelectedIndices[0]].FileData == null)
            {
                Program.Message("В этом блоке только заголовок.");
                return;
            }
            Project.view = Project.TAP[listView1.SelectedIndices[0]];
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
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Экспорт в WAV-файл";
            saveFileDialog1.Filter = Program.FileTypeWAV;
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
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
                System.IO.BinaryWriter file = new System.IO.BinaryWriter(new System.IO.FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create));
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
        //Импорт из TZX
        private void tZXфайлпокаНеРаботаетToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = Program.FileTypeTZX;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            TZXload(openFileDialog1.FileName);
            Project.Change();
            DrawTap();
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
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = Program.FileTypeTZX;
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            try
            {
                System.IO.BinaryWriter file = new System.IO.BinaryWriter(new System.IO.FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create));
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
                block.CRCOK = true;
            }
            Project.Change();
            DrawTap();
            if (action) Program.Message("Контрольные суммы исправлены.");
            else Program.Message("Все контрольные суммы и так в норме.");
        }
        //Поиск дубликатов блоков
        private void поискДубликатовToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //Хрен знает зачем, но Стас попросил сделать такую возможность
            bool find = false;
            //УБираем выделения (тупой способ, но не знаю как проще)
            for (int i = 0; i < listView1.Items.Count; i++) listView1.Items[i].Selected = false;
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
                                listView1.Items[i].Selected = true;
                                listView1.Items[j].Selected = true;
                            }
                    if (find) break;
                }
            }
            if (find) Program.Message("Найдены дубликаты файлов. Они отмечены выделением.");
            else Program.Message("Дубликаты не обнаружены.");
        }
        //Переименование файла
        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 1)
            {
                Program.Message("Должен быть выделен один блок.");
                return;
            }
            if (Project.TAP[listView1.SelectedIndices[0]].FileTitle == null)
            {
                Program.Message("В этом блоке нет заголовка.");
                return;
            }
            //Переименование
            Project.rename = Project.TAP[listView1.SelectedIndices[0]].FileName;
            FormInput form = new FormInput();
            if (form.ShowDialog() != DialogResult.OK) return;
            //Тут доделать
            if (Project.rename.Length > 10) Project.rename = Project.rename.Substring(0, 10);
            char[] str = Project.rename.ToCharArray();
            int currentblock = listView1.SelectedIndices[0];
            //Сначала сотрём то что было, чтоб не оставалось артефактов
            for (int i = 0; i < 10; i++)
                Project.TAP[currentblock].FileTitle[i + 2] = 32;
            //Накатываем новое имя
            for (int i = 0; i < Project.rename.Length; i++)
                Project.TAP[currentblock].FileTitle[i + 2] = (byte)str[i];
            Project.TAP[currentblock].FileName = Project.rename;
            //После переименования починим CRC
            FixCRC(Project.TAP[listView1.SelectedIndices[0]].FileTitle);
            Project.Change();
            DrawTap();
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) { просмотрФайлаToolStripMenuItem_Click(null, null); }
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e) { menucut_Click(null, null); }
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e) { menucopy_Click(null, null); }
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e) { menupaste_Click(null, null); }
        private void переименоватьToolStripMenuItem1_Click(object sender, EventArgs e) { переименоватьToolStripMenuItem_Click(null, null); }
        private void toolStripButton4_Click(object sender, EventArgs e) { подвинутьВверхToolStripMenuItem_Click(null, null); }
        private void toolStripButton6_Click(object sender, EventArgs e) { подвинутьВнизToolStripMenuItem_Click(null, null); }
        private void toolStripButton5_Click(object sender, EventArgs e) { удалитьToolStripMenuItem_Click(null, null); }

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
                Project.name = file;
                SetFormText();
                Project.New();
                LoadTapFile(Project.name);
                DrawTap();
                return;
            }
            if (ext == ".tzx" & SaveQuestion())
            {
                Project.name = Program.FileUnnamed;
                SetFormText();
                Project.New();
                TZXload(file);
                DrawTap();
                return;
            }
            MessageBox.Show("Файл не поддерживается", "Taper");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Count() == 1) return;
            string file = args[1];
            string ext = System.IO.Path.GetExtension(file).ToLower();
            if (ext == ".tap" & SaveQuestion())
            {
                Project.name = file;
                SetFormText();
                Project.New();
                LoadTapFile(Project.name);
                DrawTap();
                return;
            }
            if (ext == ".tzx" & SaveQuestion())
            {
                Project.name = Program.FileUnnamed;
                SetFormText();
                Project.New();
                TZXload(file);
                DrawTap();
                return;
            }
            MessageBox.Show("Файл не поддерживается", "Taper");
        }
    }
} //846, 820, 759