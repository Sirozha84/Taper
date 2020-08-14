using System;
using System.Windows.Forms;
using NAudio.Wave;

namespace Taper
{
    public partial class FormProperties : Form
    {
        public FormProperties()
        {
            InitializeComponent();
        }

        private void FormProperties_Load(object sender, EventArgs e)
        {
            //Вкладка "Общие"
            comboBoxLanguages.Items.Add("English");
            comboBoxLanguages.Items.Add("Italian");
            comboBoxLanguages.Items.Add("Русский");
            comboBoxLanguages.SelectedIndex = 0;
            if (Properties.Settings.Default.Language == "it-IT") comboBoxLanguages.SelectedIndex = 1;
            if (Properties.Settings.Default.Language == "ru-RU") comboBoxLanguages.SelectedIndex = 2;
            radioButtonRem.Checked = Properties.Settings.Default.StartPosition == 0;
            radioButtonCenter.Checked = Properties.Settings.Default.StartPosition == 1;

            //Вкладка "Аудио", устройство воспроизведения
            int devices = WaveOut.DeviceCount;
            for (int i = 0; i < devices; i++)
            {
                WaveOutCapabilities deviceInfo = WaveOut.GetCapabilities(i);
                comboBoxPlay.Items.Add(deviceInfo.ProductName);
            }
            try
            {
                comboBoxPlay.SelectedIndex = 0;
                comboBoxPlay.SelectedIndex = Properties.Settings.Default.AudioPlay;
            }
            catch { }

            //Вкладка "Аудио", устройство записи
            devices = WaveIn.DeviceCount;
            for (int i = 0; i < devices; i++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(i);
                comboBoxRec.Items.Add(deviceInfo.ProductName);
            }
            try
            {
                comboBoxRec.SelectedIndex = 0;
                comboBoxRec.SelectedIndex = Properties.Settings.Default.AudioRec;
            }
            catch { }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = "en-US";
            if (comboBoxLanguages.SelectedIndex == 1) Properties.Settings.Default.Language = "it-IT";
            if (comboBoxLanguages.SelectedIndex == 2) Properties.Settings.Default.Language = "ru-RU";
            Properties.Settings.Default.StartPosition = radioButtonRem.Checked ? 0 : 1;
            Properties.Settings.Default.AudioPlay = comboBoxPlay.SelectedIndex;
            Properties.Settings.Default.AudioRec = comboBoxRec.SelectedIndex;
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
