

namespace BinaryTreeProject.Core.Trees.VisualNodes
{
    public interface IVisualNode
    {
        float FigureSize { get;  set; }
        float StepHeigth { get;  set; }
        Offset GetOffset(float x, float y, float xLeftChild, float xRightChild);
        OffsetOneSide GetSideOffset(float x1, float y, float x2);
    }
}
