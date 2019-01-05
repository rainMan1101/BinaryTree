using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BinaryTreeProject.App.Enums;
using BinaryTreeProject.Core.Utils;



namespace BinaryTreeProject.Core.IO.Output
{
    /*      Класс, оссуществляющий вывод результатов работы программы в виде псевдографики в текстовый файл       */

    public class TXTWriter : IWriter
    {

        //  Строка, содержащая путь к файлу с результатами работы программы 
        private string outputPath;


        //  Строка, содержащая путь к файлу с результатами декодирования
        private string outputDecodePath;


        public TXTWriter(string outputPath, string outputDecode)
        {
            this.outputPath = outputPath;
            this.outputDecodePath = outputDecode;
        }

        //  Вывод шапки
        private string PrintHeader(StreamWriter sw, int maxBinaryLength, int countColumnsInLastTableColumn, 
            int columnLengthInLastTableColunn, string label, int separator)
        {
            //  Шапка
            string thirdColumn = " Код символа ";
            // Добавочная строка к коду символа
            string addition_string = "";

            //  maxBinaryLength - размер сроки с кодом символа вместе с отступами по бокам
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

            // Первая строка оставшихся столбцов
            string steps_string_spaces = "";

            //  Формирование строки с надписью по центру
            int xcoord = countColumnsInLastTableColumn * columnLengthInLastTableColunn / 2 - label.Length / 2;
            for (int i = 0; i < xcoord; i++)
                steps_string_spaces += " ";
            steps_string_spaces += label;
            for (int i = 0, length = steps_string_spaces.Length; i < countColumnsInLastTableColumn * columnLengthInLastTableColunn - length; i++)
                steps_string_spaces += " ";

            // Для оставшихся столбцов
            string steps_string = "";


            //  Вывод номеров колонок в 3 колонке таблицы
            for(int i = 1; i <= countColumnsInLastTableColumn; i++)
            {
                int countSpaces = columnLengthInLastTableColunn - ("" + i).Length - separator;
                string str1 = "";
                string str2 = "";

                for (int j = 0; j < countSpaces; j++) str1 += " ";
                for (int j = 0; j < separator; j++) str2 += " ";

                steps_string += str1 + Convert.ToString(i) + str2;
            }


            steps_string += "|";
            steps_string_spaces += "|";

            //  Пунктир снизу
            int fullLength = firstTreeColums.Length + steps_string.Length;
            string line = "";
            for (int i = 0; i < fullLength; i++) line += "-";

            //  Вывод шапки
            sw.WriteLine(firstTreeColums + steps_string_spaces);
            sw.WriteLine(firstTreeColums_spaces + steps_string);
            sw.WriteLine(line);

            //  Возврат дополняющей строки для значений в 3 колонке таблицы
            return addition_string;
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

                    //  Максимальный размер кодового слова из всех имеющихся
                    int maxBinaryLength = codesDictionary.Values.ToArray().Max(str => str.Length);
                    //  Определение количества колонок в последней графе таблицы
                    int countColumnsInLastTableColumn = lastTableColumn.Length / probabilityDictionary.Count;
                    //  Строка1 - первая колонка таблицы, 2 - 2, 3 - 3
                    string string1, string2, string3;

                    //  Строка, содержацая пробелы, для дополнения ЗНАЧЕНИЙ из 3 графы до размеров шапки данной графы
                    string addition_string;

                    if (treeType == ETreeType.ShannonTree)
                        //  maxBinaryLength + 3 - плюс 3 пробела по краям в строке вывода string3
                        addition_string = PrintHeader(sw, maxBinaryLength + 3, countColumnsInLastTableColumn, 
                            lastTableColumn[0, 0].Length, "Ступени деления", 0);
                    else
                        addition_string = PrintHeader(sw, maxBinaryLength + 3, countColumnsInLastTableColumn, 
                            lastTableColumn[0, 0].Length, "Суммы вероятностей", 2);

                    //  Для вывода
                    maxBinaryLength = -maxBinaryLength;


                    //  Построчный вывод в файл
                    for (int i = 0; i < chars.Length; i++)
                    {
                        string1 = String.Format("    {0}    ", chars[i]);
                        string2 = String.Format(" {0:0.00000000}  ", probabilities[i]);
                        string3 = String.Format("  {0," + maxBinaryLength + "} " + addition_string, codesDictionary[chars[i]]);

                        string first_three = String.Format("|{0}|{1}|{2}|", string1, string2, string3);
                        string other_string = "";

                        for (int j = 0; j < countColumnsInLastTableColumn; j++)
                            other_string += lastTableColumn[i, j];

                        sw.WriteLine(first_three + other_string);
                    }

                }
            }
        }



        public void PrintDetailsDecoding(List<KeyValuePair<string, char>> list)
        {
            using (StreamWriter sw = new StreamWriter(outputDecodePath, false, Encoding.Default))
            {
                //  Максимальный размер двоичного кода символа
                int max = list.Max(element => element.Key.Length);
                //  Количество цифр в числе(номере строки)
                int countNum = (""+ list.Count).Length;

                #region PRINT HEADER

                string firstColumn = "Шаг";
                string secondColumn = "Комбинация";
                string thitdColumn = "Символ";

                string additionalStringFirst = "";
                string additionalStringSecond = "";

                //  Подгон размеров шапки и значений для 1 столбца
                if (countNum + 3 > firstColumn.Length)
                    while (countNum + 3 != firstColumn.Length) firstColumn += " ";

                if (countNum + 3 < firstColumn.Length)
                    while (countNum + 3 + additionalStringFirst.Length != firstColumn.Length)
                        additionalStringFirst += " ";

                //  Подгон размеров шапки и значений для 2 столбца
                if (max + 3 > secondColumn.Length)
                    while (max + 3 != secondColumn.Length) secondColumn += " ";

                if (max + 3 < secondColumn.Length)
                    while (max + 3 + additionalStringSecond.Length != secondColumn.Length)
                        additionalStringSecond += " ";

                //  Вывод шапки
                string newString = String.Format("{0}|{1}|{2}", firstColumn, secondColumn, thitdColumn);
                sw.WriteLine(newString);

                //  Вывод пунктира под шапкой
                string line = "";
                for (int i = 0; i < newString.Length; i++)
                    line += "-";

                sw.WriteLine(line);

                #endregion PRINT HEADER


                //  Вывод значений
                for (int i = 0; i < list.Count; i++)
                {
                    string str = String.Format("  {0," + countNum + "} "+ additionalStringFirst, (i + 1)) + "|";
                    str += String.Format("  {0," + (-max) + "} " + additionalStringSecond, list[i].Key) + "|";
                    str += list[i].Value;
                    sw.WriteLine(str);
                }
            }
        }


    }
}
