using System;

namespace CSharp_Ex2
{
    public class IO
    {
        private const string k_NewLineSeparator = "===="; // The seperator symbol

        // Prompts the player to input a board size and while the input is invalid prompts the player to to input again. return the size as int.
        public static int GetBoardSizeInput()
        {
            int boardSize;
            string userBoardSizeInputStr = string.Empty;
            do
            {
                Console.Write("Please enter board size: ");
                userBoardSizeInputStr = Console.ReadLine();
                Ex02.ConsoleUtils.Screen.Clear();
            }
            while (!isBoardSizeInputValid(userBoardSizeInputStr, out boardSize));

            if (userBoardSizeInputStr.Equals("Q"))
            {
                boardSize = -1;
            }

            return boardSize;
        }

        // Return the player mode by the user input
        public static Game.eMode GetPlayingMode()
        {
            string modeChosen;
            Game.eMode eModeChosen;
            do
            {
                Console.WriteLine("Please enter playing mode:");
                Console.WriteLine("Press 1 for human\nPress 2 for computer");
                modeChosen = Console.ReadLine();
            }
            while (!isModeValid(modeChosen, out eModeChosen));
            return eModeChosen;
        }

        // Prompts the player to enter a row and column to put his shape in, validates and returns the row and column as a PointIndex.
        public static PointIndex GetPlayerIntendedCell(Board i_Board, Player i_CurrentPlayer, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            string colStr = string.Empty;
            string rowStr = string.Empty;
            PointIndex intendedCell = new PointIndex(0, 0);
            bool isExit;
            do
            {
                Console.Write("The row index is: ");
                rowStr = Console.ReadLine();
                isExit = (rowStr.ToUpper() == "Q");
                if (!isExit)
                {
                    Console.Write("The column index is: ");
                    colStr = Console.ReadLine();
                    isExit = (colStr.ToUpper() == "Q");
                }
            }
            while (!isExit && !isPointIndexIsValid(rowStr, colStr, i_Board, i_CurrentPlayer, i_FirstPlayer, i_SecondPlayer, out intendedCell));

            return intendedCell;
        }

        // Checks if the given input chars are valid, if not prints out error message.
        private static bool isPointIndexIsValid(string i_RowStr, string i_ColStr, Board i_Board, Player i_CurrentPlayer, Player i_FirstPlayer, Player i_SecondPlayer, out PointIndex o_PointIndex)
        {
            int colIndex = -1;
            bool pointIndexValid = false;
            if (int.TryParse(i_RowStr, out int rowIndex))
            {
                if (int.TryParse(i_ColStr, out colIndex))
                {
                    pointIndexValid = true;
                }
            }

            o_PointIndex = new PointIndex(rowIndex, colIndex);
            if (!pointIndexValid)
            {
                string errorMessage = string.Format("Input must be between 1 and {0}", i_Board.BoardSize);
                PrintBoardWithErrors(i_Board, i_CurrentPlayer, errorMessage, i_FirstPlayer, i_SecondPlayer);
            }

            return pointIndexValid;
        }

        // Prints out the board, the player turn and the error message from the previous move.
        public static void PrintBoardWithErrors(Board i_Board, Player i_CurrentPlayer, string i_ErrorMessage, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            PrintGameBoard(i_Board, i_FirstPlayer, i_SecondPlayer);
            printErrorMessage(i_ErrorMessage);
        }

        // Print an error message with the given format
        private static void printErrorMessage(string i_ErrorMessage)
        {
            Console.WriteLine("Invalid input: {0}", i_ErrorMessage);
        }

        // Printing the end game message.
        public static void PrintGameEndedMessage(string i_EndingMessage)
        {
            Console.WriteLine("Game ended: {0}", i_EndingMessage);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Returns true if the chosen game mode is valid, otherwise returns false and prints error.
        private static bool isModeValid(string i_ModeChosen, out Game.eMode o_Mode)
        {
            o_Mode = Game.eMode.Human;
            bool modeValidation = false;
            if (i_ModeChosen == "1")
            {
                o_Mode = Game.eMode.Human;
                modeValidation = true;
            }
            else if (i_ModeChosen == "2")
            {
                o_Mode = Game.eMode.Computer;
                modeValidation = true;
            }
            else if (i_ModeChosen.ToUpper() == "Q")
            {
                o_Mode = Game.eMode.Exit;
                modeValidation = true;
            }
            else
            {
                Ex02.ConsoleUtils.Screen.Clear();
                printErrorMessage("Unsupported mode");
            }

            return modeValidation;
        }

        // Printing the playing player
        public static void PrintPlayerTurnPrompt(Player i_CurrentPlayer)
        {
            Console.WriteLine($"{i_CurrentPlayer}'s turn.");
        }

        // Returns true if the board size input is correct, otherwise prints error and returns false
        private static bool isBoardSizeInputValid(string i_Input, out int i_BoardSize)
        {
            bool boardInputValid;
            if (i_Input.ToUpper().Equals("Q"))
            {
                boardInputValid = true;
                i_BoardSize = -1;
            }
            else if (int.TryParse(i_Input, out i_BoardSize))
            {
                if (i_BoardSize >= 3 && i_BoardSize <= 9)
                {
                    boardInputValid = true;
                }
                else
                {
                    printErrorMessage("size must be a number between 3 and 9");
                    boardInputValid = false;
                }
            }
            else
            {
                printErrorMessage("size must be a number between 3 and 9");
                boardInputValid = false;
            }

            return boardInputValid;
        }

        // Prints out the Game Board
        public static void PrintGameBoard(Board i_GameBoard, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            Ex02.ConsoleUtils.Screen.Clear();

            int boardSize = i_GameBoard.BoardSize;
            Console.WriteLine($"{i_FirstPlayer}: {i_FirstPlayer.Score}    {i_SecondPlayer}: {i_SecondPlayer.Score}\n");

            Console.Write("  ");
            for (int i = 1; i <= boardSize; i++)    //
            {                                       // Prints out the first line of numbers at the top of the board
                Console.Write($"  {i} ");           //
            }                                       //

            Console.WriteLine();
            for (int i = 1; i <= boardSize; i++) // Printing the board itself 
            {
                Console.Write($"{i} "); // Print the number at the beginning of each row
                for (int j = 1; j <= boardSize; j++)
                {
                    Console.Write($"| {CellTypeUtils.ToCustomShape(i_GameBoard.BoardCells[i - 1, j - 1])} "); // Print empty spaces for the remaining columns
                }

                Console.WriteLine("|");
                printRowSeparator(boardSize);
                Console.WriteLine();
            }
        }

        // Prints the ==== separator
        private static void printRowSeparator(int i_BoardSize)
        {
            Console.Write("  ");
            for (int k = 0; k < i_BoardSize; k++)
            {
                Console.Write(k_NewLineSeparator);
            }

            Console.Write("="); // for the edge
        }
    }
}
