using System;

namespace bead.Persistence
{
    public class GameFood : GameObject
    {
        public GameFood(int x, int y)
        {
            mPosition = new Tuple<int, int>(x, y);
        }
    }
}