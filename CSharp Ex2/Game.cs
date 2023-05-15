namespace CSharp_Ex2
{
    public class Game
    {
        private const string k_GameQuitMessage = "Quitting";
        private const string k_BoardFullMessage = "Board full. No Winners.";
        private readonly Player r_FirstPlayer = new Player(ePlayers.PlayerOne, 0, eCellType.Cross);

        private Player m_CurrentPlayer;
        private bool m_QuitGame, m_GameEnded;
        private int m_BoardSize;
        private Board m_Board;
        private Player m_SecondPlayer = null;
        private AiPlayer m_AiPlayer = null;
        private string m_EndingMessage = string.Empty;

        public enum eMode
        {
            Human,
            Computer,
            Exit
        }

        public Game()
        {
            m_QuitGame = false;
            m_GameEnded = false;
            m_BoardSize = 0;
            m_CurrentPlayer = r_FirstPlayer;
            m_Board = null;
        }

        // Game loop
        public void RunGame()
        {
            m_BoardSize = IO.GetBoardSizeInput();
            if (isUserExit())
            {
                m_EndingMessage = k_GameQuitMessage;
                IO.PrintGameEndedMessage(m_EndingMessage);
            }
            else
            {
                eMode gameMode = IO.GetPlayingMode();
                switch (gameMode)
                {
                    case eMode.Exit:
                        m_EndingMessage = k_GameQuitMessage;
                        IO.PrintGameEndedMessage(m_EndingMessage);
                        break;
                    case eMode.Human:
                        m_SecondPlayer = new Player(ePlayers.PlayerTwo, 0, eCellType.Circle);
                        HumanGameManagementHandler();
                        break;
                    case eMode.Computer:
                        m_AiPlayer = new AiPlayer(ePlayers.PlayerTwo, 0, eCellType.Circle);
                        m_SecondPlayer = m_AiPlayer.PlayerData;
                        AiGameManagementHandler();
                        break;
                }
            }
        }
        //Validate user does not exit when create new board
        private bool isUserExit()
        {
            return m_BoardSize == -1;
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
            if (isUserExit())
            {
                m_QuitGame = true;
            }

            m_Board = new Board(m_BoardSize);
            while (!m_QuitGame)
            {
                do
                {
                    IO.PrintGameBoard(m_Board, r_FirstPlayer, m_SecondPlayer);
                    PointIndex playerMove = getCurrentPlayerMove();

                    if (!m_QuitGame)
                    {
                        updateBoardAndPlayers(playerMove);
                        changePlayer();
                    }

                }
                while (!isGameEnded());

                IO.PrintGameBoard(m_Board, r_FirstPlayer, m_SecondPlayer);
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
                playerMove = m_CurrentPlayer.PlayTurn(m_Board, r_FirstPlayer, m_SecondPlayer);

                if (playerMove.IsQuitting())
                {
                    m_QuitGame = true;
                }
            }
            while (!isPlayerMoveValid(playerMove));

            return playerMove;
        }

        // Checks that the move is within board bounds and isn't played on a taken cell.
        private bool isPlayerMoveValid(PointIndex i_PlayerMove)
        {
            bool moveValid = true;
            string errorMessage = string.Empty;
            int boardSize = m_Board.BoardSize;

            bool isQuitting = i_PlayerMove.IsQuitting();

            if (!isQuitting && !i_PlayerMove.IsInBounds(boardSize))
            {
                moveValid = false;
                errorMessage = "Cell out of bounds.";
            }
            else if (!isQuitting && !m_Board.IsCellEmpty(i_PlayerMove))
            {
                moveValid = false;
                errorMessage = "Cell is occupied";
            }

            if (moveValid == false)
            {
                IO.PrintBoardWithErrors(m_Board, m_CurrentPlayer, errorMessage, r_FirstPlayer, m_SecondPlayer);
            }

            return moveValid;
        }

        // Handle a game against AI computer
        private void AiGameManagementHandler()
        {
            if (isUserExit())
            {
                m_QuitGame = true;
            }

            PointIndex playerMove;
            m_Board = new Board(m_BoardSize);
            while (!m_QuitGame)
            {
                do
                {
                    if (m_CurrentPlayer.PlayerId == m_AiPlayer.Id)
                    {
                        playerMove = m_AiPlayer.PlayTurn(m_Board, m_BoardSize);
                    }
                    else
                    {
                        IO.PrintGameBoard(m_Board, r_FirstPlayer, m_SecondPlayer);
                        playerMove = getCurrentPlayerMove();
                    }

                    if (!m_QuitGame)
                    {
                        PointIndex pointIndex = new PointIndex(playerMove.Row, playerMove.Column);
                        updateBoardAndPlayers(pointIndex);
                        changePlayer();
                    }
                }
                while (!isGameEnded());

                IO.PrintGameBoard(m_Board, r_FirstPlayer, m_SecondPlayer);
                IO.PrintGameEndedMessage(m_EndingMessage);
                if (!m_QuitGame)
                {
                    resetGame();
                }
            }
        }

        // Updates the board and ends the game if the player lost. If player won then updates his score.
        private void updateBoardAndPlayers(PointIndex i_Cell)
        {
            m_Board.UpdateBoardCell(i_Cell, m_CurrentPlayer.PlayerId);
            m_GameEnded = isPlayerLost(i_Cell);

            if (m_GameEnded)
            {
                changePlayer();
                m_CurrentPlayer.Score++;
                m_EndingMessage = string.Format("{0} Won!", m_CurrentPlayer.ToString());
            }
        }

        // The algorithm responsible for checking if the player that did the current move lost.
        // Checks if a given Row/Column/Diagonal is full using the bucket arrays in Board.
        // If they are then checks if the given Row/Column/Diagonal is the same shape.
        // If so then returns true, false otherwise.
        private bool isPlayerLost(PointIndex i_Cell)
        {
            bool rowSameShpe = false, columnSameShape = false, topLeftBottomRightDiagonalSameShape = false, BottomLeftTopRightDiagonalSameShape = false;

            if (m_Board.OccupiedCellsInColumnBucket[i_Cell.Column] == m_BoardSize)
            {
                columnSameShape = m_Board.IsColumnSameShape(i_Cell.Column);
            }

            if (m_Board.OccupiedCellsInRowBucket[i_Cell.Row] == m_BoardSize)
            {
                rowSameShpe = m_Board.IsRowSameShape(i_Cell.Row);
            }

            if (i_Cell.Row == i_Cell.Column) // Check if the given point is on the top left to bottom right diagonal 
            {
                if (m_Board.OccupiedCellsInDiagonalBucketBucket[0] == m_BoardSize)
                {
                    topLeftBottomRightDiagonalSameShape = m_Board.IsDiagonalSameShape(eDiagonal.TopLeftToBottomRight);
                }
            }

            if (i_Cell.Row == (m_BoardSize - i_Cell.Column - 1)) // Check if the given point is on the bottom left to top right diagonal
            {
                if (m_Board.OccupiedCellsInDiagonalBucketBucket[1] == m_BoardSize)
                {
                    BottomLeftTopRightDiagonalSameShape = m_Board.IsDiagonalSameShape(eDiagonal.BottomLeftToTopRight);
                }
            }

            return rowSameShpe || columnSameShape || topLeftBottomRightDiagonalSameShape || BottomLeftTopRightDiagonalSameShape;
        }

        // Returns true or false based on if the game ended.
        private bool isGameEnded()
        {
            if (m_QuitGame == true)
            {
                m_GameEnded = true;
                m_EndingMessage = k_GameQuitMessage;
            }

            if (m_Board.TurnsLeft == 0 && !m_GameEnded) // If there are no moves and the game didn't end because someone won
            {
                m_GameEnded = true;
                m_EndingMessage = k_BoardFullMessage;
            }

            return m_GameEnded;
        }

        // Restes the game board and keeps the score as is
        private void resetGame()
        {
            m_Board.ResetBoard();
            m_GameEnded = false;
            m_CurrentPlayer = r_FirstPlayer;
        }

        // Changes the current player to the other player
        private void changePlayer()
        {
            if (m_CurrentPlayer.PlayerId.Equals(ePlayers.PlayerOne))
            {
                m_CurrentPlayer = m_SecondPlayer;
            }
            else
            {
                m_CurrentPlayer = r_FirstPlayer;
            }
        }
    }
}
