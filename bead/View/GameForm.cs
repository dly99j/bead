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
        private Timer mTimer;
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

        private void Game_GameAdvanced(Object sender, GameEventArgs e)
        {

        }

        private void Game_GameOver(Object sender, GameEventArgs e)
        {

        }

        private void Timer_Tick(Object sender, EventArgs e)
        {
            
        }
    }
}
