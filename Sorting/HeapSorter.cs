using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting;

public class HeapSorter<T> : A_Sorter<T> where T : IComparable<T>
{
    public HeapSorter(T[] array) : base(array)
    {
    }

    public override void Sort()
    {

        //First Heapify the array
        Heapify();
        //I got stuck on not knowing how to heapify the array :
        int sortedIndex = Array.Length - 1;
        int unsortedIndex = 0;
        //Then, starting at the end of the array, shrink the portiion of the array
        for(int i = Array.Length -1 ; i >= 0; i--)
        {
            RemoveNextMax(i);

        }


        //We have methods to get the parent index
        //And both children index

    }

    /// <summary>
    /// Move the largest value in the heap to just outside of the heap
    /// and trickle down the new top of the heap to its logical position
    /// </summary>
    /// <param name="lastpos">Last index that is part of the heap</param>
    private void RemoveNextMax(int lastpos)
    {
        //Store the largest value
        T max = Array[0];
        //Put the value at the end of the heap at the front of it
        Array[0] = Array[lastpos];
        //Trickle down the new top of the heap
        //Note that we are going to consider the heap to be one smaller now
        TrickleDown(0, lastpos - 1);
        //Insert the max value at what used to be th eend
        Array[lastpos] = max;
    }

    public void Heapify()
    {
        //Find the index of the first parent
        int parentIndex = GetParentIndex(Array.Length - 1);
        //Loop backwards from the first parent to the root
        for (int i = parentIndex; i >= 0; i--)
        {
            //For each parent, trickle it down into the proper position
            TrickleDown(i, Array.Length - 1);
        }
    }

    /// <summary>
    /// Trickles down the element indicated to its proper position
    /// </summary>
    /// <param name="index">The array index of the element to trickle down</param>
    /// <param name="lastPos">The  last valid position in the array</param>
    private void TrickleDown(int index, int lastPos)
    {
        //Grab the current value indicated by the index
        T currentValue = Array[index];
        bool done = false;

        int largerChildIndex = GetLeftChild(index);

        //while the current value is not in the right position
        while (!done && largerChildIndex <= lastPos)
        {
            int rightChildIndex = GetRightChild(index);
            //  it could potentially have children bigger than it
            //      Look at both children and determine which on is larger
            if (rightChildIndex <= lastPos &&
                Array[rightChildIndex].CompareTo(Array[largerChildIndex]) > 0)
            {
                largerChildIndex = rightChildIndex;
            }

            //      Check if the largest child is bigger than the current item
            if (currentValue.CompareTo(Array[largerChildIndex]) < 0)
            {
            //          If it is put the largest child, in the current index
            //              Either, put the current item in the largest's child old slot
            //              Or just keep track of where the largest child used to be
                Array[index] = Array[largerChildIndex];
                index = largerChildIndex;
                largerChildIndex = GetLeftChild(index);
            }
            else
            { 
            //          If the largest child isn't bigger we're done
                done = true;
            }

            //The current value is larger
            Array[index] = currentValue;
        }

    }


    /// <summary>
    /// Calculate the index of a given node's parent
    /// </summary>
    /// <param name="index"></param>The array index of the child
    /// <returns></returns>The array index ofthe parents child
    private int GetParentIndex(int index)
    {
        return (index - 1) / 2;
    }

    /// <summary>
    /// Calculate the index of a node's left child
    /// </summary>
    /// <param name="index">The array index of the parent</param>
    /// <returns>The array index of the paren'ts left child</returns>
    private int GetLeftChild(int index)
    {
        return index * 2 + 1;
    }
    /// <summary>
    /// Calculate the index of a node's right child
    /// </summary>
    /// <param name="index">The array index of the parent</param>
    /// <returns>The array index of the paren'ts right child</returns>
    private int GetRightChild(int index)
    {
        return index * 2 + 2;
    }
}
