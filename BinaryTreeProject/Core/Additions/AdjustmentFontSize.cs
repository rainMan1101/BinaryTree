using System;
using System.Drawing;


namespace BinaryTreeProject.Core.Additions
{
    /*                       Подгоняемый по высоте размер шрифта                  */

    public class AdjustmentFontSize
    {
        //  Размер шрифта, используемый по-умолчанию
        private const int DEFAULT_FONT_SIZE = 7;


        //  Название шрифта, используемого по-умолчанию
        private static FontFamily DEFAULT_FONT_FAMILY = FontFamily.GenericSansSerif;


        //  Возвращает информацию о размере строки и шрифта, необходимого для вывода строки с вероятностью
        public static FontInfo GetFontSizeProbability(float wishfulHeigth)
        {
            return GetFontSize(wishfulHeigth, String.Format("{0:0.00}", 1.23));
        }


        //  Возвращает информацию о размере строки и шрифта, необходимого для вывода 1 символа
        public static FontInfo GetFontSizeChar(float wishfulHeigth)
        {
            return GetFontSize(wishfulHeigth, "0");
        }


        //  Информация о размере строки, для заданной высоты 
        private static FontInfo GetFontSize(float wishfulHeigth, string str)
        {
            Font lastFont = new Font(DEFAULT_FONT_FAMILY, DEFAULT_FONT_SIZE);
            Graphics graph = Graphics.FromImage(new Bitmap(100, 100));


            if (wishfulHeigth > 0 && wishfulHeigth < 100)
            {
                int index = DEFAULT_FONT_SIZE;
                for (index = 1; index < 100; index++)
                {
                    //  
                    Font font = new Font(DEFAULT_FONT_FAMILY, index);

                    float current = graph.MeasureString(str, font).Height;
                    float last = graph.MeasureString(str, lastFont).Height;

                    // Условие выхода - найден наиболее подходящий по размеру шрифт
                    if (Math.Abs(wishfulHeigth - current) > Math.Abs(wishfulHeigth - last)) break;

                    lastFont = font;
                }

                //  Желаемый шрифт так и не был подобран - установка размера по-умолчанию
                if (index == 100)
                {
                    index = DEFAULT_FONT_SIZE;
                    lastFont = new Font(DEFAULT_FONT_FAMILY, DEFAULT_FONT_SIZE);
                }
            }

            //  Получение высоты и ширины строки с использованием данного шрифта
            float height = graph.MeasureString(str, lastFont).Height;
            float width = graph.MeasureString(str, lastFont).Width;

            return new FontInfo(height, width, lastFont);
        }


    }
}
