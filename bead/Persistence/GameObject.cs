using System;
using System.Collections.Generic;
using System.Text;

namespace bead.Persistence
{
    public class GameObject
    {
        protected Tuple<Int32, Int32> mPosition;
        public Tuple<int, int> Position { get => mPosition; set => mPosition = value; }
    }
}
