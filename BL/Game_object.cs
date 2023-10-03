using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman.BL
{
  public  class Game_object
    {

        private char character;
        private cell c;
        private objtype t;
        private char previous_obj;
        private int score;
        public Game_object()
        {

        }
        public Game_object(objtype t, char character, cell c)
        {
            this.t = t;
            this.c = c;
            this.character = character;
        }
        public Game_object(objtype t, char character, cell c, int score)
        {
            this.Score = score;
            this.t = t;
            this.c = c;
            this.character = character;
        }
        public char Character { get => character; set => character = value; }
        public int Score { get => score; set => score = value; }
        public char Previous_obj { get => previous_obj; set => previous_obj = value; }
        internal cell C { get => c; set => c = value; }
        internal objtype T { get => t; set => t = value; }

        public static objtype GetObjtype(char character)
        {
            if (character == 'G')
                return objtype.Enemy;
            else if (character == 'P')
            {
                return objtype.Player;
            }
            else if (character == '.')
                return objtype.Reward;
            else if (character == ' ')
                return objtype.None;
            else
                return objtype.Wall;
        }


    }
}
