using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace BinaryTreeProject.Core.Utils
{
    class PrettyOutputFile
    {
        private string _outputPath;

        private const int defaultStepLength = 3;


        public PrettyOutputFile(string outputPath)
        {
            _outputPath = outputPath;
        }

        public string PrintHeader(StreamWriter sw, int maxBCLength, int countColunns)
        {

            string thirdColumn = " Код символа ";
            // Добавочная строка к коду символа
            string addition_string = "";

            // "Код символа" - добавляем или в шапку или в таблицу
            if (thirdColumn.Length < maxBCLength + 3)
                while (maxBCLength + 3 != thirdColumn.Length)
                    thirdColumn += " ";
            else if (thirdColumn.Length > maxBCLength + 3)
                while (maxBCLength + 3 + addition_string.Length != thirdColumn.Length)
                    addition_string += " ";

            // Подчет длинны первых трех столбцов
            // Первая строка первых 3х столбцов
            string firstTreeColums = @"| Символ  | Вероятность |" + thirdColumn + "|";
            int firsColumnLength = " Символ  ".Length;
            int secondColumnLength = " Вероятность ".Length;
            int thirdColumnLength = thirdColumn.Length;

            // Вторая строка первых 3х столбцов
            string firstTreeColums_spaces = "|";
            for (int i = 0; i < firsColumnLength; i++) firstTreeColums_spaces += " ";
            firstTreeColums_spaces += "|";
            for (int i = 0; i < secondColumnLength; i++) firstTreeColums_spaces += " ";
            firstTreeColums_spaces += "|";
            for (int i = 0; i < thirdColumnLength; i++) firstTreeColums_spaces += " ";
            firstTreeColums_spaces += "|";

            // Первая стока оставштхся столбцов
            string steps_string_spaces = "";
            int xcoord = countColunns * 3 / 2 - "Ступени деления".Length / 2;
            for (int i = 0; i < countColunns * 3 / 2; i++)
                steps_string_spaces += " ";
            steps_string_spaces += "Ступени деления";
            for (int i = 0, length = steps_string_spaces.Length; i < countColunns * 3 - length; i++)
                steps_string_spaces += " ";

            // Оставшившиeся столбцов
            int countThreeFirst = firsColumnLength + secondColumnLength + thirdColumnLength;
            string steps_string = "";

            if (countColunns <= 99)
            {
                for (int i = 1; i <= countColunns; i++)
                {
                    if (i >= 1 && i <= 9)
                        steps_string += " " + Convert.ToString(i) + " ";
                    else
                        steps_string += Convert.ToString(i) + " ";
                }
            }

            steps_string += "|";
            steps_string_spaces += "|";

            int fullLength = firstTreeColums.Length + steps_string.Length;
            string line = "";
            for (int i = 0; i < fullLength; i++) line += "-";


            sw.WriteLine(firstTreeColums + steps_string_spaces);
            sw.WriteLine(firstTreeColums_spaces + steps_string);
            sw.WriteLine(line);
            return addition_string;
        }

        //public void PrintResults(char[] chars, Dictionary<char, double> probabilities, Dictionary<char, string> codes, int[] stepsIndexes)
        public void PrintResults(char[] chars, double[] probabilities, Dictionary<char, string> codes, string[,] steps)
        {

            using (StreamWriter sw = new StreamWriter(_outputPath, false, Encoding.Default))
            {
                //!!!
                Array.Sort(probabilities, chars, new CustomComparer());

                if (chars?.Count() != 0)
                {
                    int maxLength = codes.Values.ToArray().Max(str => str.Length);
                    int countAlfabet = probabilities.Length;
                    int countColunns = steps.Length / countAlfabet;
                    //string[,] steps = GenerateSteps(stepsIndexes, chars.Length);
                    string string1, string2, string3;

                    string addition_string = PrintHeader(sw, maxLength, countColunns);

                    maxLength = - maxLength;

                    for (int i = 0; i < chars.Length; i++)
                    {
                        string1 = String.Format("    {0}    ", chars[i]);
                        string2 = String.Format(" {0:0.00000000}  ", probabilities[i]);
                        string3 = String.Format("  {0," + maxLength + "} " + addition_string, codes[chars[i]]);

                        string first_three = String.Format("|{0}|{1}|{2}|", string1, string2, string3);
                        string other_string = "";

                        for (int j = 0; j < countColunns; j++)
                            other_string += steps[i, j];

                        sw.WriteLine(first_three + other_string);
                    }

                }
            }
        }

        public void PrintDetailsDecoding(List<KeyValuePair<string, char>> list)
        {
            using (StreamWriter sw = new StreamWriter(_outputPath, false, Encoding.Default))
            {
                int max = list.Max(element => element.Key.Length);
                int count = list.Count;

                //Max char count 0xFFFF = 65 535
                int maxAlphabet = 10000;
                // Количество цифр в числе
                int countNum = 5;

                while (count % maxAlphabet == count && countNum > 1)
                {
                    maxAlphabet /= 10;
                    countNum--;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    string str = String.Format("  {0," + countNum + "} ", i) + " | ";
                    str += String.Format("  {0," + (-max) + "} ", list[i].Key) + " | ";
                    str += list[i].Value;
                    sw.WriteLine(str);
                }
            }
        }
    }
}
