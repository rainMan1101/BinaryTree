using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;


namespace BinaryTreeProject.Core.IO.Input
{
    /*                           Анализатор текста            
     *                                                                    
     *       Считывает текстовый файл и определяет веротность вхождения в текст
     *    каждого конкретного символа.
     */

    public class TextAnalyzer : IReader
    {
        private Dictionary<char, double> probabilityDictionary;

        public Dictionary<char, double> ProbabilityDictionary { get { return probabilityDictionary; } }


        public TextAnalyzer(string inputFilePath)
        {
            probabilityDictionary = new Dictionary<char, double>();
            int counter = 0;

            using (StreamReader sr = new StreamReader(inputFilePath, Encoding.Default))
            {
                //  Построчное считывание файла
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();

                    //  Проход по строке
                    for (int i = 0; i < str.Length; i++)
                    {
                        counter++;
                        if (!probabilityDictionary.ContainsKey(str[i]))
                            probabilityDictionary.Add(str[i], 1.0);
                        else
                            probabilityDictionary[str[i]] = probabilityDictionary[str[i]] + 1.0;
                    }
                }
            }


            //  Вычисление вероятностей
            if (probabilityDictionary.Count != 0)
            {
                char[] keys = probabilityDictionary.Keys.ToArray();

                for (int i = 0; i < probabilityDictionary.Count; i++)
                    probabilityDictionary[keys[i]] = probabilityDictionary[keys[i]] / counter;
            }

        }



    }
}
