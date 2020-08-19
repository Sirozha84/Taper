using System;
using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;

namespace Taper
{
    public partial class FormRec : Form
    {
        WaveIn waveIn;
        Bitmap bBuffer;
        Bitmap wBuffer;
        const int bWidth = 50;
        const int bHeight = 600;
        const int wWidth = 300;
        const int wHeight = 64;

        public FormRec()
        {
            InitializeComponent();
            Text = Lang.record;
            labelSound.Text = Lang.soundSource + ":";
            labelInfo.Text = Lang.loadedData + ":";
            listView.Columns[0].Text = Lang.time;
            listView.Columns[1].Text = Lang.block;
            buttonClear.Text = Lang.clear;
            buttonCancel.Text = Lang.cancel;
        }

        private void FormTapeLoader_Load(object sender, EventArgs e)
        {
            //Подготавливаем графику
            bBuffer = new Bitmap(bWidth, bHeight);
            wBuffer = new Bitmap(wWidth, wHeight);

            //Подготовка "слушателя"
            Listener.Init();

            //Подготавливаем NAudi
            int waveInDevices = WaveIn.DeviceCount;
            if (waveInDevices == 0) return;

            for (int i = 0; i < waveInDevices; i++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(i);
                comboBoxDevices.Items.Add(deviceInfo.ProductName);
            }
            try
            {
                comboBoxDevices.SelectedIndex = Properties.Settings.Default.AudioRec;
            }
            catch
            {
                comboBoxDevices.SelectedIndex = 0;
            }
            Start();
        }

        private void FormRec_Shown(object sender, EventArgs e)
        {
            int waveInDevices = WaveIn.DeviceCount;
            if (waveInDevices < 1)
            {
                Program.Error("Не обнаружено ни одного устройства записи.");
                Close();
            }
        }

        private void comboBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AudioRec = comboBoxDevices.SelectedIndex;
            Start(); 
        }

        void Start()
        {
            Stop();
            try
            {
                waveIn = new WaveIn();
                waveIn.DeviceNumber = comboBoxDevices.SelectedIndex; //0 - номер устройства (если включен только микрофон - он будет 0)
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.WaveFormat = new WaveFormat(44100, 8, 1); //Входной формат
                waveIn.StartRecording();
            }
            catch (Exception ex)
            {
                Program.Error("Ошибка работы с аудио-устройством");
            }
        }

        void Stop()
        {
            if (waveIn == null) return;
            waveIn.StopRecording();
        }

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
            }
            else
            {
                string res = Listener.Listen(e.Buffer);
                if (res != "")
                {
                    res = DateTime.Now.ToString("HH.mm:ss☺") + res;
                    string[] s = res.Split('☺');
                    listView.Items.Add(new ListViewItem(s));
                }

                Color color = Color.Silver;
                float zoom = 256 / (float)wHeight;
                for (int i = 0; i < e.Buffer.Length & i < bHeight; i++)
                {
                    byte b = e.Buffer[i];
                    int cn = b < 128 ? 0 : 1;
                    if (Listener.mode == 1 | Listener.mode == 2)
                        color = cn == 0 ? Color.Red : Color.Cyan;
                    if (Listener.mode == 3 | Listener.mode == 4)
                        color = cn == 0 ? Color.Blue : Color.Yellow;
                    for (int j = 0; j < bWidth; j++)
                        if (j > bWidth *0.7f)
                            bBuffer.SetPixel(j, i, Color.FromArgb(b, b, b));
                        else
                            bBuffer.SetPixel(j, i, color);
                }
                for (int i = 0; i < e.Buffer.Length & i < wWidth; i++)
                {
                    byte b = e.Buffer[i];
                    for (int j = 0; j < wHeight; j++)
                        wBuffer.SetPixel(i, j, Color.Silver);
                    wBuffer.SetPixel(i, (int)(b / zoom), Color.Blue);
                }

                pictureBorder.Image = bBuffer;
                pictureWave.Image = wBuffer;
            }
        }

        private void FormTapeLoader_FormClosing(object sender, FormClosingEventArgs e) { Stop(); }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Listener.blocks.Count > 0)
            {
                Project.Change();
                foreach (byte[] block in Listener.blocks)
                    Project.Add(block);
            }
            Close();

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Listener.Init();
            listView.Items.Clear();
        }
    }
}