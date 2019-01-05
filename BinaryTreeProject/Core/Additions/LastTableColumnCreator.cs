using System;
using System.Collections.Generic;
using System.Linq;
using BinaryTreeProject.Core.Trees.BinaryTrees;


namespace BinaryTreeProject.Core.Utils
{
    /*      Класс предназначенный для генерирования форматированного (дополненого пробелами) набора строк, содержащего 
     *  информацию для вывода в последней колонке таблицы, соответстующую конкретному алгоритму.                          
     */
    public class LastTableColumnCreator
    {
        
        //  Двумерный массив строк, содержащий определенные значения в нужных строках или пробелы вместо них
        private string[,] lastColumnContent = null;


        //  Количество строк в двумерном массиве
        private int countRows;


        //  Количество столбцов в двумерном массиве
        private int countColumns;
        

        public int CountRows { get { return countRows; } }

        public int CountColumns { get { return countColumns; } }

        public string[,] LastColumnContent { get { return lastColumnContent; } }


        //  Создание объекта и вызов соответствующего для него метода
        public LastTableColumnCreator(BinaryTree binTree)
        {
            if (binTree is ShannonTree)
            {
                //  Для дерева Шеннона - получение разделителей
                ShannonTree shTree= (ShannonTree)binTree;
                lastColumnContent = GetLastColumn(shTree.GetSpliterSteps(), shTree.GetBinaryCodes().Count);
            }
            else if (binTree is HaffmanTree)
            {
                //  Для дерева Хаффмана - получение набора частичных сумм
                HaffmanTree haffTree = (HaffmanTree)binTree;
                lastColumnContent = GetLastColumn(haffTree.ListSumsProbabilities);
            }
        }


        //  Получение столбцов с вероятностями дополненные пробелами
        private string[,] GetLastColumn(List<double[]> listSumsProbabilities)
        {
            string[,] stringArr = null;
            countRows = 0;
            countColumns = listSumsProbabilities.Count;

            if (listSumsProbabilities.Count != 0)
            {
                int alphaBetCount = countRows = listSumsProbabilities[0].Length;
                stringArr = new string[alphaBetCount, listSumsProbabilities.Count];

                //  Получение частичных сумм вероятностей и дополнение пробелами снизу
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


        //  Получение столбцов с разделителями, дополненые пробелами
        private string[,] GetLastColumn(List<KeyValuePair<int, int>> list, int alphabetLength)
        {
            string[,] stringArr = null;
            countRows = alphabetLength;
            countColumns = list.GroupBy(elements => elements.Key).OrderBy(elements => elements.Key).ToArray().Count();

            if (countRows != 0 && countColumns != 0)
            {
                const int defaultStepLength = 3;
                stringArr = new string[countRows, countColumns];
                string stringTemplate = " ", stringTemplateStep = "_";

                //  Установление размера ширины колонок и разделителей
                for (int i = 1; i < defaultStepLength; i++)
                {
                    stringTemplate += stringTemplate[0];
                    stringTemplateStep += stringTemplateStep[0];
                }

                //  Все заполняется пробелами
                for (int i = 0; i < countColumns; i++)
                    for (int j = 0; j < countRows; j++)
                        stringArr[j, i] = stringTemplate;

                //  Получение разделителей для каждой колонки
                for (int i = 1; i <= countColumns; i++)
                {
                    KeyValuePair<int, int>[] arr =
                        list.Where(elements => elements.Key == i).ToArray();

                    int count = arr.Count();

                    for (int j = 0; j < count; j++)
                        stringArr[arr[j].Value, i - 1] = stringTemplateStep;
                }
            }

            return stringArr;
        }
        
    }
}
