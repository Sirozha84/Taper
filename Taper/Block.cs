﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="Bytes">Массив данных</param>
        public void AddBlock(byte[] Bytes)
        {
            if (Bytes[0] == 0 & Bytes.Length >= 19)
            {
                //Добавление заголовка
                FileTitle = Bytes;
                CRCTitle = CRCTest(Bytes, false);

                //Обрежем лишнее, бывает что заголовок содержит некий мусор выше 19-и байт, прямо после CRC
                Array.Resize(ref FileTitle, 19);

                //Парсим заголовок
                switch (Bytes[1])
                {
                    case 0: FileType = "Program:"; break;
                    case 1: FileType = "Number array:"; break;
                    case 2: FileType = "Character array:"; break;
                    case 3: FileType = "Bytes:"; break;
                }
                for (int i = 2; i < 12; i++)
                    FileName += (char)Bytes[i];
                if (FileTitle[1] == 0)
                    if (FileTitle[14] + FileTitle[15] * 256 < 10000)
                        Start = "Basic line " + (FileTitle[14] + FileTitle[15] * 256).ToString();
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
                FileData = Bytes;
                CRCData = CRCTest(Bytes, false);
            }
        }
        /// <summary>
        /// Проверка контрольной суммы
        /// </summary>
        /// <param name="bytes">Массив данных</param>
        /// /// <param name="fix">Массив данных</param>
        /// <returns></returns>
        bool CRCTest(byte[] bytes, bool fix)
        {
            return bytes[8]>32;
            byte sum = 0;
            for (int i = 0; i < bytes.Length - 1; i++) sum ^= bytes[i];
            if (fix) bytes[bytes.Length - 1] = sum;
            return bytes[bytes.Length - 1] == sum;
        }

        /// <summary>
        /// Вывод информации о контрольных суммах файла/блоков
        /// </summary>
        /// <returns></returns>
        public string CRC()
        {
            if (FileTitle != null & FileData != null) return (CRCTitle ? "OK, " : "Fail, ")+(CRCData ? "OK" : "Fail");
            if (FileTitle != null & FileData == null) return CRCTitle ? "OK" : "Fail";
            if (FileTitle == null & FileData != null) return CRCData ? "OK" : "Fail";
            return "";
        }
    }
}
