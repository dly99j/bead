using System;
using System.Windows.Forms;

namespace bead.View
{
    public partial class DifficultyForm : Form
    {
        public DifficultyForm()
        {
            InitializeComponent();
        }

        public Difficulty GameDifficulty { get; private set; }

        private void Easy_Click(object sender, EventArgs e)
        {
            GameDifficulty = Difficulty.Easy;
            this.Close();
        }

        private void Medium_Click(object sender, EventArgs e)
        {
            GameDifficulty = Difficulty.Medium;
            this.Close();
        }

        private void Hard_Click(object sender, EventArgs e)
        {
            GameDifficulty = Difficulty.Hard;
            this.Close();
        }
    }
}