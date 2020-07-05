using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public abstract class Abilities : Sprite
    {
        public Abilities(string spriteImage, int posX, int posY)
    : base(spriteImage, posX, posY)
        {
            Active = false;
            this.brzina = 2;
        }
        public bool Active { get; set; }

        private int bodoviVrijednost;
        public int BodoviVrijednost
        {
            get { return bodoviVrijednost; }
            set { bodoviVrijednost = value; }
        }

        private int brzina;

        public int Brzina
        {
            get { return brzina; }
            set { brzina = value; }
        }

        public override int Y
        {
            get { return base.Y; }
            set
            {
                if (value > GameOptions.DownEdge)
                {
                    this.y = 0;
                    this.Active = false;
                    this.SetVisible(false);
                }
                else
                    base.Y = value;
            }
        }

        public void MoveSteps()
        {
            this.Y += this.Brzina;
        }
    }

    public class Heart : Abilities
    {
        public Heart(string spriteImage, int posX, int posY)
    : base(spriteImage, posX, posY)
        {
            this.lives = 1;
        }

        private int lives;

        public int Lives
        {

            get { return lives; }
            set { lives = value; }
        }
    }
}
