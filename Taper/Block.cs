using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taper
{
    class Block
    {
        //Данные, как есть
        public byte[] FileTitle;
        public byte[] FileData;
        //"Человеческие" данные
        public string FileType;
        public string FileName = "";
        public bool CRCTitle;
        public bool CRCData;
        public Block(byte[] Bytes)
        {
            //По полученному массиву байт определяем заголовок это или блок данных
            if (Bytes[0] == 0)
            {
                //Если байт в блоке меньше чем надо, значит была ошибка в загрузке, пропускаем такой блок
                if (Bytes.Length >= 19)
                {
                    //Создаём файл с именем но без блока
                    FileTitle = Bytes;
                    //Обрежем лишнее, бывает что заголовок содержит некий мусор выше 19-и байт, прямо после CRC
                    Array.Resize(ref FileTitle, 19);

                    switch (Bytes[1])
                    {
                        case 0: FileType = "Program:"; break;
                        case 1: FileType = "Number array:"; break;
                        case 2: FileType = "Character array:"; break;
                        case 3: FileType = "Bytes:"; break;
                    }
                    for (int i = 2; i < 12; i++)
                        FileName += (char)Bytes[i];
                }
                CRCTitle = CRCTest(Bytes);
            }
            else
            {
                //Создаём файл с блоком но без имени
                FileData = Bytes;
                CRCData = CRCTest(Bytes);
            }
        }
        //Добавление блока к файлу у которого есть имя
        public void AddBlock(byte[] Bytes)
        {
            FileData = Bytes;
            CRCData = CRCTest(Bytes);
        }
        //Проверка контрольной суммы
        bool CRCTest(byte[] Bytes)
        {
            byte summ = 0;
            foreach (byte b in Bytes) summ ^= b;
            return (summ == 0);
        }
    }
}
