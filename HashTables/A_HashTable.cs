using System;
using System.Collections;
using System.Collections.Generic;


namespace HashTables;

public abstract class A_HashTable<K, V> : I_HashTable<K, V>
    where K : IComparable<K>
{
    //The table array
    //In the case of chaining, this will be an array of data structures
    //With open addressing, it'll store the key-value pairs directly
    protected object[] oDataArray;

    //We should keep track of how many elements are in our hashtable
    protected int iCount;

    //Every hashtable will also have a load factor limit - the maximum
    //percentage full that we will allow our array to file up to before rehasing
    //Microsoft uses 0.72 sp we'll use taht too. But we can change it for 
    //specific hash tables
    protected double dLoadFactorLimit = 0.72;

    //Track the number of collisions for stats purposes
    protected int iNumCollisions = 0;

    //Some properties to make those fields accessible
    public int Count { get { return iCount; } }

    public int NumCollisions { get { return iNumCollisions; } } 
    
    //We'll also have a property for the table size
    public int HTSize
    {
        get
        {
            return oDataArray.Length;
        }
    }


    public virtual K[] Keys { get; }

    public abstract void Add(K key, V value);
    public abstract V Get(K key);
    public abstract IEnumerator<V> GetEnumerator();
    public abstract V Remove(K key);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    //Every table is going to have a hash function
    //So we'll provide a default hash function
    //This hash function is going to be a two-part has function -
    //the key will be responsible for returning an interger, and we'll
    //just handle the mod part
    protected int HashFunction(K key)
    {
        int hashcode = key.GetHashCode();
        return Math.Abs(hashcode % HTSize);
    }

    public virtual void Clear()
    {
        throw new NotImplementedException();
    }
}
