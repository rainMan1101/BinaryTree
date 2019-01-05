using System;
using System.Collections.Generic;
using System.Linq;
using BinaryTreeProject.Core.Utils;


namespace BinaryTreeProject.Core.Trees
{
    /*
     *                           Дерево (BASE CLASS) 
     *
     *      Класс, инкапсулирующий в себе массивы с символами и вероятностями
     *  и указатель на древовидную структуру.
     * 
     */
    public abstract class Tree
    {
        // Корневой узел дерева
        protected Node rootNode;


        /* 
         * Коллекции, используемые методами класса Tree в процессе работы.
         * Каждому 1 элементу values соответствует 1 элемент probabilities и наоборот.
         * Эти массивы являются отсортированными в порядке убывания значений probabilities
         * и не доступны для изменения (доступ только через объект типа ReadOnlyArray) 
         * в дочерних классах. 
         */

        // Массив вероятностей
        private double[] probabilities;


        // Массив символов 
        private char[] values;                                   


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


        //  Массивы только для чтения в дочерних классах
        protected ReadOnlyArray<double> Probabilities;

        protected ReadOnlyArray<char> Values;

        #endregion


        // default (направление)
        protected bool agreement = false;

        public bool Agreement { get { return agreement; } set { agreement = value; } }


        //  Установка дерева без создания нового (копирование массивов и самого дерева)
        public void SetTree(Tree tree)
        {
            if (tree != null)
            {
                this.probabilities = new double[tree.probabilities.Length];
                this.values = new char[tree.values.Length];

                Array.Copy(tree.probabilities, this.probabilities, tree.probabilities.Length);
                Array.Copy(tree.values, this.values, tree.values.Length);

                this.agreement = tree.agreement;

                this.rootNode = CopyTree(tree.rootNode);
                //this.rootNode = tree.rootNode;

                // Init read only arrays
                Probabilities = new ReadOnlyArray<double>(probabilities);
                Values = new ReadOnlyArray<char>(values);
            }
        }


        //  Копирование древовидной структуры
        private Node CopyTree(Node originalRootNode)
        {
            if (originalRootNode.LeftChildNode == null && originalRootNode.RightChildNode == null)
            {
                return new Node()
                {
                    LeftChildNode = null,
                    RightChildNode = null,
                    Value = originalRootNode.Value,
                    Probability = originalRootNode.Probability
                };
            }
            else
            {
                Node LeftChild = CopyTree(originalRootNode.LeftChildNode);
                Node RightChild = CopyTree(originalRootNode.RightChildNode);

                return new Node()
                {
                    LeftChildNode = LeftChild,
                    RightChildNode = RightChild,
                    Value = originalRootNode.Value,
                    Probability = originalRootNode.Probability
                };
            }
        }


        /*                                    Конструкторы                                  */


        //  Конструктор, сохраняющий массивы символов и вероятностей внутри объекта класса Tree
        public Tree(Dictionary<char, double> probabilitiesDict)
        {
            values = probabilitiesDict.Keys.ToArray();
            probabilities = probabilitiesDict.Values.ToArray();

            if (values.Length == 0 || probabilities.Length == 0)
                throw new Exception("Задан пустой список символов и вероятностей.");

            /* 
             * Сортирую массив вероятностей (probabilities), в порядке их убывания.
             * Так же элементы массива символов (values) изменяют свое положение,
             * на соответствующее положение вероятностей, сопоставимым им по индексам.
             */
            Array.Sort(probabilities, values, new CustomComparer());

            // Init read only arrays
            Probabilities = new ReadOnlyArray<double>(probabilities);
            Values = new ReadOnlyArray<char>(values);
        }


        //  Конструктор копирующий массивы и древовидную структукру с другого дерева
        public Tree(Tree tree)
        {
            SetTree(tree);
        }


        // Расчет энтропии
        public double GetEntropy()
        {
            double summ = 0;

            for (int i = 0; i < probabilities.Length; i++)
                summ += probabilities[i] * Math.Log(probabilities[i], 2.0);

            return -summ;
        }
    }
}