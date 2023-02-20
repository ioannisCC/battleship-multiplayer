using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.IO;
using System.Media;
using System.Drawing;
using System.Windows.Forms;

namespace battleship
{
    public class Audio
    {
        public WaveStream MusicStream;
        public WaveOut MusicOut;
        public SoundPlayer soundPlayerHit;
        public SoundPlayer soundPlayerMiss;

        public Audio(string filename)
        {
            MusicStream = new AudioFileReader(filename);
            MusicOut = new WaveOut();
            soundPlayerHit = new SoundPlayer("explosion.wav");
            soundPlayerMiss = new SoundPlayer("splash.wav");
        }

        public void Play_BgSound()
        {
            MusicOut.Init(MusicStream);
            MusicStream.CurrentTime = new TimeSpan(0L);
            MusicOut.Play();
        }

        public void Check_Sound(PictureBox picturebox)
        {
            if (picturebox.ImageLocation == "buttonaudioOFF.png")
            {
                picturebox.ImageLocation = "buttonaudioON.png";
                MusicOut.Resume();
            }
            else
            {
                picturebox.ImageLocation = "buttonaudioOFF.png";
                MusicOut.Pause();
            }
        }

        public void Hit()
        {
            soundPlayerHit.Play();
        }

        public void Miss()
        {
            soundPlayerMiss.Play();
        }
    }
}
