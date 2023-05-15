namespace CSharp_Ex2
{
    public static class CellTypeUtils
    {
        // Convert the cellType object to string for print it
        public static string ToCustomShape(this eCellType i_cellType)
        {
            switch (i_cellType)
            {
                case eCellType.Empty:
                    return " ";
                case eCellType.Cross:
                    return "X";
                case eCellType.Circle:
                    return "O";
                default:
                    return " ";
            }
        }
    }
}
