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
    public class LifeBars
    {
        public Texture2D LifeBar { get; private set; }
        public Rectangle LifeFrame { get; private set; }
        public Rectangle BlackRect;

        public LifeBars(Microsoft.Xna.Framework.Game game, string path, int x, int y)
        {
            LifeBar = game.Content.Load<Texture2D>(path);
            LifeFrame = new Rectangle(x, y, (int)(LifeBar.Width), (int)(LifeBar.Height));
            BlackRect = new Rectangle(x+3, y+3, 8, 0);
        }

    }
}
