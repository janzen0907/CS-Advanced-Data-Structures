using System;
using System.Collections.Generic;
using DataStructuresCommon;

namespace Lists;

public interface I_List<T> : I_Collection<T> where T: IComparable<T>
{
    ///<summary>
    ///Fetches an element from a specific position in the list
    T ElementAt(int index);

    int IndexOf(T data);

    ///<summary>
    ///Insert an item at a particualr index
    ///</summary>
    ///<param name="index">where to insert</param>
    
    void Insert(int index, T data);

    ///<summary>
    ///Insert an item at a particular index
    ///</summary>
    /// <param name ="index">The index of the element to remove</param>
    T RemoveAt(int index);

    T ReplaceAt(int index, T data);

  
}