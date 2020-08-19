using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Taper
{
    public partial class FormViewer : Form
    {
        bool OnProcess = false;
        bool FirstStart = true; //При первом запуске ничего не делаем. Отменяется в DECcodes()
        public FormViewer()
        {
            InitializeComponent();
        }

        private void FormViewer_Shown(object sender, EventArgs e)
        {
            //Пытаемся автоматически понять что за файл просматривается, если непонятно - открываем в виде кода
            tabControl1.SelectedIndex = 5;
            if (Project.view.FileData == null)
            {
                Program.Error("В этом блоке нет данных");
                Close();
                return;
            }
            if (Project.view.FileData.Count() == 6914) tabControl1.SelectedIndex = 1;
            if (Project.view.FileData.Count() == 770) tabControl1.SelectedIndex = 2;
            if (Project.view.FileTitle != null && Project.view.FileTitle[1] == 0) tabControl1.SelectedIndex = 0;
            tabControl1_SelectedIndexChanged(null, null);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnProcess = false;
            switch (tabControl1.SelectedIndex)
            {
                case 0: ViewBasic(); break;
                case 1: ViewScreen(tabControl3.SelectedIndex); break;               //tabControl3_SelectedIndexChanged(null,null); break; //SCREEN$
                case 2: ViewFont(); break;
                case 3:
                    if (Project.view.FileTitle != null)
                        numericUpDown1.Value = Project.view.FileTitle[14] + Project.view.FileTitle[15] * 256;
                    else
                        numericUpDown1.Value = 16384;
                    textBox5.Text = Assembler.Disassembler(Project.view.FileData,0); break;
                case 4: ViewText(); break;
                case 5: tabControl2_SelectedIndexChanged(null, null); break; //Коды
            }
        }
        //Просмотр в виде Basic-программы
        void ViewBasic()
        {
            string[] Symbols = {"","","","","","","","","","","","","","","","","","","","","","","","","","","","","","", //0-29
                            "","","","","","","","","","","","","","","","","","","","","","","","","","","","","","", //30-59
                            "","","","","","","","","","","","","","","","","","","","","","","","","","","","","","", //60-89
                            "","","","","","","","","","","","","","","","","","","","","","","","","","","","","","", //90-119
                            "","","","","","","","","","","","","","","","","","","","","","","","","","","","","","", //120-149
                            "","","","","","","","","","","","","","",""," RND "," INKEY$ "," PI "," FN "," POINT ", //150-169
                            " SCREEN$ "," ATTR "," AT "," TAB "," VAL$ "," CODE "," VAL "," LEN "," SIN "," COS ", //170-179
                            " TAN "," ASN "," ACS "," ATN "," LN "," EXP "," INT "," SQR "," SGN "," ABS ", //180-189
                            " PEEK "," IN "," USR "," STR$ "," CHR$ "," NOT "," BIN "," OR "," AND ","<=", //190-199
                            ">=","<>"," LINE "," THEN "," TO "," STEP "," DEF FN "," CAT "," FORMAT ","", //200-209
                            ""," OPEN# "," CLOSE# "," MARGE "," VERIFY "," BEEP "," CIRCLE "," INK "," PAPER "," FLASH ", //210-219
                            " BRIGHT "," INVERSE "," OVER "," OUT "," LPRINT "," LLIST "," STOP "," READ "," DATA "," RESTORE ", //220-229
                            " NEW "," BORDER "," CONTINUE "," DIM "," REM "," FOR "," GO TO "," GO SUB "," INPUT "," LOAD ", //230-239
                            " LIST "," LET "," PAUSE "," NEXT "," POKE "," PRINT "," PLOT "," RUN "," SAVE "," RANDOMIZE ", //240-249
                            " IF "," CLS "," DRAW "," CLEAR "," RETURN "," COPY " //250-255
                           };
            OnProcess = true;
            string text = "";
            bool newstring = true;
            for (int i = 1; i < Project.view.FileData.Count() - 1; i++)
            {
                
                if (newstring)
                {
                    int num = (Project.view.FileData[i] * 256 + Project.view.FileData[i + 1]);
                    if (num > 9999) break;
                    if (num < 1000) text += " ";
                    if (num < 100) text += " ";
                    if (num < 10) text += " ";
                    text += num.ToString();
                    i+=3;
                    newstring = false;
                }
                else
                {
                    byte b = Project.view.FileData[i];
                    if (b == 13)
                    {
                        text += (char)13;
                        text += (char)10;
                        if (checkBoxSpaces.Checked)
                        {
                            text += (char)13;
                            text += (char)10;
                        }
                        newstring = true;
                    }
                    if (b == 14) i += 5;
                    if (b >= 32 & (b < 165)) text += (char)b;
                    if (b >= 165) text += Symbols[b];
                }
                if (!OnProcess) break;
            }
            textBox1.Text = text;
            OnProcess = false; ;
        }
        //Просмотр в виде текста
        void ViewText()
        {
            OnProcess = true;
            textBox2.Text = "Подготовка. Подождите немножко.";
            string text = "";
            for (int i = 1; i < Project.view.FileData.Count() - 1; i++)
            {
                byte b = Project.view.FileData[i];
                if (b == 13 | b == 10 | b >= 32) text += (char)b;
                else text += "_";
                if (!OnProcess) break;
            }
            textBox2.Text = text;
            Application.DoEvents();
            OnProcess = false;
        }
        //Вкладка просмотра кодов
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl2.SelectedIndex)
            {
                case 0: DECcodes(); break;
                case 1: HEXcodes(); break;
            }
        }
        void DECcodes()
        {
            if (FirstStart) { FirstStart = false; return; }
            if (OnProcess) return;
            OnProcess = true;
            textBox3.Text = "Подготовка. Подождите немножко.";
            string text = "Адрес|     +1  +2  +3  +4  +5  +6  +7  +8  +9" + (char)13 + (char)10;
                  text += "-----+---------------------------------------";
            int x = 0;
            int str = 0;
            for (int i = 1; i < Project.view.FileData.Count() - 1; i++)
            {
                if (x==0)
                {
                    text += (char)13;
                    text += (char)10;
                    //text += Misc.BTS(str * 10, 10); //Номер строки
                    text += "|";
                    Application.DoEvents();
                }
                text += Misc.BTS(Project.view.FileData[i], 10) + " ";
                x++;
                if (x > 9) { x = 0; str++; }
                if (!OnProcess) break;
            }
            textBox3.Text = text;
            OnProcess = false;
        }
        void HEXcodes()
        {
            if (OnProcess) return;
            OnProcess = true;
            textBox4.Text = "Подготовка. Подождите немножко.";
            string text = "Адрес|   +1 +2 +3 +4 +5 +6 +7 +8 +9 +A +B +C +D +E +F" + (char)13 + (char)10;
                  text += "-----+-----------------------------------------------";
            int x = 0;
            int str = 0;
            for (int i = 1; i < Project.view.FileData.Count() - 1; i++)
            {
                if (x == 0)
                {
                    text += (char)13;
                    text += (char)10;
                    text += (str * 16).ToString();//Misc.BTS(str * 16, 16, 2, true);
                    text += " |";
                    Application.DoEvents();
                }
                text += Misc.BTS(Project.view.FileData[i], 16) + " ";
                x++;
                if (x > 15) { x = 0; str++; }
                if (!OnProcess) break;
            }
            textBox4.Text = text;
            OnProcess = false;
        }

        //Просмотр в виде картинки
        void ViewScreen(int Mode)
        {
            int[] Adresses = {16384,16640,16896,17152,17408,17664,17920,18176,
                              16416,16672,16928,17184,17440,17696,17952,18208,
                              16448,16704,16960,17216,17472,17728,17984,18240,
                              16480,16736,16992,17248,17504,17760,18016,18272,
                              16512,16768,17024,17280,17536,17792,18048,18304,
                              16544,16800,17056,17312,17568,17824,18080,18336,
                              16576,16832,17088,17344,17600,17856,18112,18368,
                              16608,16864,17120,17376,17632,17888,18144,18400,
                              //----------------------------------------------
                              18432,18688,18944,19200,19456,19712,19968,20224,
                              18464,18720,18976,19232,19488,19744,20000,20256,
                              18496,18752,19008,19264,19520,19776,20032,20288,
                              18528,18784,19040,19296,19552,19808,20064,20320,
                              18560,18816,19072,19328,19584,19840,20096,20352,
                              18592,18848,19104,19360,19616,19872,20128,20384,
                              18624,18880,19136,19392,19648,19904,20160,20416,
                              18656,18912,19168,19424,19680,19936,20192,20448,
                              //----------------------------------------------
                              20480,20736,20992,21248,21504,21760,22016,22272,
                              20512,20768,21024,21280,21536,21792,22048,22304,
                              20544,20800,21056,21312,21568,21824,22080,22336,
                              20576,20832,21088,21344,21600,21856,22112,22368,
                              20608,20864,21120,21376,21632,21888,22144,22400,
                              20640,20896,21152,21408,21664,21920,22176,22432,
                              20672,20928,21184,21440,21696,21952,22208,22464,
                              20704,20960,21216,21472,21728,21984,22240,22496};
            Bitmap buffer = new Bitmap(256, 192);
            //Подготовка поля (на случай если данных меньше чем входит в видеопамять
            byte[] m = new byte[6912];
            for (int i = 6144; i < 6912; i++) m[i] = 56;
            int j = Math.Min(Project.view.FileData.Count() - 1, 6913);
            for (int i = 0; i < 6912; i++)
            {
                int a = i + 1 + (int)numericUpDown2.Value;
                if (a >= 0 & a <= Project.view.FileData.Count() - 2)
                    m[i] = Project.view.FileData[a];
            }
            //Собственно, рисование
            byte C=0;
            for (int y = 0; y < 191; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    byte B = m[Adresses[y] - 16384 + x];
                    if (Mode == 0) C = m[6144 + x + (y / 8) * 32];
                    if (Mode == 1) C = 56;
                    if (Mode == 2) C = 7;
                    buffer.SetPixel(x * 8, y, Pixel(C, (B & 128) == 128));
                    buffer.SetPixel(x * 8 + 1, y, Pixel(C, (B & 64) == 64));
                    buffer.SetPixel(x * 8 + 2, y, Pixel(C, (B & 32) == 32));
                    buffer.SetPixel(x * 8 + 3, y, Pixel(C, (B & 16) == 16));
                    buffer.SetPixel(x * 8 + 4, y, Pixel(C, (B & 8) == 8));
                    buffer.SetPixel(x * 8 + 5, y, Pixel(C, (B & 4) == 4));
                    buffer.SetPixel(x * 8 + 6, y, Pixel(C, (B & 2) == 2));
                    buffer.SetPixel(x * 8 + 7, y, Pixel(C, (B & 1) == 1));
                }
            }
            if (Mode == 0) pictureBox1.Image = buffer;
            if (Mode == 1) pictureBox2.Image = buffer;
            if (Mode == 2) pictureBox3.Image = buffer;
        }
        //Возвращает цвет точки по атрибуту (в зависимости включен пиксель или нет)
        Color Pixel(byte attr, bool PixelON)
        {
            Color[] palette = {Color.FromArgb(0,0,0), Color.FromArgb(0,0,210), Color.FromArgb(210,0,0), Color.FromArgb(210,0,210),
                               Color.FromArgb(0,210,0), Color.FromArgb(0,210,210), Color.FromArgb(210,210,0), Color.FromArgb(210,210,210),
                               Color.FromArgb(0,0,0), Color.FromArgb(0,0,255), Color.FromArgb(255,0,0), Color.FromArgb(255,0,255),
                               Color.FromArgb(0,255,0), Color.FromArgb(0,255,255), Color.FromArgb(255,255,0), Color.FromArgb(255,255,255)};
            if (PixelON)
                if ((attr & 64) == 0)
                    return palette[attr & 7];
                else
                    return palette[(attr & 7) + 8];
            else
                if ((attr & 64) == 0)
                    return palette[(attr & 56) / 8];
                else
                    return palette[(attr & 56) / 8 + 8];
        }
        //Просмотр в виде шрифта
        void ViewFont()
        {
            if (Project.view.FileData.Count() <= 770) button3.Enabled = false; //Зачем искать, если файл и так равен или меньше размеру шрифта
            numericUpDown3.Maximum = Project.view.FileData.Count() - 2;
            Bitmap buffer = new Bitmap(128, 48);
            int a = (int)numericUpDown3.Value + 1;
            byte b;
            byte Pal = 56;
            for (int y = 0; y < 6; y++)
                for (int x = 0; x < 16; x++)
                    for (int s = 0; s < 8; s++)
                    {
                        //try { b = Project.BlockView.FileData[a++]; } catch { b = 0; }
                        if (a > Project.view.FileData.Count()-2) b = 0;
                        else b = Project.view.FileData[a++];
                        buffer.SetPixel(x * 8, y * 8 + s, Pixel(Pal, (b & 128) == 128));
                        buffer.SetPixel(x * 8+1, y * 8 + s, Pixel(Pal, (b & 64) == 64));
                        buffer.SetPixel(x * 8+2, y * 8 + s, Pixel(Pal, (b & 32) == 32));
                        buffer.SetPixel(x * 8+3, y * 8 + s, Pixel(Pal, (b & 16) == 16));
                        buffer.SetPixel(x * 8+4, y * 8 + s, Pixel(Pal, (b & 8) == 8));
                        buffer.SetPixel(x * 8+5, y * 8 + s, Pixel(Pal, (b & 4) == 4));
                        buffer.SetPixel(x * 8+6, y * 8 + s, Pixel(Pal, (b & 2) == 2));
                        buffer.SetPixel(x * 8+7, y * 8 + s, Pixel(Pal, (b & 1) == 1));
                    }
            pictureBox4.Image = buffer;
        }
        //Поиск шрифта
        private void button3_Click(object sender, EventArgs e)
        {
            int i = (int)numericUpDown3.Value - 768;
            if (i >= 0)
                numericUpDown3.Value = i;
            else
                numericUpDown3.Value = 0;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int i = (int)numericUpDown3.Value + 768;
            if (i <= numericUpDown3.Maximum)
                numericUpDown3.Value = i;
            else
                numericUpDown3.Value = numericUpDown3.Maximum;
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e) { ViewScreen(tabControl3.SelectedIndex); }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void button2_Click(object sender, EventArgs e) { textBox5.Text = Assembler.Disassembler(Project.view.FileData, 0); }
        //Меню сохранение картинки или шрифта
        private void сохранитьИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Сохранение изображения";
            saveFileDialog1.Filter = Lang.FilterBMP;
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            try
            {
                if (tabControl1.SelectedIndex == 1)
                {
                    if (tabControl3.SelectedIndex == 0) pictureBox1.Image.Save(saveFileDialog1.FileName);
                    if (tabControl3.SelectedIndex == 1) pictureBox2.Image.Save(saveFileDialog1.FileName);
                    if (tabControl3.SelectedIndex == 2) pictureBox3.Image.Save(saveFileDialog1.FileName);
                }
                if (tabControl1.SelectedIndex == 2)
                    pictureBox4.Image.Save(saveFileDialog1.FileName);
            }
            catch { Program.Error("Произошла ошибка при сохранении файла. Файл не сохранён."); }
        }

        private void FormViewer_FormClosing(object sender, FormClosingEventArgs e) { OnProcess = false; }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e) { ViewScreen(tabControl3.SelectedIndex); }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { ViewBasic(); }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e) { ViewFont(); }
    }
}//1131
