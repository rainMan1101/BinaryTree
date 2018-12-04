using System;
using System.Drawing;


namespace BinaryTreeProject.Core.Trees.VisualTrees.Additions
{
    public class FontInfo
    {
        public FontInfo(string printString, float realFontHeigth, float realFontWidth, Font font)
        {
            RealFontHeigth = realFontHeigth;
            RealFontWidth = realFontWidth;
            PrintFont = font;
            PrintString = printString;
        }

        public float RealFontHeigth { get; }
        public float RealFontWidth { get; }
        public Font PrintFont { get; }
        public string PrintString { get; }
    }



    public class AdjustmentFontSize
    {
        private const int DEFAULT_FONT_SIZE = 7;

        private static FontFamily DEFAULT_FONT_FAMILY = FontFamily.GenericSansSerif;



        public static FontInfo GetFontSize(float wishfulHeigth, double probability)
        {
            // ???           
            string str = String.Format("{0:0.00}", probability);
            return GetFontSize(wishfulHeigth, str);
        }


        public static FontInfo GetFontSize(float wishfulHeigth, char symbol)
        {
            return GetFontSize(wishfulHeigth, "" + symbol);
        }


        private static FontInfo GetFontSize(float wishfulHeigth, string str)
        {
            int index = DEFAULT_FONT_SIZE;
            Font lastFont = new Font(DEFAULT_FONT_FAMILY, index);
            Graphics graph = Graphics.FromImage(new Bitmap(100, 100));


            if (wishfulHeigth > 0 && wishfulHeigth < 100)
            {
                for (index = 1; index < 100; index++)
                {
                    Font font = new Font(DEFAULT_FONT_FAMILY, index);

                    float current = graph.MeasureString(str, font).Height;
                    float last = graph.MeasureString(str, lastFont).Height;

                    // Условие выхода - найден наиболее подходящий по размеру шрифт
                    if (Math.Abs(wishfulHeigth - current) > Math.Abs(wishfulHeigth - last)) break;

                    lastFont = font;
                }


                if (index == 100)
                {
                    index = DEFAULT_FONT_SIZE;
                    lastFont = new Font(DEFAULT_FONT_FAMILY, DEFAULT_FONT_SIZE);
                }
            }


            float height = graph.MeasureString(str, lastFont).Height;
            float width = graph.MeasureString(str, lastFont).Width;

            return new FontInfo(str, height, width, lastFont);
        }


    }
}
