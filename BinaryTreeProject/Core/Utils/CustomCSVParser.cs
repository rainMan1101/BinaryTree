using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace BinaryTreeProject.Core.Utils
{
    public class CustomCSVParser
    {


        public static char[] GetCharArray(string inputFilePath)
        {
            string str = "";
            List<char> char_arr = new List<char>();

            using (StreamReader sr = new StreamReader(inputFilePath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine();

                    if (str.IndexOf('"') == -1)
                    {
                        char_arr.Add(str.Split(';')[0][0]);
                    }
                    else
                    {
                        string sub_str = str.Substring(0, str.LastIndexOf(';'));
                        int count_ignore = sub_str.Count(ch => ch.Equals('"'));
                        int current_ci = 0;

                        for (int i = 0; i < sub_str.Length; i++)
                        {
                            if (sub_str[i] == '"')
                                current_ci++;
                            else
                            {
                                if (current_ci == 0 || current_ci == count_ignore ||
                                    current_ci == count_ignore / 2 ||
                                    (count_ignore % 2 == 1) && ((current_ci > count_ignore / 2) || (current_ci < count_ignore / 2 + 1)))
                                {
                                    char_arr.Add(sub_str[i]);
                                    continue;
                                }
                            }
                        }
                    }
                }
            }

            return char_arr.ToArray();
        }

        public static double[] GetProbabilityArray(string inputFilePath)
        {
            string str = "";
            List<string> string_list = new List<string>();

            using (StreamReader sr = new StreamReader(inputFilePath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine();

                    if (str.IndexOf('"') == -1)
                    {
                        string_list.Add(str.Split(';')[1]);
                    }
                    else
                    {
                        string sub_str = str.Substring(str.LastIndexOf(';') + 1, str.Length - str.LastIndexOf(';') - 1);
                        string_list.Add(sub_str);
                    }
                }
            }

            string[] string_array = string_list.ToArray();

            double[] double_array = new double[string_array.Length];
            for (int i = 0; i < string_array.Length; i++)
                double_array[i] = Convert.ToDouble(string_array[i]);

            return double_array;
        }
    }
}
