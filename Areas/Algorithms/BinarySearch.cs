using System;
using System.Collections.Generic;
using System.Collections;

namespace FinanceFate.Algorithms
{
    class BinarySearch
    {
        //Main search function
        public static int Search<T>(List<T> main, T value)
        {
            //get array length, lowest point, and highest point
            int n = main.Count;
            int lower = 0;
            int higher = n-1;

            //variable to store found location
            int loc = -1;

            while (true)
            {
                //if the search window is 0 or less break loop since required value is not in the array
                if (higher < lower)
                    break;

                //calculate the mid point
                int mid = lower + (higher - lower) / 2;

                //compare the value at the midpoint to the searched for value
                if (Greater(main[mid], value))
                    //if the value is greater, set the lowest search extent to the midpoint
                    lower = mid + 1;
                else if (Less(main[mid], value))
                    //if the value is smaller, set the highest search extent to the midpoint
                    higher = mid - 1;
                else
                {
                    //if they are equal the value has been found
                    //set location variable to the midpoint pointer
                    loc = mid;
                    //break the loop
                    break;
                }
            }
            //return the found item index
            return loc;
        }
        //custom comparer functions that work for a range of variable types
        //custom greater than
        static bool Greater<T>(T a, T b)
        {
            //default instance of the System.Collections.Comparer class
            int n = Comparer.DefaultInvariant.Compare(a, b);
            //if (a < b), n < 0
            //if (a == b), n = 1
            //if (a > b), n > 0

            //compares the output value to 0, to work as a greater than comparison
            return (n < 0);
        }
        //current less than
        static bool Less<T>(T a, T b)
        {
            //default instance of the System.Collections.Comparer class
            int n = Comparer.DefaultInvariant.Compare(a, b);
            //if (a < b), n < 0
            //if (a == b), n = 1
            //if (a > b), n > 0

            //compares the output value to 0, to work as a less than comparison
            return (n > 0);
        }
    }
}
