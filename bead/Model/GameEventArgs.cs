using System;

namespace bead.Model
{
    public class GameEventArgs : EventArgs
    {
        public GameEventArgs(int time, bool isWon, int foodLeft)
        {
            this.Time = time;
            this.IsWon = isWon;
            this.FoodLeft = foodLeft;
        }

        public int Time { get; }

        public bool IsWon { get; }

        public int FoodLeft { get; }
    }
}