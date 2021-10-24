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
        private Button[,] mButtonGrid;
        private Boolean mGameStarted = false, mMapLoaded = false;
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
            UpdateFillTable();
        }

        private void Game_GameOver(object sender, GameEventArgs e)
        {
            mTimer.Stop();

            if (e.IsWon)
                MessageBox.Show("Congrats. You won", "MaciLaci", MessageBoxButtons.OK);
            else
                MessageBox.Show("You suck.", "MaciLaci", MessageBoxButtons.OK);
        }

        private void GenerateTable()
        {
            mButtonGrid = new Button[mGameModel.Width, mGameModel.Height];

            for (Int32 i = 0; i < mGameModel.GameTable.X; i++)
            {
                for (Int32 j = 0; j < mGameModel.GameTable.Y; j++)
                {
                    mButtonGrid[i, j] = new Button();
                    mButtonGrid[i, j].Location = new Point(5 + 50 * j, 35 + 50 * i);
                    mButtonGrid[i, j].Size = new Size(50, 50);
                    mButtonGrid[i, j].Enabled = false; // kikapcsolt állapot
                    mButtonGrid[i, j].TabIndex = 100 + i * mGameModel.GameTable.X + j;
                    mButtonGrid[i, j].FlatStyle = FlatStyle.Flat;

                    Controls.Add(mButtonGrid[i, j]);
                }
            }
        }

        private void UpdateFillTable()
        {
            for (Int32 i = 0; i < mGameModel.Width; i++)
            {
                for (Int32 j = 0; j < mGameModel.Height; j++)
                {
                    switch (mGameModel.CharTableRepresentation[i, j])
                    {
                        case 'E':
                            mButtonGrid[i, j].BackColor = Color.LightGray;
                            break;
                        case 'T':
                            mButtonGrid[i, j].BackColor = Color.Brown;
                            break;
                        case 'F':
                            mButtonGrid[i, j].BackColor = Color.Red;
                            break;
                        case 'G':
                            mButtonGrid[i, j].BackColor = Color.Blue;
                            break;
                        case 'P':
                            mButtonGrid[i, j].BackColor = Color.Green;
                            break;
                    }
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            mGameModel.AdvanceTime();
            UpdateFillTable();
        }

        private void MenuFileLoadGame_Click(object sender, EventArgs e)
        {
        }

        private void OnNewGame_click(object sender, EventArgs e)
        {
            mDifficultyForm.ShowDialog();
        }

        private void OnPause_click(object sender, EventArgs e)
        {
        }

        private void SetupNewGame()
        {
            mDifficulty = mDifficultyForm.GameDifficulty;
            mGameStarted = true;
            LoadMapFromFile();
            GenerateTable();
            UpdateFillTable();
            mTimer.Start();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            SetupNewGame();
        }

        private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    mGameModel.PlayerStep(GameDirection.Up);
                    break;
                case 'a':
                    mGameModel.PlayerStep(GameDirection.Left);
                    break;
                case 's':
                    mGameModel.PlayerStep(GameDirection.Down);
                    break;
                case 'd':
                    mGameModel.PlayerStep(GameDirection.Right);
                    break;
            }
        }

        private async void LoadMapFromFile()
        {
            try
            {
                switch (mDifficulty)
                {
                    case Difficulty.Easy:
                        await mGameModel.LoadGameAsync(@"C:\Users\dr. Jenei Tímea\source\repos\bead\bead\table1.txt");
                        break;
                    case Difficulty.Medium:
                        await mGameModel.LoadGameAsync(@"C:\Users\dr. Jenei Tímea\source\repos\bead\bead\table2.txt");
                        break;
                    case Difficulty.Hard:
                        await mGameModel.LoadGameAsync(@"C:\Users\dr. Jenei Tímea\source\repos\bead\bead\table3.txt");
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("error while loading map\n" + e.ToString());
            }
            mTimer.Start();
        }
    }
}