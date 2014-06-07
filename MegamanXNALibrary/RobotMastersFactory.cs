using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class RobotMastersFactory
    {
        private RobotMasters _robotMaster;

        public RobotMasters CreateRobotMaster(string type, Microsoft.Xna.Framework.Game newParent, Texture2D newRobotMaster, Vector2 newPosition, int newframeHeight, int newframeWidth)
        {
            if(type == "Protoman")
                _robotMaster = new Protoman(newParent, newRobotMaster, newPosition, newframeHeight, newframeWidth);
            else if(type == "Gutsman")
                _robotMaster = new Gutsman(newParent, newRobotMaster, newPosition, newframeHeight, newframeWidth);
            else if (type == "Fireman")
                _robotMaster = new Fireman(newParent, newRobotMaster, newPosition, newframeHeight, newframeWidth);
            else if (type == "Iceman")
                _robotMaster = new Iceman(newParent, newRobotMaster, newPosition, newframeHeight, newframeWidth);
            return _robotMaster;
        }
    }
}
