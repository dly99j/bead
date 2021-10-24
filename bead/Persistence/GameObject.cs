using System;

namespace bead.Persistence
{
    public class GameObject
    {
        protected Tuple<int, int> mPosition;

        public Tuple<int, int> Position
        {
            get => mPosition;
            set => mPosition = value;
        }
    }
}