using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;

namespace WindowsFormsApp1
{
    static class Command
    {
        public static Bitmap SS = null; // Delete Later on
        public static string LoLPath = "C:\\Users\\darko\\Desktop\\";
        public static Image LoadingScreen;


        public static int startX = Screen.PrimaryScreen.Bounds.Width / 2 - GameWindowSize.X / 2 + 150;
        public static int startY = Screen.PrimaryScreen.Bounds.Height / 2 - GameWindowSize.Y / 2 + 40;

        public static int GSX = Screen.PrimaryScreen.Bounds.Width / 2 - 80;
        public static int GSY = Screen.PrimaryScreen.Bounds.Height / 2 + GameWindowSize.Y / 2 - 40;

        #region mouse movement function introduction
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, long cButtons, long dwExtraInfo);

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        #endregion

        #region mouse movement
        public const int MOUSEEVENTF_MOVE = 0x1;
        public const int MOUSEEVENTF_LEFTDOWN = 0x2;
        public const int MOUSEEVENTF_LEFTUP = 0x4;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        public const int MOUSEEVENTF_MIDDLEUP = 0x40;
        #endregion

        public static class GameWindowSize
        {
            public static int X = 1280;
            public static int Y = 720;
        }
        public static class WindowSize
        {
            public static int Width = Screen.PrimaryScreen.Bounds.Width;
            public static int Height = Screen.PrimaryScreen.Bounds.Height;
        }
        public static Bitmap Screenshot;
        /// <summary>
        /// Takes a shot of Primary Screen
        /// </summary>
        /// <returns>Returns Image of Primary Screen</returns>
        public static Bitmap TakeScreenShotFromPrimaryScreen()
        {
            try
            {
                Screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                Graphics GFX = Graphics.FromImage(Screenshot);
                GFX.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                   Screen.PrimaryScreen.Bounds.Y,
                                   0,
                                   0,
                                   Screen.PrimaryScreen.Bounds.Size
                                   );
                return Screenshot;
            }
            catch
            {
                return null;
            }
        }

        public static List<string> FillList(Image img)
        {
            List<string> CoorList = new List<string>();
            Bitmap SB1 = new Bitmap(img);
            for (int i = 0; i < SB1.Width; i++)
            {
                for (int j = 0; j < SB1.Height; j++)
                {
                    Color color1 = SB1.GetPixel(i, j);
                    int BW = color1.R;
                    if (BW == 255)
                    {
                        CoorList.Add("" + i + "-" + j);
                    }
                }
            }
            return CoorList;
        }

        public static void GetSimpleImage(Bitmap inImage, out Bitmap outImage, out List<int[]> outList)
        {
            Bitmap SB1 = inImage; // FIXME
            Bitmap SB2 = new Bitmap(SB1.Width, SB1.Height);
            outList = new List<int[]>();
            for (int i = 0; i < SB1.Width; i++)
            {
                for (int j = 0; j < SB1.Height; j++)
                {
                    Color color1 = SB1.GetPixel(i, j);

                    byte R = color1.R;
                    if (R < 128)
                    {
                        R = 225;
                    }
                    else
                    {
                        R = 0;
                    }
                    byte G = color1.G;
                    if (G < 128)
                    {
                        G = 225;
                    }
                    else
                    {
                        G = 0;
                    }
                    byte B = color1.B;
                    if (B < 128)
                    {
                        B = 225;
                    }
                    else
                    {
                        B = 0;
                    }

                    int[] proto = { i, j, R, G, B };
                    outList.Add(proto);
                    color1 = Color.FromArgb(R, G, B);
                    SB2.SetPixel(i, j, color1);
                }
            }
            outImage = SB2;
        }

        public static bool IsOnLoadingScreen(Image ss)
        {
            /*
             * 0,30 - 250,1050
             * 
             * 1670,30 - 1900,970
             *
             */
            Thread.Sleep(1000);
            if (CheckDiff((Bitmap)LoadingScreen, (Bitmap)ss, 0, 30, 250, 1050, 5))
                return false;
            else
                return true;
        }

        public static void SaveSS(Image bitmap, string fileName)
        {
            bitmap.Save(fileName, ImageFormat.Jpeg);
        }

        public static Character GetCharacterFromGame()
        {
            Character chrs = new Character("Ashe");

            // Analizes game opening screen and finds characters
            return chrs;
        }

        public static Bitmap queue;
        public static Bitmap img2;
        public static Bitmap img3;
        public static bool AcceptGame()
        {
            //if (x1 != x2 => (diff 25 && rect 400,400))
            //  click accept
            //  wait
            img2 = TakeScreenShotFromPrimaryScreen();
            if (CheckDiff(queue, img2, Positions.ScanStarPos.X, Positions.ScanStarPos.Y, Positions.ScanStarPos.X + Positions.RectSizeToScan.X, Positions.ScanStarPos.Y + Positions.RectSizeToScan.Y, 5))
            {
                img3 = TakeScreenShotFromPrimaryScreen();
                Cursor.Position = Positions.AcceptGameButton;
                LeftClick();
                Thread.Sleep(1500);

                if (!CheckDiff(queue, img3, Positions.ScanStarPos.X, Positions.ScanStarPos.Y, Positions.ScanStarPos.X + Positions.RectSizeToScan.X, Positions.ScanStarPos.Y + Positions.RectSizeToScan.Y, 15))
                {
                    return false;
                }
                else
                {
                    Thread.Sleep(1500);
                    img3 = TakeScreenShotFromPrimaryScreen();

                    if (CheckDiff(img2, img3, Positions.ScanStarPos.X, Positions.ScanStarPos.Y, Positions.ScanStarPos.X + Positions.RectSizeToScan.X, Positions.ScanStarPos.Y + Positions.RectSizeToScan.Y, 15))
                    {
                        Thread.Sleep(1000);
                        return true;
                    }
                    else if (CheckDiff(img2, img3, Positions.ScanStarPos.X, Positions.ScanStarPos.Y, Positions.ScanStarPos.X + Positions.RectSizeToScan.X, Positions.ScanStarPos.Y + Positions.RectSizeToScan.Y, 15))
                    {
                        Thread.Sleep(1000);
                        return true;
                    }
                    else
                        return false;
                }

            }
            else
            {
                Thread.Sleep(1000);
                return false;
            }
            //if (x1 != x3 (=> diff 30 && rect 400,400)) && (x2 != x3 => (diff 40 && rect 600,600))
            //  you are in
            // return true;
            //else 
            //  try again
        }

        public static bool CheckDiff(Bitmap img1, Bitmap img2, int xStart, int yStart, int xFinish, int yFinish, float requiredDiff)
        {
            if ((img1 == null) || (img2 == null) || img1.Width != img2.Width || (img1.Height != img2.Height))
                return false;

            float diff = 0f;
            for (int x = xStart; x < xFinish; x++)
            {
                for (int y = yStart; y < yFinish; y++)
                {
                    Color index1 = img1.GetPixel(x, y);
                    Color index2 = img2.GetPixel(x, y);

                    diff += ((float)(Math.Abs(index1.R - index2.R) + Math.Abs(index1.G - index2.G) + Math.Abs(index1.B - index2.B)) * 100f) / (float)(256 * 3);
                }
            }
            diff /= (400 * 400);
            if (diff >= requiredDiff)
                return true;
            else
                return false;
        }

        public static bool CheckDiff(Bitmap img1, Bitmap img2)
        {
            return CheckDiff(img1, img2, 0, 0, img1.Width, img1.Height, 25f);
        }

        public static void SelectCharacter(Dictionary<string, Character> c, string[] lanes, string[] spellName)
        {
            int i = 0;
            foreach (Character ch in c.Values)
            {

                Cursor.Position = Positions.CharPoses[i.ToString()];
                LeftClick();
                i++;
                /*
                string charName = ch.Name;
                Cursor.Position = Positions.CharSearchBox;
                LeftClick();
                SendKeys.SendWait(charName);
                */
            }
            string lane = "Bottom";        // lanes[0];


            // lock character

            Cursor.Position = new Point(Positions.CharLockButton.X + 10, Positions.CharLockButton.Y - 10);
            Thread.Sleep(2000);
            LeftClick();
            LeftClick();

            // let other player to kow which lane you go

            SendKeys.SendWait("{ENTER}"); // this probably works?
            SendKeys.SendWait("I go " + lane + "!");
            SendKeys.SendWait("{ENTER}");

            // Select Spells you want
            /*
            //HotKey "D"
            Cursor.Position = Positions.Spell1;
            LeftClick();
            Cursor.Position = Positions.Spells[spellName[0]];
            LeftClick();

            //HotKey "F"
            Cursor.Position = Positions.Spell2;
            LeftClick();
            Cursor.Position = Positions.Spells[spellName[1]];
            LeftClick();
            */
        }

        public static void OpenTheGame()
        {
            ////////////////////////////////
            /*Do keyboard and mouse things*/
            /*    WARNING HARKODING!!!    */
            ////////////////////////////////

            // Find LolPatcher on Desktop

            // Then Click and Open LolPatcher
            Process process = Process.Start("" + LoLPath + "LOL");
            if (process.WaitForInputIdle())
            {
                Thread.Sleep(5000);
                /*
                IntPtr h = process.MainWindowHandle;
                SetForegroundWindow(h);
                */
                SendKeys.SendWait("1e2m3r4e"); // Don't Share! Even with This Program XD :D XD :D
                SendKeys.Send("{ENTER}");
            }
        }

        private static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 10, 10, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 10, 10, 0, 0);
        }

        private static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 10, 10, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 10, 10, 0, 0);
        }

        public static void Newgame(string gameType)
        {
            Positions.FillDic();
            // Do keyboard and mouse things
            Cursor.Position = new Point(startX, startY);
            Thread.Sleep(22000);
            /*
            // Game Type
            LeftClick();
            Cursor.Position = QueuePositions.queueTypePositions[gameType][0];
            Thread.Sleep(2500);
            */
            //Game Type
            LeftClick();
            Cursor.Position = Positions.queueTypePositions[gameType][0];
            Thread.Sleep(2000);
            LeftClick();

            // Map
            LeftClick();
            Cursor.Position = Positions.queueTypePositions[gameType][1];
            Thread.Sleep(2000);
            LeftClick();

            // Queue type?
            Cursor.Position = Positions.queueTypePositions[gameType][2];
            Thread.Sleep(2000);
            LeftClick();

            Cursor.Position = new Point(GSX, GSY);
            Thread.Sleep(2000);
            LeftClick();
            Thread.Sleep(3000);
            queue = Command.TakeScreenShotFromPrimaryScreen();// anında oyunbulursa diye
            LeftClick();
            Thread.Sleep(1000);

            // Enter Queue for New game
        }

        public static Situations AnalizeSituation()
        {
            if (Role != "Jungler" && IsAtBase())
                return Situations.Lane;
            if (Role == "Jungler" && IsAtBase())
                return Situations.Jung;
            if ((Role == "Jungler" || (HasTP() && IsTPUp())) && IsAnyLaneSuitableToGang())
                return Situations.Gang;
            if (Role == "Jungler" && IsDragonReady())
                return Situations.Dragon;
            if (Role == "Jungler" && IsBaronNashorReady())
                return Situations.BaronNashor;
            if (AtLane())
                return Situations.Farming;
            if (CanEnemyBeBeatable())
                return Situations.EngageWithEnemy;
            if (CantTakeTheGameSerious())
                return Situations.DoNothing;
        }

        public static void GetAllData(out Dictionary<string, Character> characters,
                                      out Dictionary<string, Dictionary<string, Skills>> DONT_USE_THIS_CHANGE_IT,
                                      out Dictionary<string, Passives> charPassives,
                                      out Dictionary<string, Monsters> monsters
            )
        {
            Dictionary<string, Character> c = new Dictionary<string, Character>();
            Dictionary<string, Dictionary<string, Skills>> DONT = new Dictionary<string, Dictionary<string, Skills>>();
            Dictionary<string, Passives> cp = new Dictionary<string, Passives>();
            Dictionary<string, Monsters> m = new Dictionary<string, Monsters>();
            ///////////////////////////
            /* Do ALL Kinda IO Things*/
            ///////////////////////////

            // Load Maps

            // Load Characters
            Character c1 = new Character("Ziggs");
            Character c2 = new Character("Ahri");
            Character c3 = new Character("Miss Fortune");
            Character c5 = new Character("Morgana");
            Character c4 = new Character("Warwick");
            c.Add("Ziggs", c1);
            c.Add("Ahri", c2);
            c.Add("Miss Fortune", c3);
            c.Add("Morgana", c4);
            c.Add("Warwick", c5);
            // Load Skills

            // Load Passives

            // Load Items

            // Load Buffs

            // Load Monsters

            // Match Character with skills and passives

            // Match Monsters with buffs
            characters = c;
            DONT_USE_THIS_CHANGE_IT = DONT;
            charPassives = cp;
            monsters = m;
        }

        public static void GoLane(Maps maps, string lane = "Mid") // Go your lane for farming or other lane for pushing
        {
            // Find correct location on the map for lane that we want
            int x = maps.lanesCoordsDic[lane][0];
            int y = maps.lanesCoordsDic[lane][1];

            // Click on the map.
            Cursor.Position = new Point(x, y);
            RightClick();

            // wait to go lane.
            Thread.Sleep(4000);
        }

        public static void GoJungle(Dictionary<string, Monsters> monsters, string monsterName)
        {
            //select one of the monsters.
            int x = monsters[monsterName].xPos;
            int y = monsters[monsterName].yPos;

            // go there.
            Cursor.Position = new Point(x, y);
            RightClick();

            //Atack it.
        }

        public static void GoGang(string destination)
        {

        }

        public static void GoForDragonOrBaronNashor(string DragonOrBaronNashor) // change func and parameter name as soon as posible
        {

        }

        public static bool DoFarming(bool isnew/*name need to be changed*/)
        {
            // Not Finished codes!!!
            List<int[]> minionsCoordslist;
            int[] minionCoords;
            if (isnew == true)
            {
                minionsCoordslist = FindMinions();
                minionCoords = minionsCoordslist[0];
                Cursor.Position = new Point(minionCoords[0], minionCoords[1]);
                RightClick();
                return false;
            }
            while (isnew == false)
            {
                minionsCoordslist = FindMinions();
                if (minionsCoordslist.Count <= 0)
                {
                    break;
                }
                minionCoords = minionsCoordslist[0];
                Cursor.Position = new Point(minionCoords[0], minionCoords[1]);
                RightClick();
            }

            return true;
        }

        // we need to figure out of red minyons' center coordinates
        // by using diferent ways such as white-black, red-blue-green, ...
        static List<int[]> FindMinions()
        {
            //FIXME: Check Later on
            List<int[]> IntList = new List<int[]>();
            /////////////////////////////////////////

            List<int[]> coords = new List<int[]>();
            // Bitmap ss = TakeScreenShotFromPrimaryScreen();
            if (SS != null)
            {
                GetSimpleImage(SS, out SS, out IntList);

            }
            return coords;
        }

        /// <summary>
        /// As what name says, Doesn't do anything
        /// </summary>
        /// <param name="timeToAFK">Seconds to stay AFK</param>
        public static void StayAFK(float timeToAFK)
        {
            Thread.Sleep((int)(timeToAFK * 1000));
        }
    }

    public enum Situations
    {
        Lane,
        Jung,
        Gang,
        Dragon,
        BaronNashor,
        Farming,
        InQueue,
        EngageWithEnemy,
        InHeroSelection,
        DoNothing
    }
}
