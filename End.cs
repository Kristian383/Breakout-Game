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
        int inputNumberPoints;
        int numberOfRepeating;
        Stopwatch stopWatchBinary;
        Stopwatch stopWatchLinear;
        Stopwatch stopWatchExponential;
        Player[] players;
        string chooseSearch = "iterative";

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

                List<Player> sortedPlayers = igraci.OrderBy(o => o.Points).ToList();
                players = sortedPlayers.ToArray();

                foreach (var item in players)
                {
                    lstResults.Items.Add(item.Name + " " + item.Points + "\n");
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
                inputNumberPoints = int.Parse(textBox1.Text);
                numberOfRepeating = int.Parse(txtBrojPonavljanja.Text);
                if (numberOfRepeating <= 0 || inputNumberPoints < 0) return;
            }
            catch
            {
                return;
            }
            richTextBox1.Text = "Positions of players with " + inputNumberPoints + " points:\n";
            if (rbtBinaryRecursion.Checked) chooseSearch = "binaryRecursion";
            else if (rbtFirstO.Checked) chooseSearch = "firstO";
            else if (rbtLastO.Checked) chooseSearch = "lastO";
            else if (rbtCount.Checked) chooseSearch = "countBs";
            else if (rbtIterative.Checked) chooseSearch = "iterative";

            IspisIndexa(players);
        }

        private void IspisIndexa(Player[] ply)
        {
            //richTextBox2.Text += "Ispis sortiranih elemenata:\n";
            int[] results = new int[ply.Length];
            int poz = 0;
            foreach (var item in ply)
            {
                results[poz++] = item.Points;

            }

            int indBinary = -1;
            int indLinear = -1;
            int indExponential = -1;
            //?********************************************************
            stopWatchBinary = new Stopwatch();
            stopWatchLinear = new Stopwatch();
            stopWatchExponential = new Stopwatch();

            double avgTimeLinear = 0;
            double avgTimeExp = 0;
            double avgTimeBin = 0;

            //mjerenja***************************************************

            for (int o = 0; o < numberOfRepeating; o++)
            {

                //stopWatchLinear.Start();
                stopWatchLinear = Stopwatch.StartNew();
                indLinear = ls.Linear(results, inputNumberPoints);
                stopWatchLinear.Stop();


                //stopWatchExponential.Start();
                stopWatchExponential = Stopwatch.StartNew();
                indExponential = es.Exponential(results, results.Length - 1, inputNumberPoints);
                stopWatchExponential.Stop();

                switch (chooseSearch)
                {
                    case "firstO":
                        stopWatchBinary = Stopwatch.StartNew();
                        //stopWatchBinary.Start();
                        indBinary = bs.FirstOccurrence(results, 0, results.Length - 1, inputNumberPoints);
                        stopWatchBinary.Stop();
                        break;
                    case "lastO":
                        //stopWatchBinary.Start();
                        stopWatchBinary = Stopwatch.StartNew();
                        indBinary = bs.LastOccurrence(results, 0, results.Length - 1, inputNumberPoints);
                        stopWatchBinary.Stop();
                        break;
                    case "countBs":
                        //stopWatchBinary.Start();
                        stopWatchBinary = Stopwatch.StartNew();
                        indBinary = bs.Count(results, inputNumberPoints, results.Length);
                        stopWatchBinary.Stop();
                        break;
                    case "binaryRecursion":
                        //stopWatchBinary.Start();
                        stopWatchBinary = Stopwatch.StartNew();
                        indBinary = bs.BinaryRecursion(results, 0, results.Length - 1, inputNumberPoints);
                        stopWatchBinary.Stop();
                        break;
                    case "iterative":
                        //stopWatchBinary.Start();
                        stopWatchBinary = Stopwatch.StartNew();
                        indBinary = bs.BinaryIterative(results, 0, results.Length - 1, inputNumberPoints);
                        stopWatchBinary.Stop();
                        break;
                }



                TimeSpan tsLin = stopWatchLinear.Elapsed;
                TimeSpan tsBin = stopWatchBinary.Elapsed;
                TimeSpan tsExp = stopWatchExponential.Elapsed;
                double milibin = tsBin.TotalMilliseconds;
                double mililin = tsLin.TotalMilliseconds;

                avgTimeLinear += tsLin.TotalMilliseconds;

                avgTimeBin += tsBin.TotalMilliseconds;
                avgTimeExp += tsExp.TotalMilliseconds;
            }
            //**********************************************************
            string igracB, igracL, igracE;
            igracB = igracL = igracE = "";

            if (chooseSearch == "countBs")
            {
                int startInd = bs.FirstOccurrence(results, 0, results.Length - 1, inputNumberPoints);
                int lastInd = bs.LastOccurrence(results, 0, results.Length - 1, inputNumberPoints);
                string igraciIstihBodova = "";

                richTextBox1.Text += "Binary count: " + indBinary + "\n" + "Players: ";

                if (startInd != -1 && lastInd != -1)
                {
                    for (int u = startInd; u <= lastInd; u++)
                    {
                        igracB = ply[u].Name;
                        igraciIstihBodova += igracB + ", ";
                    }
                    richTextBox1.Text += igraciIstihBodova;
                }
                else richTextBox1.Text += "not found";

                richTextBox1.Text += "\n\n";
            }
            else
            {
                igracB = indBinary == -1 ? "not found" : ply[indBinary].Name;
                richTextBox1.Text += "Binary index: " + indBinary + $"   Player: {igracB}\n\n";
            }

            igracE = indExponential == -1 ? "not found" : ply[indExponential].Name;
            igracL = indLinear == -1 ? "not found" : ply[indLinear].Name;

            richTextBox1.Text += "Linear index: " + indLinear + $"   Player: {igracL}\n\n";
            richTextBox1.Text += "Exponential index: " + indExponential + $"   Player: {igracE}\n\n";


            double vr1 = (avgTimeBin / numberOfRepeating);
            double vr2 = (avgTimeLinear / numberOfRepeating);
            double vr3 = (avgTimeExp / numberOfRepeating);

            richTextBox1.AppendText("Time Binary: " + String.Format("{0:0.00000000}", vr1) + "\n\n");
            richTextBox1.AppendText("Time Linear: " + String.Format("{0:0.00000000}", vr2) + "\n\n");
            richTextBox1.AppendText("Time Exponential: " + String.Format("{0:0.00000000}", vr3) + "\n\n");

            //?********************************************************

        }
    
    }
}
