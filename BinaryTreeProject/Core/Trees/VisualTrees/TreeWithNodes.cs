using System;
using System.Drawing;
using BinaryTreeProject.Core.Additions;
using BinaryTreeProject.Core.Trees.VisualNodes;
using BinaryTreeProject.Core.Trees.VisualTrees.Additions;


namespace BinaryTreeProject.Core.Trees.VisualTrees
{  
    /*                                  Дерево с узлами                    
     *                                  
     *    Данный класс позволяет, помимо рисования структуры дерева визуализировать 
     *    его узлы. Визуализация самих узлов оссуществляется классами визуальных 
     *    узлов. Данный класс рисует ветки дерева с учетом размеров визуальных узлов.
     *    Также содержит специальные методы для визуализации кодов символов, 
     *    вероятностей и символов алфавита.
     */
    public class TreeWithNodes : VisualTree
    {
        //  Используемая фигура для визуализации узлов дерева
        private IVisualNode visualNode;


        //  Высота выводимой текстовой строки со значением вероятности
        private float HEIGTH_PROB_FONT;


        //  Ширина выводимой текстовой строки со значением вероятности
        private float WIDTH_PROB_FONT;


        //  Подогнанный размер шрифта, для вывода строки с вероятностью
        private Font FONT_PROB;


        //  Высота выводимого символа
        private float HEIGTH_CHAR_FONT;


        //  Ширина выводимого символа
        private float WIDTH_CHAR_FONT;


        //  Подогнанный размер шрифта, для вывода символа
        private Font FONT_CHAR;

        //  Используемая кисть для рисования линий и узлов
        private SolidBrush DEFAULT_BRUSH;


        //  Режим отображения заданный пользователем
        private EDrawTreeMode mode;


        /*                                      Конструктор                                     */

        //  Установка визуального узла, режима отображения и цвета кисти
        public TreeWithNodes(Tree tree, IVisualNode visualNode, EDrawTreeMode mode) : base(tree)
        {
            this.visualNode = visualNode;
            this.mode = mode;
            DEFAULT_BRUSH = new SolidBrush(Color.LimeGreen);
        }


        //  Главный метод рисования, вызваемый из приложения, который вызывает все остальные методы
        public override void DrawTree(int width, int heigth, Graphics graph)
        {
            FontInfo fontInfo;
            //  Узнать размер, занимаемый выводимой строкой с вероятностью и шрифт, 
            //  необходимый для такого размера. Высота строки должна составлять 1/3 
            //  от вертикального расстояния между центрами узлов
            fontInfo = AdjustmentFontSize.GetFontSizeProbability(StepHeigth / 3); 
            HEIGTH_PROB_FONT = fontInfo.RealFontHeigth;
            WIDTH_PROB_FONT = fontInfo.RealFontWidth;
            FONT_PROB = fontInfo.PrintFont;

            //  Узнать размер, занимаемый выводимым символом (символ алфавита или 0/1)
            fontInfo = AdjustmentFontSize.GetFontSizeChar(StepHeigth / 3); 
            HEIGTH_CHAR_FONT = fontInfo.RealFontHeigth;
            WIDTH_CHAR_FONT = fontInfo.RealFontWidth;
            FONT_CHAR = fontInfo.PrintFont;

            //  Устанавливаю максимально возможный размер дополнительных пробелов между узлами
            SEPARATE_WIDTH = GetMaxSeparateWidth(width);
            //  Устанавливаю расстояние между центрами узлов по горизонтали
            visualNode.StepHeigth = GetStepHeight(heigth);
            //  Устанавливаю размер фигуры равным половине дополнительного расстояния между центрами,
            //  Таким образом, расстояние между двумя центрами будет равно 2 кругам. Расстояние в 1 круг
            //  уберется половинами кругов, а расстояние в 1 круг останется
            //  Под расстоянием в 1 круг понимается расстояние между двумя конечными кругами 
            visualNode.FigureSize = SEPARATE_WIDTH / 2;

            base.DrawTree(width, heigth, graph);
        }


        //  Установка режима рисования
        public void SetDrawMode(EDrawTreeMode mode)
        {
            this.mode = mode;
        }


        #region Additional drawing methods

        //  Вывод вероятности
        private void DrawProbability(float x, float y, double probability)
        {
            //  Изначально имеем координаты центра окружности(дочерней) над которой необходимо вывести вероятность.
            //  Изначально верхний левый угол строки вероятности выводится в центр окружноти.
            //  X - оставляем такой же. Y - уменьшаем(завышаем) на величину радиуса круга + на высоту самих выводимых букв.
            graph.DrawString(String.Format("{0:0.00}", probability), FONT_PROB, DEFAULT_BRUSH, x, y - visualNode.FigureSize / 2 - HEIGTH_PROB_FONT);
        }

        
        //  Вывод символа внутри узла
        private void DrawSymbol(float x, float y, char symbol)
        {
            // Изначально левый верхний угол символа в центре узла - сдвигаем в цент узла центр символа
            graph.DrawString("" + symbol, FONT_CHAR, DEFAULT_BRUSH, x - WIDTH_CHAR_FONT / 2, y - HEIGTH_CHAR_FONT / 2);
        }


        //  Вывод символа под узлом 
        private void DrawSymbolUnder(float x, float y, char symbol)
        {
            DrawSymbol(x, y + 1.1f * visualNode.FigureSize, symbol);
        }

        #endregion
        

        //// Рисует узел
        //private void DrawNode(float x, float y)
        //{
        //    float offset = visualNode.FigureSize / 2;
        //    //graph.DrawEllipse(defaultPen, x - offset, y - offset, visualNode.FigureSize, visualNode.FigureSize);
        //    visualNode.DrawNode()
        //}


        //  Переопределяю виртуальный метод, для того, чтобы линии выводились не от центра к центру, 
        //  а от края одного круга краю другого
        protected override void DrawLines(float x, float y, float xLeft, float xRight)
        {
            Offset offset = visualNode.GetOffset(x, y, xLeft, xRight);
            float newY = y + StepHeigth;

            //  Смещение координат вывода начала линии вниз в родительском узле, 
            //  равно по модулю и отрицательно познаку(вверх) для дочернего узла
            // Линяя вправо
            graph.DrawLine(defaultPen, x + offset.Right.X, y + offset.Right.Y, xRight - offset.Right.X, newY - offset.Right.Y);
            // Линяя влево
            graph.DrawLine(defaultPen, x - offset.Left.X, y + offset.Left.Y, xLeft + offset.Left.X, newY - offset.Left.Y);
        }

        


        /*              Виртуальные методы, вызываемые в родителском классе в построителе дерева            */


        //  Рисование в координатах центров промежуточных узлов дерева
        protected override void DrawOnTheWay(float x, float y, float xLeft, float xRight,
            char binSymbol, double nProb, double nLeftProb, double nRightProb)
        {
            //  Вывод промежуточного узла дерева
            visualNode.DrawNode(graph, x, y, defaultPen);
            //  Вывод линий, соединяющих два узла
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

                    DrawProbability(xLeft, y + StepHeigth, nLeftProb);
                    DrawProbability(xRight, y + StepHeigth, nRightProb);
                    break;

                // Отображение 0 и 1 и вероятностей
                case EDrawTreeMode.ProbabilityWithBinaryNodes:
                case EDrawTreeMode.ProbabilityWithBinaryNodesWithCharUnder:

                    DrawProbability(xLeft, y + StepHeigth, nLeftProb);
                    DrawProbability(xRight, y + StepHeigth, nRightProb);
                    DrawSymbol(x, y, binSymbol);
                    break;
            }
        }



        //  Рисование в координатах центров листьев дерева
        protected override void DrawInTheEnd(float x, float y, char binSymbol, char nSymbol, double probability)
        {
            //  Вывод конечного узла дерева
            visualNode.DrawNode(graph, x, y, defaultPen);

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
