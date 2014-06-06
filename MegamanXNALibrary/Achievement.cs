using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MegamanXNALibrary
{
    public class Achievement : Sprite, IObserver
    {
        public Texture2D AchievementTexture { get; set; }
        public Rectangle AchievementFrame { get; set; }
        public void Update(Microsoft.Xna.Framework.Game game, string path)
        {
            AchievementTexture = game.Content.Load<Texture2D>(path);
            AchievementFrame = new Rectangle(0, 380, 400, 100);
        }
    }
}
