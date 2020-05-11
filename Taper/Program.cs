using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Taper
{
    static class Program
    {
        public const string Name = "Taper";
        public const string Version = "2.1 - 9 июня 2015 года";
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
    }
}
