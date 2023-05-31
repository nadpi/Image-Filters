using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;


namespace ImageFilters
{
    static class SortHelper
    {
        public static void swap(int First_index, int Last_index, byte[] UnsortedArr)
        {
            byte temp = 0;
            temp = UnsortedArr[First_index];
            UnsortedArr[First_index] = UnsortedArr[Last_index];
            UnsortedArr[Last_index] = temp;
        }

        public static int partitioning(int First_index, int Last_index, ref byte[] UnsortedArr)
        {
            int i = First_index;
            int j = Last_index;
            byte pivot = UnsortedArr[First_index];
            while (i < j) 
            {
                do
                {
                    i++;
                } while (UnsortedArr[i] <= pivot && i < (Last_index - 1));
                do
                {
                    j--;

                } while (UnsortedArr[j] > pivot);
                if (i < j)
                {
                    swap(i, j, UnsortedArr);
                }
            }
            swap(First_index, j, UnsortedArr);
            return j;
        }
        public static byte[] Kth_element(byte[] Array, int t, int size)
        {
            //TODO: Implement Kth smallest/largest element
            
            //Exception handling
            if ((t * 2) > size)
            {
                t = size;
            }

            List<byte> ls = new List<byte>();
            byte value;
            for (int j = 0; j < Array.Length; j++)
            {
                value = Array[j];
                ls.Add(value);
            }



            while (t > 0)
            {
                //Search the input array for the MIN and MAX elements without sorting
                byte min = ls.Min();
                byte max = ls.Max();
                ls.RemoveAt(ls.IndexOf(min));
                ls.RemoveAt(ls.IndexOf(max));
                t--;
            }


            byte[] AfterTrim = ls.ToArray();

            return AfterTrim;


            // Remove the next line
            //throw new NotImplementedException();
        }

        public static byte[] CountingSort(byte[] unsortedArray)
        {
            // TODO: Implement the Counting Sort alogrithm on the input array
            int unsortedLength = unsortedArray.Length;

            //Declare arrays
            int[] counterArray = new int[256];
            byte[] sortedArray = new byte[unsortedLength];

            //Initialize Count to zeros
            for (int i = 0; i < 256; i++)
            {
                counterArray[i] = 0;
            }

            //Fill CounterArrray
            for (int i = 0; i < 256; i++)
            {
                if (i >= unsortedLength)
                {
                    break;
                }
                counterArray[unsortedArray[i]] += 1;

            }

            int currentVal = 0;
            int repeatNum = 0;
            int sortedIndex = 0;
            //Filling sorted array
            for (int i = 0; i < 256; i++)
            {
                repeatNum = counterArray[i];

                sortedIndex = currentVal;


                for (int j = repeatNum; j > 0; j--)
                {
                    sortedArray[sortedIndex] = (byte)i;
                    sortedIndex++;
                    currentVal = sortedIndex;
                }

            }

            // Remove the next line
            //throw new NotImplementedException();
            return sortedArray;
        }

        public static void QuickSort(int First_index, int Last_index, ref byte[] UnsortedArr)
        {
            // TODO: Implement the Quick Sort alogrithm on the input array
            if (First_index < (Last_index - 1))
            {
                int j = partitioning(First_index, Last_index, ref UnsortedArr);
                QuickSort(First_index, j, ref UnsortedArr);
                QuickSort(j + 1, Last_index, ref UnsortedArr);

                //throw new NotImplementedException();
            }
        }


    }
}