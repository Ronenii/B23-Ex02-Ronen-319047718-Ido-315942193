using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Ex2
{
    public static class CellTypeUtils
    {

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
