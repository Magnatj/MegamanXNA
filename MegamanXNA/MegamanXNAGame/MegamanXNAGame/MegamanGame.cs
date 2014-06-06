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
using MegamanXNALibrary;

namespace MegamanXNAGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MegamanGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Texture2D Electivo;
        Rectangle elige;

        Color color;

        Protoman mProtoSprite;
        Megaman mMegaSprite;

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

        GameStates gameState = GameStates.IntroScreen;


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
            mProtoSprite = new Protoman(this, Content.Load<Texture2D>("ProtoSolo"), new Vector2(500, 428), 22, 24);
            font = Content.Load<SpriteFont>("GameFont");
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
            SongPlaylist.AddSong(this, "Intro Song");
            SongPlaylist.AddSong(this, "Go home and be a family man");

            Rectangle fullScreenRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            mMegaSprite.LoadContent(this.Content);

            introScreen = new GameScreen(this, "PrototipoFinal", fullScreenRectangle);
            creditScreen = new GameScreen(this, "Creditos", fullScreenRectangle);
            menuScreen = new GameScreen(this, "Protoman Menu", fullScreenRectangle);
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
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        void DetectBulletCollition(GameTime gameTime)
        {
            for(int i =0; i <mMegaSprite.mBullets.Count; i++)
            {
                if (mMegaSprite.mBullets[i].HitBox.Intersects(mProtoSprite.HitBox) && mMegaSprite.mBullets[i].Visible == true)
                {
                    protoBar.BlackRect.Height += 2;
                    if (protoBar.BlackRect.Height == 68)
                    {
                        protoBar.BlackRect.Height = 68;
                        protoBar.BlackRect.Height -= 2;
                        gameState = GameStates.WinScreen;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            #region Game States
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            mMegaSprite.Update(gameTime);

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

                if (elige.X >= 280 & elige.X <= 370 & elige.Y >= 150 & elige.Y <= 250 &
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameState = GameStates.GameScreen;
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


            mProtoSprite.Update(gameTime);

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
                elige.X = 35;
                elige.Y = 40;
                megaBar.BlackRect.Height = 0;
                protoBar.BlackRect.Height = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    gameState = GameStates.MenuScreen;
            }

            SongPlaylist.UniqueInstance().Play();

            if (obstacle.ObstacleFrame.Intersects(mProtoSprite.HitBox))
            {
                protoBar.BlackRect.Height += 2;
                if (protoBar.BlackRect.Height == 68)
                {
                    protoBar.BlackRect.Height = 68;
                    protoBar.BlackRect.Height -= 2;
                    gameState = GameStates.WinScreen;
                }
            }

            DetectBulletCollition(gameTime);

            if (mProtoSprite.HitBox.Intersects(mMegaSprite.HitBox))
            {
                megaBar.BlackRect.Height += 2;

                if (megaBar.BlackRect.Height == 68)
                {
                    megaBar.BlackRect.Height = 68;
                    megaBar.BlackRect.Height -= 2;
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

            switch (gameState)
            {
                case GameStates.IntroScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        gameState = GameStates.MenuScreen;
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
                mProtoSprite.Activo = true;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(protoScreen.ScreenTexture, protoScreen.ScreenFrame, Color.White);
                spriteBatch.End();


                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(piso, pisoFrame, Color.White);
                spriteBatch.Draw(obstacle.ObstacleTexture, obstacle.ObstacleFrame, Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                mProtoSprite.DrawBullets(spriteBatch);
                mMegaSprite.Draw(spriteBatch);
                spriteBatch.Draw(megaBar.LifeBar, megaBar.LifeFrame, Color.White);
                spriteBatch.Draw(megaBar.LifeBar, megaBar.BlackRect, Color.Black);
                spriteBatch.Draw(protoBar.LifeBar, protoBar.LifeFrame, Color.White);
                spriteBatch.Draw(protoBar.LifeBar, protoBar.BlackRect, Color.Black);
                spriteBatch.DrawString(font, mMegaSprite.mBullets.Count.ToString(), new Vector2(200, 30), Color.Blue);
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
                color.A++;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(winScreen.ScreenTexture, winScreen.ScreenFrame, color);
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
                mProtoSprite.Activo = false;
                mProtoSprite.Continua = true;
                mMegaSprite.Position = new Vector2(100,416);
                mProtoSprite.Position = new Vector2(500, 428);
                mMegaSprite.mBullets.Clear();
            }

            base.Draw(gameTime);
        }
    }
}
