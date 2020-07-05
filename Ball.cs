using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public class Ball : Sprite
    {
        private int dx, dy, brickHit; //preko varijable brickHit provjeravamo jesu li sve ciglice srušene 
        public Ball(string spriteImage, int posX, int posY) : base(spriteImage, posX, posY)
        {
            dx = 1;
            dy = 1;
            brickHit = 0;
        }

        public delegate void EventHandler();
        public event EventHandler LoseLives;   //preko ovoga cemo u BGL smanjivat zivote trampolinu kada loptica dotakne dno

        public int Dy
        {
            get { return dy; }
            set { dy = value; }
        }
        public int Dx
        {
            get { return dx; }
            set { dx = value; }
        }

        public override int X
        {
            get { return x; }
            set
            {
                if (value + this.Width > GameOptions.RightEdge) this.Dx *= -1;
                else if (value < GameOptions.LeftEdge) this.Dx *= -1;
                else this.x = value;
            }
        }

        public override int Y
        {
            get { return y; }
            set
            {
                if (value < GameOptions.UpEdge) this.Dy *= -1;
                else if (value > GameOptions.DownEdge)
                {
                    LoseLives.Invoke();
                    //centriramo loptu
                    this.y = (GameOptions.DownEdge - this.Width) / 2;
                    this.x = (GameOptions.RightEdge - this.Width) / 2;
                    Game.WaitMS(500); //pauziramo lopticu da se stignemo pripremiti za novi pokusaj
                }
                else
                    this.y = value;
            }
        }

        public void ReverseY()
        {
            this.Dy *= -1;
        }

        public void ReverseX()
        {
            this.Dx *= -1;
        }

        public bool TouchingBrick(Brick br)
        {
            Sprite s = br;
            if (this.TouchingSprite(s))
            {
                return true;
            }
            return false;
        }

        public int BrickHit
        {
            get { return brickHit; }
            set { brickHit = value; }
        }
    }
}
