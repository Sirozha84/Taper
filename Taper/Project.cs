using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Taper
{
    class Project
    {
        
        public static string name = "";                     //Имя проекта
        public static bool changed;                         //Флаг изменения
        public static List<Block> TAP = new List<Block>();  //Собственно TAP-файл
        public static Block view;                           //Блок для просмотрщика
        public static string rename = "";                   //Пока не помню сути этой переменной.....
        private static List<int> lastSelect = new List<int>();     //Буфер запоминания выделения

        //Временно эти поля публичные, потом перенесу управление историей сюда
        public static List<List<Block>> history = new List<List<Block>>();  //История изменений проекта
        public static int hIndex;                           //Позиция в истории
        static List<Block> Buffer = new List<Block>(); //Буфер обмена

        /// <summary>
        /// Создание нового проекта, запускается так же и перед открытием файла
        /// </summary>
        public static void New()
        {
            TAP.Clear();
            history.Clear();
            hIndex = 0;
            name = Program.FileUnnamed;
            changed = false;
        }

        public static void Open(string filename, bool add)
        {
            if (add) Change(); else New();
            try
            {
                System.IO.BinaryReader file = new System.IO.BinaryReader(new System.IO.FileStream(filename, System.IO.FileMode.Open));
                while (file.BaseStream.Position < file.BaseStream.Length)
                {
                    int LEN = file.ReadUInt16();
                    byte[] Bytes = file.ReadBytes(LEN);
                    Add(Bytes);
                }
                file.Close();
                if (!add) name = filename;
            }
            catch { Program.Error("Произошла ошибка при открытии файла."); }
        }

        public static void Save(string filename)
        {
            try
            {
                System.IO.BinaryWriter file = new System.IO.BinaryWriter(new System.IO.FileStream(filename, System.IO.FileMode.Create));
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
            }
            catch { Program.Error("Произошла ошибка во время сохранения файла. Файл не сохранён."); }
        }

        /// <summary>
        /// Вызывается при изменениях в проекте. Меняет флаг и делает отмену
        /// </summary>
        public static void Change()
        {
            //Если индекс истории меньше чем размер истории, удаляем последующие моменты
            while (hIndex < history.Count) history.RemoveAt(history.Count - 1);
            List<Block> temp = new List<Block>();
            foreach (Block block in TAP)
                temp.Add(block);
            history.Add(temp);
            hIndex++;
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
        /// Отменить
        /// </summary>
        public static void Undo()
        {
            if (hIndex < 2) return;
            hIndex--;
            TAP.Clear();
            foreach (Block block in history[hIndex - 1])
                TAP.Add(block);
            changed = true;
        }

        /// <summary>
        /// Вернуть
        /// </summary>
        public static void Redo()
        {
            if (hIndex == history.Count) return;
            hIndex++;
            TAP.Clear();
            foreach (Block block in history[Project.hIndex - 1])
                TAP.Add(block);
            changed = true;
        }

        /// <summary>
        /// Вырезать
        /// </summary>
        /// <param name="selected">Коллекция выделенных элементов</param>
        public static void Cut(ListView.SelectedIndexCollection selected)
        {
            if (selected.Count == 0) return;
            Change();
            Buffer.Clear();
            for (int i = 0; i < selected.Count; i++)
                Buffer.Add(TAP[selected[i]]);
            for (int i = selected.Count - 1; i >= 0; i--)
                TAP.RemoveAt(selected[i]);
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
            Change();
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
        }
        
        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="selected">Коллекция выделенных элементов</param>
        public static void Delete(ListView.SelectedIndexCollection selected)
        {
            if (selected.Count == 0) return;
            Change();
            for (int i = selected.Count - 1; i >= 0; i--)
                TAP.RemoveAt(selected[i]);
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
            Change();
            //Тут доделать  //что доделать???
            if (rename.Length > 10) rename = rename.Substring(0, 10);
            char[] str = rename.ToCharArray();
            int currentblock = selected[0];
            //Сначала сотрём то что было, чтоб не оставалось артефактов
            for (int i = 0; i < 10; i++)
                TAP[currentblock].FileTitle[i + 2] = 32;
            //Накатываем новое имя
            for (int i = 0; i < rename.Length; i++)
                TAP[currentblock].FileTitle[i + 2] = (byte)str[i];
            TAP[currentblock].FileName = rename;
            //После переименования починим CRC
            TAP[selected[0]].CRCTest(0, true);
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
            if (action) Change(); else return false;
            //Ошибки есть, чиним
            foreach (Block block in TAP)
            {
                if (block.FileTitle != null) block.CRCTest(0, true);
                if (block.FileData != null) block.CRCTest(1, true);
            }
            return true;
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
            Change();
            RememberSelection(selected);
            //Теперь мы уверены, что выделено всё правильно, можно двигать
            //Запоминаем временный блок, который потом появится "снизу"
            Block temp = TAP[selected[0] - 1];
            for (int i = 0; i < selected.Count; i++)
                TAP[selected[i] - 1] = TAP[selected[i]];
            TAP[selected[selected.Count - 1]] = temp;
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
            if (selected[selected.Count - 1] == selected.Count - 1) return false; //Двигаться некуда...
            Change();
            RememberSelection(selected);
            //Теперь мы уверены, что выделено всё правильно, можно двигать
            //Запоминаем временный блок, который потом появится "снизу"
            Block temp = TAP[selected[selected.Count - 1] + 1];
            for (int i = selected.Count - 1; i >= 0; i--)
                TAP[selected[i] + 1] = TAP[selected[i]];
            TAP[selected[0]] = temp;
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
            if (!Normal) { Program.Message("Переместить можно только рядом стоящие блоки"); return false; }
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

    }
}
