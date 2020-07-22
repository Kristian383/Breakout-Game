using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    class BinarySearch
    {
        public int BinaryRecursion(int[] arr, int low,
                            int high, int key)
        {
            if (high >= low)
            {
                //int mid =  (high - low) / 2;
                int mid = low + (high - low) / 2;

                if (arr[mid] == key)
                    return mid;
                if (arr[mid] > key)//ako je element manji od mid, može se nalaziti jedino s ljeve strane te mid excludamo
                    return BinaryRecursion(arr, low, mid - 1, key);

                //u protivnom se jedino moze nalaziti s desne strane
                return BinaryRecursion(arr, mid + 1, high, key);
            }

            //element nije prisutan u listi
            return -1;
        }

        public int BinaryIterative(int[] arr, int low, int high, int key)
        {

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                if (arr[mid] == key) return mid;

                if (arr[mid] < key) low = mid + 1; //gledamo desnu polovicu liste
                else high = mid - 1;

            }
            return -1;
        }

        public int FirstOccurrence(int[] arr, int low, int high, int key)
        {
            int ans = -1;

            while (low <= high)
            {
                int mid = low + (high - low + 1) / 2;
                int midVal = arr[mid];

                if (midVal < key)
                {

                    // if mid is less than key, all elements 
                    // in range [low, mid] are also less 
                    // so we now search in [mid + 1, high] 
                    low = mid + 1;
                }
                else if (midVal > key)
                {

                    // if mid is greater than key, all elements  
                    // in range [mid + 1, high] are also greater 
                    // so we now search in [low, mid - 1] 
                    high = mid - 1;
                }
                else if (midVal == key)
                {

                    // if mid is equal to key, we note down 
                    //  the last found index then we search  
                    // for more in left side of mid 
                    // so we now search in [low, mid - 1] 
                    ans = mid;
                    high = mid - 1;
                }
            }

            return ans;
        }

        public int LastOccurrence(int[] arr, int beg, int end,
                                     int x)
        {
            int ans = -1;
            while (beg <= end)
            {
                int mid = (beg + (end - beg + 1) / 2);
                int midVal = arr[mid];

                if (midVal < x) beg = mid + 1;
                else if (midVal > x) end = mid - 1;

                else if (midVal == x)
                {
                    ans = mid;
                    beg = mid + 1; //donja granica jer gledamo desnu stranu
                }
            }
            return ans;
        }



        public int Count(int[] arr, int x, int n)
        {
            // index of first occurrence of x in arr[0..n-1]  
            int i;

            // index of last occurrence of x in arr[0..n-1] 
            int j;

            /* get the index of first occurrence of x */
            i = this.FirstOccurrence(arr, 0, n - 1, x);

            /* If x doesn't exist in arr[] then return -1 */
            if (i == -1)
                return i;

            /* Else get the index of last occurrence of x.  
                Note that we are only looking in the  
                subarray after first occurrence */
            j = this.LastOccurrence(arr, i, n - 1, x);

            /* return count */
            return j - i + 1;
        }
    }
}
