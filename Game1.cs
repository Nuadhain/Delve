#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;
#endregion

namespace DelveGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Attributes
        SpriteFont font;
        SpriteFont otherFont;
        MouseState mState;
        MouseState prevmState;
        Rectangle bgRect;

        //Start Screen
        Texture2D title;
        Texture2D button;
        Texture2D startBut;
        Texture2D instrBut;
        Texture2D exitBut;
        Texture2D titleBack;

        //Pause Screen
        Texture2D pauseBack;

        //Instructions Screen
        Texture2D returnBut;
        Texture2D instrBack;

        //Start Screen
        Vector2 titlePos;
        Vector2 charInfoPos;
        Vector2 newButtonPos;
        Vector2 startButtonPos;
        Vector2 instrButPos;
        Vector2 exitButPos;

        //Instructions Screen
        Vector2 returnButPos;

        Vector2 taskInfoPos1;
        Vector2 taskInfoPos2;
        Vector2 taskInfoPos3;
        Vector2 taskInfoPos4;
        Vector2 taskInfoPos5;
        Vector2 taskInfoPos6;

        Vector2 howToInfoPos1;
        Vector2 howToInfoPos2;
        Vector2 howToInfoPos3;
        Vector2 howToInfoPos4;
        Vector2 howToInfoPos5;
        Vector2 howToInfoPos6;

        Vector2 howToPos;
        Vector2 taskPos;

        Random rand1, rand2;

        //Character info attributes
        string playerName;
        string playerClass;

        //Array of possible names
        string[] possNames = new string[15];

        //Enum for game states
        private enum ScreenState { StartScreen, InstrScreen, PauseScreen, GameScreen, GameOverScreen };
        ScreenState screenState = new ScreenState();

        //Attributes, Textures, Lists, Etc all for the game itself
        //List of enemies
        List<Enemy> enemyList = new List<Enemy>();
        //List of all walls
        List<Wall> wallList = new List<Wall>();
        //List of all doors
        List<Door> doorList = new List<Door>();
        // list of all weapons
        List<Weapon> weaponList = new List<Weapon>();

        //Random object
        Random rgen = new Random();

        //Textures
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
        Texture2D chestUse;
        Texture2D spearLeft;
        Texture2D spearRight;
        Texture2D spearUp;
        Texture2D spearDown;
        Texture2D axeUp;
        Texture2D axeLeft;
        Texture2D axeRight;
        Texture2D axeDown;

        //Rectangles
        Rectangle weaponRect;
        Rectangle weaponRect1;
        Rectangle playerRect;
        //Rectangle door1;

        //Booleans
        Boolean weaponActive;
        Boolean allEnemiesCleared = false;
        Boolean weaponTimerActive = false;
        Boolean swordActive;
        Boolean spearActive;
        Boolean axeActive;
        Boolean wepRemove;

        KeyboardState kstate = new KeyboardState();  //Private KeyboardState prevState; - will be used when we have animation

        Player player1;

        private int floorNum = 1;
        public int FloorNum
        {
            get { return floorNum; }
        }
        private int roomNum = 1;
        private int playerScore = 0;

        private Weapon weap;

        MapReader mapFile;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Initialize screen state to start screen
            screenState = ScreenState.StartScreen;

            //Random objects to use
            rand1 = new Random();
            rand2 = new Random();

            bgRect = new Rectangle(0, 0, 1250, 625);

            //Positions
            //Start
            titlePos = new Vector2(325, 0);
            charInfoPos = new Vector2(340, 225);
            newButtonPos = new Vector2(785, 210);
            startButtonPos = new Vector2(550, 300);
            instrButPos = new Vector2(550, 400);
            exitButPos = new Vector2(550, 500);

            //Instructions
            returnButPos = new Vector2(550, 550);
            taskPos = new Vector2(20, 20);
            howToPos = new Vector2(20, 300);

            taskInfoPos1 = new Vector2(140, 20);
            taskInfoPos2 = new Vector2(140, 55);
            taskInfoPos3 = new Vector2(140, 90);
            taskInfoPos4 = new Vector2(140, 125);
            taskInfoPos5 = new Vector2(140, 160);
            taskInfoPos6 = new Vector2(140, 195);

            howToInfoPos1 = new Vector2(147, 300);
            howToInfoPos2 = new Vector2(147, 345);
            howToInfoPos3 = new Vector2(147, 380);
            howToInfoPos4 = new Vector2(147, 425);
            howToInfoPos5 = new Vector2(147, 460);
            howToInfoPos6 = new Vector2(147, 505);

            //List of names
            possNames[0] = "William";
            possNames[1] = "Brutus";
            possNames[2] = "Malcolm";
            possNames[3] = "Osmon";
            possNames[4] = "Edward";
            possNames[5] = "Phillip";
            possNames[6] = "Robert";
            possNames[7] = "Kenneth";
            possNames[8] = "Rupert";
            possNames[9] = "David";
            possNames[10] = "Perceval";
            possNames[11] = "Lawrence";
            possNames[12] = "Arthur";
            possNames[13] = "Clive";
            possNames[14] = "Mortimer";

            //Randomly assign name
            int randIndex = rand1.Next(0, 15);
            playerName = possNames[randIndex];

            //Temp info for testing classes
            playerClass = "The Well Rounded";

            //Initializing Game Specific Features
            playerRect = new Rectangle(600, 300, 50, 50);
            weaponRect = new Rectangle(playerRect.X + 40, playerRect.Y + 9, 25, 25);
            weaponRect1 = new Rectangle(playerRect.X + 40, playerRect.Y + 9, 25, 25);
            player1 = new Player(playerRect.X, playerRect.Y, playerRect.Width, playerRect.Height, "Player");
            weap = new Weapon(weaponRect.X, weaponRect.Y, weaponRect.Width, weaponRect.Height, "sword");
            weap.WeapDirection = 1;
            weaponActive = true;
            swordActive = true;
            spearActive = false;
            axeActive = false;
            wepRemove = false;
            mapFile = new MapReader();

            //Load maps and draw game here
            mapFile.readRoom(1);

            //Loop through everything
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    //Draw door at correct location
                    Rectangle door1 = new Rectangle(i * 50, j * 25, 50, 50);
                    if (mapFile.roomChars[i, j] == 'D')
                    {
                        Door door2 = new Door(i * 50, j * 25, 50, 50);
                        door2.Rect = new Rectangle(i * 50, j * 25, 50, 50);
                        doorList.Add(door2);
                    }
                    if (mapFile.roomChars[i, j] == 'O')
                    {
                        //Add each new wall to the list of walls to draw and interact with.
                        Wall wall2 = new Wall(i * 50, j * 25, 50, 50);
                        wall2.Rect = new Rectangle(i * 50, j * 25, 50, 50);
                        wallList.Add(wall2);
                    }

                    if (mapFile.roomChars[i, j] == 'E')
                    {
                        //Add a new enemy to the list of enemies to draw and interact with.
                        Enemy enem = new Enemy(i * 50, j * 25, 50, 50, floorNum);
                        enem.Rect = new Rectangle(i * 50, j * 25, 50, 50);
                        enemyList.Add(enem);
                    }
                    if (mapFile.roomChars[i, j] == 'U')
                    {
                        //Exit door
                        Door exitDoor = new Door(i * 50, j * 25, 50, 50);
                        exitDoor.Rect = new Rectangle(i * 50, j * 25, 50, 50);
                        doorList.Add(exitDoor);
                    }
                }
            }

            //Resize form
            graphics.PreferredBackBufferWidth = 1250;
            graphics.PreferredBackBufferHeight = 625;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Make mouse visible
            this.IsMouseVisible = true;

            // TODO: Add your initialization logic here

            //Audio track???

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

            // TODO: use this.Content to load your game content here
            //Fonts
            font = Content.Load<SpriteFont>("Font1");
            otherFont = Content.Load<SpriteFont>("mainFont");

            //Images for start screen and instructions and pause screen
            title = Content.Load<Texture2D>("TitleImage");
            button = Content.Load<Texture2D>("NewNameButton");
            startBut = Content.Load<Texture2D>("StartButton");
            instrBut = Content.Load<Texture2D>("InstructionsButton");
            exitBut = Content.Load<Texture2D>("ExitButton");
            returnBut = Content.Load<Texture2D>("TitleButton");
            titleBack = Content.Load<Texture2D>("TitleBack");
            instrBack = Content.Load<Texture2D>("InstructionsBack");
            pauseBack = Content.Load<Texture2D>("PauseBack");

            //Game specific sprites etc
            PlayerAvatarTexture = Content.Load<Texture2D>("HeavyUse");
            swordUpSpriteTexture = Content.Load<Texture2D>("SwordUse");
            swordRightSpriteTexture = Content.Load<Texture2D>("SwordUseSide");
            swordLeftSpriteTexture = Content.Load<Texture2D>("SwordUseSideLeft");
            swordDownSpriteTexture = Content.Load<Texture2D>("SwordUseDown");
            gameOverTexture = Content.Load<Texture2D>("GameOverScreen");
            roomTexture = Content.Load<Texture2D>("Room");
            baseSkeletonTexture = Content.Load<Texture2D>("Skeleton");
            skeletonWalkingLeft = Content.Load<Texture2D>("SkeletonWalkUse");
            skeletonWalkingRight = Content.Load<Texture2D>("SkeletonWalkingRight");
            doorsTexture = Content.Load<Texture2D>("DoorUse");
            wallsTexture = Content.Load<Texture2D>("RockUse");
            chestUse = Content.Load<Texture2D>("ChestUse");
            axeDown = Content.Load<Texture2D>("AxeDownUse");
            axeLeft = Content.Load<Texture2D>("AxeHorozontalLeftUse");
            axeRight = Content.Load<Texture2D>("AxeHorozontalUse");
            axeUp = Content.Load<Texture2D>("AxeUse");
            spearDown = Content.Load<Texture2D>("SpearUseDown");
            spearUp = Content.Load<Texture2D>("SpearUse");
            spearLeft = Content.Load<Texture2D>("SpearHorozontalLeftUse");
            spearRight = Content.Load<Texture2D>("SpearHorozontalUse");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        ///<summary>
        /// These are the various update methods for the different game states
        /// they will each be called based on the enum of the state that the game should be in
        /// enums are changed in each update and called in Update(gametime) to actually update
        /// </summary>

        //Update the start screen
        public void UpdateStart()
        {
            //This is checking if they are pressing the new name button
            prevmState = mState;
            mState = Mouse.GetState();

            screenState = ScreenState.StartScreen;

            if (prevmState.LeftButton == ButtonState.Pressed) //If the button is pressed once and then released
            {
                if (mState.LeftButton == ButtonState.Released)
                {
                    if ((mState.X >= 785 && mState.X <= 910) && (mState.Y >= 210 && mState.Y <= 270)) //Check if it is within bounds of the button
                    {
                        int randIndex = rand1.Next(0, 15); //Randomly assign new name
                        playerName = possNames[randIndex]; //Assign it 
                    }
                }
            }

            //Check if clicking start button
            if (prevmState.LeftButton == ButtonState.Pressed) //If the button is pressed once and then released
            {
                if (mState.LeftButton == ButtonState.Released)
                {
                    if ((mState.X >= 550 && mState.X <= 700) && (mState.Y >= 300 && mState.Y <= 360)) //Check if it is within bounds of the button
                        screenState = ScreenState.GameScreen; //Change game state to start game - should begin running game
                }
            }

            //Check if clicking instructions button
            if (prevmState.LeftButton == ButtonState.Pressed) //If the button is pressed once and then released
            {
                if (mState.LeftButton == ButtonState.Released)
                {
                    if ((mState.X >= 550 && mState.X <= 700) && (mState.Y >= 400 && mState.Y <= 460)) //Check if it is within bounds of the button
                    {
                        //Change game state to instructions
                        screenState = ScreenState.InstrScreen;
                    }
                }
            }

            //Check if clicking exit button
            if (prevmState.LeftButton == ButtonState.Pressed) //If the button is pressed once and then released
            {
                if (mState.LeftButton == ButtonState.Released)
                {
                    if ((mState.X >= 550 && mState.X <= 700) && (mState.Y >= 500 && mState.Y <= 560)) //Check if it is within bounds of the button
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        //Update instructions screen
        public void UpdateInstr()
        {
            //This is checking if they are pressing the new name button
            prevmState = mState;
            mState = Mouse.GetState();

            if (prevmState.LeftButton == ButtonState.Pressed) //If the button is pressed once and then released
            {
                if (mState.LeftButton == ButtonState.Released)
                {
                    if ((mState.X >= 550 && mState.X <= 700) && (mState.Y >= 550 && mState.Y <= 610)) //Check if it is within bounds of the button
                    {
                        //Change game state to start
                        screenState = ScreenState.StartScreen;
                    }
                }
            }
        }

        //Update game screen
        public void UpdateGame()
        {
            //Change screen state
            screenState = ScreenState.GameScreen;

            //Check if all enemies are dead.
            if (allEnemiesCleared == false)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].Alive == false || enemyList[i].Rect.X > graphics.PreferredBackBufferWidth || enemyList[i].Rect.Y > graphics.PreferredBackBufferHeight || enemyList[i].Rect.X < 0 || enemyList[i].Rect.Y < 0)
                    {
                        enemyList.RemoveAt(i);
                        playerScore += (50 * floorNum);
                    }
                }
            }
            if (enemyList.Count == 0)
            {
                allEnemiesCleared = true;
            }

            for (int i = 0; i < weaponList.Count; i++)
            {
                if (wepRemove == true)
                {
                    weaponList.RemoveAt(i);
                    wepRemove = false;
                }
            }

            foreach (Door portal in doorList)
            {
                if (portal.Rect.Intersects(playerRect) && allEnemiesCleared == true)
                {
                    //Reset the lists, read in a new room, regain health equal to toughness/2.
                    //Go back to top

                    allEnemiesCleared = false;

                    playerRect = new Rectangle(600, 300, 50, 50);
                    weaponRect = new Rectangle(620, 300, 25, 25);
                    roomNum++;

                    if (roomNum == 1)
                    {
                        mapFile.readRoom(1);
                    }

                    if (roomNum > 1 && roomNum < 7)
                    {
                        mapFile.readRoom(rgen.Next(3, 8));
                    }

                    if (roomNum == 6)
                    {
                        mapFile.readRoom(2);
                    }

                    //Go into new floor
                    if (roomNum == 7)
                    {
                        floorNum++;
                        mapFile.readRoom(1);
                        roomNum = 1;
                        playerScore += (100 * floorNum);
                    }

                    doorList = new List<Door>();
                    wallList = new List<Wall>();
                    enemyList = new List<Enemy>();

                    //Loop through everything for door additions
                    for (int i = 0; i < 25; i++)
                    {
                        for (int j = 0; j < 25; j++)
                        {

                            if (mapFile.roomChars[i, j] == 'D')
                            {
                                Door door2 = new Door(i * 50, j * 25, 50, 50);
                                door2.Rect = new Rectangle(i * 50, j * 25, 50, 50);
                                doorList.Add(door2);
                            }
                            if (mapFile.roomChars[i, j] == 'O')
                            {
                                //Add each new wall to the list of walls to draw and interact with.
                                Wall wall2 = new Wall(i * 50, j * 25, 50, 50);
                                wall2.Rect = new Rectangle(i * 50, j * 25, 50, 50);
                                wallList.Add(wall2);
                            }

                            if (mapFile.roomChars[i, j] == 'E')
                            {
                                //Add a new enemy to the list of enemies to draw and interact with.
                                Enemy enem = new Enemy(i * 50, j * 25, 50, 50, floorNum);
                                enem.Rect = new Rectangle(i * 50, j * 25, 50, 50);
                                enemyList.Add(enem);
                            }
                            if (mapFile.roomChars[i, j] == 'U')
                            {
                                //Exit door
                                Door exitDoor = new Door(i * 50, j * 25, 50, 50);
                                exitDoor.Rect = new Rectangle(i * 50, j * 25, 50, 50);
                                doorList.Add(exitDoor);
                            }
                            if (mapFile.roomChars[i, j] == 'W')
                            {
                                Random rgen = new Random();
                                Random rgen1 = new Random();
                                int value = rgen.Next(0, 3);
                                switch (value)
                                {
                                    case 0: Weapon wep = new Weapon(i * 50, j * 25, 25, 25, "axe");
                                        wep.Rect = new Rectangle(i * 50, j * 25, 25, 25);
                                        wep.WeaponDamage = rgen1.Next(3, 6);
                                        weaponList.Add(wep);
                                        break;

                                    case 1: Weapon wep1 = new Weapon(i * 50, j * 25, 25, 25, "spear");
                                        wep1.Rect = new Rectangle(i * 50, j * 25, 25, 25);
                                        wep1.WeaponDamage = rgen1.Next(2, 5);
                                        weaponList.Add(wep1);
                                        break;

                                    case 2: Weapon wep2 = new Weapon(i * 50, j * 25, 25, 25, "sword");
                                        wep2.Rect = new Rectangle(i * 50, j * 25, 25, 25);
                                        wep2.WeaponDamage = rgen1.Next(2, 6);
                                        weaponList.Add(wep2);
                                        break;
                                }
                            }
                        }
                    }
                }
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
                weaponRect.X = playerRect.X + 12;
                weaponRect.Y = playerRect.Y - 30;
            }
            //Right is pressed
            else if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Down) && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponRect.X = playerRect.X + 40;
                weaponRect.Y = playerRect.Y + 9;
            }
            //Left is pressed
            else if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Down) && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponRect.X = playerRect.X - 10;
                weaponRect.Y = playerRect.Y + 9;
            }
            //Down is pressed
            else if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left) && player1.isKnockedBack == false)
            {
                weaponActive = true;
                weaponRect.X = playerRect.X + 12;
                weaponRect.Y = playerRect.Y + 40;
            }

            //Check if 'p' is pressed to change game state to pause screen
            else if (kstate.IsKeyDown(Keys.P) == true)
            {
                screenState = ScreenState.PauseScreen;
            }

             //No keys pressed
            else if (kstate.IsKeyDown(Keys.Down) == false && kstate.IsKeyDown(Keys.Left) == false && kstate.IsKeyDown(Keys.Right) == false && kstate.IsKeyDown(Keys.Up) == false)
            {
                weaponActive = false;
            }

            //Enemy movement with set path for x/y coordinates: if collision occurs between you and enemy, you take damage, if it occurs
            //Between your weapon and the enemy, the enemy will die

            //Enemy pathing
            foreach (Enemy foe in enemyList)
            {
                foe.Speed = (floorNum * (1 / 2)) + 1;
                //Create a variable that represents which axis the foe is blocked from moving in collisions. (X = 0, Y = 1)
                int axisStop = 0;
                //Create a boolean that tests if there was a collision
                bool hasCollided = false;

                //Check for collisions
                foreach (Wall obstruction in wallList)
                {
                    //Check if enemy is colliding with obstacle
                    if (obstruction.Rect.Intersects(foe.Rect))
                    {
                        //Set hasCollided to true
                        hasCollided = true;

                        //Send the enemy in the opposite direction
                        if (foe.Rect.X > obstruction.Rect.X)
                        {
                            foe.Rect = new Rectangle(foe.Rect.X + foe.Speed, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                            axisStop = 0;
                        }
                        if (foe.Rect.X < obstruction.Rect.X)
                        {
                            foe.Rect = new Rectangle(foe.Rect.X - foe.Speed, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                            axisStop = 0;
                        }
                        if (foe.Rect.Y > obstruction.Rect.Y)
                        {
                            foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y + foe.Speed, foe.Rect.Width, foe.Rect.Height);
                            axisStop = 1;
                        }
                        if (foe.Rect.Y < obstruction.Rect.Y)
                        {
                            foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y - foe.Speed, foe.Rect.Width, foe.Rect.Height);
                            axisStop = 1;
                        }
                    }
                }

                //For collisions
                if (hasCollided == true)
                {
                    //Test for aggro
                    if ((foe.Rect.X - playerRect.X) < 200 && (foe.Rect.X - playerRect.X) > -200 && (foe.Rect.Y - playerRect.Y) < 200 && (foe.Rect.Y - playerRect.Y) > -200)
                    {
                        if (axisStop == 0)
                        {
                            //Try to move around the obstacle towards the player
                            if (foe.Rect.Y < playerRect.Y)
                            {
                                foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y + foe.Speed, foe.Rect.Width, foe.Rect.Height);
                            }
                            if (foe.Rect.Y > playerRect.Y)
                            {
                                foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y - foe.Speed, foe.Rect.Width, foe.Rect.Height);
                            }
                        }
                        else
                        {
                            //Try to move around the obstacle towards the player
                            if (foe.Rect.X < playerRect.X)
                            {
                                foe.Rect = new Rectangle(foe.Rect.X + foe.Speed, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                            }
                            if (foe.Rect.X > playerRect.X)
                            {
                                foe.Rect = new Rectangle(foe.Rect.X - foe.Speed, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                            }
                        }
                    }
                }
                //If the enemy has not collided with something run the regular pathing
                else
                {
                    //Test for aggro
                    if ((foe.Rect.X - playerRect.X) < 200 && (foe.Rect.X - playerRect.X) > -200 && (foe.Rect.Y - playerRect.Y) < 200 && (foe.Rect.Y - playerRect.Y) > -200)
                    {
                        //Check for player's location relative to enemy and follow player
                        if (foe.Rect.X < playerRect.X)
                        {
                            foe.Rect = new Rectangle(foe.Rect.X + foe.Speed, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                        }
                        if (foe.Rect.X > playerRect.X)
                        {
                            foe.Rect = new Rectangle(foe.Rect.X - foe.Speed, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                        }
                        if (foe.Rect.Y < playerRect.Y)
                        {
                            foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y + foe.Speed, foe.Rect.Width, foe.Rect.Height);
                        }
                        if (foe.Rect.Y > playerRect.Y)
                        {
                            foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y - foe.Speed, foe.Rect.Width, foe.Rect.Height);
                        }
                    }
                }
            }

            //Make sure updated player direction based on what the last key you hit was goes here!

            //Make sure player can't go out of bounds
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

            //Checks intersection between enemies and players
            foreach (Enemy foe in enemyList)
            {
                foe.Strength = (floorNum * (1 / 2)) + 1;
                if (foe.Rect.Intersects(playerRect))
                {
                    switch (player1.Direction)
                    {
                        case 0:
                            {
                                playerRect = new Rectangle(playerRect.X, playerRect.Y + 50, playerRect.Width, playerRect.Height);
                                weaponRect = new Rectangle(weaponRect.X, weaponRect.Y + 50, weaponRect.Width, weaponRect.Height);
                                player1.Health = player1.Health - foe.Strength;
                                player1.TakeHit();
                                if(player1.Health == 0)
                                {
                                    screenState = ScreenState.GameOverScreen;
                                }
                                break;
                            }
                        case 1:
                            {
                                playerRect = new Rectangle(playerRect.X - 100, playerRect.Y, playerRect.Width, playerRect.Height);
                                weaponRect = new Rectangle(weaponRect.X - 100, weaponRect.Y, weaponRect.Width, weaponRect.Height);
                                player1.Health = player1.Health - foe.Strength;
                                player1.TakeHit();
                                if (player1.Health == 0)
                                {
                                    screenState = ScreenState.GameOverScreen;
                                }
                                break;
                            }
                        case 2:
                            {
                                playerRect = new Rectangle(playerRect.X, playerRect.Y - 50, playerRect.Width, playerRect.Height);
                                weaponRect = new Rectangle(weaponRect.X, weaponRect.Y - 50, weaponRect.Width, weaponRect.Height);
                                player1.Health = player1.Health - foe.Strength;
                                player1.TakeHit();
                                if (player1.Health == 0)
                                {
                                    screenState = ScreenState.GameOverScreen;
                                }
                                break;
                            }
                        case 3:
                            {
                                playerRect = new Rectangle(playerRect.X + 100, playerRect.Y, playerRect.Width, playerRect.Height);
                                weaponRect = new Rectangle(weaponRect.X + 100, weaponRect.Y, weaponRect.Width, weaponRect.Y);
                                player1.Health = player1.Health - foe.Strength;
                                player1.TakeHit();
                                if (player1.Health == 0)
                                {
                                    screenState = ScreenState.GameOverScreen;
                                }
                                break;
                            }
                    }
                }
            }

            //Weapon colliding with enemies while being swung
            foreach (Enemy foe in enemyList)
            {
                if(swordActive == true && weaponActive == true|| axeActive == true && weaponActive == true|| spearActive == true && weaponActive == true)
                    while (weaponRect.Intersects(foe.Rect))
                    {
                        switch (player1.Direction)
                        {
                            case 0:
                                {
                                    if (weap.CritSuccess == true)
                                    {
                                        foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y - 50, foe.Rect.Width, foe.Rect.Height);
                                        foe.TakeHit();
                                        weap.CritSuccess = false;
                                        weap.Criticals(); //Calls the method again to get a new value for the critical hit chance
                                        break;
                                    }
                                    else
                                    {
                                        foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y - 50, foe.Rect.Width, foe.Rect.Height);
                                        foe.TakeHit();
                                        weap.Criticals(); //Calls the method again to get a new value for the critical hit chance
                                        break;
                                    }
                                }
                            case 1:
                                {
                                    if (weap.CritSuccess == true)
                                    {
                                        foe.Rect = new Rectangle(foe.Rect.X + 100, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                                        foe.TakeHit();
                                        weap.CritSuccess = false;
                                        weap.Criticals(); //Calls the method again to get a new value for the critical hit chance
                                        break;
                                    }
                                    else
                                    {
                                        foe.Rect = new Rectangle(foe.Rect.X + 100, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                                        foe.TakeHit();
                                        weap.Criticals(); //Calls the method again to get a new value for the critical hit chance
                                        break;
                                    }
                                }
                            case 2:
                                {
                                    if (weap.CritSuccess == true)
                                    {
                                        foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y + 50, foe.Rect.Width, foe.Rect.Height);
                                        foe.TakeHit();
                                        weap.CritSuccess = false;
                                        weap.Criticals(); //Calls the method again to get a new value for the critical hit chance
                                        break;
                                    }
                                    else
                                    {
                                        foe.Rect = new Rectangle(foe.Rect.X, foe.Rect.Y + 50, foe.Rect.Width, foe.Rect.Height);
                                        foe.TakeHit();
                                        weap.Criticals(); //Calls the method again to get a new value for the critical hit chance
                                        break;
                                    }
                                }
                            case 3:
                                {
                                    if (weap.CritSuccess == true)
                                    {
                                        foe.Rect = new Rectangle(foe.Rect.X - 100, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                                        foe.TakeHit();
                                        weap.CritSuccess = false;
                                        weap.Criticals(); //Calls the method again to get a new value for the critical hit chance
                                        break;
                                    }
                                    else
                                    {
                                        foe.Rect = new Rectangle(foe.Rect.X - 100, foe.Rect.Y, foe.Rect.Width, foe.Rect.Height);
                                        foe.TakeHit();
                                        weap.Criticals(); //Calls the method again to get a new value for the critical hit chance
                                        break;
                                    }
                                }
                        }
                    }
                }
         
        

            //Check intersection between rocks and players
            foreach (Wall obstruction in wallList)
            {
                if (obstruction.Rect.Intersects(playerRect))
                {
                    switch (player1.Direction)
                    {
                        case 0: playerRect.Y = playerRect.Y + player1.Agility;
                            break;

                        case 1: playerRect.X = playerRect.X - player1.Agility;
                            break;

                        case 2: playerRect.Y = playerRect.Y - player1.Agility;
                            break;

                        case 3: playerRect.X = playerRect.X + player1.Agility;
                            break;
                    }
                }
            }
        }

        //Update pause screen
        public void UpdatePause()
        {
            screenState = ScreenState.PauseScreen;
            kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.O) == true)
                screenState = ScreenState.GameScreen;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) //Update the game based on which state it is currently in
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (screenState == ScreenState.StartScreen) //Start screen
                UpdateStart();

            if (screenState == ScreenState.InstrScreen) //Instructions screen
                UpdateInstr();

            if (screenState == ScreenState.GameScreen) //Game itself
                UpdateGame();

            if (screenState == ScreenState.PauseScreen) //Pause screen
                UpdatePause();

            base.Update(gameTime);
        }

        ///<summary>
        /// These are the various draw methods for the different game states
        /// they will each be called based on the enum of the state that the game should be in
        /// enums are changed in update, and in Draw the statedraw methods will draw the appropriate screen
        /// </summary>

        //Draw the start screen
        public void DrawStart()
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue); //Clear set background

            //Display things
            spriteBatch.Begin();

            spriteBatch.Draw(titleBack, bgRect, Color.White);

            spriteBatch.Draw(title, titlePos, Color.White); //Title
            spriteBatch.DrawString(font, "Name: " + playerName + "   Class: " + playerClass, charInfoPos, Color.White); //Character information
            spriteBatch.Draw(button, newButtonPos, Color.White); //New name button
            spriteBatch.Draw(startBut, startButtonPos, Color.White); //Start button
            spriteBatch.Draw(instrBut, instrButPos, Color.White); //Instructions button
            spriteBatch.Draw(exitBut, exitButPos, Color.White); //Exit button

            spriteBatch.End();
        }

        //Draw instructions screen
        public void DrawInstr()
        {
            //GraphicsDevice.Clear(Color.Coral); //Clear set background

            //Display things
            spriteBatch.Begin();

            spriteBatch.Draw(instrBack, bgRect, Color.White);

            spriteBatch.DrawString(font, "Your Task: ", taskPos, Color.White); //Your task bit
            //Self word wrapping to display chunk of text, each position is different so that it moves down
            spriteBatch.DrawString(font, "In the Kingdom of Ardez, there exists a place of great magical power known as The Crypt of the Grey.", taskInfoPos1, Color.LightGreen);
            spriteBatch.DrawString(font, "Every one hundred years, this crypt opens with the promise of immortality granted to any who make it ", taskInfoPos2, Color.LightGreen);
            spriteBatch.DrawString(font, "to the bottom alive. Stories of this crypt have been passed down families for generations, and there", taskInfoPos3, Color.LightGreen);
            spriteBatch.DrawString(font, "are always those who prepare their entire lives for the chance to be the first to return triumphant.", taskInfoPos4, Color.LightGreen);
            spriteBatch.DrawString(font, "For some, it is the promise of glory, for others it is the untold wealth laying quietly beside long dead", taskInfoPos5, Color.LightGreen);
            spriteBatch.DrawString(font, "adventurers. But why never really matters, for once the doors shut behind you, the only way out, is down.", taskInfoPos6, Color.LightGreen);

            spriteBatch.DrawString(font, "How to Play: ", howToPos, Color.White); //How to bit
            //Self word wrapping to display chunk of text, each position is different so that it moves down
            spriteBatch.DrawString(font, "Movement: WASD to move around the dungeon, your agility determines your speed.", howToInfoPos1, Color.LightGreen);
            spriteBatch.DrawString(font, "Attacking: Arrow keys to swing your weapon in the direction you want. Enemies will take damage", howToInfoPos2, Color.LightGreen);
            spriteBatch.DrawString(font, "when you hit them based on your strength and the weapon's stats.", howToInfoPos3, Color.LightGreen);
            spriteBatch.DrawString(font, "Proceeding: Go through openings in the walls to move between rooms.", howToInfoPos4, Color.LightGreen);
            spriteBatch.DrawString(font, "To get to the next floor find the ladder and descend further down.", howToInfoPos5, Color.LightGreen);
            spriteBatch.DrawString(font, "Scoring: You will recieve points for completing floors and for slaying enemies.", howToInfoPos6, Color.LightGreen);

            spriteBatch.Draw(returnBut, returnButPos, Color.White); //Return to start button

            spriteBatch.End();
        }

        //Draw game screen
        public void DrawGame()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(roomTexture, new Rectangle(0, 0, 1250, 625), Color.White);

            //Draw all walls
            foreach (Wall obstruction in wallList)
            {
                spriteBatch.Draw(wallsTexture, obstruction.Rect, Color.White);
            }

            //Draw the weapon
            if (swordActive == true)
            {
                if (weap.CritSuccess == true)
                {
                    if (kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(swordUpSpriteTexture, new Rectangle(playerRect.X + 12, playerRect.Y - 30, 25, 25), Color.Red);
                    }
                    else if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(swordRightSpriteTexture, new Rectangle(playerRect.X + 40, playerRect.Y + 9, 25, 25), Color.Red);
                    }
                    else if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(swordDownSpriteTexture, new Rectangle(playerRect.X + 12, playerRect.Y + 50, 25, 25), Color.Red);
                    }
                    else if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Right))
                    {
                        spriteBatch.Draw(swordLeftSpriteTexture, new Rectangle(playerRect.X - 10, playerRect.Y + 9, 25, 25), Color.Red);
                    }
                }
                if (weap.CritSuccess == false)
                {
                    if (kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(swordUpSpriteTexture, new Rectangle(playerRect.X + 12, playerRect.Y - 30, 25, 25), Color.White);
                    }
                    else if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(swordRightSpriteTexture, new Rectangle(playerRect.X + 40, playerRect.Y + 9, 25, 25), Color.White);
                    }
                    else if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(swordDownSpriteTexture, new Rectangle(playerRect.X + 12, playerRect.Y + 50, 25, 25), Color.White);
                    }
                    else if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Right))
                    {
                        spriteBatch.Draw(swordLeftSpriteTexture, new Rectangle(playerRect.X - 10, playerRect.Y + 9, 25, 25), Color.White);
                    }
                }
            }
            else if (spearActive == true)
            {
                if (weap.CritSuccess == true)
                {
                    if (kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(spearUp, new Rectangle(playerRect.X + 12, playerRect.Y - 30, 25, 25), Color.Red);
                    }
                    else if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(spearRight, new Rectangle(playerRect.X + 40, playerRect.Y + 9, 25, 25), Color.Red);
                    }
                    else if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(spearDown, new Rectangle(playerRect.X + 12, playerRect.Y + 50, 25, 25), Color.Red);
                    }
                    else if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Right))
                    {
                        spriteBatch.Draw(spearLeft, new Rectangle(playerRect.X - 10, playerRect.Y + 9, 25, 25), Color.Red);
                    }
                }
                if (weap.CritSuccess == false)
                {
                    if (kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(spearUp, new Rectangle(playerRect.X + 12, playerRect.Y - 30, 25, 25), Color.White);
                    }
                    else if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(spearRight, new Rectangle(playerRect.X + 40, playerRect.Y + 9, 25, 25), Color.White);
                    }
                    else if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(spearDown, new Rectangle(playerRect.X + 12, playerRect.Y + 50, 25, 25), Color.White);
                    }
                    else if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Right))
                    {
                        spriteBatch.Draw(spearLeft, new Rectangle(playerRect.X - 10, playerRect.Y + 9, 25, 25), Color.White);
                    }
                }
            }
            else if (axeActive == true)
            {
                if (weap.CritSuccess == true)
                {
                    if (kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(axeUp, new Rectangle(playerRect.X + 12, playerRect.Y - 30, 25, 25), Color.Red);
                    }
                    else if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(axeRight, new Rectangle(playerRect.X + 40, playerRect.Y + 9, 25, 25), Color.Red);
                    }
                    else if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(axeDown, new Rectangle(playerRect.X + 12, playerRect.Y + 50, 25, 25), Color.Red);
                    }
                    else if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Right))
                    {
                        spriteBatch.Draw(axeLeft, new Rectangle(playerRect.X - 10, playerRect.Y + 9, 25, 25), Color.Red);
                    }
                }
                if (weap.CritSuccess == false)
                {
                    if (kstate.IsKeyDown(Keys.Up) == true && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(axeUp, new Rectangle(playerRect.X + 12, playerRect.Y - 30, 25, 25), Color.White);
                    }
                    else if (kstate.IsKeyDown(Keys.Right) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(axeRight, new Rectangle(playerRect.X + 40, playerRect.Y + 9, 25, 25), Color.White);
                    }
                    else if (kstate.IsKeyDown(Keys.Down) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.Left))
                    {
                        spriteBatch.Draw(axeDown, new Rectangle(playerRect.X + 12, playerRect.Y + 50, 25, 25), Color.White);
                    }
                    else if (kstate.IsKeyDown(Keys.Left) == true && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.Right))
                    {
                        spriteBatch.Draw(axeLeft, new Rectangle(playerRect.X - 10, playerRect.Y + 9, 25, 25), Color.White);
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

            // draw weapons in weapon list
            foreach (Weapon w in weaponList)
            {
                if (w.WeaponName == "axe")
                {
                    spriteBatch.Draw(axeUp, w.Rect, Color.White);
                    if (playerRect.Intersects(w.Rect))
                    {
                        axeActive = true;
                        swordActive = false;
                        spearActive = false;
                        wepRemove = true;
                    }
                }
                else if (w.WeaponName == "sword")
                {
                    spriteBatch.Draw(swordUpSpriteTexture, w.Rect, Color.White);
                    if (playerRect.Intersects(w.Rect))
                    {
                        swordActive = true;
                        axeActive = false;
                        spearActive = false;
                        wepRemove = true;
                    }
                }
                else if (w.WeaponName == "spear")
                {
                    spriteBatch.Draw(spearUp, w.Rect, Color.White);
                    if (playerRect.Intersects(w.Rect))
                    {
                        spearActive = true;
                        swordActive = false;
                        axeActive = false;
                        wepRemove = true;
                    }
                }
            }
            //Draw the player 
            spriteBatch.Draw(PlayerAvatarTexture, playerRect, Color.White);

            //Draw the doors
            foreach (Door portal in doorList)
            {
                spriteBatch.Draw(doorsTexture, portal.Rect, Color.White);
            }

            mState = Mouse.GetState();

            spriteBatch.End();
        }

        //Draw pause screen
        public void DrawPause()
        {
            spriteBatch.Begin();
            //Draw background
            spriteBatch.Draw(pauseBack, bgRect, Color.White);

            spriteBatch.End();
        }

        public void GameOverScreen()
        {

            mState = Mouse.GetState();
            spriteBatch.Begin();
            //Draw background
            spriteBatch.Draw(gameOverTexture, new Rectangle(0, 0, 1250, 625), Color.White);
            if (mState.LeftButton == ButtonState.Pressed)
            {
                Environment.Exit(0);
            }
            spriteBatch.End();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) //Draw current state based on screenState
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //Check which game state and draw accordingly
            DrawStart();

            if (screenState == ScreenState.StartScreen) //Draw start screen
                DrawStart();

            if (screenState == ScreenState.InstrScreen) //Draw instructions screen
                DrawInstr();

            if (screenState == ScreenState.GameScreen) //Draw the game itself
                DrawGame();

            if (screenState == ScreenState.PauseScreen) //Draw the pause screen
                DrawPause();

            if (screenState == ScreenState.GameOverScreen) // Draw game over screen
                GameOverScreen();

            base.Draw(gameTime);
        }
    }
}
