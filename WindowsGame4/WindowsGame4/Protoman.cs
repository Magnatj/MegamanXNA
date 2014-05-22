using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    class Protoman : Sprite
    {
        bool Continua;

        Game1 Parent;

        Texture2D ProtomanTexture;
        Rectangle Protomanframe;

        public Vector2 position;
        Vector2 origin;
        Vector2 velocity;
        Vector2 mStartingPosition = Vector2.Zero;

        int currentFrame;
        int frameHeight;
        int frameWidth;

        float timer;
        float interval = 40f;
        float startY, jumpspeed = 0;

        bool jumping;

        public Protoman(Game1 newParent, Texture2D newProtoman, Vector2 newPosition, int newframeHeight, int newframeWidth)
        {
            Parent = newParent;
            ProtomanTexture = newProtoman;
            position = newPosition;
            frameHeight = newframeHeight;
            frameWidth = newframeWidth;
            startY = position.Y;
            jumping = false;
            jumpspeed = 0;
            Continua = true;
            position.X = 500;
            position.Y = 428;
        }

        public void Update(GameTime gameTime)
        {
            Protomanframe = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            origin = new Vector2(Protomanframe.Width / 2, Protomanframe.Height / 2);
            position = position + velocity;
            jump();

            if (Continua == true)
            {
                frameHeight = 24;
                frameWidth = 28;
                ProtomanTexture = Parent.Content.Load<Texture2D>("ProtoBossIzq1");
                AnimateLeft(gameTime);
                velocity.X = -2;

                if (position.X == 12)
                {
                    Continua =false;
                }
            }

            else if (Continua == false)
            {
                frameHeight = 24;
                frameWidth = 28;
                ProtomanTexture = Parent.Content.Load<Texture2D>("ProtoDer1");
                AnimateRight(gameTime);
                velocity.X = 2;

                if (position.X == 788)
                {
                    Continua = true;
                    currentFrame = 0;
                }
            }

            else
            {
                velocity = Vector2.Zero;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ProtomanTexture, position, Protomanframe,
            Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

        public void AnimateRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

                if (currentFrame > 2)
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

                if (currentFrame > 1)
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
                    jumping =

                    false;
                }
            }

            else
            {
                if (true)
                {
                    jumping = true;
                    jumpspeed = -9;
                }
            }
        }
    }
}