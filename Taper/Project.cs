using System.Collections.Generic;

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
            changed=false;
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
    }
}
