namespace battleship
{
    partial class MainGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGame));
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxShip5 = new System.Windows.Forms.PictureBox();
            this.pictureBoxShip3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxShip4 = new System.Windows.Forms.PictureBox();
            this.pictureBoxShip2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShip5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShip3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShip4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShip2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 63);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pictureBoxShip5);
            this.panel1.Controls.Add(this.pictureBoxShip3);
            this.panel1.Controls.Add(this.pictureBoxShip4);
            this.panel1.Controls.Add(this.pictureBoxShip2);
            this.panel1.Location = new System.Drawing.Point(32, 231);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 550);
            this.panel1.TabIndex = 0;
            // 
            // pictureBoxShip5
            // 
            this.pictureBoxShip5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxShip5.Image")));
            this.pictureBoxShip5.Location = new System.Drawing.Point(797, 103);
            this.pictureBoxShip5.Name = "pictureBoxShip5";
            this.pictureBoxShip5.Size = new System.Drawing.Size(240, 30);
            this.pictureBoxShip5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxShip5.TabIndex = 7;
            this.pictureBoxShip5.TabStop = false;
            this.pictureBoxShip5.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip5_MouseDoubleClick);
            this.pictureBoxShip5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip5_MouseDown);
            this.pictureBoxShip5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip5_MouseMove);
            // 
            // pictureBoxShip3
            // 
            this.pictureBoxShip3.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBoxShip3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxShip3.Image")));
            this.pictureBoxShip3.Location = new System.Drawing.Point(902, 224);
            this.pictureBoxShip3.Name = "pictureBoxShip3";
            this.pictureBoxShip3.Size = new System.Drawing.Size(135, 31);
            this.pictureBoxShip3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxShip3.TabIndex = 5;
            this.pictureBoxShip3.TabStop = false;
            this.pictureBoxShip3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip3_MouseDoubleClick);
            this.pictureBoxShip3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip3_MouseDown);
            this.pictureBoxShip3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip3_MouseMove);
            // 
            // pictureBoxShip4
            // 
            this.pictureBoxShip4.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBoxShip4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxShip4.Image")));
            this.pictureBoxShip4.Location = new System.Drawing.Point(837, 154);
            this.pictureBoxShip4.Name = "pictureBoxShip4";
            this.pictureBoxShip4.Size = new System.Drawing.Size(200, 40);
            this.pictureBoxShip4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxShip4.TabIndex = 4;
            this.pictureBoxShip4.TabStop = false;
            this.pictureBoxShip4.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip4_MouseDoubleClick);
            this.pictureBoxShip4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip4_MouseDown);
            this.pictureBoxShip4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip4_MouseMove);
            // 
            // pictureBoxShip2
            // 
            this.pictureBoxShip2.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBoxShip2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxShip2.Image")));
            this.pictureBoxShip2.Location = new System.Drawing.Point(956, 274);
            this.pictureBoxShip2.Name = "pictureBoxShip2";
            this.pictureBoxShip2.Size = new System.Drawing.Size(81, 27);
            this.pictureBoxShip2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxShip2.TabIndex = 6;
            this.pictureBoxShip2.TabStop = false;
            this.pictureBoxShip2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip2_MouseDoubleClick);
            this.pictureBoxShip2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip2_MouseDown);
            this.pictureBoxShip2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxShip2_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(756, 231);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 550);
            this.panel2.TabIndex = 1;
            // 
            // MainGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1362, 801);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "battleship";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainGame_FormClosing);
            this.Load += new System.EventHandler(this.MainGame_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShip5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShip3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShip4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShip2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxShip4;
        private System.Windows.Forms.PictureBox pictureBoxShip3;
        private System.Windows.Forms.PictureBox pictureBoxShip2;
        private System.Windows.Forms.PictureBox pictureBoxShip5;
        private System.Windows.Forms.Panel panel2;
    }
}