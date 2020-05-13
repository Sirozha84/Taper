using System;
using System.Collections.Generic;
using System.Linq;

namespace Taper
{
    class Project
    {
        
        public static string name = "";                     //Имя проекта
        public static bool changed;                         //Флаг изменения
        public static List<Block> TAP = new List<Block>();  //Собственно TAP-файл
        public static Block view;                           //Блок для просмотрщика
        public static string rename = "";                   //Пока не помню сути этой переменной.....

        //Временно эти поля публичные, потом перенесу управление историей сюда
        public static List<List<Block>> history = new List<List<Block>>();  //История изменений проекта
        public static int hIndex;                           //Позиция в истории

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
        /// Добавление произвольного блока в проект
        /// </summary>
        /// <param name="Bytes"></param>
        public static void Add(byte[] Bytes)
        {
            //Создаём блок с именем но без данных
            if (Bytes[0] == 0)
                TAP.Add(new Block(Bytes));
            else
            {
                if (TAP.Count > 0 && TAP[TAP.Count - 1].FileName != null & TAP[TAP.Count - 1].FileData == null)
                    TAP[TAP.Count - 1].AddBlock(Bytes); //Загружаем блок в последний файл
                else
                    TAP.Add(new Block(Bytes)); //Создаём блок без имени
            }
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
                    TAP[Project.TAP.Count() - 1].AddBlock(tempfile[i + 1].FileData);
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

    }
}
