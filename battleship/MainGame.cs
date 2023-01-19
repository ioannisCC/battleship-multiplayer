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
    public partial class MainGame : Form
    {
        static Grid grid1 = new Grid(10);
        static Grid grid2 = new Grid(10);
        public Button[,] btnGrid1 = new Button[grid1.Size,grid1.Size];
        public Button[,] btnGrid2 = new Button[grid2.Size, grid2.Size];
        int counter1 = 0, counter2 = 0, counter3 = 0, counter4 = 0;
        Point offset;
        Point mousePosition;

        public MainGame()
        {
            InitializeComponent();
            /* VERY IMPORTANT */
            pictureBoxShip5.MouseDown += new MouseEventHandler(pictureBoxShip5_MouseDown);
            pictureBoxShip5.MouseMove += new MouseEventHandler(pictureBoxShip5_MouseMove);

        }

        private static Cell setCurrentCell(Grid g)
        {
            //get current row number
            int currentRow = 0;
            //get current column number
            int currentColumn = 0;
            g.theGrid[currentRow, currentColumn].CurrentlyOccupied = true;
            return g.theGrid[currentRow, currentColumn];
        }

        
        Cell currentCell1 = setCurrentCell(grid1);
        Cell currentCell2 = setCurrentCell(grid2);

        private void MainGame_Load(object sender, EventArgs e)
        {
            populateGrid(2,panel1,grid1,btnGrid1);
            pictureBoxShip5.AllowDrop = true;
        }

        private void populateGrid(int offset, Panel panel, Grid grid, Button[,] btnGrid)
        {
            int buttonSize = panel.Width/offset / grid.Size;
            panel.Height = panel.Width;

            for (int i = 0; i < grid.Size; i++)
            {
                for (int j = 0; j < grid.Size; j++)
                {
                    btnGrid[i, j] = new Button();
                    btnGrid[i, j].Height = buttonSize;
                    btnGrid[i, j].Width = buttonSize;
                    btnGrid[i, j].AllowDrop = true;

                    btnGrid[i, j].Click += Grid_Button_Click;

                    panel.Controls.Add(btnGrid[i, j]);
                    btnGrid[i, j].Location = new Point(i * buttonSize, j * buttonSize);
                    btnGrid[i,j].BackColor = Color.DarkGray;


                   
                    btnGrid[i, j].Text = i + "|" + j;
                }
            }
        }

        private void Grid_Button_Click(object sender, EventArgs e)
        {
            //
        }

        private void MainGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        void Check_DragDrop(PictureBox pictureBox)
        {
            for (int i = 0; i < grid1.Size; i++)
            {
                for (int j = 0; j < grid1.Size; j++)
                {
                    if (pictureBox.Bounds.IntersectsWith(btnGrid1[i, j].Bounds))
                    {
                        MessageBox.Show(btnGrid1[i, j].Text);
                    }
                }
            }
            /*foreach (var btn in btnGrid)
            {
                if (pictureBox.Bounds.IntersectsWith(btn.Bounds))
                    MessageBox.Show(btn.Text);
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Width = panel1.Width / 2;
            populateGrid(1, panel2, grid2, btnGrid2);
            /*int buttonSize2 = panel2.Width / grid2.Size;
            panel2.Height = panel2.Width;

            for (int i = 0; i < grid2.Size; i++)
            {
                for (int j = 0; j < grid2.Size; j++)
                {
                    btnGrid2[i, j] = new Button();
                    btnGrid2[i, j].Height = buttonSize2;
                    btnGrid2[i, j].Width = buttonSize2;
                    btnGrid2[i, j].AllowDrop = true;

                    btnGrid2[i, j].Click += Grid_Button_Click;

                    panel2.Controls.Add(btnGrid2[i, j]);
                    btnGrid2[i, j].Location = new Point(i * buttonSize2, j * buttonSize2);
                    btnGrid2[i, j].BackColor = Color.DarkGray;



                    btnGrid2[i, j].Text = i + "|" + j;
                }
            }*/
            Check_DragDrop(pictureBoxShip5);
            Check_DragDrop(pictureBoxShip4);
            Check_DragDrop(pictureBoxShip3);
            Check_DragDrop(pictureBoxShip2);
        }

        void Location_Offset(MouseEventArgs e, PictureBox pictureBox)
        {
            if (e.Button == MouseButtons.Left)
            {
                offset = e.Location;
                mousePosition = e.Location;
                pictureBox.Cursor = Cursors.Hand;
                pictureBox.Focus();
            }
        }

        //mousePosition added so the picturebox will update its location on a thrshold of 5 so it does not glitch
        void Transition_Glitch(MouseEventArgs e, PictureBox pictureBox)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Math.Abs(e.X - mousePosition.X) > 5 || Math.Abs(e.Y - mousePosition.Y) > 5)
                {
                    pictureBox.Left = e.X + pictureBox.Left - offset.X;
                    pictureBox.Top = e.Y + pictureBox.Top - offset.Y;
                    mousePosition = e.Location;
                    pictureBox.Focus();
                }
            }
        }

        void Rotate(int counter, PictureBox pictureBox)
        {
            counter++;
            Image image = pictureBox.Image;
            image.RotateFlip(RotateFlipType.Rotate90FlipXY);
            pictureBox.Image = image;
            if ((counter % 2) == 0)
            {
                pictureBox.Width = pictureBox.Image.Width;
                pictureBox.Height = pictureBox.Image.Height;
            }
            else
            {
                pictureBox.Width = pictureBox.Image.Width;
                pictureBox.Height = pictureBox.Image.Height;
            }
        }

        private void pictureBoxShip5_MouseDown(object sender, MouseEventArgs e)
        {
            Location_Offset(e, pictureBoxShip5);
        }

        private void pictureBoxShip5_MouseMove(object sender, MouseEventArgs e)
        {
            Transition_Glitch(e, pictureBoxShip5);
        }

        private void pictureBoxShip5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Rotate(counter1, pictureBoxShip5);
        }

        private void pictureBoxShip4_MouseDown(object sender, MouseEventArgs e)
        {
            Location_Offset(e,pictureBoxShip4);
        }

        private void pictureBoxShip4_MouseMove(object sender, MouseEventArgs e)
        {
            Transition_Glitch(e,pictureBoxShip4);
        }

        private void pictureBoxShip4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Rotate(counter2, pictureBoxShip4);
        }

        private void pictureBoxShip3_MouseDown(object sender, MouseEventArgs e)
        {
            Location_Offset(e, pictureBoxShip3);
        }

        private void pictureBoxShip3_MouseMove(object sender, MouseEventArgs e)
        {
            Transition_Glitch(e, pictureBoxShip3);
        }

        private void pictureBoxShip3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Rotate(counter3, pictureBoxShip3);
        }

        private void pictureBoxShip2_MouseDown(object sender, MouseEventArgs e)
        {
            Location_Offset(e,pictureBoxShip2);
        }

        private void pictureBoxShip2_MouseMove(object sender, MouseEventArgs e)
        {
            Transition_Glitch(e,pictureBoxShip2);
        }

        private void pictureBoxShip2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Rotate(counter4, pictureBoxShip2);
        }

    }
}
