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
        private int speed;
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        Player player1;
        Enemy enem;
        
        public Enemy(int x, int y, int width, int height, string nm) : base(x, y, width, height)
        {
            health = 1;
            strength = 1;
            speed = 2;
            name = nm;
            width = 75;
            height = 75;
            player1 = new Player(x, y, width, height, name);
            enem = new Enemy(x, y, width, height, name);
            alive = true;
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
            if(health <= 0)
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
