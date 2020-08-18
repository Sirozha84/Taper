namespace Taper
{
    public abstract class Lang
    {
        public abstract string language { get; }
        public abstract string addBlock { get; }
        public abstract string about { get; }
        public abstract string allBytes { get; }
        public abstract string audio { get; }
        public abstract string autor { get; }
        public abstract string blockCount { get; }
        public abstract string blockList { get; }
        public abstract string blocks { get; }
        public abstract string copy { get; }
        public abstract string close { get; }
        public abstract string cut { get; }
        public abstract string dataBlock { get; }
        public abstract string delete { get; }
        public abstract string edit { get; }
        public abstract string exit { get; }
        public abstract string expWAV { get; }
        public abstract string file { get; }
        public abstract string fileList { get; }
        public abstract string fileView { get; }
        public abstract string findDupl { get; }
        public abstract string fixCRC { get; }
        public abstract string help { get; }
        public abstract string impWAV { get; }
        public abstract string lenght { get; }
        public abstract string moveDown { get; }
        public abstract string moveUp { get; }
        public abstract string name { get; }
        public abstract string newFile { get; }
        public abstract string open { get; }
        public abstract string paste { get; }
        public abstract string play { get; }
        public abstract string properties { get; }
        public abstract string record { get; }
        public abstract string redo { get; }
        public abstract string rename { get; }
        public abstract string save { get; }
        public abstract string saveAs { get; }
        public abstract string size { get; }
        public abstract string start { get; }
        public abstract string stop { get; }
        public abstract string tools { get; }
        public abstract string typeF { get; }
        public abstract string version { get; }
        public abstract string versionHistory { get; }
        public abstract string view { get; }
        public abstract string website { get; }
        public abstract string undo { get; }
        public abstract string unnamed { get; }

        public abstract string msgNewVersions { get; }
        public abstract string msgOnlyRusVersion { get; }

        public static Lang GetCurrentLanguage()
        {
            if (Properties.Settings.Default.Language == "")
                Properties.Settings.Default.Language = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (Properties.Settings.Default.Language)
            {
                case "ru-RU":
                    return new RussianLanguage();
                default:
                    return new EnglishLanguage();
            }
        }
    }

    public class RussianLanguage : Lang
    {
        public override string language => "ru";
        public override string addBlock => "Добавить блоки";
        public override string about => "О программе";
        public override string allBytes => "Размер данных";
        public override string audio => "Аудио";
        public override string autor => "Автор";
        public override string blockCount => "Количество блоков";
        public override string blockList => "Список блоков";
        public override string blocks => "Блоки";
        public override string copy => "Копировать";
        public override string close => "Закрыть";
        public override string cut => "Вырезать";
        public override string dataBlock => "Блок данных";
        public override string delete => "Удалить";
        public override string edit => "Правка";
        public override string exit => "Закрыть";
        public override string expWAV => "Экспорт в WAV-файл";
        public override string file => "Файл";
        public override string fileList => "Список файлов";
        public override string fileView => "Просмотр файла";
        public override string findDupl => "Поиск дубликатов";
        public override string fixCRC => "Исправление байтов чётности";
        public override string help => "Справка";
        public override string impWAV => "Импорт из WAV-файла";
        public override string lenght => "Длина";
        public override string moveDown => "Переместить вниз";
        public override string moveUp => "Переместить вверх";
        public override string name => "Имя";
        public override string newFile => "Новый";
        public override string open => "Открыть";
        public override string paste => "Вставить";
        public override string play => "Вопроизведение";
        public override string properties => "Параметры";
        public override string record => "Запись";
        public override string redo => "Вернуть";
        public override string rename => "Переименовать";
        public override string save => "Сохранить";
        public override string saveAs => "Сохранить как";
        public override string size => "Размер";
        public override string start => "Старт";
        public override string stop => "Стоп";
        public override string tools => "Инструменты";
        public override string typeF => "Тип";
        public override string version => "Версия";
        public override string versionHistory => "История версий";
        public override string view => "Вид";
        public override string website => "Страница программы";
        public override string undo => "Отменить";
        public override string unnamed => "Безымянный";

        public override string msgNewVersions => "Новую версию этой и других моих программ Вы можете загрузить на сайте";
        public override string msgOnlyRusVersion => "";
    }

    public class EnglishLanguage : Lang
    {
        public override string language => "en";
        public override string addBlock => "Add blocks";
        public override string about => "About";
        public override string allBytes => "Data size";
        public override string audio => "Audio";
        public override string autor => "Autor";
        public override string blockCount => "Blocks count";
        public override string blockList => "Block list";
        public override string blocks => "Blocks";
        public override string copy => "Copy";
        public override string close => "Close";
        public override string cut => "Cut";
        public override string dataBlock => "Data block";
        public override string delete => "Delete";
        public override string edit => "Edit";
        public override string exit => "Exit";
        public override string expWAV => "Export to WAV file";
        public override string file => "File";
        public override string fileList => "File list";
        public override string fileView => "File view";
        public override string findDupl => "Find duplicates";
        public override string fixCRC => "Fix CRCs";
        public override string help => "Help";
        public override string impWAV => "Import from WAV file";
        public override string lenght => "Lenght";
        public override string moveDown => "Move down";
        public override string moveUp => "Move up";
        public override string name => "Name";
        public override string newFile => "New";
        public override string open => "Open";
        public override string paste => "Paste";
        public override string play => "Play";
        public override string properties => "Properties";
        public override string record => "Record";
        public override string redo => "Redo";
        public override string rename => "Rename";
        public override string save => "Save";
        public override string saveAs => "Save as";
        public override string size => "Size";
        public override string start => "Start";
        public override string stop => "Stop";
        public override string tools => "Tools";
        public override string typeF => "Type";
        public override string version => "Version";
        public override string versionHistory => "Versions history";
        public override string view => "View";
        public override string website => "Website page of program";
        public override string undo => "Undo";
        public override string unnamed => "Unnamed";

        public override string msgNewVersions => "New version this and other my programs you can download on website";
        public override string msgOnlyRusVersion => "Sorry! This text is only available in Russian yet :-(";
    }
}
