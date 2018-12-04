using System.Drawing;


namespace BinaryTreeProject.Core.Trees.VisualTrees
{
    public class VisualTree : Tree, IVisualTree
    {
        protected Graphics graph;

        private float COEFF_WIDTH;

        private float STEP_HEIGHT;

        // For derived classes
        protected float CoeffWidth { get { return COEFF_WIDTH; } }

        protected float StepHeigth { get { return STEP_HEIGHT; } }


        private const float MARGIN_LEFT_RIGHT = 30;

        private const float MARGIN_TOP_BOTTOM = 30;

        //!!!
        protected float SEPARATE_WIDTH = 0;

        //private const float CIRCLE_SIZE = SEPARATE_WIDTH / 2;

        protected Pen defaultColor;

        //private Tree tree;

        


        public VisualTree(Tree tree) : base(tree)
        {
            //this.tree = tree;
            defaultColor = Pens.LimeGreen;
        }


        //public override void Build()
        //{
        //    tree.Build();
        //}

        #region Virtual methods 

        /*           Функции для определения размера, используемые DrawTree          */

        protected virtual float GetCoeffWidth(float width, int countWidth)
        {
            width -= 2 * MARGIN_LEFT_RIGHT;
            width -= (countWidth - 1) * SEPARATE_WIDTH;
            width /= countWidth;
            return width;
        }


        protected virtual float GetStepHeight(float heigth, int countHeigth)
        {
            heigth -= 2 * MARGIN_TOP_BOTTOM;
            heigth /= countHeigth;
            return heigth;
        }


        protected virtual float GetStartLeftWidth(int countLeft)
        {
            float startLeftWidth = countLeft * COEFF_WIDTH;
            startLeftWidth += MARGIN_LEFT_RIGHT;
            startLeftWidth += (countLeft - 1) * SEPARATE_WIDTH;
            return startLeftWidth;
        }



        /*                Графические функции, используемые GraphBuilder             */


        // Изменяемый метод. Вызывает остальные функции для рисования объектов, 
        //котрые должны быть выведены в процессе рисования структуры дерева
        protected virtual void DrawOnTheWay(float x, float y, float xLeft, float xRight, 
            char binSymbol, double nProb, double nLeftProb, double nRightProb)
        {
            DrawLines(x, y, xLeft, xRight);
        }


        // default: Простое рисование линий (без отображения узлов и прочего)
        protected virtual void DrawLines(float x, float y, float xLeft, float xRight)
        {
            // Линяя вправо
            graph.DrawLine(defaultColor, x, y, xRight, y + STEP_HEIGHT);
            // Линяя влево
            graph.DrawLine(defaultColor, x, y, xLeft, y + STEP_HEIGHT);
        }


        // Изменяемый метод. Вызывает остальные функции для рисования объектов, 
        //котрые должны быть выведены на листьях(концах) дерева
        protected virtual void DrawInTheEnd(float x, float y, char binSymbol, char nSymbol, double probability) { }

        #endregion



        //!!!
        protected float GetStepHeight(int heigth)
        {
            return GetStepHeight(heigth, GetHeight(rootNode));
        }


        protected float GetMaxSeparateWidth(float width)
        {
            width -= 2 * MARGIN_LEFT_RIGHT;
            int countWidth = GetWidth(rootNode);

            // (width - (countWidth - 1) * n) / countWidth = 0.1;
            // width - (countWidth - 1) * n = 0.1 * countWidth
            // (countWidth - 1) * n  = width - 0.1 * countWidth
            // n = (width - 0.1 * countWidth) / (countWidth - 1)

            return (width - 0.1f * countWidth) / (countWidth - 1);
        }


        // Определяет размеры и вызывает рекурсивную функцию рисования дерева
        public virtual void DrawTree(int width, int heigth, Graphics graph)
        {
            this.graph = graph;

            int countLeft = GetWidth(rootNode.LeftChildNode);
            int countRight = GetWidth(rootNode.RightChildNode);

            int countWidth = countLeft + countRight;
            int countHeight = GetHeight(rootNode);

            COEFF_WIDTH = GetCoeffWidth(width, countWidth);
            STEP_HEIGHT = GetStepHeight(heigth, countHeight);

            float startLeftWidth = GetStartLeftWidth(countLeft);

            GraphBuilder(startLeftWidth, MARGIN_TOP_BOTTOM, rootNode, ' ');
        }

        //  Построитель - рисует структуру дерева (рекурсивно), при помощи вспомогательных функций
        private void GraphBuilder(float leftParentCoord, float topParentCoord, Node node, char binSymbol)
        {
            if (node.LeftChildNode == null && node.RightChildNode == null)
            {
                //!!! Сhangeable method
                DrawInTheEnd(leftParentCoord, topParentCoord, binSymbol, node.Value, node.Probability); 
                return;
            }
            else
            {
                float offsetToTheLeft;
                if (node.LeftChildNode.RightChildNode != null)
                {
                    int leftWidthRigth = GetWidth(node.LeftChildNode.RightChildNode);
                    offsetToTheLeft = leftWidthRigth * COEFF_WIDTH + ((leftWidthRigth) * SEPARATE_WIDTH);
                }
                else
                    offsetToTheLeft = GetWidth(node.LeftChildNode) * COEFF_WIDTH + SEPARATE_WIDTH / 2;

                float LeftChildNodeCoord = leftParentCoord - offsetToTheLeft;


                float offsetToTheRight;
                if (node.RightChildNode.LeftChildNode != null)
                {
                    int rightWidthLeft = GetWidth(node.RightChildNode.LeftChildNode);
                    offsetToTheRight = rightWidthLeft * COEFF_WIDTH + ((rightWidthLeft) * SEPARATE_WIDTH);
                }
                else
                    offsetToTheRight = GetWidth(node.RightChildNode) * COEFF_WIDTH + SEPARATE_WIDTH / 2;

                float RightChildNodeCoord = leftParentCoord + offsetToTheRight;

                //!!! Сhangeable method
                DrawOnTheWay(leftParentCoord, topParentCoord, LeftChildNodeCoord, RightChildNodeCoord,
                    binSymbol, node.Probability, node.LeftChildNode.Probability, node.RightChildNode.Probability);

                if (agreement) //#if TRUE_VARIANT
                {
                    GraphBuilder(RightChildNodeCoord, topParentCoord + STEP_HEIGHT, node.RightChildNode, '1');
                    GraphBuilder(LeftChildNodeCoord, topParentCoord + STEP_HEIGHT, node.LeftChildNode, '0');
                }
                else //#else
                {
                    GraphBuilder(RightChildNodeCoord, topParentCoord + STEP_HEIGHT, node.RightChildNode, '0');
                    GraphBuilder(LeftChildNodeCoord, topParentCoord + STEP_HEIGHT, node.LeftChildNode, '1');
                } //#endif


            }

        }

        // Определяет ширину, занимаемую деревом(поддеревом) начиная
        // от заданного узла  в количестве нижележайщих веток
        private int GetWidth(Node node)
        {
            if (node.LeftChildNode == null && node.RightChildNode == null)
                return 1;
            else
            {
                int width = 0;
                width += GetWidth(node.LeftChildNode);
                width += GetWidth(node.RightChildNode);
                return width;
            }
        }

        // Определяет высоту, занимаемую деревом(поддеревом) начиная
        // от заданного узла  в количестве веток
        private int GetHeight(Node node)
        {
            if (node.LeftChildNode == null && node.RightChildNode == null)
                return 0;
            else
            {
                int leftHeight = GetHeight(node.LeftChildNode) + 1;
                int rightHeight = GetHeight(node.RightChildNode) + 1;
                return (leftHeight >= rightHeight) ? leftHeight : rightHeight;
            }
        }
    }
}
