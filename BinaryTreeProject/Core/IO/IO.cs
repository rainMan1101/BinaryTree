using System;
using System.Collections.Generic;
using BinaryTreeProject.App.Enums;
using BinaryTreeProject.Core.IO.Input;
using BinaryTreeProject.Core.IO.Output;
using BinaryTreeProject.Core.Utils;


namespace BinaryTreeProject.Core.IO
{
    /*                              Класс, реализующий ввод-вывод приложения                            */

    public class IO
    {
        
        //  Список символов и их вероятностей
        private Dictionary<char, double> probabilityDictionary;

        public Dictionary<char, double> ProbabilityDictionary { get { return probabilityDictionary; } }


        //  Файл, содержащий список символов и соответствующих им вероятностей
        private string inputFilePath;


        //  Файл, в который заносятся результаты работы программы
        private string outputFilePath;


        //  Файл, в который заносятся результаты (шаги) декодирования сообщения
        private string outputDecodeFilePath;


        //  Экземпляр писателя
        private IWriter writer;


        //  Экземпляр считывателя
        private IReader reader;


        public IO(string inputFilePath, string outputFilePath, string outputDecodeFilePath, 
            EOutputMode outPutMode, char CSVseparator, bool TextFile)
        {
            this.inputFilePath = inputFilePath;
            this.outputFilePath = outputFilePath;
            this.outputDecodeFilePath = outputDecodeFilePath;

            SetOutputMode(outPutMode);

            if (TextFile)
                reader = new TextAnalyzer(inputFilePath);
            else
                reader = new CustomCSVParser(inputFilePath, CSVseparator);

            probabilityDictionary = reader.ProbabilityDictionary;
        }


        //  Вывод результатов работы программы
        public void PrintCodes(Dictionary<char, string> binaryCodes, LastTableColumnCreator lastTableColumn, ETreeType treeType)
        {
            if (lastTableColumn.LastColumnContent == null)
                throw new Exception("Непредвиденное исключение! Обратитесь к разработчику программы.");

            if (binaryCodes.Count == lastTableColumn.CountRows)
                writer.PrintResults(probabilityDictionary, binaryCodes, lastTableColumn.LastColumnContent, treeType);
            else
                throw new Exception("Непредвиденное исключение! Обратитесь к разработчику программы.");
        }


        //  Вывод шагов декодирования 
        public void PrintDetailsDecoding(List<KeyValuePair<string, char>> list)
        {
            writer.PrintDetailsDecoding(list);
        }


        //  Установка формата вывода
        private void SetOutputMode(EOutputMode outPutMode)
        {
            if (outPutMode == EOutputMode.TXTMode)
                writer = new TXTWriter(outputFilePath, outputDecodeFilePath);
            else
                writer = new CSVWriter(outputFilePath, outputDecodeFilePath);
        }
    }
}
