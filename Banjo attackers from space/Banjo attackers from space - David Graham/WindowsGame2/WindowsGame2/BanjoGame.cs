using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BanjoGame : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<PlainBanjo> PlainList = new List<PlainBanjo>();
        List<HunterBanjo> HunterList = new List<HunterBanjo>();
        List<DeadlyStrummer> DeadlyList = new List<DeadlyStrummer>();
        static public AccordianClass Accordian;
        List<AccordianNote> AccordianFire = new List<AccordianNote>();

        //Gamestates
        enum GameState
        {
            Title,
            Playing,
            GameOver,
            Loaded,
        }
        GameState state = GameState.Title;

        //Text
        SpriteFont Font;
        string Message = "Hello";

        //Textures
        Texture2D PlainTexture;
        Texture2D HunterTexture;
        Texture2D DeadlyStrummerTexture;
        Texture2D AccordianNoteTexture;
        Texture2D BanjoNoteTexture;
        Texture2D AccordianTexture;
        Texture2D BackgroundTexture;

        KeyboardState lastKey;

        //Rectangles
        Rectangle BackgroundRectangle;

        //vectors
        Vector2 HighScoresV;

        //Interger Values

        //Score Values
        int PlainScore = 10;
        int HunterScore = 20;
        int DeadlyStrummerScore = 50;

        //Other Values
        int PlayerScore = 0;
        int[] HighScores = new int[11];
        int[] DummyArray = new int[11];
        bool l = false;


        public BanjoGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1000;
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

            //banjoList.Add(new PlainBanjo());

            HighScoresV = new Vector2(200, 190);


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

            //Texture ContentLoads           
            BackgroundTexture = Content.Load<Texture2D>("SpaceBackground");
            PlainTexture = Content.Load<Texture2D>("PlainBanjo");
            HunterTexture = Content.Load<Texture2D>("HunterBanjo");
            DeadlyStrummerTexture = Content.Load<Texture2D>("DeadlyStrummer");
            AccordianTexture = Content.Load<Texture2D>("Accordian");
            AccordianNoteTexture = Content.Load<Texture2D>("AccordianNote");
            BanjoNoteTexture = Content.Load<Texture2D>("BanjoNote");
            Font = Content.Load<SpriteFont>("SpriteFont");

            //
            Accordian = new AccordianClass(450, 535,
                Window.ClientBounds.Width / 12,
                Window.ClientBounds.Height / 12,
                AccordianTexture);

            Swarm();

            BackgroundRectangle = new Rectangle(
                0, 0, //Top left corner
                Window.ClientBounds.Width,
                Window.ClientBounds.Height); // Size of screen display

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Keyboard input
            KeyboardState Keystate = Keyboard.GetState();
            //Exit method
            if (Keystate.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            switch (state)
            {
                case GameState.Title:
                    if (Keystate.IsKeyDown(Keys.Enter) && lastKey.IsKeyUp(Keys.Enter))
                    {
                        state = GameState.Playing;
                    }
                    if (Keystate.IsKeyDown(Keys.L) && lastKey.IsKeyUp(Keys.L))
                    {
                        load("BanjoGameSave.txt");
                        state = GameState.Loaded;
                    }
                    break;
                case GameState.Playing:
                    if (Keystate.IsKeyDown(Keys.Enter) && lastKey.IsKeyUp(Keys.Enter))
                    {
                        Swarm();
                    }
                    if (Keystate.IsKeyDown(Keys.S))
                    {
                        save("BanjoGameSave.txt");
                    }
                    //Accordian Movement

                    if (Keystate.IsKeyDown(Keys.Right))
                    {
                        Accordian.AccordianMove("Right", graphics.PreferredBackBufferWidth);
                    }
                    if (Keystate.IsKeyDown(Keys.Left))
                    {
                        Accordian.AccordianMove("Left", graphics.PreferredBackBufferWidth);
                    }
                    //accordian shooting
                    if (Keystate.IsKeyDown(Keys.Space) && lastKey.IsKeyUp(Keys.Space))
                    {
                        AccordianFire.Add(new AccordianNote(Accordian.SpritePosition.X, Accordian.SpritePosition.Y - 50,
                            Window.ClientBounds.Width / 18, Window.ClientBounds.Height / 18, AccordianNoteTexture));
                    }
                    for (int i = 0; i < AccordianFire.Count; i++)
                    {
                        AccordianFire[i].Update(gameTime, graphics.PreferredBackBufferWidth);
                    }
                    //Banjo Spawning
                    for (int i = 0; i < PlainList.Count; i++)
                    {
                        PlainList[i].Update(gameTime, graphics.PreferredBackBufferWidth);
                    }
                    for (int i = 0; i < HunterList.Count; i++)
                    {
                        HunterList[i].Update(gameTime, graphics.PreferredBackBufferWidth);
                    }
                    for (int i = 0; i < DeadlyList.Count; i++)
                    {
                        DeadlyList[i].Update(gameTime, graphics.PreferredBackBufferWidth);
                    }
                    //Accordian fire
                    foreach (AccordianNote Note in AccordianFire) foreach (PlainBanjo s in PlainList)
                        {
                            if (Note.SpriteLife > 0 && s.SpriteLife > 0)
                            {
                                if (Note.SpritePosition.Intersects(s.SpritePosition))
                                {
                                    PlayerScore = PlayerScore + PlainScore;
                                    Note.SpriteLife = 0;
                                    //remove bullet
                                    s.SpriteLife = s.SpriteLife - 1;
                                }
                            }
                        }
                    foreach (AccordianNote Note in AccordianFire) foreach (HunterBanjo s in HunterList)
                        {
                            if (Note.SpriteLife > 0 && s.SpriteLife > 0)
                            {
                                if (Note.SpritePosition.Intersects(s.SpritePosition))
                                {
                                    PlayerScore = PlayerScore + HunterScore;
                                    Note.SpriteLife = 0;
                                    //remove bullet
                                    s.SpriteLife = s.SpriteLife - 1;
                                }
                            }
                        }
                    foreach (AccordianNote Note in AccordianFire) foreach (DeadlyStrummer s in DeadlyList)
                        {
                            if (Note.SpriteLife > 0 && s.SpriteLife > 0)
                            {
                                if (Note.SpritePosition.Intersects(s.SpritePosition))
                                {
                                    Note.SpriteLife = 0;
                                    //remove bullet
                                    s.SpriteLife = s.SpriteLife - 1;
                                    if (s.SpriteLife == 0)
                                    {
                                        PlayerScore = PlayerScore + DeadlyStrummerScore;
                                    }
                                }
                            }
                        }
                    foreach (PlainBanjo s in PlainList)
                        if (Accordian.SpriteLife > 0 && s.SpriteLife > 0)
                        {
                            if (Accordian.SpritePosition.Intersects(s.SpritePosition) || s.SpritePosition.Y > graphics.PreferredBackBufferHeight)
                            {
                                s.SpriteLife = 0;
                                Accordian.SpriteLife = Accordian.SpriteLife - 1;
                            }
                        }
                    foreach (HunterBanjo s in HunterList)
                        if (Accordian.SpriteLife > 0 && s.SpriteLife > 0)
                        {
                            if (Accordian.SpritePosition.Intersects(s.SpritePosition) || s.SpritePosition.Y > graphics.PreferredBackBufferHeight)
                            {
                                s.SpriteLife = 0;
                                Accordian.SpriteLife = Accordian.SpriteLife - 1;
                            }
                        }
                    foreach (DeadlyStrummer s in DeadlyList)
                        if (Accordian.SpriteLife > 0 && s.SpriteLife > 0)
                        {
                            if (Accordian.SpritePosition.Intersects(s.SpritePosition) || s.SpritePosition.Y > graphics.PreferredBackBufferHeight)
                            {
                                s.SpriteLife = 0;
                                Accordian.SpriteLife = Accordian.SpriteLife - 1;
                            }
                        }
                    if (Accordian.SpriteLife <= 0)
                    {
                        state = GameState.GameOver;
                    }
                    Message = "Lives:" + Accordian.SpriteLife + " Score:" + PlayerScore;
                    break;
                case GameState.Loaded:
                    if (Keystate.IsKeyDown(Keys.Enter) && lastKey.IsKeyUp(Keys.Enter))
                    {
                        Swarm();
                    }
                    if (Keystate.IsKeyDown(Keys.S))
                    {
                        save("BanjoGameSave.txt");
                    }
                    //Accordian Movement

                    if (Keystate.IsKeyDown(Keys.Right))
                    {
                        Accordian.AccordianMove("Right", graphics.PreferredBackBufferWidth);
                    }
                    if (Keystate.IsKeyDown(Keys.Left))
                    {
                        Accordian.AccordianMove("Left", graphics.PreferredBackBufferWidth);
                    }
                    //accordian shooting
                    if (Keystate.IsKeyDown(Keys.Space) && lastKey.IsKeyUp(Keys.Space))
                    {
                        AccordianFire.Add(new AccordianNote(Accordian.SpritePosition.X, Accordian.SpritePosition.Y - 50,
                            Window.ClientBounds.Width / 18, Window.ClientBounds.Height / 18, AccordianNoteTexture));
                    }
                    for (int i = 0; i < AccordianFire.Count; i++)
                    {
                        AccordianFire[i].Update(gameTime, graphics.PreferredBackBufferWidth);
                    }
                    //Banjo Movement
                    for (int i = 0; i < PlainList.Count; i++)
                    {
                        PlainList[i].Update(gameTime, graphics.PreferredBackBufferWidth);
                    }
                    for (int i = 0; i < HunterList.Count; i++)
                    {
                        HunterList[i].Update(gameTime, graphics.PreferredBackBufferWidth);
                    }
                    for (int i = 0; i < DeadlyList.Count; i++)
                    {
                        DeadlyList[i].Update(gameTime, graphics.PreferredBackBufferWidth);
                    }
                    //Accordian fire
                    foreach (AccordianNote Note in AccordianFire) foreach (PlainBanjo s in PlainList)
                        {
                            if (Note.SpriteLife > 0 && s.SpriteLife > 0)
                            {
                                if (Note.SpritePosition.Intersects(s.SpritePosition))
                                {
                                    PlayerScore = PlayerScore + PlainScore;
                                    Note.SpriteLife = 0;
                                    //remove bullet
                                    s.SpriteLife = s.SpriteLife - 1;
                                }
                            }
                        }
                    foreach (AccordianNote Note in AccordianFire) foreach (HunterBanjo s in HunterList)
                        {
                            if (Note.SpriteLife > 0 && s.SpriteLife > 0)
                            {
                                if (Note.SpritePosition.Intersects(s.SpritePosition))
                                {
                                    PlayerScore = PlayerScore + HunterScore;
                                    Note.SpriteLife = 0;
                                    //remove bullet
                                    s.SpriteLife = s.SpriteLife - 1;
                                }
                            }
                        }
                    foreach (AccordianNote Note in AccordianFire) foreach (DeadlyStrummer s in DeadlyList)
                        {
                            if (Note.SpriteLife > 0 && s.SpriteLife > 0)
                            {
                                if (Note.SpritePosition.Intersects(s.SpritePosition))
                                {
                                    Note.SpriteLife = 0;
                                    //remove bullet
                                    s.SpriteLife = s.SpriteLife - 1;
                                    if (s.SpriteLife == 0)
                                    {
                                        PlayerScore = PlayerScore + DeadlyStrummerScore;
                                    }
                                }
                            }
                        }
                    foreach (PlainBanjo s in PlainList)
                        if (Accordian.SpriteLife > 0 && s.SpriteLife > 0)
                        {
                            if (Accordian.SpritePosition.Intersects(s.SpritePosition) || s.SpritePosition.Y > graphics.PreferredBackBufferHeight)
                            {
                                s.SpriteLife = 0;
                                Accordian.SpriteLife = Accordian.SpriteLife - 1;
                            }
                        }
                    foreach (HunterBanjo s in HunterList)
                        if (Accordian.SpriteLife > 0 && s.SpriteLife > 0)
                        {
                            if (Accordian.SpritePosition.Intersects(s.SpritePosition) || s.SpritePosition.Y > graphics.PreferredBackBufferHeight)
                            {
                                s.SpriteLife = 0;
                                Accordian.SpriteLife = Accordian.SpriteLife - 1;
                            }
                        }
                    foreach (DeadlyStrummer s in DeadlyList)
                        if (Accordian.SpriteLife > 0 && s.SpriteLife > 0)
                        {
                            if (Accordian.SpritePosition.Intersects(s.SpritePosition) || s.SpritePosition.Y > graphics.PreferredBackBufferHeight)
                            {
                                s.SpriteLife = 0;
                                Accordian.SpriteLife = Accordian.SpriteLife - 1;
                            }
                        }
                    if (Accordian.SpriteLife <= 0)
                    {
                        state = GameState.GameOver;
                    }
                    Message = "Lives:" + Accordian.SpriteLife + " Score:" + PlayerScore;
                    break;
                case GameState.GameOver:
                    Message = "Score:" + PlayerScore;
                    if (l == false)
                    {
                        LoadHighScore("BanjoGameScores");
                        l = true;
                        HighScoreSort(); // Updates Highscores
                    }
                    if (Keystate.IsKeyDown(Keys.Enter) && lastKey.IsKeyUp(Keys.Enter))
                    {
                        state = GameState.Title;
                        Reset();
                        l = false;
                    }
                    break;
            }
            lastKey = Keystate;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, Color.White);
            switch (state)
            {
                case GameState.Title:
                    spriteBatch.DrawString(Font, "Welcome to Banjo Space Invaders!!", new Vector2(400, 100), Color.Yellow);
                    spriteBatch.DrawString(Font, "Press the Enter key to begin", new Vector2(400, 130), Color.White);
                    spriteBatch.DrawString(Font, "Press the L key to load your last saved game", new Vector2(400, 160), Color.White);
                    spriteBatch.DrawString(Font, "Use the left and right keys to move", new Vector2(400, 190), Color.White);
                    spriteBatch.DrawString(Font, "Press the spacebar to shoot", new Vector2(400, 220), Color.White);
                    spriteBatch.DrawString(Font, "Press the S key to save your game during play", new Vector2(400, 250), Color.White);
                    break;
                case GameState.Playing:
                    spriteBatch.DrawString(Font, Message, new Vector2(10, 560), Color.White); //Lives and Score
                    Accordian.Draw(spriteBatch); //Draw Accordian
                    for (int i = 0; i < AccordianFire.Count; i++)
                    {
                        AccordianFire[i].Draw(spriteBatch); //Accordian notes
                    }
                    for (int i = 0; i < PlainList.Count; i++)
                    {
                        PlainList[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < HunterList.Count; i++)
                    {
                        HunterList[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < DeadlyList.Count; i++)
                    {
                        DeadlyList[i].Draw(spriteBatch);
                    }
                    break;
                case GameState.Loaded:
                    spriteBatch.DrawString(Font, Message, new Vector2(10, 560), Color.White); //Lives and Score
                    Accordian.Draw(spriteBatch); //Draw Accordian
                    for (int i = 0; i < AccordianFire.Count; i++)
                    {
                        AccordianFire[i].Draw(spriteBatch); //Accordian notes
                    }
                    for (int i = 0; i < PlainList.Count; i++)
                    {
                        PlainList[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < HunterList.Count; i++)
                    {
                        HunterList[i].Draw(spriteBatch);
                    }
                    for (int i = 0; i < DeadlyList.Count; i++)
                    {
                        DeadlyList[i].Draw(spriteBatch);
                    }
                    break;
                case GameState.GameOver:
                    int j = 0;
                    spriteBatch.DrawString(Font, "Game Over", new Vector2(200, 100), Color.Red);
                    spriteBatch.DrawString(Font, "Your Score:" + PlayerScore.ToString(), new Vector2(200, 140), Color.White);
                    spriteBatch.DrawString(Font, "High Scores", new Vector2(200, 170), Color.White);
                    for (int i = 0; i < HighScores.Length - 1; i++)
                    {
                        spriteBatch.DrawString(Font, HighScores[i].ToString(), new Vector2(HighScoresV.X, (HighScoresV.Y + j)), Color.White);
                        j += 20;
                    }
                    spriteBatch.DrawString(Font, "Press Enter key to return to Title Screen", new Vector2(200, 550), Color.White);

                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Reset()
        {
            PlainList = new List<PlainBanjo>();
            HunterList = new List<HunterBanjo>();
            DeadlyList = new List<DeadlyStrummer>();
            Accordian.SpriteLife = 3;
            PlayerScore = 0;
        }

        public void HighScoreSort()
        {
            HighScores[10] = PlayerScore;
            Array.Sort<int>(HighScores);
            Array.Reverse(HighScores);

            /* for (int j = 0; j < DummyArray.Length; j++)
            {
                DummyArray[j] = HighScores[j];

            }
            DummyArray[10] = PlayerScore;
            for (int i = DummyArray.Length - 1; i > 0; i--)
            {
                if (DummyArray[i] < DummyArray[i - 1])
                {
                    DummyArray[i] = DummyArray[i - 1];
                }
            }
            for (int m = 0; m < HighScores.Length; m++)
            {
                HighScores[m] = DummyArray[m];
            } */


            /*for (int l = 0;  < DummyArray.Length; l++)
            {
                if (PlayerScore > DummyArray[i])
                {
                    DummyArray[l] = HighScores[l];
                    HighScores[l] = PlayerScore;
                    DummyArray[l] = PlayerScore;
                }
            }*/
            HighScoreSave("BanjoGameScores");
        }

        public void Swarm()
        {
            Random Xvalue = new Random();
            for (int i = 0; i < 30; i++)
            {
                int X = Xvalue.Next(10, 900);
                PlainList.Add(new PlainBanjo(X, 10,
                    Window.ClientBounds.Width / 22,
                    Window.ClientBounds.Height / 8,
                    PlainTexture, 1, false));
            }

            for (int i = 0; i < 10; i++)
            {
                int X = Xvalue.Next(150, 900);
                HunterBanjo Banjo = new HunterBanjo(X, 10,
                Window.ClientBounds.Width / 22,
                Window.ClientBounds.Height / 8,
                HunterTexture, 1, false, 0);
                HunterList.Add(Banjo);
            }
            for (int i = 0; i < 3; i++)
            {
                int X = Xvalue.Next(10, 900);
                DeadlyList.Add(new DeadlyStrummer(X, 10,
                Window.ClientBounds.Width / 22,
                Window.ClientBounds.Height / 8,
                DeadlyStrummerTexture, 3, false));
            }
        }
        private void save(string filename)
        {
            System.IO.TextWriter textout = new System.IO.StreamWriter(filename);
            textout.WriteLine(PlayerScore);
            textout.WriteLine(PlainList.Count);
            textout.WriteLine(HunterList.Count);
            textout.WriteLine(DeadlyList.Count);
            textout.WriteLine(AccordianFire.Count);
            foreach (PlainBanjo i in PlainList)
            {
                i.save(textout);
            }
            foreach (HunterBanjo i in HunterList)
            {
                i.save(textout);
            }
            foreach (DeadlyStrummer i in DeadlyList)
            {
                i.save(textout);
            }

            foreach (AccordianNote i in AccordianFire)
            {
                i.save(textout);
            }
            Accordian.save(textout);
            textout.Close();
        }
        private void load(string filename)
        {
            System.IO.TextReader textIn = new System.IO.StreamReader (filename);
            PlayerScore = int.Parse(textIn.ReadLine());
            int plainListCount = int.Parse(textIn.ReadLine());
            int hunterListCount = int.Parse(textIn.ReadLine());
            int DeadlyListCount = int.Parse(textIn.ReadLine());
            int AccordianNoteCount = int.Parse(textIn.ReadLine());

            for (int i = 0; i < plainListCount; i++)
            {
                int SpriteLife = int.Parse(textIn.ReadLine());
                int XLocation = int.Parse(textIn.ReadLine());
                int YLocation = int.Parse(textIn.ReadLine());
                string directionCheck = textIn.ReadLine();
                bool DirectionChange = false;
                if (directionCheck == "true")
                {
                    DirectionChange = true;
                }
                if (directionCheck == "false")
                {
                    DirectionChange = false;
                }
                int YPosition = int.Parse(textIn.ReadLine());
                PlainBanjo banjo = new PlainBanjo(XLocation, YLocation, Window.ClientBounds.Width / 22,
                    Window.ClientBounds.Height / 8, PlainTexture, SpriteLife, DirectionChange);
                PlainList.Add(banjo);
            }
            for (int i = 0; i < hunterListCount; i++)
            {
                int SpriteLife = int.Parse(textIn.ReadLine());
                int XLocation = int.Parse(textIn.ReadLine());
                int YLocation = int.Parse(textIn.ReadLine());
                string directionCheck = textIn.ReadLine();
                bool DirectionChange = false;
                if (directionCheck == "true")
                {
                    DirectionChange = true;
                }
                if (directionCheck == "false")
                {
                    DirectionChange = false;
                }
                int YPosition = int.Parse(textIn.ReadLine());
                string temp = textIn.ReadLine();
                double time = double.Parse(temp);
                HunterBanjo banjo = new HunterBanjo(XLocation, YLocation, Window.ClientBounds.Width / 22,
                    Window.ClientBounds.Height / 8, HunterTexture, SpriteLife, DirectionChange, time);
                HunterList.Add(banjo);
            }
            for (int i = 0; i < DeadlyListCount; i++)
            {
                int SpriteLife = int.Parse(textIn.ReadLine());
                int XLocation = int.Parse(textIn.ReadLine());
                int YLocation = int.Parse(textIn.ReadLine());
                string directionCheck = textIn.ReadLine();
                bool DirectionChange = false;
                if (directionCheck == "true")
                {
                    DirectionChange = true;
                }
                if (directionCheck == "false")
                {
                    DirectionChange = false;
                }
                int YPosition = int.Parse(textIn.ReadLine());
                DeadlyStrummer banjo = new DeadlyStrummer(XLocation, YLocation, Window.ClientBounds.Width / 22,
                    Window.ClientBounds.Height / 8, DeadlyStrummerTexture, SpriteLife, DirectionChange);
                DeadlyList.Add(banjo);
            }
            for (int i = 0; i < AccordianNoteCount; i++)
            {
                int SpriteLife = int.Parse(textIn.ReadLine());
                int XLocation = int.Parse(textIn.ReadLine());
                int YLocation = int.Parse(textIn.ReadLine());
            }
            Accordian.load(textIn);
            textIn.Close();
        }
        private void HighScoreSave(string filename)
        {
            System.IO.TextWriter textout = new System.IO.StreamWriter(filename);
            for (int i = 0; i < HighScores.Length; i++)
            {
                textout.WriteLine(HighScores[i]);
            }
            textout.Close();
        }
        private void LoadHighScore(string filename)
        {
            System.IO.TextReader textIn = null;
            try
            {
                textIn = new System.IO.StreamReader(filename);
                for (int i = 0; i < HighScores.Length; i++)
                {
                    HighScores[i] = int.Parse(textIn.ReadLine());
                }
            }
            catch
            {
                for (int i = 0; i < HighScores.Length; i++)
                {
                    HighScores[i] = 0;
                }
            }
            finally
            {
                if (textIn != null)
                {
                    textIn.Close();
                }
            }
        }
    }
}
