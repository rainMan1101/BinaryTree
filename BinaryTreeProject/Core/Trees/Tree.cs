using System;
using System.Collections.Generic;
using System.Linq;
using BinaryTreeProject.Core.Utils;


namespace BinaryTreeProject.Core.Trees
{
    /* BASE CLASS */
    public abstract class Tree
    {
        // Исхдоная вероятность
        protected const double FULL_PROBABILITY = 1.0;

        // Корневой узел дерева
        protected Node rootNode;

        // Коллекции, используемые методами класса Tree в процессе работы.
        // Каждому 1 элементу values соответствует 1 элемент probabilities и наоборот.
        // Эти массивы являются отсортированными в порядке убывания значений probabilities
        // и не доступны для изменения (доступ только через объект типа ReadOnlyArray) 
        // в дочерних классах.        
        private double[] probabilities;                     // Массив вероятностей

        private char[] values;                              // Массив символов      


        #region Read only Probabilities and Values arrays

        protected class ReadOnlyArray<T>
        {
            private T[] _array = null;

            public ReadOnlyArray(ReadOnlyArray<T> rOArray) { }

            public ReadOnlyArray(T[] array) {
                _array = array;
            }

            public int Length { get { return _array.Length; } }

            public T this[int index]
            {
                get { return _array[index]; }
            }
        }

        protected ReadOnlyArray<double> Probabilities;

        protected ReadOnlyArray<char> Values;

        #endregion


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

                // Init read only arrays
                Probabilities = new ReadOnlyArray<double>(probabilities);
                Values = new ReadOnlyArray<char>(values);
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
            Array.Sort(probabilities, values, new CustomComparer());

            // Init read only arrays
            Probabilities = new ReadOnlyArray<double>(probabilities);
            Values = new ReadOnlyArray<char>(values);
        }

        public Tree(Tree tree)
        {
            SetTree(tree);
        }
    }
}