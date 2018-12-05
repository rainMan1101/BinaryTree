using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BinaryTreeProject.Core.Translation
{
    public class Decoder
    {
        private static string decodeStr;

        public static string DecodeString { get { return decodeStr; } }

        public static List<KeyValuePair<string, char>> 
        Decode(string binaryString, Dictionary<char, string> binaryDictionary)
        {
            string tempStr = "";
            decodeStr = "";

            var decodeDict = new List<KeyValuePair<string, char>>();

            for (int i = 0; i < binaryString.Length; i++)
            {
                tempStr += binaryString[i];
                if (tempStr.Length < 2) continue;

                char ch = binaryDictionary.Where(x => x.Value == tempStr).FirstOrDefault().Key;
                if (ch == default(char))
                    decodeDict.Add(new KeyValuePair<string, char>(tempStr, '-'));
                else
                {
                    decodeDict.Add(new KeyValuePair<string, char>(tempStr, ch));
                    decodeStr += ch;
                    tempStr = "";
                }
            }

            return decodeDict;
        }



        /*
                 public static string Decode(string binaryString, Dictionary<char, string> binaryDictionary, string outputFile)
        {
            string temp_str = "";
            //string decode_str = "";
            var decodeDict = new Dictionary<int, KeyValuePair<string, char>>();

            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                for (int i = 0; i < binaryString.Length; i++)
                {
                    temp_str += binaryString[i];
                    if (temp_str.Length < 2) continue;

                    char ch = binaryDictionary.Where(x => x.Value == temp_str).FirstOrDefault().Key;
                    if (ch == default(char))
                    {
                        //sw.WriteLine(i + " --- " + temp_str + " --- " + " - ");
                        decodeDict.Add(i, new KeyValuePair<string, char>(temp_str, '-'));
                        //continue;
                    }
                    else
                    {
                        decodeDict.Add(i, new KeyValuePair<string, char>(temp_str, ch));
                        //sw.WriteLine(i + " --- " + temp_str + " --- " + ch);
                        //decode_str += ch;
                        temp_str = "";
                    }
                }
            }

            //return decode_str;
        }
         
         
         */
    }
}
