using System;
using System.Collections.Generic;
using System.Linq;

namespace Taper
{
    class Listener
    {
        const int accuracy = 10;    //Точность (число послдених волн, средняя которых вычисляется для сравнения со следующей)
        
        //Анализ волны
        static byte mode;   //Режим, (0 - поиск пилот-тона, 1/2 - ожидание 1/2 части преамбулы, 3/4 - ожидание 1/2 части бита)
        static int cn;      //Нахождение волны (0 - ниже центра, 1 - выше центра)
        static int last;    //Предыдущее нахождение
        
        //Анализ длин волн
        static int len;     //Счётчик нахождения волны в одной стороне
        static int len1;    //Длина первой части волны
        static List<int> lens = new List<int>(); //Список последних длин
        static int avglen;  //Средняя длина пилот-тона (для настройки скорости)

        //Собирание данных
        static bool[] bits;
        static byte nbit;
        static List<byte> bytes = new List<byte>();
        public static List<byte[]> blocks = new List<byte[]>();
        static string result;   //Результат, отправляемый загрузчику

        /// <summary>
        /// Сброс всех данных и начало слушания новых
        /// </summary>
        public static void Init()
        {
            mode = 0;
            blocks.Clear();
        }

        /// <summary>
        /// Слушает кусок записи, возвращает результаты распознания
        /// </summary>
        /// <param name="data"></param>
        public static string Listen(byte[] data)
        {
            result = "";

            //Считаем длину волны
            foreach (byte a in data)
            {
                cn = a < 130 ? 0 : 1; //Вот это "130" надо будет тоже корректировать
                if (cn == last)
                {
                    len++;
                    //Искуственно вызываем пересечение, так как в таком режиме длина не может превышать среднюю пилота
                    if (mode > 2 && len > avglen) CenterIntersection();
                }
                else
                {
                    CenterIntersection();
                    len = 0;
                    last = cn;
                }
            }

            return result;
        }

        static void CenterIntersection()
        {
            double avg = 0;  //Средняя длина волны пилот-тона
            double percent = 0;
            lens.Add(len);
            if (lens.Count > accuracy) lens.RemoveAt(0);
            if (lens.Count == accuracy)
            {
                avg = lens.Average();
                percent = Math.Abs(avg - len) / avg;
            }

            // Поиск пилот-тона. Предположим, мы не знаем длину пилот-тона, будем считать среднюю длину поступающих волн.
            // И если следующая длина не будет отходитьи от среднего, допустим больше чем на 10 процентов на протяжении,
            // допустим 10 штук - то тогда будем считать что идёт какая-то ровная волна, её и будем считать пилот-тоном.
            if (mode == 0)
            {
                if (lens.Count == accuracy & percent < 0.1) mode = 1;
            }
            
            // После пилота найдена волна, значительно короче других, будем считать это преамбулой.
            // В какую сторону она "повёрнута", ту сторону и будем считать "первой" 0.7
            if (mode == 1)
            {
                if (percent > 0.3)
                {
                    mode = 2;
                    avglen = (int)(avg * 2);
                }
                
                return;
            }

            //После того как нашли первую "короткую" волну, ожидаемо, что далее будет короткая
            if (mode == 2)
            {
                //Ещё нужно проверить длину преамбулы, может первая часть была норм, а на второй всё оборвалось...
                nbit = 0;
                bits = new bool[8];
                bytes.Clear();
                mode = 3;
                return;
            }

            //Ждём первую часть бита
            if (mode == 3)
            {
                len1 = len;
                if (len1 > avglen) AddBlock();
                else mode = 4;
                return;
            }

            //Ждём вторую часть бита
            if (mode == 4)
            {
                len1 += len;

                //Данных больше нет, заканчиваем блок
                if (len1 > avglen) AddBlock();
                else
                {
                    bits[nbit++] = len1 > avglen * 0.62;
                    //Вот это 0.62 нужно постоянно корректировать!
                    //Ещё нужно поизучать его расчёт, посмотреть как устроены турбо-записи
                    if (nbit > 7)
                    {
                        nbit = 0;
                        bytes.Add(BitsToByte());
                    }
                    mode = 3;
                }
            }
        }

        static byte BitsToByte()
        {
            byte b = 0;
            if (bits[0]) b += 128;
            if (bits[1]) b += 64;
            if (bits[2]) b += 32;
            if (bits[3]) b += 16;
            if (bits[4]) b += 8;
            if (bits[5]) b += 4;
            if (bits[6]) b += 2;
            if (bits[7]) b += 1;
            return b;
        }

        static void AddBlock()
        {
            if (bytes.Count > 1)
            {
                //Проверяем контрольную сумму
                byte crc = 0;
                foreach (byte b in bytes) crc ^= b;
                result = Block.FileInfo(bytes.ToArray(), 2) + (crc == 0 ? "☺OK" : "☺Fail");

                //Добавление блока во временный проект
                blocks.Add(bytes.ToArray());
            }
            //Обнуления считывателя и переход обратно в поиск пилот-тона
            lens.Clear();
            mode = 0;
        }
    }
}
