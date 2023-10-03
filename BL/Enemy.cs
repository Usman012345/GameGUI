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
    class Enemy:Character
    {
        
        public Enemy(PictureBox p1,int health,objtype t,char character,cell c,ProgressBar hp) : base(p1, health, t, character, c,hp)
        {
           // Character C = new Character(p1, p2, health, t, character, c);  
        }
        public override void H_move(Game_object g,ref bool move_right)
        {
            base.H_move(g,ref move_right);

        }
       public static Enemy Create_enemy()
        {
            PictureBox p1 = new PictureBox();
            ProgressBar hp = new ProgressBar();
                hp.Value = 100;
            hp.Size = new Size(100, 10);
            p1.BackgroundImage = Game_GUI.Properties.Resources.monster;
            p1.BackgroundImageLayout = ImageLayout.Stretch;
            cell c = Grid.Get_cell(5, 20);
            c.Hp = hp;
            c.Obj = 'E';
            c.P = p1;
            c.P.BackgroundImageLayout = ImageLayout.Stretch;
            c.P.Size = new Size(80, 80);
            c.P.BackColor = Color.Transparent;
            Enemy e = new Enemy(p1, 100, objtype.Enemy, 'E', c,hp);
            return e;
        }



    }
}
