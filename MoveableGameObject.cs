using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace DelveCodeB
{
    public abstract class MoveableGameObject : GameObject
    {
        // attributes
        protected int direction;
        public int Direction // get and set properties
        {
            get { return direction; }
            set { direction = value; }
        }

        public MoveableGameObject(int x, int y, int width, int height): base()
        {

        }
        public abstract void TakeHit(); // create basic takehit method
    }
}
