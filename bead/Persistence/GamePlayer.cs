using System;
using System.Collections.Generic;
using System.Text;

namespace bead.Persistence
{
    public class GamePlayer : GameObject
    {
        private Boolean mIsCaught;
        public Boolean IsCaught { get { return mIsCaught; } }

        public GamePlayer(Int32 X, Int32 Y) 
        {
            mIsCaught = false;
            mPosition = new Tuple<Int32, Int32>(X, Y);
        }
    }
}
