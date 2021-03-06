﻿using System;
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
        public List<Bullets> mBullets = new List<Bullets>();
        ContentManager mContentManager;

        public Rectangle HitBox
        {
            get { return new Rectangle((int) Position.X, (int) Position.Y, Size.Width, Size.Height); }
        }

        const string MEGAMAN_ASSETNAME = "Sprite2";
        const string PASO_DERECHA = "Sprite6";
        const string PASO_DERECHA2 = "Sprite7";
        const string PASO_DERECHA3 = "Sprite8";
        const string BRINCO_DERECHA = "Sprite10";

        const int START_POSITION_X = 100;
        const int START_POSITION_Y = 416;
        const int MEGAMAN_SPEED = 160;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

        public bool Activo { get; set; }

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

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            if(mBullets.Count > 0)
            foreach (Bullets aLemon in mBullets)
            {
                aLemon.LoadContent(theContentManager, "Megaman");
            }
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, MEGAMAN_ASSETNAME);

        }
        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState);
            UpdateJump(aCurrentKeyboardState);

            if(Activo)
            UpdateBullets(theGameTime, aCurrentKeyboardState);


            mPreviousKeyboardState = aCurrentKeyboardState;

            base.Update(theGameTime, mSpeed, mDirection);
        }
        public void UpdateBullets(GameTime theGameTime, KeyboardState aCurrentKeyboardState)
        {
            foreach (Bullets aLemon in mBullets)
            {
                aLemon.Update(theGameTime);
            }

            for (int i = 0; i < mBullets.Count; i++)
            {
                if (mBullets[i].Visible == false)
                {
                    mBullets.Remove(mBullets[i]);
                    i--;
                }
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.X) == true && mPreviousKeyboardState.IsKeyDown(Keys.X) == false)
            {
                Shoot();
            }
        }
        public void Shoot()
        {
            if (mCurrentState == State.Walking)
            {
                if (Keyboard.GetState().IsKeyUp(Keys.Left & Keys.Right))
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
                        aLemon.LoadContent(mContentManager, "Megaman");
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
                        aLemon.LoadContent(mContentManager, "Megaman");
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
                        aLemon.LoadContent(mContentManager, "Megaman");
                        aLemon.Shoot(Position + new Vector2(-Size.Width / 2, Size.Height / 2), new Vector2(-200, 200), new Vector2(-1, 0));
                        mBullets.Add(aLemon);
                    }
                }
            }

            if (mCurrentState == State.Jumping)
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
                        aLemon.LoadContent(mContentManager, "Megaman");
                        aLemon.Shoot(Position + new Vector2(Size.Width / 2, Size.Height / 2), new Vector2(200, 200), new Vector2(1, 0));
                        mBullets.Add(aLemon);
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left))
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
                        aLemon.LoadContent(mContentManager, "Megaman");
                        aLemon.Shoot(Position + new Vector2(Size.Width / 2, Size.Height / 2), new Vector2(200, 200), new Vector2(1, 0));
                        mBullets.Add(aLemon);
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
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
                        aLemon.LoadContent(mContentManager, "Megaman");
                        aLemon.Shoot(Position + new Vector2(-Size.Width / 2, Size.Height / 2), new Vector2(-200, 200), new Vector2(-1, 0));
                        mBullets.Add(aLemon);
                    }
                }
            }
        }
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (Bullets aLemon in mBullets)
            {
                aLemon.Draw(theSpriteBatch);
            }
            base.Draw(theSpriteBatch);
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
        public void Jump()
        {
            if (mCurrentState != State.Jumping)
            {
                mCurrentState = State.Jumping;
                mStartingPosition = Position;
                mDirection.Y = MOVE_UP;
                mSpeed = new Vector2(MEGAMAN_SPEED, MEGAMAN_SPEED);
            }
        }
        public void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Walking)
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;
                if (aCurrentKeyboardState.IsKeyDown(Keys.Left))
                {
                    if (Position.X >= 0)
                    {
                        mSpeed.X = MEGAMAN_SPEED;
                        mDirection.X = MOVE_LEFT;
                    }
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right))
                {
                    if (Position.X <= (800 - 21))
                    {
                        mSpeed.X = MEGAMAN_SPEED;
                        mDirection.X = MOVE_RIGHT;
                    }
                }
            }
        }
    }
}
