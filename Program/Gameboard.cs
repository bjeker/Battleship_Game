using System;

namespace Program
{
    public class Gameboard
    {
        //sets a 10x10 gameboard grid
        private char[,] grid = new char[10, 10];

        public char[,] Grid
        {
            //get grid size
            get
            {
                return grid;
            }

            //set grid size
            set
            {
                grid = value;
            }

        }

        //location of shot
        public void SetChar(int row, int col, char aChar)
        {
            //if shot is valid
            if (row < grid.GetLength(0) && col < grid.GetLength(1))
            {
                grid[row, col] = aChar;
            }
            else
            {
                Console.WriteLine("Invalid Location", grid[row, col]);
            }

        }

        //fills grid
        public void FillGrid()
        {
            //assign spaces in each row and column
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = ' ';
                }
            }

        }
        //fills grid with char
        public void FillGrid(char aChar)
        {
            //fill grid with shots
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = aChar;
                }

            }

        }

        //user fills in grid with char
        public void UserFillGrid()
        {
            Console.WriteLine("Please enter a charcter: ");
            //other way of reading one char -> char answer2 = Console.ReadKey().KeyChar;
            char answer = Console.ReadLine()[0];
            FillGrid(answer);
        }

        //fill grid
        public void CheckerFil()
        {
            char a = 'X';
            char b = 'O';

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = ((row + col) % 2 == 0) ? a : b;
                }

            }

        }

        //display grid with row chars counting up
        public void Display()
        {
            char rowChar = 'A';
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                DrawLine();
                Console.Write(rowChar + " ");
                rowChar++;

                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Console.Write($" | {grid[row, col]}");
                }

                Console.WriteLine(" | ");

            }
            DrawLine();
            DrawColNumbers();

        }

        //draws lines for the grid
        private void DrawLine()
        {
            Console.Write(" ");
            for (int i = 0; i < grid.GetLength(1) * 4 + 1; i++)
            {
                Console.Write($".");
            }
            Console.WriteLine();
        }

        //draw numbers of columns, counting up
        private void DrawColNumbers()
        {
            Console.Write("    ");
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                Console.Write($" {i + 1}  ");
            }
            Console.WriteLine();
        }
    }
}