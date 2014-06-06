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
        void LoadContent(ContentManager theContentManager);
        void Update(GameTime theGameTime);
        void UpdateBullets(Microsoft.Xna.Framework.GameTime theGameTime, Microsoft.Xna.Framework.Input.KeyboardState aCurrentKeyboardState);
        void Shoot();
        void UpdateJump(Microsoft.Xna.Framework.Input.KeyboardState aCurrentKeyboardState);
        void Jump();
        void UpdateMovement(KeyboardState aCurrentKeyboardState);
        //void AnimateRight(GameTime gameTime);
        //void AnimateLeft(GameTime gameTime);
    }
}
