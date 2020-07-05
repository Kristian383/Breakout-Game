using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OTTER
{
    /// <summary>
    /// -
    /// </summary>
    public partial class BGL : Form
    {
        /* ------------------- */
        //forme
        public Form frmStart;
        //public Form frmKraj;

        private string player;

        public string Player
        {
            get { return player; }
            set { player = value; }
        }

        public string background;

        public string Background
        {
            get { return background; }
            set { background = value; }
        }
        #region Environment Variables

        List<Func<int>> GreenFlagScripts = new List<Func<int>>();

        /// <summary>
        /// Uvjet izvršavanja igre. Ako je <c>START == true</c> igra će se izvršavati.
        /// </summary>
        /// <example><c>START</c> se često koristi za beskonačnu petlju. Primjer metode/skripte:
        /// <code>
        /// private int MojaMetoda()
        /// {
        ///     while(START)
        ///     {
        ///       //ovdje ide kod
        ///     }
        ///     return 0;
        /// }</code>
        /// </example>
        public static bool START = true;

        //sprites
        /// <summary>
        /// Broj likova.
        /// </summary>
        public static int spriteCount = 0, soundCount = 0;

        /// <summary>
        /// Lista svih likova.
        /// </summary>
        //public static List<Sprite> allSprites = new List<Sprite>();
        public static SpriteList<Sprite> allSprites = new SpriteList<Sprite>();

        //sensing
        int mouseX, mouseY;
        Sensing sensing = new Sensing();

        //background
        List<string> backgroundImages = new List<string>();
        int backgroundImageIndex = 0;
        string ISPIS = "";

        SoundPlayer[] sounds = new SoundPlayer[1000];
        TextReader[] readFiles = new StreamReader[1000];
        TextWriter[] writeFiles = new StreamWriter[1000];
        bool showSync = false;
        int loopcount;
        DateTime dt = new DateTime();
        String time;
        double lastTime, thisTime, diff;

        #endregion
        /* ------------------- */
        #region Events

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {                
                foreach (Sprite sprite in allSprites)
                {                    
                    if (sprite != null)
                        if (sprite.Show == true)
                        {
                            g.DrawImage(sprite.CurrentCostume, new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Heigth));
                        }
                    if (allSprites.Change)
                        break;
                }
                if (allSprites.Change)
                    allSprites.Change = false;
            }
            catch
            {
                //ako se doda sprite dok crta onda se mijenja allSprites
                MessageBox.Show("Greška!");
            }
        }

        private void startTimer(object sender, EventArgs e)
        {
           // if (this.frmStart != null)
                this.frmStart.Hide();
           // else if (this.frmKraj != null) this.frmKraj.Hide();

            timer1.Start();
            timer2.Start();
            Init();
        }

        private void updateFrameRate(object sender, EventArgs e)
        {
            updateSyncRate();
        }

        /// <summary>
        /// Crta tekst po pozornici.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        public void DrawTextOnScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush = new SolidBrush(Color.WhiteSmoke);
            string text = ISPIS;

            SizeF stringSize = new SizeF();
            Font stringFont = new Font("Arial", 14);
            stringSize = e.Graphics.MeasureString(text, stringFont);

            using (Font font1 = stringFont)
            {
                RectangleF rectF1 = new RectangleF(0, 0, stringSize.Width, stringSize.Height);
                e.Graphics.FillRectangle(brush, Rectangle.Round(rectF1));
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
            }
        }

        private void mouseClicked(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;            
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = false;
            sensing.MouseDown = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            //sensing.MouseX = e.X;
            //sensing.MouseY = e.Y;
            //Sensing.Mouse.x = e.X;
            //Sensing.Mouse.y = e.Y;
            sensing.Mouse.X = e.X;
            sensing.Mouse.Y = e.Y;

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            sensing.Key = e.KeyCode.ToString();
            sensing.KeyPressedTest = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            sensing.Key = "";
            sensing.KeyPressedTest = false;
        }

        private void Update(object sender, EventArgs e)
        {
            if (sensing.KeyPressed(Keys.Escape))
            {
                START = false;
            }

            if (START)
            {
                this.Refresh();
            }
        }

        #endregion
        /* ------------------- */
        #region Start of Game Methods

        //my
        #region my

        //private void StartScriptAndWait(Func<int> scriptName)
        //{
        //    Task t = Task.Factory.StartNew(scriptName);
        //    t.Wait();
        //}

        //private void StartScript(Func<int> scriptName)
        //{
        //    Task t;
        //    t = Task.Factory.StartNew(scriptName);
        //}

        private int AnimateBackground(int intervalMS)
        {
            while (START)
            {
                setBackgroundPicture(backgroundImages[backgroundImageIndex]);
                Game.WaitMS(intervalMS);
                backgroundImageIndex++;
                if (backgroundImageIndex == 3)
                    backgroundImageIndex = 0;
            }
            return 0;
        }

        private void KlikNaZastavicu()
        {
            foreach (Func<int> f in GreenFlagScripts)
            {
                Task.Factory.StartNew(f);
            }
        }

        #endregion

        /// <summary>
        /// BGL
        /// </summary>
        public BGL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Pričekaj (pauza) u sekundama.
        /// </summary>
        /// <example>Pričekaj pola sekunde: <code>Wait(0.5);</code></example>
        /// <param name="sekunde">Realan broj.</param>
        public void Wait(double sekunde)
        {
            int ms = (int)(sekunde * 1000);
            Thread.Sleep(ms);
        }

        //private int SlucajanBroj(int min, int max)
        //{
        //    Random r = new Random();
        //    int br = r.Next(min, max + 1);
        //    return br;
        //}

        /// <summary>
        /// -
        /// </summary>
        public void Init()
        {
            if (dt == null) time = dt.TimeOfDay.ToString();
            loopcount++;
            //Load resources and level here
            this.Paint += new PaintEventHandler(DrawTextOnScreen);
            SetupGame();
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="val">-</param>
        public void showSyncRate(bool val)
        {
            showSync = val;
            if (val == true) syncRate.Show();
            if (val == false) syncRate.Hide();
        }

        /// <summary>
        /// -
        /// </summary>
        public void updateSyncRate()
        {
            if (showSync == true)
            {
                thisTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                diff = thisTime - lastTime;
                lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

                double fr = (1000 / diff) / 1000;

                int fr2 = Convert.ToInt32(fr);

                syncRate.Text = fr2.ToString();
            }

        }

        //stage
        #region Stage

        /// <summary>
        /// Postavi naslov pozornice.
        /// </summary>
        /// <param name="title">tekst koji će se ispisati na vrhu (naslovnoj traci).</param>
        public void SetStageTitle(string title)
        {
            this.Text = title;
        }

        /// <summary>
        /// Postavi boju pozadine.
        /// </summary>
        /// <param name="r">r</param>
        /// <param name="g">g</param>
        /// <param name="b">b</param>
        public void setBackgroundColor(int r, int g, int b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Postavi boju pozornice. <c>Color</c> je ugrađeni tip.
        /// </summary>
        /// <param name="color"></param>
        public void setBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        /// <summary>
        /// Postavi sliku pozornice.
        /// </summary>
        /// <param name="backgroundImage">Naziv (putanja) slike.</param>
        public void setBackgroundPicture(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        /// <summary>
        /// Izgled slike.
        /// </summary>
        /// <param name="layout">none, tile, stretch, center, zoom</param>
        public void setPictureLayout(string layout)
        {
            if (layout.ToLower() == "none") this.BackgroundImageLayout = ImageLayout.None;
            if (layout.ToLower() == "tile") this.BackgroundImageLayout = ImageLayout.Tile;
            if (layout.ToLower() == "stretch") this.BackgroundImageLayout = ImageLayout.Stretch;
            if (layout.ToLower() == "center") this.BackgroundImageLayout = ImageLayout.Center;
            if (layout.ToLower() == "zoom") this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        #endregion

        //sound
        #region sound methods

        /// <summary>
        /// Učitaj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        /// <param name="file">-</param>
        public void loadSound(int soundNum, string file)
        {
            soundCount++;
            sounds[soundNum] = new SoundPlayer(file);
        }

        /// <summary>
        /// Sviraj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        public void playSound(int soundNum)
        {
            sounds[soundNum].Play();
        }

        /// <summary>
        /// loopSound
        /// </summary>
        /// <param name="soundNum">-</param>
        public void loopSound(int soundNum)
        {
            sounds[soundNum].PlayLooping();
        }

        /// <summary>
        /// Zaustavi zvuk.
        /// </summary>
        /// <param name="soundNum">broj</param>
        public void stopSound(int soundNum)
        {
            sounds[soundNum].Stop();
        }

        #endregion

        //file
        #region file methods

        /// <summary>
        /// Otvori datoteku za čitanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToRead(string fileName, int fileNum)
        {
            readFiles[fileNum] = new StreamReader(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToRead(int fileNum)
        {
            readFiles[fileNum].Close();
        }

        /// <summary>
        /// Otvori datoteku za pisanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToWrite(string fileName, int fileNum)
        {
            writeFiles[fileNum] = new StreamWriter(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToWrite(int fileNum)
        {
            writeFiles[fileNum].Close();
        }

        /// <summary>
        /// Zapiši liniju u datoteku.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <param name="line">linija</param>
        public void writeLine(int fileNum, string line)
        {
            writeFiles[fileNum].WriteLine(line);
        }

        /// <summary>
        /// Pročitaj liniju iz datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća pročitanu liniju</returns>
        public string readLine(int fileNum)
        {
            return readFiles[fileNum].ReadLine();
        }

        /// <summary>
        /// Čita sadržaj datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća sadržaj</returns>
        public string readFile(int fileNum)
        {
            return readFiles[fileNum].ReadToEnd();
        }

        #endregion

        //mouse & keys
        #region mouse methods

        /// <summary>
        /// Sakrij strelicu miša.
        /// </summary>
        public void hideMouse()
        {
            Cursor.Hide();
        }

        /// <summary>
        /// Pokaži strelicu miša.
        /// </summary>
        public void showMouse()
        {
            Cursor.Show();
        }

        /// <summary>
        /// Provjerava je li miš pritisnut.
        /// </summary>
        /// <returns>true/false</returns>
        public bool isMousePressed()
        {
            //return sensing.MouseDown;
            return sensing.MouseDown;
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">naziv tipke</param>
        /// <returns></returns>
        public bool isKeyPressed(string key)
        {
            if (sensing.Key == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">tipka</param>
        /// <returns>true/false</returns>
        public bool isKeyPressed(Keys key)
        {
            if (sensing.Key == key.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
        /* ------------------- */

        /*public void CreateHeart()
        {
            heart.Y = 0;
            heart.X = SlucajanBroj(GameOptions.LeftEdge + heart.Width, GameOptions.RightEdge - heart.Width);
        }*/

        /* ------------ GAME CODE START ------------ */

        /* Game variables */
        private delegate void HeartEvent(int x); 

        private static event HeartEvent NewHeart;

        private void MakeHeart(int x)
        {
            //neće je pozvati ako je slučajno već aktivna
            if (heart.Active)
                return;
            heart.GotoXY(x, 0);
            heart.SetVisible(true);
            heart.Active = true;
            Game.StartScript(HeartFall);
        }

        private int HeartFall()
        {
            while (heart.Active)
            {
                heart.MoveSteps();
                Wait(0.01);
            }
            return 0;
        }

        private void ReduceLives()
        {
            trampolin.Lives--;
            ISPIS = "Zivoti: " + trampolin.Lives + "/ Bodovi: " + trampolin.Points;
        }

        private void GiveLife()
        {
            trampolin.Lives++;
            ISPIS = "Zivoti: " + trampolin.Lives + "/ Bodovi: " + trampolin.Points;
        }


        private void EndGame()
        {
            ISPIS = "KRAJ IGRE!: Bodovi:" + trampolin.Points;
            Wait(0.1);
            //pohraniti rezultate u datoteku
            using (StreamWriter sw = File.AppendText(GameOptions.resultsFile))
            {
                
                sw.WriteLine(player + " " + trampolin.Points);
            }

            START = false;

            //this.Close();
            zatvori_bgl();
            //End frmKraj = new End();
            //frmKraj.Show();
        }        

        delegate void forma_delegat();
        //ako se poziva iz skripte/metode koja je pokrenuta kao poseban proces
        //StartScript(...)
        //onda umjesto this.close pozivamo zatvori_bgl
        private void zatvori_bgl()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                forma_delegat d = new forma_delegat(zatvori_bgl);
                this.Invoke(d, new object[] {  });
            }
            else
            {
                this.Close();
            }
        }

        /* Initialization */
        Ball ball;
        Paddle trampolin;
        List<Brick> bricksList;
        Heart heart;
        bool pausePaddle = false;

        private void SetupGame()
        {
            //1. setup stage
            //SetStageTitle("PMF");
            SetStageTitle(this.Player);
            setBackgroundPicture($"backgrounds\\{Background}");
            //none, tile, stretch, center, zoom
            setPictureLayout("stretch");
            START = true;
            //2. dodavanje likova
            ball = new Ball("sprites//silverball_.png", 0, 0);
           // ball.SetTransparentColor(Color.Black);
            ball.SetSize(35);
            
            Game.AddSprite(ball);

            trampolin = new Paddle("sprites//trampolin.png", 0,0);
            trampolin.SetSize(60);
            //trampolin.SetTransparentColor(Color.Black);
            trampolin.AddCostumes("sprites//slow.png");
            Game.AddSprite(trampolin);

            heart=new Heart("sprites//heart.png", 0, 0);
            heart.SetSize(40);
            heart.SetVisible(false);
            //heart.SetTransparentColor(Color.Black);

            Game.AddSprite(heart);
            //prvo punimo listu sa ciglicama, a zatim svaku ubacujemo u Game 
            bricksList = GenerateBricks(GameOptions.brickColumns, GameOptions.brickRows);
            InsertBricks(bricksList);

            trampolin.PaddleEnd += EndGame;
            trampolin.GainLives += GiveLife;
            ball.LoseLives += ReduceLives;
            NewHeart += MakeHeart;

            //3. scripts that start
            Game.StartScript(PaddleMoving);
            Game.StartScript(BallMoving);

        }


        /*Methods*/
       
        //skripta koja se aktivira kada dotaknemo "infected" ciglicu koja ce usporiti kretanje trampolina na odreden period
        private int PaddleInfected()
        {
            
            int secondsToWait = 5;
            if (trampolin.Infected) return 0; //ako je vec usporen,necemo ga dodatno usporavat

            trampolin.Infected = true;
            trampolin.PaddleSpeed -= 7;
            trampolin.NextCostume(); pausePaddle = true;
            
            Wait(secondsToWait);
            
            trampolin.PaddleSpeed += 7;
            trampolin.Infected = false;
            
            trampolin.NextCostume(); pausePaddle = true;
            return 0;
        }
        
        
        public void InsertBricks(List<Brick> bricks)
        {
            foreach (Brick b in bricks)
            {
                Game.AddSprite(b);
            }
        }

        //metoda koja pravi ciglice i vraca listu gotovih cigli
        public List<Brick> GenerateBricks(int brickColumnCount,int brickRowCount)
        {
            List<string> bricksColors = new List<string>()
            {
                "redBrick","blueBrick","greenBrick","purpleBrick"
            }; 

            List<Brick> bricks = new List<Brick>();
            
            int offsetTop = 35;
            int offsetLeft = 55;
            int padding = 5;

            for (int i = 0; i < brickColumnCount; i++)
            {
                for (int j = 0; j < brickRowCount; j++) //napraviti da se mjesano ubacuju slike
                {
                    //ubacujemo različite boje brickova i postavljamo im vrijednosti

                    int randomBr = SlucajanBroj(0,3);
                    string color = bricksColors[randomBr];

                    Brick newBrick = new Brick($"sprites//{bricksColors[randomBr]}.png", 0, 0);
                    newBrick.SetSize(45);

                    //računamo i postavljamo kordinate za svaku ciglu 
                    int brickX = (i *(padding+ newBrick.Width)) + offsetLeft;
                    int brickY = (j * (padding + newBrick.Heigth)) + offsetTop;

                    newBrick.X = brickX;
                    newBrick.Y = brickY;

                    //ova boja cigli ce imati posebne vrijednosti i postavit ćemo joj kostim
                    if (color == "blueBrick")
                    {
                        newBrick.Lives = 2;
                        newBrick.Value = 2;
                        newBrick.AddCostumes("sprites\\blueBrickDamaged.png");
                    }
                    
                    bricks.Add(newBrick);
                }
            }

            //stavljamo "viruse" u nekoliko ciglica koje će prilikom collisiona usporiti kretanje trampolina 
            for (int k = 0; k < 20; k++)
            {
                int randomNumber = SlucajanBroj(0, brickColumnCount * brickRowCount - 1);
                 bricks[randomNumber].Infected = true;
            }
            
            return bricks;
        }
      
        //metoda koja projeverava jeli loptica dotaknula ciglu
        private void BrickCollision() //
        {

            
            foreach (Brick brick in bricksList)
            {

                
                    if (brick.Lives > 0 && ball.TouchingBrick(brick) && brick.Visible)
                    {

                        //pravimo varijable s kojima računamo vektore odbijanja
                        //svi rubovi ciglice
                        int brickLeft = brick.X;
                        int brickRight = brick.X + brick.Width;
                        int brickTop = brick.Y;
                        int brickBottom = brick.Y + brick.Heigth;
                        //svi rubovi kuglice
                        int ballLeft = ball.X;
                        int ballRight = ball.X + ball.Width;
                        int ballTop = ball.Y;
                        int ballBottom = ball.Y + ball.Heigth;

                        bool flipX = (ballRight >= brickLeft || ballLeft <= brickRight);//loptica ide s ljeva na desno ili s desna na ljevo
                        bool flipY = (ballTop + 2 >= brickBottom || ballBottom <= brickTop + 2);// ide od dolje prema gore ili odgore prema dolje

                        if (flipY) ball.ReverseY();
                        else if (flipX) ball.ReverseX();
                        
                        brick.Lives--;
                        ball.BrickHit++;
                        //provjeravamo je li to cigla koja ima više života
                        if (brick.Lives > 0) //to je cigla koja je "tvrđa"
                        {
                            brick.NextCostume();
                            ball.BrickHit--;
                        }
                        else
                        {
                            brick.SetVisible(false);
                            brick.Visible = false;
                        }

                        trampolin.Points += brick.Value;
                        
                        //provjeriti koliko ciglica srušeno i zadovoljava li uvjet za  puštanje itema(srca)
                        if (trampolin.Points % 10 == 0 && trampolin.Points != 0) NewHeart.Invoke(SlucajanBroj(GameOptions.LeftEdge + heart.Width, GameOptions.RightEdge - heart.Width));

                        if (brick.Infected)
                            { Game.StartScript(PaddleInfected);  }

                        if (brick.Lives == 0)
                            bricksList.Remove(brick);
                    
                        break;
                    }
                
            }

            //igra je gotova kada se sve ciglice sruše
            if (ball.BrickHit == GameOptions.brickRows*GameOptions.brickColumns) {
                ISPIS = "POBJEDA!      Zivoti: " + trampolin.Lives + "/ Bodovi: " + trampolin.Points;
                Wait(0.1);
                START = false;
                trampolin.GameEnded = true;
                //EndGame();
            }
            else if(trampolin.Infected) ISPIS = "USPOREN SI! Zivoti: " + trampolin.Lives + "/ Bodovi: " + trampolin.Points; 
            else { ISPIS = "Zivoti: " + trampolin.Lives + "/ Bodovi: " + trampolin.Points; } 
            
            
        }

        private void BGL_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(BGL.allSprites.Count);
            //pozivamo formu za kraj
            START = false;
            Wait(0.2);
            End k = new End();
            k.Show();
        }


        /* Scripts */

        //kretanje trampolina
        private int PaddleMoving()
        {
            trampolin.X = (GameOptions.RightEdge - trampolin.Width) / 2;
            trampolin.Y = GameOptions.DownEdge - trampolin.Heigth;
            while (START) 
            {
                if (sensing.KeyPressed(Keys.Right))
                    trampolin.X += trampolin.PaddleSpeed; //svojstva će se pobrinuti da ne izlazi iz sucelja
                else if (sensing.KeyPressed(Keys.Left))
                    trampolin.X -= trampolin.PaddleSpeed;
                
                
                if (pausePaddle && trampolin.Infected)
                {
                    Wait(0.1);pausePaddle = false;
                }
                if (trampolin.TouchingBall(ball))
                {
                    ball.ReverseY();
                }
                if (trampolin.TouchingHeart(heart))//unutar metode se invoka gainLives 
                {
                    Wait(0.1);
                    ISPIS = "Zivoti: " + trampolin.Lives + "/ Bodovi: " + trampolin.Points;


                    heart.SetVisible(false);
                    heart.Y = 0;
                    heart.Active = false;
                }

                
                Wait(0.02);

            }
            return 0;
        }



        //kretanje loptice
        private int BallMoving()
        {
            ball.X = (GameOptions.RightEdge - ball.Width) / 2;
            ball.Y = (GameOptions.DownEdge - ball.Heigth) / 2 -15;
            Wait(1);
            while (trampolin.Lives>0 && START) 
            {
                BrickCollision();
               // Wait(0.005);

                ball.X += ball.Dx;//dx i dy su 1 jer svaki piksel provjeravamo jeli se loptica sudarila sa ciglicom pa nam to omogucuje preciznost pri racunanju detekcije
                ball.Y += ball.Dy;
                
                Wait(0.0037);

            }
            return 0;
        }

        private int SlucajanBroj(int min, int max)
        {
            Random r = new Random();
            int br = r.Next(min, max + 1);
            return br;
        }

        

        /* ------------ GAME CODE END ------------ */


    }
}
