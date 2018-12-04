using System;


namespace BinaryTreeProject.Core.Trees.VisualNodes
{

    public struct OffsetOneSide
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public struct Offset
    {
        public OffsetOneSide Left { get; set; }
        public OffsetOneSide Right { get; set; }
    }



    public class CircleNode : IVisualNode
    {
        private float stepHeigth;
        private float circleSize;

        public float FigureSize { get { return circleSize; }  set { circleSize = value; } }
        public float StepHeigth { get { return stepHeigth; } set { stepHeigth = value; } }

        //public CircleNode(float stepHeigth, float circleSize)
        public CircleNode(float stepHeigth, float circleSize)
        {
            //this.stepHeigth = stepHeigth;
            this.circleSize = circleSize;
        }

        public Offset GetOffset(float x, float y, float xLeftChild, float xRightChild)
        {
            Offset offset = new Offset();
            offset.Left = GetSideOffset(x, y, xLeftChild);
            offset.Right = GetSideOffset(x, y, xRightChild);
            return offset;
        }

        public OffsetOneSide GetSideOffset(float x1, float y, float x2)
        {
            float k = stepHeigth / (x1 - x2);
            float r = circleSize / 2;

            OffsetOneSide offset = new OffsetOneSide();
            offset.X = (float)Math.Sqrt((double)(r * r) / (k * k + 1));
            offset.Y = offset.X * Math.Abs(k);

            return offset;
        }
    }
}
