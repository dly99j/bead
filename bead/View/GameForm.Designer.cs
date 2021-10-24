namespace bead
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.NewGame = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.FoodEaten = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mTimer
            // 
            this.mTimer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(565, 569);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdateCanvas);
            // 
            // NewGame
            // 
            this.NewGame.Location = new System.Drawing.Point(666, 34);
            this.NewGame.Name = "NewGame";
            this.NewGame.Size = new System.Drawing.Size(112, 34);
            this.NewGame.TabIndex = 1;
            this.NewGame.Text = "button1";
            this.NewGame.UseVisualStyleBackColor = true;
            this.NewGame.Click += new System.EventHandler(this.OnNewGame_click);
            // 
            // Pause
            // 
            this.Pause.Location = new System.Drawing.Point(666, 84);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(112, 34);
            this.Pause.TabIndex = 2;
            this.Pause.Text = "button2";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.OnPause_click);
            // 
            // FoodEaten
            // 
            this.FoodEaten.AutoSize = true;
            this.FoodEaten.Location = new System.Drawing.Point(666, 178);
            this.FoodEaten.Name = "FoodEaten";
            this.FoodEaten.Size = new System.Drawing.Size(59, 25);
            this.FoodEaten.TabIndex = 3;
            this.FoodEaten.Text = "label1";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(666, 239);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(59, 25);
            this.TimeLabel.TabIndex = 4;
            this.TimeLabel.Text = "label2";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 583);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.FoodEaten);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.NewGame);
            this.Controls.Add(this.pictureBox1);
            this.Name = "GameForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer mTimer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button NewGame;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.Label FoodEaten;
        private System.Windows.Forms.Label TimeLabel;
    }
}

