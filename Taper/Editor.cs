using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
class Editor
{
    //Название программы, версия и автор
    public const string ProgramName = "Taper";
    public const string ProgramVersion = "2.1 - 9 июня 2015 года";
    public const string ProgramAutor = "Сергей Гордеев";
    //Параметры открытия и сохранения файлов

    public const string FileUnnamed = "Безымянный";
    public const string FileTypeTAP = "Образ ленты (*.tap)|*.tap|Все файлы (*.*)|*.*";
    public const string FileTypeTZX = "Образ ленты (*.tzx)|*.tzx|Все файлы (*.*)|*.*";
    public const string FileTypeWAV = "Звуковой файл (*.wav)|*.wav|Все файлы (*.*)|*.*";
    public const string FileTypeBMP = "Точечный рисунок (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
    //Папка и файл для сохранения параметров программы
    public static string ParametersFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SG\\Taper";
    static string ParametersFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SG\\Taper\\config.cfg";
    //Прочее
    public const int ColumnsCount = 6;
    public static string Rename = "";
    //Инициализация параметров
    public static void init()
    {
        try
        {
            //Пробуем загрузить настройки, если они были сохранены
            BinaryReader file = new BinaryReader(new FileStream(ParametersFile, FileMode.Open));
            WindowsPosirion.X = file.ReadInt32();
            WindowsPosirion.Y = file.ReadInt32();
            WindowsPosirion.Width = file.ReadInt32();
            WindowsPosirion.Heidht = file.ReadInt32();
            WindowsPosirion.Max = file.ReadBoolean();
            for (int i = 0; i < ColumnsCount; i++)
                Columns.Tab[i] = file.ReadInt32();
            file.Close();
        }
        catch { }
    }
    //Сохранение параметров
    public static void saveconfig()
    {
        try
        {
            Directory.CreateDirectory(ParametersFolder);
            BinaryWriter file = new BinaryWriter(new FileStream(ParametersFile, FileMode.Create));
            file.Write(WindowsPosirion.X);
            file.Write(WindowsPosirion.Y);
            file.Write(WindowsPosirion.Width);
            file.Write(WindowsPosirion.Heidht);
            file.Write(WindowsPosirion.Max);
            for (int i = 0; i < ColumnsCount; i++)
                file.Write(Columns.Tab[i]);
            file.Close();
        }
        catch { }
    }
    //Злобное сообщение об ошибке
    public static void Error(string message)
    {
        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    //Не злобное сообщение
    public static void Message(string message)
    {
        MessageBox.Show(message, ProgramName);
    }
}
public class WindowsPosirion
{
    static public int X = 300;
    static public int Y = 300;
    static public int Width = 600;
    static public int Heidht = 400;
    static public bool Max = false;
}
public class Columns
{
    public static int[] Tab = { 100, 100, 100, 100, 120, 40 };
}
//Структура спектрумовского файла
class TapBlock
{
    //Данные, как есть
    public byte[] FileTitle;
    public byte[] FileData;
    //"Человеческие" данные
    //public bool FileNameEnabled = false;
    public string FileType;
    public string FileName = "";
    public bool CRCOK;
    public TapBlock(byte[] Bytes)
    {
        //По полученному массиву байт определяем заголовок это или блок данных
        if (Bytes[0] == 0)
        {
            //Если байт в блоке меньше чем надо, значит была ошибка в загрузке, пропускаем такой блок
            if (Bytes.Length == 19)
            {
                //Создаём файл с именем но без блока
                FileTitle = Bytes;

                switch (Bytes[1])
                {
                    case 0: FileType = "Program:"; break;
                    case 1: FileType = "Number array:"; break;
                    case 2: FileType = "Character array:"; break;
                    case 3: FileType = "Bytes:"; break;
                }
                for (int i = 2; i < 12; i++)
                    FileName += (char)Bytes[i];
            }
        }
        else
        {
            //Создаём файл с блоком но без имени
            FileData = Bytes;
            CRCOK = CRCTest(Bytes);
        }
    }
    //Добавление блока к файлу у которого есть имя
    public void AddBlock(byte[] Bytes)
    {
        FileData = Bytes;
        CRCOK = CRCTest(Bytes);
    }
    //Проверка контрольной суммы
    bool CRCTest(byte[] Bytes)
    {
        byte summ = 0;
        foreach (byte b in Bytes) summ ^= b;
        return (summ == 0);
    }
}
//Проект
class Project
{
    //Сам тап-файл, собственной персоной
    public static List<TapBlock> TAPfile = new List<TapBlock>();
    //Блок для просмотрщика
    public static TapBlock BlockView;
    //Добавление произвольного блока в проект
    public static void Add(byte[] Bytes)
    {
        //Создаём блок с именем но без данных
        if (Bytes[0] == 0)
            TAPfile.Add(new TapBlock(Bytes)); 
        else
        {
            if (TAPfile.Count > 0 && TAPfile[TAPfile.Count - 1].FileName != null & TAPfile[TAPfile.Count - 1].FileData == null)
                TAPfile[TAPfile.Count - 1].AddBlock(Bytes); //Загружаем блок в последний файл
            else
                TAPfile.Add(new TapBlock(Bytes)); //Создаём блок без имени
        }
    }
}