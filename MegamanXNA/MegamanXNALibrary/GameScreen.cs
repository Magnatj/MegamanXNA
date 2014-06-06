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
    public class GameScreen
    {
        public Texture2D ScreenTexture { get; set; }
        public Rectangle ScreenFrame { get; set; }

        public GameScreen(Microsoft.Xna.Framework.Game game, string path, Rectangle frame)
        {
            ScreenTexture = game.Content.Load<Texture2D>(path);
            ScreenFrame = frame;
        }
    }
}
