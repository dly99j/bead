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
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Location = new System.Drawing.Point(0, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(452, 455);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdateCanvas);
            // 
            // NewGame
            // 
            this.NewGame.Location = new System.Drawing.Point(533, 27);
            this.NewGame.Margin = new System.Windows.Forms.Padding(2);
            this.NewGame.Name = "NewGame";
            this.NewGame.Size = new System.Drawing.Size(90, 27);
            this.NewGame.TabIndex = 1;
            this.NewGame.Text = "New game";
            this.NewGame.UseVisualStyleBackColor = true;
            this.NewGame.Click += new System.EventHandler(this.OnNewGame_click);
            // 
            // Pause
            // 
            this.Pause.Location = new System.Drawing.Point(533, 67);
            this.Pause.Margin = new System.Windows.Forms.Padding(2);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(90, 27);
            this.Pause.TabIndex = 2;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.OnPause_click);
            // 
            // FoodEaten
            // 
            this.FoodEaten.AutoSize = true;
            this.FoodEaten.Location = new System.Drawing.Point(533, 142);
            this.FoodEaten.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FoodEaten.Name = "FoodEaten";
            this.FoodEaten.Size = new System.Drawing.Size(50, 20);
            this.FoodEaten.TabIndex = 3;
            this.FoodEaten.Text = "label1";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(533, 191);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(50, 20);
            this.TimeLabel.TabIndex = 4;
            this.TimeLabel.Text = "label2";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 466);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.FoodEaten);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.NewGame);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GameForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameForm_KeyPress);
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

