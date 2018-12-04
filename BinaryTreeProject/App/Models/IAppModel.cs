using System.Drawing;
using BinaryTreeProject.App.Enums;
using BinaryTreeProject.Core.Trees.VisualTrees.Additions;


namespace BinaryTreeProject.App.Models
{
    public interface IAppModel
    {
        void DrawTree(Graphics graph, int heigth, int width, EDrawNodeMode drawNodeMode, EDrawTreeMode drawTreeMode);
        void ReplaceTree(ETreeType treeType);
        void ReplaceAgreement(bool agreement);

        void PrintResult();
        double GetValueInfo();

        string Encode(string originalString);
        string Decode(string binaryString, string outputString);
    }
}
