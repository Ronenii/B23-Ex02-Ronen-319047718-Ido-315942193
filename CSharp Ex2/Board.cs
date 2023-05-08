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
        private int[] m_NumOfCellsOccupiedInRows; // Each index of this array represents the number of occupied cells in the corresponding row index
        private int[] m_NumOfCellsOccupiedInColumns; // Each index of this array represents the number of occupied cells in the corresponding column index
        private int[] m_NumOfCellsOccupiedInDiagonals = new int[2]; // Index 0 of this array represents the occupied cells in the top-left to bottom right diagonal, index 1 represents the occupied cells in the other diagonal

        public Board(int i_BoardSize)
        {
            m_BoardCells = new eCellType[i_BoardSize, i_BoardSize];
            m_NumOfCellsOccupiedInColumns = new int[i_BoardSize];
            m_NumOfCellsOccupiedInRows = new int[i_BoardSize];
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

        // TODO: Need to validate the given cell, we need to do this in the io class
        public void UpdateBoardCell(int i_Row, int i_Col, ePlayers i_Player)
        {
            switch(i_Player)
            {
                case ePlayers.PlayerOne:
                    m_BoardCells[i_Row, i_Col] = eCellType.Cross;
                    break;
                case ePlayers.PlayerTwo:
                    m_BoardCells[i_Row, i_Col] = eCellType.Circle;
                    break;
            }

            updateOccupationArrays(i_Row,i_Col);
        }

        private void updateOccupationArrays(int i_Row, int i_Col)
        {
            m_NumOfCellsOccupiedInRows[i_Row]++;
            m_NumOfCellsOccupiedInColumns[i_Col]++;

            // Check if cell is on diagonal 0
            if (i_Row == i_Col)
            {
                m_NumOfCellsOccupiedInDiagonals[0]++;
            }

            // Check if cell is on diagonal 1
            if (i_Row == (m_BoardSize - i_Col - 1))
            {
                m_NumOfCellsOccupiedInDiagonals[1]++;
            }

        }
    }
}
