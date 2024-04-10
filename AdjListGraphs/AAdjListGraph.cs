using Graphs;
using Lists;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace AdjListGraphs;

public class AAdjListGraph<T> : Graphs.A_Graph<T> where T : IComparable<T>

{
    //Adjancy list
    protected List<List<Edge<T>>> adjList;

    //Constructor
    public AAdjListGraph() : base()
    {
        adjList = new List<List<Edge<T>>>();
    }
    
    /// <summary>
    /// Add an edge to the graph
    /// </summary>
    /// <param name="edge"></param>
    public override void AddEdge(Edge<T> edge)
    {
        //Check to see if the edge exists. If not exception
        if(HasEdge(edge.from.Data, edge.to.Data))
        {
            //already exists
            throw new ApplicationException("Edge from " + edge.From.Data + " to " +
                edge.To.Data + " already exists in the graph.");
        }

        //Add the edge into the list first the index
        int fromIndex = revLookup[edge.From.Data];
        int toIndex = revLookup[edge.To.Data];

        //Only add the From index in the event it is an directed graph
        //In the UGraph class we can add the to edge
        adjList[fromIndex].Add(edge);

        //adjList[toIndex].Add(edge); //This would only be true for undirected
        numEdges++;

    }
    
    /// <summary>
    /// This method will enumerate all the neighbours of a vertex
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public override IEnumerable<Vertex<T>> EnumerateNeighbours(T data)
    {
        //Collection to store the neighbours
        List<Vertex<T>> vList = new List<Vertex<T>>();
        //Get the index of the vertex being passed in
        int index = revLookup[data];
        //Loop through adj list and add the edges to the list of neighbours
        foreach (Edge<T> edge in adjList[index])
        {
            if(edge != null)
            {
                vList.Add(edge.To);
            }
        }

        return vList;
        

    }

    /// <summary>
    /// This is implemented in the child classes
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override Edge<T>[] GetAllEdges()
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// This will get the edge between two vertices
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public override Edge<T> GetEdge(T from, T to)
    {
        //Get the index for the from vertex
        int fromIndex = revLookup[from];

        //loop through the adj list 
        foreach(Edge<T> edge in adjList[fromIndex])
        {
            //If the to vertex of the vertex is the to vertex we want, return that edge
            if(edge.To.Data.Equals(to))
            {
                return edge;
            }

        }
        //If no edge was found  eturn null
        return null;
    }

    public override bool HasEdge(T from, T to)
    {
        //Check to make sure both vertices exist
        if(!HasVertex(from) || !HasVertex(to))
        {
            return false;
        }
        //Get the index 
        int fromIndex = revLookup[from];

        //loop through the adj list from vertex
        foreach(Edge<T> edge in adjList[fromIndex])
        {
            //We found the edge
            if(edge.To.Data.Equals(to))
            {
                //The edge is there
                return true;
            }
        }
        //No edge found
        return false;
    }

    public override void RemoveEdge(T from, T to)
    {
        //Make sure they exist
        if(!HasVertex(from) || ! HasVertex(to))
        {
            //The edge doesn't exist
            throw new ApplicationException("The edge does not exist");
        }
        //Get the index
        int fromIndex = revLookup[from];

        //Loop through the adj list with the from vertex
        for(int i =0; i< adjList[fromIndex].Count; i++)
        {
            //Check that the to vertex matches up with whats being passed in
            if (adjList[fromIndex][i].To.Data.Equals(to))
            {
                adjList[fromIndex].RemoveAt(i);
                //if we removed it we can break, the method is done
                break;
            }
        }
    }

    protected override void AddVertexAdjustEdges(Vertex<T> v)
    {
        //Add a new list when adding a new vertex
        adjList.Add(new List<Edge<T>>());

    }

    protected override void RemoveVertexAdjustEdges(Vertex<T> v)
    {
        numEdges = 0;
        //Get the index of the vertex being removed
        int vIndex = v.Index;
        //remove the adj list for that vertex
        adjList.RemoveAt(vIndex);

    }

}