namespace Taper
{
    static class Lang
    {
        public static string langCode;

        public static string addBlocks;
        public static string about;
        public static string audio;
        public static string author;
        public static string block;
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
        public static string dataSize;
        public static string defaultDevices;
        public static string delete;
        public static string edit;
        public static string enterNewName;
        public static string exit;
        public static string exportToWAV;
        public static string file;
        public static string fileList;
        public static string fileView;
        public static string find;
        public static string findDuplicates;
        public static string fixCRC;
        public static string help;
        public static string importFromWAV;
        public static string language;
        public static string lenght;
        public static string loadedData;
        public static string loadTo;
        public static string main;
        public static string moveDown;
        public static string moveUp;
        public static string name;
        public static string newFile;
        public static string newVersions;
        public static string numberOfBlocks;
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

        public static string askSaveChange;

        public static string msgAddBlocks;
        public static string msgAllCRCsIsOK;
        public static string msgAudioDeviceError;
        public static string msgCRCsFixed;
        public static string msgDuplicatesNotFound;
        public static string msgFoundDuplicatesSelected;
        public static string msgNoDeviceIsFound;
        public static string msgOnlyRusVersion;
        public static string msgPleaseWait;
        public static string msgSelectionMustBeContinuous;

        public static string errorCommandNotExist;
        public static string errorLoad;
        public static string errorSave;

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

                    addBlocks = "Добавить блоки";
                    about = "О программе";
                    dataSize = "Размер данных";
                    audio = "Аудио";
                    author = "Автор";
                    block = "Блок";
                    numberOfBlocks = "Количество блоков";
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
                    enterNewName = "Введите новое имя:";
                    exit = "Закрыть";
                    exportToWAV = "Экспорт в WAV-файл";
                    file = "Файл";
                    fileList = "Список файлов";
                    fileView = "Просмотр файла";
                    find = "Поиск";
                    findDuplicates = "Поиск дубликатов";
                    fixCRC = "Исправление контрольных сумм";
                    help = "Справка";
                    importFromWAV = "Импорт из WAV-файла";
                    language = "Язык (Language)";
                    lenght = "Длина";
                    loadedData = "Загруженные данные";
                    loadTo = "Адрес загрузки";
                    main = "Основные";
                    moveDown = "Переместить вниз";
                    moveUp = "Переместить вверх";
                    name = "Имя";
                    newFile = "Новый";
                    newVersions = "Новую версию этой и других моих программ Вы можете загрузить на сайте";
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

                    askSaveChange = "Сохранить изменения в файле";
                    
                    msgAddBlocks = "Добавить блоки в проект? Нет - открыть файл.";
                    msgAllCRCsIsOK = "Все контрольные суммы в порядке.";
                    msgAudioDeviceError = "Ошибка работы с аудио устройством.";
                    msgCRCsFixed = "Контрольные суммы исправлены.";
                    msgDuplicatesNotFound = "Дубликаты не обнаружены.";
                    msgFoundDuplicatesSelected = "Отмечены найденые дубликаты файлов.";
                    msgNoDeviceIsFound = "Не обнаружено ни одного аудио устройства.";
                    msgOnlyRusVersion = "";
                    msgPleaseWait = "Минутку...";
                    msgSelectionMustBeContinuous = "Выделение должно быть непрерывным";

                    errorCommandNotExist = "Ошибка! Команда не существует.";
                    errorLoad = "Произошла ошибка при открытии файла или формат файла не поддерживается.";
                    errorSave = "Произошла ошибка при сохранении файла. Файл не сохранён.";

                    FilterAll = "Образ ленты (*.tap, *.tzx)|*.tap;*.tzx|Все файлы (*.*)|*.*";
                    FilterBMP = "Точечный рисунок (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
                    FilterSel = "Образ ленты TAP (*.tap)|*.tap|Образ ленты TZX (*.tzx)|*.tzx|Все файлы (*.*)|*.*";
                    FilterWAV = "Звуковой файл (*.wav)|*.wav|Все файлы (*.*)|*.*";

                    break;

                case "it-IT":

                    langCode = "it";

                    addBlocks = "Aggiungi Blocchi";
                    about = "Info";
                    audio = "Audio";
                    author = "Autore";
                    block = "Blocco";
                    blockList = "Lista Blocchi";
                    blocks = "Blocchi";
                    bytes = "Bytes";
                    cancel = "Annulla";
                    clear = "Pulisci";
                    close = "Chiudi";
                    colored = "Colori";
                    copy = "Copias";
                    cut = "Taglia";
                    dataBlock = "Blocco Dati";
                    dataSize = "Dimensione";
                    defaultDevices = "Periferiche Default";
                    delete = "Cancella";
                    edit = "Edita";
                    enterNewName = "Enter new name:";
                    exit = "Esci";
                    exportToWAV = "Esporta in file .WAV";
                    file = "File";
                    fileList = "Lista File";
                    fileView = "Esamina File";
                    find = "Cerca";
                    findDuplicates = "Cerca duplicati";
                    fixCRC = "Ripara CRC";
                    help = "Aiuto";
                    importFromWAV = "Importa da file .WAV";
                    language = "Lingua (Language)";
                    lenght = "Lunghezza";
                    loadedData = "Dati Caricati";
                    loadTo = "Carica in";
                    main = "Home";
                    moveDown = "Muovi giu'";
                    moveUp = "Muovi su'";
                    name = "Nome";
                    newFile = "Nuovo";
                    newVersions = "Puoi scaricare questo ed altri programmi dal sito Web.";
                    numberOfBlocks = "Conta Blocchi";
                    olwaysCenter = "Sempre a centro schermo";
                    open = "Apri";
                    paste = "Incolla";
                    play = "Esegui";
                    position = "Posizione";
                    properties = "Propprieta'";
                    record = "Registra";
                    redo = "Ripeti";
                    rememberLastPosition = "Ricorda ultima posizione";
                    rename = "Rinomina";
                    save = "Salva";
                    saveAs = "Salva con mone";
                    saveImage = "Salva immagine";
                    size = "Dimensione";
                    soundSource = "Sorgente Suono";
                    start = "Inizio";
                    startPosition = "Posizione Iniziale";
                    stop = "Ferma";
                    time = "Tempo";
                    tools = "Utilita'";
                    typeF = "Tipo";
                    version = "Versioni";
                    versionHistory = "Storico Versione";
                    view = "Vista";
                    website = "Sito Web del programma";
                    undo = "Annulla";
                    unnamed = "Senza nome";

                    askSaveChange = "Salva modifiche in file";

                    msgAddBlocks = "Aggiungi blocchi al progetto? No - Apri File.";
                    msgAllCRCsIsOK = "Tutti i CRCs sono OK.";
                    msgAudioDeviceError = "Audio device error.";
                    msgCRCsFixed = "CRCs Corretti.";
                    msgDuplicatesNotFound = "Duplicates not found.";
                    msgFoundDuplicatesSelected = "Trovati File duplicati e selezionati.";
                    msgNoDeviceIsFound = "No audio device is found.";
                    msgOnlyRusVersion = "Scusa! Questo testo e' disponibile solo in lingua Russa :-(";
                    msgPleaseWait = "Prego Attendi...";
                    msgSelectionMustBeContinuous = "Selection must be continuous.";

                    errorCommandNotExist = "Error! Command Not Exist.";
                    errorLoad = "Errore apertura file o file non supportato.";
                    errorSave = "Errore Salvataggio File. File non salvato.";

                    FilterAll = "Immagine Tape (*.tap, *.tzx)|*.tap;*.tzx|All files (*.*)|*.*";
                    FilterBMP = "Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                    FilterSel = "Immagine Tape TAP (*.tap)|*.tap|Tape image TZX (*.tzx)|*.tzx|All files (*.*)|*.*";
                    FilterWAV = "File Audio (*.wav)|*.wav|All files (*.*)|*.*";

                    break;

                default:

                    langCode = "en";

                    addBlocks = "Add blocks";
                    about = "About";
                    audio = "Audio";
                    author = "Author";
                    block = "Block";
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
                    dataSize = "Data size";
                    defaultDevices = "Default devices";
                    delete = "Delete";
                    edit = "Edit";
                    enterNewName = "Enter new name:";
                    exit = "Exit";
                    exportToWAV = "Export to WAV file";
                    file = "File";
                    fileList = "File list";
                    fileView = "File view";
                    find = "Find";
                    findDuplicates = "Find duplicates";
                    fixCRC = "Fix CRCs";
                    help = "Help";
                    importFromWAV = "Import from WAV file";
                    language = "Language";
                    lenght = "Lenght";
                    loadedData = "Loaded data";
                    loadTo = "Load to";
                    main = "Main";
                    moveDown = "Move down";
                    moveUp = "Move up";
                    name = "Name";
                    newFile = "New";
                    newVersions = "New version this and other my programs you can download on website";
                    numberOfBlocks = "Number of blocks";
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
                    msgAudioDeviceError = "Audio device error.";
                    msgCRCsFixed = "CRCs fixed";
                    msgDuplicatesNotFound = "Duplicates not found";
                    msgFoundDuplicatesSelected = "Found duplicates are selected";
                    msgNoDeviceIsFound = "No audio device is found";
                    msgOnlyRusVersion = "Sorry! This text is only available in Russian yet :-(";
                    msgPleaseWait = "Please wait...";
                    askSaveChange = "Save change in file";
                    msgSelectionMustBeContinuous = "Selection must be continuous";

                    errorCommandNotExist = "Error! Command Not Exist";
                    errorLoad = "Error open file of file not support.";
                    errorSave = "Error save file. File not saved.";

                    FilterAll = "Tape image (*.tap, *.tzx)|*.tap;*.tzx|All files (*.*)|*.*";
                    FilterBMP = "Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                    FilterSel = "Tape image TAP (*.tap)|*.tap|Tape image TZX (*.tzx)|*.tzx|All files (*.*)|*.*";
                    FilterWAV = "Audio file (*.wav)|*.wav|All files (*.*)|*.*";

                    break;
            }
        }
    }
}