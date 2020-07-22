using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    class ExponentialSearch
    {
        
            BinarySearch bs = new BinarySearch();
            public int Exponential(int[] arr, int n, int x)
            {

                // If x is present at  
                // first location itself 
                if (arr[0] == x)
                    return 0;

                // Find range for binary search  
                // by repeated doubling 
                int i = 1;
                while (i < n && arr[i] <= x)
                    i = i * 2;

                // Call binary search for the found range. 
                // i/2 je prethodna vrijednost, a ukoliko je "i" prešao veličinu liste, uzimamo n kao gornju granicu
                return bs.BinaryIterative(arr, i / 2, Math.Min(i, n), x);
            }
        
    }
}
