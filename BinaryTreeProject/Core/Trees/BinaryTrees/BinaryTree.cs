using System;
using System.Collections.Generic;
using System.Linq;
 

namespace BinaryTreeProject.Core.Trees.BinaryTrees
{
    /*
     *                          Бинарное дерево
     *                          
     *      Бинарное дерево представляет собой специальный класс, способный 
     *   формировать структуру дерева и кодовые комбинации из имеющихся массивов с 
     *   символами и вероятностями по определенному алгоритму. Для этого массив содержит 
     *   специальный метод Build. Даннный метод - абстрактный и должен быть переопределен 
     *   в каждом классе, реализующем тот или иной алгоритм построения дерева.
     *      Также данный класс содержит метод GetValueInfo, который что позволяет 
     *   оценить эффективность алгоритма сжатия.
     * 
     */
    public abstract class BinaryTree : Tree
    {
        
        // Исхдоная вероятность
        protected const double FULL_PROBABILITY = 1.0;


        /*                                  Конструкторы                                        */

        public BinaryTree(Dictionary<char, double> probabilitiesDict) : base(probabilitiesDict) { }

        public BinaryTree(Tree tree) : base(tree) { }


        // Коллекции для вывода 
        private Dictionary<char, string> binaryCodes;      // Символы (char) и их двоичные коды (string)

        public abstract void Build();


        // Вычисление среднего количества информации в битах (позволяет оценить эффективность алгоритма)
        public double GetValueInfo()
        {
            double valueInfo = 0;

            if (Values.Length != Probabilities.Length)
                throw new Exception("Непредвиденная ошибка! Размеры Values и Probabilities не соответствуют.");

            // Создание словаря, для поиска вероятности по символу
            Dictionary<char, double> probabilitiesDict = new Dictionary<char, double>();

            for (int i = 0; i < Values.Length; i++)
                probabilitiesDict.Add(Values[i], Probabilities[i]);

            //  Массив с двоичными кодами символов
            string[] binaryArr = GetBinaryCodes().Values.ToArray();
            //  Массив символов
            char[] charArr = GetBinaryCodes().Keys.ToArray();

            //  СУММА(Размер кода символа * его вероятность) = среднее количество информации
            for (int i = 0; i < binaryArr.Length; i++)
                valueInfo += binaryArr[i].Length * probabilitiesDict[charArr[i]];

            return valueInfo;
        }


        // Public методы, вызываются после Build()
        public Dictionary<char, string> GetBinaryCodes()
        {
            binaryCodes = new Dictionary<char, string>();
            FillBinaryCodesArray(rootNode);
            /* Передаю оригиал, так как этот словарь не используется в самом дереве,
               и не грозит нарушить его целостность. */
            return binaryCodes;
        }


        // Рекурсивный алгоритм сотавления кодового слова
        private void FillBinaryCodesArray(Node node, string binaryCode = "")
        {
            // LeftChildNode and RightChildNode always NULL together !!!
            if (node.LeftChildNode == null && node.RightChildNode == null)
                binaryCodes.Add(node.Value, binaryCode);
            else
            {
                if (agreement) //   направление
                {
                    FillBinaryCodesArray(node.RightChildNode, binaryCode + '1');    // право - 1
                    FillBinaryCodesArray(node.LeftChildNode, binaryCode + '0');     // лево - 0
                }
                else 
                {
                    FillBinaryCodesArray(node.RightChildNode, binaryCode + '0');    // право - 0
                    FillBinaryCodesArray(node.LeftChildNode, binaryCode + '1');     // лево - 1
                } 
            }
        }

   }
}