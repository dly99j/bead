using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using bead.Persistence;

namespace bead.Model
{
    public class GameModel
    {
        #region constructors

        public GameModel(IGameDataAccess dataAccess)
        {
            mDataAccess = dataAccess;
            GameTable = new GameTable();
            Time = 0;
            FoodCount = 5;
        }

        #endregion

        #region data

        private readonly IGameDataAccess mDataAccess;

        #endregion

        #region properties

        public int FoodCount { get; private set; }

        public int Time { get; private set; }

        public GameTable GameTable { get; private set; }

        public bool IsGameOver => GameTable.Ended;

        public int Width => GameTable.X;
        public int Height => GameTable.Y;
        public char[,] CharTableRepresentation => GameTable.Table;

        #endregion

        #region private methods

        private GameDirection OppositeDir(GameDirection dir)
        {
            switch (dir)
            {
                case GameDirection.Up:
                    return GameDirection.Down;
                case GameDirection.Right:
                    return GameDirection.Left;
                case GameDirection.Down:
                    return GameDirection.Up;
                case GameDirection.Left:
                    return GameDirection.Right;
            }

            throw new Exception("did I add an extra to the enum? or idk, check this. i just want to stop the " +
                                "compiler from giving me an error that this might not return");
        }

        private bool CanMove(GameObject movable, GameDirection dir)
        {
            var posX = movable.Position.Item1;
            var posY = movable.Position.Item2;
            switch (dir)
            {
                case GameDirection.Up:
                    if (posY + 1 < GameTable.Y &&
                        GameTable.Table[posX, posY + 1] != 'T')
                        return true;
                    break;
                case GameDirection.Right:
                    if (posX + 1 < GameTable.X &&
                        GameTable.Table[posX + 1, posY] != 'T')
                        return true;
                    break;
                case GameDirection.Down:
                    if (posY - 1 >= 0 &&
                        GameTable.Table[posX, posY - 1] != 'T')
                        return true;
                    break;
                case GameDirection.Left:
                    if (posX - 1 >= 0 &&
                        GameTable.Table[posX - 1, posY] != 'T')
                        return true;
                    break;
            }

            return false;
        }

        private void MoveMovable(GameObject movable, GameDirection dir)
        {
            var posX = movable.Position.Item1;
            var posY = movable.Position.Item2;
            if (CanMove(movable, dir))
                switch (dir)
                {
                    case GameDirection.Up:
                        movable.Position = new Tuple<int, int>(posX, posY + 1);
                        break;
                    case GameDirection.Right:
                        movable.Position = new Tuple<int, int>(posX + 1, posY);
                        break;
                    case GameDirection.Down:
                        movable.Position = new Tuple<int, int>(posX, posY - 1);
                        break;
                    case GameDirection.Left:
                        movable.Position = new Tuple<int, int>(posX - 1, posY);
                        break;
                }
        }

        private void MoveGuard(GameGuard g)
        {
            if (!CanMove(g, g.Direction))
                g.Direction = OppositeDir(g.Direction);
            else
                MoveMovable(g, g.Direction);
        }

        private bool IsVisibleForGuards()
        {
            var playerX = GameTable.Player.Position.Item1;
            var playerY = GameTable.Player.Position.Item2;
            foreach (var v in GameTable.Guards)
            {
                var guardX = v.Position.Item1;
                var guardY = v.Position.Item2;

                if (Enumerable.Range(playerX - 1, playerX + 1).Contains(guardX) ||
                    Enumerable.Range(playerY - 1, playerY + 1).Contains(guardY))
                    return true;
            }

            return false;
        }

        #endregion

        #region public methods

        public async Task MoveGuards()
        {
            foreach (var g in GameTable.Guards) MoveGuard(g);
        }

        public async Task MovePlayer(GameDirection dir)
        {
            if (CanMove(GameTable.Player, dir)) MoveMovable(GameTable.Player, dir);

            foreach (var v in GameTable.Foods)
                if (v.Position.Equals(GameTable.Player.Position))
                {
                    GameTable.Foods.Remove(v);
                    --GameTable.NumOfFood;
                }
        }

        public void UpdateTable()
        {
            var newTable = new char[GameTable.X, GameTable.Y];

            for (var i = 0; i < GameTable.Y; ++i)
                for (var j = 0; j < GameTable.X; ++j)
                    newTable[j, i] = 'E';

            foreach (var v in GameTable.Foods)
                newTable[v.Position.Item1, v.Position.Item2] = 'F';

            foreach (var v in GameTable.Trees)
                newTable[v.Position.Item1, v.Position.Item2] = 'T';

            foreach (var v in GameTable.Guards)
                newTable[v.Position.Item1, v.Position.Item2] = 'G';

            newTable[GameTable.Player.Position.Item1, GameTable.Player.Position.Item2] = 'P';

            GameTable.Table = newTable;
        }

        public async Task LoadGameAsync(string path)
        {
            if (mDataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            GameTable = await mDataAccess.LoadAsync(path);
            Time = 0;
            FoodCount = GameTable.NumOfFood;
        }

        public async void GameStep()
        {
            if (IsGameOver)
                return;

            await MoveGuards();
            UpdateTable();

            if (IsVisibleForGuards())
                GameTable.Ended = true;
        }

        public async void PlayerStep(GameDirection dir)
        {
            if (IsGameOver)
                return;

            await MovePlayer(dir);
            UpdateTable();

            if (IsVisibleForGuards())
                GameTable.Ended = true;
        }
        public void AdvanceTime()
        {
            if (IsGameOver)
                return;

            ++Time;
            OnGameAdvanced();
            GameStep();
        }

        #endregion

        #region events

        public event EventHandler<GameEventArgs> Advanced;

        public event EventHandler<GameEventArgs> Over;

        public event EventHandler<GameEventArgs> PlayerMove; 

        #endregion

        #region private event methods

        private void OnGameAdvanced()
        {
            if (Advanced != null)
                Advanced(this, new GameEventArgs(Time, false, FoodCount));
        }

        private void OnPlayerMove()
        {
            if (PlayerMove != null)
                PlayerMove(this, new GameEventArgs(Time, IsGameOver, FoodCount));
        }

        private void OnGameOver(Boolean isWon)
        {
            if (Over != null)
                Over(this, new GameEventArgs(Time, isWon, FoodCount));
        }

        #endregion
    }
}