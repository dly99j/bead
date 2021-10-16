using System;
using System.Collections.Generic;
using System.Linq;

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

        private Direction oppositeDir(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
            }
            throw new Exception("did I add an extra to the enum? or idk, check this. i just want to stop the " +
                                "compiler from giving me an error that this might not return");
        }

        private Boolean canMove(GameObject movable, Direction dir)
        {
            var posX = movable.Position.Item1;
            var posY = movable.Position.Item2;
            switch (dir)
            {
                case Direction.Up:
                    if (posY - 1 >= 0 ||
                        mTable[posX, posY - 1] != 'T') 
                    { return true; }
                    break;
                case Direction.Right:
                    if (posX + 1 <= mX ||
                        mTable[posX + 1, posY] != 'T')
                    { return true; }
                    break;
                case Direction.Down:
                    if (posY + 1 <= mY ||
                        mTable[posX, posY + 1] != 'T')
                    { return true; }
                    break;
                case Direction.Left:
                    if (posX - 1 >= 0 ||
                        mTable[posX - 1, posY] != 'T')
                    { return true; }
                    break;
            }
            return false;
        }

        private void moveMovable(GameObject movable, Direction dir)
        {
            var posX = movable.Position.Item1;
            var posY = movable.Position.Item2;
            if (canMove(movable, dir))
            {
                switch (dir)
                {
                    case Direction.Up:
                        movable.Position = new Tuple<Int32, Int32>(posX, posY - 1);
                        break;
                    case Direction.Right:
                        movable.Position = new Tuple<Int32, Int32>(posX + 1, posY);
                        break;
                    case Direction.Down:
                        movable.Position = new Tuple<Int32, Int32>(posX, posY + 1);
                        break;
                    case Direction.Left:
                        movable.Position = new Tuple<Int32, Int32>(posX - 1, posY);
                        break;
                }
            }
        }

        private void moveGuard(GameGuard g)
        {
            if (!canMove(g, g.Direction))
            {
                g.Direction = oppositeDir(g.Direction);
            }
            else
            {
                moveMovable(g, g.Direction);
            }
        }

        private Boolean isVisibleForGueards()
        {
            var playerX = mPlayer.Position.Item1;
            var playerY = mPlayer.Position.Item2;
            foreach (var v in mGuards)
            {
                var guardX = v.Position.Item1;
                var guardY = v.Position.Item2;

                if (Enumerable.Range(playerX - 1, playerX + 1).Contains(guardX) ||
                    Enumerable.Range(playerY - 1, playerY + 1).Contains(guardY))
                {
                    return true;
                }
            }
            return false;
        }
        public void moveGuards()
        {
            foreach (var g in mGuards)
            {
                moveGuard(g);
            }
        }

        public void movePlayer(Direction dir)
        {
            if (canMove(mPlayer, dir))
            {
                moveMovable(mPlayer, dir);
            }

            foreach (var v in mFoods)
            {
                if (v.Position.Equals(mPlayer.Position))
                {
                    mFoods.Remove(v);
                    --mNumOfFood;
                }
            }
        }

        public void updateTable()
        {
            var newTable = new Char[mX, mY];

            for (var i = 0; i < mY; ++i)
                for (var j = 0; j < mX; ++j)
                    newTable[j, i] = 'E';

            foreach (var v in mFoods)
                newTable[v.Position.Item1, v.Position.Item2] = 'F';

            foreach (var v in mTrees)
                newTable[v.Position.Item1, v.Position.Item2] = 'T';

            foreach (var v in mGuards)
                newTable[v.Position.Item1, v.Position.Item2] = 'G';

            newTable[mPlayer.Position.Item1, mPlayer.Position.Item2] = 'P';

            mTable = newTable;
        }

        public Tuple<Int32, Int32> Size { get { return new Tuple<Int32, Int32>(mX, mY); } }
        public Int32 NumOfFood { get { return mNumOfFood; } }
        public Boolean GameEnded { get { return mGameEnded; } }

        public Char getField(Int32 X, Int32 Y)
        {
            return mTable[X, Y];
        }

        #endregion
    }
}
