using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting;

public class BubbleSort<T> : A_Sorter<T> where T : IComparable<T>
{
    public BubbleSort(T[] array) : base(array)
    {
    }

    public override void Sort()
    {
        //get the length of the array
        bool sorted = false;
        int arrayLength = Array.Length - 1;

        while(!sorted)
        {
            sorted = true;
            for(int i = 0; i < arrayLength; i++)
            {
                if (Array[i].CompareTo(Array[i+1]) > 0)
                {
                    sorted = false;
                    Swap(i, i + 1); 
                }
            }
            //Optimiation for the bubble sort. forget about the last element as its
            //Already sorted
            arrayLength--;
        } 
    }
}
