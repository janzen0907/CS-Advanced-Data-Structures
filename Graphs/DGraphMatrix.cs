using System;
using System.Collections.Generic;


namespace Graphs;

public class DGraphMatrix<T> : A_GraphMatrix<T> where T : IComparable<T>
{
    public DGraphMatrix()
    {
        isDirected = true;
    }

    public override Edge<T>[] GetAllEdges()
    {
        //Create a array or a List (might be easier to store edges)
        List<Edge<T>> listEdges = new List<Edge<T>>();
        //Loop through every edge in the matrix
        for(int from=0; from<matrix.GetLength(0); from++)
        {
            for(int to=0; to<matrix.GetLength(1); to++)
            {
                if (matrix[from, to] != null)
                {
                    listEdges.Add(matrix[from,to]);
                }
            }
        }
        
        //Return the list in Array form(List has a ToArray() method)
        return listEdges.ToArray();
    }
}
    

