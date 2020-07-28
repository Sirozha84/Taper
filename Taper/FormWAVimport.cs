using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Taper
{
    public partial class FormWAVimport : Form
    {
        const int partLen = 1000;
        string file;

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
        private void buttonCancel_Click(object sender, EventArgs e) { Close(); }

        void AsynkRead(object sender, EventArgs e)
        {
            //Загрузка файла
            int Len = WAV.Load(file, partLen);
            if (Len == 0) Close();

            //Скармливание данных "слушателю"
            Listener.Init();
            for (int i = 0; i <= Len - partLen; i += partLen)
            {
                
                //По умолчанию будем считать что запись 8-и битная
                byte[] part = new byte[partLen];
                Array.Copy(WAV.wave, i, part, 0, partLen);
                //Но если она 16-и битная, делаем по другому
                if (WAV.capacity == 16)
                {
                    part = new byte[partLen / 2];
                    for (int j = 0; j < partLen; j += 2)
                        part[j / 2] = (byte)((WAV.wave[i+j] + WAV.wave[i+j + 1] * 256) / 256 - 128);
                }
                //Здесь же потом попробовать сделать и объединение каналов... только не знаю зачем

                string listen = Listener.Listen(part);
                string res = listen != "" ? DateTime.Now.ToString("HH.mm:ss☺") + WAV.Time(i) + "☺" + listen : "";
                worker.ReportProgress((int)((float)i / Len * 100), res);
            }
        }

        void ProgressChange(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            string[] res = e.UserState.ToString().Split('☺');
            if (res[0] != "") listView.Items.Add(new ListViewItem(res));

        }

        void Complate(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            buttonOK.Enabled = true;
        }
    }

}
