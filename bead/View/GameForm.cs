using bead.Model;
using bead.Persistence;
using bead.View;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace bead
{
    public enum Difficulty { Easy, Medium, Hard }
    public partial class GameForm : Form
    {
        private GameDataAccess mDataAccess;
        private GameModel mGameModel;
        private DifficultyForm mDifficultyForm;
        private Button[,] mButtonGrid;
        private Boolean mGameStarted = false, mNeedsReload = false, mReAddTick = false;
        private Difficulty mDifficulty;
        private Stopwatch mStopWatch;
        private int mTotalFood;

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            mDataAccess = new GameDataAccess();
            mDifficultyForm = new DifficultyForm();
            mGameModel = new GameModel(mDataAccess);

            mGameModel.Advanced += new EventHandler<GameEventArgs>(advanceTime);
            mGameModel.Over += new EventHandler<GameEventArgs>(gameOver);
            mGameModel.PlayerMove += new EventHandler<GameEventArgs>(playerStep);
            mGameModel.Refresh += new EventHandler<GameEventArgs>(refreshTable);

            GameAdvanceTimer = new Timer();
            GameAdvanceTimer.Interval = 1000;
            mStopWatch = new Stopwatch();
            UpdateTimer = new Timer();
            UpdateTimer.Interval = 1;

            GameAdvanceTimer.Tick += new EventHandler(AdvanceTimer_Tick);
            UpdateTimer.Tick += new EventHandler(UpdateTimer_Tick);

        }

        private void GenerateTable(Object sender, GameEventArgs e)
        {
            mButtonGrid = new Button[e.Table.M, e.Table.N];

            for (Int32 i = 0; i < e.Table.M; ++i)
            {
                for (Int32 j = 0; j < e.Table.N; ++j)
                {
                    mButtonGrid[i, j] = new Button();
                    mButtonGrid[i, j].Location = new Point(5 + 50 * j, 35 + 50 * i);
                    mButtonGrid[i, j].Size = new Size(50, 50);
                    mButtonGrid[i, j].Enabled = false;
                    mButtonGrid[i, j].TabIndex = 100 + i * e.Table.M + j;
                    mButtonGrid[i, j].FlatStyle = FlatStyle.Flat;

                    Controls.Add(mButtonGrid[i, j]);
                }
            }
        }

        private void UpdateFillTable(Object sender, GameEventArgs e)
        {
            for (Int32 i = 0; i < e.Table.M; ++i)
            {
                for (Int32 j = 0; j < e.Table.N; ++j)
                {
                    switch (e.Table.TableCharRepresentation[i, j])
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

        private void AdvanceTimer_Tick(object sender, EventArgs e)
        {
            if (!GameAdvanceTimer.Enabled) return;
            mGameModel.AdvanceTime();
            TimeLabel.Text = "Time: " + mStopWatch.ElapsedMilliseconds / 1000 + " sec";
            FoodEaten.Text = "Food eaten: " + (mTotalFood - mGameModel.FoodCount);
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            mGameModel.RefreshTable();
        }

        private void OnNewGame_click(object sender, EventArgs e)
        {
            mDifficultyForm.ShowDialog();
        }

        private void OnPause_click(object sender, EventArgs e)
        {
            if (mButtonGrid == null) return;
            if (GameAdvanceTimer.Enabled) { GameAdvanceTimer.Stop(); UpdateTimer.Stop(); mStopWatch.Stop(); Pause.Text = "Continue"; }
            else { GameAdvanceTimer.Start(); UpdateTimer.Start(); mStopWatch.Start(); Pause.Text = "Pause"; }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            SetupNewGame(sender, e);
        }

        private void KeyDown_click(object sender, KeyEventArgs e)
        {
            if (!GameAdvanceTimer.Enabled) return;

            switch (e.KeyCode)
            {
                case Keys.W:
                    mGameModel.PlayerStep(GameDirection.Up);
                    break;
                case Keys.A:
                    mGameModel.PlayerStep(GameDirection.Left);
                    break;
                case Keys.S:
                    mGameModel.PlayerStep(GameDirection.Down);
                    break;
                case Keys.D:
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
                        await mGameModel.LoadGameAsync(@"..\..\..\table1.txt");
                        this.ClientSize = new System.Drawing.Size(800, 600);
                        break;
                    case Difficulty.Medium:
                        await mGameModel.LoadGameAsync(@"..\..\..\table2.txt");
                        this.ClientSize = new System.Drawing.Size(1000, 800);
                        break;
                    case Difficulty.Hard:
                        await mGameModel.LoadGameAsync(@"..\..\..\table3.txt");
                        this.ClientSize = new System.Drawing.Size(1150, 1000);
                        break;
                }
                mTotalFood = mGameModel.FoodCount;
            }
            catch (Exception e)
            {
                MessageBox.Show("error while loading map\n" + e.ToString());
            }
        }

        private void Reset(Object sender, EventArgs e)
        {
            foreach (var v in mButtonGrid)
                Controls.Remove(v);

            mDataAccess = new GameDataAccess();
            mGameModel = new GameModel(mDataAccess);

            mGameModel.Advanced += new EventHandler<GameEventArgs>(advanceTime);
            mGameModel.Over += new EventHandler<GameEventArgs>(gameOver);
            mGameModel.PlayerMove += new EventHandler<GameEventArgs>(playerStep);
            mGameModel.Refresh += new EventHandler<GameEventArgs>(refreshTable);

            GameAdvanceTimer = new Timer();
            GameAdvanceTimer.Interval = 1000;
            UpdateTimer = new Timer();
            UpdateTimer.Interval = 1;
            mStopWatch = new Stopwatch();

            if (mReAddTick)
            {
                GameAdvanceTimer.Tick += new EventHandler(AdvanceTimer_Tick);
                UpdateTimer.Tick += new EventHandler(UpdateTimer_Tick);
                mReAddTick = false;
            }

            mNeedsReload = true;
        }

        private void SetupNewGame(Object sender, EventArgs e)
        {
            if (mGameStarted) Reset(sender, e);

            mDifficulty = mDifficultyForm.GameDifficulty;
            
            mGameStarted = true;
            LoadMapFromFile();
            GameAdvanceTimer.Start();
            mStopWatch.Start();
            Pause.Text = "Pause";
        }

        private void advanceTime(Object sender, GameEventArgs e)
        {
            if (mButtonGrid == null || mNeedsReload)
            {
                GenerateTable(sender, e);
                mNeedsReload = false;
            }
            if (!UpdateTimer.Enabled) UpdateTimer.Start();
            UpdateFillTable(sender, e);
        }
        private void refreshTable(Object sender, GameEventArgs e)
        {
            if (mButtonGrid == null || mNeedsReload)
            {
                GenerateTable(sender, e);
                mNeedsReload = false;
            }
            UpdateFillTable(sender, e);
        }
        private void playerStep(Object sender, GameEventArgs e)
        {
            if (mButtonGrid == null || mNeedsReload)
            {
                GenerateTable(sender, e);
                mNeedsReload = false;
            }
            UpdateFillTable(sender, e);
        }
        private void gameOver(Object sender, GameEventArgs e)
        {
            GameAdvanceTimer.Stop();
            UpdateTimer.Stop();
            mStopWatch.Stop();

            if (e.IsWon)
                MessageBox.Show("Congrats. You won", "MaciLaci", MessageBoxButtons.OK);
            else
                MessageBox.Show("You Lost.", "MaciLaci", MessageBoxButtons.OK);

            mReAddTick = true;
        }
    }
}