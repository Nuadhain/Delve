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
        private int dexterity;
        public int Dexterity
        {
            get { return dexterity; }
            set
            {
                if(dexterity <= 0 )
                {
                    dexterity = 1;
                }
                else if(dexterity >= 15)
                {
                    dexterity = 15;
                }
                else
                {
                    dexterity = value;
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
        private Weapon playerWeapon;
        public Weapon PlayerWeapon
        {
            get { return playerWeapon; }
            set { playerWeapon = value; }
        }
        private string playerName;
        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }
        private Random rgen;
        public Random Rgen
        {
            get { return rgen; }
            set { rgen = value; }
        }
        public Boolean alive = true;
        Weapon newWeapon;

        // constructor

        public Player(int x, int y, int width, int height, string name) : base(x, y, width, height)
        {
            playerName = name;
            playerWeapon = new Weapon(x, y, width, height, name);
            rgen = new Random();
            int value = rgen.Next(2, 11);
            strength = value / 2; // rgen starts at 2 and ends at 10, then is divided by 2 so it will be unlikely the stats will be the same
            dexterity = value / 2; // additionally, health is toughness + 1 so it doesn't take 1 hit to kill you
            toughness = value / 2;
            agility = value / 2;
            health = toughness + 1;
            width = 75;
            height = 75;
    
            newWeapon = new Weapon(x, y, 20, 20, name);
        }

        

        // takehit method
        public override void TakeHit()
        {
            health = health - 1;
            IsAlive();
        }

        // IsAlive method
        public Boolean IsAlive()
        {
            if(health == 0)
            {
                alive = false;
                EndGame();
                return false;
            }
            else
            {
                alive = true;
                return true;
            }
     
        }
        public void EndGame()
        {
            // code to reset the game back to title screen
            
        }
    }
}
