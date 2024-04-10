using System;
using System.Collections.Generic;
using DataStructuresCommon;

namespace BinarySearchTree;

//Define a delegate type that will point to a method that will
//perform some action on a data member of type T
public delegate void ProcessData<T>(T data);

public enum TRAVERSALORDER {PRE_ORDER, IN_ORDER, POST_ORDER};



    public interface I_BST<T> : I_Collection<T> where T : IComparable<T>
    {
    /// <summary>
    /// Given a data element, find the corresponding element of equal value
    /// </summary>
    /// <param name="data">The item to find</param>
    /// <returns>A reference to the item if found, otherwise returns 
    /// default value for type T</returns>
    T Find(T data);

    /// <summary>
    /// Returns the height of the tree
    /// </summary>
    /// <returns>The heigh of the tree - that is, the length 
    /// of path of the deepest node to teh root</returns>
    /// Could have made it a property instead
    int Height();

    /// <summary>
    /// Similiar to an enumerator, but instead of having a 
    /// bunch of methods to get each item, we
    /// pass in a method to run against each item 
    /// </summary>
    /// <param name="pd"></param>
    /// <param name="order"></param>
    void Iterate(ProcessData<T> pd, TRAVERSALORDER order);

    }
