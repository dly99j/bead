using System;

namespace bead.Model
{
    public class GameEventArgs : EventArgs
    {
        private Int32 mGameTime;
        private Boolean mIsWon;
        private Int32 mFoodLeft;

        public Int32 GameTime { get { return mGameTime; } }
        public Boolean IsWon { get { return mIsWon; } }
        public Int32 FoodLeft { get { return mFoodLeft; } }

        public GameEventArgs(int GameTime, bool IsWon, int FoodLeft)
        {
            mGameTime = GameTime;
            mIsWon = IsWon;
            mFoodLeft = FoodLeft;
        }
    }
}
