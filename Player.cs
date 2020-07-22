using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    class Player
    {
        int bodovi;
        string ime;
        public Player(string i, int b)
        {
            Ime = i;
            Bodovi = b;
        }

        public int Bodovi { get => bodovi; set => bodovi = value; }
        public string Ime { get => ime; set => ime = value; }

        public Player[] Sortiraj(Player[] plyrs)
        {
            int n = plyrs.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = plyrs[i].Bodovi;
                int j = i - 1;

                // Move elements of arr[0..i-1], 
                // that are greater than key, 
                // to one position ahead of 
                // their current position 
                while (j >= 0 && plyrs[j].Bodovi > key)
                {
                    plyrs[j + 1].Bodovi = plyrs[j].Bodovi;
                    j = j - 1;
                }
                plyrs[j + 1].Bodovi = key;
            }

            return plyrs;
        }
    }
}
