namespace Taper
{
    static class Lang
    {
        public static string addb, about, allBlocks, allBytes, audio;
        public static string blockList, blocks;
        public static string copy, cut;
        public static string dataBlock, delete;
        public static string edit, exit, expWAV;
        public static string file, fileList, fileView, findDupl, fixCRC;
        public static string help;
        public static string impWAV;
        public static string lenght;
        public static string moveDown, moveUp;
        public static string name, newFile;
        public static string open;
        public static string paste, play, properties;
        public static string record, redo, rename;
        public static string save, saveAs, size, start, stop;
        public static string tools, typeF;
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
                    allBlocks = "Количество блоков";
                    allBytes = "Размер данных";
                    audio = "Аудио";
                    blockList = "Список блоков";
                    blocks = "Блоки";
                    copy = "Копировать";
                    cut = "Вырезать";
                    dataBlock = "Блок данных";
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
                    lenght = "Длина";
                    moveDown = "Переместить вниз";
                    moveUp = "Переместить вверх";
                    name = "Имя";
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
                    size = "Размер";
                    start = "Старт";
                    stop = "Стоп";
                    tools = "Инструменты";
                    typeF = "Тип";
                    view = "Вид";
                    website = "Страница программы";
                    undo = "Отменить";
                    unnamed = "Безымянный";
                    break;
                default:
                    addb = "Add blocks";
                    about = "About";
                    allBlocks = "Blocks count";
                    allBytes = "Data size";
                    audio = "Audio";
                    blockList = "Block list";
                    blocks = "Blocks";
                    copy = "Copy";
                    cut = "Cut";
                    dataBlock = "Data block";
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
                    lenght = "Lenght";
                    moveDown = "Move down";
                    moveUp = "Move up";
                    name = "Name";
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
                    size = "Size";
                    start = "Start";
                    stop = "Stop";
                    tools = "Tools";
                    typeF = "Type";
                    view = "View";
                    website = "Website page of program";
                    undo = "Undo";
                    unnamed = "Unnamed";
                    break;
            }
        }
    }
}
