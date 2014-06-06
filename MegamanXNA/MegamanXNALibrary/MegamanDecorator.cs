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
    public abstract class MegamanDecorator : Sprite, IMegaman
    {
        protected IMegaman _megamanDecorator;
        public abstract void LoadContent(ContentManager theContentManager);
        public abstract void Update(GameTime theGameTime);
        public abstract void UpdateBullets(Microsoft.Xna.Framework.GameTime theGameTime, Microsoft.Xna.Framework.Input.KeyboardState aCurrentKeyboardState);
        public abstract void Shoot();
        public abstract void UpdateJump(Microsoft.Xna.Framework.Input.KeyboardState aCurrentKeyboardState);
        public abstract void Jump();
        public abstract void UpdateMovement(KeyboardState aCurrentKeyboardState);

        public MegamanDecorator(IMegaman IMegamanDecorator)
        {
            _megamanDecorator = IMegamanDecorator;
        }
    }
}
