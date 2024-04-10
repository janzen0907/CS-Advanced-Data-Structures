using System;
using System.Collections.Generic;

namespace BinarySearchTree;

public class Node<T> where T: IComparable<T>
{
    private T tData;
    private Node<T> nLeft;
    private Node<T> nRight;

    public T Data { get => tData; set => tData = value; }
    public Node<T> Left { get => nLeft; set => nLeft = value; }
    public Node<T> Right { get => nRight; set => nRight = value; }

    public Node() : this(default(T), null, null) { }
    public Node(T tData) : this(tData, null, null) { }
    public Node(T tData, Node<T> Left, Node<T> right)
    {
        this.tData = tData;
        this.nLeft = Left;
        this.nRight = Right;
    }

    public bool IsLeaf()
    {
        return Left == null && Right == null;
    }

    public override string ToString()
    {
        string sLeft = "";
        string sRight = "";

        if(Left != null) 
        {
            sLeft = "(" + Left + ")";
        }
        if(Right != null) 
        {
            sRight = "(" + Right + ")";
        }
        return "(" + Data + sLeft + sRight + ")";

    }
    
} 


