using System.Collections.Generic;
using BinaryTreeProject.App.Enums;

namespace BinaryTreeProject.Core.Utils
{
    /*          Интерфейс, описывающий требуемый функционал ПИСАТЕЛЯ
     *   ПИСАТЕЛЬ может выводить передаваеммые ему данные в необходимом формате
     *   (например: txt/csv) или на необходимое устройство (например: 
     *   файл/консоль и т.п.)
     *   
     *   ПИСАТЕЛЬ должен реализовывать два метода (которые требуются от данной 
     *   предметной области). Передаваемые ПИСАТЕЛЮ данные регламентированны 
     *   как параметры методов.
     */
    public interface IWritter
    {

        //  Вывод результатов
        void PrintResults(Dictionary<char, double> probabilityDictionary, Dictionary<char, string> codesDictionary,
            string[,] lastTableColumn, ETreeType treeType);


        //  Вывод результатов декодирования
        void PrintDetailsDecoding(List<KeyValuePair<string, char>> decodeList);
    }
}
