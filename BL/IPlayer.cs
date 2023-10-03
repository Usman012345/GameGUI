using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pacman.BL;

namespace Game_GUI.BL
{
    interface IPlayer
    {
        void V_move(Game_object G,ref bool move_down);
        bool collision();
        void fire_missle();
    }
}
