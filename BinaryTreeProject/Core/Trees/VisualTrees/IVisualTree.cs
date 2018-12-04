using System.Drawing;


namespace BinaryTreeProject.Core.Trees.VisualTrees
{
    public interface IVisualTree
    {
        void DrawTree(int width, int heigth, Graphics graph);
    }
}