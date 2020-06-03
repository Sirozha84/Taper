using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;

namespace Taper
{
    static class Audio
    {
        public static bool Plaing = false;
        public static WaveOut waveOut;
        public static Player player;
        public const int SampleRate = 44100;    //Частота дискретизации
        /// <summary>
        /// Инициализация звуковой системы
        /// </summary>
        public static void Init()
        {
            player = new Player();
            player.SetWaveFormat(SampleRate, 1);
            waveOut = new WaveOut();
            waveOut.DesiredLatency = 200; // длина буфера /2=50 миллисекунд
            waveOut.Init(player);
        }
        public static void Play()
        {
            if (Plaing) Stop();
            Init();
            WAVmaker.Make();
            Player.s = 0;
            Player.c = WAVmaker.wav.Count();
            waveOut.Play();
            Plaing = true;
        }

        public static void Stop()
        {
            waveOut.Stop();
            Plaing = false;
        }
    }

    abstract class WaveProvider32 : IWaveProvider
    {
        private WaveFormat waveFormat;

        public WaveProvider32() : this(8100, 1) { }

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
            {
                if (s < c) buffer[i + offset] = WAVmaker.wav[s++] == 127 ? 0 : .2f; //0-1 - громкость
                else Audio.Stop();
            }
            return sampleCount;
        }

    }
}
