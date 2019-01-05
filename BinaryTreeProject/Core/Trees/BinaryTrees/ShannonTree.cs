using System.Collections.Generic;


namespace BinaryTreeProject.Core.Trees.BinaryTrees
{
    /*               Класс, формирующий деревидную структуру по алгоритму Шеннона              */

    public class ShannonTree : BinaryTree
    {
        // Индексы начального и конечного элемента считанного списка 
        private const int begin = 0;

        private int end;


        /*
         *                              Ступени деления
         *                              
         *      Данный список содержит пары значений. Первое значение - текущий уровень 
         *  в дереве начиная сверху, второй - индекс в списке, в ктором произошло разделение 
         *  на два подсписка.
         *   
         */
        private List<KeyValuePair<int, int>> spliterSteps;


        /*                                  Конструкторы                                        */

        public ShannonTree(Dictionary<char, double> probabilitiesDict) : base(probabilitiesDict) { }

        public ShannonTree(Tree tree) : base(tree) { }


        // Построение кодового дерева 
        public override void Build()
        {
            spliterSteps = new List<KeyValuePair<int, int>>();
            end = Values.Length - 1;

            rootNode = BuildTree(begin, end, FULL_PROBABILITY / 2, 1); 
        }

        public List<KeyValuePair<int, int>> GetSpliterSteps()
        {
            return spliterSteps;
        }


        /*              
         *                      Рекурсивный алгоритм построения дерева
         *                          (customized Shannon algorithm)
         *              
         *              
         *   0. Массивы отсортированны по убыванию вероятностей. Для массивов устанавливливаются 
         *   индексы начала и конца. (Изначально 0 - начало, конец - размер массива - 1).
         *   
         *   1. Начиная сверху списка(с символов с наибольшей вероятностью) последовательно
         *   происходит сложение вероятностей до того момента, пока данная сумма вероятностей 
         *   не станет наибольшим числом меньшим половинной вероятности.
         *      Исходная вероятность изначально равна 1, половинная - 0.5.
         *      
         *   2. Список делится на два посписка. Все элементы списка, чья вероятность вошла 
         *   в общую сумму - первая посписок, остальные - второй. Устанавливливаются 
         *   индексы начала и конца. Для первой части начало - начало исходного списка,
         *   а конец - последний элемент входящий в сумму. Для второго начало - 
         *   следующий индекс, после индекса конца первого подсписка, а конец -
         *   конец исходного списка.
         *   
         *   3. Для первого подспика его половинная вероятность будет равна сумме вероятностей
         *   деленной на 2. А для второго -  вероятность рассчитывается как (полная вероятность 
         *   исходного списка - сумма вероятностей)/2.
         *   
         *   4. Переход к шагу 1, пока индекс начала не станет равен индексу конца.
         *   
         */
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
