using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace battleship
{
    public partial class ScoreBoard : Form
    {
        private int steps = 0;
        private int hits = 0;
        private int misses = 0;
        private int player = 0;
        private int winsP1 = 0;
        private int winsP2 = 0;
        int timeVar = 0;
        MongoClient dbClient;
        Audio bgMusic;

        public ScoreBoard(int steps, int hits, int misses, int player, MongoClient dbClient, int timeVar, int winsP1, int winsP2, Audio bgMusic)
        {
            InitializeComponent();
            this.steps = steps;
            this.hits = hits;
            this.misses = misses;
            this.player = player;
            this.dbClient = dbClient;
            this.timeVar = timeVar;
            this.winsP1 = winsP1;
            this.winsP2 = winsP2;
            this.bgMusic = bgMusic;
        }

        private void ScoreBoard_Load(object sender, EventArgs e)
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            BsonDocument document = collection.Find(new BsonDocument()).FirstOrDefault();

            labelTotalTurns.Text = (2 * steps).ToString();
            labelCroitorDuration.Text = timeVar.ToString();
            labelJackDuration.Text = timeVar.ToString();
            labelJackName.Text = document["p1Name"].ToString();
            labelCroitorName.Text = document["p2Name"].ToString();
            labelJackWins.Text = winsP1.ToString();
            labelCroitorWins.Text = winsP2.ToString();

            if (player == 1)
            {
                int hitsP2 = document["hitsP2"].AsInt32;
                int missesP2 = document["missesP2"].AsInt32;


                if ((hits * 10 - misses) > (hitsP2 * 10 - missesP2))
                {
                    labelJackScore.ForeColor = Color.Gold;
                    labelJackScore.Font = new Font(labelJackScore.Font.FontFamily, 40, labelJackScore.Font.Style);
                }
                else
                {
                    labelCroitorScore.ForeColor = Color.Gold;
                    labelCroitorScore.Font = new Font(labelCroitorScore.Font.FontFamily, 40, labelCroitorScore.Font.Style);
                }

                labelJackHits.Text = hits.ToString();
                labelJackMisses.Text = misses.ToString();
                labelJackScore.Text = (hits * 10 - misses).ToString();

                labelCroitorHits.Text = hitsP2.ToString();
                labelCroitorMisses.Text = missesP2.ToString();
                labelCroitorScore.Text = (hitsP2 * 10 - missesP2).ToString();
            }
            else
            {
                int hitsP1 = document["hitsP1"].AsInt32;
                int missesP1 = document["missesP1"].AsInt32;


                if ((hits * 10 - misses) > (hitsP1 * 10 - missesP1))
                {
                    labelCroitorScore.ForeColor = Color.Gold;
                    labelCroitorScore.Font = new Font(labelCroitorScore.Font.FontFamily, 40, labelCroitorScore.Font.Style);
                }
                else
                {
                    labelJackScore.ForeColor = Color.Gold;
                    labelJackScore.Font = new Font(labelJackScore.Font.FontFamily, 40, labelJackScore.Font.Style);
                }

                labelCroitorHits.Text = hits.ToString();
                labelCroitorMisses.Text = misses.ToString();
                labelCroitorScore.Text = (hits * 10 - misses).ToString();

                labelJackHits.Text = hitsP1.ToString();
                labelJackMisses.Text = missesP1.ToString();
                labelJackScore.Text = (hitsP1 * 10 - missesP1).ToString();
            }
        }

        private void Rematch()
        {
            this.Hide();
            MainGame rematchMaingame = new MainGame(dbClient,winsP1,winsP2, bgMusic);
            rematchMaingame.Show();
        }

        private void labelRematch_Click(object sender, EventArgs e)
        {
            Rematch();
        }

        private void pictureBoxRematch_Click(object sender, EventArgs e)
        {
            Rematch();
        }

        private void ScoreBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxAudio_Click(object sender, EventArgs e)
        {
            bgMusic.Check_Sound(pictureBoxAudio);
        }
    }
}