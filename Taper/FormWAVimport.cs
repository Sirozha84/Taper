using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Schema;

namespace Taper
{
    public partial class FormWAVimport : Form
    {
        string file;
        byte[] wav;
        BackgroundWorker worker = new BackgroundWorker();

        public FormWAVimport(string file)
        {
            InitializeComponent();
            this.file = file;
            worker.DoWork += new DoWorkEventHandler(AsynkRead);
            worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChange);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Complate);
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }

        private void buttonOK_Click(object sender, EventArgs e) { Close(); }
        private void buttonCancel_Click(object sender, EventArgs e) { Close(); }

        void AsynkRead(object sender, EventArgs e)
        {
            //Загрузка файла
            int Len = 0;
            try
            {
                BinaryReader File = new BinaryReader(new FileStream(file, FileMode.Open));
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
                //Грузим выборку
                wav = File.ReadBytes(Len);
                File.Close();
            }
            catch
            {
                Program.Error("Произошла ошибка при загрузке WAV-файла.");
                Close();
            }
            
            //Скармливание данных "слушателю"
            Listener.Init();
            int partLen = 1000;
            for (int i = 0; i < Len - partLen; i += partLen)
            {
                byte[] part = new byte[partLen];
                Array.Copy(wav, i, part, 0, partLen);
                Listener.Listen(part);
                worker.ReportProgress((int)((float)i / Len * 100));
                System.Threading.Thread.Sleep(1);
            }
        }

        void ProgressChange(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        void Complate(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            buttonOK.Enabled = true;
        }
    }
}
