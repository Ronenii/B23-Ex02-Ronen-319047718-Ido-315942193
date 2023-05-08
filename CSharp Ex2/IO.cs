using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Ex2
{
    class IO
    {
        private static int m_Row, m_Col, m_BoardSize;
        private static string m_Input, m_Input2; // Generic input vars used for all types of input
        public int Row
        {
            get
            {
                return m_Row;
            }
        }
        public int Col
        {
            get
            {
                return m_Col;
            }
        }

        //
        public static void getPlayerTurnInput()
        {
            do
            {
                Console.WriteLine("Row: ");
                m_Input = Console.ReadLine();
                Console.WriteLine("Col: ");
                m_Input2 = Console.ReadLine();
            }
            while(!playerTurnInputIsValid());

        }

        // Checks if the player's input of rows and columns is valid.
        private static bool playerTurnInputIsValid()
        {
            return rowOrColumnIsValid(m_Input) && rowOrColumnIsValid(m_Input2);
        }

        // Checks if the given input is 'Q' or a number in the range 0-9, prints out error if input is invalid.
        private static bool rowOrColumnIsValid(string i_Input)
        {
            bool inputIsValid = true;
            char input;
            if(i_Input.Length != 1)
            {
                inputIsValid = false;
            }

            input = i_Input[0];
            if(input != 'Q' && (input < '0' || input > '9'))
            {
                inputIsValid = false;
            }

            if(!inputIsValid)
            {
                Console.WriteLine("Input is invalid, please try again.");
            }

            return inputIsValid;
        }
    }
}
