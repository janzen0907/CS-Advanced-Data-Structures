using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Graphs;

//A delegate that will be used to process nodes during traversals
public delegate void VisitorDelegate<T>(T data);

public interface I_Graph<T> where T : IComparable<T>

{
    //We are not worried about Implentation details in an interface
    int NumVertices { get; }
    int NumEdges { get; }

    //Methods that work with vertices
    void AddVertex(T data);
    void RemoveVertex(T data);
    bool HasVertex(T data);
    Vertex<T> GetVertex(T data);
    //Return a collection enumeration all vertices
    IEnumerable<Vertex<T>> EnumerateVertices();
    //Sometimes we'll want to get the neighbours of a specific vertex
    IEnumerable<Vertex<T>> EnumerateNeighbours(T data);
    //We will not allow duplicates in our graphs

    //Methods that work with edges
    void AddEdge(T from, T to);
    void AddEdge(T from, T to, double weight);  
    void RemoveEdge(T from, T to);
    bool HasEdge(T from, T to);
    Edge<T> GetEdge(T from, T to);

    //Methods for some essential graph algorithims
    //AKA common operations we want to do with graphs
    I_Graph<T> ShortestWeightedPath(T start, T end);
    //Remove an unecessary edges
    I_Graph<T> MinimumSpanningTree();
    void DepthFirstTraversal(T start, VisitorDelegate<T> whatToDo);
    void BreadthFirstTraversal(T start, VisitorDelegate<T> whatToDo);
}
