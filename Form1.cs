using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using static Game_GUI.Form1;
using pacman.BL;
using Game_GUI.BL;
using pacman.DL;
using EZInput;

namespace Game_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool game_over = false;
            bool E_alive = true;
            bool E1_alive = true;
            this.DoubleBuffered = true;
            cell.Create_cells(this);
           Enemy E=Enemy.Create_enemy();
            Enemy1 E1 = Enemy1.Create_enemy1();
            Player p = Player.Create_player();
           load_grid();
            backgroundWorker1.RunWorkerAsync(E);
            backgroundWorker2.RunWorkerAsync(E1);
           
            timer2.Tick += (s, args) => timer2_Tick(p,E1);
            timer1.Tick += (s, args) => timer1_Tick(p,E);
            timer3.Tick += (s, args) => timer3_Tick(p,E,E1,ref E_alive,ref E1_alive,ref game_over);
            timer4.Tick += (s, args) => timer4_Tick(p,E,E1,ref E_alive,ref E1_alive,ref game_over);
            backgroundWorker3.RunWorkerAsync(p);
            if (E_alive == false)
            {
                for (int x = 0; x < bullet.bullets.Count; x++)
                {

                    this.Controls.Remove(bullet.bullets[x].Main);
                }
            }
            if(E1_alive == false)
            { 
                for (int x = 0; x < bullet.bullets1.Count; x++)
                {

                    this.Controls.Remove(bullet.bullets1[x].Main);
                }
            }
            

            /*int x = 0;
            bool move_down = false;
            bool move_right = false;
           */
            /*while (true)
            {
                if (Keyboard.IsKeyPressed(Key.UpArrow))
                {
                    p.V_move(p, ref move_down);
                }
                else if (Keyboard.IsKeyPressed(Key.LeftArrow))
                {
                    move_right = false;
                    p.H_move(p, ref move_right);
                }
                else if (Keyboard.IsKeyPressed(Key.RightArrow))
                {
                    move_right = true;
                    p.H_move(p, ref move_right);
                }
                Thread.Sleep(100);*/

        }
        public void load_grid()
        {
            const int width = 20;
            const int height = 20;

            for (int x = 0; x < Grid_dl.cells.GetLength(0); x++)
            {
                for (int y = 0; y < Grid_dl.cells.GetLength(1); y++)
                {
                    if (Grid_dl.cells[x, y].Obj != 'P')
                    {
                        Grid_dl.cells[x, y].P.Left = Grid_dl.cells[x, y].Y * height;
                        Grid_dl.cells[x, y].P.Top = Grid_dl.cells[x, y].X * width;
                        Grid_dl.cells[x, y].Hp.Left = Grid_dl.cells[x, y].Y * height;
                        Grid_dl.cells[x, y].Hp.Top = Grid_dl.cells[x, y].X * (width-2);

                        if (!(Grid_dl.cells[x, y].Obj == ' ' || Grid_dl.cells[x, y].Obj == '|' || Grid_dl.cells[x, y].Obj == '.' || Grid_dl.cells[x, y].Obj == '#'))
                        {

                            this.Controls.Add(Grid_dl.cells[x, y].P);
                            this.Controls.Add(Grid_dl.cells[x, y].Hp);
                        }
                    }
                    else
                    {
                        Grid_dl.cells[x, y].P.Left = Grid_dl.cells[x, y].Y * height;
                        Grid_dl.cells[x, y].P.Top = Grid_dl.cells[x, y].X * 30;
                        Grid_dl.cells[x, y].Hp.Left = Grid_dl.cells[x, y].Y * height;
                        Grid_dl.cells[x, y].Hp.Top = Grid_dl.cells[x, y].X * 34;

                        if (!(Grid_dl.cells[x, y].Obj == ' ' || Grid_dl.cells[x, y].Obj == '|' || Grid_dl.cells[x, y].Obj == '.' || Grid_dl.cells[x, y].Obj == '#'))
                        {

                            this.Controls.Add(Grid_dl.cells[x, y].P);
                            this.Controls.Add(Grid_dl.cells[x, y].Hp);
                        }
                    }
                }
            }
        }
        public bullet Fire(cell c, string enemy_type)
        {
            PictureBox p1 = new PictureBox();
            ProgressBar hp = new ProgressBar();
            hp.Value = 100;
            hp.Size = new Size(100, 10);
            p1.BackgroundImage = Game_GUI.Properties.Resources.fire_downward;
            PictureBox p2 = new PictureBox();

            p2.BackgroundImage = null;
            p1.BackgroundImageLayout = ImageLayout.Stretch;
            //   p2.BackgroundImageLayout = ImageLayout.Stretch;
            if(enemy_type=="Player_bomb")
            {
                p1.BackgroundImage = Game_GUI.Properties.Resources.bomb;
                p1.BackgroundImageLayout = ImageLayout.Stretch;
                cell f = Grid.Get_cell(c.X - 10, c.Y);
            }
            if(enemy_type=="Player")
            {
                p1.BackgroundImage = Game_GUI.Properties.Resources.fire_upward;
                p1.BackgroundImageLayout = ImageLayout.Stretch;
                cell f = Grid.Get_cell(c.X - 10, c.Y);
            }
            if (enemy_type == "Enemy")
            {
                cell f = Grid.Get_cell(c.X + 10, c.Y);
            }
            else if (enemy_type == "Enemy1")
            {
                cell f = Grid.Get_cell(c.X + 1, c.Y);
            }
            c.Obj = 'B';
            c.P = p1;
            c.P.BackgroundImageLayout = ImageLayout.Stretch;
            c.P.Size = new Size(30, 30);
            c.P.BackColor = Color.Transparent;
            bullet b = new bullet(p1, p2, 100, objtype.Enemy, 'B', c, false,hp);
            b.C.P.Left = b.C.Y * 20;
            b.C.P.Top = b.C.X * 30;
            Action a = null;
            a = delegate { this.Controls.Add(b.Main); };

            if (this.IsHandleCreated)
            {
                this.BeginInvoke(a);
            }
            return b;
        }
        






        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            int x = 0;
            bool move_down = false;
            bool move_right = false;
            Player p = e.Argument as Player;
            while (true)
            {
                if (Keyboard.IsKeyPressed(Key.UpArrow))
                {
                    move_down = false;
                    p.V_move(p, ref move_down);
                }
                if (Keyboard.IsKeyPressed(Key.LeftArrow))
                {
                    move_right = false;
                    p.H_move(p, ref move_right);
                }
                if (Keyboard.IsKeyPressed(Key.RightArrow))
                {
                    move_right = true;
                    p.H_move(p, ref move_right);
                }
                 
                 if(Keyboard.IsKeyPressed(Key.DownArrow))
                {
                    move_down = true;
                    p.V_move(p, ref move_down);
                }
                 if(Keyboard.IsKeyPressed(Key.Space))
                {
                    if (bullet.player_bullets.Count <= 5)
                    {
                        bullet b = Fire(p.C, "Player");
                        bullet.player_bullets.Add(b);
                    }
                }
                 if(Keyboard.IsKeyPressed(Key.X))
                {
                    if (p.Level_up == true && bullet.bomb==null)
                    {
                        bullet b = Fire(p.C, "Player_bomb");
                        bullet.bomb = b;
                    }

                }
                    Thread.Sleep(100);
            }

        }

        private void timer2_Tick(Player p, Enemy1 e)
        {
            if (e.HP.Value > 0)
            {
                bool value = false;
                for (int x = 0; x < bullet.bullets1.Count; x++)
                {
                    value = bullet.V_move(bullet.bullets1[x]);
                    if (value == true)
                    {
                        bullet.bullets1[x].Main.Top += 31;
                        if (bullet.bullets1[x].Main.Bounds.IntersectsWith(p.Main.Bounds) && p.HP.Value>0)
                        {
                            p.Health -= 10;
                            p.HP.Value -= 10;
                            this.Controls.Remove(bullet.bullets1[x].Main);
                            bullet.bullets1.Remove(bullet.bullets1[x]);
                        }
                    }
                    else if(p.HP.Value==0)
                    {
                        p.Main.Image = Game_GUI.Properties.Resources.boom_removebg_preview;
                        
                        this.Controls.Remove(bullet.bullets1[x].Main);
                        bullet.bullets1.Remove(bullet.bullets1[x]);
                        Thread.Sleep(50);
                        this.Controls.Remove(p.Main);
                    }
                    else
                    {
                        this.Controls.Remove(bullet.bullets1[x].Main);
                        bullet.bullets1.Remove(bullet.bullets1[x]);
                    }
                }
            }
            else
            {
                timer1.Enabled = false;
                timer1.Stop();
            }
        }

      

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Enemy E = e.Argument as Enemy;
            int count = 0;
            bool move_right = true;
            bool move_down = true;
            while (true)
            {
                if (E.HP.Value == 0)
                {
                    e.Cancel = true;
                    //this.backgroundWorker1.CancelAsync();
                }
                if (E.HP.Value > 0)
                {
                    count++;
                    Thread.Sleep(100);

                    E.H_move(E, ref move_right);
                    if (count == 10)
                    {
                        if (bullet.bullets.Count <= 3)
                        {
                            bullet b = Fire(E.C, "Enemy");
                            count = 0;
                            bullet.bullets.Add(b);

                        }
                    }
                }

            }

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Enemy1 E1 = e.Argument as Enemy1;
            int count = 0;
            bool move_right = true;
            while (true)
            {
                if (E1.HP.Value == 0)
                {
                    e.Cancel = true;
                    //this.backgroundWorker1.CancelAsync();
                }
                if (E1.HP.Value > 0)
                {
                    count++;
                    Thread.Sleep(100);

                    E1.H_move(E1, ref move_right);
                    if (count == 25)
                    {
                        if (bullet.bullets.Count <= 3)
                        {
                            bullet b = Fire(E1.C, "Enemy1");
                            count = 0;

                            bullet.bullets1.Add(b);
                        }
                    }
                }

            }
        }

        private void timer1_Tick(Player p,Enemy e)
        {
            if (e.HP.Value > 0)
            {
                bool value = false;
                for (int x = 0; x < bullet.bullets.Count; x++)
                {
                    value = bullet.V_move(bullet.bullets[x]);
                    if (value == true)
                    {
                        bullet.bullets[x].Main.Top += 31;
                        if (x < bullet.bullets.Count)
                        {
                            if (bullet.bullets[x].Main.Bounds.IntersectsWith(p.Main.Bounds)&& p.HP.Value>0)
                            {
                                p.Health -= 10;
                                p.HP.Value -= 10;
                                this.Controls.Remove(bullet.bullets[x].Main);
                                bullet.bullets.Remove(bullet.bullets[x]);
                            }
                            else if(p.HP.Value==0)
                            {
                                p.Main.Image = Game_GUI.Properties.Resources.boom_removebg_preview;
                                this.Controls.Remove(bullet.bullets[x].Main);
                                bullet.bullets.Remove(bullet.bullets[x]);
                                Thread.Sleep(50);
                                this.Controls.Remove(p.Main);
                            }
                         
                        }
                    }
                    else
                    {

                        this.Controls.Remove(bullet.bullets[x].Main);
                        bullet.bullets.Remove(bullet.bullets[x]);
                    }

                }
            }
            else
            {
                timer1.Enabled = false;
                timer1.Stop();
            }
        }

       

        private void timer3_Tick(Player p,Enemy E,Enemy1 E1,ref bool E_alive,ref bool E1_alive, ref bool game_over)
        {
               
            bool value = false;
            for (int x = 0; x < bullet.player_bullets.Count; x++)
            {
                value = Player.fire_move(bullet.player_bullets[x]);
                if (value == true)
                {
                    if (x < bullet.player_bullets.Count)
                    {
                        bullet.player_bullets[x].Main.Top -= 31;
                        if (bullet.player_bullets[x].Main.Bounds.IntersectsWith(E.Main.Bounds) && E.HP.Value > 0 && E_alive==true)
                        {
                            E.Health -= 10;
                            E.HP.Value -= 10;
                            this.Controls.Remove(bullet.player_bullets[x].Main);
                            bullet.player_bullets.Remove(bullet.player_bullets[x]);
                        }
                        else if(E.HP.Value == 0 && E_alive == true)
                        {
                            E.Main.BackgroundImage = Game_GUI.Properties.Resources.boom_removebg_preview;
                            this.Controls.Remove(bullet.player_bullets[x].Main);
                            this.Controls.Remove(bullet.player_bullets[x].Main);
                            bullet.player_bullets.Remove(bullet.player_bullets[x]);
                            Thread.Sleep(50);
                            this.Controls.Remove(E.Main);
                            this.Controls.Remove(E.HP);
                            E_alive = false;
                        }
                    }
                    if (x < bullet.player_bullets.Count)
                    {
                        if (bullet.player_bullets[x].Main.Bounds.IntersectsWith(E1.Main.Bounds) && E1.HP.Value > 0 && E1_alive==true)
                        {
                            E1.Health -= 10;
                            E1.HP.Value -= 10;
                            this.Controls.Remove(bullet.player_bullets[x].Main);
                            bullet.player_bullets.Remove(bullet.player_bullets[x]);
                        }
                        else if (E1.HP.Value == 0 && E1_alive == true)
                        {
                            E1.Main.BackgroundImage = Game_GUI.Properties.Resources.boom_removebg_preview;
                            this.Controls.Remove(bullet.player_bullets[x].Main);
                            this.Controls.Remove(bullet.player_bullets[x].Main);
                            bullet.player_bullets.Remove(bullet.player_bullets[x]);
                            Thread.Sleep(50);
                            this.Controls.Remove(E1.Main);
                            this.Controls.Remove(E1.HP);
                            E1_alive = false;
                        }

                    }
                }
                else
                {

                    this.Controls.Remove(bullet.player_bullets[x].Main);
                    bullet.player_bullets.Remove(bullet.player_bullets[x]);
                }
                
            }
            if ((E_alive == false || E1_alive == false) && p.Level_up == false)
            {
                Form3 f3 = new Form3();
                f3.Show();
                p.Level_up = true;
            }
            if (p.HP.Value == 0 && game_over==false)
            {
                Form2 f2 = new Form2();
                f2.Show();
                game_over = true;
            }
            if(E1_alive==false && E_alive==false && game_over==false)
            {
                Form2 f2 = new Form2();
                f2.Show();
                game_over = true;
                
            }
        }

        private void timer4_Tick(Player p, Enemy E, Enemy1 E1, ref bool E_alive, ref bool E1_alive,ref bool game_over)
        {
           
            bool value=false;
            if(bullet.bomb!=null)
            {
                value = Player.fire_move(bullet.bomb);
                if (value == true)
                {
                        bullet.bomb.Main.Top -= 31;
                        if (bullet.bomb.Main.Bounds.IntersectsWith(E.Main.Bounds) && E.HP.Value > 0 && E_alive == true)
                        {
                            E.Health -= 20;
                            E.HP.Value -= 20;
                            this.Controls.Remove(bullet.bomb.Main);
                        bullet.bomb = null;
                        }
                        else if (E.HP.Value == 0 && E_alive == true)
                        {
                            E.Main.BackgroundImage = Game_GUI.Properties.Resources.boom_removebg_preview;
                            this.Controls.Remove(bullet.bomb.Main);
                            this.Controls.Remove(bullet.bomb.Main);
                            bullet.bomb=null;
                            Thread.Sleep(50);
                            this.Controls.Remove(E.Main);
                            this.Controls.Remove(E.HP);
                            E_alive = false;
                        }
                    
                        if (bullet.bomb.Main.Bounds.IntersectsWith(E1.Main.Bounds) && E1.HP.Value > 20 && E1_alive == true)
                        {
                            E1.Health -= 20;
                            E1.HP.Value -= 20;
                            this.Controls.Remove(bullet.bomb.Main);
                             bullet.bomb = null;
                        }
                        else if (E1.HP.Value == 0 && E1_alive == true)
                        {
                            E1.Main.BackgroundImage = Game_GUI.Properties.Resources.boom_removebg_preview;
                            this.Controls.Remove(bullet.bomb.Main);
                            this.Controls.Remove(bullet.bomb.Main);
                            bullet.bomb=null;
                            Thread.Sleep(50);
                            this.Controls.Remove(E1.Main);
                            this.Controls.Remove(E1.HP);
                            E1_alive = false;
                        }

                    
                }
                else
                {

                    this.Controls.Remove(bullet.bomb.Main);
                    bullet.bomb = null;
                }

            }
            if ((E_alive == false || E1_alive == false) && p.Level_up == false)
            {
                Form3 f3 = new Form3();
                f3.Show();
                p.Level_up = true;
                E.HP.Value = 100;
                E1.HP.Value = 100;
            }
            if (p.HP.Value == 0 && game_over==false)
            {
                Form2 f2 = new Form2();
                f2.Show();
                game_over = true;
            }
        }
        
    }
}
