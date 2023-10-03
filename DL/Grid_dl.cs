using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pacman.BL;
using System.IO;

namespace pacman.DL
{
    class Grid_dl
    {
        public static cell[,] cells = new cell[22, 71];
        public static void storegrid()
        {
            StreamWriter data = new StreamWriter("maze.txt", false);
            for (int x = 0; x < 23; x++)
            {
                for (int y = 0; y < 70; y++)
                {
                    data.WriteLine(Grid.maze[x, y]);
                }
            }
        }
        public static void loadgrid()
        {
            StreamReader data = new StreamReader("maze.txt", true);
            for (int x = 0; x < 23; x++)
            {
                for (int y = 0; y < 70; y++)
                {
                    Grid.maze[x, y] = data.ReadLine();
                }
            }
        }
    }
}
