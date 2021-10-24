using System;

namespace bead.Persistence
{
    public enum GameDirection
    {
        Up,
        Right,
        Down,
        Left
    }

    public class GameGuard : GameObject
    {
        public GameGuard(int x, int y)
        {
            if ((x + y) % 2 == 0)
                Direction = GameDirection.Up;
            else
                Direction = GameDirection.Right;
            mPosition = new Tuple<int, int>(x, y);
        }

        public GameDirection Direction { get; set; }
    }
}