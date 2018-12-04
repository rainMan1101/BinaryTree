using System.Collections.Generic;


namespace BinaryTreeProject.Core.Trees.BinaryTrees
{
    public abstract class BinaryTree : Tree
    {

        public BinaryTree(Dictionary<char, double> probabilitiesDict) : base(probabilitiesDict) { }

        public BinaryTree(Tree tree) : base(tree) { }

        // Коллекции для вывода 
        private Dictionary<char, string> binaryCodes;      // Символы (char) и их двоичные коды (string)

        public abstract void Build();

        // Public методы, вызываются после Build()
        public Dictionary<char, string> GetBinaryCodes()
        {
            binaryCodes = new Dictionary<char, string>();
            FillBinaryCodesArray(rootNode);
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
                if (agreement) //#if TRUE_VARIAN
                {
                    FillBinaryCodesArray(node.RightChildNode, binaryCode + '1');    // право - 1
                    FillBinaryCodesArray(node.LeftChildNode, binaryCode + '0');     // лево - 0
                }
                else //#else
                {
                    FillBinaryCodesArray(node.RightChildNode, binaryCode + '0');    // право - 0
                    FillBinaryCodesArray(node.LeftChildNode, binaryCode + '1');     // лево - 1
                } //#endif
            }
        }

   }
}