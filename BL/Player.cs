using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pacman.BL;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Game_GUI.BL
{
public    class Player : Character,IPlayer
    {
        private bool level_up=false;

        public bool Level_up { get => level_up; set => level_up = value; }

        public Player(PictureBox p1, int health, objtype t, char character, cell c,ProgressBar hp,bool level_up) : base(p1, health, t, character, c,hp)
        {
            this.level_up = level_up;
            // Character C = new Character(p1, p2, health, t, character, c);  
        }
        public bool collision()
        {
            return true;
        }
        public static bool fire_move(bullet b)
        {
            cell c = cell.nextcell(b, direction.UP);
            if (c != null)
            {
                //if(c.Obj!='#')
                if (b.C.X >1)

                {
                    c.P = b.Main;
                    c.Obj = b.C.Obj;
                    b.C.Obj = ' ';
                    b.C.P = null;
                    b.C = c;
                    return true;
                }
                else
                {
                    b.C.Obj = ' ';
                    b.C.P = null;
                    b.C = c;
                    return false;
                }
            }
            return true;
        }
        public void fire_missle()
        {

        }
      public static Player Create_player()
        {
            PictureBox p1 = new PictureBox();
            ProgressBar hp = new ProgressBar();
            hp.Value = 100;
            hp.Size=new Size(100, 10);
            p1.BackgroundImage = Game_GUI.Properties.Resources.player;
           
            p1.BackgroundImageLayout = ImageLayout.Stretch;
            cell c = Grid.Get_cell(21, 35);
            c.Hp = hp;
            c.Obj = 'P';
            c.P = p1;
            c.P.BackgroundImageLayout = ImageLayout.Stretch;
            c.P.Size = new Size(80, 80);
            c.P.BackColor = Color.Transparent;
            Action a = null;
            Player p = new Player(p1, 100, objtype.Enemy, 'E', c,hp,false);
            return p;
        }
        public void V_move(Game_object G, ref bool move_down)
        {
            Action a = null;
            int speed = 12;
            // if (main.Left>=00 &&main.Left <= 718)
            if (G.C.X < 20 && move_down == true)
            {

                cell c = cell.nextcell(G, direction.Down);
                if (!(c.X==20))
                {
                    c.P = G.C.P;
                    G.C.P = null;
                    a = delegate { main.Top += 30; HP.Top += 31; };
                    G.C.Obj = ' ';
                    G.C = c;
                    G.C.Obj = 'P';

                }

            }
            else if (G.C.X >= 3)//main.Left==718 && main.Left>00)
            {
                move_down = false;
                cell c = cell.nextcell(G, direction.UP);
                if (!(c.Obj == '.'))
                {
                    c.P = G.C.P;
                    G.C.P = null;
                    a = delegate { main.Top -= 30;HP.Top -= 30; };
                    G.C.Obj = ' ';
                    G.C = c;
                    G.C.Obj = 'P';
                }
                /*G.C.P = null;
                G.C = c;
                a = delegate { main.Left -= 12; };*/
            }
            else
                move_down = true;
            if (main.IsHandleCreated)
            {
                main.BeginInvoke(a);
            }

        }
        
    }
}
