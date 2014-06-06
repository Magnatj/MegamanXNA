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
    public interface IMegaman
    {
        void UpdateBullets(GameTime theGameTime, KeyboardState aCurrentKeyboardState);
        void Shoot();
        void UpdateJump(KeyboardState aCurrentKeyboardState);
        void Jump();
        void AnimateRight(GameTime gameTime);
        void AnimateLeft(GameTime gameTime);
    }
}
