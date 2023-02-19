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

        public VictoryDefeat(bool victory)
        {
            InitializeComponent();
            this.victory = victory;
        }

        private void VictoryDefeat_Load(object sender, EventArgs e)
        {
            pictureBoxVictory.Hide();
            pictureBoxDefeat.Hide();
            Determine_PictureBox(victory);
        }

        private void Determine_PictureBox(bool victory)
        {
            if (victory)
            { 
                pictureBoxVictory.Show();
                MessageBox.Show("inside true if");
            }
            else
            {
                pictureBoxDefeat.Show();
                MessageBox.Show("inside false if");
            }
        }
    }
}
