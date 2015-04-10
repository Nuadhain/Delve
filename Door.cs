using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelveCodeB
{
    class Door : GameObject
    {
        // door class to make sure doors can be passed through
        public Door(int x, int y, int width, int height) : base()
        {
            width = 50;
            height = 50;
        }
    }
}
