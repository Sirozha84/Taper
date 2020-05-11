using System.Collections.Generic;

namespace Taper
{
    class Project
    {
        //Сам тап-файл, собственной персоной
        public static List<Block> TAPfile = new List<Block>();
        //Блок для просмотрщика
        public static Block BlockView;
        //Пока не помню сути этой переменной.....
        public static string Rename = "";


        /// <summary>
        /// Добавление произвольного блока в проект
        /// </summary>
        /// <param name="Bytes"></param>
        public static void Add(byte[] Bytes)
        {
            //Создаём блок с именем но без данных
            if (Bytes[0] == 0)
                TAPfile.Add(new Block(Bytes));
            else
            {
                if (TAPfile.Count > 0 && TAPfile[TAPfile.Count - 1].FileName != null & TAPfile[TAPfile.Count - 1].FileData == null)
                    TAPfile[TAPfile.Count - 1].AddBlock(Bytes); //Загружаем блок в последний файл
                else
                    TAPfile.Add(new Block(Bytes)); //Создаём блок без имени
            }
        }
    }
}
