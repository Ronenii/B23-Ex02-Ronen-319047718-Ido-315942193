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
            while (!rowOrColumnInputIsValid(rowOrColumInputStr,i_InputType, i_BoardSize));

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

        public static void printGameBoard(Board i_GameBoard)
        {
            int printedBoardSize = (i_GameBoard.BoardSize * 2) + 1;
            for(int i = 0; i < printedBoardSize; i++)
            {
                
            }
        }
    }
}
