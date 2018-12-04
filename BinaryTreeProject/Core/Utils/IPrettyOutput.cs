using System.Collections.Generic;


namespace BinaryTreeProject.Core.Utils
{
    public interface IPrettyOutput
    {
        void PrintResults(char[] chars, Dictionary<char, double> probabilities, Dictionary<char, string> codes, int[] stepsIndexes);
    }
}
