using System;
using System.IO;

namespace Taper
{
    static class WAV
    {
        public static int channels; //Количество каналов
        public static int capacity; //Битность
        public static int sampling; //Частота дискретизации
        public static byte[] wave;  //Звуковые данные

        public static int Load(string file, int partLen)
        {
            int Len = 0;
            try
            {
                BinaryReader File = new BinaryReader(new FileStream(file, FileMode.Open));
                //Прочитаем сперва параметры файла
                File.ReadBytes(22);
                channels = File.ReadInt16();
                sampling = File.ReadInt32();
                File.ReadBytes(6);
                capacity = File.ReadInt16();
                File.ReadBytes(4);
                Len = File.ReadInt32();// +44;
                //Грузим выборку
                wave = File.ReadBytes(Len);
                //Искуственно увеличиваем длину данных, на случай если данные обрываются ровно в конце
                Array.Resize(ref wave, wave.Length + partLen);
                Len += partLen;
                File.Close();
            }
            catch
            {
                Program.Error("Произошла ошибка при загрузке WAV-файла.");
            }
            return Len;
        }

        /// <summary>
        /// Расчёт человеческого времени от номера семпла
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public static string Time(int sample)
        {
            int sec = sample / sampling;
            if (capacity == 16)
                sec = sample / sampling / 2;
            int min = sec / 60;
            sec = sec % 60;
            return min.ToString("00:") + sec.ToString("00");
        }
    }
}
