using System;

namespace bead.Persistence
{
    public class GamePlayer : GameObject
    {
        public GamePlayer(int x, int y)
        {
            IsCaught = false;
            mPosition = new Tuple<int, int>(x, y);
        }

        public bool IsCaught { get; }
    }
}