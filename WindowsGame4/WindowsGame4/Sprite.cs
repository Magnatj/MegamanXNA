using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame4
{
    class Sprite
    {
        public string AssetName;
        public Rectangle Size;
        private float mScale = 1.0f;
        public float Scale        
        {
            get 
            { 
                return mScale;
            }            
            set            
            { 
                mScale = value;
                Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            } 
        }

        public Vector2 Position = new Vector2(0,0);
       
        private Texture2D mSpriteTexture;

        public void LoadContent(ContentManager theContentManager, string theAssetName) 
        { 
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName); 
            AssetName = theAssetName; 
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale)); 
        }

        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        { 
            Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds; 
        }

        public virtual void Draw(SpriteBatch theSpriteBatch) 
        {
            theSpriteBatch.Draw(mSpriteTexture, Position, 
                new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
                Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
