using System.Linq;
using NAudio.Wave;

namespace Taper
{
    static class Audio
    {
        public static bool Plaing = false;
        public static WaveOut waveOut;
        public static Player player;
        public const int SampleRate = 44100;    //Частота дискретизации
        public static int block;
        
        /// <summary>
        /// Вопроизведение
        /// </summary>
        /// <param name="num">Номер блока, с которого начинать</param>
        public static void Play(int num)
        {
            block = num;
            if (num >= Project.TAP.Count) return;
            if (Plaing) Stop();

            //Подготовка данных
            WAVmaker.BlockToWav(Project.TAP[num]);
            Player.s = 0;
            Player.c = WAVmaker.wav.Count();

            //Инициализация звуковой системы
            try
            {
                player = new Player();
                player.SetWaveFormat(SampleRate, 1);
                waveOut = new WaveOut();
                waveOut.DeviceNumber = Properties.Settings.Default.AudioPlay;
                waveOut.DesiredLatency = 200; // длина буфера /2=50 миллисекунд
                waveOut.Init(player);
                waveOut.Play();
                Plaing = true;
                Program.mainform.PlayerIndication(num);
            }
            catch
            {
                Program.Error(Lang.msgAudioDeviceError);
            }
        }

        public static void Stop()
        {
            if (!Plaing) return;
            waveOut.Stop();
            Plaing = false;
            Program.mainform.PlayerIndication(-1);
        }
    }

    abstract class WaveProvider32 : IWaveProvider
    {
        private WaveFormat waveFormat;

        public WaveProvider32() : this(8000, 1) { }

        public WaveProvider32(int sampleRate, int channels) { SetWaveFormat(sampleRate, channels); }

        public void SetWaveFormat(int sampleRate, int channels)
        {
            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            WaveBuffer waveBuffer = new WaveBuffer(buffer);
            int samplesRequired = count / 4;
            int samplesRead = Read(waveBuffer.FloatBuffer, offset / 4, samplesRequired);
            return samplesRead * 4;
        }

        public abstract int Read(float[] buffer, int offset, int sampleCount);

        public WaveFormat WaveFormat
        {
            get { return waveFormat; }
        }
    }
    class Player : WaveProvider32
    {
        public static int s;
        public static int c;

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            //Заполнение буфера звука
            for (int i = 0; i < sampleCount; i++)
                if (s < c) buffer[i + offset] = WAVmaker.wav[s++] < 128 ? 0 : .2f; //0-1 - громкость
            
            //Если блок кончается переходим на следующий
            if (s >= c)
            {
                Audio.Stop();
                Audio.Play(Audio.block + 1); 
            }
            return sampleCount;
        }

    }
}
