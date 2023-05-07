using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Ex2
{
    internal enum eCellType
    {
        Empty,
        Cross,
        Circle
    }

    public class Board
    {
        private int m_BoardSize;
        private eCellType[,] m_BoardCells = null;

        public Board(int i_BoardSize)
        {
            m_BoardCells = new eCellType[i_BoardSize, i_BoardSize];
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        // Updates the given cell to the given player's shape. 

        // TODO: Need to validate the given cell, still don't know if we need to do it here or in the IO class
        public void UpdateBoardCell(ushort i_Row, int i_Col, ePlayers i_Player)
        {
            switch (i_Player)
            {
                case ePlayers.PlayerOne:
                    m_BoardCells[i_Row, i_Col] = eCellType.Cross;
                    break;
                case ePlayers.PlayerTwo:
                    m_BoardCells[i_Row, i_Col] = eCellType.Circle;
                    break;
            }
        }
    }
}
