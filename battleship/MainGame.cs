using MongoDB.Bson;
using MongoDB.Driver;
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
<<<<<<< HEAD
    //test
=======
>>>>>>> d8c200267686ddb2df5c8b1a173d368bf28d7fd3
{   /* username: battleshipGame
     * password: unipi */
    public partial class MainGame : Form
    {
        static Grid grid1 = new Grid(10);
        static Grid grid2 = new Grid(10);
        public Button[,] btnGrid1 = new Button[grid1.Size, grid1.Size];
        public Button[,] btnGrid2 = new Button[grid2.Size, grid2.Size];
        int counter1 = 0, counter2 = 0, counter3 = 0, counter4 = 0;
        public bool stopDragDrop = true, wait = true;
        bool exist = false;
        Point offset;
        Point mousePosition;
        MongoClient dbClient;
        public static string name;

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
            dbClient = new MongoClient("mongodb+srv://battleshipGame:unipi@cluster0.f3k5ehu.mongodb.net/?retryWrites=true&w=majority");
            Initialize_Database();
            populateGrid(2, panel1, grid1, btnGrid1);
            pictureBoxShip5.AllowDrop = true;            
        }
        
        private void Wait()
        {
            while(wait)
            {

            }
        }

        private void Initialize_Database()
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");
            var updateX = Builders<BsonDocument>.Update.Set("x", 0);
            var updateY = Builders<BsonDocument>.Update.Set("y", 0);
            var updateTmp = Builders<BsonDocument>.Update.Set("tmp", 0);
            var updateP1 = Builders<BsonDocument>.Update.Set("p1", true);
            var updateP2 = Builders<BsonDocument>.Update.Set("p2", true);
            collection.UpdateOne(filter, updateX);
            collection.UpdateOne(filter, updateY);
            collection.UpdateOne(filter, updateTmp);
            collection.UpdateOne(filter, updateP1);
            collection.UpdateOne(filter, updateP2);
        }

        private void Pull_Player_Choice()
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            BsonDocument document = collection.Find(new BsonDocument()).FirstOrDefault();
            /* to kanoume epidh den jeroume pws na paroume to value twn pi, p2 kateyueian */
            string[] words = document.ToString().Split(',');
            string[] tempP = words[4].Split(':');
            string p1 = tempP[1].Substring(1, tempP[1].Length-1);
            tempP = words[5].Split(':');
            string p2 = tempP[1].Substring(1, tempP[1].Length-3);

            if (p1 == "false")
            {
                pictureBoxPlayer1.Hide();
                textBox1.Hide();
                MessageBox.Show("douleuei1");
            }
            else if (p2 == "false")
            {
                pictureBoxPlayer2.Hide();
                textBox2.Hide();
                MessageBox.Show("douleuei2");
            }

            if (p1 == "false" && p2 == "false") timer_Pull.Stop();
        }

        private void timer_Pull_Tick(object sender, EventArgs e)
        {
            Pull_Player_Choice();
        }

        private void Push_Player_Choice(string p)
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");
            var updateP = Builders<BsonDocument>.Update.Set(p, false);
            collection.UpdateOne(filter, updateP);
        }

        private void populateGrid(int offset, Panel panel, Grid grid, Button[,] btnGrid)
        {
            int buttonSize = (panel.Width / offset) / grid.Size;  /* we needed offset because panel sizes are difernet each time */
            panel.Height = panel.Width;

            for (int i = 0; i < grid.Size; i++)
            {
                for (int j = 0; j < grid.Size; j++)
                {
                    btnGrid[i, j] = new Button();
                    btnGrid[i, j].Height = buttonSize;
                    btnGrid[i, j].Width = buttonSize;
                    btnGrid[i, j].AllowDrop = true;

                    if (exist)
                        btnGrid2[i, j].Click += new EventHandler(Grid_Button_Click);

                    panel.Controls.Add(btnGrid[i, j]);
                    btnGrid[i, j].Location = new Point(i * buttonSize, j * buttonSize);
                    //btnGrid[i, j].BackColor = Color.Transparent;
                    //btnGrid[i, j].ForeColor = Color.Transparent;
                    btnGrid[i, j].UseVisualStyleBackColor = true;
                    //btnGrid[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(100, Color.Black);

                    btnGrid[i, j].Text = i + "|" + j;
                }
            }
        }

        private void depopulateGrid(Grid grid, Button[,] btnGrid)
        {
            for (int i = 0; i < grid.Size; i++)
            {
                for (int j = 0; j < grid.Size; j++)
                {
                    btnGrid[i, j].Hide();
                }
            }
        }

        private void Grid_Button_Click(object sender, EventArgs e)
        {
            Button clicked = sender as Button; /* as operator functions as cast */
            //clicked.BackColor = Color.Red;
            clicked.Image = Image.FromFile("png-transparent-explosion-animation-sprite-blast-orange-special-effects-particle-system-thumbnail.png");
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");
            var updateX = Builders<BsonDocument>.Update.Set("x", Int32.Parse(clicked.Text.Substring(0, 1)));
            var updateY = Builders<BsonDocument>.Update.Set("y", Int32.Parse(clicked.Text.Substring(clicked.Text.Length-1)));
            collection.UpdateOne(filter, updateX);
            collection.UpdateOne(filter, updateY);
        }

        private void MainGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /* check with many cells on the grid the pictuebox is colliding */
        private int Check_DragDrop(PictureBox pictureBox)
        {
            int btnMultitude = 0;
            for (int i = 0; i < grid1.Size; i++)
            {
                for (int j = 0; j < grid1.Size; j++)
                {
                    if (pictureBox.Bounds.IntersectsWith(btnGrid1[i, j].Bounds))
                    {
                        btnMultitude++;
                        grid1.theGrid[i, j].CurrentlyOccupied = true;
                    }
                }
            }
            return btnMultitude;
        }

        /* check if each ship placed correctly (number of cells occupying) */
        private bool Check_Positioning(PictureBox pictureBox, int multitude, string name)
        {
            if (Check_DragDrop(pictureBox) != multitude)
            {
                MessageBox.Show("wrong positioning on ship " + name);
                return false;
            }
            else
            {
                grid1.ClearGrid();
                Check_DragDrop(pictureBoxShip5);
                Check_DragDrop(pictureBoxShip4);
                Check_DragDrop(pictureBoxShip3);
                Check_DragDrop(pictureBoxShip2);
                return true;
            }
        }

        /* check positioning and start game */
        private void button1_Click(object sender, EventArgs e)
        {
            if (!(Check_Positioning(pictureBoxShip5, 5, "aircraft carrier") &&
            Check_Positioning(pictureBoxShip4, 4, "destroyer") &&
            Check_Positioning(pictureBoxShip3, 3, "minesweeper") &&
            Check_Positioning(pictureBoxShip2, 2, "submarine")))
            { }
            else
            {
                exist = true;
                stopDragDrop = false;
                /*foreach (var ocp in grid1.theGrid)
                {
                    if (ocp.CurrentlyOccupied)
                        //MessageBox.Show(ocp.ToString());
                }*/
                panel1.Width = panel1.Width / 2;
                populateGrid(1, panel2, grid2, btnGrid2);
                //depopulateGrid(grid1, btnGrid1);
                /*PictureBox pictureBoxSea = new PictureBox();
                pictureBoxSea.Size = panel1.Size;
                pictureBoxSea.Location = panel1.Location;
                pictureBoxSea.ImageLocation = "istockphoto-1385726058-612x612.jpg";
                pictureBoxSea.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxSea.BringToFront();
                Controls.Add(pictureBoxSea);*/
            }
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

        /* mousePosition added so the picturebox will update its location on a thrshold of 5 so it does not glitch */
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

        /* rotates 90 degrees vertically the picturebox */
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

        /* movement & rotation for each piscturebox start */
        private void pictureBoxShip5_MouseDown(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Location_Offset(e, pictureBoxShip5);
        }

        private void pictureBoxShip5_MouseMove(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Transition_Glitch(e, pictureBoxShip5);
        }

        private void pictureBoxShip5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Rotate(counter1, pictureBoxShip5);
        }

        private void pictureBoxShip4_MouseDown(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Location_Offset(e,pictureBoxShip4);
        }

        private void pictureBoxShip4_MouseMove(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Transition_Glitch(e,pictureBoxShip4);
        }

        private void pictureBoxShip4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Rotate(counter2, pictureBoxShip4);
        }

        private void pictureBoxShip3_MouseDown(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Location_Offset(e, pictureBoxShip3);
        }

        private void pictureBoxShip3_MouseMove(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Transition_Glitch(e, pictureBoxShip3);
        }

        private void pictureBoxShip3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Rotate(counter3, pictureBoxShip3);
        }

        private void pictureBoxShip2_MouseDown(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Location_Offset(e,pictureBoxShip2);
        }

        private void pictureBoxShip2_MouseMove(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Transition_Glitch(e,pictureBoxShip2);
        }

        private void pictureBoxShip2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Rotate(counter4, pictureBoxShip2);
        }

        /* movement & rotation for each piscturebox end */

        private void pictureBoxPlayer1_Click(object sender, EventArgs e)
        {
            pictureBoxPlayer1.Hide();
            pictureBoxPlayer1.Controls.Remove(pictureBoxPlayer1);
            pictureBoxPlayer2.Hide();
            pictureBoxPlayer2.Controls.Remove(pictureBoxPlayer2);
            textBox2.Hide();
            textBox2.Controls.Remove(textBox2);
            Push_Player_Choice("p1");
        }

        private void pictureBoxPlayer2_Click(object sender, EventArgs e)
        {
            pictureBoxPlayer2.Hide();
            pictureBoxPlayer2.Controls.Remove(pictureBoxPlayer1);
            pictureBoxPlayer1.Hide();
            pictureBoxPlayer1.Controls.Remove(pictureBoxPlayer2);
            textBox1.Hide();
            textBox1.Controls.Remove(textBox1);
            Push_Player_Choice("p2");
        }
    }
}
