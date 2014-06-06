using System;

namespace MegamanXNAGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MegamanGame game = new MegamanGame())
            {
                game.Run();
            }
        }
    }
#endif
}

