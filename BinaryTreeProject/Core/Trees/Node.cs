namespace BinaryTreeProject.Core.Trees
{
    /*                          Узел дерева                         */

    public class Node
    {

        //  Левый дочерний узел дерева
        public Node LeftChildNode { get; set; }


        //  Правый дочерний узел
        public Node RightChildNode { get; set; }


        //  Символ, соответсвующий данному узлу
        public char Value { get; set; }


        //  Вероятность, соответствующая данному узлу
        public double Probability { get; set; }

    }
}
