using System.Collections.Generic;


namespace BinaryTreeProject.Core.Trees.BinaryTrees
{
    public class ShannonTree : BinaryTree
    {
        // Индексы начального и конечного элемента считанного списка 
        private const int begin = 0;

        private int end;

        // Коллекции для вывода 
        //private List<int> spliterSteps;                  // Ступени деления
        /*
         * TODO: Ступени строятся неверно.
         *      Сейчас: по одному разделителю на каждый шаг.
         *      Нужно: по два разделителя на каждый шаг, кроме первого
         *      
         *      Структура: List<KeyValuePair<int, int>>
         */
        private List<KeyValuePair<int, int>> spliterSteps;

        // Конструкторы
        public ShannonTree(Dictionary<char, double> probabilitiesDict) : base(probabilitiesDict) { }

        public ShannonTree(Tree tree) : base(tree) { }

        // Построение кодового дерева
        public override void Build()
        {
            spliterSteps = new List<KeyValuePair<int, int>>();
            end = Values.Length - 1;

            rootNode = BuildTree(begin, end, FULL_PROBABILITY / 2, 1); // Начало с  1 !!!
        }

        public List<KeyValuePair<int, int>> GetSpliterSteps()
        {
            // Передаю оригиал, так как этот массив не используется в самом дереве,
            // и не грозит нарушить его целостность.
            return spliterSteps;
        }

        // Рекурсивный алгоритм построения дерева
        private Node BuildTree(int begin, int end, double avrProb, int stepNumber)
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
                //spliterSteps.Add(spliter);
                spliterSteps.Add(new KeyValuePair<int, int>(stepNumber, spliter));
                stepNumber++;

                return
                    new Node()
                    {
                        LeftChildNode = BuildTree(begin, spliter, totalP / 2, stepNumber),
                        RightChildNode = BuildTree(spliter + 1, end, (avrProb * 2 - totalP) / 2, stepNumber),
                        Value = default(char),
                        Probability = avrProb * 2
                    };
            }
        }

    }


}
