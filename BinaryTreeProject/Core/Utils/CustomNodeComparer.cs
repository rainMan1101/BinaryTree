using System.Collections.Generic;
using BinaryTreeProject.Core.Trees;


namespace BinaryTreeProject.Core.Utils
{
    public class CustomNodeComparer : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            if (x.Probability > y.Probability)
                return -1;
            if (x.Probability < y.Probability)
                return 1;
            else
                return 0;
        }
    }
}
