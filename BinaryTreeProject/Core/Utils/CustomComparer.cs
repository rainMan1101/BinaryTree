using System.Collections.Generic;


namespace BinaryTreeProject.Core.Utils
{
    public class CustomComparer: IComparer<double>
    {
        public int Compare(double x, double y)
        {
            if (x > y)
                return -1;
            if (x < y)
                return 1;
            else
                return 0;
        }
    }
}
