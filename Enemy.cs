using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelveCodeB
{
    class Enemy : MoveableGameObject
    {
        // enemy class to fight players, will randomly be loaded and have random movement
        private int health;
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        private int strength;
        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }
        private Boolean alive;
        public Boolean Alive
        {
            get { return alive; }
            set { alive = value; }
        }
        private int speed;
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private int floor = 1;
        public int Floor
        {
            get { return floor; }
            set { Floor = value; }
        }

        public Enemy(int x, int y, int width, int height, int floor) : base(x, y, width, height)
        {
            alive = true;
            health = (floor * (1 / 2)) + 3;
        }

        // takehit method
        public override void TakeHit()
        {
            health = health - 1;
            IsAlive();
        }

        // isalive method
        public void IsAlive()
        {
            if (health <= 0)
            {
                alive = false;
            }
            else
            {
                alive = true;
            }
        }
    }
}
