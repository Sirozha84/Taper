using System;
using System.Windows.Forms;

namespace Taper
{
    static class Program
    {
        public static string version = "3.1 (27.03.2022)";
        public static FormMain mainform;
        
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainform = new FormMain();
            Application.Run(mainform);
        }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        /// <param name="message">Текст</param>
        public static void Error(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Информационное сообщение
        /// </summary>
        /// <param name="message">Текст</param>
        public static void Message(string message)
        {
            MessageBox.Show(message, Application.ProductName);
        }
    }
}
