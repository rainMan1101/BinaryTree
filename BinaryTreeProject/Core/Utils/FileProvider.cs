using System;
using System.Collections.Generic;
using BinaryTreeProject.App.Enums;


namespace BinaryTreeProject.Core.Utils
{
    public class FileProvider
    {
        private Dictionary<char, double> probabilites_map = null;
        
        public Dictionary<char, double> ProbabilitesMap { get { return probabilites_map; } }


        // Calculate values
        private double[] sortedProbabilities;

        private char[] sortedValues;

        private string inputFilePath;

        private string outputFilePath;

        private string outputDecode;

        IWritter writer;



        public FileProvider(string inputFilePath, string outputFilePath, string outputDecode, EOutputMode outPutMode)
        {
            this.inputFilePath = inputFilePath;
            this.outputFilePath = outputFilePath;

            //sortedValues = CustomCSVParser.GetCharArray(inputFilePath);
            //sortedProbabilities = CustomCSVParser.GetProbabilityArray(inputFilePath);

            this.outputDecode = outputDecode;
            RepleceOutputMode(outPutMode);

            CustomCSVParser paser = new CustomCSVParser(inputFilePath);
            sortedValues = paser.Values;
            sortedProbabilities = paser.Probabilities;

            // Exception при разной рамерности колонок символов и вероятностей из считанного CSV файла
            if (sortedValues.Length != sortedProbabilities.Length)
                throw new Exception(@"Ошибка считывания CSV-файла. Количество символов 
                    не совпадает с количеством вероятностей.");

            // Dictionary необходим для того, чтобы подтвердить, что символы не повторяются
            // ArgumentException - при повторении ключей.
            probabilites_map = new Dictionary<char, double>();
            for (int i = 0; i < sortedValues.Length; i++)
                probabilites_map.Add(sortedValues[i], sortedProbabilities[i]);
        }


        public void PrintCodes(Dictionary<char, string> binaryCodes, string[,] lastColumn, int countRows, int countColumns, ETreeType treeType)
        {
            //!!! Обрабатывать в модели или выдавать пользователю?
            if (binaryCodes == null)
                throw new Exception();

            if (lastColumn == null)
                throw new Exception();
            //!!!

            //!!!
            if (binaryCodes.Count == countRows)
            {
                //IWritter writer = new TXTWritter(outputFilePath);
                //IWritter writer = new CSVWritter(outputFilePath);
                writer.PrintResults(sortedValues, sortedProbabilities, binaryCodes, lastColumn, treeType);
            }
            else //!!!
                throw new Exception();
        }

        public void PrintDetailsDecoding(List<KeyValuePair<string, char>> list)
        {
            //IWritter writer = new TXTWritter(outputFile);
            //IWritter writer = new CSVWritter(outputFile);
            writer.PrintDetailsDecoding(list);
        }


        public void RepleceOutputMode(EOutputMode outPutMode)
        {
            if (outPutMode == EOutputMode.TXTMode)
                writer = new TXTWritter(outputFilePath, outputDecode);
            else
                writer = new CSVWritter(outputFilePath, outputDecode);
        }
    }
}
