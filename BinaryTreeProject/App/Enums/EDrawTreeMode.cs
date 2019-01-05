namespace BinaryTreeProject.Core.Trees.VisualTrees.Additions
{
    
    /*              Режим визуализации дерева с узлами            */

    public enum EDrawTreeMode
    {
        
        //  Не отрображать ничего
        Nothing,


        //  Отображать вероятность
        Probability,


        //  Отображать двоичный код символа в узлах
        BinaryNodes,


        //  Отображать символы алфавита в крайних узлах (листьях) дерева
        CharNodes,


        //  Отображать символы алфавита под крайними узлами (листьями) дерева
        CharUnder,


        //  Отображать вероятность и двоичный код символа в узлах
        ProbabilityWithBinaryNodes,


        //  Отображать вероятность и символы алфавита в крайних узлах
        ProbabilityWithCharNodes,


        //  Отображать вероятность и символы алфавита под крайними узлами
        ProbabilityWithCharUnder,


        //  Отображать двоичный код символа и символы алфавита под крайними узлами
        BinaryNodesWithCharUnder,


        //  Отображать вероятность, двоичный код и символы алфавита под крайними узлами 
        ProbabilityWithBinaryNodesWithCharUnder

    }
}
