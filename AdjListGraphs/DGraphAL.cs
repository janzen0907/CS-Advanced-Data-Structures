using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdjListGraphs
{
    public class DGraphAL<T> : AAdjListGraph<T> where T : IComparable<T>
    {
        public DGraphAL() 
        {
            isDirected = true;
        }

        public override Edge<T>[] GetAllEdges()
        {
            //List to store all edges
            List<Edge<T>> listEdges = new List<Edge<T>>();

            //Loop through the adj list
            for(int from = 0; from < adjList.Count; from++)
            {
                //Loop through the edges of the current vertex
                foreach(Edge<T> edge in adjList[from])
                {
                    listEdges.Add(edge);
                }
            }
            return listEdges.ToArray();
        }
    }
}
