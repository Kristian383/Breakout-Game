using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public class Brick : Sprite
    {
        private int lives, value;
        private bool visible, infected;//infected ce oznacavati je li cigla usporava ili ubrzava kretanje trampolina

        public Brick(string spriteImage, int posX, int posY) : base(spriteImage, posX, posY)
        {
            this.lives = 1;
            this.value = 1;
            this.visible = true;
            this.infected = false;
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public bool Infected
        {
            get { return infected; }
            set { infected = value; }
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
