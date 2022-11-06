using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace FinanceFate.Algorithms
{
    public class Math2
    {
        public static T Clamp<T>(T value, T start, T end)
        {
            //Clamp for generic type T
            //Replacment for the Clamp function in Mathf
            if (Greater(value, end))
                return end;
            else if (Less(value, start))
                return end;
            else
                return value;
        }
        static bool Greater<T>(T a, T b)
        {
            //default instance of the System.Collections.Comparer class
            int Compn = Comparer.DefaultInvariant.Compare(a, b);
            //if (a < b), n < 0
            //if (a == b), n = 1
            //if (a > b), n > 0

            //compares the output value to 0, to work as a greater than comparison
            return (Compn < 0);
        }
        //current less than
        static bool Less<T>(T a, T b)
        {
            //default instance of the System.Collections.Comparer class
            int Compn = Comparer.DefaultInvariant.Compare(a, b);
            //if (a < b), n < 0
            //if (a == b), n = 1
            //if (a > b), n > 0

            //compares the output value to 0, to work as a less than comparison
            return (Compn > 0);
        }
    }
}