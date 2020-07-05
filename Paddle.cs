using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public class Paddle:Sprite
    {
       
            private int lives, paddleSpeed, points;
            private bool infected; //abilityReady, 

            public Paddle(string spriteImage, int posX, int posY) : base(spriteImage, posX, posY)
            {
                this.paddleSpeed = 15;
                this.points = 0;
                this.lives = 3;
                this.gameEnded = false;
            this.infected = false;
                //this.abilityReady = false;
            }

            public delegate void EventHandler();
            public event EventHandler PaddleEnd; //događaj za kraj igre
            public event EventHandler GainLives;


            public int Lives
            {
                get { return lives; }
                set
                {
                    lives = value;
                    if (lives <= 0)
                        PaddleEnd.Invoke();
                }
            }

            public int PaddleSpeed
            {
                get { return paddleSpeed; }
                set
                {
                    paddleSpeed = value;

                }
            }

            public bool Infected
            {
                get { return infected; }
                set
                {

                    infected = value;

                }
            }
            public int Points
            {
                get { return points; }
                set
                {
                    points = value;
                }
            }

        public bool GameEnded {
            get { return gameEnded; }
            set
           {
                if (value == true) PaddleEnd.Invoke();
                else gameEnded = value;
            }
        }
        bool gameEnded = false;

        public bool TouchingHeart(Heart h)
            {
                Sprite s = h;
                if (this.TouchingSprite(s))
                {
                    h.Active = false;
                    GainLives.Invoke();
                    return true;
                }
                return false;
            }

            public bool TouchingBall(Ball b)
            {
                Sprite s = b;
                if (this.TouchingSprite(s))
                {
                    return true;
                }
                return false;
            }

        




        }
    
}
