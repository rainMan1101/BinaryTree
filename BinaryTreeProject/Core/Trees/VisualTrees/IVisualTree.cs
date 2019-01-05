using System.Drawing;


namespace BinaryTreeProject.Core.Trees.VisualTrees
{
    /*      Интерфейс, который должны поддерживать классы, визуализирующие древовидную структуру      */

    public interface IVisualTree
    {
        
        //  Рисовать дерево на заданной области определенного размера
        void DrawTree(int width, int heigth, Graphics graph);


        //  Определять оптимальную высоту для экрана вывода
        int GetOptimalHeigth();


        //  Определять оптимальную ширину для экрана вывода
        int GetOptimalWidth();

    }
}