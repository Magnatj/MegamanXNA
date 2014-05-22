using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame4
{
    class Megaman1 : Sprite
    {
        Game1 Parent;
        Protoman Proto;

        KeyboardState oldstate;

        List<Bullets> mBullets = new List<Bullets>();

        ContentManager mContentManager;

        Texture2D texture;

        Rectangle rectangle;

        public Vector2 position;
        Vector2 origin;
        public Vector2 velocity;

        public int currentFrame;
        int FrameHeight;
        int FrameWidth;


        float timer;
        float interval = 40f;


        bool jumping;
        float startY, jumpspeed = 0;


        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            foreach (Bullets aLemon in mBullets)
            {
                aLemon.LoadContent(theContentManager);
            }
            Position = new Vector2(position.X, position.Y);
            base.LoadContent(theContentManager, "Santo Limon");
        }

        private void UpdateBullets(GameTime gameTime, KeyboardState currentState)
        {
            foreach (Bullets aLemon in mBullets)
            {
                aLemon.Update(gameTime);
            }

            if (currentState.IsKeyDown(Keys.X) == true && oldstate.IsKeyDown(Keys.X) == false)
            {
                Dispara();
            }
        }

        private void Dispara()
        {
            if (Keyboard.GetState().IsKeyUp(Keys.Right & Keys.Left))
            {
                bool aCreateNew = true;

                foreach (Bullets aLemon in mBullets)
                {
                    if (aLemon.Visible == false)
                    {
                        aCreateNew = false;
                        aLemon.Shoot(Position + new Vector2(Size.Width / 2, Size.Height / 2), new Vector2(200, 0), new Vector2(1, 0));
                        break;
                    }
                }

                if (aCreateNew == true)
                {
                    Bullets aLemon = new Bullets();
                    aLemon.LoadContent(mContentManager);
                    aLemon.Shoot(Position +

                    new Vector2(Size.Width / 2, Size.Height / 2), new Vector2(200, 200), new Vector2(1, 0));
                    mBullets.Add(aLemon);
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Right) & Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                bool aCreateNew = true;

                foreach (Bullets aLemon in mBullets)
                {
                    if (aLemon.Visible == false)
                    {
                        aCreateNew = false;
                        aLemon.Shoot(Position + new Vector2(Size.Width / 2, Size.Height / 2), new Vector2(200, 0), new Vector2(1, 0));
                        break;
                    }
                }

                if (aCreateNew == true)
                {
                    Bullets aLemon = new Bullets();
                    aLemon.LoadContent(mContentManager);
                    aLemon.Shoot(Position + new Vector2(Size.Width / 2, Size.Height / 2), new Vector2(200, 200), new Vector2(1, 0));
                    mBullets.Add(aLemon);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) & Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                bool aCreateNew = true;
                foreach (Bullets aLemon in mBullets)
                {
                    if (aLemon.Visible == false)
                    {
                        aCreateNew = false;
                        aLemon.Shoot(Position + new Vector2(-Size.Width / 2, Size.Height / 2), new Vector2(200, 0), new Vector2(-1, 0));
                        break;
                    }
                }

                if (aCreateNew == true)
                {
                    Bullets aLemon = new Bullets();
                    aLemon.LoadContent(mContentManager);
                    aLemon.Shoot(Position + new Vector2(-Size.Width / 2, Size.Height / 2), new Vector2(-200, 200), new Vector2(-1, 0));
                    mBullets.Add(aLemon);
                }
            }
        }

        public Megaman1(Game1 newParent, Texture2D newTexture, Vector2 newPosition, int newFrameHeight, int newFrameWidth)
        {
            Parent = newParent;
            texture = newTexture;
            position = newPosition;
            FrameHeight = newFrameHeight;
            FrameWidth = newFrameWidth;
            startY = position.Y;
            jumping = false;
            jumpspeed = 0;
        }

        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            position = position + velocity;

            KeyboardState currentState = Keyboard.GetState();
            UpdateBullets(gameTime, currentState);
            jump();


            if (currentState.IsKeyDown(Keys.Right) && position.X <= 788)
            {
                FrameHeight = 24;
                FrameWidth = 22;
                texture = Parent.Content.Load<Texture2D>("Linea Derecha 2");
                AnimateRight(gameTime);
                velocity.X = 3;

                if (position.X == 787)
                {
                    currentFrame = 0;
                }
            }


            else if (oldstate.IsKeyDown(Keys.Right) && position.X <= 787)
            {
                currentFrame = 0;
            }

            else if (currentState.IsKeyDown(Keys.Left) && position.X >= 12)
            {
                    FrameHeight = 24;
                    FrameWidth = 22;
                    texture = Parent.Content.Load<Texture2D>("Linea Izquierda 2");
                    AnimateLeft(gameTime);
                    velocity.X = -3;
                
                if (position.X == 13)
                {
                    currentFrame = 3;
                }
            }

            else if (oldstate.IsKeyDown(Keys.Left) && position.X >= 13)
            {
                currentFrame = 3;
            }

            else
            {
                velocity = Vector2.Zero;
            }
            oldstate = currentState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

        public void AnimateRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

                if (currentFrame > 3)
                {
                    currentFrame = 1;
                }
            }
        }

        public void AnimateLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

                if (currentFrame > 2)
                {
                    currentFrame = 0;
                }
            }
        }

        public void jump()
        {
            if (jumping)
            {
                position.Y += jumpspeed;
                jumpspeed += 1;

                if (position.Y >= startY)
                {
                    position.Y = startY;
                    jumping = false;
                }
            }

            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Z))
                {
                    jumping = true;
                    jumpspeed = -13;
                }
            }
        }
    }
}