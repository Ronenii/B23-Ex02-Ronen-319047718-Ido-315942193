using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Ex2
{
    class IO
    {
        private static string m_Input; // Generic input vars used for all types of input

        // Prompts the player to input a board size and while the input is invalid prompts the player to to input again. return the size as int.
        public static int getBoardSizeInput()
        {
            do
            {
                Console.WriteLine("Please enter board size: ");
                m_Input = Console.ReadLine();
            }
            while (!boardSizeIsValid());

            return Int32.Parse(m_Input);
        }
        

        // Returns true if the input is correct, otherwise prints error and returns false
        private static bool boardSizeIsValid()
        {
            if(!(inputLengthIsValid() && boardInputCharIsValid()))
            {
                Console.WriteLine("Size must be between 3-9, please try again.");
            }
            return inputLengthIsValid() && boardInputCharIsValid();
        }

        // Returns true if the given input char is valid
        private static bool boardInputCharIsValid()
        {
            char input = m_Input[0];
            return ((input >= '3') && (input <= '9'));
        }

        // Prompts the player to input a row or a column and prompts him again while input is invalid. returns the input as int.
        public static int GetPlayerInput(string i_InputType, int i_BoardSize)
        {
            do
            {
                Console.WriteLine("{0}: ", i_InputType);
                m_Input = Console.ReadLine();
            }
            while (!rowOrColumnInputIsValid(i_InputType, i_BoardSize));

            return Int32.Parse(m_Input);
        }

        // Checks if the given input is 'Q' or a number in the range 0-9, prints out error if input is invalid.
        private static bool rowOrColumnInputIsValid(string i_InputType, int i_BoardSize)
        {
            if(!(inputLengthIsValid() && rowOrColumnCharIsValid(i_BoardSize)))
            {
                Console.WriteLine("{0} is invalid, please try again.", i_InputType);
            }
            return inputLengthIsValid() && rowOrColumnCharIsValid(i_BoardSize);
        }

        private static bool inputLengthIsValid()
        {
            return m_Input.Length == 1;
        }

        // Returns true if the row or column input is inbounds or is q
        private static bool rowOrColumnCharIsValid(int i_BoardSize)
        {
            char input = m_Input[0];
            return (input == 'Q') || (input >= '1' && input <= Convert.ToChar(i_BoardSize));
        }
    }
}
