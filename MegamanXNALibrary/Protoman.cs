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
    public class Protoman : Sprite
    {
        bool Continua;

        Microsoft.Xna.Framework.Game Parent;

        Texture2D ProtomanTexture;
        public Rectangle ProtomanFrame;

        public Vector2 position;
        Vector2 origin;
        Vector2 velocity;
        Vector2 mStartingPosition = Vector2.Zero;

        int currentFrame;
        int frameHeight;
        int frameWidth;

        float timer;
        private const float interval = 40f;
        float startY, jumpspeed = 0;

        bool jumping;

        public Protoman(Microsoft.Xna.Framework.Game newParent, Texture2D newProtoman, Vector2 newPosition, int newframeHeight, int newframeWidth)
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

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height);
        }

        public void Update(GameTime gameTime)
        {
            ProtomanFrame = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            origin = new Vector2(ProtomanFrame.Width / 2, ProtomanFrame.Height / 2);
            position = position + velocity;
            Jump();

            switch (Continua)
            {
                case true:
                    frameHeight = 24;
                    frameWidth = 28;
                    ProtomanTexture = Parent.Content.Load<Texture2D>("ProtoBossIzq1");
                    AnimateLeft(gameTime);
                    velocity.X = -2;
                    if (position.X != 12) return;
                    Continua = false;
                    break;
                case false:
                    frameHeight = 24;
                    frameWidth = 28;
                    ProtomanTexture = Parent.Content.Load<Texture2D>("ProtoDer1");
                    AnimateRight(gameTime);
                    velocity.X = 2;
                    if (position.X != 788) return;
                    Continua = true;
                    currentFrame = 0;
                    break;
                default:
                    velocity = Vector2.Zero;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ProtomanTexture, position, ProtomanFrame,
            Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

        public void AnimateRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if (!(timer > interval)) return;

            currentFrame++;
            timer = 0;

            if (currentFrame > 2)
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

            if (currentFrame > 1)
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
                //if (false) return;

                jumping = true;
                jumpspeed = -9;
            }
        }
    }
}
