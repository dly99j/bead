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

namespace bead
{
    public partial class Form1 : Form
    {
        private GameModel mGameModel;
        public Form1()
        {
            InitializeComponent();
            mGameModel = new GameModel();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
