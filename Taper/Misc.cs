using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taper
{
    static class Misc
    {
        static string[] hex = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

        /// <summary>
        /// Перевод одного байта в строку
        /// </summary>
        /// <param name="b">байт</param>
        /// <param name="System">Система исчесления, 10 или 16</param>
        /// <param name="Probels"></param>
        /// <returns></returns>
        public static string BTS(byte b, byte System)
        {
            if (System == 10)
            {
                if (b < 10) return "  " + b.ToString();
                if (b < 100) return " " + b.ToString();
                return b.ToString();
            }
            if (System == 16)
                return hex[(b & 240) / 16] + hex[b & 15];
            return "Error";
        }

        /// <summary>
        /// Перевод двух байт в строку
        /// </summary>
        /// <param name="b1">байт 1</param>
        /// <param name="b2">байт 2</param>
        /// <param name="System">Система исчесления, 10 или 16</param>
        /// <param name="Probels"></param>
        /// <returns></returns>
        public static string BTS(byte b1, byte b2, byte System, bool Probels)
        {
            int i = b1 + b2 * 256;
            if (System == 10)
            {
                if (Probels)
                {
                    if (i < 10) return "    " + i.ToString();
                    if (i < 100) return "   " + i.ToString();
                    if (i < 1000) return "  " + i.ToString();
                    if (i < 10000) return " " + i.ToString();
                }
                return i.ToString();
            }
            if (System == 16)
                return hex[(i & 61440) / 4096] + hex[(i & 3840) / 256] + hex[(i & 240) / 16] + hex[i & 15];
            return "Error";
        }
    }
}
