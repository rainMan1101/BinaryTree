using System.Collections.Generic;
using BinaryTreeProject.App.Enums;

namespace BinaryTreeProject.Core.Utils
{
    public interface IWritter
    {
        void PrintResults(char[] chars, double[] probabilities, Dictionary<char, string> codes, string[,] steps, ETreeType treeType);
        void PrintDetailsDecoding(List<KeyValuePair<string, char>> list);
    }
}
