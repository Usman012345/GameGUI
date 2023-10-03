using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pacman.DL;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using pacman.BL;
//using static Pacman_Gui.Form1;

namespace pacman.BL
{
   public class cell
    {
        const int width = 20;
        const int height = 20;
        private int x;
        private int y;
        private char obj;
        private PictureBox p = new PictureBox();
        private ProgressBar hp = new ProgressBar();
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public char Obj { get => obj; set => obj = value; }
        public PictureBox P { get => p; set => p = value; }
        public ProgressBar Hp { get => hp; set => hp = value; }

        public cell(int x, int y, char obj, Form f1)
        {
            this.x = x;
            this.y = y;
            this.obj = obj;
            if(obj==' ')
            {
                p.BackgroundImage = null;
            }
            if(obj=='E')
            {
                p.BackgroundImage = Game_GUI.Properties.Resources.monster;
            }
            else if (obj == 'P')
            {
                p.BackgroundImage = Game_GUI.Properties.Resources.player;
            }
            p.Size = new Size(width, height);
            p.BackgroundImageLayout = ImageLayout.Stretch;
        }
        public static void Create_cells(Form f1)
        {
            int z = 0;
            // int t = 0;
            for (int x = 0; x < Grid.maze.GetLength(0); x++)
            {
                char[] temp = Grid.maze[x, z].ToCharArray();
                cell[] cells = new cell[temp.GetLength(0)];
                for (int y = 0; y < temp.GetLength(0); y++)
                {
                    cell c = new cell(x, y, temp[y], f1);
                    cells[y] = c;
                }
                for (int a = 0; a < cells.GetLength(0); a++)
                {
                    Grid_dl.cells[x, a] = cells[a];
                }
                //t++;
            }
        }
        public static cell nextcell(Game_object G, direction d)
        {
            if (d == direction.UP)
            {
                return Grid.Get_cell(G.C.x - 1, G.C.y);
            }
            else if (d == direction.Down)
            {
                return Grid.Get_cell(G.C.x + 1, G.C.y);
            }
            else if (d == direction.Left)
            {
                return Grid.Get_cell(G.C.x, G.C.y - 1);
            }
            else if (d == direction.Right)
            {
                return Grid.Get_cell(G.C.x, G.C.y + 1);
            }
            return null;

        }
    }
}
