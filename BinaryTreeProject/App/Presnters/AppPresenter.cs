using BinaryTreeProject.App.Enums;
using BinaryTreeProject.App.Models;
using BinaryTreeProject.App.Views;
using BinaryTreeProject.Core.Trees.VisualTrees.Additions;


namespace BinaryTreeProject.App.Presnters
{
    public class AppPresenter
    {
        IAppViewExtended view;
        IAppModel model = null; //??


        private EDrawTreeMode GetDrawMode()
        {
            EDrawTreeMode mode = EDrawTreeMode.Nothing;

            switch (view.DrawSymbolMode)
            {
                case EDrawSymbolMode.DrawSymbolsUnderNodes:

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
            this.view = view;
            
            //this.view.DrawWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            //PictureBoxSizeMode.AutoSize;
            //this.view.DrawWindow.Image = new Bitmap(2000, 1000);


           

            this.view.ResultClick +=
                (o, e) =>
                {
                    //  try/catch
                    model = new AppModel(view.InputFile, view.OutputFile, view.TreeType, view.Agreement);


                    // GRAPHICS EVENTS

                    this.view.DrawWindow.Paint +=
                        (obj, ex) => model.DrawTree(ex.Graphics, view.DrawWindow.Height, view.DrawWindow.Width,
                                                    view.DrawNodeMode, GetDrawMode());

                    this.view.ModeChanged +=
                        (obj, ex) => this.view.DrawWindow.Invalidate();

                    this.view.DrawWindow.Resize +=
                        (obj, ex) => this.view.DrawWindow.Invalidate();


                    // OTHER EVENTS

                    this.view.EncodeClick +=
                        (obj, ex) => this.view.BinaryString = model.Encode(view.OriginalString);

                    this.view.DecodeClick +=
                        (obj, ex) => this.view.OriginalString = model.Decode(view.BinaryString, view.OutputDecodeFile);

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

                    model.PrintResult();
                    this.view.ValueInfo = model.GetValueInfo().ToString();
                    this.view.DrawWindow.Invalidate();
                };

        }

    }
}
