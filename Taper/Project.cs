﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Taper
{
    class Project
    {
        
        public static string name = "";                     //Имя проекта
        public static bool changed;                         //Флаг изменения
        public static List<Block> TAP = new List<Block>();  //Собственно TAP-файл
        public static string rename = "";                   //Пока не помню сути этой переменной.....
        private static List<int> lastSelect = new List<int>();     //Буфер запоминания выделения

        //Временно эти поля публичные, потом перенесу управление историей сюда
        public static List<List<Block>> history = new List<List<Block>>();  //История изменений проекта
        public static int hIndex;                           //Позиция в истории
        public static List<Block> Buffer = new List<Block>(); //Буфер обмена

        /// <summary>
        /// Создание нового проекта, запускается так же и перед открытием файла
        /// </summary>
        public static void New()
        {
            TAP.Clear();
            name = "";
            Change(true);
        }

        public static void Open(string filename, bool newProject)
        {
            if (newProject) New();
            bool loaded = false;
            string ext = Path.GetExtension(filename).ToLower();
            try
            {
                if (ext == ".tap")
                {
                    //Открываем TAP-файл
                    BinaryReader file = new BinaryReader(new FileStream(filename, FileMode.Open));
                    while (file.BaseStream.Position < file.BaseStream.Length)
                    {
                        int LEN = file.ReadUInt16();
                        byte[] Bytes = file.ReadBytes(LEN);
                        Add(Bytes);
                    }
                    file.Close();
                    if (newProject) name = filename;
                    loaded = true;
                }
                if (ext == ".tzx")
                {
                    //Открываем TZX-файл
                    BinaryReader file = new BinaryReader(new FileStream(filename, FileMode.Open));
                    file.ReadBytes(10); //10 байт какой-то херни в начале файла
                    while (file.BaseStream.Position < file.BaseStream.Length)
                    {
                        file.ReadBytes(3); //3 байта какой-то херни в начале каждого блока
                        int LEN = file.ReadUInt16();
                        byte[] Bytes = file.ReadBytes(LEN);
                        Add(Bytes);
                    }
                    file.Close();
                    if (newProject) name = filename;
                    loaded = true;
                }
                Change(newProject);
            }
            catch { }

            if (!loaded)
            {
                Program.Error(Lang.errorLoad);
                New();
            }
        }

        public static void Save(string filename)
        {
            string ext = Path.GetExtension(filename).ToLower();
            bool save = false;
            try
            {
                if (ext == ".tap")
                {
                    //По умолчанию сохраняем в TAP-формате
                    BinaryWriter file = new BinaryWriter(new FileStream(filename, FileMode.Create));
                    foreach (Block block in TAP)
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
                    name = filename;
                    changed = false;
                    save = true;
                }
                if (ext == ".tzx")
                {
                    //Сохраняем в TZX-формат
                    BinaryWriter file = new BinaryWriter(new FileStream(filename, FileMode.Create));
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
                    foreach (Block block in TAP)
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
                    name = filename;
                    changed = false;
                    save = true;
                }
            }
            catch { }
            if (!save) Program.Error(Lang.errorSave);
        }

        /// <summary>
        /// Создание точки отмены. Вызывается после изменения, загрузки или начала нового проекта.
        /// </summary>
        /// <param name="newProject">Это новый проект?</param>
        public static void Change(bool newProject)
        {
            //Если это новый или загруженный проект, сбрасываем историю изменений
            if (newProject)
            {
                history.Clear();
                hIndex = -1;
                changed = false;
            }

            //Если индекс истории меньше чем размер истории, удаляем последующие моменты
            //while (history.Count > hIndex) history.RemoveAt(history.Count - 1);
            history.RemoveRange(hIndex + 1, history.Count - hIndex - 1); // - Проверить

            //Создание точки отмены
            List<Block> temp = new List<Block>();
            foreach (Block block in TAP) temp.Add(block.Copy());
            history.Add(temp);
            hIndex++;
            changed = !newProject;
        }

        /// <summary>
        /// Отменить
        /// </summary>
        public static void Undo()
        {
            hIndex--;
            TAP.Clear();
            foreach (Block block in history[hIndex]) TAP.Add(block.Copy());
            changed = true;
        }

        /// <summary>
        /// Вернуть
        /// </summary>
        public static void Redo()
        {
            hIndex++;
            TAP.Clear();
            foreach (Block block in history[hIndex]) TAP.Add(block.Copy());
            changed = true;
        }

        /// <summary>
        /// Добавление блока в проект
        /// </summary>
        /// <param name="Bytes"></param>
        public static void Add(byte[] Bytes)
        {
            TAP.Add(new Block(Bytes));
        }

        /// <summary>
        /// Собрать блоки по файлам
        /// </summary>
        public static void ListFiles()
        {
            List<Block> tempfile = new List<Block>();
            foreach (Block block in TAP)
                tempfile.Add(block);
            TAP.Clear();
            for (int i = 0; i < tempfile.Count(); i++)
            {
                if (tempfile[i].FileTitle != null & tempfile[i].FileData == null & i < tempfile.Count() - 1 &&
                    tempfile[i + 1].FileTitle == null & tempfile[i + 1].FileData != null)
                {
                    TAP.Add(new Block(tempfile[i].FileTitle));
                    TAP[TAP.Count() - 1].AddBlock(tempfile[i + 1].FileData);
                    i++;
                }
                else
                    TAP.Add(tempfile[i]);
            }
        }

        /// <summary>
        /// Разбить файлы на блоки
        /// </summary>
        public static void ListBlocks()
        {
            List<Block> tempfile = new List<Block>();
            foreach (Block block in TAP)
                tempfile.Add(block);
            TAP.Clear();
            foreach (Block block in tempfile)
            {
                if (block.FileTitle != null)
                    TAP.Add(new Block(block.FileTitle));
                if (block.FileData != null)
                    TAP.Add(new Block(block.FileData));
            }
        }

        /// <summary>
        /// Вырезать
        /// </summary>
        /// <param name="selected">Коллекция выделенных элементов</param>
        public static void Cut(ListView.SelectedIndexCollection selected)
        {
            if (selected.Count == 0) return;
            Buffer.Clear();
            for (int i = 0; i < selected.Count; i++)
                Buffer.Add(TAP[selected[i]]);
            for (int i = selected.Count - 1; i >= 0; i--)
                TAP.RemoveAt(selected[i]);
            Change(false);
        }

        /// <summary>
        /// Копирование
        /// </summary>
        /// <param name="selected">Коллекция выделенных элементов</param>
        public static void Copy(ListView.SelectedIndexCollection selected)
        {
            if (selected.Count == 0) return;
            Buffer.Clear();
            for (int i = 0; i < selected.Count; i++)
                Buffer.Add(TAP[selected[i]]);
        }

        /// <summary>
        /// Вставить
        /// </summary>
        /// <param name="selected">Коллекция выделенных элементов</param>
        public static void Paste(ListView.SelectedIndexCollection selected)
        {
            if (Buffer.Count == 0) return;
            if (selected.Count == 0)
            {
                foreach (Block block in Buffer)
                    TAP.Add(block);
            }
            else
            {
                //Там-то было легко... только добавить к концу, здесь же сначала надо раздвинуть блоки
                for (int i = 0; i < Buffer.Count; i++)
                    TAP.Add(null);
                for (int i = TAP.Count - 1; i >= selected[0] + Buffer.Count; i--)
                    TAP[i] = TAP[i - Buffer.Count];
                for (int i = 0; i < Buffer.Count; i++)
                    TAP[selected[0] + i] = Buffer[i];
            }
            Change(false);
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="selected">Коллекция выделенных элементов</param>
        public static void Delete(ListView.SelectedIndexCollection selected)
        {
            if (selected.Count == 0) return;
            for (int i = selected.Count - 1; i >= 0; i--)
                TAP.RemoveAt(selected[i]);
            Change(false);
        }

        /// <summary>
        /// Переименование файла
        /// </summary>
        /// <param name="selected">Коллекция выделенных элементов</param>
        public static void Rename(ListView.SelectedIndexCollection selected)
        {
            if (selected.Count != 1) return;
            //Переименование
            rename = TAP[selected[0]].FileName;
            FormInput form = new FormInput();
            if (form.ShowDialog() != DialogResult.OK) return;
            //Тут доделать  //что доделать???
            if (rename.Length > 10) rename = rename.Substring(0, 10);
            char[] str = rename.ToCharArray();
            int currentblock = selected[0];

            // Добавим пустой заголовок Bytes: к блоку, если заголовка не было
            if (TAP[currentblock].FileTitle == null)
            {
                TAP[currentblock].FileTitle = new byte[19];
                TAP[currentblock].FileTitle[1] = 3;
                TAP[currentblock].FileType = "Bytes";
                TAP[currentblock].Start = "0";
                TAP[currentblock].Len = TAP[currentblock].Size;
                // -2 for flag and checksum
                TAP[currentblock].FileTitle[12] = (byte)((TAP[currentblock].FileData.Length - 2) % 256);
                TAP[currentblock].FileTitle[13] = (byte)((TAP[currentblock].FileData.Length - 2) / 256);
            }
            
            //Сначала сотрём то что было, чтоб не оставалось артефактов
            for (int i = 0; i < 10; i++)
                TAP[currentblock].FileTitle[i + 2] = 32;
            //Накатываем новое имя
            for (int i = 0; i < rename.Length; i++)
                TAP[currentblock].FileTitle[i + 2] = (byte)str[i];
            TAP[currentblock].FileName = rename;
            //После переименования починим CRC
            TAP[selected[0]].CRCTest(0, true);
            Change(false);
        }
        
        /// <summary>
        /// Исправление контрольных сумм
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static bool FixCRCs()
        {
            //Сначала проверим, будут ли изменения (есть ли несовпадения сумм)
            bool action = false;
            foreach (Block block in TAP)
            {
                action |= block.FileTitle != null && !block.CRCTitle;
                action |= block.FileData != null && !block.CRCData;
            }
            if (!action) return false;
            
            //Ошибки есть, чиним
            foreach (Block block in TAP)
            {
                if (block.FileTitle != null) block.CRCTest(0, true);
                if (block.FileData != null) block.CRCTest(1, true);
            }
            Change(false);
            return action;
        }

        /// <summary>
        /// Перемещение блоков ввеих
        /// </summary>
        /// <param name="selected"></param>
        public static bool MoveUp(ListView.SelectedIndexCollection selected)
        {
            //Сначала проверим, нормально ли выделено
            if (!NormalSelection(selected)) return false;
            if (selected[0] == 0) return false; //Двигаться некуда...
            RememberSelection(selected);
            //Теперь мы уверены, что выделено всё правильно, можно двигать
            //Запоминаем временный блок, который потом появится "снизу"
            Block temp = TAP[selected[0] - 1];
            for (int i = 0; i < selected.Count; i++)
                TAP[selected[i] - 1] = TAP[selected[i]];
            TAP[selected[selected.Count - 1]] = temp;
            Change(false);
            return true;
        }
        
        /// <summary>
        /// Перемещение блоков вниз
        /// </summary>
        /// <param name="selected"></param>
        public static bool MoveDown(ListView.SelectedIndexCollection selected)
        {
            //Сначала проверим, нормально ли выделено
            if (!NormalSelection(selected)) return false;
            if (selected[selected.Count - 1] == TAP.Count - 1) return false; //Двигаться некуда...
            RememberSelection(selected);
            //Теперь мы уверены, что выделено всё правильно, можно двигать
            //Запоминаем временный блок, который потом появится "снизу"
            Block temp = TAP[selected[selected.Count - 1] + 1];
            for (int i = selected.Count - 1; i >= 0; i--)
                TAP[selected[i] + 1] = TAP[selected[i]];
            TAP[selected[0]] = temp;
            Change(false);
            return true;
        }
        /// <summary>
        /// Проверка на последовательность выделения
        /// </summary>
        /// <returns></returns>
        static bool NormalSelection(ListView.SelectedIndexCollection selected)
        {
            if (selected.Count == 0) return false;
            if (selected.Count == 1) return true; //Если выделен только один - уже хорошо
            bool Normal = true;
            for (int i = 0; i < selected.Count - 1; i++)
                if (selected[i] + 1 != selected[i + 1]) Normal = false;
            if (!Normal) { Program.Message(Lang.msgSelectionMustBeContinuous); return false; }
            return true;
        }

        /// <summary>
        /// Запомнить выделение
        /// </summary>
        /// <returns></returns>
        static void RememberSelection(ListView.SelectedIndexCollection selected)
        {
            lastSelect.Clear();
            foreach (int i in selected)
                lastSelect.Add(i);
        }

        /// <summary>
        /// Восстановление выделения
        /// </summary>
        /// <param name="list">Листвью</param>
        /// /// <param name="shift">Сдвиг (-1, +1)</param>
        public static void RestroreSelection(ListView list, int shift)
        {
            foreach (int i in lastSelect)
                list.Items[i + shift].Selected = true;
        }

        /// <summary>
        /// Поиск дубликатов
        /// </summary>
        public static void FindDuplicates(ListView list)
        {
            bool find = false;
            //УБираем выделения (тупой способ, но не знаю как проще)
            for (int i = 0; i < list.Items.Count; i++) list.Items[i].Selected = false;
            for (int i = 0; i < TAP.Count() - 1; i++)
            {
                if (TAP[i].FileData != null)
                {
                    //То, с чем будем сравнивать всё остальное
                    byte[] block = TAP[i].FileData;
                    for (int j = i + 1; j < TAP.Count(); j++)
                        if (TAP[j].FileData != null)
                            if (TAP[i].FileData == TAP[j].FileData)
                            {
                                find = true;
                                list.Items[i].Selected = true;
                                list.Items[j].Selected = true;
                            }
                    if (find) break;
                }
            }
            if (find) Program.Message(Lang.msgFoundDuplicatesSelected);
            else Program.Message(Lang.msgDuplicatesNotFound);
        }
    }
}
