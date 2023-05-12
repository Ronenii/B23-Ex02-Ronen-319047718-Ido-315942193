using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Ex2
{
    public enum eCellType
    {
        Empty,
        Cross,
        Circle
    }

    public enum eRowColumn
    {
        Row,
        Column
    }

    public class Board
    {
        private int m_BoardSize;
        private int m_turn;
        private eCellType[,] m_BoardCells = null;
        private int[] m_NumOfCellsOccupiedInRows; // Each index of this array represents the number of occupied cells in the corresponding row index
        private int[] m_NumOfCellsOccupiedInColumns; // Each index of this array represents the number of occupied cells in the corresponding column index
        private int[] m_NumOfCellsOccupiedInDiagonals = new int[2]; // Index 0 of this array represents the occupied cells in the top-left to bottom right diagonal, index 1 represents the occupied cells in the other diagonal

        public Board(int i_BoardSize)
        {
            m_BoardCells = new eCellType[i_BoardSize, i_BoardSize];
            initBoard(i_BoardSize);
            m_NumOfCellsOccupiedInColumns = new int[i_BoardSize];
            m_NumOfCellsOccupiedInRows = new int[i_BoardSize];
            m_turn = 0;
            m_BoardSize = i_BoardSize;
        }


        public eCellType[,] BoardCells
        {
            get
            {
                return m_BoardCells;
            }
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
        public int Turn
        {
            get
            {
                return m_turn;
            }
            set
            {
                m_turn = value;
            }
        }

        // Updates the given cell to the given player's shape. 

        // TODO: Need to validate the given cell, we need to do this in the io class
        public void UpdateBoardCell(int i_Row, int i_Col, ePlayers i_Player)
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

            updateOccupationArrays(i_Row, i_Col);
        }

        // updated the occupation arrays initialized in the beginning.
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

        //Initilize board with empty cells
        public void initBoard(int i_BoardSize)
        {
            m_turn = i_BoardSize * i_BoardSize;
            for (int rowIndex = 0; rowIndex < i_BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < i_BoardSize; colIndex++)
                {
                    m_BoardCells[rowIndex, colIndex] = eCellType.Empty;
                }
            }
        }
    }
}
