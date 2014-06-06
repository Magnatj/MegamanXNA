using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MegamanXNALibrary
{
    public class MegamanContext
    {
        private IMegaman _strategy = new Megaman();

        //interchangeable algorithm
        public void SwitchStrategy()
        {
            if (_strategy is Megaman)
            {
                _strategy = new FireMegamanDecorator(_strategy, new Vector2());
            }

            else
            {
                _strategy = new Megaman();
            }
        }
    }
}
