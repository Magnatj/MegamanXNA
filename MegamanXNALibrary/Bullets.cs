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
    public class Bullets : Sprite
    {
        const int MAX_DISTANCE = 500;
        public bool Visible = false;
        public Vector2 mStartPosition;
        Vector2 mSpeed;
        public Vector2 mDirection;

        public Rectangle HitBox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, 16, 12); }
        }

        public void LoadContent(ContentManager theContentManager, string type)
        {
            if(type == "Megaman")
                base.LoadContent(theContentManager, "Santo Limon");

            else if(type == "Fire")
                base.LoadContent(theContentManager, "Fire bullet");

            else if(type == "Rock")
                base.LoadContent(theContentManager, "Rock bullet");
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
        public override void DrawBullets(SpriteBatch theSpriteBatch)
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
        }
    }
}
