using System;
using System.Collections.Generic;


namespace Graphs;

public class Vertex<T> : IComparable<Vertex<T>> where T : IComparable<T>
{
    private T data; //the data the vertex stores
    private int index; //the index of the vertex in the vertex array

    public Vertex(int index, T data)
    {
        this.index = index;
        this.data = data;
    }

    //Properties 
    public T Data { get => data; }
    public int Index { get => index; set => index = value; }

    public int CompareTo(Vertex<T> other)
    {
        return Data.CompareTo(other.Data);
    }

    public override bool Equals(object obj)
    {
        return this.CompareTo((Vertex<T>)obj) == 0;
    }

    public override string ToString()
    {
        return "[" + data + "(" + index + ")]";
    }

}
