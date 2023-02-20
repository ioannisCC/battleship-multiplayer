using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleship
{
    public class Player
    {
        private int player;
        private string fieldName;
        private int winsP;
        private int stage;
        private string name;

        public Player(string fieldName, int player, int winsP, int stage, string name)
        {
            this.fieldName = fieldName;
            this.player = player;
            this.winsP = winsP;
            this.stage = stage;
            this.name = name;
        }

        public void P(Player player, int stage, Timer timer)
        {
            stage = player.stage;
            timer.Start();
        }

        public int playerGetSet
        {
            get { return player; }
            set { player = value; }
        }
        public int stageGetSet
        {
            get { return stage; }
            set { stage = value; }
        }
        public string nameGetSet
        {
            get { return name; }
            set { name = value; }
        }
        public string fieldNameGetSet
        {
            get { return fieldName; }
            set { fieldName = value; }
        }
        public int winsGetSet
        {
            get { return winsP; }
            set { winsP = value; }
        }
    }
}
