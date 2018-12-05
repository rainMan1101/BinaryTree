using System.Collections.Generic;


namespace BinaryTreeProject.Core.Trees.BinaryTrees
{
    public class ShannonTree : BinaryTree
    {
        // Индексы начального и конечного элемента считанного списка 
        private const int begin = 0;

        private int end;

        // Коллекции для вывода 
        private List<int> spliterSteps;                  // Ступени деления
        /*
         * TODO: Ступени строятся неверно.
         *      Сейчас: по одному разделителю на каждый шаг.
         *      Нужно: по два разделителя на каждый шаг, кроме первого
         *      
         *      Структура: List<KeyValuePair<int, int>>
         */


        // Конструкторы
        public ShannonTree(Dictionary<char, double> probabilitiesDict) : base(probabilitiesDict) { }

        public ShannonTree(Tree tree) : base(tree) { }

        // Построение кодового дерева
        public override void Build()
        {
            spliterSteps = new List<int>();
            end = Values.Length - 1;

            rootNode = BuildTree(begin, end, FULL_PROBABILITY / 2);
        }

        public int[] GetSpliterSteps()
        {
            // Передаю оригиал, так как этот массив не используется в самом дереве,
            // и не грозит нарушить его целостность.
            return spliterSteps.ToArray();
        }

        // Рекурсивный алгоритм построения дерева
        private Node BuildTree(int begin, int end, double avrProb)
        {
            if (begin == end)
                return
                    new Node()
                    {
                        LeftChildNode = null,
                        RightChildNode = null,
                        Value = Values[begin],
                        Probability = Probabilities[begin]
                    };
            else
            {
                // Total probability
                double totalP = 0;
                int spliter = begin;

                if (avrProb >= Probabilities[begin])
                {
                    for (int i = begin; (avrProb >= totalP + Probabilities[i]) && (i < end); i++)
                    {
                        totalP += Probabilities[i];
                        spliter++;
                    }

                    spliter -= 1;
                }

                // ступени деления
                spliterSteps.Add(spliter);

                return
                    new Node()
                    {
                        LeftChildNode = BuildTree(begin, spliter, totalP / 2),
                        RightChildNode = BuildTree(spliter + 1, end, (avrProb * 2 - totalP) / 2),
                        Value = default(char),
                        Probability = avrProb * 2
                    };
            }
        }

    }


}
