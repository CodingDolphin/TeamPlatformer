using System;

namespace TeamPlatformer
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TeamPlatformer game = new TeamPlatformer())
            {
                game.Run();
            }
        }
    }
#endif
}

