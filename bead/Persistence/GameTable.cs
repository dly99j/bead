using System;
using System.Collections.Generic;

namespace bead.Persistence
{
    public class GameTable
    {
        #region constructors

        public GameTable(int x, int y)
        {
            if (x < 1 || y < 1) throw new ArgumentOutOfRangeException("invalid table size");

            this.X = x;
            this.Y = y;
            Table = new char[this.X, this.Y];
            NumOfGuards = 0;
            NumOfFood = 0;
            Guards = new List<GameGuard>();
            Trees = new List<GameTree>();
            Foods = new List<GameFood>();
            Ended = false;
        }

        public GameTable() : this(10, 10)
        {
        }

        #endregion

        #region methods

        public void setFieldOnInit(int x, int y, char val)
        {
            if (x > this.X || x < 0 || y > this.Y || y < 0)
                throw new ArgumentOutOfRangeException("invalid coordinates");

            if (val != 'P' || val != 'G' || val != 'T' || val != 'F' || val != 'E')
                throw new ArgumentException("invalid field type");

            Table[x, y] = val;

            if (val == 'G')
            {
                ++NumOfGuards;
                Guards.Add(new GameGuard(x, y));
            }
            else if (val == 'F')
            {
                ++NumOfFood;
                Foods.Add(new GameFood(x, y));
            }
            else if (val == 'P')
            {
                Player = new GamePlayer(x, y);
            }
            else if (val == 'T')
            {
                Trees.Add(new GameTree(x, y));
            }
        }

        public char GetField(int x, int y)
        {
            return Table[x, y];
        }

        #endregion

        #region properties

        public Tuple<int, int> Size => new Tuple<int, int>(X, Y);

        //E = empty field
        //F = Food
        //G = Guard
        //P = Player
        //T = Tree
        public char[,] Table { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public GamePlayer Player { get; set; }
        public List<GameFood> Foods { get; set; }
        public List<GameTree> Trees { get; set; }
        public List<GameGuard> Guards { get; set; }
        public int NumOfGuards { get; set; }
        public int NumOfFood { get; set; }
        public bool Ended { get; set; }

        #endregion
    }
}