using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Lists;


namespace HashTables;

public class ChainingHT<K, V> : A_HashTable<K, V>
    where K : IComparable<K>
{

    //We're already keeping track of the number of stored values - the Count
    //In a chaining hashtable, we might also want to keep track of th enumber
    //of buckets that have someting in them because these could be different numbers
    private int iBucketCount = 0;

    //Set an inital table size - we don't have to start with a prime numbe
    //unlike in open addressing
    private const int iInitialSize = 4;

    //This is going to return an array of type k (they keys type)
    //This array should be sorted
    //And it is a list of all the key in the hashtable
    //TODO: Get this working ASAP
    public override K[] Keys
    { 
        get
        {
            //return an array of type k
            K[] ret =  new K[oDataArray.Length];

            foreach(KeyValue<K,V> kv in oDataArray)
            {
                //return alCurrent.ElementAt(iArrayListPos).Value;
                //This is not working i think it the count causing issues
                if(kv.Key != null)
                {
                    //This count is not the right position to be checking
                    ret[iCount] = kv.Key;
                    iCount++;

                }
               
            }

            Array.Sort(ret);
            Console.WriteLine(ret);
            return ret;
        }
    }

    public ChainingHT()
    {
        oDataArray = new object[iInitialSize];
    }

    //Know how to write the hash function method and know how it works for midterm
    public override void Add(K key, V value)
    {
        
        //First, find what bucket/location in the table we're adding to 
        //Aka the hash
        int iHash = HashFunction(key);
        KeyValue<K, V> keyValue = new KeyValue<K, V>(key, value);
        //Declare a variable for the arraylist we're going to put it in
        ArrayList<KeyValue<K, V>> alCurrent;

        //We've got two possible causes
        //There might already be an arrayList in the bucket 
        //If theirs something already there then we would just add it
        //to the existing arrayList
        //or it might be empty and we'll have to create it
        if (oDataArray[iHash] == null)
        {
            //There is nothing in the bucket
            //we need to create a new array list
            alCurrent = new ArrayList<KeyValue<K, V>>();
            //Put the new array list into the data array
            oDataArray[iHash] = alCurrent;
            //We need to keep track of how many buckets have something in them
            iBucketCount++;
        }
        else
        {
            //There is already an item in the bucked
            //We'll grab the arrayList from the bucket
            alCurrent = (ArrayList<KeyValue<K, V>>)oDataArray[iHash];
            //Now we need to check for duplicates
            //If this key already exists in the arrayList, throw an exception
            if(alCurrent.Contains(keyValue))
            {
                throw new ApplicationException("Key already exists in Hashtable");
            }
            //Since there's already at least one value in this bucket,
            //we'll mark a collision
            //This is just for statistacal purposes
            iNumCollisions++;
        }
        //In either case, we end up with alCurrent being the arraylist to
        //put our keyvalue into
        alCurrent.Add(keyValue);
        //Increment our count
        iCount++;

        //TODO: for our future selves: we still need to check if the table
        //is overloaded, and rehash if necessary
        if(isOverloaded())
        {
            expandHashTable();
        }
    }
    
    
    /// <summary>
    /// Checks if the tables load factor is over the limit we've set
    /// </summary>
    /// <returns>true if overloaded, false otherwise</returns>
    private bool isOverloaded()
    {
        //Check the count of items in the table / by the size of the HT
        //If that is greater than the loadfactor then it is overloaded and we need to expand
        bool isOverloaded = (double)iCount / HTSize > dLoadFactorLimit;
        return isOverloaded;
    }

    
    /// <summary>
    /// Expands the hashtable by creating a new, bigger hashtable and copying 
    /// over the contents
    /// </summary>
    private void expandHashTable()
    {
        //Create a variable that is a reference to the original data
        //I need to learn how to do this. Stuck on creating the variable
        //to link up with the main class    
        object[] oOldArray = oDataArray;
        //Replace our data array with one that is twice the size
        oDataArray = new object[oOldArray.Length * 2];

        //Replace oDataArray with a new array that is TWICE the size of the old array

        //Reset all our counter - iCount, iBucketCount, iNumCollisions - to zero
        iCount = 0;
        iBucketCount = 0;
        iNumCollisions = 0;

        //Go through everything in the old table (foreach)
        for (int i = 0; i < oOldArray.Length; i++)
        {
            if (oOldArray[i] != null)
            {
                //Check every item in the arraylist
                ArrayList<KeyValue<K, V>> alCurrent = (ArrayList<KeyValue<K, V>>)oOldArray[i];
                foreach (KeyValue<K, V> kv in alCurrent)
                {
                    //Add the key value pair from the array list to our table using add
                    Add(kv.Key, kv.Value);
                }
            }
        }
    }

    public override void Clear()
    {
        iBucketCount = 0;
        iNumCollisions = 0;
        iCount = 0;
        oDataArray = new object[iInitialSize];
    }
    

    public override V Get(K key)
    {
        //Note takes in a key and returns a value 
        V vReturn = default(V);
        //First we will get the Hashcode
        int hash = HashFunction(key);
        //Next we'll grab the arraylist at the location indicated
        //  by the hashcode
        //Declare a variable for the arraylist we're going to put it in
        ArrayList<KeyValue<K, V>> alCurrent = 
            (ArrayList < KeyValue < K, V >>) oDataArray[hash];

        //Keep track of whether or not we've found the item.
        //  To start with, this is false
        bool bFound = false;

        //if the arraylist exists the key might be in it
        //  search through the array list
        if(alCurrent != null)
        {
            foreach(KeyValue<K, V> kv in alCurrent)
            {
                if(kv.Key.Equals(key))
                {
                    //found the key
                    bFound = true;
                    vReturn = kv.Value;
                    break;
                }
            }
         
        }
        if(bFound == false)
        {
            throw new ApplicationException("Key not found in Hashtable");
        }
        return vReturn;
        //      if we find the key, note that we've found it, and keep track of the value
        //if we havent found the value throw an application exception
        //Return the value 
    }

    public override IEnumerator<V> GetEnumerator()
    {
        return new chainingEnumerator<V>(this);
    }

    public override V Remove(K key)
    {
        // Let's track of the item and whether or not it has been removed
        KeyValue<K, V> kvRemoved = null;
        // Get the hashcode for the key sent in
        int iHashCode = HashFunction(key);
        // Get a reference to the ArrayList at the position indicated by the hashcode
        ArrayList<KeyValue<K, V>> alCurrent =
            (ArrayList<KeyValue<K, V>>)oDataArray[iHashCode];

        // Check to make sure that arraylist is not null
        if (alCurrent != null)
        {
            //    Check if the item is inside the ArrayList using the ArrayList class
            for (int i = 0; i < alCurrent.Count; i++)
            {
                if (alCurrent.ElementAt(i).Key.Equals(key))
                {
                    //         If it is, remove it, store the value.
                    kvRemoved = alCurrent.RemoveAt(i);
                    //         Don't forget to decrement the count.
                    iCount--;
                    //         If the arraylist is now empty,
                    if (alCurrent.Count == 0)
                    {
                        //              remove it from the table
                        oDataArray[iHashCode] = null;
                        //              don't forget to decrement the bucket count
                        iBucketCount--;
                    }
                    break;
                }
            }

        }

        // If we didn't find the item, throw an exception
        if (kvRemoved == null)
        {
            throw new ApplicationException("Key does not exist in hashtable");
        }

        // Return the value from the KeyValue pair that was removed. 
        return kvRemoved.Value;
    }

    /*
    public override V Remove(K key)
    {

        //Keep track of wheter or not it has been removed
        KeyValue<K, V> kvRemoved = null;
        //Get the hashcode for the key sent in
        int hash = HashFunction(key);
        //Get a reference to the ArraLiyst at the position indicated by the hashcode
        ArrayList<KeyValue<K,V>> alCurrent = 
            (ArrayList<KeyValue<K,V>>) oDataArray[hash];

        //Check to make sure that arraylist is not null
        if(alCurrent != null)
        {
            //Check if the item is inside the ArrayList using the ArrayList class
            //If it is, remove it, store the value
            for (int i = 0; i < alCurrent.Count; i++)
            {
                if (alCurrent.ElementAt(i).Key.Equals(key))
                {
                    //Wasn't sure on this in class
                    kvRemoved = alCurrent.ElementAt(i);
                    iCount--;

                }
                //If the arrayList is now empty
                {
                    if(alCurrent.Count == 0)
                    {
                        //remove it from the table
                        //Also wasn't sure how to do this during class
                        oDataArray[hash] = null;
                        //don't forget to decrement the bucket count
                        iBucketCount--;
                    }
                    break;
                }
            }
        } 
        if(kvRemoved == null)
        {
            //If we didn't find the item throw an exeption
            throw new ApplicationException("Item was not found in the HT");
        }
        //Return the value from the KV pair that was removed
        return kvRemoved.Value;
    }
    */

    private class chainingEnumerator<V> : IEnumerator<V>
    {
        //Keep track of a few things
        //  Keep a referece to the parent hashTable so i can access its data array
        ChainingHT<K, V> parent;
        // -Keep track of what array slot you;re in in the hastable
        int iBucket = -1;
        //Keep track of what index youre at inside the array list
        int iArrayListPos = -1;

        public chainingEnumerator(ChainingHT<K, V> parent)
        {
            this.parent = parent;   
        }
        //Return the value in the current slot of the arraylist you are currently in
        //which will be in the slot of data array youre currently pointing at
        public V Current
        {
            get
            {
                //First we'll grab the Arraylist in the current bucket
                ArrayList<KeyValue<K, V>> alCurrent = (ArrayList<KeyValue<K, V>>)parent.oDataArray[iBucket];
                //Return teh element at the position indicate by teh ArrayList Position
                return alCurrent.ElementAt(iArrayListPos).Value;
            }
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            parent = null;
        }

        public bool MoveNext()
        {
            bool bMoved = false;
            //First check if your current position in the array points at a arrayList
            if ((iBucket > -1) && (parent.oDataArray[iBucket] != null))
            {
                //If it does, check if the arrayList has more items
                ArrayList<KeyValue<K, V>> alCurrent = (ArrayList<KeyValue<K, V>>)parent.oDataArray[iBucket];
                //If it does, advance to look at the next item in the ArrayList
                if (++iArrayListPos < alCurrent.Count)
                {
                    //there is still more data
                    bMoved = true;
                }
            }
                
                if(!bMoved)
                {
                    while(!bMoved && (++iBucket < parent.oDataArray.Length))
                    {
                        //Pull the item out of the bucket
                        ArrayList<KeyValue<K, V>> alCurrent = (ArrayList<KeyValue<K, V>>)parent.oDataArray[iBucket];
                        if(alCurrent != null)
                        {
                        //If we've hit anotehr ArrayList, the curent item will be the first
                        //in that arraylist
                        iArrayListPos = 0;
                            bMoved = true;
                        }

                    }
                }

            
            //Return true if we were able to advance, false otherwise
            return bMoved;
        }

        public void Reset()
        {
            iBucket = -1;
            iArrayListPos = -1;
        }
    }
}
