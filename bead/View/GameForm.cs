using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bead.Model;
using bead.Persistence;

namespace bead
{
    public partial class GameForm : Form
    {
        private GameModel mGameModel;
        private GameDataAccess mDataAccess;
        private Int32 mObjectWidth, mObjectHeight;
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(Object sender, EventArgs e)
        {
            mDataAccess = new GameDataAccess();

            mGameModel = new GameModel(mDataAccess);

            mGameModel.GameAdvanced += new EventHandler<GameEventArgs>(Game_GameAdvanced);
            mGameModel.GameOver += new EventHandler<GameEventArgs>(Game_GameOver);

            mTimer = new Timer();
            mTimer.Interval = 1000;
            mTimer.Tick += new EventHandler(Timer_Tick);


        }

        private void Game_GameAdvanced(Object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void Game_GameOver(Object sender, GameEventArgs e)
        {
            mTimer.Stop();

            if (e.IsWon)
            {
                MessageBox.Show("Congrats. You won", "MaciLaci",MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("You suck.", "MaciLaci", MessageBoxButtons.OK);
            }
        }

        private void DrawObject(GameObject obj, Graphics canvas, Brush colour)
        {
            var canvas = e.Graphics;
            canvas.FillRectangle(colour, new Rectangle
            (
                obj.Position.Item1 * mObjectWidth,
                obj.Position.Item2 * mObjectHeight,
                mObjectWidth,
                mObjectHeight
            ));
        }

        private void UpdateCanvas(Object sender, PaintEventArgs e)
        {
            var canvas = e.Graphics;

            foreach (var v in mGameModel.Table.Foods)
            {
                DrawObject(v, canvas, Brushes.Red);
            }

            foreach (var v in mGameModel.Table.Trees)
            {
                DrawObject(v, canvas, Brushes.Brown);
            }

            foreach (var v in mGameModel.Table.Guards)
            {
                DrawObject(v, canvas, Brushes.Blue);
            }

            DrawObject(mGameModel.Table.Player, canvas, Brushes.Green);
        }

        private void Timer_Tick(Object sender, EventArgs e)
        {
            
        }

        private void MenuFileLoadGame_Click(Object sender, EventArgs e)
        {
            
        }

        private void OnNewGame_click(object sender, EventArgs e)
        {

        }

        private void OnPause_click(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(Object sender, CancelEventArgs e)
        {

        }
    }
}
