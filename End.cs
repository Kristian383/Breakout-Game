using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OTTER
{
    public partial class End : Form
    {
        public End()
        {
            InitializeComponent();
        }

        private void btnKraj_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Kraj_Load(object sender, EventArgs e)
        {

            using (StreamReader sr = File.OpenText(GameOptions.resultsFile))
            {
                
                string linija = sr.ReadLine();
                while (linija != null)
                {
                    string[] playerData = linija.Split();
                    string player = playerData[0];
                    int points = int.Parse(playerData[playerData.Length-1]);
                    if (points != 0)
                    {
                        lstResults.Items.Add(linija + "\n");
                    }
                    linija = sr.ReadLine();
                }
                
                
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Start st = new Start(); 
            this.Close();
            st.Show();
        }
        
    }
}
