using System.Drawing;
using BinaryTreeProject.Core.Additions;


namespace BinaryTreeProject.Core.Trees.VisualNodes
{
    /*
     *      Интерфейс, содержащий набор необходимых свойств и методов, 
     *  для классов, описывающих визуальное представления узлов дерева.
     *  
     */
    public interface IVisualNode
    {

        //  Размер стороны квадрата, ограничивающего фигуру
        //  (Размер фигуры задается в TreeWithNodes в зависимости от размера экрана)
        float FigureSize { get; set; }


        //  Принятое расстояние по вертикали между центами узлов
        float StepHeigth { set; }


        //  Вывод фигуры по заданным координатам (координаты центра узла)
        void DrawNode(Graphics graph, float x, float y, Pen pen);


        //  Насколько нужно сместить координаты по X и Y, относительно центра фигуры, 
        //  чтобы линяя выходила из края фигуры
        OffsetOneSide GetSideOffset(float x1, float y, float x2);


        //  Выполняет GetSideOffset для левого и правого дочернего узла 
        Offset GetOffset(float x, float y, float xLeftChild, float xRightChild);
    }
}
