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

namespace WindowsGame4
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Song introSong, gameSong;
        bool songStart = false;
        int playqueue = 1;

        Texture2D loseImagen;
        Rectangle uSuck;

        Protoman mProtoSprite;
        Bullets mBala;
        Megaman mMegaSprite;
        //Megaman1 MegaSprite;

        Obstaculo Obstaculo;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D piso;
        Rectangle pisoFrame;

        Texture2D Piedra;
        Rectangle piedraFrame;

        Texture2D barraVida;
        Rectangle vidaFrame;
        Rectangle barraNegra;

        Texture2D protoVida;
        Rectangle protoVidaFrame;
        Rectangle protoBarraNegra;

        Texture2D background;
        Rectangle mainFrame;

        Texture2D Protomenu;
        Rectangle menu;
        Texture2D Electivo;
        Rectangle elige;

        Texture2D introScreen;
        Rectangle introScreenFrame;

        Texture2D winScreen;
        Rectangle uWinFrame;

        Texture2D creditsScreen;
        Rectangle creditsFrame;

        Color color;

        public Rectangle laBala = new Rectangle();
        public Rectangle r = new Rectangle();
        public Rectangle piedraIzq = new Rectangle();
        public Rectangle piedraDer = new Rectangle();
        public Rectangle piedraTop = new Rectangle();
        public Rectangle proto = new Rectangle();

        enum Gamestates { introScreen, menuScreen, gameScreen, gameOverScreen, winScreen, creditScreen };
        Gamestates gameState = Gamestates.introScreen;


        public Game1()
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
            //MegaSprite = new Megaman1(this, Content.Load < Texture2D > ("Linea Derecha 2"), new Vector2(100, 428), 22, 24);
            mProtoSprite = new Protoman(this, Content.Load<Texture2D>("ProtoSolo"), new Vector2(500,428),22, 24);
            mMegaSprite = new Megaman();
            
            color = new Color(255, 255, 255);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

                introSong = Content.Load<Song>("Music\\Intro Song");
                gameSong = Content.Load<Song>("Music\\Go home and be a family man");

                mMegaSprite.LoadContent(this.Content);

                creditsScreen = Content.Load<Texture2D>("Creditos");
                creditsFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
          

            Protomenu = Content.Load<Texture2D>("Protoman Menu");
            menu = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Electivo = Content.Load<Texture2D>("Para elegir");
            elige = new Rectangle(35, 40, 200, 100);

            loseImagen = Content.Load<Texture2D>("You suck!!!");
            uSuck = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            winScreen = Content.Load<Texture2D>("You win!!!");
            uWinFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            barraVida = Content.Load<Texture2D>("Barra de vida a usar con X llena");
            vidaFrame = new Rectangle(30, 40, (int)(barraVida.Width), (int)(barraVida.Height));
            barraNegra = new Rectangle(33, 43, 8, 0);

            protoVida = Content.Load<Texture2D>("Barra de vida Protoman");
            protoVidaFrame = new Rectangle(760, 40, (int)protoVida.Width, (int)protoVida.Height);
            protoBarraNegra = new Rectangle(763, 43, 8, 0);

            piso = Content.Load<Texture2D>("Terreno completo para el nivel");
            pisoFrame = new Rectangle(0, 440, (int)(piso.Width), (int)(piso.Height));

            Piedra = Content.Load<Texture2D>("Piedra sprite");
            piedraFrame = new Rectangle(200, 408, (int)(Piedra.Width), (int)(Piedra.Height)); 

            background = Content.Load<Texture2D>("FONDO COMPLETO");
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            introScreen = Content.Load<Texture2D>("PrototipoFinal");
            introScreenFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
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

            mMegaSprite.Update(gameTime);

            if (gameState == Gamestates.creditScreen)
            {
                elige.X = 35;
                elige.Y = 40;
                if (Keyboard.GetState().IsKeyDown(Keys.Delete))
                {
                    barraNegra.Height = 0;
                    protoBarraNegra.Height = 0;
                    gameState = Gamestates.menuScreen;
                }
            }

            if (gameState == Gamestates.menuScreen)
            {
                if(Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    elige.X += 2;

                    if (elige.X == 621)
                    {
                        elige.X = 621;
                        elige.X -= 2;
                    }
                }
                else if(Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    elige.Y += 2;

                    if (elige.Y == 386)
                    {
                        elige.Y = 386;
                        elige.Y -= 2;
                    }
                }
                else if(Keyboard.GetState().IsKeyDown(Keys.Left))
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

                if(elige.X >= 280 & elige.X <= 370 & elige.Y >= 150 & elige.Y <= 250 &
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameState = Gamestates.gameScreen;
                    mMegaSprite.mBullets = new List<Bullets>();
                }

                if (elige.X >= 500 & elige.X <= 670 & elige.Y >= 150 & elige.Y <= 250 &
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameState = Gamestates.creditScreen;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                barraNegra.Height += 2;

                if (barraNegra.Height == 68)
                {
                    barraNegra.Height = 68;
                    barraNegra.Height -= 2;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                barraNegra.Height -= 2;

                if (barraNegra.Height == -2)
                {
                    barraNegra.Height = -2;
                    barraNegra.Height += 2;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                protoBarraNegra.Height += 2;

                if (protoBarraNegra.Height == 68)
                {
                    protoBarraNegra.Height = 68;
                    protoBarraNegra.Height -= 2;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                protoBarraNegra.Height -= 2;

                if (protoBarraNegra.Height == -2)
                {
                    protoBarraNegra.Height = -2;
                    protoBarraNegra.Height += 2;
                }
            }

            mProtoSprite.Update(gameTime);

            if(gameState == Gamestates.gameOverScreen)
            {
                elige.X = 35;
                elige.Y = 40;
                barraNegra.Height = 0;
                protoBarraNegra.Height = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    gameState = Gamestates.menuScreen;

                }

            if (gameState == Gamestates.winScreen)
            {
                elige.X = 35;
                elige.Y = 40;
                barraNegra.Height = 0;
                protoBarraNegra.Height = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    gameState = Gamestates.menuScreen;
            }

            if (!songStart)
            {
                if (MediaPlayer.State.Equals(MediaState.Stopped))
                {
                    if (playqueue == 2)
                    {
                        MediaPlayer.Play(gameSong);
                        playqueue = 2;
                    }

                    if (playqueue == 1)
                    {
                        MediaPlayer.Play(introSong);
                        playqueue = 2;
                    }
                }

            }

            r = new Rectangle((int)mMegaSprite.Position.X, (int)mMegaSprite.Position.Y, mMegaSprite.Size.Width, mMegaSprite.Size.Height);
            piedraIzq = new Rectangle((int)piedraFrame.Left, (int)piedraFrame.Y+3, 0, piedraFrame.Height);
            piedraDer = new Rectangle((int)piedraFrame.Right, (int)piedraFrame.Y+3, 0, piedraFrame.Height);
            piedraTop = new Rectangle((int)piedraFrame.X, (int)piedraFrame.Top, piedraFrame.Width, 0);
            proto = new Rectangle((int)mProtoSprite.position.X, (int)mProtoSprite.position.Y,
                mProtoSprite.Size.Width, mProtoSprite.Size.Height);
            laBala = new Rectangle((int)piedraFrame.X, (int)piedraFrame.Y, piedraFrame.Width, piedraFrame.Height);

            if(laBala.Intersects(proto))
            {
                    protoBarraNegra.Height += 2;
                    if (protoBarraNegra.Height == 68)
                    {
                        protoBarraNegra.Height = 68;
                        protoBarraNegra.Height -= 2;
                        gameState = Gamestates.winScreen;
                    }
                }

            for (int i = 0; i < mMegaSprite.mBullets.Count; i++)
            {
                if (mMegaSprite.mBullets[i].rect.Intersects(proto))
                {
                    protoBarraNegra.Height += 2;
                    if (protoBarraNegra.Height == 68)
                    {
                        protoBarraNegra.Height = 68;
                        protoBarraNegra.Height -= 2;
                        gameState = Gamestates.winScreen;
                    }
                }
            }

            if (proto.Intersects(r))
            {
                    barraNegra.Height += 2;

                    if (barraNegra.Height == 68)
                    {
                        barraNegra.Height = 68;
                        barraNegra.Height -= 2;
                        gameState = Gamestates.gameOverScreen;
                    }
 
            }

            if (piedraFrame.Intersects(r))
            {
                        if (r.Intersects(piedraDer))
                        {
                            mMegaSprite.Position.X = piedraFrame.Right;
                        }

                        else if (r.Intersects(piedraIzq))
                        {
                            mMegaSprite.Position.X = piedraFrame.Left - mMegaSprite.Size.Width - 1;
                        }

                        else if (r.Intersects(piedraTop))
                        {
                            mMegaSprite.Position.Y = piedraFrame.Top - mMegaSprite.Size.Height + 1;
                        }
                }
            

            if (pisoFrame.Intersects(r))
            {
                mMegaSprite.Position.Y = pisoFrame.Top - mMegaSprite.Size.Height + 1;
            }


            switch (gameState)
            { 
                case Gamestates.introScreen:
                    if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        gameState = Gamestates.menuScreen;
                        //Keyboard.GetState().IsKeyUp(Keys.Enter);
                        //if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        //{
                            //gameState = Gamestates.gameScreen;
                        //}
                    }
                    break;
            }
            // TODO: Add your update logic here
            //MegaSprite.Update(gameTime);

            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (gameState == Gamestates.introScreen)
            {
                color.A++;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(introScreen, introScreenFrame, color);
                spriteBatch.End();
            }

            if (gameState == Gamestates.menuScreen)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(Protomenu, menu, Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(Electivo, elige, Color.White);
                spriteBatch.End();
            }

            if (gameState == Gamestates.gameScreen)
            {

                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(background, mainFrame, Color.White);
                spriteBatch.End();


                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(piso, pisoFrame, Color.White);
                spriteBatch.Draw(Piedra, piedraFrame, Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                mProtoSprite.Draw(spriteBatch);
                mMegaSprite.Draw(spriteBatch);
                spriteBatch.Draw(barraVida, vidaFrame, Color.White);
                spriteBatch.Draw(barraVida, barraNegra, Color.Black);
                spriteBatch.Draw(protoVida, protoVidaFrame, Color.White);
                spriteBatch.Draw(protoVida, protoBarraNegra, Color.Black);
                spriteBatch.End();
            }

            if (gameState == Gamestates.gameOverScreen)
            {
                color.A++;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(loseImagen, uSuck, color);
                spriteBatch.End();
            }

            if (gameState == Gamestates.winScreen)
            {
                color.A++;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(winScreen, uWinFrame, color);
                spriteBatch.End();
            }

            if
                (gameState == Gamestates.creditScreen)
            {
                color.A++;
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                spriteBatch.Draw(creditsScreen, creditsFrame, color);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
