using System;
using System.Collections.Generic;


namespace Graphs;

public class Edge<T> : IComparable<Edge<T>> where T : IComparable <T>
{
    public Vertex<T> from;
    public Vertex<T> to;
    private bool isWeighted;
    private double weight;

    public Vertex<T> From { get => from; }
    public Vertex<T> To { get => to;}
    public bool IsWeighted { get => isWeighted;}
    public double Weight { get => weight;}

    /// <summary>
    /// This constructor will probably never be used directly, but we'll chain it with other
    /// constructors
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="weight"></param>
    /// <param name="isWeighted"></param>
    public Edge(Vertex<T> from, Vertex<T> to,  double weight, bool isWeighted)
    {
        this.from = from;
        this.to = to;
        this.isWeighted = isWeighted;
        this.weight = weight;
    }

    public Edge(Vertex<T> from, Vertex<T> to, double weight) :
        this(from, to, weight, true) { }

    public Edge(Vertex<T> from, Vertex<T> to) :
        this(from, to, double.PositiveInfinity, false) { }

    public int CompareTo(Edge<T> other)
    {
        int result = 0;
        //If the edges have weights, their weights should be compared
        if(other.isWeighted && this.isWeighted)
        {
            result = this.Weight.CompareTo(other.Weight);
        }
        //If they have teg same weight, ,aybe their equal
        if(result == 0)
        {
            //Compare the from vertices
            result = From.CompareTo(other.From);
            //if the from vertices are the same compare teh to's
            if(result == 0)
            {
                result = To.CompareTo(other.To);
            }
        }
        return result;
    }

    public override bool Equals(object obj)
    {
        return this.CompareTo((Edge<T>) obj) == 0;

    }

}
