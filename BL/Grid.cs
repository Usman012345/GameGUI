using System;
using pacman.DL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pacman.BL;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using Game_GUI;



namespace pacman.BL
{
    class Grid
    {
        public int column = 70;
        public int rows = 23;
        public static string[,] maze = { { " || .......................................................  ......  ||" } ,
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ||                                                                  ||" },
                     { " ######################################################################" } };

        public static cell Get_cell(int x, int y)
        {
            for (int a = 0; a < Grid_dl.cells.GetLength(0); a++)
            {
                for (int b = 0; b < Grid_dl.cells.GetLength(1); b++)
                {
                    if (Grid_dl.cells[a, b].X == x && Grid_dl.cells[a, b].Y == y)
                        return Grid_dl.cells[a, b];
                }
            }
            return null;
        }  
    }
}
