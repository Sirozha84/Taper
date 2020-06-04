﻿using System;
using System.Windows.Forms;

namespace Taper
{
    static class Program
    {
        public const string Name = "Taper";
        public const string Version = "3.0 Beta (03.06.2015)";
        //Параметры открытия и сохранения файлов
        public const string FileUnnamed = "Безымянный";
        public const string FilterAll = "Образ ленты (*.tap, *.tzx)|*.tap;*.tzx|Все файлы (*.*)|*.*";
        public const string FilterSel = "Образ ленты TAP (*.tap)|*.tap|Образ ленты TZX (*.tzx)|*.tzx|Все файлы (*.*)|*.*";
        public const string FilterWAV = "Звуковой файл (*.wav)|*.wav|Все файлы (*.*)|*.*";
        public const string FilterBMP = "Точечный рисунок (*.bmp)|*.bmp|Все файлы (*.*)|*.*";

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
            MessageBox.Show(message, Name);
        }
    }
}
