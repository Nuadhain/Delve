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

        public Weapon(int x, int y, int width, int height, string name) : base(x, y, width, height)
        {
            width = 25;
            height = 25;
            weaponName = name;
        }
    }
}
