using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Graphs;

public abstract class A_GraphMatrix<T> : A_Graph<T> where T : IComparable<T>
{
    protected Edge<T>[,] matrix; //2D array in C#

    protected A_GraphMatrix()
    {
        matrix = new Edge<T>[0, 0];
    }

    //This does not complain as it is another abstract class. We are
    //going to implement some stuff in this class
    public override void AddEdge(Edge<T> edge)
    {
        //Check to make sure the edge doesn't exist. If it does, throw an exception
        //We are being passed in an edge object
        if (HasEdge(edge.From.Data, edge.To.Data))
        {
            //The edge already exists
            throw new ApplicationException("Edge from " + edge.From.Data + " to " +
                edge.To.Data + " already exists in the graph.");
        }
        //Add it inot our matrix of edges
        //Note Always FROM in the first dimension and To in the second dimension
        matrix[edge.From.Index, edge.To.Index] = edge;
        //Increment the edge count
        numEdges++;
    }

    //Gets all of the neighbours of a particulare vertex
    public override IEnumerable<Vertex<T>> EnumerateNeighbours(T data)
    {
        //Create a collection that IEnumerable to store the neighbours
        //(List will work for this)
        List<Vertex<T>> vList = new List<Vertex<T>>();
        //Someone is a neighbour if we can get to there from here
        //Go through the matrix and test for every possible edge from T data
        //NOTE: I was trying to loop through the entire table that was uneened
        Vertex<T> vFrom = GetVertex(data); //Get the data as this is the from
        for (int i = 0; i < matrix.GetLength(1); i++) //loop
        {
            if (matrix[vFrom.Index, i] != null)
            {
                //  If it does exist, add the other end of the edge to our list of neightbours
                vList.Add(matrix[vFrom.Index, i].To);
            }
        }

        //Return the list of neighbours
        return vList;
    }


    public override Edge<T> GetEdge(T from, T to)
    {
        return matrix[GetVertex(from).Index, GetVertex(to).Index];
    }

    public override bool HasEdge(T from, T to)
    {
        //Getting in the data values of two vertices
        //Check to make sure both vertices exist
        if (!HasVertex(from) || !HasVertex(to))
        {
            //If either of the vertices doesn't exist we know the edge doesn't exist
            return false;
        }
        //If either slot is not equal to null then that edge exists
        return matrix[GetVertex(from).Index, GetVertex(to).Index] != null;
    }

    public override void RemoveEdge(T from, T to)
    {
        throw new NotImplementedException();
    }

    //Called by the parent whenever a new vertex is added.
    // Space needs to be created for this vertex's potential edges
    //This involves expanding the array and copying over existing edges
    protected override void AddVertexAdjustEdges(Vertex<T> v)
    {
        //Reference to the old array
        Edge<T>[,] oldMatrix = matrix;
        //Create a new larger obkect
        matrix = new Edge<T>[NumVertices, NumVertices];
        //Then we copy the array
        //Get length 0 returns the first dimension of the array
        for (int x = 0; x < oldMatrix.GetLength(0); x++)
        {
            //Looping through every slot in the array
            for (int y = 0; y < oldMatrix.GetLength(1); y++)
            {
                //Copy the array
                matrix[x, y] = oldMatrix[x, y];
            }
        }
    }

    //We need to resize the array - this is called by our parent
    //when we remove a edge

    protected override void RemoveVertexAdjustEdges(Vertex<T> v)
    {
        // Reset your edge count
        numEdges = 0;
        // Create the new, smaller array
        Edge<T>[,] oldMatrix = matrix;
        matrix = new Edge<T>[NumVertices, NumVertices];

        // Go through every row and column in the
        // original table, using AddEdge to add it to the current (resized)
        // matrix. BUT skip over any edge that has the vertex being removed
        // as EITHER its to or its from
        for (int x = 0; x < oldMatrix.GetLength(0); x++)
        {
            for (int y = 0; y < oldMatrix.GetLength(1); y++)
            {
                // Check to see if an edge exists in the old matrix
                if (oldMatrix[x, y] != null)
                {
                    // Skip over the row and column of the removed vertex
                    if (x != v.Index && y != v.Index)
                    {
                        AddEdge(oldMatrix[x, y]);
                    }
                }
            }
        }

    }
}

