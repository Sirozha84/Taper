using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Taper
{
    static class WAVmaker
    {
        public static List<byte> wav;

        /// <summary>
        /// Сохранение в WAV-файл
        /// </summary>
        public static void Save(string filename)
        {
            try
            {
                BinaryWriter file = new BinaryWriter(new FileStream(filename, FileMode.Create));
                file.Write('R');
                file.Write('I');
                file.Write('F');
                file.Write('F');
                file.Write((int)0); //Длина, пока пропустим
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
                file.Write((int)0); //Длина, пока пропустим

                int len = 0;
                foreach (Block block in Project.TAP)
                {
                    BlockToWav(block);
                    file.Write(wav.ToArray());
                    len += wav.Count();
                }
                //Вернёмся в те места, где нужно указать длину файла
                file.Seek(4, 0);
                file.Write(len + 26);
                file.Seek(40, 0);
                file.Write(len);

                file.Close();
            }
            catch { Program.Error("Произошла ошибка при сохранении файла. Файл не сохранён."); }
        }

        /// <summary>
        /// Добавление блока в WAV
        /// </summary>
        /// <param name="block"></param>
        public static void BlockToWav(Block block)
        {
            wav = new List<byte>();
            if (block.FileTitle != null)
            {
                MakeWav(block.FileTitle, 0);
            }
            if (block.FileData != null)
            {
                MakeWav(block.FileData, 1);
            }
        }


        /// <summary>
        /// Добавление блока в WAV: 0 - заголовок, 1 - блок
        /// </summary>
        static void MakeWav(byte[] block, byte Type)
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
                AddBitToWav(b & 128);
                AddBitToWav(b & 64);
                AddBitToWav(b & 32);
                AddBitToWav(b & 16);
                AddBitToWav(b & 8);
                AddBitToWav(b & 4);
                AddBitToWav(b & 2);
                AddBitToWav(b & 1);
            }
            //Пишем тишину после
            for (int i = 0; i < 30000; i++) wav.Add(127);
        }

        /// <summary>
        /// Добавление бита в выборку: 0, 1
        /// </summary>
        /// <param name="bit">Добавляемый бит</param>
        static void AddBitToWav(int bit)
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
    }
}
