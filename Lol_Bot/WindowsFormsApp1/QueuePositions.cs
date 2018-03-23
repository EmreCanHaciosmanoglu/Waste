using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Positions
    {
        // 800 350 => 400x400
        public static Dictionary<string, Point[]> queueTypePositions = new Dictionary<string, Point[]>();
        public static Dictionary<string, Point> CharPoses = new Dictionary<string, Point>();
        public static Dictionary<string, Point> Spells = new Dictionary<string, Point>();

        public static int ScreenMiddleX = Screen.PrimaryScreen.Bounds.Width / 2;
        public static int ScreenMiddleY = Screen.PrimaryScreen.Bounds.Height / 2;

        public static Point CharSearchBox = new Point(ScreenMiddleX + 200, ScreenMiddleY - Command.GameWindowSize.Y + 100); // Check !!!
        public static Point CharLockButton = new Point(ScreenMiddleX, (int)(ScreenMiddleY + Command.GameWindowSize.Y * (3f / 8f)));  // Check Maybe?
        public static Point AcceptGameButton = new Point(ScreenMiddleX + 10, (int)(ScreenMiddleY + Command.GameWindowSize.Y  / 3f - 30));
        public static Point Spell1 = new Point(ScreenMiddleX + 25, ScreenMiddleY + (Command.GameWindowSize.Y * 11) / 24 + 20);
        public static Point Spell2 = new Point(ScreenMiddleX + 80, ScreenMiddleY + (Command.GameWindowSize.Y * 11) / 24 + 20);

        public static Point ScanStarPos = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - Command.GameWindowSize.X/2, Screen.PrimaryScreen.Bounds.Height / 2 - Command.GameWindowSize.Y/2); // change this into screen width things!!!
        public static Point RectSizeToScan = new Point(Command.GameWindowSize.X/2, Command.GameWindowSize.Y/2);

        public static void FillDic()
        {
            Point[] ps = { new Point(0, 0), new Point(ScreenMiddleX - 200, ScreenMiddleY - 100), new Point(ScreenMiddleX - 200, ScreenMiddleY + 210) }; // Okayish?
            queueTypePositions["CO-Op VS. AI _ SUMMONER'S RIFT _ INTRO"] = ps;
            Point[] ps2 = { new Point(0, 0), new Point(ScreenMiddleX + 250, ScreenMiddleY - 100), new Point(ScreenMiddleX - 200, ScreenMiddleY + 210) }; // Okayish?
            queueTypePositions["RURF"] = ps2;
            Point[] ps3 = 
            {
                new Point(ScreenMiddleX - (Command.GameWindowSize.X*3) / 8 + 10, ScreenMiddleY - (Command.GameWindowSize.Y * 3) / 8 + 5),
                new Point(ScreenMiddleX - 130, ScreenMiddleY - 100),
                new Point(ScreenMiddleX - 130, ScreenMiddleY + 120)
            };
            queueTypePositions["CO-Op VS. AI _ SUMMONER'S RIFT _ INTRO_LVL1"] = ps3;
            Point[] ps4 =
            {
                new Point(ScreenMiddleX - (Command.GameWindowSize.X*3) / 8 + 10, ScreenMiddleY - (Command.GameWindowSize.Y * 3) / 8 + 5),
                new Point(ScreenMiddleX - 130, ScreenMiddleY - 100),
                new Point(ScreenMiddleX - 130, ScreenMiddleY + 150)
            };
            queueTypePositions["CO-Op VS. AI _ SUMMONER'S RIFT _ INTRO_LVL2"] = ps4;
            Point[] ps5 =
            {
                new Point(ScreenMiddleX - (Command.GameWindowSize.X*3) / 8 + 10, ScreenMiddleY - (Command.GameWindowSize.Y * 3) / 8 + 5),
                new Point(ScreenMiddleX - 130, ScreenMiddleY - 100),
                new Point(ScreenMiddleX - 130, ScreenMiddleY + 180)
            };
            queueTypePositions["CO-Op VS. AI _ SUMMONER'S RIFT _ INTRO_LVL3"] = ps5;

            /////////////////////////////////
            Point charPos1 = new Point(ScreenMiddleX - 250, ScreenMiddleY - 200);
            CharPoses["0"] = charPos1;
            Point charPos2 = new Point(ScreenMiddleX - 150, ScreenMiddleY - 200);
            CharPoses["1"] = charPos2;
            Point charPos3 = new Point(ScreenMiddleX - 50, ScreenMiddleY - 200);
            CharPoses["2"] = charPos3;
            Point charPos4 = new Point(ScreenMiddleX + 50, ScreenMiddleY - 200);
            CharPoses["4"/*3*/] = charPos4;
            Point charPos5 = new Point(ScreenMiddleX + 150, ScreenMiddleY - 200);
            CharPoses["3"/*4*/] = charPos5;

            ////////////////////////////////
            Point p = new Point(0,0);
            Spells["Flash"] = p;
            p = new Point(0, 0);
            Spells["Teleport"] = p;
        }
    }
}
