using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame4
{
    class Bullets : Sprite
    {
        const int MAX_DISTANCE = 500; 
        public bool Visible = false; 
        public Vector2 mStartPosition; 
        Vector2 mSpeed; 
        public Vector2 mDirection;

        public Rectangle rect;

        public void LoadContent(ContentManager theContentManager) 
        { 
            base.LoadContent(theContentManager, "Santo Limon");  
        }
        public void Update(GameTime theGameTime)
        {
            if (Vector2.Distance(mStartPosition, Position) > MAX_DISTANCE)
            {
                Visible = false;
            }
            if (Visible == true)
            {
                base.Update(theGameTime, mSpeed, mDirection);
            } 
        }
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            if (Visible == true)
            {
                base.Draw(theSpriteBatch); 
            } 
        }
        public void Shoot(Vector2 theStartPosition, Vector2 theSpeed, Vector2 theDirection)
        {
            Position = theStartPosition; 
            mStartPosition = theStartPosition; 
            mSpeed = theSpeed;
            mDirection = theDirection; Visible = true;
            rect = new Rectangle((int)Position.X, (int)Position.Y, 30, 30);
        }
    }
}
