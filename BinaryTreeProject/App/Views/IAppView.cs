using System;
using System.Windows.Forms;
using BinaryTreeProject.App.Enums;


namespace BinaryTreeProject.App.Views
{
    /*
     *      Пользовательский интрефейс приложения (базовый набор свойств)
     *      
     *      Представление(View) (форма) должно реализовывать данный интерфейс.
     *  Таким образом, представление можно рассматривать как субъект,
     *  с определенным набором данных и событий, на который может воздействовать 
     *  Presenter, причем сам  View ни на кого воздействовать не может. 
     *  
     */

    public interface IAppView
    {

        /*                              Основные поля                           */


        //  Входной файл (содержащий символы и вероятности)        
        string InputFile { get; }


        //  Выходной файл (в него записываются результаты работы программы, 
        //  в зависимости от выбранного алгоритма)
        string OutputFile { get; }


        //  Значение средней информации в битах
        string ValueInfo { set; }


        //  Значение энтропии
        string ValueEntropy { set; }


        //  Область для рисования дерева
        PictureBox DrawWindow { get; }


        //  Тип входного файла
        bool IsTextFile { get; }


        //  Получение результата
        event EventHandler ResultClick;


        //  Переход в полноэкранный режим
        event EventHandler FullScreenModeClick;


        /*                         Параметры                     */


        //  Алгоритм для кодирования и построения дерева
        ETreeType TreeType { get; }


        //  Соглашение
        bool Agreement { get; }


        //  Разделитель для значений в CSV файле
        char CSVSeparator { get; }


        //  Формат вывода CSV/TXT
        EOutputMode OutputMode { get; }


        //  Изменение алгоритма кодирования
        event EventHandler TreeTypeChange;

        
        //  Изменение соглашения
        event EventHandler AgreementChanged;


        //  Изменение формата вывода
        event EventHandler OutputModeChanged;




        /*                Кодирование / Декодирование          */


        //  Файл для выводов результатов пошагового декодирования
        string OutputDecodeFile { get; }


        //  Исходная строка, которую необходимо закодировать
        string OriginalString { get; set; }


        //  Строка, в которую выводится закодированное слово
        string BinaryString { get; set; }


        //  Закодировать 
        event EventHandler EncodeClick;


        //  Декодировать
        event EventHandler DecodeClick;
    }
}