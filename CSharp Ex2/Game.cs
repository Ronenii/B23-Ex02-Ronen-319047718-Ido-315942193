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
        private Player m_firstPlayer = new Player(ePlayers.PlayerOne, 0, eMode.Human, eCellType.Cross);
        private Player m_secondPlayer = new Player(ePlayers.PlayerTwo, 0, eMode.Human, eCellType.Circle);

        public Game()
        {
            m_quitGame = false;
            m_boardSize = 0;
            m_currentPlayer = m_firstPlayer;
        }

        // Main game loop
        public void RunGame()
        {
            m_boardSize = IO.getBoardSizeInput();
            m_secondPlayer.Mode = IO.getPlayingMode();
            gameManagementHandler();
        }

        private void gameManagementHandler()
        {
            if (m_boardSize == -1)
            {
                m_quitGame = true;
            }
            Board board = new Board(m_boardSize);
            while (!m_quitGame)
            {
                while (!isGameEnded())
                {
                    playTurn(board);
                    Ex02.ConsoleUtils.Screen.Clear();
                    IO.printGameBoard(board);
                }
                resetGame(board);
            }
        }
        // Player turn logic.
        private void playTurn(Board i_board)
        {
            int row;
            int column;
            do
            {
                PointIndex point = IO.GetHumanPointIndex();
                row = point.Row;
                column = point.Column;
            }
            while (!isMoveValid(row, column, i_board));

            i_board.BoardCells[row, column] = m_currentPlayer.CellType;
            i_board.Turn--;

            changePlayer();

        }

        // Returns true or false based on if the game ended.
        private bool isGameEnded()
        {
            bool gameStatus = false;
            if (m_quitGame == true)
            {
                gameStatus = true;
            }
            if (isPlayerWon())
            {
                gameStatus = true;
            }
            return gameStatus;
        }

        private bool isPlayerWon()
        {
            return false;
        }

        // Restes the game board and keeps the score as is
        private void resetGame(Board i_board)
        {
            i_board.initBoard(m_boardSize);
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
        private bool isMoveValid(int i_row, int i_col, Board board)
        {
            bool moveValidation = true;
            if (i_col >= m_boardSize || i_row >= m_boardSize || i_row < 0 || i_col < 0)
            {
                moveValidation = false;
            }
            else if (!board.BoardCells[i_row, i_col].Equals(eCellType.Empty))
            {
                moveValidation = false;
            }
            if (moveValidation == false)
            {
                Console.WriteLine("The choosen cell is invalid!");
            }
            return moveValidation;
        }
    }
}
