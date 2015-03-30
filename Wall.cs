using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelveCodeB
{
    class Wall : GameObject
    {
        public Wall(int x, int y, int width, int height) : base(x, y, width, height)
        {
            width = 100;
            height = 100;
        }
    }
}
