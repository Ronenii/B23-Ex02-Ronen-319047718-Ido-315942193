using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Ex2
{
    class IO
    {
        private const string newLineSperator = "====";

        // Prompts the player to input a board size and while the input is invalid prompts the player to to input again. return the size as int.
        public static int getBoardSizeInput()
        {
            int boardSize;
            string userBoardSizeInputStr;
            do
            {
                Console.Write("Please enter board size: ");
                userBoardSizeInputStr = Console.ReadLine();
            }
            while (!isBoardSizeInputValid(userBoardSizeInputStr));

            if (userBoardSizeInputStr.Equals("Q"))
            {
                boardSize = -1;
            }
            else
            {
                boardSize = int.Parse(userBoardSizeInputStr);
            }

            return boardSize;
        }

        public static eMode getPlayingMode()
        {
            string modeChoosen;
            eMode eModeChoosen;
            do
            {
                Console.WriteLine("Please enter playing mode:");
                Console.WriteLine("Press 1 for human\nPress 2 for computer");
                modeChoosen = Console.ReadLine();
            }
            while (!isModeValid(modeChoosen, out eModeChoosen));
            return eModeChoosen;
        }

        // Prompts the player to enter a row and column to put his shape in, validates and returns the row and column as a PointIndex.
        public static PointIndex GetHumanPointIndex(Board i_Board, Player i_CurrentPlayer)
        {
            string rowStr;
            string colStr;
            PointIndex o_pointIndex;
            do
            {
                Console.Write("The row index is: ");
                rowStr = Console.ReadLine();
                Console.Write("The column index is: ");
                colStr = Console.ReadLine();
            }
            while (!isPointIndexIsValid(rowStr, colStr, i_Board, i_CurrentPlayer, out o_pointIndex));
            return o_pointIndex;
        }

        // Checks if the given input chars are valid, if not prints out error message.
        private static bool isPointIndexIsValid(string i_RowStr, string i_ColStr, Board i_Board, Player i_CurrentPlayer, out PointIndex o_pointIndex)
        {
            int rowIndex = -1;
            int colIndex = -1;
            bool pointIndexValidation = false;
            if (int.TryParse(i_RowStr, out rowIndex))
            {
                if (int.TryParse(i_ColStr, out colIndex))
                {
                    pointIndexValidation = true;
                }
            }
            o_pointIndex = new PointIndex(rowIndex, colIndex);
            if (!pointIndexValidation)
            {
                string errorMessage = string.Format("Input must be between 1 and {0}", i_Board.BoardSize);
                PrintBoardWithErrors(i_Board, i_CurrentPlayer, errorMessage);
            }
            return pointIndexValidation;
        }

        // Prints out the board, the player turn and the error message from the previous move.
        public static void PrintBoardWithErrors(Board i_Board, Player i_CurrentPlayer, string i_ErrorMessage)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            printGameBoard(i_Board);
            PrintPlayerTurnPrompt(i_CurrentPlayer);
            printErrorMessage(i_ErrorMessage);
        }

        private static void printErrorMessage(string i_ErrorMessage)
        {
            Console.WriteLine("Invalid input: {0}", i_ErrorMessage);
        }

        // Returns true if the chosen game mode is valid, otherwise returns false and prints error.
        private static bool isModeValid(string i_ModeChoosen, out eMode o_Mode)
        {
            o_Mode = eMode.Human;
            bool modeValidation = false;
            if (i_ModeChoosen == "1")
            {
                o_Mode = eMode.Human;
                modeValidation = true;
            }
            else if (i_ModeChoosen == "2")
            {
                o_Mode = eMode.Computer;
                modeValidation = true;
            }
            else
            {
                Ex02.ConsoleUtils.Screen.Clear();
                printErrorMessage("Unsupported mode");
            }
            return modeValidation;
        }

        public static void PrintPlayerTurnPrompt(Player i_CurrentPlayer)
        {
            if (i_CurrentPlayer.PlayerId == ePlayers.PlayerOne)
            {
                Console.WriteLine("Player one's turn.");
            }
            else
            {
                Console.WriteLine("Player two's turn.");
            }
        }

        // Returns true if the board size input is correct, otherwise prints error and returns false
        private static bool isBoardSizeInputValid(string i_Input, out int i_BoardSize)
        {

            bool boardInputValid = false;
            if(i_Input.Equals("Q"))
            {
                boardInputValid = true;
                i_BoardSize = -1;
            }
            else if(int.TryParse(i_Input, out i_BoardSize))
            {
                if(i_BoardSize >= 3 && i_BoardSize <= 9)
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
        public static void printGameBoard(Board i_GameBoard)
        {
            int boardSize = i_GameBoard.BoardSize;

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
        private static void printRowSeparator(int boardSize)
        {
            Console.Write("  ");
            for (int k = 0; k < boardSize; k++)
            {
                Console.Write(newLineSperator);
            }
            Console.Write("="); // for the edge
        }
    }
}
