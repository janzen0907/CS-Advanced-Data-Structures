using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Graphs;

public class UGraphMatrix<T> : A_GraphMatrix<T> where T : IComparable<T>
{
    public UGraphMatrix()
    {
        isDirected = false;
    }

    public override int NumEdges
    {
        get
        {
            //Because there are two copies of every edge, we need to
            // divide by two to get our edge count
            return base.numEdges / 2; 
        }
    }

    //Since this is undirected, when a user adds an edge, we add it in both directions
    public override void AddEdge(T from, T to)
    {
        //Add hte edge in both direction
        //Base is similar to super in java
        base.AddEdge(from, to);
        base.AddEdge(to, from);
    }

    public override void AddEdge(T from, T to, double weight)
    {
        base.AddEdge(from, to, weight);
        base.AddEdge(to, from, weight);
    }

    public override Edge<T>[] GetAllEdges()
    {
        //Return a list of all the edge
        List<Edge<T>> edges = new List<Edge<T>>(); 
        //Use the indexes we want to vist
        for(int from=0; from<matrix.GetLength(0); from++) 
        { 
            for(int to = from + 1; to<matrix.GetLength(1); to++)
            {
                if (matrix[from, to] != null)
                {
                    edges.Add(matrix[from, to]);
                }
            }
        }
        //But each edge should only be returned once
        //if you return A->V, don't also return B->A
        //either do the from or to portion of the array dont do both
        return edges.ToArray();

    }
}
