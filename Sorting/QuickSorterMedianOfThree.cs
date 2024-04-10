using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting;

public class QuickSorterMedianOfThree<T> : QuickSorter<T> where T : IComparable<T>

{
    public QuickSorterMedianOfThree(T[] array) : base(array)
    {
    }

    protected override int FindPivot(int first, int last)
    {
        //Look at three values - the first value in the Array
        
        //The last value in the array section, and a value approximately halfway
        int middleIndex = (first + last) / 2;
        if (Array[first].CompareTo(Array[middleIndex]) > 0)
        {
            Swap(first, middleIndex);
        }
        if (Array[first].CompareTo(Array[last]) > 0)
        {
            Swap(first, last);
        }
        if (Array[middleIndex].CompareTo(Array[last]) > 0)
        {
            Swap(middleIndex, last);
        }
        
        return middleIndex;
        //Return the index of teh three that is inbetween the other two


    }
}
