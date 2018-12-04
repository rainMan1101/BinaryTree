using System.Collections.Generic;
using BinaryTreeProject.Core.Utils;


namespace BinaryTreeProject.Core.Trees.BinaryTrees
{
    class HaffmanTree : BinaryTree
    {
        private List<double[]> sumsProbabilities = null;

        public List<double[]> ListSumsProbabilities { get { return sumsProbabilities; } }


        public HaffmanTree(Dictionary<char, double> probabilitiesDict) : base(probabilitiesDict) { }

        public HaffmanTree(Tree tree) : base(tree) { }

        public override void Build()
        {
            sumsProbabilities = new List<double[]>();
            rootNode = BuildTree();
        }




        private Node BuildTree()
        {
            // Создание конечных узлов дерева
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < values.Length; i++)
                nodes.Add(new Node()
                {
                    LeftChildNode = null,
                    RightChildNode = null,
                    Value = values[i],
                    Probability = probabilities[i]
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
                // Store array
                StroreProbabilities(nodes.ToArray());

                pointer = new Node();
                if (agreement) //#if TRUE_VARIAN
                {
                    pointer.RightChildNode = nodes[nodes.Count - 2];        // большее (1) - направо
                    pointer.LeftChildNode = nodes[nodes.Count - 1];         // меньшее (0) - налево
                }
                else //#else
                {
                    pointer.LeftChildNode = nodes[nodes.Count - 2];         // большее (1) - налево
                    pointer.RightChildNode = nodes[nodes.Count - 1];        // меньшее (0) - направо
                } //#endif

                pointer.Probability =
                pointer.LeftChildNode.Probability +
                pointer.RightChildNode.Probability;

                nodes.RemoveAt(nodes.Count - 2);
                nodes.RemoveAt(nodes.Count - 1);

                nodes.Add(pointer);
            }
            // Store array
            StroreProbabilities(nodes.ToArray());

            return pointer;
        }



        private void StroreProbabilities(Node[] nodes)
        {
            double[] sumProb = new double[nodes.Length];

            for (int i = 0; i < nodes.Length; i++)
                sumProb[i] = nodes[i].Probability;

            sumsProbabilities.Add(sumProb);
        }

    }
}


