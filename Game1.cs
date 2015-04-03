namespace DelveCodeB
{
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
        Texture2D[] doors;
        // SpriteFont font;
        Texture2D gameOver;
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
        // private KeyboardState prevState; - will be used when we have animation
        Player player1;
        // Enemy enem;
        private int timer;
        Boolean timerActive;
        Texture2D room;
        Rectangle door1;
        MapReader mapFile;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            walls = new List<Texture2D>();
            doors = new Texture2D[12];
            enemyArray = new Texture2D[3];
            enemyLocation = new Rectangle(20, 20, 50, 50);
            enemyLocation1 = new Rectangle(400, 100, 50, 50);
            enemyLocation2 = new Rectangle(250, 250, 50, 50);
            location = new Rectangle(600, 300, 50, 50);
            weaponLocation = new Rectangle(location.X + 30, location.Y + 9, 25, 25);
            weaponLocation1 = new Rectangle(location.X + 30, location.Y + 9, 25, 25);
            player1 = new Player(location.X, location.Y, location.Width, location.Height, "Player");
            pattern = 1;
            pattern2 = 1;
            pattern3 = 1;
            timer = 0;
            timerActive = false;
            mapFile = new MapReader();
            graphics.PreferredBackBufferWidth = 1250;
            graphics.PreferredBackBufferHeight = 625;
            graphics.ApplyChanges();
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
            weaponSprite = Content.Load<Texture2D>("SwordUse");
            gameOver = Content.Load<Texture2D>("EndGame");
            room = Content.Load<Texture2D>("Room");
            // populare enemy array with enemies
            for (int i = 0; i < enemyArray.Length; i++)
            {
                enemyArray[i] = Content.Load<Texture2D>("Skeleton");
            }
            doors[0] = Content.Load<Texture2D>("DoorMidRightUse");


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

            if (timerActive == true)
            {
                timer++;
            }


            if (timer == 30)
            {
                timer = 0;
                timerActive = false;
            }

            // enemy movement with set path for x/y coordinates: if collision occurs between you and enemy, you take damage, if it occurs
            // between your weapon and the enemy, the enemy will die
            // REPLACE THIS CODE WITH PATHING WHEN READY

            if (pattern == 1 || pattern == 0 || pattern == 2)
            {
                if (pattern == 1)
                {
                    enemyArray[0] = Content.Load<Texture2D>("SkeletonWalkingRight");
                    enemyLocation.X += 1;
                    if (enemyLocation.X >= 900)
                    {
                        pattern--;
                    }
                }
                if (pattern == 0)
                {
                    enemyArray[0] = Content.Load<Texture2D>("SkeletonWalking");
                    enemyLocation.X -= 1;
                    if (enemyLocation.X <= 10)
                    {
                        pattern++;
                    }
                }
                if (pattern == 2)
                {
                    enemyLocation.X = 8000;
                    enemyLocation.Y = 8000;
                }

            }
            if (pattern2 == 1 || pattern2 == 0 || pattern2 == 2)
            {
                if (pattern2 == 1)
                {
                    //enemyArray[1] = Content.Load<Texture2D>("Skeleton");
                    enemyLocation1.Y += 1;
                    if (enemyLocation1.Y >= 500)
                    {
                        pattern2--;
                    }
                }
                if (pattern2 == 0)
                {
                    //enemyArray[1] = Content.Load<Texture2D>("Skeleton");
                    enemyLocation1.Y -= 1;
                    if (enemyLocation1.Y <= 3)
                    {
                        pattern2++;
                    }
                }
                if (pattern2 == 2)
                {
                    enemyLocation1.Y = 8000;
                    enemyLocation1.X = 8000;
                }

            }
            if (pattern3 == 1 || pattern3 == 0 || pattern3 == 2 || pattern3 == 3 || pattern3 == 4)
            {
                if (pattern3 == 1)
                {
                    enemyArray[2] = Content.Load<Texture2D>("SkeletonWalkingRight");
                    enemyLocation2.X += 1;
                    if (enemyLocation2.X >= 600)
                    {
                        pattern3++;
                    }
                }
                if (pattern3 == 0)
                {
                    enemyArray[2] = Content.Load<Texture2D>("SkeletonWalking");
                    enemyLocation2.X -= 1;
                    if (enemyLocation2.X <= 50)
                    {
                        pattern3 = pattern3 + 3;
                    }
                }
                if (pattern3 == 2)
                {
                    //enemyArray[2] = Content.Load<Texture2D>("SkeletonWalking");
                    enemyLocation2.Y -= 1;
                    if (enemyLocation2.Y <= 20)
                    {
                        pattern3 = pattern3 - 2;
                    }
                }
                if (pattern3 == 3)
                {
                    //enemyArray[2] = Content.Load<Texture2D>("SkeletonWalking");
                    enemyLocation2.Y += 1;
                    if (enemyLocation2.Y >= 500)
                    {
                        pattern3 = pattern3 - 2;
                    }
                }
                if (pattern3 == 4)
                {
                    enemyLocation2.X = 8000;
                    enemyLocation2.Y = 8000;
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
            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(room, new Rectangle(0, 0, 1250, 625), Color.White);

            // load maps and draw game here
            mapFile.readRoom(1);

            for (int i = 0; i < 25;i++ )
            {
                for(int j = 0; j < 25;j++)
                {
                   Rectangle door1 = new Rectangle(i * 50, j * 25, 50, 50);
                   if(mapFile.roomXY[i,j] == 'D')
                   {
                       spriteBatch.Draw(doors[0], door1 ,Color.White);
                      
                   }
                   if(mapFile.roomXY[i,j] == 'P')
                   {
                       spriteBatch.Draw(playerAvatar, location, Color.White);
                   }
                   if(mapFile.roomXY[i,j] == 'E')
                   {
                       // spriteBatch.Draw(enemyArray[0], new Rectangle(i*50, j*50, 50, 50), Color.White);
                   }
                }
            }

            //spriteBatch.Draw(playerAvatar, location, Color.White);
            spriteBatch.Draw(weaponSprite, weaponLocation, Color.White);
            
            if(weaponLocation.X <= 30)
            {
                location.X = 30;
                weaponLocation.X = 60;
            }
            if(location.X <= 30)
            {
                location.X = 30;
                weaponLocation.X = 60;   
            }

            if(location.Y <= 0)
            {
                weaponLocation.Y = 18;
                weaponLocation.X = location.X + 30;
                location.Y = 1;
            }
            if(weaponLocation.Y <= 0)
            {
                weaponLocation.Y = 18;
                weaponLocation.X = location.X + 30;
                location.Y = 1;
            }
            if(location.X >= graphics.PreferredBackBufferWidth - 80)
            {
                location.X = graphics.PreferredBackBufferWidth - 85;
                weaponLocation.X = location.X - 10;
            }
            if(location.Y >= graphics.PreferredBackBufferHeight - 85)
            {
                location.Y = graphics.PreferredBackBufferHeight - 90;
               // weaponLocation.Y = location.Y - 5;
            }
            if(weaponLocation.Y >= graphics.PreferredBackBufferHeight - 90)
            {
                weaponLocation.Y -= 5; 
            }
           

            kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.W) == true && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.S) && kstate.IsKeyUp(Keys.A) && player1.isKnockedBack == false)
            {
                location.Y = location.Y - 1;
                weaponLocation.Y = weaponLocation.Y - 1;
            }
            if (kstate.IsKeyDown(Keys.D) == true && kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.S) && kstate.IsKeyUp(Keys.A) && player1.isKnockedBack == false)
            {
                location.X = location.X + 1;
                weaponLocation.X = weaponLocation.X + 1;
            }
            if (kstate.IsKeyDown(Keys.S) == true && kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.A) && player1.isKnockedBack == false)
            {
                location.Y = location.Y + 1;
                weaponLocation.Y = weaponLocation.Y + 1;
            }
            if (kstate.IsKeyDown(Keys.A) == true && kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.S) && player1.isKnockedBack == false)
            {
                location.X = location.X - 1;
                weaponLocation.X = weaponLocation.X - 1;
            }
            if (kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Down) && timerActive == false && player1.isKnockedBack == false)
            {
                active = true;
                timerActive = true;
                weaponLocation.X = location.X + 12;
                weaponLocation.Y = location.Y - 30;
                spriteBatch.Draw(weaponSprite, weaponLocation, Color.Red);

            }
            if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Down) && timerActive == false && player1.isKnockedBack == false)
            {
                active = true;
                timerActive = true;
                weaponLocation.X = location.X + 30;
                weaponLocation.Y = location.Y + 9;
                spriteBatch.Draw(weaponSprite, weaponLocation, Color.Red);
            }
            if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Down) && timerActive == false && player1.isKnockedBack == false)
            {
                active = true;
                timerActive = true;
                weaponLocation.X = location.X - 10;
                weaponLocation.Y = location.Y + 9;
                spriteBatch.Draw(weaponSprite, weaponLocation, Color.Red);
            }
            if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left) && timerActive == false && player1.isKnockedBack == false)
            {
                active = true;
                timerActive = true;
                weaponLocation.X = location.X + 12;
                weaponLocation.Y = location.Y + 40;
                spriteBatch.Draw(weaponSprite, weaponLocation, Color.Red);
            }

            if (kstate.IsKeyDown(Keys.Down) == false && kstate.IsKeyDown(Keys.Left) == false && kstate.IsKeyDown(Keys.Right) == false && kstate.IsKeyDown(Keys.Up) == false)
            {
                active = false;
            }
            
            if (location.Intersects(enemyLocation))
            {
                switch(location.X)
                {
                    default:
                        if(location.X >= enemyLocation.X)
                        {
                            location.X += 100;
                            weaponLocation.X += 100;
                        }
                        else if(location.Y >= enemyLocation.Y)
                        {
                            location.Y += 50;
                            weaponLocation.Y += 50;
                        }
                        if(location.X <= enemyLocation.X)
                        {
                            location.X -= 100;
                            weaponLocation.X -= 100;
                        }
                        else if(location.Y <= enemyLocation.Y)
                        {
                            location.Y -= 50;
                            weaponLocation.Y -= 50;
                        }
                        break;
                }
                spriteBatch.Draw(enemyArray[0], enemyLocation, Color.White);
                spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.White);
                spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);
                player1.TakeHit();
                
            }
            if (location.Intersects(enemyLocation1))
            {
                switch (location.X)
                {
                    default:
                        if (location.X >= enemyLocation1.X)
                        {
                            location.X += 100;
                            weaponLocation.X += 100;
                        }
                        if (location.Y >= enemyLocation1.Y)
                        {
                            location.Y += 50;
                            weaponLocation.Y += 50;
                        }
                        if (location.X <= enemyLocation1.X)
                        {
                            location.X -= 100;
                            weaponLocation.X -= 100;
                        }
                        if (location.Y <= enemyLocation1.Y)
                        {
                            location.Y -= 50;
                            weaponLocation.Y -= 50;
                        }
                        break;
                }
                spriteBatch.Draw(enemyArray[0], enemyLocation, Color.White);
                spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.White);
                spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);
                player1.TakeHit();
            }
            if (location.Intersects(enemyLocation2))
            {
                switch (location.X)
                {
                    default:
                        if (location.X >= enemyLocation2.X)
                        {
                            location.X += 100;
                            weaponLocation.X += 100;
                        }
                        if (location.Y >= enemyLocation2.Y)
                        {
                            location.Y += 50;
                            weaponLocation.Y += 50;
                        }
                        if (location.X <= enemyLocation2.X)
                        {
                            location.X -= 100;
                            weaponLocation.X -= 100;
                        }
                        if (location.Y <= enemyLocation2.Y)
                        {
                            location.Y -= 50;
                            weaponLocation.Y -= 50;
                        }
                        break;
                }
                spriteBatch.Draw(enemyArray[0], enemyLocation, Color.White);
                spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.White);
                spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);
                player1.TakeHit();
            }

            // draw collisions of weapon and enemy - REPLACE WHEN YOU CAN, THIS ONLY TESTS TO SEE IF THE WEAPON COLLIDES,
            // AND IF IT DOES IT DELETES THE ENEMY TEMPORARILY, MAKE SURE TO REPLACE!
            if (active == true)
            {
                if (weaponLocation.Intersects(enemyLocation))
                {
                    pattern = 2;
                    spriteBatch.Draw(playerAvatar, location, Color.White);
                    spriteBatch.Draw(weaponSprite, weaponLocation, Color.White);
                    spriteBatch.Draw(enemyArray[0], enemyLocation, Color.Red);
                    spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.White);
                    spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);
                }
                if (weaponLocation.Intersects(enemyLocation1))
                {
                    pattern2 = 2;
                    spriteBatch.Draw(playerAvatar, location, Color.White);
                    spriteBatch.Draw(weaponSprite, weaponLocation, Color.White);
                    spriteBatch.Draw(enemyArray[0], enemyLocation, Color.White);
                    spriteBatch.Draw(enemyArray[1], enemyLocation1, Color.Red);
                    spriteBatch.Draw(enemyArray[2], enemyLocation2, Color.White);
                }
                if (weaponLocation.Intersects(enemyLocation2))
                {
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

            while(player1.IsAlive() == false)
            {
                spriteBatch.Draw(gameOver, new Rectangle(0, 0, 1250, 625), Color.White);
                break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
