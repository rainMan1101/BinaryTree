using System;
using System.Drawing;
using BinaryTreeProject.Core.Trees.VisualNodes;
using BinaryTreeProject.Core.Trees.VisualTrees.Additions;


namespace BinaryTreeProject.Core.Trees.VisualTrees
{
    public class TreeWithNodes : VisualTree
    {
        //public TreeWithNodes(Graphics graph, Tree tree, EDrawTreeMode mode) : base(graph, tree) { }
        private IVisualNode visualNode = null; //нужно ли???

        // 2?   ->  1: HEIGTH_FONT
        private float HEIGTH_PROB_FONT;
        private float WIDTH_PROB_FONT;
        private Font FONT_PROB;

        private float HEIGTH_CHAR_FONT;
        private float WIDTH_CHAR_FONT;
        private Font FONT_CHAR;

        private SolidBrush DEFAULT_BRUSH;

        private EDrawTreeMode mode;



        public TreeWithNodes(Tree tree, IVisualNode visualNode, EDrawTreeMode mode) : base(tree)
        {
            this.visualNode = visualNode;
            this.mode = mode;
            DEFAULT_BRUSH = new SolidBrush(Color.LimeGreen);
        }

        public override void DrawTree(int width, int heigth, Graphics graph)
        {
            FontInfo fontInfo;
            fontInfo = AdjustmentFontSize.GetFontSize(StepHeigth / 3, 1.23); // (test data)
            HEIGTH_PROB_FONT = fontInfo.RealFontHeigth;
            WIDTH_PROB_FONT = fontInfo.RealFontWidth;
            FONT_PROB = fontInfo.PrintFont;

            fontInfo = AdjustmentFontSize.GetFontSize(StepHeigth / 3, '0');  // (test data)
            HEIGTH_CHAR_FONT = fontInfo.RealFontHeigth;
            WIDTH_CHAR_FONT = fontInfo.RealFontWidth;
            FONT_CHAR = fontInfo.PrintFont;


            //SEPARATE_WIDTH = 30;
            SEPARATE_WIDTH = GetMaxSeparateWidth(width);
            visualNode.StepHeigth = GetStepHeight(heigth);
            visualNode.FigureSize = SEPARATE_WIDTH / 2;

            base.DrawTree(width, heigth, graph);
        }


        public void SetDrawMode(EDrawTreeMode mode)
        {
            this.mode = mode;
        }


        #region Additional drawing methods

        //private void DrawProbability(float x1, float y, float x2, double probability)
        //{
        //    float drawX = (x2 - x1) / 2 - WIDTH_PROB_FONT / 2;
        //    //float drawX = (x2 - x1) - WIDTH_PROB_FONT;
        //    graph.DrawString(String.Format("{0:0.00}", probability), FONT_PROB, DEFAULT_BRUSH, x1 + drawX, y + visualNode.FigureSize/2);
        //}

        private void DrawProbabilityL(float x, float y, double probability)
        {
            graph.DrawString(String.Format("{0:0.00}", probability), FONT_PROB, DEFAULT_BRUSH, x, y + visualNode.FigureSize / 2);
        }

        private void DrawProbabilityR(float x, float y, double probability)
        {
            graph.DrawString(String.Format("{0:0.00}", probability), FONT_PROB, DEFAULT_BRUSH, x - WIDTH_PROB_FONT, y + visualNode.FigureSize / 2);
        }


        private void DrawSymbol(float x, float y, char symbol)
        {
            graph.DrawString("" + symbol, FONT_CHAR, DEFAULT_BRUSH, x - WIDTH_CHAR_FONT / 2, y - HEIGTH_CHAR_FONT / 2);
        }

        private void DrawSymbolUnder(float x, float y, char symbol)
        {
            DrawSymbol(x, y + 1.2f * visualNode.FigureSize, symbol);
            //!!!
        }

        #endregion




        // Рисует узел
        private void DrawNode(float x, float y)
        {
            float offset = visualNode.FigureSize / 2;
            graph.DrawEllipse(defaultColor, x - offset, y - offset, visualNode.FigureSize, visualNode.FigureSize);
        }


        protected override void DrawLines(float x, float y, float xLeft, float xRight)
        {
            Offset offset = visualNode.GetOffset(x, y, xLeft, xRight);
            float newY = y + StepHeigth;

            // Линяя вправо
            graph.DrawLine(defaultColor, x + offset.Right.X, y + offset.Right.Y, xRight - offset.Right.X, newY - offset.Right.Y);
            // Линяя влево
            graph.DrawLine(defaultColor, x - offset.Left.X, y + offset.Left.Y, xLeft + offset.Left.X, newY - offset.Left.Y);
        }




        protected override void DrawOnTheWay(float x, float y, float xLeft, float xRight,
            char binSymbol, double nProb, double nLeftProb, double nRightProb)
        {
            DrawNode(x, y);
            DrawLines(x, y, xLeft, xRight);

            switch (mode)
            {
                // Отображение 0 и 1 (внутри узлов)
                case EDrawTreeMode.BinaryNodes:
                case EDrawTreeMode.BinaryNodesWithCharUnder:
                    DrawSymbol(x, y, binSymbol);
                    break;

                // Отображение вероятностей (сверху по середине ветки)
                case EDrawTreeMode.Probability: 
                case EDrawTreeMode.ProbabilityWithCharNodes: 
                case EDrawTreeMode.ProbabilityWithCharUnder:
                    //DrawProbability(xLeft, y, x, nLeftProb);
                    //DrawProbability(x, y, xRight, nRightProb);

                    DrawProbabilityL(xLeft, y, nLeftProb);
                    DrawProbabilityL(xRight, y, nRightProb);
                    break;

                // Отображение 0 и 1 и вероятностей
                case EDrawTreeMode.ProbabilityWithBinaryNodes:
                case EDrawTreeMode.ProbabilityWithBinaryNodesWithCharUnder:

                    //DrawProbability(xLeft, y, x, nLeftProb);
                    //DrawProbability(x, y, xRight, nRightProb);

                    DrawProbabilityL(xLeft, y, nLeftProb);
                    DrawProbabilityL(xRight, y, nRightProb);
                    DrawSymbol(x, y, binSymbol);
                    break;
            }
        }




        protected override void DrawInTheEnd(float x, float y, char binSymbol, char nSymbol, double probability)
        {
            DrawNode(x, y);

            switch (mode)
            {
                // Отображение символов в последених узлах
                case EDrawTreeMode.CharNodes: 
                case EDrawTreeMode.ProbabilityWithCharNodes:
                    
                    DrawSymbol(x, y, nSymbol);
                    break;

                // Отображение 0 и 1 (внутри узлов)
                case EDrawTreeMode.BinaryNodes:
                case EDrawTreeMode.ProbabilityWithBinaryNodes:

                    DrawSymbol(x, y, binSymbol);
                    break;

                // Отображение символов под последними узлами
                case EDrawTreeMode.CharUnder:
                case EDrawTreeMode.ProbabilityWithCharUnder:

                    DrawSymbolUnder(x, y, nSymbol);
                    break;

                // Отображение 0 и 1 и символов под последними узлами
                case EDrawTreeMode.BinaryNodesWithCharUnder:
                case EDrawTreeMode.ProbabilityWithBinaryNodesWithCharUnder:
                    DrawSymbol(x, y, binSymbol);
                    DrawSymbolUnder(x, y, nSymbol);
                    break;                
            }
        }

    }
}
