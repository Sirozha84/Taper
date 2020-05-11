using System;
using System.Windows.Forms;

namespace Taper
{
    static class Program
    {
        public const string Name = "Taper";
        public const string Version = "2.1 - 9 июня 2015 года";
        //Параметры открытия и сохранения файлов
        public const string FileUnnamed = "Безымянный";
        public const string FileTypeTAP = "Образ ленты (*.tap)|*.tap|Все файлы (*.*)|*.*";
        public const string FileTypeTZX = "Образ ленты (*.tzx)|*.tzx|Все файлы (*.*)|*.*";
        public const string FileTypeWAV = "Звуковой файл (*.wav)|*.wav|Все файлы (*.*)|*.*";
        public const string FileTypeBMP = "Точечный рисунок (*.bmp)|*.bmp|Все файлы (*.*)|*.*";

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            //Application.Run(new FormTapeLoad());
        }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        /// <param name="message">Текст</param>
        public static void Error(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Информационное сообщение
        /// </summary>
        /// <param name="message">Текст</param>
        public static void Message(string message)
        {
            MessageBox.Show(message, Program.Name);
        }
    }
}
