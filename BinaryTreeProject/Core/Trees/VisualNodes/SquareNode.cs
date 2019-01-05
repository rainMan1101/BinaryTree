using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryTreeProject.Core.Additions;

namespace BinaryTreeProject.Core.Trees.VisualNodes
{
    public class SquareNode : IVisualNode
    {
        //  Расстояние между центрами дочернего и родительского узла по вертикали
        private float stepHeigth;


        //  Размер стороны квадрата
        private float squareSize;


        public float FigureSize { get { return squareSize; } set { squareSize = value; } }
        public float StepHeigth { set { stepHeigth = value; } }

        public void DrawNode(Graphics graph, float x, float y, Pen pen)
        {
            graph.DrawRectangle(pen, x - squareSize / 2, y - squareSize / 2, squareSize, squareSize);
        }

        public Offset GetOffset(float x, float y, float xLeftChild, float xRightChild)
        {
            Offset offset = new Offset();
            offset.Left = GetSideOffset(x, y, xLeftChild);
            offset.Right = GetSideOffset(x, y, xRightChild);
            return offset;
        }


        //  Определяет насколко нужно сместить координаты по X и Y, относительно
        // центра фигуры, чтобы линяя выходила из края фигуры.
        public OffsetOneSide GetSideOffset(float x1, float y, float x2)
        {
            OffsetOneSide offset = new OffsetOneSide();
            //  Линии будут выходить из вершин квадрата
            offset.X = (float)Math.Sqrt(0.29 * squareSize * squareSize);
            offset.Y = offset.X;
            return offset;
        }
    }
}
