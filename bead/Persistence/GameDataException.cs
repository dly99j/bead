using System;

namespace bead.Persistence
{
    public class GameDataException : Exception
    {
        public GameDataException()
        {
        }
        public GameDataException(string message) : base(message)
        {
        }
    }
}