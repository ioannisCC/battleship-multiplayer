using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using System.IO;
using System.Media;

namespace battleship
{
    public partial class Game : Form
    {
        private WaveStream MusicStream;
        private WaveOut MusicOut;
        int[] rainSpeeds = { 4, 6, 8, 3, 5, 6, 7, 4, 2 };
        int loadingSpeed = 100;
        float initialPercentage = 0;

        /* function in order to make the main menu options look between arrow when hovering*/
        private void Show_Arrows(int X1, int Y1, int X2, int Y2)
        {
            pictureBox1.Location = new Point(X1, Y1);
            pictureBox1.ImageLocation = "RightArrow.png";
            pictureBox1.Size = new Size(54, 37);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Show();
            pictureBox2.Location = new Point(X2, Y2);
            pictureBox2.ImageLocation = "leftArrow.png";
            pictureBox2.Size = new Size(54, 37);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Show();

        }
        private void Hide_Arrows()
        {
            pictureBox1.Hide();
            pictureBox2.Hide();
        }

        public Game()
        {
            InitializeComponent();
            //Play_Sound("Bear_McCreary_-Memories_of_Mother_Gratomic.com.wav");
        }

        void Play_Sound(string filename)
        {
            MusicStream = new AudioFileReader(filename);
            MusicOut = new WaveOut();
            MusicOut.Init(MusicStream);
            MusicStream.CurrentTime = new TimeSpan(0L);
            MusicOut.Play();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            pictureBox3.Hide();
            groupBox1.Hide();
            labelInstructions.Hide();
        }        

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            Hide_Arrows();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            Show_Arrows(115, 275, 331, 275);
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            Show_Arrows(33, 387, 415, 387);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            Hide_Arrows();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            Show_Arrows(115, 514, 323, 514);
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            Hide_Arrows();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            foreach(var lbl in Controls.OfType<Label>())
               lbl.Hide();

            pictureBox3.Show();
            labelInstructions.Show();
            
        }

        /* loading screen code start*/

        /* timer1_tick contains the falling rockets effect by moving them and at the end of the groupbox they are instantaited again*/
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                switch(i)
                {
                    case 0:
                        pictureBox13.Location = new Point(pictureBox13.Location.X, pictureBox13.Location.Y + rainSpeeds[i]);
                        if (pictureBox13.Location.Y > panel1.Size.Height + pictureBox5.Size.Height)
                        {
                            pictureBox13.Location = new Point(pictureBox13.Location.X, 0 - pictureBox13.Size.Height);
                        }
                        break;
                    case 1:
                        pictureBox6.Location = new Point(pictureBox6.Location.X, pictureBox6.Location.Y + rainSpeeds[i]);
                        if (pictureBox6.Location.Y > panel1.Size.Height + pictureBox6.Size.Height)
                        {
                            pictureBox6.Location = new Point(pictureBox6.Location.X, 0 - pictureBox6.Size.Height);
                        }
                        break;
                    case 2:
                        pictureBox7.Location = new Point(pictureBox7.Location.X, pictureBox7.Location.Y + rainSpeeds[i]);
                        if (pictureBox7.Location.Y > panel1.Size.Height + pictureBox7.Size.Height)
                        {
                            pictureBox7.Location = new Point(pictureBox7.Location.X, 0 - pictureBox7.Size.Height);
                        }
                        break;
                    case 3:
                        pictureBox8.Location = new Point(pictureBox8.Location.X, pictureBox8.Location.Y + rainSpeeds[i]);
                        if (pictureBox8.Location.Y > panel1.Size.Height + pictureBox8.Size.Height)
                        {
                            pictureBox8.Location = new Point(pictureBox8.Location.X, 0 - pictureBox8.Size.Height);
                        }
                        break;
                    case 4:
                        pictureBox9.Location = new Point(pictureBox9.Location.X, pictureBox9.Location.Y + rainSpeeds[i]);
                        if (pictureBox9.Location.Y > panel1.Size.Height + pictureBox9.Size.Height)
                        {
                            pictureBox9.Location = new Point(pictureBox9.Location.X, 0 - pictureBox9.Size.Height);
                        }
                        break;
                    case 5:
                        pictureBox10.Location = new Point(pictureBox10.Location.X, pictureBox10.Location.Y + rainSpeeds[i]);
                        if (pictureBox10.Location.Y > panel1.Size.Height + pictureBox10.Size.Height)
                        {
                            pictureBox10.Location = new Point(pictureBox10.Location.X, 0 - pictureBox10.Size.Height);
                        }
                        break;
                    case 6:
                        pictureBox11.Location = new Point(pictureBox11.Location.X, pictureBox11.Location.Y + rainSpeeds[i]);
                        if (pictureBox11.Location.Y > panel1.Size.Height + pictureBox11.Size.Height)
                        {
                            pictureBox11.Location = new Point(pictureBox11.Location.X, 0 - pictureBox11.Size.Height);
                        }
                        break;
                    case 7:
                        pictureBox12.Location = new Point(pictureBox12.Location.X, pictureBox12.Location.Y + rainSpeeds[i]);
                        if (pictureBox12.Location.Y > panel1.Size.Height + pictureBox12.Size.Height)
                        {
                            pictureBox12.Location = new Point(pictureBox12.Location.X, 0 - pictureBox12.Size.Height);
                        }
                        break;

                }
            }
        }

        /* timer2_tick contains the explosion effect by simply moving slowly a panel which is covering the image */
        private void timer2_Tick(object sender, EventArgs e)
        {
            initialPercentage += loadingSpeed;
            float percentage = initialPercentage / pictureBox4.Height * 100;
            label6.Text = (int)percentage + "%";
            panel2.Location = new Point(panel2.Location.X, panel2.Location.Y + loadingSpeed);
            if (panel2.Location.Y > pictureBox4.Location.Y + pictureBox4.Height)
            {
                label6.Text = "100 %";
                groupBox1.Hide();
                this.timer1.Stop();
                this.timer2.Stop();
                this.Hide();
                MainGame instance = new MainGame();
                instance.Show();
            }
        }

        /* loading screen code end */

        /* initiate loading screen*/
        private void label2_Click(object sender, EventArgs e)
        {
            foreach (var lbl in Controls.OfType<Label>())
                lbl.Hide();
            this.BackColor = Color.White;
            this.Size = new Size(400, 704);
            this.CenterToScreen();
            this.BackgroundImage = null;
            groupBox1.Show();
            timer1.Start();
            timer2.Start();
            /*GraphicsPath shape = new GraphicsPath();
            shape.AddEllipse(0, 0, 50, 50);
            this.Region = new Region(shape);*/
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Hide();
            label2.Show();
            label3.Show();
            label4.Show();
            labelInstructions.Hide();
        }
    }
}