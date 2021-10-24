using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using bead.Model;
using bead.Persistence;
using bead.View;

namespace bead
{
    public enum Difficulty {Easy, Medium, Hard}
    public partial class GameForm : Form
    {
        private GameDataAccess mDataAccess;
        private GameModel mGameModel;
        private int mObjectWidth, mObjectHeight;
        private DifficultyForm mDifficultyForm;
        private Boolean mGameStarted = false;
        private Difficulty mDifficulty;

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            mDataAccess = new GameDataAccess();
            mDifficultyForm = new DifficultyForm();
            mGameModel = new GameModel(mDataAccess);

            mGameModel.Advanced += Game_GameAdvanced;
            mGameModel.Over += Game_GameOver;

            mTimer = new Timer();
            mTimer.Interval = 1000;
            mTimer.Tick += Timer_Tick;
        }

        private void Game_GameAdvanced(object sender, EventArgs e)
        {
        }

        private void Game_GameOver(object sender, GameEventArgs e)
        {
            mTimer.Stop();

            if (e.IsWon)
                MessageBox.Show("Congrats. You won", "MaciLaci", MessageBoxButtons.OK);
            else
                MessageBox.Show("You suck.", "MaciLaci", MessageBoxButtons.OK);
        }

        private void DrawObject(GameObject obj, Graphics canvas, Brush colour)
        {
            canvas.FillRectangle(colour, new Rectangle
            (
                obj.Position.Item1 * mObjectWidth,
                obj.Position.Item2 * mObjectHeight,
                mObjectWidth,
                mObjectHeight
            ));
        }

        private void UpdateCanvas(object sender, PaintEventArgs e)
        {
            var canvas = e.Graphics;

            if (mGameStarted)
            {
                foreach (var v in mGameModel.GameTable.Foods) DrawObject(v, canvas, Brushes.Red);

                foreach (var v in mGameModel.GameTable.Trees) DrawObject(v, canvas, Brushes.Brown);

                foreach (var v in mGameModel.GameTable.Guards) DrawObject(v, canvas, Brushes.Blue);

                DrawObject(mGameModel.GameTable.Player, canvas, Brushes.Green);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            mGameModel.AdvanceTime();
            pictureBox1.Invalidate();
        }

        private void MenuFileLoadGame_Click(object sender, EventArgs e)
        {
        }

        private void OnNewGame_click(object sender, EventArgs e)
        {
            mDifficultyForm.ShowDialog();

            mGameStarted = true;
        }

        private void OnPause_click(object sender, EventArgs e)
        {
        }

        private void SetupNewGame()
        {
            mDifficulty = mDifficultyForm.GameDifficulty;

            LoadMapFromFile();
        }

        private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':

            }
        }

        private async void LoadMapFromFile()
        {
            try
            {
                switch (mDifficulty)
                {
                    case Difficulty.Easy:
                        await mGameModel.LoadGameAsync(@"C:\Users\horva\Desktop\bead\bead\table1.txt");
                        break;
                    case Difficulty.Medium:
                        await mGameModel.LoadGameAsync(@"C:\Users\horva\Desktop\bead\bead\table2.txt");
                        break;
                    case Difficulty.Hard:
                        await mGameModel.LoadGameAsync(@"C:\Users\horva\Desktop\bead\bead\table3.txt");
                        break;
                }
            }
            catch
            {
                MessageBox.Show("error while loading map");
            }
        }
    }
}