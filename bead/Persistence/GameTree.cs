using System;

namespace bead.Persistence
{
    public class GameTree : GameObject
    {
        public GameTree(int x, int y)
        {
            mPosition = new Tuple<int, int>(x, y);
        }
    }
}