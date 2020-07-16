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
                MessageBox.Show("В этом блоке нет данных");
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
                    Assembler(); break;
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
                    text += Converter(str * 10, 10, 2, true);
                    text += "|";
                    Application.DoEvents();
                }
                text += Converter(Project.view.FileData[i], 10, 1, true) + " ";
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
                    text += Converter(str * 16, 16, 2, true);
                    text += " |";
                    Application.DoEvents();
                }
                text += Converter(Project.view.FileData[i], 16, 1, true) + " ";
                x++;
                if (x > 15) { x = 0; str++; }
                if (!OnProcess) break;
            }
            textBox4.Text = text;
            OnProcess = false;
        }
        //Генерирует строку из числа, переводя его в DEC или HEX, добавляя пробелы в зависимости от количества байтов
        string Converter(int x, byte System, byte Bytes, bool Probels)
        {
            string[] hex = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            if (System == 10)
            {
                if (Bytes == 1)
                {
                    if (x < 10) return "  " + x.ToString();
                    if (x < 100) return " " + x.ToString();
                    return x.ToString();
                }
                if (Bytes == 2)
                {
                    if (Probels)
                    {
                        if (x < 10) return "    " + x.ToString();
                        if (x < 100) return "   " + x.ToString();
                        if (x < 1000) return "  " + x.ToString();
                        if (x < 10000) return " " + x.ToString();
                    }
                    return x.ToString();
                }
            }
            if (System == 16)
            {
                if (Bytes == 1) return hex[(x & 240) / 16] + hex[x & 15];
                if (Bytes == 2) return hex[(x & 61440) / 4096] + hex[(x & 3840) / 256] + hex[(x & 240) / 16] + hex[x & 15];
            }
            return "Error";
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
        
        void Assembler()
        {
            byte Sys = 10;
            if (OnProcess) return; //Эта конструкция предотвратит зависание, если полоьзователь много раз запустил процесс и хочет выйти
            OnProcess = true;
            //Загрузка программы в указанный адрес
            byte[] M = new byte[65556]; //Больше 65536 На случай, если в конце стоит команда, требующая "ещё" байтов
            int j = 1;
            int start =(int)numericUpDown1.Value;
            int end = Math.Min( (int)numericUpDown1.Value + Project.view.FileData.Count() - 2,65535);
            for (int i = start ; i < 65536 & j < Project.view.FileData.Count() - 1; i++, j++)
            {
                M[i] = Project.view.FileData[j];
            }
            //Создание справочника меток
            string[] Labels = new string[65536];
            for (int i = start; i < end-2; i++)
            {
                int s = 0;
                switch (M[i])
                {
                    case 16:
                    case 24:
                    case 32:
                    case 40:
                    case 48:
                    case 56:
                        //Относительный переход JR
                        s = JR(i, M[i + 1]);
                        i++;
                        break;
                    case 34:
                    case 42:
                    case 194:
                    case 195:
                    case 196:
                    case 202:
                    case 204:
                    case 205:
                    case 210:
                    case 212:
                    case 218:
                    case 220:
                    case 226:
                    case 228:
                    case 234:
                    case 236:
                    case 242:
                    case 244:
                    case 250:
                    case 252:
                        //Абсолютный переход JP
                        s = M[i + 1] + M[i + 2] * 256;
                        i += 2;
                        break;
                }
                if (s >= 0 & s <= 65535)
                    Labels[s] = "!";
            }
            Labels[0] = "0";
            int sh = 1;
            for (int i = 1; i < 65536; i++) if (Labels[i] == "!") Labels[i] = "Label_" + (sh++).ToString();
            //Создание текста
            textBox5.Text = "Дизассемблирование машинного кода. Подождите минутку.";
            string text = "";
            string EOF = ""+(char)13+(char)10;
            string IX;
            for (int i = start; i < end; i++)
            {
                if (Labels[i] != null) text += Labels[i] + ":" + EOF;
                string C = "";
                switch (M[i])
                {
                    case 0: C = "NOP"; break;
                    case 1: C = "LD BC, " + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false); i += 2; break;
                    case 2: C = "LD (BC), A"; break;
                    case 3: C = "INC BC"; break;
                    case 4: C = "INC B"; break;
                    case 5: C = "DEC B"; break;
                    case 6: C = "LD B, " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 7: C = "RLCA"; break;
                    case 8: C = "EX AF, AF"; break;
                    case 9: C = "ADD HL, BC"; break;
                    case 10: C = "LD A, (BC)"; break;
                    case 11: C = "DEC BC"; break;
                    case 12: C = "INC C"; break;
                    case 13: C = "DEC C"; break;
                    case 14: C = "LD C, " + Converter(M[i + 1], Sys, 1, false); i += 2; break;
                    case 15: C = "RRCA"; break;
                    case 16: C = "DJNZ " + Labels[JR(i, M[i + 1])]; i++; break;
                    case 17: C = "LD DE, " + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false); i += 2; break;
                    case 18: C = "LD (DE), A"; break;
                    case 19: C = "INC DE"; break;
                    case 20: C = "INC D"; break;
                    case 21: C = "DEC D"; break;
                    case 22: C = "LD D, " + Converter(M[i + 1], Sys, 1, false); i += 2; break;
                    case 23: C = "RLA"; break;
                    case 24: C = "JR " + Labels[JR(i, M[i + 1])]; i++; break;
                    case 25: C = "ADD HL, DE"; break;
                    case 26: C = "LD A, (DE)"; break;
                    case 27: C = "DEC DE"; break;
                    case 28: C = "INC E"; break;
                    case 29: C = "DEC E"; break;
                    case 30: C = "LD E, " + Converter(M[i + 1], Sys, 1, false); i += 2; break;
                    case 31: C = "RRA"; break;
                    case 32: C = "JR NZ, " + Labels[JR(i, M[i + 1])]; i++; break;
                    case 33: C = "LD HL, " + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false); i += 2; break;
                    case 34: C = "LD (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + "), HL"; i += 2; break;
                    case 35: C = "INC HL"; break;
                    case 36: C = "INC H"; break;
                    case 37: C = "DEC H"; break;
                    case 38: C = "LD H, " + Converter(M[i + 1], Sys, 1, false); i += 2; break;
                    case 39: C = "DAA"; break;
                    case 40: C = "JR Z, " + Labels[JR(i, M[i + 1])]; i++; break;
                    case 41: C = "ADD HL, HL"; break;
                    case 42: C = "LD HL, (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + ")"; i += 2; break;
                    case 43: C = "DEC HL"; break;
                    case 44: C = "INC L"; break;
                    case 45: C = "DEC L"; break;
                    case 46: C = "LD L, " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 47: C = "CPL"; break;
                    case 48: C = "JR NC, " + Labels[JR(i, M[i + 1])]; i++; break;
                    case 49: C = "LD SP, " + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false); i += 2; break;
                    case 50: C = "LD (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + "), A"; i += 2; break;
                    case 51: C = "INC SP"; break;
                    case 52: C = "INC (HL)"; break;
                    case 53: C = "DEC (HL)"; break;
                    case 54: C = "LD (HL), " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 55: C = "SCF"; break;
                    case 56: C = "JR C, " + Labels[JR(i, M[i + 1])]; i++; break;
                    case 57: C = "ADD HL, SP"; break;
                    case 58: C = "LD A, (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + ")"; i += 2; break;
                    case 59: C = "DEC SP"; break;
                    case 60: C = "INC A"; break;
                    case 61: C = "DEC A"; break;
                    case 62: C = "LD A, " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 63: C = "CCF"; break;
                    case 64: C = "LD B, B"; break;
                    case 65: C = "LD B, C"; break;
                    case 66: C = "LD B, D"; break;
                    case 67: C = "LD B, E"; break;
                    case 68: C = "LD B, H"; break;
                    case 69: C = "LD B, L"; break;
                    case 70: C = "LD B, (HL)"; break;
                    case 71: C = "LD B, A"; break;
                    case 72: C = "LD C, B"; break;
                    case 73: C = "LD C, C"; break;
                    case 74: C = "LD C, D"; break;
                    case 75: C = "LD C, E"; break;
                    case 76: C = "LD C, H"; break;
                    case 77: C = "LD C, L"; break;
                    case 78: C = "LD C, (HL)"; break;
                    case 79: C = "LD C, A"; break;
                    case 80: C = "LD D, B"; break;
                    case 81: C = "LD D, C"; break;
                    case 82: C = "LD D, D"; break;
                    case 83: C = "LD D, E"; break;
                    case 84: C = "LD D, H"; break;
                    case 85: C = "LD D, L"; break;
                    case 86: C = "LD D, (HL)"; break;
                    case 87: C = "LD D, A"; break;
                    case 88: C = "LD E, B"; break;
                    case 89: C = "LD E, C"; break;
                    case 90: C = "LD E, D"; break;
                    case 91: C = "LD E, E"; break;
                    case 92: C = "LD E, H"; break;
                    case 93: C = "LD E, L"; break;
                    case 94: C = "LD E, (HL)"; break;
                    case 95: C = "LD E, A"; break;
                    case 96: C = "LD H, B"; break;
                    case 97: C = "LD H, C"; break;
                    case 98: C = "LD H, D"; break;
                    case 99: C = "LD H, E"; break;
                    case 100: C = "LD H, H"; break;
                    case 101: C = "LD H, L"; break;
                    case 102: C = "LD H, (HL)"; break;
                    case 103: C = "LD H, A"; break;
                    case 104: C = "LD L, B"; break;
                    case 105: C = "LD L, C"; break;
                    case 106: C = "LD L, D"; break;
                    case 107: C = "LD L, E"; break;
                    case 108: C = "LD L, H"; break;
                    case 109: C = "LD L, L"; break;
                    case 110: C = "LD L, (HL)"; break;
                    case 111: C = "LD L, A"; break;
                    case 112: C = "LD (HL), B"; break;
                    case 113: C = "LD (HL), C"; break;
                    case 114: C = "LD (HL), D"; break;
                    case 115: C = "LD (HL), E"; break;
                    case 116: C = "LD (HL), H"; break;
                    case 117: C = "LD (HL), L"; break;
                    case 118: C = "HALT"; break;
                    case 119: C = "LD (HL), A"; break;
                    case 120: C = "LD A, B"; break;
                    case 121: C = "LD A, C"; break;
                    case 122: C = "LD A, D"; break;
                    case 123: C = "LD A, E"; break;
                    case 124: C = "LD A, H"; break;
                    case 125: C = "LD A, L"; break;
                    case 126: C = "LD A, (HL)"; break;
                    case 127: C = "LD A, A"; break;
                    case 128: C = "ADD A, B"; break;
                    case 129: C = "ADD A, C"; break;
                    case 130: C = "ADD A, D"; break;
                    case 131: C = "ADD A, E"; break;
                    case 132: C = "ADD A, H"; break;
                    case 133: C = "ADD A, L"; break;
                    case 134: C = "ADD A, (HL)"; break;
                    case 135: C = "ADD A, A"; break;
                    case 136: C = "ADC A, B"; break;
                    case 137: C = "ADC A, C"; break;
                    case 138: C = "ADC A, D"; break;
                    case 139: C = "ADC A, E"; break;
                    case 140: C = "ADC A, H"; break;
                    case 141: C = "ADC A, L"; break;
                    case 142: C = "ADC A, (HL)"; break;
                    case 143: C = "ADC A, A"; break;
                    case 144: C = "ADC B"; break;
                    case 145: C = "SUB C"; break;
                    case 146: C = "SUB D"; break;
                    case 147: C = "SUB E"; break;
                    case 148: C = "SUB H"; break;
                    case 149: C = "SUB L"; break;
                    case 150: C = "SUB (HL)"; break;
                    case 151: C = "SUB A"; break;
                    case 152: C = "SBC A, B"; break;
                    case 153: C = "SBC A, C"; break;
                    case 154: C = "SBC A, D"; break;
                    case 155: C = "SBC A, E"; break;
                    case 156: C = "SBC A, H"; break;
                    case 157: C = "SBC A, L"; break;
                    case 158: C = "SBC A, (HL)"; break;
                    case 159: C = "SBC A, A"; break;
                    case 160: C = "AND B"; break;
                    case 161: C = "AND C"; break;
                    case 162: C = "AND D"; break;
                    case 163: C = "AND E"; break;
                    case 164: C = "AND H"; break;
                    case 165: C = "AND L"; break;
                    case 166: C = "AND (HL)"; break;
                    case 167: C = "AND A"; break;
                    case 168: C = "XOR B"; break;
                    case 169: C = "XOR C"; break;
                    case 170: C = "XOR D"; break;
                    case 171: C = "XOR E"; break;
                    case 172: C = "XOR H"; break;
                    case 173: C = "XOR L"; break;
                    case 174: C = "XOR (HL)"; break;
                    case 175: C = "XOR A"; break;
                    case 176: C = "OR B"; break;
                    case 177: C = "OR C"; break;
                    case 178: C = "OR D"; break;
                    case 179: C = "OR E"; break;
                    case 180: C = "OR H"; break;
                    case 181: C = "OR L"; break;
                    case 182: C = "OR (HL)"; break;
                    case 183: C = "OR A"; break;
                    case 184: C = "CP B"; break;
                    case 185: C = "CP C"; break;
                    case 186: C = "CP D"; break;
                    case 187: C = "CP E"; break;
                    case 188: C = "CP H"; break;
                    case 189: C = "CP L"; break;
                    case 190: C = "CP (HL)"; break;
                    case 191: C = "CP A"; break;
                    case 192: C = "RET NZ"; break;
                    case 193: C = "POP BC"; break;
                    case 194: C = "JP NZ, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 195: C = "JP " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 196: C = "CALL NZ, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 197: C = "PUSH BC"; break;
                    case 198: C = "ADD A, " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 199: C = "RST 0"; break;
                    case 200: C = "RET Z"; break;
                    case 201: C = "RET"; break;
                    case 202: C = "JP Z, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 204: C = "CALL Z, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 205: C = "CALL " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 206: C = "ADC A, " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 207: C = "RST 8"; break;
                    case 208: C = "RET NC"; break;
                    case 209: C = "POP DE"; break;
                    case 210: C = "JP NC, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 211: C = "OUT (" + Converter(M[i + 1], Sys, 1, false) + "), A"; i++; break;
                    case 212: C = "CALL NC, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 213: C = "PUSH DE"; break;
                    case 214: C = "SUB " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 215: C = "RST 10"; break;
                    case 216: C = "RET C"; break;
                    case 217: C = "EXX"; break;
                    case 218: C = "JP C, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 219: C = "IN A, (" + Converter(M[i + 1], Sys, 1, false) + ")"; i++; break;
                    case 220: C = "CALL C, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 222: C = "SBC A, " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 223: C = "RST 18"; break;
                    case 224: C = "RET PO"; break;
                    case 225: C = "POP HL"; break;
                    case 226: C = "JP PO, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 227: C = "EX (SP), HL"; break;
                    case 228: C = "CALL PO, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 229: C = "PUSH HL"; break;
                    case 230: C = "AND " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 231: C = "RST 20"; break;
                    case 232: C = "RET PE"; break;
                    case 233: C = "JP (HL)"; break;
                    case 234: C = "JP PE, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 235: C = "EX DE, HL"; break;
                    case 236: C = "CALL PE, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 238: C = "XOR " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 239: C = "RST 28"; break;
                    case 240: C = "RET P"; break;
                    case 241: C = "POP AF"; break;
                    case 242: C = "JP P, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 243: C = "DI"; break;
                    case 244: C = "CALL P, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 245: C = "PUSH AF"; break;
                    case 246: C = "OR " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 247: C = "RST 30"; break;
                    case 248: C = "RET M"; break;
                    case 249: C = "LD SP, HL"; break;
                    case 250: C = "JP M, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 251: C = "EI"; break;
                    case 252: C = "CALL M, " + Labels[M[i + 1] + M[i + 2] * 256]; i += 2; break;
                    case 254: C = "CP A, " + Converter(M[i + 1], Sys, 1, false); i++; break;
                    case 255: C = "RST 8"; break;
                    #region После CB
                    case 203: //После CB
                        i++;
                        switch (M[i])
                        {
                            case 0: C = "RLC B"; break;
                            case 1: C = "RLC C"; break;
                            case 2: C = "RLC D"; break;
                            case 3: C = "RLC E"; break;
                            case 4: C = "RLC H"; break;
                            case 5: C = "RLC L"; break;
                            case 6: C = "RLC (HL)"; break;
                            case 7: C = "RLC A"; break;
                            case 8: C = "RRC B"; break;
                            case 9: C = "RRC C"; break;
                            case 10: C = "RRC D"; break;
                            case 11: C = "RRC E"; break;
                            case 12: C = "RRC H"; break;
                            case 13: C = "RRC L"; break;
                            case 14: C = "RRC (HL)"; break;
                            case 15: C = "RRC A"; break;
                            case 16: C = "RL B"; break;
                            case 17: C = "RL C"; break;
                            case 18: C = "RL D"; break;
                            case 19: C = "RL E"; break;
                            case 20: C = "RL H"; break;
                            case 21: C = "RL L"; break;
                            case 22: C = "RL (HL)"; break;
                            case 23: C = "RL A"; break;
                            case 24: C = "RR B"; break;
                            case 25: C = "RR C"; break;
                            case 26: C = "RR D"; break;
                            case 27: C = "RR E"; break;
                            case 28: C = "RR H"; break;
                            case 29: C = "RR L"; break;
                            case 30: C = "RR (HL)"; break;
                            case 31: C = "RR A"; break;
                            case 32: C = "SLA B"; break;
                            case 33: C = "SLA C"; break;
                            case 34: C = "SLA D"; break;
                            case 35: C = "SLA E"; break;
                            case 36: C = "SLA H"; break;
                            case 37: C = "SLA L"; break;
                            case 38: C = "SLA (HL)"; break;
                            case 39: C = "SLA A"; break;
                            case 40: C = "SRA B"; break;
                            case 41: C = "SRA C"; break;
                            case 42: C = "SRA D"; break;
                            case 43: C = "SRA E"; break;
                            case 44: C = "SRA H"; break;
                            case 45: C = "SRA L"; break;
                            case 46: C = "SRA (HL)"; break;
                            case 47: C = "SRA A"; break;
                            case 56: C = "SRL B"; break;
                            case 57: C = "SRL C"; break;
                            case 58: C = "SRL D"; break;
                            case 59: C = "SRL E"; break;
                            case 60: C = "SRL H"; break;
                            case 61: C = "SRL L"; break;
                            case 62: C = "SRL (HL)"; break;
                            case 63: C = "SRL A"; break;
                            case 64: C = "BIT 0, B"; break;
                            case 65: C = "BIT 0, C"; break;
                            case 66: C = "BIT 0, D"; break;
                            case 67: C = "BIT 0, E"; break;
                            case 68: C = "BIT 0, H"; break;
                            case 69: C = "BIT 0, L"; break;
                            case 70: C = "BIT 0, (HL)"; break;
                            case 71: C = "BIT 0, A"; break;
                            case 72: C = "BIT 1, B"; break;
                            case 73: C = "BIT 1, C"; break;
                            case 74: C = "BIT 1, D"; break;
                            case 75: C = "BIT 1, E"; break;
                            case 76: C = "BIT 1, H"; break;
                            case 77: C = "BIT 1, L"; break;
                            case 78: C = "BIT 1, (HL)"; break;
                            case 79: C = "BIT 1, A"; break;
                            case 80: C = "BIT 2, B"; break;
                            case 81: C = "BIT 2, C"; break;
                            case 82: C = "BIT 2, D"; break;
                            case 83: C = "BIT 2, E"; break;
                            case 84: C = "BIT 2, H"; break;
                            case 85: C = "BIT 2, L"; break;
                            case 86: C = "BIT 2, (HL)"; break;
                            case 87: C = "BIT 2, A"; break;
                            case 88: C = "BIT 3, B"; break;
                            case 89: C = "BIT 3, C"; break;
                            case 90: C = "BIT 3, D"; break;
                            case 91: C = "BIT 3, E"; break;
                            case 92: C = "BIT 3, H"; break;
                            case 93: C = "BIT 3, L"; break;
                            case 94: C = "BIT 3, (HL)"; break;
                            case 95: C = "BIT 3, A"; break;
                            case 96: C = "BIT 4, B"; break;
                            case 97: C = "BIT 4, C"; break;
                            case 98: C = "BIT 4, D"; break;
                            case 99: C = "BIT 4, E"; break;
                            case 100: C = "BIT 4, H"; break;
                            case 101: C = "BIT 4, L"; break;
                            case 102: C = "BIT 4, (HL)"; break;
                            case 103: C = "BIT 4, A"; break;
                            case 104: C = "BIT 5, B"; break;
                            case 105: C = "BIT 5, C"; break;
                            case 106: C = "BIT 5, D"; break;
                            case 107: C = "BIT 5, E"; break;
                            case 108: C = "BIT 5, H"; break;
                            case 109: C = "BIT 5, L"; break;
                            case 110: C = "BIT 5, (HL)"; break;
                            case 111: C = "BIT 5, A"; break;
                            case 112: C = "BIT 6, B"; break;
                            case 113: C = "BIT 6, C"; break;
                            case 114: C = "BIT 6, D"; break;
                            case 115: C = "BIT 6, E"; break;
                            case 116: C = "BIT 6, H"; break;
                            case 117: C = "BIT 6, L"; break;
                            case 118: C = "BIT 6, (HL)"; break;
                            case 119: C = "BIT 6, A"; break;
                            case 120: C = "BIT 7, B"; break;
                            case 121: C = "BIT 7, C"; break;
                            case 122: C = "BIT 7, D"; break;
                            case 123: C = "BIT 7, E"; break;
                            case 124: C = "BIT 7, H"; break;
                            case 125: C = "BIT 7, L"; break;
                            case 126: C = "BIT 7, (HL)"; break;
                            case 127: C = "BIT 7, A"; break;
                            case 128: C = "RES 0, B"; break;
                            case 129: C = "RES 0, C"; break;
                            case 130: C = "RES 0, D"; break;
                            case 131: C = "RES 0, E"; break;
                            case 132: C = "RES 0, H"; break;
                            case 133: C = "RES 0, L"; break;
                            case 134: C = "RES 0, (HL)"; break;
                            case 135: C = "RES 0, A"; break;
                            case 136: C = "RES 1, B"; break;
                            case 137: C = "RES 1, C"; break;
                            case 138: C = "RES 1, D"; break;
                            case 139: C = "RES 1, E"; break;
                            case 140: C = "RES 1, H"; break;
                            case 141: C = "RES 1, L"; break;
                            case 142: C = "RES 1, (HL)"; break;
                            case 143: C = "RES 1, A"; break;
                            case 144: C = "RES 2, B"; break;
                            case 145: C = "RES 2, C"; break;
                            case 146: C = "RES 2, D"; break;
                            case 147: C = "RES 2, E"; break;
                            case 148: C = "RES 2, H"; break;
                            case 149: C = "RES 2, L"; break;
                            case 150: C = "RES 2, (HL)"; break;
                            case 151: C = "RES 2, A"; break;
                            case 152: C = "RES 3, B"; break;
                            case 153: C = "RES 3, C"; break;
                            case 154: C = "RES 3, D"; break;
                            case 155: C = "RES 3, E"; break;
                            case 156: C = "RES 3, H"; break;
                            case 157: C = "RES 3, L"; break;
                            case 158: C = "RES 3, (HL)"; break;
                            case 159: C = "RES 3, A"; break;
                            case 160: C = "RES 4, B"; break;
                            case 161: C = "RES 4, C"; break;
                            case 162: C = "RES 4, D"; break;
                            case 163: C = "RES 4, E"; break;
                            case 164: C = "RES 4, H"; break;
                            case 165: C = "RES 4, L"; break;
                            case 166: C = "RES 4, (HL)"; break;
                            case 167: C = "RES 4, A"; break;
                            case 168: C = "RES 5, B"; break;
                            case 169: C = "RES 5, C"; break;
                            case 170: C = "RES 5, D"; break;
                            case 171: C = "RES 5, E"; break;
                            case 172: C = "RES 5, H"; break;
                            case 173: C = "RES 5, L"; break;
                            case 174: C = "RES 5, (HL)"; break;
                            case 175: C = "RES 5, A"; break;
                            case 176: C = "RES 6, B"; break;
                            case 177: C = "RES 6, C"; break;
                            case 178: C = "RES 6, D"; break;
                            case 179: C = "RES 6, E"; break;
                            case 180: C = "RES 6, H"; break;
                            case 181: C = "RES 6, L"; break;
                            case 182: C = "RES 6, (HL)"; break;
                            case 183: C = "RES 6, A"; break;
                            case 184: C = "RES 7, B"; break;
                            case 185: C = "RES 7, C"; break;
                            case 186: C = "RES 7, D"; break;
                            case 187: C = "RES 7, E"; break;
                            case 188: C = "RES 7, H"; break;
                            case 189: C = "RES 7, L"; break;
                            case 190: C = "RES 7, (HL)"; break;
                            case 191: C = "RES 7, A"; break;
                            case 192: C = "SET 0, B"; break;
                            case 193: C = "SET 0, C"; break;
                            case 194: C = "SET 0, D"; break;
                            case 195: C = "SET 0, E"; break;
                            case 196: C = "SET 0, H"; break;
                            case 197: C = "SET 0, L"; break;
                            case 198: C = "SET 0, (HL)"; break;
                            case 199: C = "SET 0, A"; break;
                            case 200: C = "SET 1, B"; break;
                            case 201: C = "SET 1, C"; break;
                            case 202: C = "SET 1, D"; break;
                            case 203: C = "SET 1, E"; break;
                            case 204: C = "SET 1, H"; break;
                            case 205: C = "SET 1, L"; break;
                            case 206: C = "SET 1, (HL)"; break;
                            case 207: C = "SET 1, A"; break;
                            case 208: C = "SET 2, B"; break;
                            case 209: C = "SET 2, C"; break;
                            case 210: C = "SET 2, D"; break;
                            case 211: C = "SET 2, E"; break;
                            case 212: C = "SET 2, H"; break;
                            case 213: C = "SET 2, L"; break;
                            case 214: C = "SET 2, (HL)"; break;
                            case 215: C = "SET 2, A"; break;
                            case 216: C = "SET 3, B"; break;
                            case 217: C = "SET 3, C"; break;
                            case 218: C = "SET 3, D"; break;
                            case 219: C = "SET 3, E"; break;
                            case 220: C = "SET 3, H"; break;
                            case 221: C = "SET 3, L"; break;
                            case 222: C = "SET 3, (HL)"; break;
                            case 223: C = "SET 3, A"; break;
                            case 224: C = "SET 4, B"; break;
                            case 225: C = "SET 4, C"; break;
                            case 226: C = "SET 4, D"; break;
                            case 227: C = "SET 4, E"; break;
                            case 228: C = "SET 4, H"; break;
                            case 229: C = "SET 4, L"; break;
                            case 230: C = "SET 4, (HL)"; break;
                            case 231: C = "SET 4, A"; break;
                            case 232: C = "SET 5, B"; break;
                            case 233: C = "SET 5, C"; break;
                            case 234: C = "SET 5, D"; break;
                            case 235: C = "SET 5, E"; break;
                            case 236: C = "SET 5, H"; break;
                            case 237: C = "SET 5, L"; break;
                            case 238: C = "SET 5, (HL)"; break;
                            case 239: C = "SET 5, A"; break;
                            case 240: C = "SET 6, B"; break;
                            case 241: C = "SET 6, C"; break;
                            case 242: C = "SET 6, D"; break;
                            case 243: C = "SET 6, E"; break;
                            case 244: C = "SET 6, H"; break;
                            case 245: C = "SET 6, L"; break;
                            case 246: C = "SET 6, (HL)"; break;
                            case 247: C = "SET 6, A"; break;
                            case 248: C = "SET 7, B"; break;
                            case 249: C = "SET 7, C"; break;
                            case 250: C = "SET 7, D"; break;
                            case 251: C = "SET 7, E"; break;
                            case 252: C = "SET 7, H"; break;
                            case 253: C = "SET 7, L"; break;
                            case 254: C = "SET 7, (HL)"; break;
                            case 255: C = "SET 7, A"; break;
                            default: C = "Ошибка, несуществующая команда [" + M[i - 1] + "][" + M[i] + "]"; break;
                        }
                        break;
                    #endregion
                    #region После ED
                    case 237: //После ED
                        i++;
                        switch (M[i])
                        {
                            case 64: C = "IN B, (C)"; break;
                            case 65: C = "OUT (C), B"; break;
                            case 66: C = "SBC HL, BC"; break;
                            case 67: C = "LD (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + "), BC"; i += 2; break;
                            case 68: C = "NEG"; break;
                            case 69: C = "RETN"; break;
                            case 70: C = "IM 0"; break;
                            case 71: C = "LD I, A"; break;
                            case 72: C = "IN C, (C)"; break;
                            case 73: C = "OUT (C), C"; break;
                            case 74: C = "ADC HL, BC"; break;
                            case 75: C = "LD BC, (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + ")"; i += 2; break;
                            case 77: C = "RETI"; break;
                            case 79: C = "LD R, A"; break;
                            case 80: C = "IN D, (C)"; break;
                            case 81: C = "OUT (C), D"; break;
                            case 82: C = "SBC HL, DE"; break;
                            case 83: C = "LD (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + "), DE"; i += 2; break;
                            case 86: C = "IM 1"; break;
                            case 87: C = "LD A, I"; break;
                            case 88: C = "IN E, (C)"; break;
                            case 89: C = "OUT (C), E"; break;
                            case 90: C = "ADC HL, DE"; break;
                            case 91: C = "LD DE, (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + ")"; i += 2; break;
                            case 94: C = "IM 2"; break;
                            case 95: C = "LD A, R"; break;
                            case 96: C = "IN H, (C)"; break;
                            case 97: C = "OUT (C), H"; break;
                            case 98: C = "SBC HL, HL"; break;
                            case 99: C = "LD (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + "), HL"; i += 2; break;
                            case 103: C = "RRD"; break;
                            case 104: C = "IN L, (C)"; break;
                            case 105: C = "OUT (C), L"; break;
                            case 106: C = "ADC HL, HL"; break;
                            case 107: C = "LD HL, (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + ")"; i += 2; break;
                            case 111: C = "RLD"; break;
                            case 112: C = "IN F, (C)"; break;
                            case 114: C = "SBC HL, SP"; break;
                            case 115: C = "LD (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + "), HL"; i += 2; break;
                            case 120: C = "IN A, (C)"; break;
                            case 121: C = "OUT (C), A"; break;
                            case 122: C = "ADC HL, SP"; break;
                            case 123: C = "LD S, (" + Converter(M[i + 1] + M[i + 2] * 256, Sys, 2, false) + ")"; i += 2; break;
                            case 168: C = "LDD"; break;
                            case 169: C = "CDP"; break;
                            case 170: C = "IND"; break;
                            case 171: C = "OUTD"; break;
                            case 176: C = "LDIR"; break;
                            case 177: C = "CPIR"; break;
                            case 178: C = "INIR"; break;
                            case 179: C = "OTIR"; break;
                            case 184: C = "LDDR"; break;
                            case 185: C = "CPDR"; break;
                            case 186: C = "INDR"; break;
                            case 187: C = "OTDR"; break;
                            default: C = "Ошибка, несуществующая команда [" + M[i - 1] + "][" + M[i] + "]"; break;
                        }
                        break;
                    #endregion
                    #region После DD и FD
                    case 221: //После DD - Префикс IX
                    case 253: //После FD - Префикс IY
                        if (M[i] == 251) IX = "IX"; else IX = "IY";
                        switch (M[++i])
                        {
                            case 9: C = "ADD " + IX + ", BC"; break;
                            case 25: C = "ADD " + IX + ", DE"; break;
                            case 33: C = "LD " + IX + ", " + Converter(M[++i] + M[++i] * 256, Sys, 2, false); break;
                            case 34: C = "LD (" + Converter(M[++i] + M[++i] * 256, Sys, 2, false) + "), " + IX; break;
                            case 35: C = "INC " + IX; break;
                            case 41: C = "ADD " + IX + ", " + IX; break;
                            case 42: C = "LD " + IX + ", (" + Converter(M[++i] + M[++i] * 256, Sys, 2, false) + ")"; break;
                            case 43: C = "DEC " + IX; break;
                            case 52: C = "INC (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 53: C = "DEC (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 54: C = "LD (" + IX + "+" + Converter(M[++i], Sys, 1, false) + "), " + Converter(M[++i], Sys, 1, false); break;
                            case 57: C = "ADD " + IX + ", SP"; break;
                            case 70: C = "LD B, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 78: C = "LD C, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 86: C = "LD D, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 94: C = "LD E, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 102: C = "LD H, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 110: C = "LD L, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 112: C = "LD (" + IX + "+" + Converter(M[++i], Sys, 1, false) + "), B"; break;
                            case 113: C = "LD (" + IX + "+" + Converter(M[++i], Sys, 1, false) + "), C"; break;
                            case 114: C = "LD (" + IX + "+" + Converter(M[++i], Sys, 1, false) + "), D"; break;
                            case 115: C = "LD (" + IX + "+" + Converter(M[++i], Sys, 1, false) + "), E"; break;
                            case 116: C = "LD (" + IX + "+" + Converter(M[++i], Sys, 1, false) + "), H"; break;
                            case 117: C = "LD (" + IX + "+" + Converter(M[++i], Sys, 1, false) + "), L"; break;
                            case 119: C = "LD (" + IX + "+" + Converter(M[++i], Sys, 1, false) + "), A"; break;
                            case 126: C = "LD A, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 134: C = "ADD A, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 142: C = "ADC A, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 150: C = "SUB (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 158: C = "SBC A, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 166: C = "AND (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 174: C = "XOR (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 182: C = "OR (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 190: C = "CP (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")"; break;
                            case 203:
                                if (M[i + 2] == 6) C = "RLC (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 14) C = "RRC (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 22) C = "RL (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 30) C = "RR (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 38) C = "SLA (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 46) C = "SRA (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 62) C = "SRL (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 70) C = "BIT 0, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 78) C = "BIT 1, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 86) C = "BIT 2, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 94) C = "BIT 3, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 102) C = "BIT 4, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 110) C = "BIT 5, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 118) C = "BIT 6, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 126) C = "BIT 7, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 134) C = "RES 0, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 142) C = "RES 1, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 150) C = "RES 2, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 158) C = "RES 3, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 166) C = "RES 4, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 174) C = "RES 5, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 182) C = "RES 6, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 190) C = "RES 7, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 198) C = "SET 0, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 206) C = "SET 1, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 214) C = "SET 2, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 222) C = "SET 3, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 230) C = "SET 4, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 238) C = "SET 5, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 246) C = "SET 6, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                if (M[i + 2] == 254) C = "SET 7, (" + IX + "+" + Converter(M[++i], Sys, 1, false) + ")";
                                i++;
                                break;
                            case 225: C = "POP " + IX; break;
                            case 227: C = "EX (SP), " + IX; break;
                            case 229: C = "PUSH " + IX; break;
                            case 233: C = "JP (" + IX + ")"; break;
                            case 249: C = "LD SP, " + IX; break;
                            default: C = "Ошибка, несуществующая команда [" + M[i - 1] + "][" + M[i] + "]"; break;
                        }
                        break;
                    #endregion
                }
                text += "    " + C + EOF;
                Application.DoEvents();
                if (!OnProcess) break;
            }
            
            //for (int i = 49990; i < 65536; i++) text += i.ToString() + " - " + M[i].ToString()+ " - " + Labels[i] + (char)13 + (char)10; //Показывает метки
            textBox5.Text = text;
            OnProcess = false;
        }

        int JR(int adr, byte TO)
        {
            int result = 0;
            if (TO < 128) result = adr + TO + 2;
            else result = adr + TO - 254;
            if (result<0) return 0;
            if (result>65535) return 65535;
            return result;
        }

        private void button2_Click(object sender, EventArgs e) { Assembler(); }
        //Меню сохранение картинки или шрифта
        private void сохранитьИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Сохранение изображения";
            saveFileDialog1.Filter = Program.FilterBMP;
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
