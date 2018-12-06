using System;
using System.Collections.Generic;
using System.Linq;
using BinaryTreeProject.Core.Trees.BinaryTrees;


namespace BinaryTreeProject.Core.Utils
{
    public class Converter
    {

        //private const int defaultStepLength = 3;

        private string[,] lastColumnContent = null;

        private int countRows;

        private int countColumns;
        

        public int CountRows { get { return countRows; } }

        public int CountColumns { get { return countColumns; } }

        public string[,] LastColumnContent { get { return lastColumnContent; } }


        public Converter(BinaryTree binTree)
        {
            if (binTree is ShannonTree)
            {
                ShannonTree shTree= (ShannonTree)binTree;
                lastColumnContent = GetLastColumn(shTree.GetSpliterSteps(), shTree.GetBinaryCodes().Count);
            }
            else if (binTree is HaffmanTree)
            {
                HaffmanTree haffTree = (HaffmanTree)binTree;
                lastColumnContent = GetLastColumn(haffTree.ListSumsProbabilities);
            }
        }



        private string[,] GetLastColumn(List<double[]> listSumsProbabilities)
        {
            string[,] stringArr = null;
            countRows = 0;
            countColumns = listSumsProbabilities.Count;

            if (listSumsProbabilities.Count != 0)
            {
                int alphaBetCount = countRows = listSumsProbabilities[0].Length;
                stringArr = new string[alphaBetCount, listSumsProbabilities.Count];


                for (int i = 0; i < listSumsProbabilities.Count; i++)
                {
                    double[] sumsProbabilities = listSumsProbabilities[i];

                    int j = 0;
                    for (;  j < sumsProbabilities.Length; j++)
                        stringArr[j, i] = String.Format(" {0:0.00000000}  ", sumsProbabilities[j]);
                    for (; j < alphaBetCount; j++)
                        stringArr[j, i] = "          ";
                }
            }

            return stringArr;
        }



        private string[,] GetLastColumn(List<KeyValuePair<int, int>> list, int alphabetLength)
        {
            countRows = alphabetLength;
            countColumns = list.GroupBy(elements => elements.Key).OrderBy(elements => elements.Key).ToArray().Count();

            const int defaultStepLength = 3;
            string[,] steps = new string[countRows, countColumns];
            string stringTemplate = " ", stringTemplateStep = "_";

            for (int i = 1; i < defaultStepLength; i++)
            {
                stringTemplate += stringTemplate[0];
                stringTemplateStep += stringTemplateStep[0];
            }


            for (int i = 0; i < countColumns; i++)
                for (int j = 0; j < countRows; j++)
                    steps[j, i] = stringTemplate;


            for (int i = 1; i <= countColumns; i++)
            {
                KeyValuePair<int, int>[] arr = 
                    list.Where(elements => elements.Key == i).ToArray();

                int count = arr.Count();

                for (int j = 0; j < count; j++)
                    steps[arr[j].Value, i - 1] = stringTemplateStep;
            }

            return steps;
        }
        
    }
}
