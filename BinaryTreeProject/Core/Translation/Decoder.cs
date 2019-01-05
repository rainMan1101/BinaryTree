using System.Collections.Generic;
using System.Linq;


namespace BinaryTreeProject.Core.Translation
{
    /*                  Класс, оссуществляющий декодирование заданного сообщения                  */

    public class Decoder
    {
        
        //  Декодированная стока (исходное сообщение, состоящее из символов алфавита)
        private static string decodeStr;

        public static string DecodeString { get { return decodeStr; } }


        //  Метод, возвращающий список с результатом декодирования (кодовая комбинация и  соответствущий ей символ)
        //  и сохраняющий декодированную строку в decodeStr.
        public static List<KeyValuePair<string, char>> Decode(string binaryString, 
            Dictionary<char, string> binaryDictionary)
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

    }
}
