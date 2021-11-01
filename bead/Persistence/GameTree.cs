using System;

namespace bead.Persistence
{
    public class GameTree : GameObject
    {
        public GameTree(int m, int n)
        {
            mPosition = new Tuple<int, int>(m, n);
        }
    }
}