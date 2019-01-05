using System.Collections.Generic;
using BinaryTreeProject.Core.Utils;


namespace BinaryTreeProject.Core.Trees.BinaryTrees
{
    /*               Класс, формирующий деревидную структуру по алгоритму Хаффмана              */

    public class HaffmanTree : BinaryTree
    {
        //  Частичные суммы вероятностей
        private List<double[]> sumsProbabilities = null;

        public List<double[]> ListSumsProbabilities { get { return sumsProbabilities; } }


        /*                                  Конструкторы                                        */

        public HaffmanTree(Dictionary<char, double> probabilitiesDict) : base(probabilitiesDict) { }

        public HaffmanTree(Tree tree) : base(tree) { }

        
        public override void Build()
        {
            sumsProbabilities = new List<double[]>();
            rootNode = BuildTree();
        }


        /*
         *              Итеративный алгоритм построения дерева Хаффмана
         *                            (Haffman algorithm)
         * 
         * 
         *      0. Формирование списка узлов(конечных) для каждого символа.
         *      
         *      1. Сортировка списка узлов по убыванию вероятностей.
         *      
         *      2. Создание нового узла, который будет указывать на 2 дочерних -
         *      2 последних узла из списка.
         *      
         *      3. Удаление последних двух элементов списка.
         *      
         *      4. Добавление в список узла, созданного на 2 шаге.
         *      
         *      5. Переход к шагу 1, пока в списке не останиется один элемент -
         *      корневой узел дерева.
         * 
         */
        private Node BuildTree()
        {
            // Создание конечных узлов дерева
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < Values.Length; i++)
                nodes.Add(new Node()
                {
                    LeftChildNode = null,
                    RightChildNode = null,
                    Value = Values[i],
                    Probability = Probabilities[i]
                });


            // Построение дерева

            CustomNodeComparer comparer = new CustomNodeComparer();
            Node pointer = new Node()       // default
            {
                LeftChildNode = null,
                RightChildNode = null,
                Probability = default(double),
                Value = default(char)
            };

            while (nodes.Count > 1)
            {
                nodes.Sort(comparer);
                // Store probability array
                StroreProbabilities(nodes.ToArray());

                pointer = new Node();
                if (agreement) //   Направление
                {
                    pointer.RightChildNode = nodes[nodes.Count - 2];        // большее (1) - направо
                    pointer.LeftChildNode = nodes[nodes.Count - 1];         // меньшее (0) - налево
                }
                else 
                {
                    pointer.LeftChildNode = nodes[nodes.Count - 2];         // большее (1) - налево
                    pointer.RightChildNode = nodes[nodes.Count - 1];        // меньшее (0) - направо
                } 

                pointer.Probability =
                pointer.LeftChildNode.Probability +
                pointer.RightChildNode.Probability;

                nodes.RemoveAt(nodes.Count - 2);
                nodes.RemoveAt(nodes.Count - 1);

                nodes.Add(pointer);
            }
            // Store probability array
            StroreProbabilities(nodes.ToArray());

            return pointer;
        }
        

        //  Запоминание частичных сумм вероятностей в процессе построения дерева
        private void StroreProbabilities(Node[] nodes)
        {
            double[] sumProb = new double[nodes.Length];

            for (int i = 0; i < nodes.Length; i++)
                sumProb[i] = nodes[i].Probability;

            sumsProbabilities.Add(sumProb);
        }

    }
}


