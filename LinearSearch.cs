using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    class LinearSearch
    {
        public int Linear(int[] arr, int x)
        {
            int n = arr.Length;
            for (int i = 0; i < n; i++)
            {
                if (arr[i] == x)
                    return i;
            }
            return -1;
        }
    }
}
