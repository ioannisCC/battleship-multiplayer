namespace battleship
{
    partial class VictoryDefeat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VictoryDefeat));
            this.pictureBoxVictory = new System.Windows.Forms.PictureBox();
            this.pictureBoxDefeat = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVictory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDefeat)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxVictory
            // 
            this.pictureBoxVictory.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxVictory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxVictory.BackgroundImage")));
            this.pictureBoxVictory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxVictory.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxVictory.Image")));
            this.pictureBoxVictory.Location = new System.Drawing.Point(332, 207);
            this.pictureBoxVictory.Name = "pictureBoxVictory";
            this.pictureBoxVictory.Size = new System.Drawing.Size(739, 289);
            this.pictureBoxVictory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxVictory.TabIndex = 10;
            this.pictureBoxVictory.TabStop = false;
            // 
            // pictureBoxDefeat
            // 
            this.pictureBoxDefeat.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxDefeat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxDefeat.BackgroundImage")));
            this.pictureBoxDefeat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxDefeat.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxDefeat.Image")));
            this.pictureBoxDefeat.Location = new System.Drawing.Point(332, 207);
            this.pictureBoxDefeat.Name = "pictureBoxDefeat";
            this.pictureBoxDefeat.Size = new System.Drawing.Size(739, 289);
            this.pictureBoxDefeat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDefeat.TabIndex = 11;
            this.pictureBoxDefeat.TabStop = false;
            // 
            // VictoryDefeat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1362, 801);
            this.Controls.Add(this.pictureBoxDefeat);
            this.Controls.Add(this.pictureBoxVictory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VictoryDefeat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VictoryDefeat";
            this.Load += new System.EventHandler(this.VictoryDefeat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVictory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDefeat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxVictory;
        private System.Windows.Forms.PictureBox pictureBoxDefeat;
    }
}