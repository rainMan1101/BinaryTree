using System;
using BinaryTreeProject.App.Enums;


namespace BinaryTreeProject.App.Views
{
    /*      
     *      Пользовательский интрефейс приложения (расширенный набор свойств)
     *            
     *  Влючает в себя свойства визуального представления дерева.
     *  
     */

    public interface IAppViewExtended : IAppView
    {
        //  Режим представления узлов дерева  
        EDrawNodeMode DrawNodeMode { get; }


        //  Режим отображения символов алфавита 
        EDrawSymbolMode DrawSymbolMode { get; }


        //  Отображать вероятность ?
        bool DrawProbability { get; }


        //  Отображать двоичный код внутри узлов дерева ?
        bool DrawBinaryCodes { get; }


        //  Изменение какого-либо из свойств отображения дерева
        event EventHandler ModeChanged;
    }
}
