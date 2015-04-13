namespace DelveCodeB
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //list of enemies
        List<Enemy> enemyList = new List<Enemy>();
        //list of all walls
        List<Wall> wallList = new List<Wall>();
        //list of all doors
        List<Door> doorList = new List<Door>();

        Texture2D wallsTexture;
        Texture2D doorsTexture;
        Texture2D baseSkeletonTexture;
        Texture2D skeletonWalkingLeft;
        Texture2D skeletonWalkingRight;
        Texture2D swordUpSpriteTexture;
        Texture2D swordRightSpriteTexture;
        Texture2D swordLeftSpriteTexture;
        Texture2D swordDownSpriteTexture;
        Texture2D PlayerAvatarTexture;
        Texture2D gameOverTexture;
        Texture2D roomTexture;

        Rectangle weaponRect;
        Rectangle weaponRect1;
        Rectangle playerRect;
        Rectangle door1;

        Boolean weaponActive = false;
        Boolean allEnemiesCleared = false;

        KeyboardState kstate = new KeyboardState();
        // private KeyboardState prevState; - will be used when we have animation
        Player player1;

        private int weaponTimer;
        private int floorNum = 1;
        Boolean weaponTimerActive;
        MapReader mapFile;
        private Weapon weap;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            playerRect = new Rectangle(600, 300, 50, 50);
            weaponRect = new Rectangle(playerRect.X + 40, playerRect.Y + 9, 25, 25);
            weaponRect1 = new Rectangle(playerRect.X + 40, playerRect.Y + 9, 25, 25);
            player1 = new Player(playerRect.X, playerRect.Y, playerRect.Width, playerRect.Height, "Player");
            weap = new Weapon(weaponRect.X, weaponRect.Y, 25, 25);
            weaponTimer = 0;
            weaponTimerActive = false;
            mapFile = new MapReader();
            graphics.PreferredBackBufferWidth = 1250;
            graphics.PreferredBackBufferHeight = 625;
            graphics.ApplyChanges();
            // initialize playerAvatar, font and vector2 with correct dimensions

            // load maps and draw game here
            mapFile.readRoom(3);


            //loop through everything
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {//Draw door at correct location
                    Rectangle door1 = new Rectangle(i * 50, j * 25, 50, 50);
                    if (mapFile.roomChars[i, j] == 'D')
                    {
                        doorList.Add(new Door(i * 50, j * 25, 50, 50));

                    }
                    if (mapFile.roomChars[i, j] == 'O')
                    {
                        //Add each new wall to the list of walls to draw and interact with.
                        wallList.Add(new Wall(i * 50, j * 25, 50, 50));
                    }

                    if (mapFile.roomChars[i, j] == 'E')
                    {
                        //Add a new enemy to the list of enemies to draw and interact with.
                        enemyList.Add(new Enemy(i * 50, j * 25, 50, 50, floorNum));
                    }
                }
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PlayerAvatarTexture = Content.Load<Texture2D>("Heavy");
            swordUpSpriteTexture = Content.Load<Texture2D>("SwordUse");
            swordRightSpriteTexture = Content.Load<Texture2D>("SwordUseSide");
            swordLeftSpriteTexture = Content.Load<Texture2D>("SwordUseSideLeft");
            swordDownSpriteTexture = Content.Load<Texture2D>("SwordUseDown");
            gameOverTexture = Content.Load<Texture2D>("EndGame");
            roomTexture = Content.Load<Texture2D>("Room");
            baseSkeletonTexture = Content.Load<Texture2D>("Skeleton");
            skeletonWalkingLeft = Content.Load<Texture2D>("SkeletonWalking");
            skeletonWalkingRight = Content.Load<Texture2D>("SkeletonWalkingRight");
            doorsTexture = Content.Load<Texture2D>("DoorUse");
            wallsTexture = Content.Load<Texture2D>("RockUse");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Check if all enemies are dead.
            if (allEnemiesCleared == false)
            {
                for(int i = 0; i < enemyList.Count; i++)
                {
                    if(enemyList[i].Alive == false)
                    {
                        enemyList.RemoveAt(i);
                    }
                }
            }
            if(enemyList.Count == 0)
            {
                allEnemiesCleared = true;
            }

            //Timer for weapon swings
            if (weaponTimerActive == true)
            {
                weaponTimer++;
            }
            if (weaponTimer == 51 - player1.Dexterity)
            {
                weaponTimer = 0;
                weaponTimerActive = false;
            }

            //Draw the weapon.
            if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyDown(Keys.Up) == false && kstate.IsKeyDown(Keys.Right) == false && kstate.IsKeyDown(Keys.Down) == false)
            {
                weap.WeaponTexture = swordLeftSpriteTexture;
                weap.WeapDirection = 3;
            }
            if (kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyDown(Keys.Left) == false && kstate.IsKeyDown(Keys.Right) == false && kstate.IsKeyDown(Keys.Down) == false)
            {
                weap.WeaponTexture = swordUpSpriteTexture;
                weap.WeapDirection = 0;
            }
            if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyDown(Keys.Left) == false && kstate.IsKeyDown(Keys.Right) == false && kstate.IsKeyDown(Keys.Up) == false)
            {
                weap.WeaponTexture = swordDownSpriteTexture;
                weap.WeapDirection = 2;
            }
            if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyDown(Keys.Up) == false && kstate.IsKeyDown(Keys.Left) == false && kstate.IsKeyDown(Keys.Down) == false)
            {
                weap.WeaponTexture = swordRightSpriteTexture;
                weap.WeapDirection = 1;
            }
            //The movement with "W","A","S","D"
            kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.W) == true && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.S) && kstate.IsKeyUp(Keys.A) && player1.isKnockedBack == false)
            {
                playerRect.Y = playerRect.Y - player1.Agility;
                weaponRect.Y = weaponRect.Y - player1.Agility;
                player1.Direction = 0;
            }
            if (kstate.IsKeyDown(Keys.D) == true && kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.S) && kstate.IsKeyUp(Keys.A) && player1.isKnockedBack == false)
            {
                playerRect.X = playerRect.X + player1.Agility;
                weaponRect.X = weaponRect.X + player1.Agility;
                player1.Direction = 1;
            }
            if (kstate.IsKeyDown(Keys.S) == true && kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.A) && player1.isKnockedBack == false)
            {
                playerRect.Y = playerRect.Y + player1.Agility;
                weaponRect.Y = weaponRect.Y + player1.Agility;
                player1.Direction = 2;
            }
            if (kstate.IsKeyDown(Keys.A) == true && kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.S) && player1.isKnockedBack == false)
            {
                playerRect.X = playerRect.X - player1.Agility;
                weaponRect.X = weaponRect.X - player1.Agility;
                player1.Direction = 3;
            }
            if (kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Down) && weaponTimerActive == false && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponTimerActive = true;
                weaponRect.X = playerRect.X + 12;
                weaponRect.Y = playerRect.Y - 30;
                //spriteBatch.Draw(swordUpSpriteTexture, weaponRect, Color.White);

            }
            //Right is pressed
            if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Down) && weaponTimerActive == false && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponTimerActive = true;
                weaponRect.X = playerRect.X + 40;
                weaponRect.Y = playerRect.Y + 9;
                //spriteBatch.Draw(swordRightSpriteTexture, weaponRect, Color.White);
            }
            //Left is pressed
            if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Down) && weaponTimerActive == false && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponTimerActive = true;
                weaponRect.X = playerRect.X - 10;
                weaponRect.Y = playerRect.Y + 9;
                //spriteBatch.Draw(swordLeftSpriteTexture, weaponRect, Color.White);
            }
            //Down is pressed
            if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left) && weaponTimerActive == false && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponTimerActive = true;
                weaponRect.X = playerRect.X + 12;
                weaponRect.Y = playerRect.Y + 40;
                //spriteBatch.Draw(swordDownSpriteTexture, weaponRect, Color.White);
            }

            //No keys pressed
            if (kstate.IsKeyDown(Keys.Down) == false && kstate.IsKeyDown(Keys.Left) == false && kstate.IsKeyDown(Keys.Right) == false && kstate.IsKeyDown(Keys.Up) == false)
            {
                weaponActive = false;
            }

            // enemy movement with set path for x/y coordinates: if collision occurs between you and enemy, you take damage, if it occurs
            // between your weapon and the enemy, the enemy will die
            // REPLACE THIS CODE WITH PATHING WHEN READY

            //Removed this because it conflicted with the rewrite.

            // make sure updated player direction based on what the last key you hit was goes here!

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(roomTexture, new Rectangle(0, 0, 1250, 625), Color.White);


            if (weaponRect.X <= 30)
            {
                playerRect.X = 30;
                weaponRect.X = 60;
            }
            if (playerRect.X <= 30)
            {
                playerRect.X = 30;
                weaponRect.X = 60;
            }

            if (playerRect.Y <= 0)
            {
                weaponRect.Y = 18;
                weaponRect.X = playerRect.X + 30;
                playerRect.Y = 1;
            }
            if (weaponRect.Y <= 0)
            {
                weaponRect.Y = 18;
                weaponRect.X = playerRect.X + 30;
                playerRect.Y = 1;
            }
            if (playerRect.X >= graphics.PreferredBackBufferWidth - 80)
            {
                playerRect.X = graphics.PreferredBackBufferWidth - 85;
                weaponRect.X = playerRect.X - 10;
            }
            if (playerRect.Y >= graphics.PreferredBackBufferHeight - 65)
            {
                playerRect.Y = graphics.PreferredBackBufferHeight - 70;
            }
            if (weaponRect.Y >= graphics.PreferredBackBufferHeight - 70)
            {
                weaponRect.Y -= 5;
            }


         

            //Checks intersection between player and enemies.

            foreach (Enemy foe in enemyList)
            {
                if (foe.Rect.Intersects(playerRect))
                {
                    switch (player1.Direction)
                    {
                        case 0:
                            {

                                playerRect = new Rectangle(playerRect.X, playerRect.Y + 50, playerRect.Width, playerRect.Height);
                                weaponRect = new Rectangle(weaponRect.X, weaponRect.Y + 50, weaponRect.Width, weaponRect.Height);
                                player1.TakeHit();
                                break;

                            }
                        case 1:
                            {
                                playerRect = new Rectangle(playerRect.X - 100, playerRect.Y, playerRect.Width, playerRect.Height);
                                weaponRect = new Rectangle(weaponRect.X - 100, weaponRect.Y, weaponRect.Width, weaponRect.Height);
                                player1.TakeHit();
                                break;
                            }
                        case 2:
                            {
                                playerRect = new Rectangle(playerRect.X, playerRect.Y - 50, playerRect.Width, playerRect.Height);
                                weaponRect = new Rectangle(weaponRect.X, weaponRect.Y - 50, weaponRect.Width, weaponRect.Height);
                                player1.TakeHit();
                                break;
                            }
                        case 3:
                            {
                                playerRect = new Rectangle(playerRect.X + 100, playerRect.Y, playerRect.Width, playerRect.Height);
                                weaponRect = new Rectangle(weaponRect.X + 100, weaponRect.Y, weaponRect.Width, weaponRect.Y);
                                player1.TakeHit();
                                break;
                            }
                    }
                }
            }


            // draw collisions of weapon and enemy - REPLACE WHEN YOU CAN, THIS ONLY TESTS TO SEE IF THE WEAPON COLLIDES,
            // AND IF IT DOES IT DELETES THE ENEMY TEMPORARILY, MAKE SURE TO REPLACE!

            //Weapon colliding with enemies while being swung
            foreach (Enemy foe in enemyList)
            {
                if (weaponActive)
                {
                    while (weaponRect.Intersects(foe.Rect))
                    {
                        switch (player1.Direction)
                        {
                            case 0:
                                {
                                    foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y - 50, foe.Rect.Width, foe.Rect.Height);
                                    foe.TakeHit();
                                    break;
                                }
                            case 1:
                                {
                                    foe.Rect = new Rectangle(foe.Rect.X + 100, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                                    foe.TakeHit();
                                    break;
                                }
                            case 2:
                                {
                                    foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y + 50, foe.Rect.Width, foe.Rect.Height);
                                    foe.TakeHit();
                                    break;
                                }
                            case 3:
                                {
                                    foe.Rect = new Rectangle(foe.Rect.X - 100, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                                    foe.TakeHit();
                                    break;
                                }
                        }
                    }
                }
            }

            //Draw all enemies.
            foreach (Enemy foe in enemyList)
            {
                switch (foe.Direction)
                {
                    //0=up 1=right 2=down 3=left
                    case 0: spriteBatch.Draw(baseSkeletonTexture, foe.Rect, Color.White); break;
                    case 1: spriteBatch.Draw(skeletonWalkingRight, foe.Rect, Color.White); break;
                    case 2: spriteBatch.Draw(baseSkeletonTexture, foe.Rect, Color.White); break;
                    case 3: spriteBatch.Draw(skeletonWalkingLeft, foe.Rect, Color.White); break;
                }

            }

            // draw all walls
            foreach(Wall obstruction in wallList)
            {
                spriteBatch.Draw(wallsTexture, obstruction.Rect, Color.White);
            }

            // draw the weapon
            if (kstate.IsKeyDown(Keys.Down) == true || kstate.IsKeyDown(Keys.Left) == true || kstate.IsKeyDown(Keys.Right) == true || kstate.IsKeyDown(Keys.Up) == true)
            {
                switch (weap.WeapDirection)
                {
                    case 0: spriteBatch.Draw(swordUpSpriteTexture, weaponRect, Color.White);
                        break;

                    case 1: spriteBatch.Draw(swordRightSpriteTexture, weaponRect, Color.White);
                        break;

                    case 2: spriteBatch.Draw(swordDownSpriteTexture, weaponRect, Color.White);
                        break;

                    case 3: spriteBatch.Draw(swordLeftSpriteTexture, weaponRect, Color.White);
                        break;
                }
            }

            //draw the player 
            spriteBatch.Draw(PlayerAvatarTexture, playerRect, Color.White);

            // draw the doors
            foreach(Door portal in doorList)
            {
                spriteBatch.Draw(doorsTexture, portal.Rect, Color.White);
            }

            while (player1.IsAlive() == false)
            {
                spriteBatch.Draw(gameOverTexture, new Rectangle(0, 0, 1250, 625), Color.White);
                break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
