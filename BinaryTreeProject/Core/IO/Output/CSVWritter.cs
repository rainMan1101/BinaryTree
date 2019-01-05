using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BinaryTreeProject.App.Enums;


namespace BinaryTreeProject.Core.Utils
{
    /*                  Класс, оссуществляющий вывод результатов работы программы в формате CSV                 */

    public class CSVWritter : IWritter
    {
        //  Строка, содержащая путь к файлу с результатами работы программы 
        private string outputPath;


        //  Строка, содержащая путь к файлу с результатами декодирования
        private string outputDecodePath;


        public CSVWritter(string outputPath, string outputDecodePath)
        {
            this.outputPath = outputPath;
            this.outputDecodePath = outputDecodePath;
        }

        public void PrintDetailsDecoding(List<KeyValuePair<string, char>> list)
        {
            using (StreamWriter sw = new StreamWriter(outputDecodePath, false, Encoding.Default))
            {
                //  Вывод шапки
                sw.WriteLine("Шаг;Комбинация;Символ");

                //  Вывод значений
                for (int i = 0; i < list.Count; i++)
                {
                    string subStr = (list[i].Value == ' ') ? "\" \"" : (list[i].Value == ';') ? "\";\"" : "" + list[i].Value;
                    string str = (i+1) + ";" + "_" +  list[i].Key + ";" + subStr;
                    sw.WriteLine(str);
                }
            }
        }


        public void PrintResults(Dictionary<char, double> probabilityDictionary, Dictionary<char, string> codesDictionary, 
            string[,] lastTableColumn, ETreeType treeType)
        {
            using (StreamWriter sw = new StreamWriter(outputPath, false, Encoding.Default))
            {
                char[] chars = probabilityDictionary.Keys.ToArray();
                double[] probabilities = probabilityDictionary.Values.ToArray();
                Array.Sort(probabilities, chars, new CustomComparer());

                

                if (probabilityDictionary?.Count() != 0)
                {
                    //  Количество строк
                    int countAlfabet = probabilityDictionary.Count;
                    //  Количество столбцов в 3 графе
                    int countColunns = lastTableColumn.Length / countAlfabet;

                    //  Вывод шапки
                    sw.WriteLine("Символ;Вероятность;Код символа;Ступени деления");
                    string strHeader = ";;;";
                    for (int i = 1; i <= countColunns; i++)
                        strHeader += ";" + i;
                    sw.WriteLine(strHeader);

                    //  Вывод значений
                    for (int i = 0; i < chars.Length; i++)
                    {
                        string subStr = (chars[i] == ' ') ? "\" \"" : (chars[i] == ';') ? "\";\"" : "" + chars[i];

                        string str = subStr + ";" + probabilities[i] + ";" + "_" + codesDictionary[chars[i]];
                        string other_string = "";

                        for (int j = 0; j < countColunns; j++)
                            other_string += ";" + lastTableColumn[i, j];

                        sw.WriteLine(str + other_string);
                    }
                }

            }
        }



    }
}
