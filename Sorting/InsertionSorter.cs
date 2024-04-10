using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting;

public class InsertionSorter<T> : A_Sorter<T> where T : IComparable<T>
{
    public InsertionSorter(T[] array) : base(array)
    {
    }

    public override void Sort()
    {
        //Go through every value but the first and insert it into the
        //sorted part of the array
        for(int i =1; i< Array.Length; i++)
        {
            //Insert the tiem at i into the sorted array, which runs
            //from 0 to i-1
            insertInOder(i);

        }
        
    }

    private void insertInOder(int indexUnsorted)
    {
        //int currentItem = indexUnsorted - 1;
        T unsortedElement = Array[indexUnsorted];
        ////While we haven't hit the front end of the array and the unsorted element
       // while (currentItem >= 0 && Array[currentItem].CompareTo(valueUnsorted) > 0)
       //{
            //Shuffle the current sorted value one location to the right
           // Array[currentItem + 1] = Array[currentItem];
           // currentItem--;
       // }

        //Place the unsorted item in its correct position
       // Array[currentItem + 1] = valueUnsorted;
   // }
    //Grab the unsorted element we're trying to put in posisiton
    //Keep track of the index of the current already sorted item
    //  we're comparing to - it'll start off being 1 to the ledt
    //  of the unsorted item
        
        int currentItem = indexUnsorted - 1;
        //still belongs before the current item..
        //While we haven't hit the front end of the array and the unsorted element
        while(currentItem >= 0 && unsortedElement.CompareTo(Array[currentItem]) <0) 
        {
            //  Shuffle the current sorted value one location to the right
            //  Look at the iem to the left next
            Array[currentItem + 1] = Array[currentItem];
            currentItem--;
            //The index is now either before the front of the array, OR
            //at the first element that belongs before our unsorted item,
            //so in either case. place the formerly unsorted item AFTER it
            
        }
        Array[currentItem + 1] = unsortedElement;   

        

    }
}
