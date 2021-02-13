using System;
namespace game_of_life
{
    public class GameOfLife
    {
        int Generation { get; set; }
        int Rows { get; set; }
        int Columns { get; set; }
        int [,] Current { get; set; }
        int [,] Next { get; set; }


        public GameOfLife(int width, int height)
        {
            Rows = height;
            Columns = width;
            Current = new int[Rows,Columns];
            Next = new int[Rows, Columns];
            Init();
        }

        public GameOfLife(int width, int height, string type) : this(width, height)
        {
            Generation = 1;
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Current[row, col] = 0;
                }
            }
            if (type.Equals("glider"))
            {
                Current[0, 1] = 1;
                Current[1, 2] = 1;
                Current[2, 0] = 1;
                Current[2, 1] = 1;
                Current[2, 2] = 1;
            }
        }

        public void Init()
        {
            Random rnd = new Random();
            Generation = 1;
            for(int row = 0; row < Rows; row++)
            {
                for(int col = 0; col < Columns; col++)
                {
                    Current[row, col] = rnd.Next(2);
                }
            }
        }

        public void Display()
        {
            Console.WriteLine("Gen: " + Generation);
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Console.Write((Current[row, col] == 1 ? "X" : "-") + " ");
                }
                Console.WriteLine();
            }
        }

        public void DisplayNext()
        {
            Console.WriteLine("Next");
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Console.Write((Next[row, col] == 1 ? "X" : "-") + " ");
                }
                Console.WriteLine();
            }
        }

        public void CalculateNext()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    int neighbors = AliveNeighbors(row, col);
                    if(neighbors == 3)
                    {
                        Next[row, col] = 1;
                    }
                    else if(neighbors == 2 && Current[row,col] == 1)
                    {
                        Next[row, col] = 1;
                    }
                    else
                    {
                        Next[row, col] = 0;
                    }
                }
            }
            Generation++;
            CopyNextToCurrent();
        }

        public void CopyNextToCurrent()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Current[row, col] = Next[row, col];
                }
            }
        }

        public int AliveNeighbors(int row, int col)
        {
            int n = 0;
            int start_row = row > 0 ? row - 1 : row;
            int start_col = col > 0 ? col - 1 : col;
            int end_row = row < Rows - 1 ? row + 1 : row;
            int end_col = col < Columns - 1 ? col + 1 : col;

            for(int r = start_row; r <= end_row; r++)
            {
                for(int c = start_col; c <= end_col; c++)
                {
                    n += Current[r, c];
                }
            }
            n -= Current[row, col]; // Remove this cell if 1

            return n;
        }
    }
}
