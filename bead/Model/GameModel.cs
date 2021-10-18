using System;
using bead.Persistence;
using System.Threading.Tasks;
using System.Linq;

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

        #region constructors

        public GameModel(IGameDataAccess dataAccess)
        {
            mDataAccess = dataAccess;
            mTable = new GameTable();
            mGameTime = 0;
            mFoodCount = 5;
        }

        #endregion

        #region properties

        public Int32 FoodCount { get { return mFoodCount; } }
        public Int32 GameTime { get { return mGameTime; } }
        public GameTable Table { get { return mTable; } }
        public Boolean IsGameOver { get { return (mTable.GameEnded); } }

        #endregion

        #region private methods

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
                        mTable.Table[posX, posY - 1] != 'T')
                    { return true; }
                    break;
                case Direction.Right:
                    if (posX + 1 <= mTable.X ||
                        mTable.Table[posX + 1, posY] != 'T')
                    { return true; }
                    break;
                case Direction.Down:
                    if (posY + 1 <= mTable.Y ||
                        mTable.Table[posX, posY + 1] != 'T')
                    { return true; }
                    break;
                case Direction.Left:
                    if (posX - 1 >= 0 ||
                        mTable.Table[posX - 1, posY] != 'T')
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
            var playerX = mTable.Player.Position.Item1;
            var playerY = mTable.Player.Position.Item2;
            foreach (var v in mTable.Guards)
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

        #endregion

        #region public methods


        public void moveGuards()
        {
            foreach (var g in mTable.Guards)
            {
                moveGuard(g);
            }
        }

        public void movePlayer(Direction dir)
        {
            if (canMove(mTable.Player, dir))
            {
                moveMovable(mTable.Player, dir);
            }

            foreach (var v in mTable.Foods)
            {
                if (v.Position.Equals(mTable.Player.Position))
                {
                    mTable.Foods.Remove(v);
                    --mTable.NumOfFood;
                }
            }
        }

        public void updateTable()
        {
            var newTable = new Char[mTable.X, mTable.Y];

            for (var i = 0; i < mTable.Y; ++i)
                for (var j = 0; j < mTable.X; ++j)
                    newTable[j, i] = 'E';

            foreach (var v in mTable.Foods)
                newTable[v.Position.Item1, v.Position.Item2] = 'F';

            foreach (var v in mTable.Trees)
                newTable[v.Position.Item1, v.Position.Item2] = 'T';

            foreach (var v in mTable.Guards)
                newTable[v.Position.Item1, v.Position.Item2] = 'G';

            newTable[mTable.Player.Position.Item1, mTable.Player.Position.Item2] = 'P';

            mTable.Table = newTable;
        }

        public async Task loadGameAsync(String path)
        {
            if (mDataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            mTable = await mDataAccess.LoadAsync(path);
            mGameTime = 0;
            mFoodCount = mTable.NumOfFood;
        }

        #endregion

        #region events

        public event EventHandler<GameEventArgs> GameAdvanced;

        public event EventHandler<GameEventArgs> GameOver;

        #endregion
    }
}
