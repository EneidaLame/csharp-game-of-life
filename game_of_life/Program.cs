using System;
using System.Threading;

namespace game_of_life
{
    class Program
    {
        static void Main(string[] args)
        {
            GameOfLife game = new GameOfLife(10, 10, "glider");

            while (true)
            {
                game.Display();
                game.CalculateNext();
                Thread.Sleep(5000);
            }
        }
    }
}
