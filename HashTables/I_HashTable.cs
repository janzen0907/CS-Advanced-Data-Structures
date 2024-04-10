using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HashTables;
// K -> Generic Value representing the data type of the key
// V ->> Generic Value representing the data type of th value
public interface I_HashTable<K, V> : IEnumerable<V>
    where K : IComparable<K>
{
    /// <summary>
    /// A sorted array of all the keys in the hashtable
    /// </summary>
    public K[] Keys { get; }

    /// <summary>
    /// Return a value from the hashtable
    /// </summary>
    /// <param name="key">The key of the value to return</param>
    /// <returns>The value</returns>
    V Get(K key);

    /// <summary>
    /// Add the key and the value as a key-value pair to the hashtable
    /// </summary>
    /// <param name="key">Determines the location in the hashtable</param>
    /// <param name="value">Value to store at that location</param>
    // If yu weren't writing this generic, and you knew in advance what type of object
    //you'd be storing in the hashtable, and you knew the key was going to be one of the 
    //fields in the object, you could just pass in the object. Like if I was storing objects of 
    //type Student, and using Student's StudentNumber as the key, I could just do 
    //Add(Student stu) and use the stu.StudentNumber as the key
    void Add(K key, V value);

    /// <summary>
    /// Remove the value associated with the key passed in
    /// </summary>
    /// <param name="key">The key of the value to remove</param>
    /// <returns>The removed value</returns>
    V Remove(K key);

    /// <summary>
    /// Removes all key-value pairs from the hashtable and intializes to
    /// the default array size
    /// </summary>
    void Clear();

}
