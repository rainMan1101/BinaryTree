using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.VisualBasic.FileIO;

namespace BinaryTreeProject.Core.Utils
{
    public class CustomCSVParser
    {
        private double[] probabilities = null;

        private char[] values = null;


        public double[] Probabilities { get { return probabilities; } }

        public char[] Values { get { return values; } }


        public CustomCSVParser(string inputFilePath)
        {
            List<string> listProbabilities = new List<string>();
            List<string> listValues = new List<string>();

            // READING 
            using (TextFieldParser parser = new TextFieldParser(inputFilePath, Encoding.Default, false))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");


                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (fields.Length < 2) continue;
                    else
                    {
                        //fields[0].Replace("\"", "").Replace("'", "");
                        //listValues.Add(fields[0].Replace("\"", "").Replace("'", ""));
                        listValues.Add(fields[0].Replace("\"", ""));
                        listProbabilities.Add(fields[1]);
                    }
                }
            }

            // CONVERT
            string[] stringProbabilities = listProbabilities.ToArray();
            string[] stringValues = listValues.ToArray();

            double[] doubleProbabilities = new double[stringProbabilities.Length];
            char[] charValues = new char[stringValues.Length];

            for (int i = 0; i < stringProbabilities.Length; i++)
                doubleProbabilities[i] = double.Parse(stringProbabilities[i]);

            for (int i = 0; i < stringValues.Length; i++)
                charValues[i] = char.Parse(stringValues[i]);

            probabilities = doubleProbabilities;
            values = charValues;
        }


        //public static char[] GetCharArray(string inputFilePath)
        //{
        //    string str = "";
        //    List<char> char_arr = new List<char>();

        //    using (StreamReader sr = new StreamReader(inputFilePath, Encoding.Default))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            str = sr.ReadLine();

        //            if (str.IndexOf('"') == -1)
        //            {
        //                char_arr.Add(str.Split(';')[0][0]);
        //            }
        //            else
        //            {
        //                string sub_str = str.Substring(0, str.LastIndexOf(';'));
        //                int count_ignore = sub_str.Count(ch => ch.Equals('"'));
        //                int current_ci = 0;

        //                for (int i = 0; i < sub_str.Length; i++)
        //                {
        //                    if (sub_str[i] == '"')
        //                        current_ci++;
        //                    else
        //                    {
        //                        if (current_ci == 0 || current_ci == count_ignore ||
        //                            current_ci == count_ignore / 2 ||
        //                            (count_ignore % 2 == 1) && ((current_ci > count_ignore / 2) || (current_ci < count_ignore / 2 + 1)))
        //                        {
        //                            char_arr.Add(sub_str[i]);
        //                            continue;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return char_arr.ToArray();
        //}

        //public static double[] GetProbabilityArray(string inputFilePath)
        //{
        //    string str = "";
        //    List<string> string_list = new List<string>();

        //    using (StreamReader sr = new StreamReader(inputFilePath, Encoding.Default))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            str = sr.ReadLine();

        //            if (str.IndexOf('"') == -1)
        //            {
        //                string_list.Add(str.Split(';')[1]);
        //            }
        //            else
        //            {
        //                string sub_str = str.Substring(str.LastIndexOf(';') + 1, str.Length - str.LastIndexOf(';') - 1);
        //                string_list.Add(sub_str);
        //            }
        //        }
        //    }

        //    string[] string_array = string_list.ToArray();

        //    double[] double_array = new double[string_array.Length];
        //    for (int i = 0; i < string_array.Length; i++)
        //        double_array[i] = Convert.ToDouble(string_array[i]);

        //    return double_array;
        //}

    }
}
