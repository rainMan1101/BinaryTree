using System.Drawing;

// Боковые отступы должны быть больше половины фигуры

namespace BinaryTreeProject.Core.Trees.VisualTrees
{
    /*                                  Визуальное дерево                                
     *                              
     *      Класс содержащий базовые методы для рисования стуктуры дерева. 
     *  DrawTree - метод с которого начинается все рисование, вызывается из основной программы.
     *  GraphBuilder - построитель. Оссуществляет рисование структуры дерева при помощи вызовов 
     *  виртуальных методов. Построитель определяет координаты центров узлов, функции рисуют
     *  отрезки, соединяющие данные центры. Визуальное предстоавление узлв в данном классе не
     *  предусмотренно.
     *  Виртуальне методы, вызываемые, GraphBuilder:
     *   DrawOnTheWay   -   для рисования относительно координат промежуточных узлов.
     *   DrawInTheEnd   -   для рисования относительно координат конечных узлов (листьев дерева).
     *   DrawLines      -   метод используемый по усолчанию методом DrawOnTheWay для рисования
     *                      линий от центра одногого узла к другому.
     */
    public class VisualTree : Tree, IVisualTree
    {
        //  Область для рисования
        protected Graphics graph;


        //  Расстояние по горизонтали между центрами крайнего дочернего узла и его 
        //  родительского узла (= один шаг по горизонтали)
        private float COEFF_WIDTH;


        //  Один шаг по вертикали
        private float STEP_HEIGHT;


        // For derived classes
        protected float CoeffWidth { get { return COEFF_WIDTH; } }

        protected float StepHeigth { get { return STEP_HEIGHT; } }


        //  Отступы слева и справа
        private const float MARGIN_LEFT_RIGHT = 30;


        //  Отступы сверху и снизу
        private const float MARGIN_TOP_BOTTOM = 30;


        //  Дополнительный разделитель между узлами
        protected float SEPARATE_WIDTH = 0;


        //  Ручка для рисования линий
        protected Pen defaultPen;



        /*                                      Конструктор                                     */

        public VisualTree(Tree tree) : base(tree)
        {
            defaultPen = Pens.LimeGreen;
        }



        #region Virtual methods 

        /*                Графические функции, используемые GraphBuilder             */


        // Изменяемый метод. Вызывает остальные функции для рисования объектов, 
        //котрые должны быть выведены в процессе рисования структуры дерева
        protected virtual void DrawOnTheWay(float x, float y, float xLeft, float xRight, 
            char binSymbol, double nProb, double nLeftProb, double nRightProb)
        {
            DrawLines(x, y, xLeft, xRight);
        }


        // Изменяемый метод. Вызывает остальные функции для рисования объектов, 
        //котрые должны быть выведены на листьях(концах) дерева
        protected virtual void DrawInTheEnd(float x, float y, char binSymbol, char nSymbol, double probability) { }


        // default: Простое рисование линий (без отображения узлов и прочего)
        protected virtual void DrawLines(float x, float y, float xLeft, float xRight)
        {
            // Линяя вправо
            graph.DrawLine(defaultPen, x, y, xRight, y + STEP_HEIGHT);
            // Линяя влево
            graph.DrawLine(defaultPen, x, y, xLeft, y + STEP_HEIGHT);
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
            //  В первый узел выводится пустота
            GraphBuilder(startLeftWidth, MARGIN_TOP_BOTTOM, rootNode, ' ');
        }

        #endregion


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
                //  Расстояние между центрами (в пикселях) от текущего узла до левого дочернего
                float offsetToTheLeft;
                //  Если слева узел не последний (не один)
                if (node.LeftChildNode.RightChildNode != null)
                {
                    //  Количество веток между текущим и левым дочерним узлом
                    int leftWidthRigth = GetWidth(node.LeftChildNode.RightChildNode);
                    offsetToTheLeft = leftWidthRigth * (COEFF_WIDTH +  SEPARATE_WIDTH);
                }
                else
                    //  Если слева узел 1, то GetWidth(node.LeftChildNode) = 1 * (один шаг по горизонтали) +
                    //  + половина дополнительного расстояния
                    offsetToTheLeft = GetWidth(node.LeftChildNode) * COEFF_WIDTH + SEPARATE_WIDTH / 2;

                //  Текущая координата -(минус - сдвиг влево) смещение = координата центра левого дочернего узла
                float LeftChildNodeCoord = leftParentCoord - offsetToTheLeft;


                //  Расстояние между центрами (в пикселях) от текущего узла до правого дочернего
                float offsetToTheRight;
                //  Если справа узел не последний (не один)
                if (node.RightChildNode.LeftChildNode != null)
                {
                    //  Количество веток между текущим и правым дочерним узлом
                    int rightWidthLeft = GetWidth(node.RightChildNode.LeftChildNode);
                    //  Расстояние между центрами (в пикселях) от текущего узла до правого дочернего
                    offsetToTheRight = rightWidthLeft * (COEFF_WIDTH + SEPARATE_WIDTH);
                }
                else
                    offsetToTheRight = GetWidth(node.RightChildNode) * COEFF_WIDTH + SEPARATE_WIDTH / 2;

                //  Координата центра левого дочернго узла
                float RightChildNodeCoord = leftParentCoord + offsetToTheRight;

                //!!! Сhangeable method
                DrawOnTheWay(leftParentCoord, topParentCoord, LeftChildNodeCoord, RightChildNodeCoord,
                    binSymbol, node.Probability, node.LeftChildNode.Probability, node.RightChildNode.Probability);

                //  Рекурсивный вызов построителя для нижележащих узлов
                if (agreement) //   Направление
                {
                    GraphBuilder(RightChildNodeCoord, topParentCoord + STEP_HEIGHT, node.RightChildNode, '1');
                    GraphBuilder(LeftChildNodeCoord, topParentCoord + STEP_HEIGHT, node.LeftChildNode, '0');
                }
                else 
                {
                    GraphBuilder(RightChildNodeCoord, topParentCoord + STEP_HEIGHT, node.RightChildNode, '0');
                    GraphBuilder(LeftChildNodeCoord, topParentCoord + STEP_HEIGHT, node.LeftChildNode, '1');
                }


            }

        }


        //  Расстояние между центрами узлов по вертикали
        protected float GetStepHeight(int heigth)
        {
            return GetStepHeight(heigth, GetHeight(rootNode));
        }


        // Максимально возможное дополнительное расстоение между узлами
        protected float GetMaxSeparateWidth(float width)
        {
            width -= 2 * MARGIN_LEFT_RIGHT;
            int countWidth = GetWidth(rootNode);

            /* (width - (countWidth - 1) * n) / countWidth = 0.1; 
             * - чтобы ширина линий, соединяющих центры была не меньше 0.1
             * 
             * width - (countWidth - 1) * n = 0.1 * countWidth
             * (countWidth - 1) * n  = width - 0.1 * countWidth
             * n = (width - 0.1 * countWidth) / (countWidth - 1) */

            return (width - 0.1f * countWidth) / (countWidth - 1);
        }



        /*           Функции для определения размера, используемые DrawTree          */


        //      Определение величины одного шага по горизонтали
        //  (= расстоянию между центрами листьев дерева)
        private float GetCoeffWidth(float width, int countWidth)
        {
            //  Вычитание отступов справа и слева
            width -= 2 * MARGIN_LEFT_RIGHT;
            //  Вычитание дополнительных пробелов между узлами
            width -= (countWidth - 1) * SEPARATE_WIDTH;
            //  Определение шага
            width /= countWidth;
            return width;
        }


        //      Определение величины одного шага по горизонтали
        //  (= расстоянию между центрами самого высокого и самого низкого узла)
        private float GetStepHeight(float heigth, int countHeigth)
        {
            heigth -= 2 * MARGIN_TOP_BOTTOM;
            heigth /= countHeigth;
            return heigth;
        }


        //  Координата центра корневого узла дерева по горизонтали 
        private float GetStartLeftWidth(int countLeft)
        {
            float startLeftWidth = countLeft * COEFF_WIDTH;
            startLeftWidth += MARGIN_LEFT_RIGHT;
            startLeftWidth += (countLeft - 1) * SEPARATE_WIDTH;
            return startLeftWidth;
        }


        // Определяет ширину, занимаемую деревом(поддеревом) начиная
        // от заданного узла  в количестве веток
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
        // от заданного узла  в количестве нижележайщих веток
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
