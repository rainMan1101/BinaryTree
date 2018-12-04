using System;
using BinaryTreeProject.App.Enums;


namespace BinaryTreeProject.App.Views
{
    public interface IAppViewExtended : IAppView
    {

        EDrawNodeMode DrawNodeMode { get; }

        EDrawSymbolMode DrawSymbolMode { get; }

        bool DrawProbability { get; }

        bool DrawBinaryCodes { get; }

        event EventHandler ModeChanged;

    }
}
