﻿using System;
using System.Collections.Generic;
using System.Text;

namespace bead.Persistence
{
    public class GameFood : GameObject
    {
        public GameFood(Int32 X, Int32 Y)
        {
            mPosition = new Tuple<Int32, Int32>(X, Y);
        }
    }
}