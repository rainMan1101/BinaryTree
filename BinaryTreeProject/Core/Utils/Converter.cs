using System;
using System.Collections.Generic;
using BinaryTreeProject.Core.Trees.BinaryTrees;


namespace BinaryTreeProject.Core.Utils
{
    public class Converter
    {

        private const int defaultStepLength = 3;

        private string[,] lastColumnContent = null;

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

            if (listSumsProbabilities.Count != 0)
            {
                int alphaBetCount = listSumsProbabilities[0].Length;
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



        private string[,] GetLastColumn(int[] stepsIndexes, int alphabetLength)
        {
            string[,] steps = new string[alphabetLength, stepsIndexes.Length];
            string stringTemplate = " ", stringTemplateStep = "_";

            for (int i = 1; i < defaultStepLength; i++)
            {
                stringTemplate += stringTemplate[0];
                stringTemplateStep += stringTemplateStep[0];
            }


            // столбцы
            for (int i = 0; i < stepsIndexes.Length; i++)
            {
                for (int j = 0; j < alphabetLength; j++)
                    steps[j, i] = stringTemplate;
                steps[stepsIndexes[i], i] = stringTemplateStep;
            }

            return steps;
        }
    }
}
