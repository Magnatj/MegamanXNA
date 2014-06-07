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
    public class Iceman : RobotMasters
    {
        Microsoft.Xna.Framework.Game Parent;

        Texture2D IcemanTexture;
        public Rectangle IcemanFrame;

        public override Rectangle HitBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, Size.Width, Size.Height); }
        }

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

        //public bool Activo { get; set; }
        bool jumping;

        public Iceman(Microsoft.Xna.Framework.Game newParent, Texture2D newIceman, Vector2 newPosition, int newframeHeight, int newframeWidth)
        {
            Parent = newParent;
            IcemanTexture = newIceman;
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

        public override void Update(GameTime gameTime)
        {
            IcemanFrame = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            origin = new Vector2(IcemanFrame.Width / 2, IcemanFrame.Height / 2);
            if (Activo)
            {
                position = position + velocity;
                Jump();

                switch (Continua)
                {
                    case true:
                        //frameHeight = 24;
                        //frameWidth = 28;
                        IcemanTexture = Parent.Content.Load<Texture2D>("IcemanIzq2");
                        //AnimateLeft(gameTime);
                        velocity.X = -7;
                        if (position.X >= 14) return;
                        Continua = false;
                        break;
                    case false:
                        //frameHeight = 24;
                        //frameWidth = 28;
                        IcemanTexture = Parent.Content.Load<Texture2D>("IcemanDer2");
                        //AnimateRight(gameTime);
                        velocity.X = 7;
                        if (position.X <= 788) return;
                        Continua = true;
                        currentFrame = 0;
                        break;
                    default:
                        velocity = Vector2.Zero;
                        break;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(IcemanTexture, position, IcemanFrame,
            Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }
        public override void AnimateRight(GameTime gameTime)
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
        public override void AnimateLeft(GameTime gameTime)
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
        public override void Jump()
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
