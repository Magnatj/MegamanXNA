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
    public class Obstacle
    {
        private Microsoft.Xna.Framework.Game Parent;

        public Vector2 position;

        public Vector2 Origin;

        public Texture2D ObstacleTexture;
        public Rectangle ObstacleFrame;
        public Rectangle ObstacleTop;
        public Rectangle ObstacleLeft;
        public Rectangle ObstacleRight;

        public void Draw(SpriteBatch spriteBatch)
        {
            Origin = new Vector2(ObstacleTexture.Width / 2, ObstacleTexture.Height / 2);
            spriteBatch.Draw(ObstacleTexture, position, ObstacleFrame, Color.White, 0f, Origin, 1.0f, SpriteEffects.None, 0);
        }

        public Obstacle(Microsoft.Xna.Framework.Game newParent, string path, int x, int y)
        {
            Parent = newParent;
            ObstacleTexture = Parent.Content.Load<Texture2D>(path); //Por ejemplo "Obstacle sprite"

            position = new Vector2(x,y);

            ObstacleFrame = new Rectangle(x, y, (int)(ObstacleTexture.Width), (int)(ObstacleTexture.Height));

            ObstacleLeft = new Rectangle((int)ObstacleFrame.Left, (int)ObstacleFrame.Y + 3, 0, ObstacleFrame.Height);
            ObstacleRight = new Rectangle((int)ObstacleFrame.Right, (int)ObstacleFrame.Y + 3, 0, ObstacleFrame.Height);
            ObstacleTop = new Rectangle((int)ObstacleFrame.X, (int)ObstacleFrame.Top, ObstacleFrame.Width, 0);
        }
    }
}
