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
            this.GameAdvanceTimer = new System.Windows.Forms.Timer(this.components);
            this.NewGame = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.FoodEaten = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // NewGame
            // 
            this.NewGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewGame.Location = new System.Drawing.Point(49, 26);
            this.NewGame.Margin = new System.Windows.Forms.Padding(2);
            this.NewGame.Name = "NewGame";
            this.NewGame.Size = new System.Drawing.Size(112, 34);
            this.NewGame.TabIndex = 1;
            this.NewGame.Text = "Difficulty";
            this.NewGame.UseVisualStyleBackColor = true;
            this.NewGame.Click += new System.EventHandler(this.OnNewGame_click);
            // 
            // Pause
            // 
            this.Pause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pause.Location = new System.Drawing.Point(49, 83);
            this.Pause.Margin = new System.Windows.Forms.Padding(2);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(112, 34);
            this.Pause.TabIndex = 2;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.OnPause_click);
            // 
            // FoodEaten
            // 
            this.FoodEaten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FoodEaten.AutoSize = true;
            this.FoodEaten.Location = new System.Drawing.Point(49, 245);
            this.FoodEaten.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FoodEaten.Name = "FoodEaten";
            this.FoodEaten.Size = new System.Drawing.Size(102, 25);
            this.FoodEaten.TabIndex = 3;
            this.FoodEaten.Text = "Food eaten";
            // 
            // TimeLabel
            // 
            this.TimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(49, 204);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(116, 25);
            this.TimeLabel.TabIndex = 4;
            this.TimeLabel.Text = "Time elapsed";
            // 
            // Start
            // 
            this.Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Start.Location = new System.Drawing.Point(49, 142);
            this.Start.Margin = new System.Windows.Forms.Padding(2);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(112, 34);
            this.Start.TabIndex = 5;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 315);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.FoodEaten);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.NewGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GameForm";
            this.Text = "MaciLaci";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer GameAdvanceTimer;
        private System.Windows.Forms.Button NewGame;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.Label FoodEaten;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Timer UpdateTimer;
    }
}

