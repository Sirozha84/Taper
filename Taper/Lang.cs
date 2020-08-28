namespace Taper
{
    static class Lang
    {
        public static string langCode;

        public static string addBlock;
        public static string about;
        public static string allBytes;
        public static string audio;
        public static string autor;
        public static string block;
        public static string blockCount;
        public static string blockList;
        public static string blocks;
        public static string bytes;
        public static string cancel;
        public static string clear;
        public static string close;
        public static string colored;
        public static string copy;
        public static string cut;
        public static string dataBlock;
        public static string defaultDevices;
        public static string delete;
        public static string edit;
        public static string exit;
        public static string expWAV;
        public static string file;
        public static string fileList;
        public static string fileView;
        public static string find;
        public static string findDupl;
        public static string fixCRC;
        public static string help;
        public static string impWAV;
        public static string language;
        public static string lenght;
        public static string loadedData;
        public static string loadTo;
        public static string main;
        public static string moveDown;
        public static string moveUp;
        public static string name;
        public static string newFile;
        public static string olwaysCenter;
        public static string open;
        public static string paste;
        public static string play;
        public static string position;
        public static string properties;
        public static string record;
        public static string redo;
        public static string rememberLastPosition;
        public static string rename;
        public static string save;
        public static string saveAs;
        public static string saveImage;
        public static string size;
        public static string soundSource;
        public static string start;
        public static string startPosition;
        public static string stop;
        public static string time;
        public static string tools;
        public static string typeF;
        public static string version;
        public static string versionHistory;
        public static string view;
        public static string website;
        public static string undo;
        public static string unnamed;

        public static string msgAddBlocks;
        public static string msgAllCRCsIsOK;
        public static string msgCRCsFixed;
        public static string msgErrorLoad;
        public static string msgErrorSave;
        public static string msgFoundDuplicatesSelected;
        public static string msgNewVersions;
        public static string msgOnlyRusVersion;
        public static string msgPleaseWait;
        public static string msgSaveChange;

        public static string FilterAll;
        public static string FilterBMP;
        public static string FilterSel;
        public static string FilterWAV;

        public static void Init()
        {
            if (Properties.Settings.Default.Language == "")
                Properties.Settings.Default.Language = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (Properties.Settings.Default.Language)
            {
                case "ru-RU":

                    langCode = "ru";

                    addBlock = "Добавить блоки";
                    about = "О программе";
                    allBytes = "Размер данных";
                    audio = "Аудио";
                    autor = "Автор";
                    block = "Блок";
                    blockCount = "Количество блоков";
                    blockList = "Список блоков";
                    blocks = "Блоки";
                    bytes = "Байт";
                    cancel = "Отмена";
                    clear = "Очистить";
                    close = "Закрыть";
                    colored = "Цветной";
                    copy = "Копировать";
                    cut = "Вырезать";
                    dataBlock = "Блок данных";
                    defaultDevices = "Устройства по умолчанию";
                    delete = "Удалить";
                    edit = "Правка";
                    exit = "Закрыть";
                    expWAV = "Экспорт в WAV-файл";
                    file = "Файл";
                    fileList = "Список файлов";
                    fileView = "Просмотр файла";
                    find = "Поиск";
                    findDupl = "Поиск дубликатов";
                    fixCRC = "Исправление контрольных сумм";
                    help = "Справка";
                    impWAV = "Импорт из WAV-файла";
                    language = "Язык (Language)";
                    lenght = "Длина";
                    loadedData = "Загруженные данные";
                    loadTo = "Адрес загрузки";
                    main = "Основные";
                    moveDown = "Переместить вниз";
                    moveUp = "Переместить вверх";
                    name = "Имя";
                    newFile = "Новый";
                    olwaysCenter = "Всегда в центре экрана";
                    open = "Открыть";
                    paste = "Вставить";
                    play = "Вопроизведение";
                    position = "Позиция";
                    properties = "Параметры";
                    record = "Запись";
                    redo = "Вернуть";
                    rememberLastPosition = "Запоминать последнее положение";
                    rename = "Переименовать";
                    save = "Сохранить";
                    saveAs = "Сохранить как";
                    saveImage = "Сохранить изображение";
                    size = "Размер";
                    soundSource = "Источник звука";
                    start = "Старт";
                    startPosition = "Начальная позиция окна";
                    stop = "Стоп";
                    time = "Время";
                    tools = "Инструменты";
                    typeF = "Тип";
                    version = "Версия";
                    versionHistory = "История версий";
                    view = "Вид";
                    website = "Страница программы";
                    undo = "Отменить";
                    unnamed = "Безымянный";

                    msgAddBlocks = "Добавить блоки в проект? Нет - открыть файл.";
                    msgAllCRCsIsOK = "Все контрольные суммы в порядке";
                    msgCRCsFixed = "Контрольные суммы исправлены";
                    msgErrorLoad = "Произошла ошибка при открытии файла или формат файла не поддерживается.";
                    msgErrorSave = "Произошла ошибка при сохранении файла. Файл не сохранён.";
                    msgFoundDuplicatesSelected = "Отмечены найденые дубликаты файлов";
                    msgNewVersions = "Новую версию этой и других моих программ Вы можете загрузить на сайте";
                    msgOnlyRusVersion = "";
                    msgPleaseWait = "Минутку...";
                    msgSaveChange = "Сохранить изменения в файле";

                    FilterAll = "Образ ленты (*.tap, *.tzx)|*.tap;*.tzx|Все файлы (*.*)|*.*";
                    FilterBMP = "Точечный рисунок (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
                    FilterSel = "Образ ленты TAP (*.tap)|*.tap|Образ ленты TZX (*.tzx)|*.tzx|Все файлы (*.*)|*.*";
                    FilterWAV = "Звуковой файл (*.wav)|*.wav|Все файлы (*.*)|*.*";

                    break;
                default:

                    langCode = "en";

                    addBlock = "Add blocks";
                    about = "About";
                    allBytes = "Data size";
                    audio = "Audio";
                    autor = "Autor";
                    block = "Block";
                    blockCount = "Blocks count";
                    blockList = "Block list";
                    blocks = "Blocks";
                    bytes = "Bytes";
                    cancel = "Cancel";
                    clear = "Clear";
                    close = "Close";
                    colored = "Colored";
                    copy = "Copy";
                    cut = "Cut";
                    dataBlock = "Data block";
                    defaultDevices = "Default devices";
                    delete = "Delete";
                    edit = "Edit";
                    exit = "Exit";
                    expWAV = "Export to WAV file";
                    file = "File";
                    fileList = "File list";
                    fileView = "File view";
                    find = "Find";
                    findDupl = "Find duplicates";
                    fixCRC = "Fix CRCs";
                    help = "Help";
                    impWAV = "Import from WAV file";
                    language = "Language";
                    lenght = "Lenght";
                    loadedData = "Loaded data";
                    loadTo = "Load to";
                    main = "Main";
                    moveDown = "Move down";
                    moveUp = "Move up";
                    name = "Name";
                    newFile = "New";
                    olwaysCenter = "Olways in center screen";
                    open = "Open";
                    paste = "Paste";
                    play = "Play";
                    position = "Position";
                    properties = "Properties";
                    record = "Record";
                    redo = "Redo";
                    rememberLastPosition = "Remember last position";
                    rename = "Rename";
                    save = "Save";
                    saveAs = "Save as";
                    saveImage = "Save image";
                    size = "Size";
                    soundSource = "Sound source";
                    start = "Start";
                    startPosition = "Start position";
                    stop = "Stop";
                    time = "Time";
                    tools = "Tools";
                    typeF = "Type";
                    version = "Version";
                    versionHistory = "Version history";
                    view = "View";
                    website = "Website page of program";
                    undo = "Undo";
                    unnamed = "Unnamed";

                    msgAddBlocks = "Add blocks to project? No - open file.";
                    msgAllCRCsIsOK = "All CRCs is OK";
                    msgCRCsFixed = "CRCs fixed";
                    msgErrorLoad = "Error open file of file not support.";
                    msgErrorSave = "Error save file. File not saved.";
                    msgFoundDuplicatesSelected = "Found duplicates are selected";
                    msgNewVersions = "New version this and other my programs you can download on website";
                    msgOnlyRusVersion = "Sorry! This text is only available in Russian yet :-(";
                    msgPleaseWait = "Please wait...";
                    msgSaveChange = "Save change in file";

                    FilterAll = "Tape image (*.tap, *.tzx)|*.tap;*.tzx|All files (*.*)|*.*";
                    FilterBMP = "Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                    FilterSel = "Tape image TAP (*.tap)|*.tap|Tape image TZX (*.tzx)|*.tzx|All files (*.*)|*.*";
                    FilterWAV = "Audio file (*.wav)|*.wav|All files (*.*)|*.*";

                    break;
            }
        }
    }
}