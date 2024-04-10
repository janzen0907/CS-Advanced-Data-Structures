using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HashTables;

public abstract class A_OpenAddressing<K, V> : A_HashTable<K, V>
    where K : IComparable<K>
{
    private int iTombstones = 0;

    public override K[] Keys => base.Keys;

    //Most of our open addressing tables will need to keep track of 
    //the "next primte number":
    private PrimeNumber pn = new PrimeNumber();

    //Set up the inital array to be a prime number size
    protected A_OpenAddressing()
    {
        oDataArray = new object[pn.GetNextPrime()];

    }

    //Abstract method to get the incremenet value (this will
    //vary based on implementation but all open addressing tables
    //will need it
    protected abstract int GetIncrement(int iAttempt, K key);

    public override void Add(K key, V value)
    {
        //Keep track of how many attempts have been made
        int iAttempt = 1;
        //Keep track of the inital hash key
        int iInitalHash = HashFunction(key);
        //Wrap up the key and value as a KeyValue pair
        KeyValue<K, V> kvNew = new KeyValue<K, V>(key, value);
        //Keep track of our current position in the hashtable
        int iCurrentPosition = iInitalHash;
        //Keep checking positions in the hashtable according to our probe sequence
        //until we find a null (empty slot on our table)
        while (oDataArray[iCurrentPosition] != null) 
        {
            //      Check to make sure the current position doesnt't contain a duplicate
            //      of our keyValue
            if (oDataArray[iCurrentPosition].GetType() == typeof(KeyValue<K,V>))
            {
                //Cast to KeyValue
                KeyValue<K,V> kvInBucket = (KeyValue<K, V>)oDataArray[iCurrentPosition];
                //check to see if the keys are equal, if tehy are we have a dup
                if(kvInBucket.Equals(kvNew))
                {
                    throw new ApplicationException("Item already present in Hashtable");
                }
            }else if(oDataArray[iCurrentPosition].GetType() != typeof(Tombstone))
            {
                //      Check to make sure the object is a KeyValue pair(if it isn't, exception)
                throw new ApplicationException("Unexpected Object of type " + 
                    oDataArray[iCurrentPosition].GetType());

            }
            //
            //      Advance our current location to look at the next location by
            //      by combining our intial position with the get increment function
            iCurrentPosition = iInitalHash + GetIncrement(iAttempt++, key);
            //      if we fall off the bottom of the table , loop back to the top
            //This code below is a short form of:
            //iCurrentPosition = iCurrentPosition % HTSize
            iCurrentPosition %= HTSize;
        //     Rememebr to keep track of collions
        iNumCollisions++;
            
        }
        //
        //Once the above loop is done, we should have hit a null at the current location
        //Add the KeyValue to the current location
        oDataArray[iCurrentPosition] = kvNew;
        //Remember to increment our count
        iCount++;
        //
        //Dont forget tot check for the table being overloaded and see if it needs expanding
        if(isOverLoaded())
        {
            expandHashTable();
        }
    }
    private bool isOverLoaded()
    {
        return (iCount + iTombstones) / (double)HTSize > dLoadFactorLimit;
    }

    private void expandHashTable()
    {
         //Declare the old array
        object[] oOldArray = oDataArray;
        //Expand oDataArray, double the size
        oDataArray = new object[pn.GetNextPrime()];

        //Reset the counters to 0
        iCount = 0;
        iNumCollisions = 0;
        iTombstones = 0;

        //Loop through the items in the old array
        foreach(object o in oOldArray) 
        {
            if (o != null)
            {
                if (o.GetType() == typeof(KeyValue<K, V>))
                {
                    KeyValue<K,V> kv = (KeyValue<K,V>)o;
                    this.Add(kv.Key, kv.Value); 
                }
            }

        }
    }

    public override void Clear()
    {
        pn = new PrimeNumber();
        oDataArray = new object[pn.GetNextPrime()];
        iCount = 0;
        iNumCollisions = 0;
        iTombstones = 0;
    }

    public override V Get(K key)
    {
        //Similar to get in the chainingHT, and add in open addressing
        //Track of the value were returning
        V vReturn = default(V);
        //Find initial position to check
        int hash = HashFunction(key);
        //Keep track of where we're currently looking in the table
        //  starting at the inital hash
        int iCurrentPosition = hash;
        //Keep track of the number of incremenets
        int iAttempts = 1;
        //Track wheter we found it or not
        bool bFound = false;
        //index of the first tombstone
        int tombstoneIndex = -1;

        //LOOP
        //Keep searching the table until we find the key or we hit a null
        while (oDataArray[iCurrentPosition] != null && !bFound)
        {
            //  for our current position see if there is a KeyValue here    
            if (oDataArray[iCurrentPosition].GetType() == typeof(KeyValue<K, V>))
            {
                //      If there is, see if the key is the key we're looking for
                KeyValue<K, V> kvInBucket = (KeyValue<K, V>)oDataArray[iCurrentPosition];
                if(kvInBucket.Key.Equals(key))
                {
                    //We FOUND IT!!!
                    bFound = true;
                    //If it is, we've found our item! Keep track of it.
                    vReturn = kvInBucket.Value;
                    //Mark this position as a tombstone
                    oDataArray[iCurrentPosition] = new Tombstone();
                }
            }
            //Check if its a tombstone at the current position
            else if (oDataArray[iCurrentPosition].GetType() == typeof(Tombstone))
            {
                //move the tombstone index to where we found one
                tombstoneIndex = iCurrentPosition;
                //Move the item to where the tombstone was
                oDataArray[tombstoneIndex] = oDataArray[iCurrentPosition];
                //Mark the spot that just moved as a tombstone
                oDataArray[iCurrentPosition] = new Tombstone();
                
            }
                iCurrentPosition = hash + GetIncrement(iAttempts++, key);
                //this is all i missed in class. Still dont really get what it does
                iCurrentPosition %= HTSize;
            //  Advance to the next location
        //Remeber if we go past the end of the table, we need to go bakc to the start
        }
            
        //If we haven't found it after the loop throw an exception
        if(!bFound)
        {
            throw new ApplicationException("The key was not found in the table");
        }
            
       
        //Return the value
        return vReturn;
    }

    public override IEnumerator<V> GetEnumerator()
    {
        return new openAddressingEnumerator<V>(this);
    }

    public override V Remove(K key)
    {
        //Get the hashcode
        int hash = HashFunction(key);
        //Keep track of the current location we are looking at
        int iCurrentHash = hash;
        //Track attempts
        int iAttempts = 1;
        //Track if we found it
        bool bFound = false;
        //Var to hold the value when we find it
        V vReturn = default(V); 

        //Loop until either we find our key or hit a null
        while(!bFound && oDataArray[iCurrentHash] != null)
        {
            //Check to see if the item is of type key value
            if (oDataArray[iCurrentHash].GetType() == typeof(KeyValue<K, V>))
            {
                KeyValue<K,V> kv = (KeyValue<K,V>) oDataArray[iCurrentHash];
                if(kv.Key.Equals(key))
                {
                    //Store its value and mark as found
                    vReturn = kv.Value;
                    oDataArray[iCurrentHash] = new Tombstone();
                    bFound = true;
                    iCount--;
                    iTombstones++;
                    //We found the item replace it with a TombStone
                }
            }
            //Go to the next location
            iCurrentHash = hash + GetIncrement(iAttempts++, key);
            iCurrentHash %= HTSize;
        }
        //Throw an exception if its not in the table
        if(!bFound)
        {
            throw new ApplicationException("Key" + key + "does not exist in table");
        }

        return vReturn;
    }

    private class openAddressingEnumerator<V> : IEnumerator<V>
    {
        A_OpenAddressing<K, V> parent = null;
        private int iCurrent = -1;

        public openAddressingEnumerator(A_OpenAddressing<K, V> parent)
        {
            this.parent = parent;  
            Reset();
        }

        public V Current
        {

            get
            {

                KeyValue<K, V> kv = (KeyValue<K, V>)parent.oDataArray[iCurrent];
                return kv.Value;   
            }
        }


        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            parent = null;
        }

        public bool MoveNext()
        {
            iCurrent++;
            bool bMoved = false;
            //Need to check if there is room to move in the hash table
            //Move to the next location
            while(!bMoved && (iCurrent < parent.HTSize))
            {

                if(parent.oDataArray[iCurrent] != null &&
                    parent.oDataArray[iCurrent].GetType() == typeof(KeyValue<K,V>))
                {
                    bMoved = true;
                }
                else
                {
                    iCurrent++;
                }
            }

            return bMoved;
            

        }

        public void Reset()
        {
            iCurrent = -1;
        }
    }

    //The easiest class you will ever write
    internal class Tombstone
    {

    }
}
