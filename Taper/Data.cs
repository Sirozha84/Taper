using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taper
{
    static class Data
    {
        public static string Text(byte[] data)
        {
            string text = "";
            for (int i = 1; i < data.Count() - 1; i++)
            {
                byte b = data[i];
                if (b == 13 | b == 10 | b >= 32) text += (char)b;
                else text += "_";
            }
            return text;
        }

        public static string Bytes(byte[] data, int Sys)
        {
            string text;
            if (Sys == 10)
            {
                text = "Адрес|     +1  +2  +3  +4  +5  +6  +7  +8  +9" + (char)13 + (char)10;
                text += "-----+---------------------------------------";
                int x = 0;
                int str = 0;
                for (int i = 1; i < data.Count() - 1; i++)
                {
                    if (x == 0)
                    {
                        text += (char)13;
                        text += (char)10;
                        //text += Misc.BTS(str * 10, 10); //Номер строки
                        text += "|";
                    }
                    text += Misc.BTS(data[i], 10) + " ";
                    x++;
                    if (x > 9) { x = 0; str++; }
                }

            }
            else
            {
                text = "Адрес|   +1 +2 +3 +4 +5 +6 +7 +8 +9 +A +B +C +D +E +F" + (char)13 + (char)10;
                text += "-----+-----------------------------------------------";
                int x = 0;
                int str = 0;
                for (int i = 1; i < data.Count() - 1; i++)
                {
                    if (x == 0)
                    {
                        text += (char)13;
                        text += (char)10;
                        text += (str * 16).ToString();//Misc.BTS(str * 16, 16, 2, true);
                        text += " |";
                    }
                    text += Misc.BTS(data[i], 16) + " ";
                    x++;
                    if (x > 15) { x = 0; str++; }
                }
            }
            return text;
        }
    }
}
