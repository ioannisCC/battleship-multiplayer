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
            this.pictureBoxVictoryDefeat = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVictoryDefeat)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxVictoryDefeat
            // 
            this.pictureBoxVictoryDefeat.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxVictoryDefeat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxVictoryDefeat.Location = new System.Drawing.Point(304, 235);
            this.pictureBoxVictoryDefeat.Name = "pictureBoxVictoryDefeat";
            this.pictureBoxVictoryDefeat.Size = new System.Drawing.Size(739, 289);
            this.pictureBoxVictoryDefeat.TabIndex = 0;
            this.pictureBoxVictoryDefeat.TabStop = false;
            // 
            // VictoryDefeat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1362, 801);
            this.Controls.Add(this.pictureBoxVictoryDefeat);
            this.Name = "VictoryDefeat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VictoryDefeat";
            this.Load += new System.EventHandler(this.VictoryDefeat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVictoryDefeat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxVictoryDefeat;
    }
}