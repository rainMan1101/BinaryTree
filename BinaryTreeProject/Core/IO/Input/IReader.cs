using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeProject.Core.IO.Input
{
    /*           Reader - должен возвращать список символов и вероятностей            */

    public interface IReader
    {

        //  Список символов и вероятностей  
        Dictionary<char, double> ProbabilityDictionary { get; }

    }
}
