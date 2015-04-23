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
    class Player : MoveableGameObject
    {
        // properties
        private int strength;
        public int Strength
        {
            get { return strength; }
            set
            {
                if (strength <= 0)
                {
                    strength = 1;
                }
                else if (strength >= 15)
                {
                    strength = 15;
                }
                else
                {
                    strength = value;
                }
            }
        }
          private int luck;
        public int Luck
        {
            get { return luck; }
            set
            {
                if (luck <= 0)
                {
                    luck = 1;
                }
                else if (luck >= 15)
                {
                    luck = 15;
                }
                else
                {
                    luck = value;
                }
            }
        }
        private int toughness;
        public int Toughness
        {
            get { return toughness; }
            set
            {
                if (toughness <= 0)
                {
                    toughness = 1;
                }
                else if (toughness >= 15)
                {
                    toughness = 15;
                }
                else
                {
                    toughness = value;
                }
            }
        }
        private int health;
        public int Health
        {
            get { return health; }
            set
            {
                if (health <= 0)
                {
                    health = 1;
                }
                else if (health >= 15)
                {
                    health = 15;
                }
                else
                {
                    health = value;
                }
            }
        }
        private int agility;
        public int Agility
        {
            get { return agility; }
            set
            {
                if (agility <= 0)
                {
                    agility = 1;
                }
                else if (agility >= 15)
                {
                    agility = 15;
                }
                else
                {
                    agility = value;
                }
            }
        }
        private string playerName;
        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        // random number generators
        private Random rgen1;
        private Random rgen2;
        private Random rgen3;
        private Random rgen4;

        public Boolean alive = true;
        private int knockbackTimer;
        public int KnockbackTimer
        {
            get { return knockbackTimer; }
            set { knockbackTimer = value; }
        }
        public Boolean isKnockedBack = false;

        int[] possStartAttributes = new int[4];



        // constructor

        public Player(int x, int y, int width, int height, string name)
            : base(x, y, width, height)
        {
            playerName = name;
            rgen1 = new Random();
            rgen2 = new Random();
            rgen3 = new Random();
            rgen4 = new Random();
            int value = rgen1.Next(0, 4);
            int value1 = rgen2.Next(0, 4);
            int value2 = rgen3.Next(0, 4);
            int value3 = rgen4.Next(0, 4);
            width = 50;
            height = 50;
            knockbackTimer = 0;

            possStartAttributes[0] = 1;
            possStartAttributes[1] = 2;
            possStartAttributes[2] = 3;
            possStartAttributes[3] = 5;

            while (value == value1 || value == value2 || value == value3)
            {
                value = rgen1.Next(0, 4);
            }
            while (value1 == value || value1 == value2 || value1 == value3)
            {
                value1 = rgen2.Next(0, 4);
            }
            while (value2 == value || value2 == value1 || value2 == value3)
            {
                value2 = rgen3.Next(0, 4);
            }
            while (value3 == value || value3 == value2 || value3 == value1)
            {
                value3 = rgen4.Next(0, 4);
            }

            strength = possStartAttributes[value];
            dexterity = possStartAttributes[value1];
            agility = possStartAttributes[value2];
            toughness = possStartAttributes[value3];
            health = toughness + 2;
        }


        // takehit method
        public override void TakeHit()
        {
            health = health - 1;
            isKnockedBack = true;
            knockbackTimer++;
            if (knockbackTimer == 1)
            {
                knockbackTimer = 0;
                isKnockedBack = false;
            }
            IsAlive();
        }

        // IsAlive method
        public Boolean IsAlive()
        {
            if (health <= 0)
            {
                alive = false;
                return false;
            }
            else
            {
                alive = true;
                return true;
            }

        }
    }
}
