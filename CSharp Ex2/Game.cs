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
        private static ePlayers currentPlayer = ePlayers.PlayerOne;
        private static Board gameBoard;
        private static int player1Score = 0,  player2Score = 0;
        private static bool quitGame = false;

        // Main game loop
        public static void RunGame()
        {
            initGame();
            while (!quitGame)
            {
                while (!gameEnded() && !quitGame)
                {
                    playTurn();
                }
                resetGame();
            }
        }


        // Initializes the board and everything else needed for the game
        private static void initGame()
        {
            
        }

        // Player turn logic.
        private static void playTurn()
        {
            // TODO: Prompt player to make a move
            // TODO: Check player's move
            // TODO: update the game board
            changePlayer();
        }

        // Returns true or false based on if the game ended.
        private static bool gameEnded()
        { 
            
            return false;
        }

        // Restes the game board and keeps the score as is
        private static void resetGame()
        {
            // TODO: Clear the screen and start a new game, player score doesn't reset
        }

        // Changes the current player to the other player
        private static void changePlayer()
        {
            if (currentPlayer.Equals(ePlayers.PlayerOne))
            {
                currentPlayer = ePlayers.PlayerTwo;
            }
            else
            {
                currentPlayer = ePlayers.PlayerOne;
            }
        }
    }
}
