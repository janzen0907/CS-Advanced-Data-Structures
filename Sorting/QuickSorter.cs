using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting;

public class QuickSorter<T> : A_Sorter<T> where T : IComparable<T>
{
    public QuickSorter(T[] array) : base(array)
    {
    }

    public override void Sort()
    {
        recQuickSort(0, Array.Length - 1);
    }

    /// <summary>
    /// Quicksorts a section of the array
    /// </summary>
    /// <param name="first">Index for the start of the array section to be sorted</param>
    /// <param name="last">Index for the end of the array section to be sorted</param>
    private void recQuickSort(int first, int last)
    {

        // Base case is having only 1 element in this section of the array
        //    and in that case, we do nothing, it is already sorted

        // Recursive case is 2 or more items
        if (last - first > 0)
        {
            //Select a pivot value and find its location
            int pivotValue = FindPivot(first, last);
            //     Swap the pivot value to the end of the array section we are quicksorting
            Swap(pivotValue, last);
            //     Partition the array section relative to the pivot value
            int partitionIndex = partition(first, last - 1, Array[last]);
            //     After we've partioned, we know where the middle point is, which should
            //         contain a value greater than the pivot. So we can swap the pivot into there.
            Swap(partitionIndex, last);
            //If we've got a lot of items still, sort each side in parallel
            if (last - first > 1000)
            {
                //Multi-Threading to make the sort more efficent
                Parallel.Invoke(
                    () => recQuickSort(first, partitionIndex - 1),
                    () => recQuickSort(partitionIndex + 1, last));
            } else
            {
                //     Quicksort on the left
                recQuickSort(first, partitionIndex - 1);
                //     Quicksort on the right
                recQuickSort(partitionIndex + 1, last);

            }
            
            
        }
        
    }

    /// <summary>
    /// Given a section of the array, return the index of a value to use as the pivot
    /// 
    /// </summary>
    /// <param name="first">First index in the array section we're finding a pivot in</param>
    /// <param name="last">The last index in teh array section</param>
    /// <returns></returns>
    protected virtual int FindPivot(int first, int last)
    {

        return last;

    }

    /// <summary>
    /// Partitions the array section defined by left and right
    /// </summary>
    /// <param name="left">the index of the start of the array section</param>
    /// <param name="right">One</param>
    /// <param name="PivotValue"></param>
    /// <returns></returns>
    public int partition(int left, int right, T PivotValue)
    {
            left--;
            right++;
            // Until the pointers meet (while the left pointer is less than the right pointer
            do
            {
                //      Move the left pointer right until we find a value greater than the pivot
                while (Array[++left].CompareTo(PivotValue) < 0) ;
                //      Move the right pointer left until we either hit the left pointer,
                //         OR we find a value less than the pivot
                while (right > left && Array[--right].CompareTo(PivotValue) > 0) ;
                //      Swap around the two values
                Swap(left, right);


            } while (left < right);

            // Once we're done - once the pointers have crossed over - return the index
            // of the left pointer
            return left;
        }
    }

