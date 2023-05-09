using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Ex2
{
    public enum ePlayers
    {
        PlayerOne,
        PlayerTwo
    }

    public class Game
    {
        private ePlayers m_currentPlayer;
        private int m_player1Score;
        private int m_player2Score;
        private bool m_quitGame;
        private int m_boardSize;

        public Game()
        {
            m_currentPlayer = ePlayers.PlayerOne;
            m_player1Score = 0;
            m_player2Score = 0;
            m_quitGame = false;
            m_boardSize = 0;
        }

        // Main game loop
        public void RunGame()
        {
            m_boardSize = IO.getBoardSizeInput();
            gameManagementHandler();
        }

        private void gameManagementHandler()
        {
            if (m_boardSize == -1)
            {
                m_quitGame = true;
            }
            while (!m_quitGame)
            {
                while (!isGameEnded())
                {
                    playTurn();
                }
                resetGame();
            }
        }
        // Player turn logic.
        private void playTurn()
        {
            // TODO: Prompt player to make a move
            // TODO: Check player's move
            // TODO: update the game board
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
            throw new NotImplementedException();
        }

        // Restes the game board and keeps the score as is
        private static void resetGame()
        {
            // TODO: Clear the screen and start a new game, player score doesn't reset
        }

        // Changes the current player to the other player
        private void changePlayer()
        {
            if (m_currentPlayer.Equals(ePlayers.PlayerOne))
            {
                m_currentPlayer = ePlayers.PlayerTwo;
            }
            else
            {
                m_currentPlayer = ePlayers.PlayerOne;
            }
        }
    }
}
