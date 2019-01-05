using System;
using System.Collections.Generic;


namespace BinaryTreeProject.Core.Translation
{
    /*                  Класс, оссуществляющий кодирование заданного сообщения                  */

    public class Encoder
    {
        public static string Encode(string originalString, Dictionary<char, String> binaryDictionary)
        {
            string encodeString = "";

            for (int i = 0; i < originalString.Length; i++)
                encodeString += binaryDictionary[originalString[i]];

            return encodeString;
        }
    }
}
