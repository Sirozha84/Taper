namespace Taper
{
    static class Lang
    {
        public static string addb, about, audio;
        public static string blockList, blocks;
        public static string copy, cut;
        public static string delete;
        public static string edit, exit, expWAV;
        public static string file, fileList, fileView, findDupl, fixCRC;
        public static string help;
        public static string impWAV;
        public static string moveDown, moveUp;
        public static string newFile;
        public static string open;
        public static string paste, play, properties;
        public static string record, redo, rename;
        public static string save, saveAs, stop;
        public static string tools;
        public static string view;
        public static string website;
        public static string undo, unnamed;
        public static void Init()
        {
            if (Properties.Settings.Default.Language == "")
                Properties.Settings.Default.Language = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (Properties.Settings.Default.Language)
            {
                case "ru-RU":
                    addb = "Добавить блоки";
                    about = "О программе";
                    audio = "Аудио";
                    blockList = "Список блоков";
                    blocks = "Блоки";
                    copy = "Копировать";
                    cut = "Вырезать";
                    delete = "Удалить";
                    edit = "Правка";
                    exit = "Закрыть";
                    expWAV = "Экспорт в WAV-файл";
                    file = "Файл";
                    fileList = "Список файлов";
                    fileView = "Просмотр файла";
                    findDupl = "Поиск дубликатов";
                    fixCRC = "Исправление байтов чётности";
                    help = "Справка";
                    impWAV = "Импорт из WAV-файла";
                    moveDown = "Переместить вниз";
                    moveUp = "Переместить вверх";
                    newFile = "Новый";
                    open = "Открыть";
                    paste = "Вставить";
                    play = "Вопроизведение";
                    properties = "Параметры";
                    record = "Запись";
                    redo = "Вернуть";
                    rename = "Переименовать";
                    save = "Сохранить";
                    saveAs = "Сохранить как";
                    stop = "Стоп";
                    tools = "Инструменты";
                    view = "Вид";
                    website = "Страница программы";
                    undo = "Отменить";
                    unnamed = "Безымянный";
                    break;
                default:
                    addb = "Add blocks";
                    about = "About";
                    audio = "Audio";
                    blockList = "Block list";
                    blocks = "Blocks";
                    copy = "Copy";
                    cut = "Cut";
                    delete = "Delete";
                    edit = "Edit";
                    exit = "Exit";
                    expWAV = "Export to WAV file";
                    file = "File";
                    fileList = "File list";
                    fileView = "File view";
                    findDupl = "Find duplicates";
                    fixCRC = "Fix CRCs";
                    help = "Help";
                    impWAV = "Import from WAV file";
                    moveDown = "Move down";
                    moveUp = "Move up";
                    newFile = "New";
                    open = "Open";
                    paste = "Paste";
                    play = "Play";
                    properties = "Properties";
                    record = "Record";
                    redo = "Redo";
                    rename = "Rename";
                    save = "Save";
                    saveAs = "Save as";
                    stop = "Stop";
                    tools = "Tools";
                    view = "View";
                    website = "Website page of program";
                    undo = "Undo";
                    unnamed = "Unnamed";
                    break;
            }
        }
    }
}
