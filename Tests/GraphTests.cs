using Graphs;
using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace Tests;

public class GraphAlgorithimTests
{
    DGraphMatrix<int> dgInts;
    UGraphMatrix<string> ugStrings;

    [SetUp]
    public void Setup()
    {
        dgInts = new DGraphMatrix<int>();
        dgInts.AddVertex(10);
        dgInts.AddVertex(11);
        dgInts.AddVertex(12);
        dgInts.AddVertex(13);
        dgInts.AddVertex(14);
        dgInts.AddVertex(20);
        dgInts.AddVertex(30);
        dgInts.AddVertex(40);
        dgInts.AddEdge(12, 14);
        dgInts.AddEdge(12, 10);
        dgInts.AddEdge(14, 11);
        dgInts.AddEdge(10, 11);
        dgInts.AddEdge(13, 14);
        dgInts.AddEdge(11, 20);
        dgInts.AddEdge(20, 30);
        dgInts.AddEdge(30, 13);
        dgInts.AddEdge(30, 12);
        dgInts.AddEdge(13, 12);
        dgInts.AddEdge(13, 10);

        //Creating Undirected Graph
        ugStrings = new UGraphMatrix<string>();
        ugStrings.AddVertex("B");
        ugStrings.AddVertex("C");
        ugStrings.AddVertex("X");
        ugStrings.AddVertex("Y");
        ugStrings.AddVertex("V");
        ugStrings.AddVertex("Q");
        ugStrings.AddVertex("A");
        ugStrings.AddEdge("B", "C", 10);
        ugStrings.AddEdge("B", "X", 6);
        ugStrings.AddEdge("C", "X", 16);
        ugStrings.AddEdge("X", "Y", 3);
        ugStrings.AddEdge("X", "Q", 20);
        ugStrings.AddEdge("Y", "A", 7);
        ugStrings.AddEdge("A", "V", 5);
        ugStrings.AddEdge("V", "Q", 4);
        ugStrings.AddEdge("X", "A", 8);

        
    }

    [Test]
    public void TestUGMMinimumSpanningTree()
    {

        UGraphMatrix<string> ugMST =
            (UGraphMatrix<string>)ugStrings.MinimumSpanningTree();
        Assert.IsTrue(ugMST.HasEdge("B", "C"));
        Assert.IsTrue(ugMST.HasEdge("B", "X"));
        Assert.IsTrue(ugMST.HasEdge("X", "Y"));
        Assert.IsTrue(ugMST.HasEdge("Y", "A"));
        Assert.IsTrue(ugMST.HasEdge("A", "V"));
        Assert.IsTrue(ugMST.HasEdge("V", "Q"));
        Assert.IsFalse(ugMST.HasEdge("X", "A"));
        Assert.IsFalse(ugMST.HasEdge("X", "C"));
        Assert.IsFalse(ugMST.HasEdge("X", "C"));
        Assert.IsFalse(ugMST.HasEdge("X", "Q"));
    }

    [Test]
    public void TestUGMergeGraphs()
    {
        //Create another graph to merge in
        UGraphMatrix<string> ugMore = new UGraphMatrix<string>();
        ugMore.AddVertex("M");
        ugMore.AddVertex("N");
        ugMore.AddEdge("M", "N", 30);
        ugStrings.MergeGraphs(ugMore);
        Assert.IsTrue(ugStrings.HasVertex("M"));
        Assert.IsTrue(ugStrings.HasVertex("N"));
        Assert.IsTrue(ugStrings.HasVertex("B"));
        Assert.IsTrue(ugStrings.HasVertex("C"));
        Assert.IsTrue(ugStrings.HasEdge("M", "N"));
        Assert.IsTrue(ugStrings.HasEdge("V", "Q"));
    }

    //This is still failing im not to sure why, i'll deal with this later
    [Test]
    public void TestShortestWeightedPath()
    {

        I_Graph<string> pathBtoQ = ugStrings.ShortestWeightedPath("B", "Q");
        Assert.IsTrue(pathBtoQ.HasEdge("B", "X"));
        Assert.IsTrue(pathBtoQ.HasEdge("X", "Y"));
        Assert.IsTrue(pathBtoQ.HasEdge("Y", "A"));
        Assert.IsTrue(pathBtoQ.HasEdge("A", "V"));
        Assert.IsTrue(pathBtoQ.HasEdge("V", "Q"));

        Assert.IsFalse(pathBtoQ.HasEdge("B", "C"));
        Assert.IsFalse(pathBtoQ.HasEdge("C", "X"));
        Assert.IsFalse(pathBtoQ.HasEdge("X", "Q"));

        Assert.IsFalse(pathBtoQ.HasVertex("C"));

    }
    [Test]
    public void TestDGBredthFirstTraversal()
    {
        Console.WriteLine("BreathFirstTraversal starting at 13");
        dgInts.BreadthFirstTraversal(13, printInt);
    }

    [Test]
    public void TestDGDepthFirstTraversal()
    {
        Console.WriteLine("Depth-First Traversal starting at 13");
        dgInts.DepthFirstTraversal(13, printInt);
    }

    private void printInt(int i)
    {
        
        Console.WriteLine(i);
    }
}



//This focues on our graph code
public class GraphClassTests
{
    DGraphMatrix<string> dgmString;
    UGraphMatrix<string> ugmString;

    [SetUp]
    public void Setup()
    {
        dgmString = new DGraphMatrix<string>();
        ugmString = new UGraphMatrix<string>();
    }

    [Test]
    public void TestUGMGetAllEdges()
    {
        ugmString.AddVertex("Yellow");
        ugmString.AddVertex("Red");
        ugmString.AddVertex("Green");
        ugmString.AddVertex("Blue");
        List<Edge<string>> edgesToAdd = new List<Edge<string>>();
        edgesToAdd.Add(new Edge<string>(new Vertex<string>(0, "Yellow"),
            new Vertex<string>(0, "Red")));
        edgesToAdd.Add(new Edge<string>(new Vertex<string>(0, "Red"),
            new Vertex<string>(0, "Green")));
        edgesToAdd.Add(new Edge<string>(new Vertex<string>(0, "Green"),
            new Vertex<string>(0, "Blue")));
        foreach (Edge<string> edge in edgesToAdd)
        {
            ugmString.AddEdge(edge.From.Data, edge.To.Data);
        }
        Assert.AreEqual(3, ugmString.NumEdges);
        //Let's get all the edges
        Edge<string>[] allEdges = ugmString.GetAllEdges();
        Console.WriteLine("All the edges in our DGM:");
        //Assert Via Eyeballs:
        foreach (Edge<string> edge in allEdges)
        {
            Console.WriteLine("Edge from " + edge.From + " to " + edge.to);
        }

    }


    [Test]
    public void TestDGMGetAllEdges()
    {
        dgmString.AddVertex("Yellow");
        dgmString.AddVertex("Red");
        dgmString.AddVertex("Green");
        dgmString.AddVertex("Blue");
        List<Edge<string>> edgesToAdd = new List<Edge<string>>();
        edgesToAdd.Add(new Edge<string>(new Vertex<string>(0, "Yellow"),
            new Vertex<string>(0, "Red")));
        edgesToAdd.Add(new Edge<string>(new Vertex<string>(0, "Red"),
            new Vertex<string>(0, "Green")));
        edgesToAdd.Add(new Edge<string>(new Vertex<string>(0, "Green"),
            new Vertex<string>(0, "Blue")));
        foreach(Edge<string> edge in edgesToAdd)
        {
            dgmString.AddEdge(edge.From.Data, edge.To.Data);
        }
        Assert.AreEqual(3, dgmString.NumEdges);
        //Let's get all the edges
        Edge<string>[] allEdges = dgmString.GetAllEdges();
        Console.WriteLine("All the edges in our DGM:");
        foreach(Edge<string> edge in allEdges)
        {
            Console.WriteLine("Edge from " + edge.From + " to " + edge.to);
            Assert.IsTrue(edgesToAdd.Contains(edge));
            edgesToAdd.Remove(edge);
        }
        Assert.AreEqual(0, edgesToAdd.Count);

    }

    [Test]
    public void TestUGMAddAndHasEdges()
    {
        ugmString.AddVertex("Yellow");
        ugmString.AddVertex("Red");
        ugmString.AddVertex("Green");
        ugmString.AddVertex("Blue");
        Assert.AreEqual(0, ugmString.NumEdges);
        ugmString.AddEdge("Yellow", "Red");
        ugmString.AddEdge("Red", "Blue");
        Assert.AreEqual(2, ugmString.NumEdges);
        Assert.IsTrue(ugmString.HasEdge("Yellow", "Red"));
        Assert.IsTrue(ugmString.HasEdge("Red", "Yellow"));
        Assert.IsFalse(ugmString.HasEdge("Yellow", "Blue"));

    }

    [Test]
    public void TestDGMAddAndHasVertex()
    {
        dgmString.AddVertex("Blue");
        dgmString.AddVertex("Green");
        dgmString.AddVertex("Red");
        Assert.Throws(typeof(ApplicationException), () => dgmString.AddVertex("Green"));
        Assert.AreEqual(true, dgmString.HasVertex("Blue"));
        Assert.AreEqual(true, dgmString.HasVertex("Green"));
        Assert.AreEqual(true, dgmString.HasVertex("Red"));
        Assert.AreEqual(false, dgmString.HasVertex("Purple"));
        Assert.AreEqual(3, dgmString.NumVertices);
    }

    [Test]
    public void TestDGMGetAndRemoveVertex()
    {
        dgmString.AddVertex("Blue");
        dgmString.AddVertex("Green");
        dgmString.AddVertex("Red");
        Vertex<string> v = dgmString.GetVertex("Green");
        Assert.AreEqual("Green", v.Data);
        Assert.Throws(typeof(ApplicationException), () => dgmString.GetVertex("Purple"));
        Assert.Throws(typeof(ApplicationException), () => dgmString.RemoveVertex("Purple"));
        dgmString.RemoveVertex("Green");
        Assert.Throws(typeof(ApplicationException), () => dgmString.GetVertex("Green"));
        Assert.AreEqual(false, dgmString.HasVertex("Green"));
        v = dgmString.GetVertex("Red");
        Assert.AreEqual("Red", v.Data);
        Assert.AreEqual(2, dgmString.NumVertices);


    }

    [Test]
    public void TestDGMAddAndHasEdge()
    {
        dgmString.AddVertex("Blue");
        dgmString.AddVertex("Green");
        dgmString.AddVertex("Red");
        Assert.AreEqual(0, dgmString.NumEdges);
        Assert.AreEqual(false, dgmString.HasEdge("Blue", "Red"));
        dgmString.AddEdge("Blue", "Red");
        Assert.AreEqual(1, dgmString.NumEdges);
        Assert.AreEqual(true, dgmString.HasEdge("Blue", "Red"));
        Assert.AreEqual(false, dgmString.HasEdge("Green", "Red"));
        dgmString.AddEdge("Green", "Red");
        Assert.AreEqual(2, dgmString.NumEdges);
        Assert.AreEqual(true, dgmString.HasEdge("Green", "Red"));
        Assert.AreEqual(false, dgmString.HasEdge("Red", "Green"));
        dgmString.AddEdge("Red", "Green");
        Assert.AreEqual(true, dgmString.HasEdge("Red", "Green"));

    }

    [Test]
    public void TestDGMGetEdge()
    {
        dgmString.AddVertex("Blue");
        dgmString.AddVertex("Green");
        dgmString.AddVertex("Red"); 
        dgmString.AddEdge("Blue", "Red");
        dgmString.AddEdge("Green", "Red");
        dgmString.AddEdge("Red", "Green");
        Edge<string> e = dgmString.GetEdge("Green", "Red");
        Assert.AreEqual("Green", e.From.Data);
        Assert.AreEqual("Red", e.To.Data);
        Assert.AreEqual(null, dgmString.GetEdge("Green", "Blue"));


    }

    [Test]
    public void TestDGMEnumerateNeighbours()
    {
        dgmString.AddVertex("Blue");
        dgmString.AddVertex("Green");
        dgmString.AddVertex("Red");
        dgmString.AddEdge("Blue", "Red");
        dgmString.AddEdge("Green", "Red");
        dgmString.AddEdge("Red", "Green");
        IEnumerable<Vertex<string>> myEnumerable = dgmString.EnumerateNeighbours("Blue");
        IEnumerator<Vertex<string>> myEnum = myEnumerable.GetEnumerator();
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual(new Vertex<string>(0, "Red"), myEnum.Current);
        Assert.AreEqual(false, myEnum.MoveNext());

        dgmString.AddEdge("Green", "Blue");
        myEnumerable = dgmString.EnumerateNeighbours("Green");
        List<Vertex<string>> vList = new List<Vertex<string>>();
        vList.Add(new Vertex<string>(0, "Blue"));
        vList.Add(new Vertex<string>(0, "Red"));
        foreach(Vertex<string> v in myEnumerable)
        {
            Assert.AreEqual(true, vList.Contains(v));
            vList.Remove(v);
        }
        Assert.AreEqual(0, vList.Count);
    }

    [Test]
    public void TestDGMRemoveVertexEdgeTest()
    {
        dgmString.AddVertex("Blue");
        dgmString.AddVertex("Green");
        dgmString.AddVertex("Red");
        dgmString.AddEdge("Blue", "Red");
        dgmString.AddEdge("Green", "Red");
        dgmString.AddEdge("Red", "Green");
        Assert.AreEqual(3, dgmString.NumEdges);
        dgmString.RemoveVertex("Green");
        Assert.AreEqual(true, dgmString.HasEdge("Blue", "Red"));
        Assert.AreEqual(false, dgmString.HasEdge("Green", "Red"));
        Assert.AreEqual(false, dgmString.HasEdge("Red", "Green"));
        Assert.AreEqual(1, dgmString.NumEdges);
        Edge<string> e = dgmString.GetEdge("Blue", "Red");
        Assert.AreEqual("Blue", e.From.Data);
        Assert.AreEqual("Red", e.To.Data);
    }

    

}
