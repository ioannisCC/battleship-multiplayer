using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleship
{   /* username: battleshipGame
     * password: unipi */
    public partial class MainGame : Form
    {
        private bool check = true; /* used to disable the mouse click event whenever is the other's player turn */
        private bool allowClick = true;
        private bool PictureBoxPlayer1 = true; /* true means captain1 (jack) is available */
        private bool PictureBoxPlayer2 = true; /* true means captain2 (croitor) is available */
        private int stage = 1;  /* used at timer_Pull in order to determine eacg stage of the game (captain choice, ship placement, game itself) */
        private int winsP1 = 0;
        private int winsP2 = 0;
        private int turn = 1;  /* which players turn is */
        private int oldX = 0, oldY = 0;
        private int steps = 0; /* # of turns in order to terminate the game */
        private int timerVar; /* game duration */
        private int counter1 = 0, counter2 = 0, counter3 = 0, counter4 = 0; /* for ships rotation */
        private bool stopDragDrop = true;
        private bool exist = false; /* in order to make clickable only the second grid */
        private static Grid grid1 = new Grid(10);
        private static Grid grid2 = new Grid(10);
        private Button[,] btnGrid1 = new Button[grid1.Size, grid1.Size];
        private Button[,] btnGrid2 = new Button[grid2.Size, grid2.Size];
        private Point offset;
        private Point mousePosition;
        private MongoClient dbClient;
        private Audio sound;
        private Player playerP;


        public MainGame(MongoClient dbClient, int winsP1, int winsP2, Audio bgMusic)
        {
            InitializeComponent();
            this.dbClient = dbClient;
            /* VERY IMPORTANT for drag and drop (ship movement) */
            pictureBoxShip5.MouseDown += new MouseEventHandler(pictureBoxShip5_MouseDown);
            pictureBoxShip5.MouseMove += new MouseEventHandler(pictureBoxShip5_MouseMove);
            this.winsP1 = winsP1;
            this.winsP2 = winsP2;
            this.sound = bgMusic;
        }

        private void MainGame_Load(object sender, EventArgs e)
        {
            Initialize_Database();
            Bitmap bm = new Bitmap(new Bitmap("scope.png"), 50, 50);
            Cursor = new Cursor(bm.GetHicon());
            pictureBoxShip5.AllowDrop = true;
            pictureBoxShip2.Hide();
            pictureBoxShip3.Hide();
            pictureBoxShip4.Hide();
            pictureBoxShip5.Hide();
            buttonStart.Hide();
            labelTimer.Hide();
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
            var updateHitsP1 = Builders<BsonDocument>.Update.Set("hitsP1", 0);
            var updateHitsP2 = Builders<BsonDocument>.Update.Set("hitsP2", 0);
            var updateMissesP1 = Builders<BsonDocument>.Update.Set("missesP1", 0);
            var updateMissesP2 = Builders<BsonDocument>.Update.Set("missesP2", 0);

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
            collection.UpdateOne(filter, updateHitsP1);
            collection.UpdateOne(filter, updateHitsP2);
            collection.UpdateOne(filter, updateMissesP1);
            collection.UpdateOne(filter, updateMissesP2);
        }

        private void Push_Statistics(int redCounter, int greenCounter)
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");

            if (playerP.playerGetSet == 1)
            {
                var updateHitsP1 = Builders<BsonDocument>.Update.Set("hitsP1", redCounter);
                var updateMissesP1 = Builders<BsonDocument>.Update.Set("missesP1", greenCounter);
                collection.UpdateOne(filter, updateHitsP1);
                collection.UpdateOne(filter, updateMissesP1);
            }
            else
            {
                var updateHitsP2 = Builders<BsonDocument>.Update.Set("hitsP2", redCounter);
                var updateMissesP2 = Builders<BsonDocument>.Update.Set("missesP2", greenCounter);
                collection.UpdateOne(filter, updateHitsP2);
                collection.UpdateOne(filter, updateMissesP2);
            }
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
                buttonStart.Show();
            }
            else if (p1 == false)
                Disable_Captain1();
            else if (p2 == false)
                Disable_Captain2();
        }

        private void Push_ReadyP(string fieldReady)
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");
            var updatePReady = Builders<BsonDocument>.Update.Set(fieldReady, true);
            collection.UpdateOne(filter, updatePReady);
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

        private int[] Pull_Coordinates()
        {
            int[] coordinates = { 0, 0 };
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            BsonDocument document = collection.Find(new BsonDocument()).FirstOrDefault();
            if (playerP.playerGetSet == 2)
            {
                /*string[] words = document.ToString().Split(',');
                string[] temp = words[1].Split(':');
                string x = temp[1].Substring(1, temp[1].Length - 1);
                temp = words[2].Split(':');
                string y = temp[1].Substring(1, temp[1].Length - 1);
                coordinates[0] = Int32.Parse(x);
                coordinates[1] = Int32.Parse(y);
                return coordinates;*/
                coordinates[0] = document["x"].AsInt32;
                coordinates[1] = document["y"].AsInt32;
            }
            else
            {
                /*string[] words = document.ToString().Split(',');
                string[] temp = words[10].Split(':');
                string x2 = temp[1].Substring(1, temp[1].Length - 1);
                temp = words[11].Split(':');
                string y2 = temp[1].Substring(1, temp[1].Length - 2);
                coordinates[0] = Int32.Parse(x2);
                coordinates[1] = Int32.Parse(y2);
                return coordinates;*/
                coordinates[0] = document["x2"].AsInt32;
                coordinates[1] = document["y2"].AsInt32;
            }
            return coordinates;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timerVar++;
            labelTimer.Text = "Time elapsed: " + timerVar;
        }

        private void CheckGameStatus()
        {
            int grayCounter = 0;
            int redCounter = 0;
            int greenCounter = 0;
            for (int i = 0; i < grid1.Size; i++)
                for (int j = 0; j < grid1.Size; j++)
                    if (grid1.theGrid[i, j].CurrentlyOccupied && btnGrid1[i, j].BackColor == Color.Gray)
                        grayCounter++;

            for (int i = 0; i < grid2.Size; i++)
                for (int j = 0; j < grid2.Size; j++)
                    if (btnGrid2[i, j].BackColor == Color.Red)
                        redCounter++;
                    else if (btnGrid2[i, j].BackColor == Color.Green)
                        greenCounter++;

            if (grayCounter == 14 && playerP.playerGetSet == 1)
            {
                winsP2++;
                End_Game("croitorWins.jpg", redCounter, greenCounter);
            }
            else if (grayCounter == 14 && playerP.playerGetSet == 2)
            {
                winsP1++;
                End_Game("jackWins.jpg", redCounter, greenCounter);
            }
            else if (redCounter == 14 && playerP.playerGetSet == 1)
            {
                winsP1++;
                End_Game("jackWins.jpg", redCounter, greenCounter);
                playerP.winsGetSet++;
            }
            else if (redCounter == 14 && playerP.playerGetSet == 2)
            {
                winsP2++;
                End_Game("croitorWins.jpg", redCounter, greenCounter);
                playerP.winsGetSet++;
            }
        }

        private void End_Game(string filename, int redCounter, int greenCounter)
        {
            timer_Pull.Stop();
            timer.Stop();
            panel1.Hide();
            panel2.Hide();
            Push_Statistics(redCounter, greenCounter);
            this.Hide();
            ScoreBoard scoreForm = new ScoreBoard(steps, redCounter, greenCounter, playerP.playerGetSet, dbClient, timerVar, winsP1, winsP2, sound);
            scoreForm.BackgroundImage = Image.FromFile(filename);
            scoreForm.BackgroundImageLayout = ImageLayout.Stretch;
            Thread.Sleep(2000);
            this.Hide();
            scoreForm.Show();
        }

        private void timer_Pull_Tick(object sender, EventArgs e)
        {
            if (stage == 1)
            {
                Pull_Player_Choice();
                if (!PictureBoxPlayer1)     /* make the captains pictureboxes unavailable */
                    pictureBoxPlayer1.ImageLocation = "Captain1checked.png";
                if (!PictureBoxPlayer2)
                    pictureBoxPlayer2.ImageLocation = "Captain2checked.png";
            }
            else if (stage == 2)
                Pull_ReadyP();
            else if (stage == 3)
            {
                var database = dbClient.GetDatabase("battleship");
                var collection = database.GetCollection<BsonDocument>("targetLocation");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");

                int[] newCoordinates = Pull_Coordinates();
                if (oldX != newCoordinates[0] || oldY != newCoordinates[1])
                {
                    if (grid1.theGrid[newCoordinates[0], newCoordinates[1]].CurrentlyOccupied)
                    {
                        var updateHit = Builders<BsonDocument>.Update.Set("hit", true);
                        collection.UpdateOne(filter, updateHit);
                        sound.Hit();
                    }
                    else
                    {
                        var updateHit = Builders<BsonDocument>.Update.Set("hit", false);
                        collection.UpdateOne(filter, updateHit);
                        sound.Miss();
                    }

                    /* color the grid1 with your ships */
                    btnGrid1[newCoordinates[0], newCoordinates[1]].BackColor = Color.Gray;
                    btnGrid1[newCoordinates[0], newCoordinates[1]].ForeColor = Color.Gray;

                    oldX = newCoordinates[0];
                    oldY = newCoordinates[1];


                    /* check who's turn is and add the click event to the button grid depending on which player you are */
                    if (turn % 2 == 0 && !Enabled_Buttons())
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                btnGrid2[i, j].Click += Grid_Button_Click;
                                check = true;
                            }
                        }
                    }

                    if (turn % 2 != 0 && !Enabled_Buttons())
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                btnGrid2[i, j].Click += Grid_Button_Click;
                                check = true;
                            }
                        }
                    }
                }
                CheckGameStatus();
            }
            else
                timer_Pull.Stop();
        }

        /* returns if the other player is done with his turn */
        private bool Color_Buttons()
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            BsonDocument document = collection.Find(new BsonDocument()).FirstOrDefault();

            return document["hit"].AsBoolean;
        }

        /* checks if the buttons of grid2 (where you shoot) are available */
        private bool Enabled_Buttons()
        {
            foreach (Button button in btnGrid2)
            {
                if (check)
                    return true;
                else
                    return false;
            }

            return false;
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
                    btnGrid[i, j].FlatStyle = FlatStyle.Popup;
                    btnGrid[i, j].ForeColor = Color.Transparent;

                    /* add the click event only to buttons that belong to the grid you shoot */
                    if (exist)
                        btnGrid2[i, j].Click += new EventHandler(Grid_Button_Click);

                    panel.Controls.Add(btnGrid[i, j]);
                    btnGrid[i, j].Location = new Point(i * buttonSize, j * buttonSize);
                    btnGrid[i, j].UseVisualStyleBackColor = false;
                    btnGrid[i, j].Text = i + "|" + j;
                }
            }

            pictureBoxShip2.Show();
            pictureBoxShip3.Show();
            pictureBoxShip4.Show();
            pictureBoxShip5.Show();
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
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "1");

            if (playerP.playerGetSet == 1)
            {
                var updateX = Builders<BsonDocument>.Update.Set("x", Int32.Parse(clicked.Text.Substring(0, 1)));
                var updateY = Builders<BsonDocument>.Update.Set("y", Int32.Parse(clicked.Text.Substring(clicked.Text.Length - 1)));
                collection.UpdateOne(filter, updateX);
                collection.UpdateOne(filter, updateY);
            }
            else
            {
                var updateX2 = Builders<BsonDocument>.Update.Set("x2", Int32.Parse(clicked.Text.Substring(0, 1)));
                var updateY2 = Builders<BsonDocument>.Update.Set("y2", Int32.Parse(clicked.Text.Substring(clicked.Text.Length - 1)));
                collection.UpdateOne(filter, updateX2);
                collection.UpdateOne(filter, updateY2);
            }
            Thread.Sleep(1000);  /* in order to have time to get the data to the database */
            if (Color_Buttons())
            {
                clicked.BackColor = Color.Red;
                clicked.Text = "";
                sound.Hit();
            }
            else
            {
                clicked.BackColor = Color.Green;
                clicked.Text = "";
                sound.Miss();
            }

            if (turn % 2 == 0 && Enabled_Buttons())
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        btnGrid2[i, j].Click -= Grid_Button_Click;
                        check = false;
                    }
                }
            }

            if (turn % 2 != 0 && Enabled_Buttons())
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        btnGrid2[i, j].Click -= Grid_Button_Click;
                        check = false;
                    }
                }
            }

            turn++;
            steps++;
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

        private void Check_Ships()
        {
            if (!(Check_Positioning(pictureBoxShip5, 5, "aircraft carrier") &&
        Check_Positioning(pictureBoxShip4, 4, "destroyer") &&
        Check_Positioning(pictureBoxShip3, 3, "minesweeper") &&
        Check_Positioning(pictureBoxShip2, 2, "submarine")))
            { }
            else
            {
                Push_ReadyP(playerP.fieldNameGetSet);
            }
        }

        /* check positioning and start game */
        private void buttonStart_Click(object sender, EventArgs e)
        {
            stage = playerP.stageGetSet;
            playerP.P(playerP, 2, timer_Pull);
            Check_Ships();
        }

        private void Start_Game()
        {
            turn++;
            stage = 3;
            exist = true;
            stopDragDrop = false;
            labelTimer.Show();
            buttonStart.Hide();
            timer.Start();
            panel1.Width = panel1.Width / 2;
            populateGrid(1, panel2, grid2, btnGrid2);

            if (playerP.playerGetSet == 2)
            {
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                    {
                        btnGrid2[i, j].Click -= Grid_Button_Click;
                        check = false;
                    }
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

        /* mousePosition added so the picturebox will update its location on a thrshold of 5 so it does not glitch  and that drag and drop works*/
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
            if (PictureBoxPlayer1 && allowClick) /* if pictureBoxPlayer1 is available you can hover it and click it */
                pictureBoxPlayer1.ImageLocation = "Captain1hover.png";
        }

        private void pictureBoxPlayer1_MouseLeave(object sender, EventArgs e)
        {

            if (PictureBoxPlayer1 && allowClick) /* if pictureBoxPlayer1 is available you can hover it and click it */
                pictureBoxPlayer1.ImageLocation = "Captain1.png"; 
        }

        private void pictureBoxPlayer1_Click(object sender, EventArgs e)
        {
            if (PictureBoxPlayer1 && allowClick && !string.IsNullOrEmpty(textBoxName.Text))
            {
                playerP = new Player("p1Ready", 1, winsP1, 2, textBoxName.Text);
                Push_Player_Choice("p1", "p1Name", playerP.nameGetSet);
                Disable_Captain1();
                Disable_Captain2();
                allowClick = false;
                textBoxName.Hide();
                pictureBoxEnterName.Hide();
            }
            else
                MessageBox.Show("please enter your name");
        }

        private void pictureBoxPlayer2_MouseEnter(object sender, EventArgs e)
        {
            if (PictureBoxPlayer2 && allowClick) /* if pictureBoxPlayer2 is available you can hover it and click it */
                pictureBoxPlayer2.ImageLocation = "Captain2hover.png";
        }

        private void pictureBoxPlayer2_MouseLeave(object sender, EventArgs e)
        {
            if (PictureBoxPlayer2 && allowClick) /* if pictureBoxPlayer2 is available you can hover it and click it */
                pictureBoxPlayer2.ImageLocation = "Captain2.png";
        }

        private void pictureBoxPlayer2_Click(object sender, EventArgs e)
        {
            if (PictureBoxPlayer2 && allowClick && !string.IsNullOrEmpty(textBoxName.Text))
            {
                playerP = new Player("p2Ready", 2,winsP2,2,textBoxName.Text);
                Push_Player_Choice("p2", "p2Name", playerP.nameGetSet);
                Disable_Captain1();
                Disable_Captain2();
                allowClick = false;
                textBoxName.Hide();
                pictureBoxEnterName.Hide();
            }
            else
                MessageBox.Show("please enter your name");
        }
        
        private void Disable_Captain1()
        {
            PictureBoxPlayer1 = false;
            pictureBoxPlayer1.ImageLocation = "Captain1checked.png";
            pictureBoxPlayer1.MouseEnter -= pictureBoxPlayer1_MouseEnter;
            pictureBoxPlayer1.MouseLeave -= pictureBoxPlayer1_MouseLeave;
            pictureBoxPlayer1.Click -= pictureBoxPlayer1_Click;
            pictureBoxPlayer1.Cursor = Cursors.Default;
        }

        private void Disable_Captain2()
        {
            PictureBoxPlayer2 = false;
            pictureBoxPlayer2.ImageLocation = "Captain2checked.png";
            pictureBoxPlayer2.MouseEnter -= pictureBoxPlayer2_MouseEnter;
            pictureBoxPlayer2.MouseLeave -= pictureBoxPlayer2_MouseLeave;
            pictureBoxPlayer2.Click -= pictureBoxPlayer2_Click;
            pictureBoxPlayer2.Cursor = Cursors.Default;
        }

        private void pictureBoxAudio_Click(object sender, EventArgs e)
        {
            sound.Check_Sound(pictureBoxAudio);
        }

        private void MainGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}