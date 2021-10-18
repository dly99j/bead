using System;
using System.Collections.Generic;

namespace bead.Persistence
{
    public class GameTable
    {
        #region data

        private Int32 mX;
        private Int32 mY;

        //E = empty field
        //F = Food
        //G = Guard
        //P = Player
        //T = Tree
        private Char[,] mTable;
        private Int32 mNumOfGuards;
        private Int32 mNumOfFood;
        private Boolean mGameEnded;

        private GamePlayer mPlayer;
        private List<GameGuard> mGuards;
        private List<GameFood> mFoods;
        private List<GameTree> mTrees;

        #endregion

        #region constructors

        public GameTable(Int32 X, Int32 Y)
        {
            if (X < 1 || Y < 1)
            {
                throw new ArgumentOutOfRangeException("invalid table size");
            }

            mX = X;
            mY = Y;
            mTable = new Char[mX, mY];
            mNumOfGuards = 0;
            mNumOfFood = 0;
            mGuards = new List<GameGuard>();
            mTrees = new List<GameTree>();
            mFoods = new List<GameFood>();
            mGameEnded = false;
        }

        public GameTable() : this(10, 10) { }

        #endregion

        #region methods

        public void setFieldOnInit(Int32 X, Int32 Y, Char val)
        {
            if (X > mX || X < 0 || Y > mY || Y < 0)
            {
                throw new ArgumentOutOfRangeException("invalid coordinates");
            }

            if (val != 'P' || val != 'G' || val != 'T' || val != 'F' || val != 'E')
            {
                throw new ArgumentException("invalid field type");
            }

            mTable[X, Y] = val;

            if (val == 'G')
            {
                ++mNumOfGuards;
                mGuards.Add(new GameGuard(X, Y));
            } 
            else if (val == 'F')
            {
                ++mNumOfFood;
                mFoods.Add(new GameFood(X, Y));
            } 
            else if (val == 'P')
            {
                mPlayer = new GamePlayer(X, Y);
            } 
            else if (val == 'T')
            {
                mTrees.Add(new GameTree(X, Y));
            }
        }
        public Char getField(Int32 X, Int32 Y)
        {
            return mTable[X, Y];
        }

        #endregion

        #region properties

        public Tuple<Int32, Int32> Size { get { return new Tuple<Int32, Int32>(mX, mY); } }
        public char[,] Table { get => mTable; set => mTable = value; }
        public int X { get => mX; set => mX = value; }
        public int Y { get => mY; set => mY = value; }
        public GamePlayer Player { get => mPlayer; set => mPlayer = value; }
        public List<GameFood> Foods { get => mFoods; set => mFoods = value; }
        public List<GameTree> Trees { get => mTrees; set => mTrees = value; }
        public List<GameGuard> Guards { get => mGuards; set => mGuards = value; }
        public int NumOfGuards { get => mNumOfGuards; set => mNumOfGuards = value; }
        public int NumOfFood { get => mNumOfFood; set => mNumOfFood = value; }
        public bool GameEnded { get => mGameEnded; set => mGameEnded = value; }

        #endregion
    }
}
