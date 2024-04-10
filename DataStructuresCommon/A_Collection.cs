using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresCommon;

/// <summary>
/// Partial implementation of some of the common features of Collections ADTs
/// </summarY>
/// <typeparm name="T">The type a collection is storing</typeparm>
public abstract class A_Collection<T> : I_Collection<T> where T: IComparable<T>
{
    public abstract void Add(T data);
    public abstract void Clear();
    public abstract bool Remove(T data);
    public abstract IEnumerator<T> GetEnumerator();

    /// <summary>
    /// Count is a property detailing the number of items in the collection
    /// </summary>
    /// <value>The number of items in the collection</value>
    /// We have marked this method virtual - that means child implementations
    /// can override this method.
    /// How is this method going to work? This method is just going to loop
    /// through all the contents of the collection, and our children might
    /// find more efficient ways of doing things.
    public virtual int Count
    {
        get
        {
            int count = 0;
            // Foreach will work for collections that implement the
            // IEnumerable interface (which I_Collection does, which means
            // we do too). It depends on the GetEnumerator method;
            foreach (T item in this)
            {
                count++;
            }
            return count;

        }
    }

    // We can also implement contains using the Enumerator from GeEnumerator
    public virtual bool Contains(T data)
    {
        bool found = false;

        // Get an instance of the enumerator - since we implement
        // IEnumerable, we know this exists!
        IEnumerator<T> myEnumerator = GetEnumerator();
        // Go to the start of the collection
        myEnumerator.Reset();

        // Loop through until you find the data
        while (!found && myEnumerator.MoveNext())
        {
            found = myEnumerator.Current.Equals(data);
        }
        
        return found;
    }

    //We're just going to implement this here so we dont have to
    //repeat this boilerplate later
     IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    //Override the ToString. Typically we wouldn't do this in a data
    //structure, but it'll be useful for debugging
    public override string ToString()
    {
        StringBuilder result = new StringBuilder("[");
        string seperator =", ";

        foreach(T item in this)
        {
            result.Append(item + seperator);
        }
        if(Count > 0)
        {
            
            result.Remove(result.Length - seperator.Length, seperator.Length);
        }

        result.Append("]");
        return result.ToString();
    }



}

