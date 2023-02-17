using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleship
{   /* username: battleshipGame
     * password: unipi */
    public partial class MainGame : Form
    {
        int steps = 0;
        int player = 0;
        int stage = 1;
        int turn = 1;
        int oldX = 0, oldY = 0;
        int steps = 0;
        int timerVar;
        bool allowClick = true;
        bool PictureBoxPlayer1 = true; //true means available 
        bool PictureBoxPlayer2 = true;
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
            Bitmap bm = new Bitmap(new Bitmap("scope.png"), 50, 50);
            Cursor = new Cursor(bm.GetHicon());
            //  populateGrid(2, panel1, grid1, btnGrid1);
            pictureBoxShip5.AllowDrop = true;
            pictureBoxShip2.Hide();
            pictureBoxShip3.Hide();
            pictureBoxShip4.Hide();
            pictureBoxShip5.Hide();
            buttonStart.Hide();
        }

        private void Initialize_Database()
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");
            var updateX = Builders<BsonDocument>.Update.Set("x", 0);
            var updateY = Builders<BsonDocument>.Update.Set("y", 0);
            var updateP1 = Builders<BsonDocument>.Update.Set("p1", true);
            var updateP2 = Builders<BsonDocument>.Update.Set("p2", true);
            var updateP1Name = Builders<BsonDocument>.Update.Set("p1Name", "");
            var updateP2Name = Builders<BsonDocument>.Update.Set("p2Name", "");
            var updateP1Ready = Builders<BsonDocument>.Update.Set("p1Ready", false);
            var updateP2Ready = Builders<BsonDocument>.Update.Set("p2Ready", false);
            var updateX2 = Builders<BsonDocument>.Update.Set("x2", 0);
            var updateY2 = Builders<BsonDocument>.Update.Set("y2", 0);
            var updateHit = Builders<BsonDocument>.Update.Set("hit", false);

            collection.UpdateOne(filter, updateX);
            collection.UpdateOne(filter, updateY);
            collection.UpdateOne(filter, updateHit);
            collection.UpdateOne(filter, updateP1);
            collection.UpdateOne(filter, updateP2);
            collection.UpdateOne(filter, updateP1Name);
            collection.UpdateOne(filter, updateP2Name);
            collection.UpdateOne(filter, updateP1Ready);
            collection.UpdateOne(filter, updateP2Ready);
            collection.UpdateOne(filter, updateX2);
            collection.UpdateOne(filter, updateY2);

        }

        private void Pull_Player_Choice()
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            BsonDocument document = collection.Find(new BsonDocument()).FirstOrDefault();
            bool p1 = document["p1"].AsBoolean;
            bool p2 = document["p2"].AsBoolean;

            if (p1 == false && p2 == false) //if both players have been chosen start the game
            {
                populateGrid(2, panel1, grid1, btnGrid1);
                pictureBoxPlayer1.Hide();
                pictureBoxPlayer2.Hide();
                timer_Pull.Stop();
            }
            else if (p1 == false)
            {
                // pictureBoxPlayer1.Hide();
                pictureBoxPlayer1.Controls.Remove(pictureBoxPlayer1);
                pictureBoxPlayer1.ImageLocation = "Captain1checked.png";
                PictureBoxPlayer1 = false;
            }
            else if (p2 == false)
            {
                //pictureBoxPlayer2.Hide();
                pictureBoxPlayer2.Controls.Remove(pictureBoxPlayer2);
                pictureBoxPlayer2.ImageLocation = "Captain2checked.png";
                PictureBoxPlayer2 = false;
            }

        }

        private void CheckGameStatus()
        {
            int greyCounter = 0;
            int redCounter = 0;
            for (int i = 0; i < grid1.Size; i++)
                for (int j = 0; j < grid1.Size; j++)
                    if (grid1.theGrid[i, j].CurrentlyOccupied && btnGrid1[i, j].BackColor == Color.Gray)
                        greyCounter++;

            for (int i = 0; i < grid2.Size; i++)
                for (int j = 0; j < grid2.Size; j++)
                    if (btnGrid2[i, j].BackColor == Color.Red)
                        redCounter++;

            if (greyCounter == 14 && player == 1)
            {
                timer_Pull.Stop();
                MessageBox.Show("captain Croitor wins");
            }
            else if (greyCounter == 14 && player == 2)
            {
                timer_Pull.Stop();
                timer.Stop();
                MessageBox.Show("captain Jack wins");
            }
            else if (redCounter == 14 && player == 1)
            {
                timer_Pull.Stop();
                timer.Stop();
                MessageBox.Show("captain jack wins");
            }
            else if (redCounter == 14 && player == 2)
            {
                timer_Pull.Stop();
                timer.Stop();
                MessageBox.Show("captain croitor wins");
            }

        }

        private void CheckGameStatus()
        {
            int greyCounter = 0;
            int redCounter = 0;
            for (int i = 0; i < grid1.Size; i++)
                for (int j = 0; j < grid1.Size; j++)
                    if (grid1.theGrid[i,j].CurrentlyOccupied && btnGrid1[i,j].BackColor == Color.Gray)
                        greyCounter++;

            for (int i = 0; i < grid2.Size; i++)
                for (int j = 0; j < grid2.Size; j++)
                    if (btnGrid2[i, j].BackColor == Color.Red)
                        redCounter++;

            if (greyCounter == 14 && player == 1)
            {
                MessageBox.Show("captain croitor wins");
                timer_Pull.Stop();
                timer.Stop();
            }
            else if (greyCounter == 14 && player == 2)
            {
                MessageBox.Show("captain jack wins");
                timer_Pull.Stop();
                timer.Stop();
            }
            else if (redCounter == 14 && player == 1)
            {
                MessageBox.Show("captain jack wins");
                timer_Pull.Stop();
                timer.Stop();
            }
            else if (redCounter == 14 && player == 2)
            {
                MessageBox.Show("captain croitor wins");
                timer_Pull.Stop();
                timer.Stop();
            }
        }

        private void timer_Pull_Tick(object sender, EventArgs e)
        {

            if (stage == 1)
            {
                Pull_Player_Choice();
                if (!PictureBoxPlayer1)     //gia na menei topika kitrino
                    pictureBoxPlayer1.ImageLocation = "Captain1hover.png";
                if (!PictureBoxPlayer2)
                    pictureBoxPlayer2.ImageLocation = "Captain2hover.png";
            }
            else if (stage == 2)
                Pull_ReadyP();
            else
                timer_Pull.Stop();
        }

        private bool Color_Buttons()
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            BsonDocument document = collection.Find(new BsonDocument()).FirstOrDefault();

            return document["hit"].AsBoolean;
        }

        private bool Enabled_Buttons()
        {
            foreach (Button button in btnGrid2)
            {
                if (button.Enabled == true)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private void Push_Player_Choice(string p, string fieldName, string name)
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");
            var updateP = Builders<BsonDocument>.Update.Set(p, false);
            var updatePName = Builders<BsonDocument>.Update.Set(fieldName, name);
            collection.UpdateOne(filter, updateP);
            collection.UpdateOne(filter, updatePName);
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
                    btnGrid[i, j].ForeColor = Color.White;

                    //  btnGrid[i, j].Hide();
                }
            }

            pictureBoxShip2.Show();
            pictureBoxShip3.Show();
            pictureBoxShip4.Show();
            pictureBoxShip5.Show();
            buttonStart.Show();
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
            clicked.Enabled = false;
            //clicked.Image = Image.FromFile("png-transparent-explosion-animation-sprite-blast-orange-special-effects-particle-system-thumbnail.png");
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");
            var updateX = Builders<BsonDocument>.Update.Set("x", Int32.Parse(clicked.Text.Substring(0, 1)));
            var updateY = Builders<BsonDocument>.Update.Set("y", Int32.Parse(clicked.Text.Substring(clicked.Text.Length - 1)));
            collection.UpdateOne(filter, updateX);
            collection.UpdateOne(filter, updateY);
            Wait();
            stage++;
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
            if ((Check_DragDrop(pictureBox) != multitude))
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

        private void Pull_ReadyP()
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filterP1 = Builders<BsonDocument>.Filter.Eq("p1Ready", true);
            var filterP2 = Builders<BsonDocument>.Filter.Eq("p2Ready", true);
            var documentP1 = collection.Find(filterP1).FirstOrDefault();
            var documentP2 = collection.Find(filterP2).FirstOrDefault();
            var (p1Ready, p2Ready) = (false, false);
            try
            {
                p1Ready = documentP1["p1Ready"].AsBoolean;
                p2Ready = documentP2["p2Ready"].AsBoolean;
            }
            catch
            {

            }
            finally
            {
                if (p1Ready && p2Ready)
                {
                    Start_Game();
                }
            }
        }

        private void P1()
        {
            stage = 2;
            timer_Pull.Start();
            Check_Ships("p1Ready");
        }

        private void P2()
        {
            stage = 2;
            timer_Pull.Start();
            Check_Ships("p2Ready");
        }

        private void P(string fieldReady)
        {
            stage = 2;
            timer_Pull.Start();
            Check_Ships(fieldReady);
        }

        private void Push_ReadyP(string fieldReady)
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");
            var updatePReady = Builders<BsonDocument>.Update.Set(fieldReady, true);
            collection.UpdateOne(filter, updatePReady);
        }

        private void Check_Ships(string fieldReady)
        {
            if (!(Check_Positioning(pictureBoxShip5, 5, "aircraft carrier") &&
        Check_Positioning(pictureBoxShip4, 4, "destroyer") &&
        Check_Positioning(pictureBoxShip3, 3, "minesweeper") &&
        Check_Positioning(pictureBoxShip2, 2, "submarine")))
            { }
            else
            {
                Push_ReadyP(fieldReady);
            }
        }

        /* check positioning and start game */
        private void buttonStart_Click(object sender, EventArgs e)
        {
            timer.Start();
            if (player == 1)
            {
                P1();
            }
            else
            {
                P2();
            }
        }

        private void Start_Game()
        {
            turn++;
            stage = 3;
            exist = true;
            stopDragDrop = false;
            buttonStart.Hide();
            panel1.Width = panel1.Width / 2;
            populateGrid(1, panel2, grid2, btnGrid2);
            //depopulateGrid(grid1, btnGrid1);
            if (player == 2)
            {
                foreach (Button btn in btnGrid2)
                {
                    btn.Enabled = false;
                }
            }
            else
            {

            }
            int temp;
            
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
                Location_Offset(e, pictureBoxShip4);
        }

        private void pictureBoxShip4_MouseMove(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Transition_Glitch(e, pictureBoxShip4);
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
                Location_Offset(e, pictureBoxShip2);
        }


        private void pictureBoxShip2_MouseMove(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Transition_Glitch(e, pictureBoxShip2);
        }


        private void pictureBoxShip2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (stopDragDrop)
                Rotate(counter4, pictureBoxShip2);
        }

        /* movement & rotation for each piscturebox end */

        private void pictureBoxPlayer1_MouseEnter(object sender, EventArgs e)
        {
            if (PictureBoxPlayer1 && allowClick) //if pictureBoxPlayer1 is available you can hover it and click it
                pictureBoxPlayer1.ImageLocation = "Captain1hover.png";
        }

            private void pictureBoxPlayer1_MouseLeave(object sender, EventArgs e)
            {

            if (PictureBoxPlayer1 && allowClick) //if pictureBoxPlayer1 is available you can hover it and click it
                pictureBoxPlayer1.ImageLocation = "Captain1.png";
            // else
            //    pictureBoxPlayer1.ImageLocation = "Captain1hover.png";
        }

        private void pictureBoxPlayer1_Click(object sender, EventArgs e)
        {
            if (PictureBoxPlayer1 && allowClick && !string.IsNullOrEmpty(textBoxName.Text))
            {
                player = 1;
                Push_Player_Choice("p1", "p1Name", textBoxName.Text);
                pictureBoxPlayer1.ImageLocation = "Captain1hover.png";
                PictureBoxPlayer1 = false;
                allowClick = false;
                textBoxName.Hide();
                labelNameChoice.Hide();
            }
            else
                MessageBox.Show("please enter your name");
        }

        private void pictureBoxPlayer2_MouseEnter(object sender, EventArgs e)
        {
            if (PictureBoxPlayer2 && allowClick) //if pictureBoxPlayer2 is available you can hover it and click it
                pictureBoxPlayer2.ImageLocation = "Captain2hover.png";
        }

        private void pictureBoxPlayer2_MouseLeave(object sender, EventArgs e)
        {
            if (PictureBoxPlayer2 && allowClick) //if pictureBoxPlayer2 is available you can hover it and click it
                pictureBoxPlayer2.ImageLocation = "Captain2.png";
            //   else
            //     pictureBoxPlayer2.ImageLocation = "Captain2hover.png";
        }

        private void pictureBoxPlayer2_Click(object sender, EventArgs e)
        {
            if (PictureBoxPlayer2 && allowClick && !string.IsNullOrEmpty(textBoxName.Text))
            {
                player = 2;
                Push_Player_Choice("p2", "p2Name", textBoxName.Text);
                pictureBoxPlayer2.ImageLocation = "Captain2hover.png";
                PictureBoxPlayer2 = false;
                allowClick = false;
                textBoxName.Hide();
                labelNameChoice.Hide();
            }
            else
                MessageBox.Show("please enter your name");
        }
    }
}