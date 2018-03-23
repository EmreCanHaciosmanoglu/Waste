using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Interface : Form
    {
        #region mouse movement function introduction
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void Mouse_event(int dwFlags, int dx, int dy, long cButtons, long dwExtraInfo);
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

        Maps map;
        Character myCharacter;

        Dictionary<string, Character> characters;
        Dictionary<string, Dictionary<string, Skills>> DONT_USE_THIS_CHANGE_IT;
        Dictionary<string, Passives> charPassives;
        Dictionary<string, Monsters> monsters;

        public Interface()
        {
            InitializeComponent();
            map = new Maps();
            characters = new Dictionary<string, Character>();
            DONT_USE_THIS_CHANGE_IT = new Dictionary<string, Dictionary<string, Skills>>();
            charPassives = new Dictionary<string, Passives>();
            monsters = new Dictionary<string, Monsters>();

            Command.LoadingScreen = new Bitmap("C:\\Users\\darko\\OneDrive\\Masaüstü\\AI\\Lol_Bot\\WindowsFormsApp1\\bin\\Debug\\LoadingScreen.jpeg");
        }

        private void Interface_Load(object sender, EventArgs e)
        {
            // Get All Data for game from File(NO NEED!SS)
            Command.GetAllData(out characters,
                               out DONT_USE_THIS_CHANGE_IT,
                               out charPassives,
                               out monsters
                               );
            //////////////////////////////
            cbGameType.Items.Add("CO-Op VS. AI _ SUMMONER'S RIFT _ INTRO");
            cbGameType.Items.Add("NormalSoloQueue");
            cbGameType.Items.Add("RURF");
            cbGameType.Items.Add("CO-Op VS. AI _ SUMMONER'S RIFT _ INTRO_LVL1");
            cbGameType.Items.Add("CO-Op VS. AI _ SUMMONER'S RIFT _ INTRO_LVL2");
            cbGameType.Items.Add("CO-Op VS. AI _ SUMMONER'S RIFT _ INTRO_LVL3");
        }
        // 850 690
        // 850 720
        private void Button1_Click(object sender, EventArgs e)
        {
            string[] Lanes = { "Top", "Mid" };
            string[] Spells = { "Flash", "Teleport" };
            // Opens The Game
            Command.OpenTheGame();
            int a = 1;
            // Starts A New Game
            if (a == 1)
            {
                //Command.Newgame("CO-Op VS. AI _ SUMMONER'S RIFT _ INTRO");
                //Command.Newgame("RURF");
                Command.Newgame(cbGameType.SelectedItem.ToString());
            }
            else
            {
                // OR
                Command.Newgame("NormalSoloQueue");
            }
            while (!Command.AcceptGame()) ;

            // We are in CharSelection
            Command.queue.Dispose();
            Command.img2.Dispose();
            Command.img3.Dispose();
            Command.SelectCharacter(characters, Lanes, Spells);
            Thread.Sleep(15000);

            ScreenCapture sc = new ScreenCapture();
            Image imageCheck = sc.CaptureScreen();

            while (!Command.IsOnLoadingScreen(imageCheck))
            {
                imageCheck = sc.CaptureScreen();
            }

            // Get to Know Characters(allies and Enemies) From Openneing Screen
            Command.GetCharacterFromGame();

            //We are in the Loading Screen

            while (Command.IsOnLoadingScreen(sc.CaptureScreen())) ;

            //We are in the game (or somehow game is closed)

            // Then Loop will be started
            this.Text = "We Are In The Game, Yuppyyyyyyy!!!";
            timer1.Enabled = true;
            timer1.Interval = 1000;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Analize Situation to
            switch (Command.AnalizeSituation())
            {
                case Situations.Lane:
                    // go to any Lane 
                    Command.GoLane(map, Command.Role);
                    break;
                case Situations.Jung:
                    // go to Jung
                    Command.GoJungle(monsters, "Gromp");
                    // Or
                    Command.GoJungle(monsters, "Counter");
                    break;
                case Situations.Gang:
                    // Go for Gang
                    Command.GoGang("Bottom");       //  Go for Lane
                    // OR                               Or
                    Command.GoGang("ADCarry");      //  Go for Character
                    break;
                case Situations.Dragon:
                    // Go for Dragon
                    Command.GoForDragon();
                    break;
                case Situations.BaronNashor:
                    // Go for Baron Nashor
                    Command.GoForBaronNashor();
                    break;
                case Situations.Farming:
                    //Delete Later -- Just for testing !!
                    Bitmap deneme = new Bitmap(pbSS.Image);
                    Command.SS = deneme;
                    /////////////////////////////////////

                    // start Farming
                    if (Command.DoFarming(true) == false)
                    {
                        timer1.Enabled = false;
                        Command.DoFarming(false);
                        timer1.Enabled = true;
                    }
                    break;
                case Situations.EngageWithEnemy:
                    Command.AttackTheEnemy();
                    break;
                case Situations.DoNothing:
                    // For Trolling :d(AFK)

                    Command.StayAFK(0.05f);
                    // Or Maybe There is a problem
                    // Caused by this program or Internet or another thing
                    break;
                default:
                    break;
            }
        }
    }
}
