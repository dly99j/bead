using System;
using bead.Persistence;

namespace bead.Model
{
    public class GameModel
    {
        #region data

        private IGameDataAccess mDataAccess;
        private GameTable mTable;
        private Int32 mGameTime;
        private Int32 mFoodCount;


        #endregion

        #region properties

        public Int32 FoodCount { get { return mFoodCount; } }
        public Int32 GameTime { get { return mGameTime; } }
        public GameTable Table { get { return mTable; } }
        public Boolean IsGameOver { get { return (mFoodCount == mTable.NumOfFood ||); } }

        #endregion
    }
}
