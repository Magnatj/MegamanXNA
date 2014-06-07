using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MegamanXNALibrary;

namespace MegamanXNAGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MegamanGame : Microsoft.Xna.Framework.Game, ISubject
    {
        #region Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        private List<IObserver> observers = new List<IObserver>();
        private Achievement protoAchievement = new MegamanXNALibrary.Achievement();

        Texture2D Electivo;
        Rectangle elige;

        Color color;

        Protoman mProtoSprite;
        Megaman mMegaSprite;
        private bool protomanDefeated = false;

        private LifeBars megaBar;
        private LifeBars protoBar;

        private Obstacle obstacle;
        private Texture2D piso;
        private Rectangle pisoFrame;

        private GameScreen introScreen;
        private GameScreen winScreen;
        private GameScreen loseScreen;
        private GameScreen menuScreen;
        private GameScreen creditScreen;
        private GameScreen protoScreen;

        private const int ENEMY_LIFEBAR_X = 760;
        private const int ENEMY_LIFEBAR_Y = 40;

        private static Vector2 enemyStartPosition = new Vector2(500, 428);

        private MegamanDecorator specialMegaman;
        private MegamanFactory factory = new MegamanFactory();

        private RobotMasters robotMaster;
        private RobotMastersFactory enemyFactory = new RobotMastersFactory();

        GameStates gameState = GameStates.IntroScreen;
        #endregion
        public MegamanGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            mMegaSprite = new Megaman();
            //mProtoSprite = new Protoman(this, Content.Load<Texture2D>("ProtoSolo"), new Vector2(500, 428), 22, 24);
            robotMaster = enemyFactory.CreateRobotMaster("Protoman", this, Content.Load<Texture2D>("ProtoSolo"),
                enemyStartPosition, 22, 24);
            font = Content.Load<SpriteFont>("GameFont");
            Register(protoAchievement);
            //mMegaSprite = new Megaman();

            color = new Color(255, 255, 255);

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
            #region Load
            SongPlaylist.AddSong(this, "Intro Song");
            SongPlaylist.AddSong(this, "Go home and be a family man");

            Rectangle fullScreenRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            mMegaSprite.LoadContent(this.Content);

            introScreen = new GameScreen(this, "PrototipoFinal", fullScreenRectangle);
            creditScreen = new GameScreen(this, "InstructionScreen", fullScreenRectangle);
            menuScreen = new GameScreen(this, "Final Menu", fullScreenRectangle);
            loseScreen = new GameScreen(this, "You suck!!!", fullScreenRectangle);
            winScreen = new GameScreen(this, "You win!!!", fullScreenRectangle);
            protoScreen = new GameScreen(this, "FONDO COMPLETO PROTOMAN", fullScreenRectangle);

            Electivo = Content.Load<Texture2D>("Para elegir");
            elige = new Rectangle(35, 40, 200, 100);

            megaBar = new LifeBars(this, "Barra de vida a usar con X llena", 30, 40);
            protoBar = new LifeBars(this, "Barra de vida Protoman", 760, 40);

            piso = Content.Load<Texture2D>("Terreno completo para el nivel");
            pisoFrame = new Rectangle(0, 440, (int)(piso.Width), (int)(piso.Height));

            obstacle = new Obstacle(this, "Piedra sprite", 200, 408);
            #endregion
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #region Collition
        void DetectBulletCollition(GameTime gameTime)
        {
            if (mMegaSprite.Activo)
            {
                for (int i = 0; i < mMegaSprite.mBullets.Count; i++)
                {
                    if (mMegaSprite.mBullets[i].HitBox.Intersects(robotMaster.HitBox) &&
                        mMegaSprite.mBullets[i].Visible == true)
                    {
                        protoBar.BlackRect.Height += 2;
                        if (protoBar.BlackRect.Height >= 68)
                        {
                            if(robotMaster is Protoman)
                                Notificate("ProtomanDefeated");
                            else if(robotMaster is Gutsman)
                                Notificate("GutsmanDefeated");
                            else if(robotMaster is Fireman)
                                Notificate("FiremanDefeated");
                            else if(robotMaster is Iceman)
                                Notificate("IcemanDefeated");
                            protoBar.BlackRect.Height = 0;
                            gameState = GameStates.WinScreen;
                        }
                        break;
                    }
                }
            }
        }

        void DetectSpecialBulletCollition(GameTime gameTime)
        {
            for (int i = 0; i < specialMegaman.mBullets.Count; i++)
            {
                if (specialMegaman.mBullets[i].HitBox.Intersects(robotMaster.HitBox) &&
                    specialMegaman.mBullets[i].Visible == true && specialMegaman is RockMegamanDecorator)
                {
                    protoBar.BlackRect.Height += 4;
                    if (protoBar.BlackRect.Height >= 68)
                    {
                        if (robotMaster is Protoman)
                            Notificate("ProtomanDefeated");
                        else if (robotMaster is Gutsman)
                            Notificate("GutsmanDefeated");
                        else if (robotMaster is Fireman)
                            Notificate("FiremanDefeated");
                        else if (robotMaster is Iceman)
                            Notificate("IcemanDefeated");
                        protoBar.BlackRect.Height = 0;
                        gameState = GameStates.WinScreen;
                    }
                }

                else if (specialMegaman.mBullets[i].HitBox.Intersects(robotMaster.HitBox) &&
                    specialMegaman.mBullets[i].Visible == true && specialMegaman is FireMegamanDecorator)
                {
                    protoBar.BlackRect.Height += 10;
                    if (protoBar.BlackRect.Height >= 68)
                    {
                        if (robotMaster is Protoman)
                            Notificate("ProtomanDefeated");
                        else if (robotMaster is Gutsman)
                            Notificate("GutsmanDefeated");
                        else if (robotMaster is Fireman)
                            Notificate("FiremanDefeated");
                        else if (robotMaster is Iceman)
                            Notificate("IcemanDefeated");
                        protoBar.BlackRect.Height = 0;
                        gameState = GameStates.WinScreen;
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            #region Game States
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed &&
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Unregister(protoAchievement);
                this.Exit();
            }
            mMegaSprite.Update(gameTime);

            if(specialMegaman!=null)
                specialMegaman.Update(gameTime);

            if (gameState == GameStates.CreditScreen)
            {
                elige.X = 35;
                elige.Y = 40;
                if (Keyboard.GetState().IsKeyDown(Keys.Delete))
                {
                    megaBar.BlackRect.Height = 0;
                    protoBar.BlackRect.Height = 0;
                    gameState = GameStates.MenuScreen;
                }
            }

            if (gameState == GameStates.MenuScreen)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    elige.X += 2;

                    if (elige.X == 621)
                    {
                        elige.X = 621;
                        elige.X -= 2;
                    }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    elige.Y += 2;

                    if (elige.Y == 386)
                    {
                        elige.Y = 386;
                        elige.Y -= 2;
                    }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    elige.X -= 2;

                    if (elige.X == 1)
                    {
                        elige.X = 1;
                        elige.X += 2;
                    }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    elige.Y -= 2;

                    if (elige.Y == 0)
                    {
                        elige.Y = 0;
                        elige.Y += 2;
                    }
                }

                Rectangle fullScreenRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

                if (elige.X >= 280 & elige.X <= 370 & elige.Y >= 35 & elige.Y <= 130 &
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    robotMaster = enemyFactory.CreateRobotMaster("Iceman", this, Content.Load<Texture2D>("IcemanIzq2"),
                        enemyStartPosition, 27, 24);
                    protoBar = new LifeBars(this, "Barra de vida Iceman", ENEMY_LIFEBAR_X, ENEMY_LIFEBAR_Y);
                    protoScreen = new GameScreen(this, "FONDO COMPLETO ICEMAN", fullScreenRectangle);
                    gameState = GameStates.GameScreen;
                    mMegaSprite.Activo = true;
                    mMegaSprite.mBullets = new List<Bullets>();
                }

                if (elige.X >= 280 & elige.X <= 370 & elige.Y >= 320 & elige.Y <= 420 &
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    robotMaster = enemyFactory.CreateRobotMaster("Fireman", this, Content.Load<Texture2D>("FiremanLeft"),
                        enemyStartPosition, 31, 24);
                    protoBar = new LifeBars(this, "Barra de vida Fireman", ENEMY_LIFEBAR_X, ENEMY_LIFEBAR_Y);
                    protoScreen = new GameScreen(this, "FONDO COMPLETO FIREMAN", fullScreenRectangle);
                    gameState = GameStates.GameScreen;
                    mMegaSprite.Activo = true;
                    mMegaSprite.mBullets = new List<Bullets>();
                }

                if (elige.X >= 25 & elige.X <= 150 & elige.Y >= 150 & elige.Y <= 250 &
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    robotMaster = enemyFactory.CreateRobotMaster("Gutsman", this, Content.Load<Texture2D>("Gust2"),
                        enemyStartPosition, 32, 31);
                    protoBar = new LifeBars(this, "Barra de vida Gutsman", ENEMY_LIFEBAR_X, ENEMY_LIFEBAR_Y);
                    protoScreen = new GameScreen(this, "FONDO COMPLETO", fullScreenRectangle);
                    gameState = GameStates.GameScreen;
                    mMegaSprite.Activo = true;
                    mMegaSprite.mBullets = new List<Bullets>();
                }

                if (elige.X >= 280 & elige.X <= 370 & elige.Y >= 150 & elige.Y <= 250 &
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    robotMaster = enemyFactory.CreateRobotMaster("Protoman", this, Content.Load<Texture2D>("ProtoSolo"),
                        enemyStartPosition, 22, 24);
                    protoBar = new LifeBars(this, "Barra de vida Protoman", ENEMY_LIFEBAR_X, ENEMY_LIFEBAR_Y);
                    protoScreen = new GameScreen(this, "FONDO COMPLETO PROTOMAN", fullScreenRectangle);
                    gameState = GameStates.GameScreen;
                    mMegaSprite.Activo = true;
                    mMegaSprite.mBullets = new List<Bullets>();
                }

                if (elige.X >= 500 & elige.X <= 670 & elige.Y >= 150 & elige.Y <= 250 &
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameState = GameStates.CreditScreen;
                }
            }
            #endregion

            #region Hidden Tricks

            if (Keyboard.GetState().IsKeyDown(Keys.Add))
            {
                SongPlaylist.UniqueInstance().IncreaseVolume();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
            {
                SongPlaylist.UniqueInstance().DecreaseVolume();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.C) && mMegaSprite.Position.Y == 416)
            {
                if (specialMegaman == null)
                {
                    specialMegaman = factory.CrearMegaman("Fire", mMegaSprite.Position);
                    specialMegaman.LoadContent(this.Content);
                    specialMegaman.mBullets = new List<Bullets>();
                    mMegaSprite.Activo = false;
                }

                else if (specialMegaman is FireMegamanDecorator)
                {
                    specialMegaman = factory.CrearMegaman("Rock", mMegaSprite.Position);
                    specialMegaman.LoadContent(this.Content);
                    specialMegaman.mBullets = new List<Bullets>();
                    mMegaSprite.Activo = false;
                }

                else if (specialMegaman is RockMegamanDecorator)
                {
                    specialMegaman = null;
                    mMegaSprite.Activo = true;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                megaBar.BlackRect.Height += 2;

                if (megaBar.BlackRect.Height == 68)
                {
                    megaBar.BlackRect.Height = 68;
                    megaBar.BlackRect.Height -= 2;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                megaBar.BlackRect.Height -= 2;

                if (megaBar.BlackRect.Height == -2)
                {
                    megaBar.BlackRect.Height = -2;
                    megaBar.BlackRect.Height += 2;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                protoBar.BlackRect.Height += 2;

                if (protoBar.BlackRect.Height == 68)
                {
                    protoBar.BlackRect.Height = 68;
                    protoBar.BlackRect.Height -= 2;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                protoBar.BlackRect.Height -= 2;

                if (protoBar.BlackRect.Height == -2)
                {
                    protoBar.BlackRect.Height = -2;
                    protoBar.BlackRect.Height += 2;
                }
            }
            #endregion

            robotMaster.Update(gameTime);

            #region Victory/Defeat ScreenState
            if (gameState == GameStates.GameOverScreen)
            {
                elige.X = 35;
                elige.Y = 40;
                megaBar.BlackRect.Height = 0;
                protoBar.BlackRect.Height = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    gameState = GameStates.MenuScreen;

            }

            if (gameState == GameStates.WinScreen)
            {
                if (robotMaster is Protoman)
                    Notificate("ProtomanDefeated");
                else if (robotMaster is Gutsman)
                    Notificate("GutsmanDefeated");
                else if (robotMaster is Fireman)
                    Notificate("FiremanDefeated");
                else if (robotMaster is Iceman)
                    Notificate("IcemanDefeated");
                elige.X = 35;
                elige.Y = 40;
                megaBar.BlackRect.Height = 0;
                protoBar.BlackRect.Height = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    gameState = GameStates.MenuScreen;
            }
            #endregion

            SongPlaylist.UniqueInstance().Play();

            #region Normal Megaman Collition
            if (obstacle.ObstacleFrame.Intersects(robotMaster.HitBox))
            {
                robotMaster.Continua = false;

                if (robotMaster.HitBox.Intersects(obstacle.ObstacleTop))
                {
                    robotMaster.Position.Y = obstacle.ObstacleFrame.Top - robotMaster.Size.Height + 1;
                }
            }

            DetectBulletCollition(gameTime);

            if (robotMaster.HitBox.Intersects(mMegaSprite.HitBox))
            {
                megaBar.BlackRect.Height += 6;

                if (megaBar.BlackRect.Height >= 68)
                {
                    megaBar.BlackRect.Height = 0;
                    gameState = GameStates.GameOverScreen;
                }

            }

            if (obstacle.ObstacleFrame.Intersects(mMegaSprite.HitBox))
            {
                if (mMegaSprite.HitBox.Intersects(obstacle.ObstacleRight))
                {
                    mMegaSprite.Position.X = obstacle.ObstacleFrame.Right;
                }

                else if (mMegaSprite.HitBox.Intersects(obstacle.ObstacleLeft))
                {
                    mMegaSprite.Position.X = obstacle.ObstacleFrame.Left - mMegaSprite.Size.Width - 1;
                }

                else if (mMegaSprite.HitBox.Intersects(obstacle.ObstacleTop))
                {
                    mMegaSprite.Position.Y = obstacle.ObstacleFrame.Top - mMegaSprite.Size.Height + 1;
                }
            }


            if (pisoFrame.Intersects(mMegaSprite.HitBox))
            {
                mMegaSprite.Position.Y = pisoFrame.Top - mMegaSprite.Size.Height + 1;
            }
            #endregion

            #region Special Megaman Collition
            if (specialMegaman != null && obstacle.ObstacleFrame.Intersects(specialMegaman.HitBox))
            {
                if (specialMegaman.HitBox.Intersects(obstacle.ObstacleRight))
                {
                    specialMegaman.Position.X = obstacle.ObstacleFrame.Right;
                }

                else if (specialMegaman.HitBox.Intersects(obstacle.ObstacleLeft))
                {
                    specialMegaman.Position.X = obstacle.ObstacleFrame.Left - specialMegaman.Size.Width - 1;
                }

                else if (specialMegaman.HitBox.Intersects(obstacle.ObstacleTop))
                {
                    specialMegaman.Position.Y = obstacle.ObstacleFrame.Top - specialMegaman.Size.Height + 1;
                }
            }

            if (specialMegaman != null)
            {
                DetectSpecialBulletCollition(gameTime);
            }
            #endregion

            switch (gameState)
            {
                case GameStates.IntroScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        gameState = GameStates.CreditScreen;
                        //Keyboard.GetState().IsKeyUp(Keys.Enter);
                        //if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        //{
                        //gameState = Gamestates.gameScreen;
                        //}
                    }
                    break;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            if (gameState == GameStates.IntroScreen)
            {
                color.A++;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(introScreen.ScreenTexture, introScreen.ScreenFrame, color);
                spriteBatch.End();
            }

            if (gameState == GameStates.MenuScreen)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(menuScreen.ScreenTexture, menuScreen.ScreenFrame, Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(Electivo, elige, Color.White);
                spriteBatch.End();
            }

            if (gameState == GameStates.GameScreen)
            {
                robotMaster.Activo = true;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(protoScreen.ScreenTexture, protoScreen.ScreenFrame, Color.White);
                spriteBatch.End();


                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(piso, pisoFrame, Color.White);
                spriteBatch.Draw(obstacle.ObstacleTexture, obstacle.ObstacleFrame, Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                robotMaster.Draw(spriteBatch);
                mMegaSprite.Draw(spriteBatch);
                if(specialMegaman != null)
                specialMegaman.Draw(spriteBatch);
                spriteBatch.Draw(megaBar.LifeBar, megaBar.LifeFrame, Color.White);
                spriteBatch.Draw(megaBar.LifeBar, megaBar.BlackRect, Color.Black);
                spriteBatch.Draw(protoBar.LifeBar, protoBar.LifeFrame, Color.White);
                spriteBatch.Draw(protoBar.LifeBar, protoBar.BlackRect, Color.Black);
                spriteBatch.DrawString(font, mMegaSprite.mBullets.Count.ToString(), new Vector2(200, 30), Color.Blue);
                if (specialMegaman != null)
                    spriteBatch.DrawString(font, specialMegaman.mBullets.Count.ToString(), new Vector2(500, 30), Color.Red);
                spriteBatch.End();
            }

            if (gameState == GameStates.GameOverScreen)
            {
                color.A++;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(loseScreen.ScreenTexture, loseScreen.ScreenFrame, color);
                spriteBatch.End();
            }

            if (gameState == GameStates.WinScreen)
            {
                if (robotMaster is Protoman)
                    Notificate("ProtomanDefeated");
                else if (robotMaster is Gutsman)
                    Notificate("GutsmanDefeated");
                else if (robotMaster is Fireman)
                    Notificate("FiremanDefeated");
                else if (robotMaster is Iceman)
                    Notificate("IcemanDefeated");
                color.A++;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(winScreen.ScreenTexture, winScreen.ScreenFrame, color);
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(protoAchievement.AchievementTexture, protoAchievement.AchievementFrame, color);
                spriteBatch.End();
            }

            if
                (gameState == GameStates.CreditScreen)
            {
                color.A++;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(creditScreen.ScreenTexture, creditScreen.ScreenFrame, color);
                spriteBatch.End();
            }

            if (gameState != GameStates.GameScreen)
            {
                specialMegaman = null;
                robotMaster.Activo = false;
                robotMaster.Continua = true;
                mMegaSprite.Position = new Vector2(100, 416);
                robotMaster.Position = new Vector2(500, 428);
                mMegaSprite.mBullets.Clear();
            }

            base.Draw(gameTime);
        }
        public void Register(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Unregister(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void Notificate(string path)
        {
            foreach (var observer in observers)
            {
                observer.Update(this, path);
            }
        }
    }
}
