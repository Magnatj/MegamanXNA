using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MegamanXNALibrary
{
    public class MegamanFactory
    {
        private MegamanDecorator _megaman;

        public MegamanDecorator CrearMegaman(string type, Vector2 position)
        {
            if(type == "Fire")
                _megaman = new FireMegamanDecorator(_megaman, position);

            else if(type == "Rock")
                _megaman = new RockMegamanDecorator(_megaman,position);
            return _megaman;
        }
    }
}
