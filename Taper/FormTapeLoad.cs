using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Taper
{
    public partial class FormTapeLoad : Form
    {
        string FileName = "";
        byte[] spectrum = new byte[100];
        Graphics Canvas; //Для графики
        Bitmap Buffer = new Bitmap(1, 1);
        string HightSam; //Для обозначения на рисунке максимальной и инимальной частоты
        string LowSam;
        List<byte[]> Blocks = new List<byte[]>(); //Найденные данные будут храниться тут...
        List<byte> Block = new List<byte>();
        string LOG = "";
        int GranVL; //Грани для определения категории волны
        int GranVR;
        int GranNL;
        int GranNR;
        //public List<Block> TAPFile = new List<Block>();
        public FormTapeLoad()
        {
            InitializeComponent();
        }
        //Загрузка файла и преобразование его в ряд байт
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = Program.FilterWAV;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            //Начинаем загрузку
            button1.Enabled = false;
            button4.Enabled = false;
            FileName = openFileDialog1.FileName;
            progressBar1.Style = ProgressBarStyle.Marquee;
            this.Refresh();
            if (!LoadWavFile()) return;
            //Загрузка типа завершена, подготовим форму
            button1.Enabled = true;
            button4.Enabled = true;
            button1.Text = openFileDialog1.FileName;
            button2.Enabled = true;
            Analise();
            button1.Text = openFileDialog1.FileName;
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            numericUpDown3.Enabled = true;
            numericUpDown4.Enabled = true;
            progressBar1.Style = ProgressBarStyle.Continuous;
        }
        //Преобразование двух байт в интеджер с минусом! Крутая функция, возможно ещё пригодится
        int TwoBytesToInt(byte b1, byte b2)
        {
            int result = b1 + b2 * 256;
            if (result >= 32768) result = result - 65536;
            return result;
        }
        //Загрузка файла и превращение его в удобочитаемый вид с одновременным анализом
        bool LoadWavFile()
        {
            Log(openFileDialog1.FileName);
            WAV.Data.Clear();
            byte[] wave;
            int Len;
            try
            {
                System.IO.BinaryReader File = new System.IO.BinaryReader(new System.IO.FileStream(FileName, System.IO.FileMode.Open));
                //Прочитаем сперва параметры файла
                File.ReadBytes(22);
                WAV.Channels = File.ReadInt16();
                WAV.Sampling = File.ReadInt32();
                File.ReadBytes(6);
                WAV.Bits = File.ReadInt16();
                File.ReadBytes(4);
                Len = File.ReadInt32();// +44;
                string Channels = "Моно";
                if (WAV.Channels == 2) Channels = "Стерео";
                Log(WAV.Bits.ToString() + " bit; " + Channels + "; " + WAV.Sampling.ToString() + " Hz");
                Log("Загрузка файла");
                //Грузим выборку
                wave = File.ReadBytes(Len);
                File.Close();
            }
            catch
            {
                Log("Сбой загрузки");
                Program.Error("Произошла ошибка при загрузке WAV-файла.");
                return false;
            }
            //Обрабатываем то что загрузилось
            bool readOK;
            for (int ii = 0; ii < Len; )
            {
                Application.DoEvents();
                int result = 0;
                readOK = true; //Вот, будем проверять всё ли прочиталось, а то попадаются 16-битные записи где не хватает второго байта
                for (int i = 0; i < WAV.Channels; i++) //Грузим каналы по очереди (вдруг больше 2-х будет)
                {
                    try
                    {
                        if (WAV.Bits == 8) result += wave[ii];
                        if (WAV.Bits == 16) { result += TwoBytesToInt(wave[ii], wave[ii + 1]); ii++; }
                    }
                    catch
                    {
                        readOK = false;
                    }
                    ii++;
                }
                if (!readOK) break; 
                result /= WAV.Channels;
                if (WAV.Bits == 16) { result += 32768; result /= 256; }
                try
                {
                    WAV.Data.Add((byte)result);
                }
                catch
                {
                    break;
                }
            }
            HightSam = WAV.Sampling.ToString() + " Hz";
            LowSam = (WAV.Sampling / 100).ToString() + " Hz";
                //Ищим ось (находим минимумы и максимумы, ровно между ними и будет ось)
            byte min = WAV.Data[0];
            byte max = WAV.Data[0];
            foreach (byte b in WAV.Data)
            {
                if (b < min) min = b;
                if (b > max) max = b;
            }
            byte OS = (byte)((min + max) / 2);
            //Преобразовываем выборку в "ровную пилу" _-__--_- для последующего удобства использования
            for (int i = 0; i<WAV.Data.Count;i++)
                if (WAV.Data[i] < OS) WAV.Data[i] = 0;
                else WAV.Data[i] = 1;
            return true;
        }
        //Анализ файла
        void Analise()
        {
            Log("Анализ спектра");
            spectrum = new byte[100];
            int lastlen = 0;
            int len = 0;
            int storona = 0; //(1 - выше оси, 0 - ниже оси)
            foreach (byte b in WAV.Data)
            {
                if ((b == 1 & storona == 1) | (b == 0 & storona == 0)) len++;
                if (len > 49) len = 49;
                if ((b == 0 & storona == 1) | (b == 1 & storona == 0))
                {
                    if (AiB(len, lastlen)) spectrum[len + lastlen]++;
                    lastlen = len;
                    len = 1;
                    storona = 1 - storona;
                }
            }
            //Расстановка границ (примерно)
            if (WAV.Sampling == 8000) { numericUpDown1.Value = 2; numericUpDown2.Value = 5; numericUpDown3.Value = 6; numericUpDown4.Value = 10; }
            if (WAV.Sampling == 11025) { numericUpDown1.Value = 3; numericUpDown2.Value = 8; numericUpDown3.Value = 9; numericUpDown4.Value = 17; }
            if (WAV.Sampling == 22050) { numericUpDown1.Value = 8; numericUpDown2.Value = 14; numericUpDown3.Value = 19; numericUpDown4.Value = 30; }
            if (WAV.Sampling == 44100) { numericUpDown1.Value = 13; numericUpDown2.Value = 26; numericUpDown3.Value = 37; numericUpDown4.Value = 58; }
            if (WAV.Sampling == 48000) { numericUpDown1.Value = 15; numericUpDown2.Value = 30; numericUpDown3.Value = 38; numericUpDown4.Value = 66; }
            if (WAV.Sampling == 88200) { numericUpDown1.Value = 32; numericUpDown2.Value = 50; numericUpDown3.Value = 75; numericUpDown4.Value = 100; }
            if (WAV.Sampling == 96000) { numericUpDown1.Value = 40; numericUpDown2.Value = 50; numericUpDown3.Value = 85; numericUpDown4.Value = 100; }
        }
        //Сравнение двух чисел
        bool AiB(int a, int b)
        {
            return ((float)Math.Min(a, b) / Math.Max(a, b) > 0.75f);
        }
        //Добавление строки в лог
        void Log(string str)
        {
            listBox1.Items.Add(System.DateTime.Now.ToString("HH:mm:ss - ") + str);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        //Обновление картинки
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (WAV.Sampling == 0) return;
            if (Buffer.Width != pictureBox1.Width | Buffer.Height != pictureBox1.Height)
            {
                Buffer = new Bitmap(pictureBox1.Width,pictureBox1.Height);
                Canvas = Graphics.FromImage(Buffer);
            }

            float x = (float)pictureBox1.Width / 100;
            float y = (float)pictureBox1.Height / 300;
            float ym = pictureBox1.Height - 22;
            Canvas.Clear(Color.White);
            Canvas.FillRectangle(Brushes.LightCyan, 0, 0, (int)numericUpDown4.Value * x, pictureBox1.Height);
            Canvas.FillRectangle(Brushes.White, 0, 0, (int)numericUpDown3.Value * x, pictureBox1.Height);
            Canvas.FillRectangle(Brushes.LightBlue, 0, 0, (int)numericUpDown2.Value * x, pictureBox1.Height);
            Canvas.FillRectangle(Brushes.White, 0, 0, (int)numericUpDown1.Value * x, pictureBox1.Height);
            for (int i = 0; i < 99; i++)
                Canvas.DrawLine(Pens.DarkGreen, i * x, ym - spectrum[i]*y, (i + 1) * x, ym - spectrum[i + 1]*y);
            Canvas.DrawString(HightSam, new Font("Arial", 10), Brushes.Black, 10, 10);
            Canvas.DrawString(LowSam, new Font("Arial", 10), Brushes.Black, pictureBox1.Width - 70, 10);
            this.pictureBox1.Image = Buffer;
        }
        //Блокировка разборкировка кнопок и элементов управления
        void ButtonsEnabled(bool en)
        {
            button1.Enabled = en;
            button2.Enabled = en;
            button3.Enabled = en;
            button4.Enabled = en;
            numericUpDown1.Enabled = en;
            numericUpDown2.Enabled = en;
            numericUpDown3.Enabled = en;
            numericUpDown4.Enabled = en;
            checkBox1.Enabled = en;
            checkBox2.Enabled = en;
            if (!en) progressBar1.Style = ProgressBarStyle.Marquee;
            else progressBar1.Style = ProgressBarStyle.Continuous;
        }
        //Вот тут-то жопа и начинается, начинаем рапознавание..........................................................
        void WAVLoad()
        {
            ButtonsEnabled(false);
            GranVL = (int)numericUpDown1.Value;
            GranVR = (int)numericUpDown2.Value;
            GranNL = (int)numericUpDown3.Value;
            GranNR = (int)numericUpDown4.Value;
            const int PilotCount = 100; //Количество волн для пилот-тона
            //System.IO.StreamWriter file = new System.IO.StreamWriter(Editor.ParametersFolder + "tmp", false);
            //if (LOG != "") file = new System.IO.StreamWriter(LOG, false);
            Log("Распознавание");
            //FullLog(file, "Начато распознавание файла " + FileName, 0);
            int len = 0;
            int storona = 0;
            int firststorona = 0; //Сторона, с которой начинается волна 0_-- или 1--_
            int status = 0; //0 - ничего, 1 - первый этап, 2 - второй этап
            int bits = 0; //Счётчик битов
            byte Byte = 0; //Последний считанный байт
            int pilot = 0; //Количество гребёнок пилота
            Blocks.Clear();
            //long position = -1;
            for (int i = 0; i< WAV.Data.Count - 1;i++)
            //foreach (byte b in WAV.Data)
            {
                Application.DoEvents();
                //Поиск пилот-тона
                /*if (checkBox1.Checked)
                    if (WAV.Data[i] == 1) FullLog(file, "   |", i);
                                     else FullLog(file, "|", i);*/
                switch(status)
                {
                    case 0:
                        //Ишем пилот-тон
                        if (WAV.Data[i] == 1 & storona == 0) storona = 1;
                        if (WAV.Data[i] == 0 & storona == 1) storona = 0;
                        //Плывём по пилот-тону
                        if (WAV.Data[i] == 1 & storona == 1) len++;
                        if (WAV.Data[i] == 0 & storona == 0) len++;
                        //Волна пересекает ось
                        if ((WAV.Data[i + 1] == 1 & storona == 0) | (WAV.Data[i + 1] == 0 & storona == 1))
                        {
                            if (len * 2 < GranNL)
                            {
                                status = 0; //Делаем на всякий случай сброс, если ничего не проканает, будем искать заново
                                //Найден подготовительный сигнал (найдена волна соответсвующая нулю)
                                if (pilot > PilotCount)
                                {
                                    //Вроде всё верно, переходим к фазе чтения данных
                                    status = 1;
                                    firststorona = storona;
                                }
                                else
                                    status = 0;
                            }
                            if (len * 2 >= GranNL) pilot++;
                            len = 0;
                            storona = 1 - storona;
                        }
                        break;
                    //Нашли первый кусок подготовительной волны, ждём пока кончится второй кусок
                    case 1:
                        if ((firststorona == 0 & WAV.Data[i + 1] == 0) | (firststorona == 1 & WAV.Data[i + 1] == 1))
                        {
                            //FullLog(file, "* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *", i);
                            //FullLog(file, "Найден подготовительный сигнал. Длина пилот-тона: " + pilot.ToString(), i);
                            //if (firststorona == 0 && checkBox1.Checked) FullLog(file, "--->", i); else FullLog(file, "<---", i);
                            pilot = 0;
                            Block.Clear();
                            status = 2;
                            len = 0;
                            bits = 0;
                            Byte = 0;
                        }
                        break;
                    //первый этап чтения бита
                    case 2:
                        if (WAV.Data[i] == firststorona) len++;
                        if (WAV.Data[i + 1] != firststorona) status = 3;
                        if (len > GranNR) status = 5;
                        break;
                    //второй этап чтения бита
                    case 3:
                        if (WAV.Data[i] != firststorona) len++;
                        if (WAV.Data[i + 1] == firststorona) status = 4;
                        if (len > GranNR) status = 5;
                        break;
                }
                //Волна "кончилась"
                if (status == 4 & Kat(len) == 2) status = 5;
                if (status == 4)
                {
                    if (Kat(len) == 0)
                    {
                        //0
                        //FullLog(file, "---- Длина волны " + len.ToString() + ". Бит 0", i);
                    }
                    if (Kat(len) == 1)
                    {
                        //1
                        //FullLog(file, "---- Длина волны " + len.ToString() + ". Бит 1", i);
                        if (bits == 0) Byte += 128;
                        if (bits == 1) Byte += 64;
                        if (bits == 2) Byte += 32;
                        if (bits == 3) Byte += 16;
                        if (bits == 4) Byte += 8;
                        if (bits == 5) Byte += 4;
                        if (bits == 6) Byte += 2;
                        if (bits == 7) Byte += 1;
                    }
                    bits++;
                    if (bits == 8)
                    {
                        //Байт сформировался
                        Block.Add(Byte);
                        //FullLog(file, "8 Бит прочитано, сформирован байт: " + Byte.ToString(), i);
                        bits = 0;
                        Byte = 0;
                    }
                    status = 2;
                    len = 0;
                }
                //Волна слишком длинная. Либо это конец данных, либо R Tape Loading Error
                if (status == 5)
                {
                    if (Block.Count() > 1)
                    {
                        //FullLog(file, "Сигнал потерян. Данные закончились. Блок в " + Block.Count() + " байт распознан.", i);
                        if (bits == 0) Log("Блок " + (Block.Count() - 2).ToString() + " байт: OK");
                        else
                        {
                            Log("R Tape loading error. Загружено байт: " + Block.Count());
                            Log("Ошибка произошла на минуте: " + (i / WAV.Sampling / 60).ToString() + "; секунде: " + ((float)i / WAV.Sampling % 60).ToString("0.000"));
                            Block.Add(0); //Добавляем байт контрольной суммы (так как последний таковым не является)
                        }
                        Blocks.Add(Block.ToArray());
                    }
                    status = 0;
                    storona = 0;
                }
                //И вот жопа, вроде как, закончилась...................................................................
            }
            Log("Завершено. Блоков найдено: " + Blocks.Count);
            Log("- - - - - - - - - -");
            //FullLog(file, "Завершено. Блоков найдено: " + Blocks.Count.ToString(), position);
            //file.Close();
            ButtonsEnabled(true);
        }
        //Определение к какой категории подходит волна с длиной LEN
        byte Kat(int LEN)
        {
            if (LEN >= GranNL & LEN <= GranNR) return 1;
            if (LEN >= GranVL & LEN <= GranVR) return 0;
            return 2;
        }        
        //Запись в полный лог в файл
        void FullLog(System.IO.StreamWriter file, string text, long position)
        {
            if (LOG == "") return;
            try
            {
                int minutes = (int)(position / WAV.Sampling / 60);
                float seconds = (float)position / WAV.Sampling % 60;
                file.WriteLine(minutes + ":" + seconds.ToString("00.000") + " - " + text);
            }
            catch { }
        }
        //Кнопка " LOAD"" "
        private void button2_Click(object sender, EventArgs e)
        {
            WAVLoad();
        }
        //Кнопка отмены
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        //Кнопка добавления файлов в прокт и выход
        private void button3_Click(object sender, EventArgs e)
        {
            int blokovdodobavlenia = Project.TAP.Count;

            foreach (byte[] block in Blocks) //о как, оказывается можно и так!
                if (block.Count() > 1)
                    Project.Add(block);
            //Почистим проект от блоков, не содержащих ни заголовка, ни тела
            for (int i = Project.TAP.Count - 1; i >= 0; i--)
            {
                if (Project.TAP[i].FileTitle == null & Project.TAP[i].FileData == null)
                    Project.TAP.RemoveAt(i);
            }

            if (Project.TAP.Count>blokovdodobavlenia)
                DialogResult = DialogResult.OK;
            this.Close();
        }
        //Рисование диапазона в герцах
        void DrawGranes()
        {
            label2.Text = "(" + (WAV.Sampling / numericUpDown1.Value).ToString("0") + " - " + (WAV.Sampling / numericUpDown2.Value).ToString("0") + "Hz)";
            label5.Text = "(" + (WAV.Sampling / numericUpDown3.Value).ToString("0") + " - " + (WAV.Sampling / numericUpDown4.Value).ToString("0") + "Hz)";
        }

        #region Управление контролами частот
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value <= numericUpDown1.Value)
            {
                numericUpDown2.Value = numericUpDown1.Value + 1;
                numericUpDown2_ValueChanged(null, null);
            }
            DrawGranes();
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value <= numericUpDown2.Value)
            {
                numericUpDown3.Value = numericUpDown2.Value + 1;
                numericUpDown3_ValueChanged(null, null);
            }
            if (numericUpDown1.Value >= numericUpDown2.Value)
            {
                numericUpDown1.Value = numericUpDown2.Value - 1;
                numericUpDown1_ValueChanged(null, null);
            }
            DrawGranes();
        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown4.Value <= numericUpDown3.Value)
            {
                numericUpDown4.Value = numericUpDown3.Value + 1;
                numericUpDown4_ValueChanged(null, null);
            }
            if (numericUpDown2.Value >= numericUpDown3.Value)
            {
                numericUpDown2.Value = numericUpDown3.Value - 1;
                numericUpDown2_ValueChanged(null, null);
            }
            DrawGranes();
        }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value >= numericUpDown4.Value)
            {
                numericUpDown3.Value = numericUpDown4.Value - 1;
                numericUpDown3_ValueChanged(null, null);
            }
            DrawGranes();
        }
        #endregion

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    checkBox2.Checked = false;
                    return;
                }
                LOG = saveFileDialog1.FileName;
                checkBox1.Enabled = true;
            }
            else
            {
                LOG = "";
                checkBox1.Enabled = false;
            }
        }
    }
    public class WAV
    {
        public static int Bits;
        public static int Channels;
        public static int Sampling;
        public static List<byte> Data = new List<byte>();
    }
}//505