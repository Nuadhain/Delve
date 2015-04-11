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


        Texture2D wallsTexture;
        Texture2D doorsTexture;
        Texture2D baseSkeletonTexture;
        Texture2D skeletonWalkingLeft;
        Texture2D skeletonWalkingRight;
        Texture2D weaponSpriteTexture;
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
        Boolean weaponTimerActive;       
        MapReader mapFile;




        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            playerRect = new Rectangle(600, 300, 50, 50);
            weaponRect = new Rectangle(playerRect.X + 30, playerRect.Y + 9, 25, 25);
            weaponRect1 = new Rectangle(playerRect.X + 30, playerRect.Y + 9, 25, 25);
            player1 = new Player(playerRect.X, playerRect.Y, playerRect.Width, playerRect.Height, "Player");
            weaponTimer = 0;
            weaponTimerActive = false;
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
            PlayerAvatarTexture = Content.Load<Texture2D>("Heavy");
            weaponSpriteTexture = Content.Load<Texture2D>("SwordUse");
            gameOverTexture = Content.Load<Texture2D>("EndGame");
            roomTexture = Content.Load<Texture2D>("Room");
            baseSkeletonTexture = Content.Load<Texture2D>("Skeleton");
            skeletonWalkingLeft = Content.Load<Texture2D>("SkeletonWalking");
            skeletonWalkingRight = Content.Load<Texture2D>("SkeletonWalkingRight");

            doorsTexture = Content.Load<Texture2D>("DoorUse");
            
            //TODO: replace this with the rock.
            wallsTexture = Content.Load<Texture2D>("Light");


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
            if(allEnemiesCleared ==true)
            {
                
            }




            //Timer for weapon swings
            if (weaponTimerActive == true)
            {
                weaponTimer++;
            }
            if (weaponTimer == 30)
            {
                weaponTimer = 0;
                weaponTimerActive = false;
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

            // load maps and draw game here
            mapFile.readRoom(1);


            //loop through everything
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {//Draw door at correct location
                    Rectangle door1 = new Rectangle(i * 50, j * 25, 50, 50);
                    if (mapFile.roomChars[i, j] == 'D')
                    {
                        spriteBatch.Draw(doorsTexture, door1, Color.White);

                    }
                    if(mapFile.roomChars[i,j] =='O')
                    {
                        //Add each new wall to the list of walls to draw and interact with.
                        wallList.Add(new Wall(i * 50, j * 25, 50, 50));
                    }

                    if (mapFile.roomChars[i, j] == 'P')
                    {
                        spriteBatch.Draw(PlayerAvatarTexture, playerRect, Color.White);
                    }
                    if (mapFile.roomChars[i, j] == 'E')
                    {
                        //Add a new enemy to the list of enemies to draw and interact with.
                        enemyList.Add(new Enemy(i * 50, j * 25, 50, 50, "enemy"));
                    }
                }
            }

            //spriteBatch.Draw(playerAvatar, location, Color.White);
            

            
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
                // weaponLocation.Y = location.Y - 5;
            }
            if (weaponRect.Y >= graphics.PreferredBackBufferHeight - 70)
            {
                weaponRect.Y -= 5;
            }


            //The movement with "W","A","S","D"
            kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.W) == true && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.S) && kstate.IsKeyUp(Keys.A) && player1.isKnockedBack == false)
            {
                playerRect.Y = playerRect.Y - 1;
                weaponRect.Y = weaponRect.Y - 1;
                player1.Direction = 0;
            }
            if (kstate.IsKeyDown(Keys.D) == true && kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.S) && kstate.IsKeyUp(Keys.A) && player1.isKnockedBack == false)
            {
                playerRect.X = playerRect.X + 1;
                weaponRect.X = weaponRect.X + 1;
                player1.Direction =1;
            }
            if (kstate.IsKeyDown(Keys.S) == true && kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.A) && player1.isKnockedBack == false)
            {
                playerRect.Y = playerRect.Y + 1;
                weaponRect.Y = weaponRect.Y + 1;
                player1.Direction =2;
            }
            if (kstate.IsKeyDown(Keys.A) == true && kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.S) && player1.isKnockedBack == false)
            {
                playerRect.X = playerRect.X - 1;
                weaponRect.X = weaponRect.X - 1;
                player1.Direction=3;
            }
            if (kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Down) && weaponTimerActive == false && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponTimerActive = true;
                weaponRect.X = playerRect.X + 12;
                weaponRect.Y = playerRect.Y - 30;
                spriteBatch.Draw(weaponSpriteTexture, weaponRect, Color.Red);

            }
            //Right is pressed
            if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Down) && weaponTimerActive == false && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponTimerActive = true;
                weaponRect.X = playerRect.X + 30;
                weaponRect.Y = playerRect.Y + 9;
                spriteBatch.Draw(weaponSpriteTexture, weaponRect, Color.Red);
            }
            //Left is pressed
            if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Down) && weaponTimerActive == false && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponTimerActive = true;
                weaponRect.X = playerRect.X - 10;
                weaponRect.Y = playerRect.Y + 9;
                spriteBatch.Draw(weaponSpriteTexture, weaponRect, Color.Red);
            }
            //Down is pressed
            if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left) && weaponTimerActive == false && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponTimerActive = true;
                weaponRect.X = playerRect.X + 12;
                weaponRect.Y = playerRect.Y + 40;
                spriteBatch.Draw(weaponSpriteTexture, weaponRect, Color.Red);
            }

            //No keys pressed
            if (kstate.IsKeyDown(Keys.Down) == false && kstate.IsKeyDown(Keys.Left) == false && kstate.IsKeyDown(Keys.Right) == false && kstate.IsKeyDown(Keys.Up) == false)
            {
                weaponActive = false;
            }

            //Checks intersection between player and enemies.
            
            foreach(Enemy foe in enemyList)
            {
                if(foe.IsColliding(player1))
                {
                    switch(player1.Direction)
                    {
                        case 0:
                            {

                                player1.Rect = new Rectangle(player1.Rect.X,player1.Rect.Y+50,player1.Rect.Width,player1.Rect.Height);
                                weaponRect = new Rectangle(weaponRect.X, weaponRect.Y + 50, weaponRect.Width, weaponRect.Height);
                                player1.TakeHit();
                                break;
                                
                            }
                        case 1:
                            {
                                player1.Rect = new Rectangle(player1.Rect.X - 100, player1.Rect.Y, player1.Rect.Width, player1.Rect.Height);
                                weaponRect = new Rectangle(weaponRect.X - 100, weaponRect.Y, weaponRect.Width, weaponRect.Height);
                                player1.TakeHit();
                                break;
                            }
                        case 2:
                            {
                                player1.Rect = new Rectangle(player1.Rect.X, player1.Rect.Y - 50, player1.Rect.Width, player1.Rect.Height);
                                weaponRect = new Rectangle(weaponRect.X, weaponRect.Y - 50, weaponRect.Width, weaponRect.Height);
                                player1.TakeHit();
                                break;
                            }
                        case 3:
                            {
                                player1.Rect = new Rectangle(player1.Rect.X + 100, player1.Rect.Y, player1.Rect.Width, player1.Rect.Height);
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
            foreach(Enemy foe in enemyList)
            {
                if(weaponActive)
                {
                    while(weaponRect.Intersects(foe.Rect))
                    {
                        switch(player1.Direction)
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
           foreach(Enemy foe in enemyList)
           {
               switch(foe.Direction)
               {
                       //0=up 1=right 2=down 3=left
                   case 0: spriteBatch.Draw(baseSkeletonTexture, foe.Rect, Color.White); break;
                   case 1: spriteBatch.Draw(skeletonWalkingRight, foe.Rect, Color.White); break;
                   case 2: spriteBatch.Draw(baseSkeletonTexture, foe.Rect, Color.White); break;
                   case 3: spriteBatch.Draw(skeletonWalkingLeft, foe.Rect, Color.White); break;
               }
               
           }
           //Draw the weapon.
           spriteBatch.Draw(weaponSpriteTexture, weaponRect, Color.White);

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
