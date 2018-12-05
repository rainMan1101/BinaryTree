﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BinaryTreeProject.App.Enums;


namespace BinaryTreeProject.Core.Utils
{
    public class PrettyOutputFile
    {
        private string _outputPath;

        private const int defaultStepLength = 3;


        public PrettyOutputFile(string outputPath)
        {
            _outputPath = outputPath;
        }

        public string PrintHeader(StreamWriter sw, int maxBinaryLength, 
            int countColunns, int symbolLengthColumn, string label, int separator)
        {
            const int additionalSpacesBinaryCount = 3;
            maxBinaryLength += additionalSpacesBinaryCount;

            string thirdColumn = " Код символа ";
            // Добавочная строка к коду символа
            string addition_string = "";

            if (thirdColumn.Length < maxBinaryLength)
                while (maxBinaryLength != thirdColumn.Length) thirdColumn += " ";

            if (thirdColumn.Length > maxBinaryLength)
                while (maxBinaryLength != thirdColumn.Length) addition_string += " ";


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

            int xcoord = countColunns * symbolLengthColumn / 2 - label.Length / 2;
            for (int i = 0; i < xcoord; i++)
                steps_string_spaces += " ";
            steps_string_spaces += label;
            for (int i = 0, length = steps_string_spaces.Length; i < countColunns * symbolLengthColumn - length; i++)
                steps_string_spaces += " ";

            // Оставшившиeся столбцов
            int countThreeFirst = firsColumnLength + secondColumnLength + thirdColumnLength;
            string steps_string = "";


            
            for(int i = 1; i <= countColunns; i++)
            {
                int countSpaces = symbolLengthColumn - ("" + i).Length - separator;
                string str1 = "";
                string str2 = "";

                for (int j = 0; j < countSpaces; j++) str1 += " ";
                for (int j = 0; j < separator; j++) str2 += " ";

                steps_string += str1 + Convert.ToString(i) + str2;
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


        public void PrintResults(char[] chars, double[] probabilities, Dictionary<char, string> codes, string[,] steps, ETreeType treeType)
        {

            using (StreamWriter sw = new StreamWriter(_outputPath, false, Encoding.Default))
            {
                //!!!
                Array.Sort(probabilities, chars, new CustomComparer());

                if (chars?.Count() != 0)
                {
                    int maxBinaryLength = codes.Values.ToArray().Max(str => str.Length);
                    int countAlfabet = probabilities.Length;
                    int countColunns = steps.Length / countAlfabet;
                    string string1, string2, string3;


                    string addition_string;

                    if (treeType == ETreeType.ShannonTree)
                        addition_string = PrintHeader(sw, maxBinaryLength, countColunns, steps[0, 0].Length, "Ступени деления", 0);
                    else
                        addition_string = PrintHeader(sw, maxBinaryLength, countColunns, steps[0, 0].Length, "Суммы вероятностей", 2);

                    maxBinaryLength = -maxBinaryLength;



                    for (int i = 0; i < chars.Length; i++)
                    {
                        string1 = String.Format("    {0}    ", chars[i]);
                        string2 = String.Format(" {0:0.00000000}  ", probabilities[i]);
                        string3 = String.Format("  {0," + maxBinaryLength + "} " + addition_string, codes[chars[i]]);

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
                // Количество цифр в числе
                int countNum = (""+ count).Length;


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
