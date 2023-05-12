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
            while (!isBoardInputValid(userBoardSizeInputStr));

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

        private static bool isPointIndexIsValid(string i_rowStr, string i_colStr, Board i_Board, Player i_CurrentPlayer, out PointIndex o_pointIndex)
        {
            int rowIndex = -1;
            int colIndex = -1;
            bool pointIndexValidation = false;
            if (int.TryParse(i_rowStr, out rowIndex))
            {
                if (int.TryParse(i_colStr, out colIndex))
                {
                    pointIndexValidation = true;
                }
            }
            o_pointIndex = new PointIndex(rowIndex, colIndex);
            if (!pointIndexValidation)
            {
                PrintBoardWithErrors(i_Board, i_CurrentPlayer, "Cell out of bounds");
            }
            return pointIndexValidation;
        }

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

        private static bool isModeValid(string i_modeChoosen, out eMode o_mode)
        {
            o_mode = eMode.Human;
            bool modeValidation = false;
            if (i_modeChoosen == "1")
            {
                o_mode = eMode.Human;
                modeValidation = true;
            }
            else if (i_modeChoosen == "2")
            {
                o_mode = eMode.Computer;
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

        // Returns true if the input is correct, otherwise prints error and returns false
        private static bool isBoardInputValid(string i_Input)
        {
            bool boardInputValid = (inputLengthIsValid(i_Input) && isBoardInputCharValid(i_Input));
            if (!boardInputValid)
            {
                Console.WriteLine("Size must be between 3-9, please try again.");
            }
            return boardInputValid;
        }

        // Returns true if the given input char is valid
        private static bool isBoardInputCharValid(string i_Input)
        {
            char input = i_Input[0];
            return ((input >= '3') && (input <= '9')) || (input == 'Q');
        }


        private static bool inputLengthIsValid(string i_Input)
        {
            return i_Input.Length == 1;
        }


        // Prints out the Game Board
        public static void printGameBoard(Board i_GameBoard)
        {
            int boardSize = i_GameBoard.BoardSize;

            Console.Write("  ");
            for (int i = 1; i <= boardSize; i++)
            {
                Console.Write($"  {i} ");
            }
            Console.WriteLine();
            for (int i = 1; i <= boardSize; i++)
            {
                Console.Write($"{i} ");
                for (int j = 1; j <= boardSize; j++)
                {
                    Console.Write($"| {CellTypeUtils.ToCustomShape(i_GameBoard.BoardCells[i - 1, j - 1])} "); // Print empty spaces for the remaining columns
                }
                Console.WriteLine("|");
                printLineSeperator(boardSize);
                Console.WriteLine();
            }
        }

        private static void printLineSeperator(int boardSize)
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
