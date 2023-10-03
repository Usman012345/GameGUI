using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using pacman.BL;
using System.Threading.Tasks;

namespace Game_GUI.BL
{
  public abstract class Character:Game_object
    {
        protected PictureBox main = new PictureBox();
        protected int health;
        private ProgressBar hP = new ProgressBar();

        public PictureBox Main { get => main; set => main = value; }
        public int Health { get => health; set => health = value; }
        public ProgressBar HP { get => hP; set => hP = value; }

        public Character(PictureBox p1,int health,objtype t,char character,cell c,ProgressBar HP):base(t,character,c)
        {
            this.Health = health;
           this. main = p1;
            this.HP = HP;
        }
        public virtual void H_move(Game_object G,ref bool move_right)
        {
            Action a=null;
            int speed = 12;
            // if (main.Left>=00 &&main.Left <= 718)
            if ( G.C.Y < 68 && move_right == true)
            {

                cell c = cell.nextcell(G, direction.Right);
                if (!(c.Obj == '|'))
                {
                    c.P = G.C.P;
                    G.C.P = null;
                   a=delegate{ main.Left += 20;hP.Left += 20; };
                    G.C.Obj = ' ';
                    G.C = c;
                    G.C.Obj = 'E';

                }

            }
            else if (G.C.Y >3)//main.Left==718 && main.Left>00)
            {
                move_right = false;
                cell c = cell.nextcell(G, direction.Left);
                if (!(c.Obj == '|'))
                {
                    c.P = G.C.P;
                    G.C.P = null;
                    a = delegate { main.Left -= 20; hP.Left -= 20; };
                    G.C.Obj = ' ';
                    G.C = c;
                    G.C.Obj = 'E';
                }
                }
            else
                move_right = true;
            if (main.IsHandleCreated)
            {
                main.BeginInvoke(a);
            }
        }
        
    }
}
