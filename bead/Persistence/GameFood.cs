﻿using System;

namespace bead.Persistence
{
    public class GameFood : GameObject
    {
        public GameFood(int m, int n)
        {
            mPosition = new Tuple<int, int>(m, n);
        }
    }
}