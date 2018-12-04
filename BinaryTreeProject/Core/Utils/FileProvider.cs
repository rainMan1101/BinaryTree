using System;
using System.Collections.Generic;


namespace BinaryTreeProject.Core.Utils
{
    /*
     * TODO:
     * 
     * Rewrite constructor
     * Make it :
     *      FileProvider(string inputFilePath, string outputFilePath, string outputFilePathDekode)
     * 
     */


    class FileProvider
    {
        private Dictionary<char, double> probabilites_map = null;
        
        public Dictionary<char, double> ProbabilitesMap { get { return probabilites_map; } }


        // Calculate values
        private double[] sortedProbabilities;

        private char[] sortedValues;

        private string inputFilePath;

        private string outputFilePath;


        // Set values 
        private string[,] lastColumn = null;

        private Dictionary<char, String> binary_codes = null;

        public Dictionary<char, String> Binary_codes { set { binary_codes = value; } }

        public string[,] LastColumn { set { lastColumn = value; } }


        // Getters
        public double[] Probabilities { get { return sortedProbabilities; } }

        public char[] Values { get { return sortedValues; } }



        public FileProvider(string inputFilePath, string outputFilePath)
        {
            this.inputFilePath = inputFilePath;
            this.outputFilePath = outputFilePath;

            sortedValues = CustomCSVParser.GetCharArray(inputFilePath);
            sortedProbabilities = CustomCSVParser.GetProbabilityArray(inputFilePath);
            //Array.Sort(sortedProbabilities, sortedValues, new CustomComparer());


            /* TODO: Rewrite this crap */
            probabilites_map = new Dictionary<char, double>();
            for (int i = 0; i < sortedValues.Length; i++)
                probabilites_map.Add(sortedValues[i], sortedProbabilities[i]);

            //Array.Sort(probabilites_map.Values.ToArray(), probabilites_map.Keys.ToArray(), new CustomComparer());
        }


        public void PrintCodes()
        {
            if (binary_codes != null)
            {
                PrettyOutputFile prettyOutput = new PrettyOutputFile(outputFilePath);
                //prettyOutput.PrintResults(sortedValues, probabilites_map, binary_codes, stepsIndexes);
                //prettyOutput.PrintResults(probabilites_map, binary_codes, stepsIndexes);
                prettyOutput.PrintResults(sortedValues, sortedProbabilities, binary_codes, lastColumn);
            }
        }


    }
}
