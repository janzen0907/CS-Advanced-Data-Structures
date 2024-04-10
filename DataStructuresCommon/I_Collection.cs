using System;
using System.Collections.Generic;

namespace DataStructuresCommon;

///
/// <summary>
/// Interface for implementation of Collection ADTS
/// </summary>
/// <typeparm name="T">The type of what the collection holds</typeparm>
public interface I_Collection<T> : IEnumerable<T> where T: IComparable<T>
{

    ///
    /// <summary>
    /// Adds the given item to the collection
    /// </summary>
    /// <param name="data">Item to add</param>
    void Add(T data);

    /// <summary>
    /// Removes all items from the collection
    /// </summary>
    void Clear();

    /// <summary>
    /// Determine if the data item is in the collection
    /// </summary>
    /// <param name="data">The item to look for </param>
    /// <returns>True if found, false otherwise</returns>
    bool Contains(T data);

    /// <summary>
    /// Determines if this instance is equal to another instance
    /// </summary>
    /// <param name="other">The data structure to compare to </param>
    /// <returns>True if equal, false otherwise</returns>
    bool Equals(object other);

    /// <summary>
    /// Remove the first instance of a value, if it exists
    /// </summary>
    /// <param name="data">The item to remove</param>
    /// <returns>Return true if an item was removed, false otherwise</returns>
    bool Remove(T data);

    /// <summary>
    /// A property used to access the number of elements in a collection
    /// </summary>
    /// <value> The number of items in the collection </value>
    int Count
    {
        get;
    }

}

