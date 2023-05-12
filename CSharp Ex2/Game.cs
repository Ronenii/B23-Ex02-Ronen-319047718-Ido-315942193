using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Ex2
{
    public class Game
    {
        private Player m_currentPlayer;
        private bool m_quitGame;
        private int m_boardSize;
        private Board m_board;
        private Player m_firstPlayer = new Player(ePlayers.PlayerOne, 0, eMode.Human, eCellType.Cross);
        private Player m_secondPlayer = new Player(ePlayers.PlayerTwo, 0, eMode.Human, eCellType.Circle);

        public Game()
        {
            m_quitGame = false;
            m_boardSize = 0;
            m_currentPlayer = m_firstPlayer;
            m_board = null;
        }

        // Game loop
        public void RunGame()
        {
            m_boardSize = IO.getBoardSizeInput();
            m_secondPlayer.Mode = IO.getPlayingMode();
            gameManagementHandler();
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
                resetGame();
                while (!isGameEnded())
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    IO.printGameBoard(m_board);
                    playTurn();
                }
                //TODO: handle Finish Game with the next four lines
                Ex02.ConsoleUtils.Screen.Clear();
                IO.printGameBoard(m_board);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        // Player turn logic.
        private void playTurn()
        {
            int row;
            int column;
            IO.PrintPlayerTurnPrompt(m_currentPlayer);
            do
            {
                PointIndex point = IO.GetHumanPointIndex(m_board, m_currentPlayer);
                row = point.Row;
                column = point.Column;
            }
            while (!isMoveValid(row, column, m_board));

            m_board.BoardCells[row - 1, column - 1] = m_currentPlayer.CellType;
            m_board.Turn--;

            changePlayer();

        }

        // Returns true or false based on if the game ended.
        private bool isGameEnded()
        {
            bool gameEnded = false;
            if (m_quitGame == true)
            {
                gameEnded = true;
            }
            if (isPlayerWon())
            {
                gameEnded = true;
            }
            if (m_board.Turn == 0)
            {
                gameEnded = true;
            }
            return gameEnded;
        }

        private bool isPlayerWon()
        {
            return false;
        }

        // Restes the game board and keeps the score as is
        private void resetGame()
        {
            m_board.initBoard(m_boardSize);
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
        private bool isMoveValid(int i_row, int i_col, Board i_Board)
        {
            bool moveValidation = true;
            string errorMessage = string.Empty;
            if (i_col > m_boardSize || i_row > m_boardSize || i_row < 1 || i_col < 1)
            {
                moveValidation = false;
                errorMessage = "Cell out of bounds.";
            }
            else if (!i_Board.BoardCells[i_row - 1, i_col - 1].Equals(eCellType.Empty))
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
