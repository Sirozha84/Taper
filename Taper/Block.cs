using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Taper
{
    class Block
    {
        //Описание
        public string FileType;
        public string FileName = "";
        public string Start;
        public string Len;
        public string Size;
        public bool CRCTitle;
        public bool CRCData;
        //Данные
        public byte[] FileTitle;
        public byte[] FileData;
        public Block(byte[] Bytes)
        {
            AddBlock(Bytes);
        }

        /// <summary>
        /// Добавление блока к файлу у которого есть имя
        /// </summary>
        /// <param name="bytes">Массив данных</param>
        public void AddBlock(byte[] bytes)
        {
            if (bytes[0] == 0 & bytes.Length >= 19)
            {
                //Добавление заголовка
                FileTitle = bytes;
                CRCTest(0, false);

                //Обрежем лишнее, бывает что заголовок содержит некий мусор выше 19-и байт, прямо после CRC
                Array.Resize(ref FileTitle, 19);

                //Парсим заголовок
                FileType = FileInfo(bytes, 0);
                FileName = FileInfo(bytes, 1);
                if (FileTitle[1] == 0)
                    if (FileTitle[14] + FileTitle[15] * 256 < 10000)
                        Start = "Basic " + (FileTitle[14] + FileTitle[15] * 256).ToString();
                    else
                        Start = "No run";
                if (FileTitle[1] == 3)
                    Start = (FileTitle[14] + FileTitle[15] * 256).ToString();
                Len = (FileTitle[12] + FileTitle[13] * 256).ToString();
                Size = (FileTitle[14] + FileTitle[15] * 256).ToString();
            }
            else
            {
                //Добавление блока данных
                FileData = bytes;
                CRCTest(1, false);
            }
        }

        /// <summary>
        /// Получение сведений о блоке данных
        /// </summary>
        /// <param name="bytes">Блок</param>
        /// <param name="format">Формат выходных данных: 0 - тип блока, 1 - имя файла, 2 - Тип: имя</param>
        /// <returns></returns>
        public static string FileInfo(byte[] bytes, int format)
        {
            string result = "";
            if (bytes.Length >= 19 && bytes[0] == 0)
            {
                if (format == 0 | format == 2)
                {
                    if (bytes[1] == 0) result = "Program";
                    if (bytes[1] == 1) result = "Number array";
                    if (bytes[1] == 2) result = "Character array";
                    if (bytes[1] == 3) result = "Bytes";
                }
                if (format == 2) result += ": ";
                if (format == 1 | format == 2)
                    for (int i = 2; i < 12; i++)
                        result += (char)bytes[i];
            }
            else result = "- - - - - " + (bytes.Length - 2).ToString() + " байт";
            return result;
        }

        /// <summary>
        /// Проверка контрольной суммы
        /// </summary>
        /// <param name="block">0 - заголовок, 1 - данные</param>
        /// /// <param name="fix">Чинить?</param>
        /// <returns></returns>
        public void CRCTest(byte block, bool fix)
        {
            byte[] d;
            if (block == 0) d = FileTitle; else d = FileData;
            byte sum = 0;
            for (int i = 0; i < d.Length - 1; i++) sum ^= d[i];
            if (fix) d[d.Length - 1] = sum;
            bool res = d[d.Length - 1] == sum;
            if (block == 0) CRCTitle = res; else CRCData = res;
        }

        /// <summary>
        /// Вывод информации о контрольных суммах файла/блоков
        /// </summary>
        /// <returns></returns>
        public string CRCview()
        {
            if (FileTitle != null & FileData != null) return (CRCTitle ? "OK, " : "Fail, ")+(CRCData ? "OK" : "Fail");
            if (FileTitle != null & FileData == null) return CRCTitle ? "OK" : "Fail";
            if (FileTitle == null & FileData != null) return CRCData ? "OK" : "Fail";
            return "";
        }
    }
}
