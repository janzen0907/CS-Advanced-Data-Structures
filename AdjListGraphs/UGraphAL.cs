using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdjListGraphs;

public class UGraphAL<T> : AAdjListGraph<T> where T : IComparable<T>

{
    public UGraphAL()
    {
        isDirected = false;
    }

    public override int NumEdges {get {  return base.NumEdges/ 2;}}

    public override void AddEdge(T from, T to)
    {
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

        //List to store edges
        List<Edge<T>> edges = new List<Edge<T>>();

        //Loop through the adj list
        for (int from = 0; from < adjList.Count; from++)
        {
            //Loop through the edges at current vertex
            foreach (Edge<T> edge in adjList[from])
            {
                edges.Add(edge);
            }
        }
        return edges.ToArray();
    }
}
