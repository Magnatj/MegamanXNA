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
    public abstract class RobotMasters : Sprite
    {
        public bool Continua;
        public bool Activo { get; set; }
        public virtual Rectangle HitBox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height); }
        }
        public abstract void Update(GameTime gameTime);
        public abstract void AnimateRight(GameTime gameTime);
        public abstract void AnimateLeft(GameTime gameTime);
        public abstract void Jump();

        public RobotMasters()
        {
        }
    }
}
