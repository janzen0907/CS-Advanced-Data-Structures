using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography;
using BinarySearchTree;
using Lists;


namespace AVLTTests;

public class AVLTTEsts
{
    AVLT<string> sAVLT;
    AVLT<Student> stuAVLT;
    ArrayList<string> aList;



    [SetUp]
    public void SetUp()
    {
        sAVLT = new AVLT<string>();
        sAVLT.Add("Mango");
        sAVLT.Add("Apple");
        sAVLT.Add("Banana");
        sAVLT.Add("Plum");
        sAVLT.Add("Kiwi");
        sAVLT.Add("Pear");
        stuAVLT = new AVLT<Student>();
        stuAVLT.Add(new Student("Fred", 501, "Fred@hotmail.com"));
        stuAVLT.Add(new Student("Hu", 133, "Hu@gmail.com"));
        stuAVLT.Add(new Student("Nancy", 613, "nancy@saskpolytech.ca"));
        stuAVLT.Add(new Student("Ahmed", 189, "ahmed@gmail.com"));
        stuAVLT.Add(new Student("Jane", 648, "jane@gmail.com"));
    }


    //I coudld't figure out what i was doing wrong as far as these tests go
    //I'm not to sure if its logic error in my Iterative Method or if i just don't understand
    //How to properly test something like this. 
    //Adding some tests for our orginal iterate to ensure that my implemetation of it
    //for the AVLT class uses the same data and will pass as well
    
    [Test]
    public void TestPreOrderTraversal()
    { 
        //create an empty AVLT
        AVLT<string> addInto = new AVLT<string>();

        // I used arrow notation so that i could use the data of our original sBST
        //then add it into the empty AVLT i created. I then can ensure the traversal is 
        //working as intended
      
        sAVLT.Iterate((data) => addInto.Add(data), TRAVERSALORDER.PRE_ORDER);

        //sAVLT.Iterate(sAVLT.Iterate((sAVLT.Data), TRAVERSALORDER.PRE_ORDER));

        Assert.AreEqual(6, addInto.Count);
        Assert.AreEqual("Mango", addInto.ElementAt(0));
        Assert.AreEqual("Apple", addInto.ElementAt(1));
        Assert.AreEqual("Banana", addInto.ElementAt(2));
        Assert.AreEqual("Kiwi", addInto.ElementAt(3));
        Assert.AreEqual("Plum", addInto.ElementAt(4));
        Assert.AreEqual("Pear", addInto.ElementAt(5));

    }
    
    //Im really not sure how to test this, it seems to be working in the BST(other than post order)
    //class but not here. Maybe im thinking of the testing wrong as an AVL is self 
    //balancing. Thus I am unsure if adding the elements then checking them at certain
    //indexs is even the right way to go about this.
    [Test]
    public void TestInOrderTraversal()
    {
        
        
        //Empty list to hold our value
        //Tried to do the testing with a list in this instace but also, did not seem to work
        //as expected
        List<string> addInto = new List<string>();    

        
        sAVLT.Iterate((data) => addInto.Add(data), TRAVERSALORDER.IN_ORDER);
        Console.WriteLine(addInto);

        List<string> expected = new List<string> {"Apple", "Banana", "Kiwi", "Mango", "Pear", "Plum" };
        Assert.AreEqual(expected, addInto);
        Assert.AreEqual(6, addInto.Count);
   
    }

    [Test]
    public void TestPostOrderTraversal()
    {
        
        List<string> addInto = new List<string>(); ;

        sAVLT.Iterate((data) => addInto.Add(data), TRAVERSALORDER.POST_ORDER);

        Assert.AreEqual(6, addInto.Count);
        Assert.AreEqual("Kiwi", addInto.ElementAt(0));
        Assert.AreEqual("Banana", addInto.ElementAt(1));
        Assert.AreEqual("Apple", addInto.ElementAt(2));
        Assert.AreEqual("Plum", addInto.ElementAt(3));
        Assert.AreEqual("Pear", addInto.ElementAt(4));
        Assert.AreEqual("Mango", addInto.ElementAt(5));
    }



    [Test]
    public void TestAVLTSpeed()
    {
        //How many numbers to add
        int toAdd = 5000;
        //The largest number to generate
        int iLargest = toAdd * 10;
        Random randA = new Random(9999);
        Random randB = new Random(9999);

        BST<int> iBST = new BST<int>();
        AVLT<int> iAVLT = new AVLT<int>();

        //Vars to record start and stop time 
        long endBSTAdd;
        long endAVLTAdd;
        long startBSTAdd;
        long startAVLTAdd;
        long startBSTFind;
        long endBSTFind;
        long startAVLTFind;
        long endAVLTFind;

        //Start timer
        startBSTAdd = Environment.TickCount;
        for(int i=0; i< toAdd; i++)
        {
            iBST.Add(randA.Next(1, iLargest));
        }
        //end timer
        endBSTAdd = Environment.TickCount;

        startAVLTAdd = Environment.TickCount;
        for(int i = 0; i < toAdd; i++)
        {
            iAVLT.Add(randB.Next(1, iLargest));
        }
        endAVLTAdd = Environment.TickCount;

        Console.WriteLine("Adding {0} of random numbers took the bst" +
            "{1} tick and took the AVLT {2} ticks", toAdd, endBSTAdd -
            startBSTAdd, endAVLTAdd - startAVLTAdd);

        Console.WriteLine("The height of the BST is {0} and the height of " +
            "the AVLT is {1}", iBST.Height(), iAVLT.Height());

        //the main difference in speed between the AVLT and the BST
        //should be the search time
        
        randA = new Random(9999);
        randB = new Random(9999);

        

        //Start searching the bst
        startBSTFind = Environment.TickCount;
        for(int i=0;i<toAdd; i++)
        {
            iBST.Find(randA.Next(1, iLargest));
        }
        endBSTFind = Environment.TickCount;

        startAVLTFind = Environment.TickCount;
        for( int i = 0;i<toAdd; i++)
        {
            iAVLT.Find(randB.Next(1, iLargest));
        }
        endAVLTFind = Environment.TickCount;

        Console.WriteLine("Finding {0} of random numbers took the bst " +
            "{1}tick and took the AVLT {2} ticks ", toAdd, endBSTFind - startBSTFind
            , endAVLTFind - startAVLTFind);

        //For some reason this breaks my test
        iBST.Clear();
        iAVLT.Clear();


        startBSTAdd = Environment.TickCount;
        for (int i = 0; i < toAdd; i++)
        {
            iBST.Add(i);
        }
        //end timer
        endBSTAdd = Environment.TickCount;

        startAVLTAdd = Environment.TickCount;
        for (int i = 0; i < toAdd; i++)
        {
            iAVLT.Add(i);
        }
        endAVLTAdd = Environment.TickCount;

        Console.WriteLine("Adding {0} of Sequential numbers took the bst" +
            "{1} tick and took the AVLT {2} ticks", toAdd, endBSTAdd -
            startBSTAdd, endAVLTAdd - startAVLTAdd);


        //Start searching the bst
        startBSTFind = Environment.TickCount;
        for (int i = 0; i < toAdd; i++)
        {
            iBST.Find(i);
        }
        endBSTFind = Environment.TickCount;

        startAVLTFind = Environment.TickCount;
        for (int i = 0; i < toAdd; i++)
        {
            iAVLT.Find(i);
        }
        endAVLTFind = Environment.TickCount;

        Console.WriteLine("Finding {0} of Sequential numbers took the bst " +
            "{1}tick and took the AVLT {2} ticks ", toAdd, endBSTFind - startBSTFind
            , endAVLTFind - startAVLTFind);

        Console.WriteLine("The height of the BST is {0} and the height of " +
            "the AVLT is {1}", iBST.Height(), iAVLT.Height());
    }

    [Test]
    public void TestBalancing()
    {
        Assert.AreEqual(2, sAVLT.Height());
        AVLT<int> iAVLT = new AVLT<int>();
        iAVLT.Add(1);
        iAVLT.Add(2);
        iAVLT.Add(3);
        iAVLT.Add(4);
        iAVLT.Add(5);
        Assert.AreEqual(2, iAVLT.Height());
    }

    [Test]
    public void TestLeftRotation()
    {
        //To start with the tree will look like this
        // 1
        //  \
        //   2
        //  / \
        //  5  3
        //      \
        //       4

        Node<string> root = new Node<string>("First");
        Node<string> second = new Node<string>("Second");
        Node<string> third = new Node<string>("Third");
        Node<string> fourth = new Node<string>("Fourth");
        Node<string> fifth = new Node<string>("Fifth");

        root.Right = second;
        second.Right = third;
        second.Left = fifth;
        third.Right = fourth;

        //Now we will do the rotation
        root = AVLT<string>.SingleLeft(root);
        Assert.AreEqual("Second", root.Data);
        Assert.AreEqual("First", root.Left.Data);
        Assert.AreEqual("Third", root.Right.Data);
        Assert.AreEqual("Fourth", root.Right.Right.Data);
        Assert.AreEqual("Fifth", root.Left.Right.Data);
    }

    [Test]
    public void TestRightRotation()
    {

        Node<string> root = new Node<string>("First");
        Node<string> second = new Node<string>("Second");
        Node<string> third = new Node<string>("Third");
        Node<string> fourth = new Node<string>("Fourth");
        Node<string> fifth = new Node<string>("Fifth");

        root.Left = second;
        second.Left = third;
        second.Right = fifth;
        third.Left = fourth;

        //Now we will do the rotation
        root = AVLT<string>.SingleRight(root);
        Assert.AreEqual("Second", root.Data);
        Assert.AreEqual("First", root.Right.Data);
        Assert.AreEqual("Third", root.Left.Data);
        Assert.AreEqual("Fourth", root.Left.Left.Data);
        Assert.AreEqual("Fifth", root.Right.Left.Data);
        Assert.AreEqual("Fifth", root.Right.Left.Data);
    }

    [Test]
    public void TestDoubleLeft()
    {
        //To start with the tree will look like this
        //  A
        //   \
        //    B
        //   /
        //  C
        Node<string> root = new Node<string>("A");
        root.Right = new Node<string>("B");
        root.Right.Left = new Node<string>("C");

        root = AVLT<string>.DoubleLeft(root);
        //First the rotation will do this
        //  A
        //   \
        //    C
        //     \
        //      B

        //Now the tree should look like this:
        //   C
        //  / \
        // A   B
        Assert.AreEqual("C", root.Data);
        Assert.AreEqual("A", root.Left.Data);
        Assert.AreEqual("B", root.Right.Data);

    }

    [Test]
    public void TestDoubleRight()
    {
      
        Node<string> root = new Node<string>("A");
        root.Left = new Node<string>("B");
        root.Left.Right = new Node<string>("C");

        root = AVLT<string>.DoubleRight(root);
        

        Assert.AreEqual("C", root.Data);
        Assert.AreEqual("A", root.Right.Data);
        Assert.AreEqual("B", root.Left.Data);

    }


    [Test]
    public void TestAVLTRemove()
    {
        Assert.AreEqual(true, sAVLT.Remove("Pear"));
        Assert.AreEqual(5, sAVLT.Count);
        Assert.AreEqual(false, sAVLT.Remove("Peach"));
        Assert.AreEqual(5, sAVLT.Count);
        Assert.AreEqual(true, sAVLT.Remove("Mango"));
        Assert.AreEqual(4, sAVLT.Count);
        try
        {
            sAVLT.Find("Mango");
            Assert.Fail();
        }
        catch (ApplicationException ex) 
        {
            Assert.AreEqual("Mango was not found in tree", ex.Message);
        }
        Assert.AreEqual(true, stuAVLT.Remove(new Student(null, 501, null)));
        Assert.AreEqual(4, stuAVLT.Count);
    }


    [Test]
    public void TestAVLTFind()
    {
        Student fred = stuAVLT.Find(new Student(null, 501, null));
        Assert.AreEqual("Fred", fred.Name);
        Assert.AreEqual("Fred@hotmail.com", fred.Email);
        Student jane = stuAVLT.Find(new Student(null, 648, null));
        Assert.AreEqual("Jane", jane.Name);
        Assert.AreEqual("jane@gmail.com", jane.Email);
    }

    [Test]
    public void TestAVLTCreation()
    {
        AVLT<string> AVLT = new AVLT<string>();
    }

    [Test]
    public void TestAVLTAdd()
    {
        AVLT<string> AVLT = new AVLT<string>();
        Assert.AreEqual(0, AVLT.Count);
        AVLT.Add("Mango");
        AVLT.Add("Apple");
        AVLT.Add("Banana");
        AVLT.Add("Plum");
        AVLT.Add("Kiwi");
        AVLT.Add("Pear");
        Assert.AreEqual(6, AVLT.Count);
    }

    [Test]
    public void TestAVLTClear()
    {
        Assert.AreEqual(6, sAVLT.Count);
        sAVLT.Clear();
        Assert.AreEqual(0, sAVLT.Count);
    }

    [Test]
    public void TestFindSmallest()
    {
        Assert.AreEqual("Apple", sAVLT.FindSmallest());
        sAVLT.Add("Acai");
        Assert.AreEqual("Acai", sAVLT.FindSmallest());

    }

    [Test]
    public void TestFindLargest()
    {
        Assert.AreEqual("Plum", sAVLT.FindLargest());
        sAVLT.Add("Strawberry");
        Assert.AreEqual("Strawberry", sAVLT.FindLargest());

    }

    private class Student : IComparable<Student>
    {
        private string name;
        private int studentNumber;
        private string email;

        public string Name { get => name; set => name = value; }
        public int StudentNumber { get => studentNumber; set => studentNumber = value; }
        public string Email { get => email; set => email = value; }

        public Student(string name, int studentNumber, string email)
        {
            this.name = name;
            this.studentNumber = studentNumber;
            this.email = email;
        }

        // I will compare students based on student number
        public int CompareTo(Student other)
        {
            if (studentNumber < other.studentNumber) return -1;
            if (studentNumber > other.studentNumber) return 1;
            return 0;
        }
    }

    public void WriteData(string data)
    {
        Console.WriteLine(data);
        aList.Add(data);   
    }
}

