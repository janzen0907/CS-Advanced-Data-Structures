using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting;

public class BubbleSortPrac<T> : A_Sorter<T> where T : IComparable<T>
{
    public BubbleSortPrac(T[] array) : base(array)
    {
    }

    public override void Sort()
    {
        Boolean isSorted = false;
        int arrayLength = Array.Length - 1;
        while(!isSorted)
        {
            isSorted = true;
            for(int i = 1; i <arrayLength; i++)
            {
                if (Array[i].CompareTo(Array[i + 1]) > 0)
                {
                    isSorted = false;
                    Swap(i, i + 1);
                }
              
            }
            arrayLength--;
        }
    }
}


