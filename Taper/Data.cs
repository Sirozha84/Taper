using System.Linq;

namespace Taper
{
    static class Data
    {
        public static string Text(byte[] data)
        {
            if (data == null) return "";
            string text = "";
            for (int i = 1; i < data.Count() - 1; i++) text += chr(data[i]);
            return text;
        }

        public static string Bytes(byte[] data, int start, byte Sys)
        {
            int a = start;
            if (data == null) return "";
            string text = "";
            string txt = "";
            for (int i = 1; i < data.Count() - 1; i++)
            {
                //Добавление колонки с адресом
                if (i % Sys == 1) text += Misc.BTS(a % 256, a / 256, Sys, true) + ": ";

                //Добавление колонки с данными
                text += Misc.BTS(data[i], Sys) + " ";
                txt += chr(data[i]);

                //Если данные кончились а строка нет - добавляем пробелов
                if (i == data.Count() - 2)
                {
                    while (i % Sys != 0)
                    {
                        i++;
                        text += Sys == 10 ? "    " : "   ";
                    }
                }

                //Добавление char-колонки
                if (i % Sys == 0) 
                {
                    text += "| " + txt + (char)13 + (char)10;
                    txt = "";
                }

                a++;
            }
            return text;
        }

        static char chr(byte b)
        {
            if (b >= 32 & b <= 127) return (char)b;
            return ' ';// '□';
        }
    }
}
