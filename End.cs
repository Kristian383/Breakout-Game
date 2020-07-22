using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

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

        BinarySearch bs = new BinarySearch();
        ExponentialSearch es = new ExponentialSearch();
        LinearSearch ls = new LinearSearch();
        int trazeniBroj;
        int brojPonavljanja;
        Stopwatch stopWatchBinary;
        Stopwatch stopWatchLinear;
        Stopwatch stopWatchExponential;
        Player[] players;
        string odabirSearcha = "iterative";

        private void Kraj_Load(object sender, EventArgs e)
        {

            using (StreamReader sr = File.OpenText(GameOptions.resultsFile))
            {
                List<Player> igraci = new List<Player>();
                string linija = sr.ReadLine();
                while (linija != null)
                {
                    string[] playerData = linija.Split();
                    string player = playerData[0];
                    int points = int.Parse(playerData[playerData.Length - 1]);
                    if (points != 0)
                    {
                        //lstResults.Items.Add(linija + "\n");
                        Player pl = new Player(player, points);
                        igraci.Add(pl);
                    }
                    linija = sr.ReadLine();
                }

                List<Player> sortirani = igraci.OrderBy(o => o.Bodovi).ToList();
                players = sortirani.ToArray();

                foreach (var item in players)
                {
                    lstResults.Items.Add(item.Ime + " " + item.Bodovi + "\n");
                }

            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Start st = new Start(); 
            this.Close();
            st.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                trazeniBroj = int.Parse(textBox1.Text);
                brojPonavljanja = int.Parse(txtBrojPonavljanja.Text);
                if (brojPonavljanja <= 0 || trazeniBroj < 0) return;
            }
            catch
            {
                return;
            }
            richTextBox1.Text = "Ispis pozicije igrača sa " + trazeniBroj + " pointsa:\n";
            if (rbtBinaryRecursion.Checked) odabirSearcha = "binaryRecursion";
            else if (rbtFirstO.Checked) odabirSearcha = "firstO";
            else if (rbtLastO.Checked) odabirSearcha = "lastO";
            else if (rbtCount.Checked) odabirSearcha = "countBs";
            else if (rbtIterative.Checked) odabirSearcha = "iterative";

            IspisIndexa(players);
        }

        private void IspisIndexa(Player[] ply)
        {
            //richTextBox2.Text += "Ispis sortiranih elemenata:\n";
            int[] rezultati = new int[ply.Length];
            int poz = 0;
            foreach (var item in ply)
            {
                rezultati[poz++] = item.Bodovi;

            }

            int indBinary = -1;
            int indLinear = -1;
            int indExponential = -1;
            //?********************************************************
            stopWatchBinary = new Stopwatch();
            stopWatchLinear = new Stopwatch();
            stopWatchExponential = new Stopwatch();

            double srednjaVrijednostLin = 0;
            double srednjaVrijednostExp = 0;
            double srednjaVrijednostBin = 0;

            //mjerenja***************************************************

            for (int o = 0; o < brojPonavljanja; o++)
            {

                //stopWatchLinear.Start();
                stopWatchLinear = Stopwatch.StartNew();
                indLinear = ls.Linear(rezultati, trazeniBroj);
                stopWatchLinear.Stop();


                //stopWatchExponential.Start();
                stopWatchExponential = Stopwatch.StartNew();
                indExponential = es.Exponential(rezultati, rezultati.Length - 1, trazeniBroj);
                stopWatchExponential.Stop();

                switch (odabirSearcha)
                {
                    case "firstO":
                        stopWatchBinary = Stopwatch.StartNew();
                        //stopWatchBinary.Start();
                        indBinary = bs.FirstOccurrence(rezultati, 0, rezultati.Length - 1, trazeniBroj);
                        stopWatchBinary.Stop();
                        break;
                    case "lastO":
                        //stopWatchBinary.Start();
                        stopWatchBinary = Stopwatch.StartNew();
                        indBinary = bs.LastOccurrence(rezultati, 0, rezultati.Length - 1, trazeniBroj);
                        stopWatchBinary.Stop();
                        break;
                    case "countBs":
                        //stopWatchBinary.Start();
                        stopWatchBinary = Stopwatch.StartNew();
                        indBinary = bs.Count(rezultati, trazeniBroj, rezultati.Length);
                        stopWatchBinary.Stop();
                        break;
                    case "binaryRecursion":
                        //stopWatchBinary.Start();
                        stopWatchBinary = Stopwatch.StartNew();
                        indBinary = bs.BinaryRecursion(rezultati, 0, rezultati.Length - 1, trazeniBroj);
                        stopWatchBinary.Stop();
                        break;
                    case "iterative":
                        //stopWatchBinary.Start();
                        stopWatchBinary = Stopwatch.StartNew();
                        indBinary = bs.BinaryIterative(rezultati, 0, rezultati.Length - 1, trazeniBroj);
                        stopWatchBinary.Stop();
                        break;
                }



                TimeSpan tsLin = stopWatchLinear.Elapsed;
                TimeSpan tsBin = stopWatchBinary.Elapsed;
                TimeSpan tsExp = stopWatchExponential.Elapsed;
                double milibin = tsBin.TotalMilliseconds;
                double mililin = tsLin.TotalMilliseconds;

                srednjaVrijednostLin += tsLin.TotalMilliseconds;

                srednjaVrijednostBin += tsBin.TotalMilliseconds;
                srednjaVrijednostExp += tsExp.TotalMilliseconds;
            }
            //**********************************************************
            string igracB, igracL, igracE;
            igracB = igracL = igracE = "";

            if (odabirSearcha == "countBs")
            {
                int pocetniInd = bs.FirstOccurrence(rezultati, 0, rezultati.Length - 1, trazeniBroj);
                int zadnjiInd = bs.LastOccurrence(rezultati, 0, rezultati.Length - 1, trazeniBroj);
                string igraciIstihBodova = "";

                richTextBox1.Text += "Binary count: " + indBinary + "\n" + "Igraci: ";

                if (pocetniInd != -1 && zadnjiInd != -1)
                {
                    for (int u = pocetniInd; u <= zadnjiInd; u++)
                    {
                        igracB = ply[u].Ime;
                        igraciIstihBodova += igracB + ", ";
                    }
                    richTextBox1.Text += igraciIstihBodova;
                }
                else richTextBox1.Text += "nema";

                richTextBox1.Text += "\n\n";
            }
            else
            {
                igracB = indBinary == -1 ? "nema" : ply[indBinary].Ime;
                richTextBox1.Text += "Binary index: " + indBinary + $"   Igrač: {igracB}\n\n";
            }

            igracE = indExponential == -1 ? "nema" : ply[indExponential].Ime;
            igracL = indLinear == -1 ? "nema" : ply[indLinear].Ime;

            richTextBox1.Text += "Linear index: " + indLinear + $"   Igrač: {igracL}\n\n";
            richTextBox1.Text += "Exponential index: " + indExponential + $"   Igrač: {igracE}\n\n";


            double vr1 = (srednjaVrijednostBin / brojPonavljanja);
            double vr2 = (srednjaVrijednostLin / brojPonavljanja);
            double vr3 = (srednjaVrijednostExp / brojPonavljanja);

            richTextBox1.AppendText("Vrijeme Binary: " + String.Format("{0:0.00000000}", vr1) + "\n\n");
            richTextBox1.AppendText("Vrijeme Linear: " + String.Format("{0:0.00000000}", vr2) + "\n\n");
            richTextBox1.AppendText("Vrijeme Exponential: " + String.Format("{0:0.00000000}", vr3) + "\n\n");

            //?********************************************************

        }
    
    }
}
