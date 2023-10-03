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
   public class bullet:Character
    {
        private bool moved=false;
        public static List<bullet> bullets = new List<bullet>();
        public static List<bullet> bullets1 = new List<bullet>();
        public static List<bullet> player_bullets = new List<bullet>();
        public static bullet bomb=null;

        public bool Moved { get => moved; set => moved = value; }

        public bullet(PictureBox p1, PictureBox p2, int health, objtype t, char character, cell c,bool moved,ProgressBar hp) : base(p1, health, t, character, c,hp)
        {
            this.moved = moved;
            // Character C = new Character(p1, p2, health, t, character, c);  
        }
        

        public static bool V_move(bullet b)
        {
            cell c = cell.nextcell(b, direction.Down);
            if (c != null)
            {
                //if(c.Obj!='#')
                if (b.C.X<20)
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
        public static bullet V__move()
        {
            for (int index=0; index < bullet.bullets1.Count; index++)
            {
                Form1 f1 = new Form1();
                Action a = null;
                int speed = 12;
                if (bullet.bullets1[index].C.X < 20)
                {

                    cell c = cell.nextcell(bullet.bullets1[index], direction.Down);

                    if (!(c.Obj == '|'))
                    {
                        c.P = bullet.bullets1[index].Main;
                        bullet.bullets1[index].C.P = null;
                        bullet.bullets1[index].C.Obj = ' ';
                        bullet.bullets1[index].C = c;
                        bullet.bullets1[index].C.P = bullet.bullets1[index].Main;
                        bullet.bullets1[index].C.Obj = 'B';
                      

                    }
                }
                else
                {

                    bullet.bullets1[index].C.Obj = ' ';
                    return bullet.bullets1[index];
                }
                /*if (bullet.bullets1[index].main.IsHandleCreated)
                {
                    bullet.bullets1[index].main.BeginInvoke(a);
                }*/

            }
            return null;

        }
    }
}
