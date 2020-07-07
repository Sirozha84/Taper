using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Taper
{
    class Listener
    {
        const int accuracy = 10;    //Точность (число послдених волн, средняя которых вычисляется для сравнения со следующей)
        static string pos;  //Позиция в вавке
        static byte mode;   //Режим, (0 - поиск пилот-тона, 1 - ожидание преамбулы)
        static int cn;      //Нахождение волны (0 - ниже центра, 1 - выше центра)
        static int last;    //Предыдущее нахождение
        static int len;     //Счётчик нахождения волны в одной стороне
        static int len1;    //Длина первой части волны
        static List<int> lens = new List<int>(); //Список последних длин
        //static int firstCn; //С какой стороны волна начинается (0 - снизу вверх, 1 - сверху вниз, 2 - ещё не известно)
        static string result;   //Результат, отправляемый загрузчику

        /// <summary>
        /// Сброс всех данных и начало слушания новых
        /// </summary>
        public static void Init()
        {
            mode = 0;
        }

        /// <summary>
        /// Слушает кусок записи, возвращает результаты распознания
        /// </summary>
        /// <param name="data"></param>
        public static string Listen(string position, byte[] data)
        {
            pos = position;
            result = "";

            //Считаем длину волны
            foreach (byte a in data)
            {
                cn = a < 128 ? 0 : 1;
                if (cn == last)
                    len++;
                else
                {
                    CenterIntersection();
                    len = 0;
                    last = cn; 
                }
            }


            //result = DateTime.Now.ToString("hh:mm:ss") + "☺" + pos + "☺Hello!☺OK";
            //Надо понимать, если в один кусочек будет два события, отобразится только последнее
            //хотя за столь маленький отрезок в теории больше одного события не может быть.

            return result;
        }

        static void CenterIntersection()
        {
            lens.Add(len);
            if (lens.Count > accuracy) lens.RemoveAt(0);
            double avg = lens.Average();
            double percent = Math.Abs(avg - len) / avg;

            // Поиск пилот-тона. Предположим, мы не знаем длину пилот-тона, будем считать среднюю длину поступающих волн.
            // И если следующая длина не будет отходитьи от среднего, допустим больше чем на 10 процентов на протяжении,
            // допустим 10 штук - то тогда будем считать что идёт какая-то ровная волна, её и будем считать пилот-тоном.
            if (mode == 0)
            {
                if (lens.Count == accuracy & percent < 0.1)
                {
                    result = DateTime.Now.ToString("HH:mm:ss") + "☺" + pos + "☺Найден пилот-тон";
                    mode = 1;
                }
            }
            
            // После пилота найдена волна, значительно короче других, будем считать это преамбулой.
            // В какую сторону она "повёрнута", ту сторону и будем считать "первой"
            if (mode == 1)
            {
                if (percent > 0.3)
                {
                    result = DateTime.Now.ToString("HH:mm:ss") + "☺" + pos + "☺Найдена преамбула";
                    mode = 2;
                }
            }

            //После того как нашли первую "короткую" волну, ожидаемо, что далее будет короткая
            if (mode == 2)
            {

                /*if (percent > 1)
                {
                    result = DateTime.Now.ToString("HH:mm:ss") + "☺" + pos + "☺Сигнал потерян";
                    mode = 0;
                }*/
            }

        }
    }
}
