using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    /// <summary>
    /// Game options
    /// </summary>
    static class GameOptions
    {
        public static bool kraj = false;

        public static int SpriteHeight = 100;
        public static int SpriteWidth = 100;

        public static int LeftEdge = 0;
        public static int RightEdge = 700;
        public static int UpEdge = 0;
        public static int DownEdge = 500;
        

        public static int brickWidth = 50;
        public static int brickHeight = 20;

        public static int brickColumns=7;
        public static int brickRows = 4;

        public static string resultsFile = "results.txt";
    }
}
