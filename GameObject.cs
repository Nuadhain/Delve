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
    public abstract class GameObject
    {
        // abstract class to provide location and size of everything in the game, as well as to make a basic collision method
        public Rectangle rect;
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        public GameObject(int x, int y, int width, int height)
        {
            rect = new Rectangle(x, y, width, height);
            
        }

        public Boolean IsColliding(GameObject piece) // not sure what to do with this, probably in player
        {
            if (piece.rect.Intersects(rect))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
