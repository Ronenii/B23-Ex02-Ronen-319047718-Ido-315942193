using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharp_Ex2
{
    public class Game
    {
        private const string gameQuitMessage = "Quitting";
        private const string boardFullMessage = "Board full. No Winners.";

        private Player m_currentPlayer;
        private bool m_quitGame, m_gameEnded;
        private int m_boardSize;
        private Board m_board;
        private Player m_firstPlayer = new Player(ePlayers.PlayerOne, 0, eMode.Human, eCellType.Cross);
        private Player m_secondPlayer = new Player(ePlayers.PlayerTwo, 0, eMode.Human, eCellType.Circle);
        private string m_EndingMessage = string.Empty;
        public Game()
        {
            m_quitGame = false;
            m_gameEnded = false;
            m_boardSize = 0;
            m_currentPlayer = m_firstPlayer;
            m_board = null;
        }

        // Game loop
        public void RunGame()
        {
            m_boardSize = IO.getBoardSizeInput();
            if (m_boardSize == -1)
            {
                m_EndingMessage = gameQuitMessage;
            }
            else
            {
                m_secondPlayer.Mode = IO.getPlayingMode();
                if (m_secondPlayer.Mode == eMode.Exit)
                {
                    m_EndingMessage = gameQuitMessage;
                }
                else
                {
                    gameManagementHandler();
                }
            }
        }

        // The handler of the main game loop
        private void gameManagementHandler()
        {
            if (m_boardSize == -1)
            {
                m_quitGame = true;
            }
            m_board = new Board(m_boardSize);
            while (!m_quitGame)
            {
                while (!isGameEnded())
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    IO.printGameBoard(m_board);
                    playTurn();
                }

                Ex02.ConsoleUtils.Screen.Clear();
                IO.printGameBoard(m_board);
                IO.printGameEndedMessage(m_EndingMessage);
                IO.PrintScore(m_firstPlayer,m_secondPlayer);
                resetGame();
            }


        }

        // Player turn logic.
        // 1) Player chooses where to put his marker.
        // 2) Validates said marker and prompt the player to input again if marker is invalid.
        // 3) Updates the board and checks if the player lost.
        // 4) changes to the next player
        private void playTurn()
        {
            int row;
            int column;
            IO.PrintPlayerTurnPrompt(m_currentPlayer);
            do
            {
                PointIndex point = IO.GetHumanPointIndex(m_board, m_currentPlayer);
                row = point.Row - 1;
                column = point.Column - 1;
            }
            while (!isMoveValid(row, column, m_board));

            if (row != -1 && column != -1)
            {
                updateBoard(row, column);
                changePlayer();
            }
            else
            {
                m_quitGame = true;
            }

        }

        // Updates the board and ends the game if the player lost
        private void updateBoard(int i_Row, int i_Column)
        {
            m_board.UpdateBoardCell(i_Row, i_Column, m_currentPlayer.PlayerId);
            m_gameEnded = isPlayerLost(i_Row, i_Column);

            if (m_gameEnded)
            {
                changePlayer();
                m_currentPlayer.Score++;
                m_EndingMessage = string.Format("{0} Won!", m_currentPlayer.ToString());
            }
        }

        // The algorithm responsible for checking if the player that did the current move lost.
        // Checks if a given Row/Column/Diagonal is full using the bucket arrays in Board.
        // If they are then checks if the given Row/Column/Diagonal is the same shape.
        // If so then returns true, false otherwise.
        private bool isPlayerLost(int i_Row, int i_Column)
        {
            bool rowSameShpe = false, columnSameShape = false, topLeftBottomRightDiagonalSameShape = false, BottomLeftTopRightDiagonalSameShape = false;

            if (m_board.OccupiedCellsInColumnBucket[i_Column] == m_boardSize)
            {
                columnSameShape = isColumnSameShape(i_Column);
            }

            if (m_board.OccupiedCellsInRowBucket[i_Row] == m_boardSize)
            {
                rowSameShpe = isRowSameShape(i_Row);
            }

            if ((i_Row) == (i_Column)) // Check if the given point is on the top left to bottom right diagonal 
            {
                if (m_board.OccupiedCellsInDiagonalBucketBucket[0] == m_boardSize)
                {
                    topLeftBottomRightDiagonalSameShape = isDiagonalSameShape(eDiagonal.TopLeftToBottomRight);
                }
            }

            if (i_Row == (m_boardSize - i_Column - 1)) // Check if the given point is on the bottom left to top right diagonal
            {
                if (m_board.OccupiedCellsInDiagonalBucketBucket[1] == m_boardSize)
                {
                    BottomLeftTopRightDiagonalSameShape = isDiagonalSameShape(eDiagonal.BottomLeftToTopRight);
                }
            }

            return rowSameShpe || columnSameShape || topLeftBottomRightDiagonalSameShape || BottomLeftTopRightDiagonalSameShape;
        }

        // Return true if the given row is the same shape
        private bool isRowSameShape(int i_Row)
        {
            bool rowSameShape = true;
            for (int i = 0; i < m_boardSize - 1; i++)
            {
                if (m_board.BoardCells[i_Row, i] != m_board.BoardCells[i_Row, i + 1])
                {
                    rowSameShape = false;
                }
            }

            return rowSameShape;
        }

        // Returns true the given column is the same shape
        private bool isColumnSameShape(int i_Column)
        {
            bool columnSameShape = true;
            for (int i = 0; i < m_boardSize - 1; i++)
            {
                if (m_board.BoardCells[i, i_Column] != m_board.BoardCells[i + 1, i_Column])
                {
                    columnSameShape = false;
                }
            }

            return columnSameShape;
        }

        // Return true if the given diagonal is the same shape
        private bool isDiagonalSameShape(eDiagonal Diagonal)
        {
            bool diagonalSameShape = true;

            switch (Diagonal)
            {
                case eDiagonal.TopLeftToBottomRight:
                    for (int i = 0; i < m_boardSize - 1; i++)
                    {
                        if (m_board.BoardCells[i, i] != m_board.BoardCells[i + 1, i + 1])
                        {
                            diagonalSameShape = false;
                        }
                    }
                    break;
                case eDiagonal.BottomLeftToTopRight:
                    for (int i = 0; i < m_boardSize - 1; i++)
                    {
                        if (m_board.BoardCells[m_boardSize - i - 1, i] != m_board.BoardCells[m_boardSize - i - 2, i + 1])
                        {
                            diagonalSameShape = false;
                        }
                    }
                    break;
            }

            return diagonalSameShape;
        }

        // Returns true or false based on if the game ended.
        private bool isGameEnded()
        {
            if (m_quitGame == true)
            {
                m_gameEnded = true;
                m_EndingMessage = gameQuitMessage;
            }
            if (m_board.TurnsLeft == 0 && !m_gameEnded) //If there are no moves and the game didn't end because someone won
            {
                m_gameEnded = true;
                m_EndingMessage = boardFullMessage;
            }
            return m_gameEnded;
        }

        // Restes the game board and keeps the score as is
        private void resetGame()
        {
            m_board.resetBoard();
            m_gameEnded = false;
            m_currentPlayer = m_firstPlayer;
        }

        // Changes the current player to the other player
        private void changePlayer()
        {
            if (m_currentPlayer.PlayerId.Equals(ePlayers.PlayerOne))
            {
                m_currentPlayer = m_secondPlayer;
            }
            else
            {
                m_currentPlayer = m_firstPlayer;
            }
        }

        // Checks that the move is within board bounds and isn't played on a taken cell.
        private bool isMoveValid(int i_Row, int i_Column, Board i_Board)
        {
            bool moveValidation = true;
            string errorMessage = string.Empty;
            if (i_Row == -1 && i_Column == -1)
            {
                m_EndingMessage = gameQuitMessage;
            }
            else if (i_Column >= m_boardSize || i_Row >= m_boardSize || i_Row < 0 || i_Column < 0)
            {
                moveValidation = false;
                errorMessage = "Cell out of bounds.";
            }
            else if (!i_Board.BoardCells[i_Row, i_Column].Equals(eCellType.Empty))
            {
                moveValidation = false;
                errorMessage = "Cell is occupied";
            }
            if (moveValidation == false)
            {
                IO.PrintBoardWithErrors(i_Board, m_currentPlayer, errorMessage);
            }
            return moveValidation;
        }
    }
}
