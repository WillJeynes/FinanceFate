using System;
using System.Collections.Generic;
using System.Collections;

namespace FinanceFate.Algorithms
{
    class MergeSort
    {
        //Main starting function
        public static List<T> Sort<T>(List<T> main)
        {
            //Get the size
            int size = main.Count;

            //return to main the output of the recusive subroutine mergeSort
            return (mergeSort(main, 0, size - 1));

        }
        //recursive subroutine to divide and merge the data
        static List<T> mergeSort<T>(List<T> main, int start, int end)
        {
            if (start < end)
            {
                //get the midpoint to split down
                int mid = start + (end - start) / 2;

                //split each half, then call this subroutine to split each of the halves, creating recursion.
                main = mergeSort(main, start, mid);
                main = mergeSort(main, mid + 1, end);

                //merge split halves together
                main = merge(main, start, mid, end);
            }
            //pass the modified list back to the calling subroutine
            return main;
        }
        //main subroutine to merge two previously split halves of a list
        static List<T> merge<T>(List<T> main, int start, int mid, int end)
        {
            //get the lengths of each of the respective halves
            int l1 = (mid - start) + 1;
            int l2 = end - mid;

            //create holding arrays for the two lists
            T[] left = new T[l1];
            T[] right = new T[l2];

            //popoulate the left list with half of the values
            for (int i = 0; i < l1; i++)
                left[i] = main[start + i];
            //populate the right list with the other half of the values
            for (int j = 0; j < l2; j++)
                right[j] = main[mid + 1 + j];

            //counter initialisation
            int ii = 0;
            int ij = 0;
            int k = start;

            //loop until all of the values of one of the lists has been used
            while (ii < l1 && ij < l2)
            {
                //compare a value on the left with the corresponding value on the right
                if (GreaterEqual(left[ii], right[ij]))
                {
                    //if the value on the left is smaller add it to the list
                    main[k] = left[ii];
                    ii++;
                }
                else
                {
                    //if the value on the right is smaller add it to the list
                    main[k] = right[ij];
                    ij++;
                }
                k++;
            }

            //once all of one of the lists has been used the remaining unused values from the other list must be added to the main array to complete the sort
            //loop for all of the remaining values of the left list
            while (ii < l1)
            {
                //add value to main list
                main[k] = left[ii];
                ii++;
                k++;
            }
            //loop for all of the remaining values of the right list
            while (ij < l2)
            {
                //add value to main list
                main[k] = right[ij];
                ij++;
                k++;
            }

            return main;
        }
        //custom comparer function that works for a range of variable types
        static bool GreaterEqual<T>(T a, T b)
        {
            //default instance of the System.Collections.Comparer class
            int n = Comparer.DefaultInvariant.Compare(a, b);
            //if (a < b), n < 0
            //if (a == b), n = 1
            //if (a > b), n > 0

            //compares the output value to 0, to work as a greater than or equal to comparison
            return (n <= 0);
        }
    }
}
