using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace Graphs;

public abstract class A_Graph<T> : I_Graph<T> where T : IComparable<T>
{
    //What will every graph need to keep track of?
    //We'll need a list of vertices
    protected List<Vertex<T>> vertices; //C# List class instead of our own
    //Store the number of edges
    protected int numEdges;
    //Is the graph directed or not?
    protected bool isDirected;
    //Is the graph weighted
    protected bool isWeighted;
    //Keep track of the indexes for every vertex, so we can look up the vertex
    protected Dictionary<T, int> revLookup; //This is a hashmap under the hood

    //Some helper methods to work with edges 
    protected abstract void AddVertexAdjustEdges(Vertex<T> v);
    protected abstract void RemoveVertexAdjustEdges(Vertex<T> v);
    //A AddEdge method that adds the edge object, our children will be responsible
    //for storing it
    public abstract void AddEdge(Edge<T> edge);

    //Other methods that our children will have to implement for us
    public abstract IEnumerable<Vertex<T>> EnumerateNeighbours(T data);
    public abstract bool HasEdge(T from, T to);
    public abstract Edge<T> GetEdge(T from, T to);
    public abstract void RemoveEdge(T from, T to);
    public abstract Edge<T>[] GetAllEdges();

    //Properties
    public int NumVertices 
    { 
        get
        {
            return vertices.Count;
        }
    }
    public virtual int NumEdges 
    { 
        get
        {
            return numEdges;
        }
    }

    //Edge methods
    public virtual void AddEdge(T from, T to)
    {
        //If the first edge is unweighted, the graph will be unweighted
        if(numEdges == 0)
        {
            isWeighted = false;
        }
        else if (isWeighted)
        {
            throw new ApplicationException("Can't add unweighted edge to a weighted graph");
        }
        //Create new edge object
        Edge<T> e = new Edge<T>(GetVertex(from), GetVertex(to));
        //Add the edge objkect to the hraph
        AddEdge(e);
    }

    public virtual void AddEdge(T from, T to, double weight)
    { 
        //If the first edge is weighted, then this graph will be weighted
        if(numEdges == 0)
        {
            isWeighted = true;
        }
        else if (!isWeighted)
        {
            throw new ApplicationException("Can't add weighted edged to unweighted graph");
        }
        //Create new edge object
        Edge<T> e = new Edge<T>(GetVertex(from), GetVertex(to), weight);
        //Add the edge objkect to the hraph
        AddEdge(e);

    }

    public A_Graph()
    {
        vertices = new List<Vertex<T>>();
        numEdges = 0;
        revLookup = new Dictionary<T, int>();
    }

    //Vertex Methods we're implementing 
    public void AddVertex(T data)
    {
        //Duplicate Vertexs will not be allowed
        //Use the has vertex method
        if(HasVertex(data))
        {
            throw new ApplicationException("Vertex already exists in graph");
        }

        //Create the new vertex
        Vertex<T> v = new Vertex<T>(vertices.Count, data);
        //Add it to the list of vertices
        vertices.Add(v);
        revLookup.Add(data, v.Index);
        AddVertexAdjustEdges(v);

    }

    public  IEnumerable<Vertex<T>> EnumerateVertices()
    {
        return vertices;
    }

    public  Vertex<T> GetVertex(T data)
    {
        //Data value and index
        //List of vertices, will contain the vertex objects
        //Dictionary(HashTable) called revLookup, Key is data value, Value is index
        //revLookup["Red"];
        //Lookup blue on the hashtable, get the value
            //Match that value to the vertex data
        if(!HasVertex(data))
        {
            throw new ApplicationException("Vertex " + data + " does not exist in graph");
        }
        int index = revLookup[data];
        return vertices[index];
    }
    public  void RemoveVertex(T data)
    { 
        //When we remove a vertex if we to decrement everything after the
        //data value by one
        //We also have to go through keys in the table and decrement them
        
            Vertex<T> v = GetVertex(data); 
            //Remove it from Lists of vertices
            //Remove it from HashTable
            vertices.Remove(v);
            revLookup.Remove(data);
            for(int i = v.Index; i < vertices.Count; i++)
            {
                //Decrement the index
                vertices[i].Index--;
                //Update the index stored in reverse lookup
                revLookup[vertices[i].Data]--;
            }
            //Remember to adjusst your edges!
            RemoveVertexAdjustEdges(v);
    }

    public  bool HasVertex(T data)
    {
        return revLookup.ContainsKey(data);
        //Return True if a vertex where data == T is in the list of verticies
        //return false otherwise
        //bool bFound = false;
        //int i = 0;
        ////while(!bFound && i < vertices.Count) 
        //{ 
           // if(vertices.ElementAt(i).Data.Equals(data))
           // {
              //  bFound =  true;
           // }

       // }
           //return false;

    }


    //Graph Algorithims
    public  I_Graph<T> MinimumSpanningTree()
    {
        //Keep track of the forest - the collection of subgraphs
        A_Graph<T>[] forest = new A_Graph<T>[NumVertices]; //Array of forests
        //Keep track of all the edges
        Edge<T>[] eArray = GetAllEdges(); //Array of all the edges
        //sort the edges by weight
        Array.Sort(eArray);

        //A_Graph<T> returnGraph = new A_Graph<T>(); 

        //Create the forest
        for(int i=0; i< NumVertices; i++)
        {
            //Just like before, we can use reflection to return an instance of the curretn type
            A_Graph<T> nGraph = 
                (A_Graph<T>)this.GetType().Assembly.CreateInstance(this.GetType().FullName);
            nGraph.AddVertex(vertices[i].Data);
            forest[i] = nGraph;
            
            //While there is more than 1 tree in the forest
        }

        int iCurrentEdge = 0;

        while (forest.Length > 1)
        {
           //  If we run out of edges throw an exception saying the graph is disconnected
            if (iCurrentEdge >=  eArray.Length)
            {
                throw new ApplicationException("The graph is disconnected");
            }
            Edge<T> e = eArray[iCurrentEdge++];
                //  Grab the next edge
                for (int i = 0; i < eArray.Length; i++)
                {
                    //  Figure out what tree in the forest the To Vertex is in

                    int fromVertexIndex = FindTree(e.From, forest);
                    int toVertexIndex = FindTree(e.To, forest);
                    //Vertex<T> fromVertex = FindTree(forest[i].GetVertex());
                    //  Figure out what tree in the forest the From Vertex is in
                    // Vertex<T> toVertex = FindTree(forest[i].GetVertex());
                    //      NOTE: we have written methods for these 2 above items
                    //  If they are in different trees
                    if (fromVertexIndex != toVertexIndex)
                    {
                    //      Merge the trees into one tree
                    forest[toVertexIndex].MergeGraphs(forest[fromVertexIndex]);
                    //      Add, to that merged graph, the edge that connected the two
                    forest[toVertexIndex].AddEdge(e.From.Data, e.To.Data, e.Weight);
                    //      Remove the graph we merged from because it is not a duplicate
                    forest = Timber(fromVertexIndex, forest);
                    }
                }

            }

        //Once there is only 1 tree in the forest, return that tree
        return forest[0];
    }

    /// <summary>
    /// Given an array of graphs and a vertex, find the index of the graph
    /// the vertex is in
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    private int FindTree(Vertex<T> v, A_Graph<T>[] forest)
    {
        //This was Wade's solution
        
        int i = 0;
        while (!forest[i].HasVertex(v.Data) && i++ < forest.Length) ;
        if(i == forest.Length)
        {
            throw new ApplicationException("Item was not found");
        }
        return i;
        
        /*  
        //variable for the index of the item
        int found = 0;
        //loop through the forest
        for(int i = 0;i< forest.Length;i++)
        {
            //check if v is equal to the current position in the forest
            if (forest[i].HasVertex(v.Data))
            {
                //We found it 
                found = i;
            }
            //We didnt find it, throw an exception
            else
            {
                throw new ApplicationException("Item was not found in the Graph");
            }
        }
        return found;
        */
    }

    /// <summary>
    /// Takes all the edges and vertices from another graphs and adds them to this one
    /// </summary>
    /// <param name="graphFrom"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void MergeGraphs(A_Graph<T> graphFrom)
    {
        //Get all the edges and vertices from graphFrom and add them to the new graph
         
        //for each through the graph to get all the edges and the vertices?
        foreach(Vertex<T> vertices in graphFrom.EnumerateVertices())
        {
            this.AddVertex(vertices.Data);
        }

        foreach(Edge<T> edges in graphFrom.GetAllEdges())
        {
            this.AddEdge(edges.From.Data, edges.To.Data, edges.Weight);
            
        }
    }

    /// <summary>
    /// Given a forest (array of trees), remove teh tree in the location indicated by tree cut
    /// </summary>
    /// <param name="treeCut"></param>
    /// <param name=""></param>
    /// <returns></returns>
    private A_Graph<T>[] Timber(int treeCut, A_Graph<T>[] forest)
    {
        //Create a new array one less than the origanal array
        A_Graph<T>[] tempForest = new A_Graph<T>[forest.Length - 1];
        //Counter to increment new array
        int j = 0;
        //Loop through old array
        for(int i=0; i<forest.Length; i++)
        {
            //Check if i is equal to tree cut
            if( i != treeCut )
            {
                //If its not the item we increment j and make it the item at i in forest
                tempForest[j++] = forest[i];
            }
        }
        //Return the graph with the cut down tree
        return tempForest;
    }



    public I_Graph<T> ShortestWeightedPath(T start, T end)
    {
        /*
         * vTable ->  array of VertexData objects so we can track distance, previous, and known
         *    - it'll have the same indexes as our vertices do
         *    - We'll start off by loading it up with all the vertices in the graph. Their initial
         *       data should infinite distance, null for previous, and false for known
         * startIndex -> where in vTable our starting point (our starting vertex).
         *    - set the starting vertex's data in vTable to have a distance of 0
         *    
         * pq -> priority queue
         * To start off, we'll add our starting vertex's data to the priority queue
         */
        VertexData[] vTable = new VertexData[NumVertices];
        int iStartIndex = GetVertex(start).Index;
        for (int i = 0; i < NumVertices; i++)
        {
            vTable[i] = new VertexData(vertices[i], double.PositiveInfinity, null);
        }

        vTable[iStartIndex].Distance = 0;

        PriorityQueue pq = new PriorityQueue();
        pq.Enqueue(vTable[iStartIndex]);

        /* while there are still VertexData objects in the priority queue
        *     current vertex --> priority queue dequeue
        *     if the current vertex is not known already
        *        set the current vertex's data to known
        *        for each neighbour, wVertex, of currentVertex
        *            w --> get the VertexData object from vTable for wVertex
        *            edge --> get the Edge object connecting the current vertex to wVertex
        *            proposed distance = current vertex's distance + the cost of the edge
        *            if w's current distance > proposed distance
        *                update our vertexdata table:
        *                w's distance = proposed distance
        *                w's previous = current vertex
        *                enqueue w (this will sort it back into the queue)
        * 
        * Use buildgraph to build the graph of the path, and return it.
        */

        while (!pq.IsEmpty())
        {
            VertexData vCurrent = pq.Dequeue();
            if (!vCurrent.Known)
            {
                vCurrent.Known = true;
                foreach (Vertex<T> wVertex in EnumerateNeighbours(vCurrent.Vertex.Data))
                {
                    VertexData wVertexData = vTable[wVertex.Index];
                    Edge<T> eEdge = GetEdge(vCurrent.Vertex.Data, wVertex.Data);
                    double dProposed = vCurrent.Distance + eEdge.Weight;
                    if (wVertexData.Distance > dProposed)
                    {
                        wVertexData.Distance = dProposed;
                        wVertexData.Previous = vCurrent.Vertex;
                        pq.Enqueue(wVertexData);
                    }
                }
            }
        }

        Console.WriteLine("Djisktra Table:");
        for (int j = 0; j < vTable.Length; j++)
        {
            Console.WriteLine(vTable[j].ToString());
        }


        return BuildGraph(this.GetVertex(end), vTable);
    }

    /// <summary>
    /// Take our table of vertex data and an endpoint, and turn it into
    /// a graph which contains only the most efficent path from the start
    /// to the end
    /// </summary>
    /// <param name="vEnd">Vertex to end at</param>
    /// <param name="vTable">The table of vertex data</param>
    /// <returns>Graph of the shortese weighted path</returns>
    /// <exception cref="NotImplementedException"></exception>
    private I_Graph<T> BuildGraph(Vertex<T> vEnd, VertexData[] vTable)
    {
        //Problem to solve:
        //We want to return an instace that is the same type as the class
        //this was called from
        //The problem is we don't know - at compile time - what the class is
        //Two potential solutions
        //1. We could create an abstract method where the child can return an
        //instace of itself
        //2. We could do C#'s relection to return an instance of the child
        //using GetType
        //Wont be asked about reflection for exam but should know for industry
        A_Graph<T> result = (A_Graph<T>)GetType().Assembly.CreateInstance(this.GetType().FullName);


        // Add the end vertex to the result (just using result.AddVertex)
        result.AddVertex(vEnd.Data);
        // dCurrent --> VertexData vTable[location of vEnd]
         VertexData dCurrent = vTable[vEnd.Index];
         // vPrevious --> dCurrent's previous 
          Vertex<T> vPrevious = dCurrent.Previous;
         // while vPrevious is not null
         while(vPrevious != null)
        {
            //  add vPrevious to the result graph
            result.AddVertex(vPrevious.Data);
            //  get the edge from dCurrent' Vertex to vPrevious, and add it to the graph
            Edge<T> eEdge = GetEdge(vPrevious.Data, dCurrent.Vertex.Data);
            result.AddEdge(eEdge.From.Data, eEdge.To.Data, eEdge.Weight);
            //  dCurrent --> vTable[location of dCurrent's previous]
            dCurrent = vTable[vPrevious.Index];
            //  vPrevious --> previous of dCurrent
            vPrevious = dCurrent.Previous;
            
        }
           
         // return the result
        return result;

    }

    /// <summary>
    /// We need a way to keep track of the tentative distance, the previous vertex
    /// and known values for each vertex.
    /// This a row from the table that Wade drew on the board 
    /// </summary>
    internal class VertexData : IComparable
    {
        public Vertex<T> Vertex;
        public double Distance;
        public bool Known;
        public Vertex<T> Previous;

        public VertexData(Vertex<T> vertex, double distance, Vertex<T> previous, bool known = false)
        {
            Vertex = vertex;
            Distance = distance;
            Known = known;
            Previous = previous;
        }

        public int CompareTo(object obj)
        {
            //Compare based on distance
            return this.Distance.CompareTo(((VertexData)obj).Distance);
        }

        public override string ToString()
        {
            return "Vertex: " + Vertex + " Distance: " + Distance + " Previous " +
                Previous + " Known " + Known;
        }
    }

    /// <summary>
    /// The purpose of this class is to provide a sorted list of values
    /// A call to dequeue will remove a value from teh list. Enqueue will add
    /// a value to the List. The list will re-sort after every additioin
    /// Queue sorted by a value that determines priority
    /// 
    /// This queue is used to storea all of the VertexData obkects that have had
    /// their tenative distance updated, but are still unknown
    /// </summary>
    internal class PriorityQueue
    {
        //We are not using generics for this because it is an internal class
        //that will store class specific items
        private List<VertexData> list;

        public PriorityQueue()
        {
            list = new List<VertexData>();
        }

        internal void Enqueue(VertexData vData)
        {
            list.Add(vData);
            //After every addition we will sort the list so that the lowest
            //distance vertex data is at the front of the queue
            //The efficency depends on the List's sort method
            list.Sort();

        }

        internal VertexData Dequeue()
        {
            //This simulates dequeue from the Queue data structure
            VertexData returnValue = list[0];
            list.RemoveAt(0);
            return returnValue;
        }

        internal bool IsEmpty()
        {
            if(list.Count > 0)
            {
                return false;
            }
            else 
            { 
                return true; 
            }
        }
    }



    //Class Notes: If 13 has 3 neighbors then we vists all of its neighbours first
    //The only real difference is if we storing the nodes we visit in a stack or a que
    public  void BreadthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
    {
        Vertex<T> vCurrent;
        Queue<Vertex<T>> verticesRemaining = new Queue<Vertex<T>>();
        Dictionary<T, T> visitedVertices = new Dictionary<T, T>();


        verticesRemaining.Enqueue(GetVertex(start));

        while (verticesRemaining.Count > 0)
        {
            vCurrent = verticesRemaining.Dequeue();
            if (!visitedVertices.ContainsKey(vCurrent.Data))
            {
                //    Mark the current item as visited
                visitedVertices.Add(vCurrent.Data, vCurrent.Data);
                //    Run the whatToDo method on the current item
                whatToDo(vCurrent.Data);
                //    For every one of the current vertex's neighbours:
                foreach (Vertex<T> v in EnumerateNeighbours(vCurrent.Data))
                {
                    verticesRemaining.Enqueue(v);
                }
            }

        }
    }
    //Notes: If we started with 14 we would go to one of 14's neighbour, then we visit the neighbours neighbor
    //Keep track of which nodes we have already visited 
    public void DepthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
    {
        // Things we need to keep track of:
        // * The current vertex we are looking at
        // * A stack of vertices we need to visit in the future
        // * a list or dictionary or whatever of the vertices we've already visited

        Vertex<T> vCurrent;
        Stack<Vertex<T>> verticesRemaining = new Stack<Vertex<T>>();
        Dictionary<T, T> visitedVertices = new Dictionary<T, T>();

        //
        // Push the vertex with the start data onto the stack
        verticesRemaining.Push(GetVertex(start));

        // While there are items in the stack:
        while (verticesRemaining.Count > 0)
        {
            //    The current item becomes the item popped off the stack
            vCurrent = verticesRemaining.Pop();
            if (!visitedVertices.ContainsKey(vCurrent.Data))
            {
                //    Mark the current item as visited
                visitedVertices.Add(vCurrent.Data, vCurrent.Data);
                //    Run the whatToDo method on the current item
                whatToDo(vCurrent.Data);
                //    For every one of the current vertex's neighbours:
                foreach (Vertex<T> v in EnumerateNeighbours(vCurrent.Data))
                {
                    //           Add the neighbour to the stack
                    verticesRemaining.Push(v);
                }
            }

        }
    }


}
