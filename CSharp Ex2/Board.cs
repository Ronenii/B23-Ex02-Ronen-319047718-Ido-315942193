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

    public enum eDiagonal
    {
        TopLeftToBottomRight = 0,
        BottomLeftToTopRight
    }

    public class Board
    {
        private int m_BoardSize;
        private int m_TurnsLeft;
        private eCellType[,] m_BoardCells = null;
        private int[] m_OccupiedCellsInRowBucket; // Each index of this array represents the number of occupied cells in the corresponding row index
        private int[] m_OccupiedCellsInColumnBucket; // Each index of this array represents the number of occupied cells in the corresponding column index
        private int[] m_OccupiedCellsInDiagonalBucket = new int[2]; // Index 0 of this array represents the occupied cells in the top-left to bottom right diagonal, index 1 represents the occupied cells in the other diagonal

        public Board(int i_BoardSize)
        {
            m_BoardCells = new eCellType[i_BoardSize, i_BoardSize];
            initBoard(i_BoardSize);
            m_OccupiedCellsInColumnBucket = new int[i_BoardSize];
            m_OccupiedCellsInRowBucket = new int[i_BoardSize];
            m_TurnsLeft = i_BoardSize * i_BoardSize;
            m_BoardSize = i_BoardSize;
        }


        public eCellType[,] BoardCells
        {
            get
            {
                return m_BoardCells;
            }
            set
            {
                m_BoardCells = value;
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
        public int TurnsLeft
        {
            get
            {
                return m_TurnsLeft;
            }
            set
            {
                m_TurnsLeft = value;
            }
        }

        public int[] OccupiedCellsInRowBucket
        {
            get
            {
                return m_OccupiedCellsInRowBucket;
            }
            set
            {
                m_OccupiedCellsInRowBucket = value;
            }
        }

        public int[] OccupiedCellsInColumnBucket
        {
            get
            {
                return m_OccupiedCellsInColumnBucket;
            }
            set
            {
                m_OccupiedCellsInColumnBucket = value;
            }
        }

        public int[] OccupiedCellsInDiagonalBucketBucket
        {
            get
            {
                return m_OccupiedCellsInDiagonalBucket;
            }
            set
            {
                m_OccupiedCellsInDiagonalBucket = value;
            }
        }

        // Updates the given cell to the given player's shape. 

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

            m_TurnsLeft--;
            updateOccupationArrays(i_Row, i_Col);
        }

        // updated the occupation arrays initialized in the beginning.
        private void updateOccupationArrays(int i_Row, int i_Col)
        {
            m_OccupiedCellsInRowBucket[i_Row]++;
            m_OccupiedCellsInColumnBucket[i_Col]++;

            // Check if cell is on diagonal 0
            if (i_Row == i_Col)
            {
                m_OccupiedCellsInDiagonalBucket[0]++;
            }

            // Check if cell is on diagonal 1
            if (i_Row == (m_BoardSize - i_Col - 1))
            {
                m_OccupiedCellsInDiagonalBucket[1]++;
            }

        }

        //Initilize board with empty cells
        private void initBoard(int i_BoardSize)
        {
            for (int rowIndex = 0; rowIndex < i_BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < i_BoardSize; colIndex++)
                {
                    m_BoardCells[rowIndex, colIndex] = eCellType.Empty;
                }
            }
        }

        public void resetBoard()
        {
            m_TurnsLeft = m_BoardSize * m_BoardSize;
            initBoard(m_BoardSize);
            Array.Clear(m_OccupiedCellsInColumnBucket,0,m_BoardSize);
            Array.Clear(m_OccupiedCellsInRowBucket,0,m_BoardSize);
            Array.Clear(m_OccupiedCellsInDiagonalBucket,0,2);

        }
    }
}
