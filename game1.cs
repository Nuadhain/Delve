/// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D playerAvatar;
        Texture2D[] enemyArray;
        Texture2D weaponSprite;
        List<Texture2D> walls;
        List<Texture2D> doors;
        SpriteFont font;
        Vector2 size;
        Rectangle weaponLocation;
        Rectangle weaponLocation1;
        Rectangle location;
        Rectangle enemyLocation;
        Rectangle enemyLocation1;
        Rectangle enemyLocation2;
        Boolean active = false;
        int pattern;
        int pattern2;
        int pattern3;
        KeyboardState kstate = new KeyboardState();
        private KeyboardState prevState;
        Player player1;
        Enemy enem;
        private int timer;
        Boolean timerActive;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            walls = new List<Texture2D>();
            doors = new List<Texture2D>();
            enemyArray = new Texture2D[3];
            enemyLocation = new Rectangle(20, 20, 75, 75);
            enemyLocation1 = new Rectangle(400, 100, 75, 75);
            enemyLocation2 = new Rectangle(250, 250, 75, 75);
            location = new Rectangle(500, 3, 75, 75);
            size = new Vector2(3, 20);
            weaponLocation = new Rectangle(location.X + 60, location.Y + 10, 25, 25);
            weaponLocation1 = new Rectangle(location.X + 60, location.Y + 10, 25, 25);
            player1 = new Player(location.X, location.Y, location.Width, location.Height, "Player");
            pattern = 1;
            pattern2 = 1;
            pattern3 = 1;
            timer = 0;
            timerActive = false;
            // initialize playerAvatar, font and vector2 with correct dimensions
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
            playerAvatar = Content.Load<Texture2D>("Heavy");
            weaponSprite = Content.Load<Texture2D>("sword1");
            for(int i = 0; i < enemyArray.Length; i++)
            {
                enemyArray[i] = Content.Load<Texture2D>("Light");
            }
            

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

            if(timerActive == true)
            {
                timer++;
            }


            if(timer == 30)
            {
                timer = 0;
                timerActive = false;
            }

            // enemy movement with set path for x/y coordinates: if collision occurs between you and enemy, you take damage, if it occurs
            // between your weapon and the enemy, the enemy will die
            // REPLACE THIS CODE WITH PATHING WHEN READY

                if (pattern == 1 || pattern == 0 || pattern == 2)
                {
                    if(pattern == 1)
                    {
                        enemyLocation.X += 1;
                        if (enemyLocation.X >= 400)
                        {
                            pattern--;
                        }
                    }
                    if (pattern == 0)
                    {
                        enemyLocation.X -= 1;
                        if (enemyLocation.X <= 3)
                        {
                                pattern++;
                        }
                    }
                    if(pattern == 2)
                    {
                        enemyLocation.X = 800;
                        enemyLocation.Y = 800;
                    }
                  
                }
                if (pattern2 == 1 || pattern2 == 0 || pattern2 == 2)
                {
                    if (pattern2 == 1)
                    {
                        enemyLocation1.Y += 1;
                        if (enemyLocation1.Y >= 400)
                        {
                            pattern2--;
                        }
                    }
                    if (pattern2 == 0)
                    {
                        enemyLocation1.Y -= 1;
                        if (enemyLocation1.Y <= 3)
                        {
                            pattern2++;
                        }
                    }
                    if(pattern2 == 2)
                    {
                        enemyLocation1.Y = 800;
                        enemyLocation1.X = 800;
                    }

                }
                if (pattern3 == 1 || pattern3 == 0 || pattern3 == 2 || pattern3 == 3 || pattern3 == 4)
                {
                    if (pattern3 == 1)
                    {
                        enemyLocation2.X += 1;
                        if (enemyLocation2.X >= 600)
                        {
                            pattern3++;
                        }
                    }
                    if (pattern3 == 0)
                    {
                        enemyLocation2.X -= 1;
                        if (enemyLocation2.X <= 50)
                        {
                            pattern3 = pattern3 + 3;
                        }
                    }
                    if(pattern3 == 2)
                    {
                        enemyLocation2.Y -= 1;
                        if(enemyLocation2.Y <= 20)
                        {
                            pattern3 = pattern3 - 2;
                        }
                    }
                    if(pattern3 == 3)
                    {
                        enemyLocation2.Y += 1;
                        if(enemyLocation2.Y >= 300)
                        {
                            pattern3 = pattern3 - 2;
                        }
                    }
                    if(pattern3 == 4)
                    {
                        enemyLocation2.X = 800;
                        enemyLocation2.Y = 800;
                    }

                }
            // make sure updated player direction based on what the last key you hit was goes here!

                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aquamarine);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            // load maps and draw game here
            
            spriteBatch.Draw(playerAvatar, location, Color.White);
            spriteBatch.Draw(weaponSprite, weaponLocation, Color.White);
            
            kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.W) == true)
            {
                location.Y = location.Y - 1;
                weaponLocation.Y = weaponLocation.Y - 1;
            }
            if (kstate.IsKeyDown(Keys.D) == true)
            {
                location.X = location.X + 1;
                weaponLocation.X = weaponLocation.X + 1;
            }
            if (kstate.IsKeyDown(Keys.S) == true)
            {
                location.Y = location.Y + 1;
                weaponLocation.Y = weaponLocation.Y + 1;
            }
            if (kstate.IsKeyDown(Keys.A) == true)
            {
                location.X = location.X - 1;
                weaponLocation.X = weaponLocation.X - 1;
            }
            if(kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Down) && timerActive == false)
            {
                active = true;
                timerActive = true;
                weaponLocation.X = location.X + 25;
                weaponLocation.Y = location.Y - 25;
                spriteBatch.Draw(weaponSprite, weaponLocation, Color.Red);
                
            }
            if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Down) && timerActive == false)
            {
                active = true;
                timerActive = true;
                weaponLocation.X = location.X + 75;
                weaponLocation.Y = location.Y + 19;
                spriteBatch.Draw(weaponSprite, weaponLocation, Color.Red);
            }
            if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Down) && timerActive == false)
            {
                active = true;
                timerActive = true;
                weaponLocation.X = location.X - 25;
                weaponLocation.Y = location.Y + 20;
                spriteBatch.Draw(weaponSprite, weaponLocation, Color.Red);
            }
            if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left) && timerActive == false)
            {
                active = true;
                timerActive = true;
                weaponLocation.X = location.X + 25;
                weaponLocation.Y = location.Y + 75;
                spriteBatch.Draw(weaponSprite, weaponLocation, Color.Red);
            }

            if (kstate.IsKeyDown(Keys.Down) == false && kstate.IsKeyDown(Keys.Left) == false && kstate.IsKeyDown(Keys.Right) == false && kstate.IsKeyDown(Keys.Up) == false)
            {
                active = false;
            }

            if(location.Intersects(enemyLocation))
            {
                spriteBatch.Draw(playerAvatar, location, Color.Red);
                spriteBatch.Draw(enemyArray[0], enemyLocation, Color.White);
                spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.White);
                spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);
                player1.TakeHit();
            }
            if (location.Intersects(enemyLocation1))
            {
                spriteBatch.Draw(playerAvatar, location, Color.Red);
                spriteBatch.Draw(enemyArray[0], enemyLocation, Color.White);
                spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.White);
                spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);
                player1.TakeHit();
            }
            if (location.Intersects(enemyLocation2))
            {
                spriteBatch.Draw(playerAvatar, location, Color.Red);
                spriteBatch.Draw(enemyArray[0], enemyLocation, Color.White);
                spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.White);
                spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);
                player1.TakeHit();
            }

            // draw collisions of weapon and enemy - REPLACE WHEN YOU CAN, THIS ONLY TESTS TO SEE IF THE WEAPON COLLIDES,
            // AND IF IT DOES IT DELETES THE ENEMY TEMPORARILY, MAKE SURE TO REPLACE!
            if(active == true)
            {
                if (weaponLocation.Intersects(enemyLocation))
                {
                    enemyArray[0] = Content.Load<Texture2D>("DeadSoldier");
                    pattern = 2;
                    spriteBatch.Draw(playerAvatar, location, Color.White);
                    spriteBatch.Draw(weaponSprite, weaponLocation, Color.White);
                    spriteBatch.Draw(enemyArray[0], enemyLocation, Color.Red);
                    spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.White);
                    spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);
                }
                if (weaponLocation.Intersects(enemyLocation1))
                {
                    enemyArray[1] = Content.Load<Texture2D>("DeadSoldier");
                    pattern2 = 2;
                    spriteBatch.Draw(playerAvatar, location, Color.White);
                    spriteBatch.Draw(weaponSprite, weaponLocation, Color.White);
                    spriteBatch.Draw(enemyArray[0], enemyLocation, Color.White);
                    spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.Red);
                    spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);
                }
                if (weaponLocation.Intersects(enemyLocation2))
                {
                    enemyArray[2] = Content.Load<Texture2D>("DeadSoldier");
                    pattern3 = 4;
                    spriteBatch.Draw(playerAvatar, location, Color.White);
                    spriteBatch.Draw(weaponSprite, weaponLocation, Color.White);
                    spriteBatch.Draw(enemyArray[0], enemyLocation, Color.White);
                    spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.White);
                    spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.Red);
                }
            }
            

            spriteBatch.Draw(enemyArray[0], enemyLocation, Color.White);
            spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.White);
            spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
