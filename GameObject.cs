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
        public int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int height;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public GameObject()
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
