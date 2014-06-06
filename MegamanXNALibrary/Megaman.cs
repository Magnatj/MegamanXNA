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

namespace MegamanXNALibrary
{
    public class Megaman : Sprite, IMegaman
    {
        private Microsoft.Xna.Framework.Game Parent;

        KeyboardState oldstate;

        public List<Bullets> mBullets = new List<Bullets>();
        ContentManager mContentManager;

        Texture2D texture;

        public Rectangle MegamanFrame;

        public Vector2 position;
        Vector2 origin;
        public Vector2 velocity;

        public int currentFrame;
        int FrameHeight;
        int FrameWidth;


        float timer;
        private const float interval = 40f;


        bool jumping;
        float startY, jumpspeed = 0;

        const int START_POSITION_X = 100;
        const int START_POSITION_Y = 416;
        const int MEGAMAN_SPEED = 160;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

        Vector2 mStartingPosition = Vector2.Zero;

        enum State
        {
            Walking,
            Jumping
        }
        State mCurrentState = State.Walking;
        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;
        KeyboardState mPreviousKeyboardState;

        public Rectangle miBala;


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

        public void UpdateBullets(GameTime gameTime, KeyboardState currentState)
        {
            foreach (Bullets aLemon in mBullets)
            {
                aLemon.Update(gameTime);
            }

            if (currentState.IsKeyDown(Keys.X) == true && oldstate.IsKeyDown(Keys.X) == false)
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            if (Keyboard.GetState().IsKeyUp(Keys.Right & Keys.Left))
            {
                bool aCreateNew = true;

                foreach (Bullets aLemon in mBullets)
                {
                    if (!aLemon.Visible)
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
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, MegamanFrame, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

        public void DrawBullets(SpriteBatch theSpriteBatch)
        {
            foreach (Bullets aLemon in mBullets)
            {
                aLemon.Draw(theSpriteBatch);
            }
            base.Draw(theSpriteBatch);
        }


        public Megaman(Microsoft.Xna.Framework.Game newParent, Texture2D newTexture, Vector2 newPosition, int newFrameHeight, int newFrameWidth)
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

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height);
        }

        public void Update(GameTime gameTime)
        {
            MegamanFrame = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            origin = new Vector2(MegamanFrame.Width / 2, MegamanFrame.Height / 2);
            position = position + velocity;

            KeyboardState currentState = Keyboard.GetState();
            UpdateBullets(gameTime, currentState);
            Jump();


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

        public void UpdateJump(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Walking)
            {
                if (aCurrentKeyboardState.IsKeyDown(Keys.Z) == true && mPreviousKeyboardState.IsKeyDown(Keys.Z) == false)
                {
                    Jump();
                }
            }

            if (mCurrentState == State.Jumping)
            {
                if (mStartingPosition.Y - Position.Y > 70)
                {
                    mDirection.Y = MOVE_DOWN;
                }
                if (Position.Y > mStartingPosition.Y)
                {
                    if (Position.X >= 0 || Position.X <= (800 - 21))
                    {
                        Position.Y = mStartingPosition.Y; mCurrentState = State.Walking; mDirection = Vector2.Zero;
                    }
                }
                if (aCurrentKeyboardState.IsKeyDown(Keys.Left))
                {
                    if (Position.X >= 0)
                    {
                        mSpeed.X = MEGAMAN_SPEED;
                        mDirection.X = MOVE_LEFT;
                    }

                    if (Position.X <= 0)
                    {
                        Position.X = 0;
                    }
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right))
                {
                    if (Position.X <= (800 - 21))
                    {
                        mSpeed.X = MEGAMAN_SPEED;
                        mDirection.X = MOVE_RIGHT;
                    }

                    if (Position.X >= (800 - 21))
                    {
                        Position.X = 800 - 21;
                    }

                }
            }
        }

        public void Draw1(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, MegamanFrame, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

        public void AnimateRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if (!(timer > interval)) return;

            currentFrame++;
            timer = 0;

            if (currentFrame > 3)
            {
                currentFrame = 1;
            }
        }

        public void AnimateLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if (!(timer > interval)) return;

            currentFrame++;
            timer = 0;

            if (currentFrame > 2)
            {
                currentFrame = 0;
            }
        }

        public void Jump()
        {
            if (jumping)
            {
                position.Y += jumpspeed;
                jumpspeed += 1;

                if (!(position.Y >= startY)) return;

                position.Y = startY;
                jumping = false;
            }

            else
            {
                if (!Keyboard.GetState().IsKeyDown(Keys.Z)) return;

                jumping = true;
                jumpspeed = -13;
            }
        }
    }
}
