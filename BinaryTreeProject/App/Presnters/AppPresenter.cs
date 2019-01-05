using System;
using System.Windows.Forms;
using System.Collections.Generic;
using BinaryTreeProject.App.Enums;
using BinaryTreeProject.App.Models;
using BinaryTreeProject.App.Views;
using BinaryTreeProject.Core.Trees.VisualTrees.Additions;


namespace BinaryTreeProject.App.Presnters
{
    /*                      Представитель — реализует взаимодействие между Моделью и Представлением                    */

    public class AppPresenter
    {
        
        //  Экземпляр представления
        IAppViewExtended view;


        //  Экземпляр модели
        IAppModel model;


        //  Сопоставление конкретной конфигурации переключателей с определенным элементом перечисления EDrawTreeMode
        private EDrawTreeMode GetDrawMode()
        {
            EDrawTreeMode mode = EDrawTreeMode.Nothing;

            switch (view.DrawSymbolMode)
            {
                //  Рисовать символы под узлами
                case EDrawSymbolMode.DrawSymbolsUnderNodes:

                    //  
                    if (view.DrawProbability && view.DrawBinaryCodes)
                        mode = EDrawTreeMode.ProbabilityWithBinaryNodesWithCharUnder;
                    else
                    {
                        if (view.DrawProbability)
                            mode = EDrawTreeMode.ProbabilityWithCharUnder;
                        else if (view.DrawBinaryCodes)
                            mode = EDrawTreeMode.BinaryNodesWithCharUnder;
                        else
                            mode = EDrawTreeMode.CharUnder;
                    }

                    break;
                    

                case EDrawSymbolMode.DrawSymbolsInNodes:

                    if (view.DrawProbability)
                        mode = EDrawTreeMode.ProbabilityWithCharNodes;
                    else
                        mode = EDrawTreeMode.CharNodes;

                    break;
                    

                case EDrawSymbolMode.NotDrawSymbols:

                    if (view.DrawProbability && view.DrawBinaryCodes)
                        mode = EDrawTreeMode.ProbabilityWithBinaryNodes;
                    else
                    {
                        if (view.DrawProbability) mode = EDrawTreeMode.Probability;
                        if (view.DrawBinaryCodes) mode = EDrawTreeMode.BinaryNodes;
                    }

                    break;
            }

            return mode; 
        }



        public AppPresenter(IAppViewExtended view)
        {
            //  При запуске программы инициализируем экземпляр представления
            this.view = view;


                this.view.ResultClick +=
                    (o, e) =>
                    {
                        try
                        {
                            //  При нажатии кнопки "Получить результат" создается экземпляр модели
                            model = new AppModel(view.InputFile, view.OutputFile, view.TreeType, view.Agreement,
                            view.OutputDecodeFile, view.OutputMode, view.CSVSeparator);



                            // GRAPHICS EVENTS (пересривовка при изменении размеров окна и режима представления дерева)

                            this.view.DrawWindow.Paint +=
                                    (obj, ex) => model.DrawTree(ex.Graphics, view.DrawWindow.Height, view.DrawWindow.Width,
                                                                view.DrawNodeMode, GetDrawMode());

                            this.view.ModeChanged +=
                                (obj, ex) => this.view.DrawWindow.Invalidate();

                            this.view.DrawWindow.Resize +=
                                (obj, ex) => this.view.DrawWindow.Invalidate();


                            // ENCODE / DECODE EVENTS

                            this.view.EncodeClick +=
                             (obj, ex) => {
                                 try
                                 {
                                     this.view.BinaryString = model.Encode(view.OriginalString);
                                 }
                                 catch (KeyNotFoundException)
                                 {
                                     MessageBox.Show("Введен символ, которого нет в используемом алфавите!");
                                 }
                                 catch (Exception subEx)
                                 {
                                     MessageBox.Show(subEx.Message);
                                 }
                             };
                                  

                            this.view.DecodeClick +=
                                (obj, ex) => this.view.OriginalString = model.Decode(view.BinaryString, view.OutputDecodeFile);


                            // OTHER EVENTS

                            this.view.TreeTypeChange +=
                                (obj, ex) =>
                                {
                                    model.ReplaceTree(view.TreeType);
                                    this.view.DrawWindow.Invalidate();
                                };

                            this.view.AgreementChanged +=
                                (obj, ex) =>
                                {
                                    model.ReplaceAgreement(view.Agreement);
                                    this.view.DrawWindow.Invalidate();
                                };

                            //  Переход в полноэкранный режим
                            this.view.FullScreenModeClick +=
                            (obj, ex) =>
                            {
                                this.view.DrawWindow.Height = model.GetOptimalDrawingPanelHeigth();
                                this.view.DrawWindow.Width = model.GetOptimalDrawingPanelWidth();
                                this.view.DrawWindow.Invalidate();
                            };

                            // Вывод результатов и прорисовка дерева

                            model.PrintResult();
                            this.view.ValueInfo = model.GetValueInfo().ToString();
                            this.view.DrawWindow.Invalidate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    };

        }

    }
}
