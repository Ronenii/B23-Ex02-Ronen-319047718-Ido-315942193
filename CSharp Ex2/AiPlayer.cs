using System;

namespace CSharp_Ex2
{
    class AiPlayer
    {
        private Player m_aiPlayer;
        private int m_maxDepth;

        public AiPlayer(ePlayers i_PlayerId, int i_Score, eCellType i_CellType)
        {
            m_aiPlayer = new Player(i_PlayerId, i_Score, i_CellType);
        }

        public int Score
        {
            get
            {
                return m_aiPlayer.Score;
            }
            set
            {
                m_aiPlayer.Score = value;
            }
        }

        public ePlayers Id
        {
            get
            {
                return m_aiPlayer.PlayerId;
            }
            set
            {
                m_aiPlayer.PlayerId = value;
            }
        }

        public Player PlayerData
        {
            get
            {
                return m_aiPlayer;
            }
            set
            {
                m_aiPlayer = value;
            }
        }
        // Using minmax algorithm determines the best cell to take.
        public PointIndex PlayTurn(Board i_GameBoard)
        {
            PointIndex bestCell;
            int bestScore = int.MinValue;
            int bestRow = -1;
            int bestColumn = -1;
            int boardSize = i_GameBoard.BoardSize;
            m_maxDepth = i_GameBoard.BoardSize * 3;
            for (int row = 0; row < boardSize; row++)
            {
                for (int column = 0; column < boardSize; column++)
                {
                    if (i_GameBoard.BoardCells[row, column] == eCellType.Empty)
                    {
                        i_GameBoard.BoardCells[row, column] = m_aiPlayer.CellType;
                        int score = minmax(i_GameBoard, 0, false);
                        i_GameBoard.BoardCells[row, column] = eCellType.Empty;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestRow = row;
                            bestColumn = column;
                        }
                    }
                }
            }

            bestCell = new PointIndex(bestRow, bestColumn);
            return bestCell;
        }

        // Explores the game tree and possible board outcomes. Using DFS creates said tree.
        // Alternates between players Minimizing outcomes for the opponent and maximizing for the AI.
        // Keeps exploring until reaches Wo, loss or darw. It then backtracks and continues to spread results across the tree.
        // Finally it selects the result with the highest score as the optimal move.
        private int minmax(Board i_GameBoard, int i_Depth, bool i_IsMaximizing)
        {

            int score = evaluateBoard(i_GameBoard, i_Depth);
            if (i_Depth == m_maxDepth || IsGameOver(i_GameBoard))
            {
                return score;
            }

            if (i_IsMaximizing)
            {
                int bestScore = int.MinValue;
                int boardSize = i_GameBoard.BoardSize;

                for (int row = 0; row < boardSize; row++)
                {
                    for (int column = 0; column < boardSize; column++)
                    {
                        if (i_GameBoard.BoardCells[row, column] == eCellType.Empty)
                        {
                            i_GameBoard.BoardCells[row, column] = m_aiPlayer.CellType;
                            score = minmax(i_GameBoard, i_Depth + 1, false);
                            i_GameBoard.BoardCells[row, column] = eCellType.Empty;

                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                int boardSize = i_GameBoard.BoardSize;

                for (int row = 0; row < boardSize; row++)
                {
                    for (int column = 0; column < boardSize; column++)
                    {
                        if (i_GameBoard.BoardCells[row, column] == eCellType.Empty)
                        {
                            i_GameBoard.BoardCells[row, column] = m_aiPlayer.CellType;
                            score = minmax(i_GameBoard, i_Depth + 1, true);
                            i_GameBoard.BoardCells[row, column] = eCellType.Empty;

                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
        }

        // Assigns scores to board states. if the game is over the AI Lost (based on the depth).
        private int evaluateBoard(Board i_GameBoard, int i_Depth)
        {
            if (IsGameOver(i_GameBoard))
            {
                if (i_Depth % 2 == 0)
                {
                    return -10;
                }
                else
                {
                    return 10;
                }
            }
            else if (i_GameBoard.TurnsLeft == 0)
            {
                return 0;
            }
            else
            {
                return i_Depth; //We may need something else over here
            }
        }

        // Check for if any of the rows, diagonals, columns are full
        private bool IsGameOver(Board i_GameBoard)
        {
            int boardSize = i_GameBoard.BoardSize;
            bool columnComplete;
            bool rowComplete;
            bool topLeftBottomRightDiagonalComplete;
            bool bottomLeftTopRightDiagonalComplete;
            for (int row = 0; row < boardSize; row++)
            {
                rowComplete = i_GameBoard.IsRowSameShape(row);
                if (rowComplete)
                {
                    return true;
                }
            }

            for (int column = 0; column < boardSize; column++)
            {
                columnComplete = i_GameBoard.IsColumnSameShape(column);
                if (columnComplete)
                {
                    return true;
                }
            }

            topLeftBottomRightDiagonalComplete = i_GameBoard.IsDiagonalSameShape(eDiagonal.TopLeftToBottomRight);
            if (topLeftBottomRightDiagonalComplete)
            {
                return true;
            }

            bottomLeftTopRightDiagonalComplete = i_GameBoard.IsDiagonalSameShape(eDiagonal.BottomLeftToTopRight);
            if (bottomLeftTopRightDiagonalComplete)
            {
                return true;
            }

            return false;
        }
    }
}
