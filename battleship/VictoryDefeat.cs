using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleship
{
    public partial class VictoryDefeat : Form
    {
        bool victory = false;

        public VictoryDefeat(bool vitory)
        {
            InitializeComponent();
            this.victory = vitory;
        }

        private void VictoryDefeat_Load(object sender, EventArgs e)
        {
            if (victory)
                pictureBoxVictoryDefeat.ImageLocation = "victory.png";
            else
                pictureBoxVictoryDefeat.ImageLocation = "defeat.png";
        }
    }
}
