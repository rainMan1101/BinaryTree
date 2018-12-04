using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BinaryTreeProject.Core.Translation
{
    public class Decoder
    {
        public static string Decode(string binaryString, Dictionary<char, String> binaryDictionary, string outputFile)
        {
            string temp_str = "";
            string decode_str = "";

            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                for (int i = 0; i < binaryString.Length; i++)
                {
                    temp_str += binaryString[i];
                    if (temp_str.Length < 2) continue;

                    char ch = binaryDictionary.Where(x => x.Value == temp_str).FirstOrDefault().Key;
                    if (ch == default(char))
                    {
                        sw.WriteLine(i + " --- " + temp_str + " --- " + " - ");
                        continue;
                    }
                    else
                    {
                        sw.WriteLine(i + " --- " + temp_str + " --- " + ch);
                        decode_str += ch;
                        temp_str = "";
                    }
                }
            }

            return decode_str;
        }
    }
}
