using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinarySearchTree;


namespace HashTables;

public class ChainingBST<K, V> : A_HashTable<K, V>
    where K : IComparable<K>
{
    private int iBucketCount = 0; //counter for the buckets in the HT
    private const int iInitialSize = 5; //Size of the original HT, double when needed

    public ChainingBST()
    {
        oDataArray = new object[iInitialSize];
    }

    /// <summary>
    /// Method to add BSTs into the buckets of our chainging HT
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <exception cref="ApplicationException"></exception>
    public override void Add(K key, V value)
    {
        int iHash = HashFunction(key); //Find the hash
        KeyValue<K, V> keyValue = new KeyValue<K, V>(key, value);
        //Variable for the BST to store data
        BST<KeyValue<K, V>> bstCurrent;


        if (oDataArray[iHash] == null) //Nothing in the bucked
        {

            bstCurrent = new BST<KeyValue<K, V>>(); //Create new BST
            oDataArray[iHash] = bstCurrent; //Put the BST into the data array
            iBucketCount++; //Increment bucket count
        }
        else
        {   //Something in the bucket, get the current BST
            bstCurrent = (BST<KeyValue<K, V>>)oDataArray[iHash];
            //Check for duplicates if key is already in BST
            if (bstCurrent.Contains(keyValue))
            {
                throw new ApplicationException("Key already exists in HashTable");
            }
            iNumCollisions++; //There was a collision, track it

        }
        bstCurrent.Add(keyValue); //Add the value into the bst
        iCount++;//Increment the count
        //If the hash table is overloaded then we expand it
        if (isOverloaded())
        {
            expandHashTable();
        }
    }

    /// <summary>
    /// Check if the table is over loaded based on the limit we set
    /// </summary>
    /// <returns></returns>
    private bool isOverloaded()
    {
        bool isOVerloaded = (double)iBucketCount / HTSize > dLoadFactorLimit;
        return isOVerloaded;
    }

    /// <summary>
    /// Expand the hashtable and create a new, bigger hashtable, copy elements over
    /// </summary>
    private void expandHashTable()
    {
        object[] oOldArray = oDataArray; //Reference to original data 
        oDataArray = new object[oOldArray.Length * 2]; //double the size
        //Reset counters
        iCount = 0;
        iBucketCount = 0;
        iNumCollisions = 0;

        //Go through all the items in the old table
        for (int i = 0; i < oOldArray.Length; i++)
        {
            if (oOldArray[i] != null)
            {
                //Check all the items in the bST
                BST<KeyValue<K, V>> bstCurrent = (BST<KeyValue<K, V>>)oOldArray[i];
                foreach (KeyValue<K, V> kv in bstCurrent)
                {
                    Add(kv.Key, kv.Value); //Add the KV pair from the BST 
                }
            }
        }
    }

    public override V Remove(K key)
    {
        ///Keep track of the item 
        KeyValue<K, V> kvRemoved = null;
        int iHash = HashFunction(key);

        //Refernce to the bst at the position of the hashcode
        BST<KeyValue<K, V>> bstCurrent = (BST<KeyValue<K, V>>)oDataArray[iHash];
        //Make sure bst is not null
        if (bstCurrent != null)
        {
            //Check if the itim is inside the BST
            for (int i = 0; i < bstCurrent.Count; i++)
            {
                //Wade said that looping through by using ElementAt is very inefficent
                //BST has its own more efficent methods 
                if (bstCurrent.ElementAt(i).Key.Equals(key))
                {
                    kvRemoved = bstCurrent.ElementAt(i); //store value the value
                    bstCurrent.Remove(kvRemoved); // Remove it from the BST
                    iCount--; //Decrement counter
                    //If the BST is empty
                    if (bstCurrent.Count == 0)
                    {
                        //Remove the bst from the HT
                        oDataArray[iHash] = null;
                        //decrement the number of buckets
                        iBucketCount--;


                    }
                    break;
                }
            }
        }

        //item was not found
        if (kvRemoved == null)
        {
            throw new ApplicationException("Key does not exist in HashTable");
        }

        //Return removed value
        return kvRemoved.Value;
    }

    public override V Get(K key)
    {
        V vReturn = default(V);
        //get hashcode
        int hash = HashFunction(key);
        BST<KeyValue<K, V>> bstCurrrent = (BST<KeyValue<K, V>>)oDataArray[hash];
        //keep track of if we found it or not
        bool isFound = false;
        if (bstCurrrent != null)
        {
            foreach (KeyValue<K, V> kv in bstCurrrent)
            {
                if (kv.Key.Equals(key))
                {
                    //found it!
                    isFound = true;
                    vReturn = kv.Value;
                    break;
                }
            }
        }
        if (!isFound)
        {
            throw new ApplicationException("Key not found in Hashtable");
        }
        return vReturn;
    }

    //To String method to show every key value pair in the hashTable 
    public override string ToString()
    {
        //Create a new String
        string kvpString = "";

        //Loop through the HT
        for (int i = 0; i < oDataArray.Length; i++)
        {
            //get the current BST
            BST<KeyValue<K, V>> bstCurrent = (BST<KeyValue<K, V>>)oDataArray[i];
            //If the bst actually contains infomation then loop through the current bucket
            //check thaht the current bst is not null
            if (bstCurrent != null)
            {
                foreach (KeyValue<K, V> kv in bstCurrent)
                {
                    //Add the line to our string
                    kvpString += " Key: " + kv.Key + " Value: " + kv.Value;
                    Console.WriteLine(kvpString + '\n');
                }

            }
        }
        return kvpString;
    }

    public override IEnumerator<V> GetEnumerator()
    {
        return new ChainingEnumerator<V>(this);
    }

    private class ChainingEnumerator<V> : IEnumerator<V>
    {
        ChainingBST<K, V> parent; // parent ChainingBST
        int iBucket = -1; //index in the bucket
        IEnumerator<KeyValue<K, V>> bstEnumerator = null; //Enumerator for the bst

        public ChainingEnumerator(ChainingBST<K, V> parent)
        {
            this.parent = parent;
        }

        //Return the value in the current slot of the BST
        public V Current
        {
            get
            {
                //I was overthinking this a bunch literally just need
                //to get the current value
                return bstEnumerator.Current.Value;
            }
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            parent = null;
            bstEnumerator = null;
        }

        public bool MoveNext()
        {

            bool bMoved = false;
            //First check if your current position in the array points at a arrayList
            if ((iBucket > -1) && (parent.oDataArray[iBucket] != null))
            {
                //BST<KeyValue<K, V>> bstCurrent = (BST<KeyValue<K, V>>)parent.oDataArray[iBucket];
                //bstEnumerator = bstCurrent.GetEnumerator();
                if (bstEnumerator.MoveNext())
                {
                    //there is still more data
                    bMoved = true;
                }
            }
            //If the enumerator could not move next
            if (!bMoved)
            {
                while (!bMoved && (++iBucket < parent.oDataArray.Length)) //check the next bucket
                {

                    BST<KeyValue<K, V>> bstCurrent = (BST<KeyValue<K, V>>)parent.oDataArray[iBucket];
                    if (bstCurrent != null)
                    {
                        //Set the enumerator to the current BST
                        bstEnumerator = bstCurrent.GetEnumerator();
                        //Move next
                        bstEnumerator.MoveNext();
                        bMoved = true;
                    }

                }
            }
            return bMoved;
        }

            public void Reset()
            {
                iBucket = -1;
                bstEnumerator = null;
            }
        }

    }

    
        




