using System.Drawing;

namespace BinaryTreeProject.Core.Additions
{
    /*                     Класс, хранящий информацию о шрифте                  */

    public class FontInfo
    {  
        public FontInfo(float realFontHeigth, float realFontWidth, Font font)
        {
            RealFontHeigth = realFontHeigth;
            RealFontWidth = realFontWidth;
            PrintFont = font;
        }


        //  Высота выводимой строки
        public float RealFontHeigth { get; }


        //  Ширина выводимой строки
        public float RealFontWidth { get; }


        //  Используемый шрифт
        public Font PrintFont { get; }
    }
}
