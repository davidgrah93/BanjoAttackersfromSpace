using System;

namespace WindowsGame2
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static void Main(string[] args)
        {
            using (BanjoGame game = new BanjoGame())
            {
                game.Run();
            }
        }
    }
#endif
}

