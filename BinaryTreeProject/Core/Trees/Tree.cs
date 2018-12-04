using System;
using System.Collections.Generic;
using System.Linq;


namespace BinaryTreeProject.Core.Trees
{
    /* BASE CLASS */
    public abstract class Tree
    {
        // Исхдоная вероятность
        protected const double FULL_PROBABILITY = 1.0;

        // Корневой узел дерева
        protected Node rootNode;

        // Коллекции, используемые методами в процессе работы
        protected double[] probabilities;                     // Массив вероятностей

        protected char[] values;                              // Массив символов      

        // For default
        protected bool agreement = false;

        public bool Agreement { get { return agreement; } set { agreement = value; } }

        public void SetTree(Tree tree)
        {
            if (tree != null) {
                this.probabilities = new double[tree.probabilities.Length];
                this.values = new char[tree.values.Length];

                Array.Copy(tree.probabilities, this.probabilities, tree.probabilities.Length);
                Array.Copy(tree.values, this.values, tree.values.Length);

                this.agreement = tree.agreement;

                //// TODO: rewrite this crap
                //this.rootNode = new Node()
                //{
                //    LeftChildNode = tree.rootNode.LeftChildNode, //Старая ссылка остается!!!
                //    RightChildNode = tree.rootNode.RightChildNode,
                //    Value = tree.rootNode.Value,
                //    Probability = tree.rootNode.Probability
                //};
                this.rootNode = tree.rootNode;
            }
        }

        public Tree(Dictionary<char, double> probabilitiesDict)
        {
            values = probabilitiesDict.Keys.ToArray();
            probabilities = probabilitiesDict.Values.ToArray();

            if (values.Length == 0 || probabilities.Length == 0)
                throw new Exception("Задан пустой список символов и вероятностей.");

            // Сортирую массив вероятностей (probabilities), в порядке их убывания.
            // Так же элементы массива символов (values) изменяют свое положение,
            // на соответствующее положение вероятностей, сопоставимым им по индексам.
            Array.Sort(probabilities, values, new Utils.CustomComparer());
        }

        public Tree(Tree tree)
        {
            SetTree(tree);
        }
    }
}