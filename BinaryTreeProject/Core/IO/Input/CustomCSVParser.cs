using System.Collections.Generic;
using System.Text;
using System.IO;
using System;


namespace BinaryTreeProject.Core.Utils
{
    /*                                          Парсер CSV-файла                                        */

    public class CustomCSVParser
    {
        
        //  Список символов и их вероятностей, полученный из файла
        private Dictionary<char, double> probabilityDictionary;

        public Dictionary<char, double> ProbabilityDictionary { get{ return probabilityDictionary; } }


        public CustomCSVParser(string inputFilePath, char separator)
        {
            List<double> listProbabilities = new List<double>();
            List<char> listValues = new List<char>();

            using (StreamReader sr = new StreamReader(inputFilePath, Encoding.Default))
            {
                int counter = 1;

                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    string[] arr;

                    //  Замена символа разделителя, перед разделением значений
                    if (str.IndexOf("\"" + separator + "\"") != -1)
                    {
                        str = str.Replace("\"" + separator + "\"", "@@@@@@@@@@"); // use ten dogs :)
                        arr = str.Split(separator);
                        if (arr.Length == 2)
                        {
                            arr[0] = arr[0].Replace("@@@@@@@@@@", "" + separator); // use ten dogs :)
                            arr[1] = arr[1].Replace("@@@@@@@@@@", "" + separator); // use ten dogs :)
                        }
                        
                    }
                    else
                        arr = str.Split(separator);

                    // CHECKING
                    if (arr.Length != 2)
                        throw new Exception("CSV файл должен содержать 2 колонки!");
                    else
                    {
                        if (arr[0] == "")
                            throw new Exception($"В {counter} строке не указан символ.\n" +
                                $"|{arr[0]}| |{arr[1]}|");

                        if (arr[1] == "")
                            throw new Exception($"В {counter} строке не указана вероятность.\n" +
                                $"|{arr[0]}| |{arr[1]}|");


                        // Excel features
                        if (arr[0] == "\"\"\" \"\"\"") arr[0] = "\"";
                        if (arr[0] == "\"" + separator + "\"") arr[0] = "" + separator; 
                        //

                        //  Если использовано больше одного символа - вылетит ошибка
                        listValues.Add(Char.Parse(arr[0]));
                        listProbabilities.Add(Double.Parse(arr[1]));
                    }

                    counter++;
                }

            }

            char[] values = listValues.ToArray();
            double[] probabilities = listProbabilities.ToArray();
            probabilityDictionary = new Dictionary<char, double>();

            //   values.Length == probabilities.Length
            for (int i = 0; i < values.Length; i++)
                probabilityDictionary.Add(values[i], probabilities[i]);
        }

    }
}
