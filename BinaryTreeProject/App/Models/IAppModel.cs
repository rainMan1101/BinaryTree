using System.Drawing;
using BinaryTreeProject.App.Enums;
using BinaryTreeProject.Core.Trees.VisualTrees.Additions;


namespace BinaryTreeProject.App.Models
{
    /*                          Интерфейс, описывающий функционал приложения                    */

    //  Приложение "должно уметь":
    public interface IAppModel
    {
        
        //  Отображать дерево
        void DrawTree(Graphics graph, int heigth, int width, EDrawNodeMode drawNodeMode, EDrawTreeMode drawTreeMode);


        //  Изменять внешний вид дерева
        void ReplaceTree(ETreeType treeType);


        //  Изменять направление
        void ReplaceAgreement(bool agreement);


        //  Выводить результаты
        void PrintResult();


        //  Выводить среднее количество информации которую несет один сивол в битах
        double GetValueInfo();


        //  Кодировать исходное сообщение по определенному алгоритму
        string Encode(string originalString);


        //  Расшифровывать закодированную стоку и выводить шаги декодирования в файл
        string Decode(string binaryString, string outputString);

    }
}
