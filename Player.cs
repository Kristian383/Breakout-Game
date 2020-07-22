using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    class Player
    {
        int points;
        string name;
        public Player(string i, int b)
        {
            Name = i;
            Points = b;
        }

        public int Points { get => points; set => points = value; }
        public string Name { get => name; set => name = value; }

        public Player[] SortPlayers(Player[] plyrs)
        {
            int n = plyrs.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = plyrs[i].Points;
                int j = i - 1;

                // Move elements of arr[0..i-1], 
                // that are greater than key, 
                // to one position ahead of 
                // their current position 
                while (j >= 0 && plyrs[j].Points > key)
                {
                    plyrs[j + 1].Points = plyrs[j].Points;
                    j = j - 1;
                }
                plyrs[j + 1].Points = key;
            }

            return plyrs;
        }
    }
}
