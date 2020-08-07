﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace Taper
{
    public partial class FormRec : Form
    {
        WaveIn waveIn;
        Bitmap buffer;
        const int bwidth = 50;
        const int bheight = 600;
        public FormRec()
        {
            InitializeComponent();
        }

        private void FormTapeLoader_Load(object sender, EventArgs e)
        {
            trackBarFilter.Value = Properties.Settings.Default.Filter;
            trackBarFilter_Scroll(null, null);

            //Подготавливаем графику
            buffer = new Bitmap(bwidth, bheight);

            //Подготовка "слушателя"
            Listener.Init();

            //Подготавливаем NAudi
            int waveInDevices = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                comboBoxDevices.Items.Add(deviceInfo.ProductName);
                //Console.WriteLine("Device {0}: {1}, {2} channels", waveInDevice, deviceInfo.ProductName, deviceInfo.Channels);
            }
            comboBoxDevices.SelectedIndex = 0;
            Start();
        }

        private void comboBoxDevices_SelectedIndexChanged(object sender, EventArgs e) {  Start(); }

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
                MessageBox.Show(ex.Message);
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
                float zoom = 256 / (float)bwidth;
                for (int i = 0; i < e.Buffer.Length & i < bheight; i++)
                {
                    byte b = e.Buffer[i];
                    if (radioButtonSpectrum.Checked)
                    {
                        if (Listener.mode == 1 | Listener.mode == 2)
                            color = b < Program.center ? Color.Red : Color.Cyan;
                        if (Listener.mode == 3 | Listener.mode == 4)
                            color = b < Program.center ? Color.Blue : Color.Yellow;
                        for (int j = 0; j < bwidth; j++)
                            if (j > bwidth / 2 & checkBoxAll.Checked)
                                buffer.SetPixel(j, i, Color.FromArgb(b, b, b));
                            else
                                buffer.SetPixel(j, i, color);
                    }
                    if (radioButtonWave.Checked)
                    {
                        for (int j = 0; j < bwidth; j++)
                            buffer.SetPixel(j, i, color);
                        buffer.SetPixel((int)(b / zoom), i, Color.Blue);
                    }
                }
                border.Image = buffer;
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

        private void trackBarFilter_Scroll(object sender, EventArgs e)
        {
            labelFV.Text = trackBarFilter.Value.ToString();
            Properties.Settings.Default.Filter = trackBarFilter.Value;
        }
    }
}