using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelveCodeB
{
    class Weapon : GameObject
    {
        // make weapon class which will be used by player
        
        // attributes
        private string weaponName;
        public string WeaponName
        {
            get { return weaponName; }
            set { weaponName = value; }
        }
        private int weaponRange;
        public int WeaponRange
        {
            get { return weaponRange; }
            set { weaponRange = value; }
        }

        private Texture2D weaponTexture;
        public Texture2D WeaponTexture
        {
            get { return weaponTexture; }
            set { weaponTexture = value; }
        }

        private int weapDirection;
        public int WeapDirection
        {
            get { return weapDirection; }
            set { weapDirection = value; }
        }

        public Weapon(int x, int y, int width, int height) : base(x, y, width, height)
        {
            width = 25;
            height = 25;
            weapDirection = 1;
        }
    }
}
