using System.Windows.Forms;
using System.Drawing;

namespace Taper
{
    partial class FormAbout : Form
    {
		public FormAbout()
		{
			InitializeComponent();
			Lang.Init();

			Text = Lang.about;
			tabPageAbout.Text = Lang.about;
			labelVersion.Text = Lang.version + ": " + Application.ProductVersion;
			labelName.Text = Application.ProductName;
			labelAutor.Text = Lang.author + (Lang.langCode == "ru" ? ": Сергей Гордеев" : ": Sergey Gordeev");
			labelNewVersions.Text = Lang.msgNewVersions;
			tabPageHistory.Text = Lang.versionHistory;
			buttonClose.Text = Lang.close;

			Font fontR = new Font(history.Font.FontFamily, history.Font.Size, FontStyle.Regular);
			Font fontB = new Font(history.Font.FontFamily, history.Font.Size, FontStyle.Bold);

			if (Lang.langCode != "ru")
			{
				history.SelectionFont = fontR;
				history.SelectionAlignment = HorizontalAlignment.Center;
				history.AppendText("\n\n\n\n\n\n" + Lang.msgOnlyRusVersion);
				return;
			}

			history.SelectionFont = fontB;
			history.AppendText("Версия 3.0 Beta (19.08.2020)\n\n");
			history.SelectionFont = fontR;
			history.AppendText("• Ещё в разработке\n\n");
			history.SelectionFont = fontB;
			history.AppendText("Версия 2.1 (09.06.2015)\n\n");
			history.SelectionFont = fontR;
			history.AppendText("• Просмотр шрифта с возможностью сохранения изображения\n" +
				"• Drag'n'Drop: теперь файлы можно открывать перекинув их в окно программы\n" +
				"• Ещё TAP-файлы теперь можно \"открывать с помощью\"(принимаются параметры командной строки)\n" +
				"• Доработан просмотр BASIC-программ и добавлена возможность включить отступы для удобочитаемости\n" +
				"• Доработан дизассемблер, теперь воспринимаются команды с IX и IY\n" +
				"• Исправлено сохранение изображений без атрибутов\n" +
				"• Исправлен вопрос о сохранении безымянного файла, когда имя было известно\n" +
				"• Исправлено добавление блоков из WAV (действие не отменялось и не сохранялось при закрытии)\n" +
				"• Исправлены некоторые ошибки при загрузке WAV-файлов\n" +
				"• Исправлено сохранение в WAV нестандартных блоков\n" +
				"• Исправлено переименование файлов (оставались артефакты от старого имени)\n" +
				"• Прочие мелкие исправления\n\n");
			history.SelectionFont = fontB;
			history.AppendText("Версия 2.0 (16.07.2011)\n\n");
			history.SelectionFont = fontR;
			history.AppendText("• Программа полностью переписана с ноля на .NET\n" +
				"• Добавлены привычные удобства типа отмены, возврата, копирования, вырезания и вставки\n" +
				"• Выделять теперь можно сразу несколько блоков и делать с ними вышеуказанные операции\n" +
				"• Группы файлов теперь тоже можно перемещать\n" +
				"• Заголовок и блок данных группируются в файлы. А также можно видеть о них подробную информацию\n" +
				"• Заголовки, блоки данных и нестандартные блоки теперь различаются правильно\n" +
				"• Доработан алгоритм распознавания аудиозаписей, настройка чувствительности " +
				"упростилась, а детальный лог позволяет реставрировать аудиозаписи\n" +
				"• Программа теперь понимает WAV-файлы с любой частотой, количества каналов, 8 и 16 бит\n" +
				"• Сохранение в WAV-файлы также улучшено\n" +
				"• Улучшенный просмотр рисунков с возможностью растягивать картинку и сохранять её в файл\n" +
				"• При просмотре изображения появилась возможность смещения (если изображение начинается не с первого байта\n" +
				"• Добавился инструмент для поиска дубликатов\n" +
				"• Импорт и экспорт в TZX-файлы.\n" +
				"• Возможность переименовывать заголовки блоков\n" +
				"• Дизассемблер\n\n");
			history.SelectionFont = fontB;
			history.AppendText("Версия 1.2 (06.09.2007)\n\n");
			history.SelectionFont = fontR;
			history.AppendText("• Уточнение пользователем неясных сигналов при загрузке из WAV-файлов\n" +
				"• Изменён анализ спектра\n" +
				"• Исправлена ошибка, возникающая при добавлении загруженных блоков в проект (не добавлялась контрольная сумма)\n" +
				"• Добавлен инструмент для проверки и исправления контрольных сумм\n" +
				"• Отмена долгих процессов\n\n");
			history.SelectionFont = fontB;
			history.AppendText("Версия 1.1 (01.09.2007)\n\n");
			history.SelectionFont = fontR;
			history.AppendText("• Загрузка из WAV-файлов\n" +
				"• Анализатор спектра WAV-файлов\n" +
				"• Сохранение проекта в WAV-файл\n" +
				"• Новая панель управления\n" +
				"• Добавлена строка состояния с выводом количества блоков, файлов и общего размера данных\n" +
				"• Индикация прогресса при загрузке/ сохранении файлов\n" +
				"• Справка\n" +
				"• Доработано отображение BASIC-программ\n" +
				"• Исправлены недочёты в диалоговых окнах\n" +
				"• Исправлен просмотр изображений, теперь оно не затирается другими окнами\n\n");
			history.SelectionFont = fontB;
			history.AppendText("Версия 1.0 (26.08.2004)\n\n");
			history.SelectionFont = fontR;
			history.AppendText("• Добавлен просмотр текста\n" +
				"• Добавлен просмотр кода\n" +
				"• Доработан просмотр изображений\n" +
				"• Добавлена панель управления\n" +
				"• Добавлена возможность изменять размер основного окна\n"+
				"• Исправлено множество ошибок, среди которых неправильное сохранение файла\n\n");
			history.SelectionFont = fontB;
			history.AppendText("Версия 0.1 Beta (17.08.2004)\n\n");
			history.SelectionFont = fontR;
			history.AppendText("• Основная работа с TAP-файлами: простое редактирование, просмотр\n");
		}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.sg-software.ru");
        }

        private void linkLabelSite_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.sg-software.ru");
        }
    }
}
