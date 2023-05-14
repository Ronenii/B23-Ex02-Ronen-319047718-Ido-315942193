﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Player m_firstPlayer = new Player(ePlayers.PlayerOne, 0, eCellType.Cross);
        private Player m_secondPlayer = null;
        private AiPlayer m_aiPlayer = null;
        private string m_EndingMessage = string.Empty;

        public enum eMode
        {
            Human,
            Computer,
            Exit
        }

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
                IO.PrintGameEndedMessage(m_EndingMessage);
            }
            else
            {
                eMode gameMode = IO.getPlayingMode();
                switch (gameMode)
                {
                    case eMode.Exit:
                        m_EndingMessage = gameQuitMessage;
                        IO.PrintGameEndedMessage(m_EndingMessage);
                        break;
                    case eMode.Human:
                        m_secondPlayer = new Player(ePlayers.PlayerTwo, 0, eCellType.Circle);
                        HumanGameManagementHandler();
                        break;
                    case eMode.Computer:
                        m_aiPlayer = new AiPlayer(ePlayers.PlayerTwo, 0, eCellType.Circle);
                        m_secondPlayer = m_aiPlayer.PlayerData;
                        AiGameManagementHandler();
                        break;
                }
            }
        }

        // The handler of the main game loop
        // 1) If the player entered q when prompted to get board size the game is terminated here.
        // 2) Player makes a move and it gets validated.
        // 3) The move is then updated in the board.
        // 4) And the player is changed at the end of the turn
        // 5) If the game ended then it will print out the board and reason (board full, win, quitting etc)
        // 6) If the player didn't quit then the game restarts.
        private void HumanGameManagementHandler()
        {
            if (m_boardSize == -1)
            {
                m_quitGame = true;
            }
            m_board = new Board(m_boardSize);
            while (!m_quitGame)
            {
                do
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    IO.printGameBoard(m_board, m_firstPlayer, m_secondPlayer);
                    PointIndex playerMove = getCurrentPlayerMove();

                    if (!m_quitGame)
                    {
                        updateBoardAndPlayers(playerMove.Row, playerMove.Column);
                        changePlayer();
                    }

                } while (!isGameEnded());

                Ex02.ConsoleUtils.Screen.Clear();
                IO.printGameBoard(m_board, m_firstPlayer, m_secondPlayer);
                IO.PrintGameEndedMessage(m_EndingMessage);
                resetGame();
            }
        }

        // 1) Prints the current game board and prompts the player to make a move.
        // 2) Receives Player position input and validates it (valid location and valid string)
        private PointIndex getCurrentPlayerMove()
        {
            PointIndex playerMove;

            do
            {
                playerMove = m_currentPlayer.playTurn(m_board, m_firstPlayer, m_secondPlayer);

                if (playerMove.IsQitting())
                {
                    m_quitGame = true;
                }
            } while (!isPlayerMoveValid(playerMove));

            return playerMove;
        }

        // Checks that the move is within board bounds and isn't played on a taken cell.
        private bool isPlayerMoveValid(PointIndex i_PlayerMove)
        {
            bool moveValidation = true;
            string errorMessage = string.Empty;
            int boardSize = m_board.BoardSize;
            int row = i_PlayerMove.Row;
            int column = i_PlayerMove.Column;

            if (i_PlayerMove.IsQitting())
            {
                // To avoid entering the other ifs with -1 in row and column indexes causing out of bounds error.
            }
            else if (!i_PlayerMove.IsInbounds(boardSize))
            {
                moveValidation = false;
                errorMessage = "Cell out of bounds.";
            }
            else if (!m_board.BoardCells[row, column].Equals(eCellType.Empty))
            {
                moveValidation = false;
                errorMessage = "Cell is occupied";
            }
            if (moveValidation == false)
            {
                IO.PrintBoardWithErrors(m_board, m_currentPlayer, errorMessage, m_firstPlayer, m_secondPlayer);
            }
            return moveValidation;
        }

        private void AiGameManagementHandler()
        {
            if (m_boardSize == -1)
            {
                m_quitGame = true;
            }

            PointIndex playerMove;
            m_board = new Board(m_boardSize);
            while (!m_quitGame)
            {
                do
                {
                    if (m_currentPlayer.PlayerId == m_aiPlayer.Id)
                    {
                        playerMove = m_aiPlayer.playTurn(m_board);
                    }
                    else
                    {
                        Ex02.ConsoleUtils.Screen.Clear();
                        IO.printGameBoard(m_board, m_firstPlayer, m_secondPlayer);
                        playerMove = getCurrentPlayerMove();
                    }

                    if (!m_quitGame)
                    {
                        updateBoardAndPlayers(playerMove.Row, playerMove.Column);
                        changePlayer();
                    }

                } while (!isGameEnded());

                Ex02.ConsoleUtils.Screen.Clear();
                IO.printGameBoard(m_board, m_firstPlayer, m_secondPlayer);
                IO.PrintGameEndedMessage(m_EndingMessage);
                if (!m_quitGame)
                {
                    resetGame();
                }
            }        
}

        // Updates the board and ends the game if the player lost. If player won then updates his score.
        private void updateBoardAndPlayers(int i_Row, int i_Column)
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
                columnSameShape = m_board.IsColumnSameShape(i_Column);
            }

            if (m_board.OccupiedCellsInRowBucket[i_Row] == m_boardSize)
            {
                rowSameShpe = m_board.IsRowSameShape(i_Row);
            }

            if ((i_Row) == (i_Column)) // Check if the given point is on the top left to bottom right diagonal 
            {
                if (m_board.OccupiedCellsInDiagonalBucketBucket[0] == m_boardSize)
                {
                    topLeftBottomRightDiagonalSameShape = m_board.IsDiagonalSameShape(eDiagonal.TopLeftToBottomRight);
                }
            }

            if (i_Row == (m_boardSize - i_Column - 1)) // Check if the given point is on the bottom left to top right diagonal
            {
                if (m_board.OccupiedCellsInDiagonalBucketBucket[1] == m_boardSize)
                {
                    BottomLeftTopRightDiagonalSameShape = m_board.IsDiagonalSameShape(eDiagonal.BottomLeftToTopRight);
                }
            }

            return rowSameShpe || columnSameShape || topLeftBottomRightDiagonalSameShape || BottomLeftTopRightDiagonalSameShape;
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
                IO.PrintBoardWithErrors(i_Board, m_currentPlayer, errorMessage, m_firstPlayer, m_secondPlayer);
            }
            return moveValidation;
        }
    }
}
