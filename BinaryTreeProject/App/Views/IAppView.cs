using System;
using System.Windows.Forms;
using BinaryTreeProject.App.Enums;


namespace BinaryTreeProject.App.Views
{
    /*
     * Форма(View) должна реализовывать данный интерфейс.
     * Таким образом, форму можно рассматривать как субъект,
     * с определенным набором данных и событий, на который 
     * может воздействовать Presenter, причем сам  View
     * ни на кого воздействовать не может. 
     */

    public interface IAppView
    {
        // Files operations
        string InputFile { get; }

        string OutputFile { get; }

        string ValueInfo { set; }

        ETreeType TreeType { get; }

        bool Agreement { get; }

        event EventHandler TreeTypeChange;

        event EventHandler ResultClick;

        event EventHandler AgreementChanged;

        // Drawing
        PictureBox DrawWindow { get; }


        // Encode / Decode
        string OutputDecodeFile { get; }

        string OriginalString { get; set; }

        string BinaryString { get; set; }

        event EventHandler EncodeClick;

        event EventHandler DecodeClick;
    }
}
