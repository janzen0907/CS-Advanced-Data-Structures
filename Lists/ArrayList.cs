using System;
using System.Collections;
using System.Collections.Generic;

using DataStructuresCommon;

namespace Lists;
//An implementation of a list using arrays


public class ArrayList<T> : A_List<T> where T: IComparable<T>
{
    //Interior member to hold our values. Initialize to empty. 
    private T[] values = new T[0];

    public override int Count {
    get
    {
        return values.Length;
    }
}
    
    

    //To add, we'll just create a new bigger array and add onto the end of it
    //Practice writing these methods and know how to do them..
    public override void Add(T data)
    {
        //Create a new array one bigger than the old one
        T[] newArr = new T[values.Length + 1];
        //Loop through the array
        for(int i=0; i< values.Length; i++)
        {
            newArr[i] = values[i];
        }

        newArr[values.Length] = data;

        values = newArr;

    }

    public override T ElementAt(int index)
    {
        
        return values[index];
    }

    public override void Clear()
    {
        values = new T[0];
    }

    public override void Insert(int index, T data)
    {
        //Put a piece of data in a specific spot in the arrayList
        T[]newValues = new T[values.Length + 1];

        int j = 0; 
        //Loop through existing values, when we get to the index position 
        //insert the new data
        for(int i=0; i<values.Length; i++)
        {
            //Check if the index is equal to the given index
            if(i == index)
            {
                //insert the data into the newValues array at the index we
                //provided
                newValues[j] = data;
                //increment j
                j++;
            }
            //Add the the values at index j to the newValues array
            newValues[j] = values[i];
            //increment j
            j++;
        } 
        //values will now be equal to our new array
        values = newValues;
    }

    public override bool Remove(T data)
    {
        //Remembert that type T is IComparable, which means any object
        //of type T has a CompareTo method. If compareTo of two objects

        for(int i = 0; i < values.Length; i++)
        {
            //compare i to our data, if it is greater or less
            //then it doesnt need to be removed
            //I was putting just i it needed to be Values at i
            //Just i is an index. 
            if(values[i].CompareTo(data) == 0)
            {
                //We've found the element we're looking for
                //Lets remove it
                RemoveAt(i);
                return true;
            
            }
        }
        
        return false;
    }

    public override T RemoveAt(int index)
    {
        T[] newValues = new T[values.Length - 1];
        T tReturn = default(T);

        //Very much like insert, except instead of adding an extra value
        //at the index, we'll skip the index
        int j = 0;
        for(int i=0; i< values.Length; i++)
        {
            if(i == index)
            {
                tReturn = values[i];
            }
            else
            {
                newValues[j] = values[i];
                j++;
            }            
        }

        values = newValues;
        return tReturn;
    }

    public override T ReplaceAt(int index, T data)
    {

        T tReturn = values[index];
        values[index] = data;
        return tReturn;
    }

    public override IEnumerator<T> GetEnumerator() {
        return new Enumerator(this);
    }

    private class Enumerator: IEnumerator<T>
    {

        // A reference to the ArrayList we're enumerating
        private ArrayList<T> parent;
        // The current index we're visiting
        private int index;

        public Enumerator(ArrayList<T> parent)
        {
            this.parent = parent;
            Reset();
        }

        public T Current
        {
            get
            {
                return parent.values[index];
            }
        }

        // Also a version of the current method that doesn't use generics
        object IEnumerator.Current
        {
            get
            {
                return parent.values[index];
            }
        }

        // Resets to a position logically before the first element
        public void Reset()
        {
            index = -1;
        }

        // MoveNext moves to the next position if it can
        // if it can't, return false
        public bool MoveNext()
        {
            if ((index + 1) < parent.values.Length)
            {
                index++;
                return true;
            }
            else
            {
                return false;
            }
        }

        // This is supposed to clean up resources used by the enumerator
        // Mostly, set references to null
        public void Dispose()
        {
            parent = null;
        }

    }
}