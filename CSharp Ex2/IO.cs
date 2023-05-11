using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Ex2
{
    class IO
    {
        // Prompts the player to input a board size and while the input is invalid prompts the player to to input again. return the size as int.
        public static int getBoardSizeInput()
        {
            int boardSize;
            string userBoardSizeInputStr;
            do
            {
                Console.WriteLine("Please enter board size: ");
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

        // Prompts the player to input a row or a column and prompts him again while input is invalid. returns the input as int.
        private static int GetRowOrColumnInput(string i_InputType, int i_BoardSize)
        {
            string rowOrColumInputStr;
            do
            {
                Console.WriteLine("{0}: ", i_InputType);
                rowOrColumInputStr = Console.ReadLine();
            }
            while (!rowOrColumnInputIsValid(rowOrColumInputStr, i_InputType, i_BoardSize));

            return int.Parse(rowOrColumInputStr);
        }

        public static void getPlayerTurn(Board i_GameBoard, ePlayers i_CurrentPlayer)
        {
            int boardSize = i_GameBoard.BoardSize, row, col;
            printPlayerTurnPrompt(i_CurrentPlayer);
            do
            {
                row = GetRowOrColumnInput("Row", boardSize);
                col = GetRowOrColumnInput("Col", boardSize);
            }
            while (!isCellEmpty(row, col, i_GameBoard));

            i_GameBoard.UpdateBoardCell(row, col, i_CurrentPlayer);
        }

        private static void printPlayerTurnPrompt(ePlayers i_CurrentPlayer)
        {
            if (i_CurrentPlayer == ePlayers.PlayerOne)
            {
                Console.WriteLine("Player one's turn.");
            }
            else
            {
                Console.WriteLine("Player two's turn.");
            }
        }

        private static bool isCellEmpty(int i_Row, int i_Col, Board i_GameBoard)
        {
            bool isCellEmptyRet = (i_GameBoard.BoardCells[i_Row, i_Col] == eCellType.Empty);
            if (!isCellEmptyRet)
            {
                Console.WriteLine("Cell is occupied, please try another cell.");
            }

            return isCellEmptyRet;
        }

        // Checks if the given input is 'Q' or a number in the range 0-9, prints out error if input is invalid.
        private static bool rowOrColumnInputIsValid(string i_Input, string i_RowOrColumnStr, int i_BoardSize)
        {
            if (!(inputLengthIsValid(i_Input) && rowOrColumnCharIsValid(i_Input, i_BoardSize)))
            {
                Console.WriteLine("{0} is invalid, please try again.", i_RowOrColumnStr);
            }
            return inputLengthIsValid(i_Input) && rowOrColumnCharIsValid(i_Input, i_BoardSize);
        }

        private static bool inputLengthIsValid(string i_Input)
        {
            return i_Input.Length == 1;
        }

        // Returns true if the row or column input is inbounds or is q
        private static bool rowOrColumnCharIsValid(string i_Input, int i_BoardSize)
        {
            char input = i_Input[0];
            return (input == 'Q') || (input >= '1' && input <= Convert.ToChar(i_BoardSize));
        }

        // Prints out the Game Board
        public static void printGameBoard(Board i_GameBoard)
        {
            int boardSize = i_GameBoard.BoardSize;
            int numOfRowsToPrint = (boardSize * 2); // The number of rows to print in the table (after printing the initial number row
            int rowInGameBoard; // The corresponding row in i_GameBoard to the printed table

            printNumberRow(boardSize);
            for (int i = 0; i < numOfRowsToPrint; i++)
            {
                if (i % 2 == 0)
                {
                    rowInGameBoard = i % 2;
                    printTableRow(i_GameBoard, rowInGameBoard);
                }
                else
                {
                    printRowSeparator(boardSize);
                }
            }
        }

        // Prints the row of numbers at the top of the table
        private static void printNumberRow(int i_BoardSize)
        {
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    Console.Write(" ");
                }

                Console.Write(i + 1); // print column number
            }
            Console.WriteLine();
        }

        // Prints the given row to print (the row which contains the Xs and Os)
        private static void printTableRow(Board i_GameBoard, int i_RowToPrint)
        {
            int boardSize = i_GameBoard.BoardSize;
            int columnsToPrint = boardSize * 4 + 1; // The the number of columns to print in the row (after printing the row number)
            int columnInGameBoard; // The corresponding row in i_GameBoard to the printed table

            Console.Write(i_RowToPrint + 1);
            for (int i = 0; i < columnsToPrint; i++)
            {
                if (i % 4 == 0)
                {
                    Console.Write("|");

                }
                else if ((i + 2) % 4 == 0)
                {
                    columnInGameBoard = i / 4;
                    printCell(i_GameBoard.BoardCells[i_RowToPrint, columnInGameBoard]);
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }

        // Receives an enum of Cell type and prints the corresponding shape
        private static void printCell(eCellType i_CurrentCell)
        {
            switch (i_CurrentCell)
            {
                case eCellType.Empty:
                    Console.Write(" ");
                    break;
                case eCellType.Circle:
                    Console.Write("O");
                    break;
                case eCellType.Cross:
                    Console.Write("X");
                    break;
            }
        }

        // Prints the separating rows (the ========== rows)
        private static void printRowSeparator(int i_BoardSize)
        {
            int columnsToPrint = i_BoardSize * 4 + 1; // The the number of columns to print in the row (after printing an initial space)
            Console.Write(" ");
            for (int i = 0; i < columnsToPrint; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

    }
}
