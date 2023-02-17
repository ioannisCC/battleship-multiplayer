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
        MongoClient dbClient;
        int timeVar = 0;
        public ScoreBoard(int steps, int hits, int misses, int player, MongoClient dbClient, int timeVar)
        {
            InitializeComponent();
            this.steps = steps;
            this.hits = hits;
            this.misses = misses;
            this.player = player;
            this.dbClient = dbClient;
            this.timeVar = timeVar;
        }

        private void ScoreBoard_Load(object sender, EventArgs e)
        {
            var database = dbClient.GetDatabase("battleship");
            var collection = database.GetCollection<BsonDocument>("targetLocation");
            BsonDocument document = collection.Find(new BsonDocument()).FirstOrDefault();

            labelTotalTurns.Text = steps.ToString();
            labelCroitorDuration.Text = timeVar.ToString();
            labelJackDuration.Text = timeVar.ToString();

            if (player == 1)
            {
                if ((hits * 10 - misses) > (document["hitsP2"].AsInt32 * 10 - document["missesP2"].AsInt32))
                {
                    labelCroitorScore.ForeColor = Color.Gold;
                    labelCroitorScore.Font = new Font(labelCroitorScore.Font.FontFamily, 40, labelCroitorScore.Font.Style);
                }
                else 
                {
                    labelJackScore.ForeColor = Color.Gold;
                    labelJackScore.Font = new Font(labelJackScore.Font.FontFamily, 40, labelJackScore.Font.Style);
                }

                labelJackHits.Text = hits.ToString();
                labelJackMisses.Text = misses.ToString();
                labelJackAccuracy.Text = ((hits/(misses+hits))*100).ToString() + "%";
                labelJackScore.Text = (hits*10-misses).ToString();

                labelCroitorHits.Text = document["hitsP2"].AsInt32.ToString();
                labelCroitorMisses.Text = document["missesP2"].AsInt32.ToString();
                labelCroitorAccuracy.Text = ((document["hitsP2"].AsInt32 / (document["missesP2"].AsInt32 + document["hitsP2"].AsInt32)) * 100).ToString() + "%";
                labelCroitorScore.Text = (document["hitsP2"].AsInt32 * 10 - document["missesP2"].AsInt32).ToString();
            }
            else
            {
                if ((hits * 10 - misses) > (document["hitsP1"].AsInt32 * 10 - document["missesP1"].AsInt32))
                {
                    labelJackScore.ForeColor = Color.Gold;
                    labelJackScore.Font = new Font(labelJackScore.Font.FontFamily, 40, labelJackScore.Font.Style);                    
                }
                else
                {
                    labelCroitorScore.ForeColor = Color.Gold;
                    labelCroitorScore.Font = new Font(labelCroitorScore.Font.FontFamily, 40, labelCroitorScore.Font.Style);
                }

                labelCroitorHits.Text = hits.ToString();
                labelCroitorMisses.Text = misses.ToString();
                labelCroitorAccuracy.Text = ((hits / (misses + hits)) * 100).ToString() + "%";
                labelCroitorScore.Text = (hits * 10 - misses * 1).ToString();

                labelJackHits.Text = document["hitsP1"].AsInt32.ToString();
                labelJackMisses.Text = document["missesP1"].AsInt32.ToString();
                labelJackAccuracy.Text = ((document["hitsP1"].AsInt32 / (document["missesP1"].AsInt32 + document["hitsP1"].AsInt32)) * 100).ToString() + "%";
                labelJackScore.Text = (document["hitsP1"].AsInt32 * 10 - document["missesP1"].AsInt32).ToString();
            }
        }

        private void Rematch()
        {
            this.Hide();
            MainGame rematchMaingame = new MainGame(dbClient);
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
    }
}
