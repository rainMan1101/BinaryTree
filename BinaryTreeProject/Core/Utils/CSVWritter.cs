using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BinaryTreeProject.App.Enums;


namespace BinaryTreeProject.Core.Utils
{
    public class CSVWritter : IWritter
    {
        private string outputPath;

        private string outputDecodePath;

        public CSVWritter(string outputPath, string outputDecode)
        {
            this.outputPath = outputPath;
            this.outputDecodePath = outputDecode;
        }

        public void PrintDetailsDecoding(List<KeyValuePair<string, char>> list)
        {
            using (StreamWriter sw = new StreamWriter(outputDecodePath, false, Encoding.Default))
            {
                sw.WriteLine("Шаг;Комбинация;Символ");

                for (int i = 0; i < list.Count; i++)
                {
                    string subStr = (list[i].Value == ' ') ? "\" \"" : (list[i].Value == ';') ? "\";\"" : "" + list[i].Value;
                    string str = i + ";" + "_" +  list[i].Key + ";" + subStr;
                    sw.WriteLine(str);
                }
            }
        }


        public void PrintResults(char[] chars, double[] probabilities, Dictionary<char, string> codes, string[,] steps, ETreeType treeType)
        {
            using (StreamWriter sw = new StreamWriter(outputPath, false, Encoding.Default))
            {
                //!!!
                Array.Sort(probabilities, chars, new CustomComparer());

                

                if (chars?.Count() != 0)
                {
                    int countAlfabet = probabilities.Length;
                    int countColunns = steps.Length / countAlfabet;


                    sw.WriteLine("Символ;Вероятность;Код символа;Ступени деления");
                    string strHeader = ";;;";
                    for (int i = 1; i <= countColunns; i++)
                        strHeader += ";" + i;


                    for (int i = 0; i < chars.Length; i++)
                    {
                        string subStr = (chars[i] == ' ') ? "\" \"" : (chars[i] == ';') ? "\";\"" : "" + chars[i];

                        string str = subStr + ";" + probabilities[i] + ";" + "_" + codes[chars[i]];
                        string other_string = "";

                        for (int j = 0; j < countColunns; j++)
                            other_string += ";" + steps[i, j];

                        sw.WriteLine(str + other_string);
                    }
                }
            }
        }

    }
}
