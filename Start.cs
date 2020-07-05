using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OTTER
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

       
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private string playerName ;

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            GameOptions.kraj = false;
            if (txtPlayerName.Text == "")
            {
                MessageBox.Show("Upišite ime!");
                return;
            }
           
            BGL igra = new BGL();

            if (rbtNature.Checked) igra.Background = "nature.jpg";
            else if (rbtSavana.Checked) igra.Background = "savana.jpg";
            else igra.Background = "default.jpg";
            
            playerName = txtPlayerName.Text;

            igra.frmStart = this;
            igra.Player = txtPlayerName.Text;
            

            igra.ShowDialog();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            RemoveSprites();
        }
        private void RemoveSprites()
        {
            //vrati brojač na 0
            BGL.spriteCount = 0;
            //izbriši sve spriteove
            BGL.allSprites.Clear();
            //počisti memoriju
            GC.Collect();
        }
    }
}
