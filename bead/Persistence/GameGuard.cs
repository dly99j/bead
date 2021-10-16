using System;
using System.Collections.Generic;
using System.Text;

namespace bead.Persistence
{
    public enum Direction { Up, Right, Down, Left }
    public class GameGuard : GameObject
    {
        private Direction mDirection;
        public Direction Direction { get => mDirection; set => mDirection = value; }

        public GameGuard(Int32 X, Int32 Y)
        {
            if ((X + Y) % 2 == 0)
            {
                mDirection = Direction.Up;
            } else
            {
                mDirection = Direction.Right;
            }
            mPosition = new Tuple<Int32, Int32>(X, Y);
        }
    }
}
