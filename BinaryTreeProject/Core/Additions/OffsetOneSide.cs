namespace BinaryTreeProject.Core.Additions
{
    /*                  Смещение с одной стороны 
     *                  
     *  Определяет насколко нужно сместить координаты по X и Y, относительно
     *  центра фигуры, чтобы линяя выходила из края фигуры.              
     */
    public struct OffsetOneSide
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}
