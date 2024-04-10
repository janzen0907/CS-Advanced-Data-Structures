using System;
using System.Collections.Generic;
using System.Linq;
using BinarySearchTree;
using Lists;

namespace Tests;

public class BSTTests
{
    BST<string> sBST;
    BST<Student> stuBST;
    ArrayList<string> aList;



    [SetUp]
    public void SetUp()
    {
        sBST = new BST<string>();
        sBST.Add("Mango");
        sBST.Add("Apple");
        sBST.Add("Banana");
        sBST.Add("Plum");
        sBST.Add("Kiwi");
        sBST.Add("Pear");
        stuBST = new BST<Student>();
        stuBST.Add(new Student("Fred", 501, "Fred@hotmail.com"));
        stuBST.Add(new Student("Hu", 133, "Hu@gmail.com"));
        stuBST.Add(new Student("Nancy", 613, "nancy@saskpolytech.ca"));
        stuBST.Add(new Student("Ahmed", 189, "ahmed@gmail.com"));
        stuBST.Add(new Student("Jane", 648, "jane@gmail.com"));
    }

    //Adding some tests for our orginal iterate to ensure that my implemetation of it
    //for the AVLT class uses the same data and will pass as well
    //[Test]
    
    public void TestPreOrderTraversal()
    {
        //create an empty BST
        BST<string> addInto = new BST<string>();

        // I used arrow notation so that i could use the data of our original sBST
        //then add it into the empty BST i created. I then can ensure the traversal is 
        //working as intended
        sBST.Iterate((data) => addInto.Add(data), TRAVERSALORDER.PRE_ORDER);

        Assert.AreEqual("Mango", addInto.ElementAt(0));
        Assert.AreEqual("Apple", addInto.ElementAt(1));
        Assert.AreEqual("Banana", addInto.ElementAt(2));
        Assert.AreEqual("Kiwi", addInto.ElementAt(3));
        Assert.AreEqual("Plum", addInto.ElementAt(4));
        Assert.AreEqual("Pear", addInto.ElementAt(5));
        
    }
    

    [Test]
    public void TestInOrderTraversal()
    {
        //create an empty BST
        BST<string> addInto = new BST<string>();

        // I used arrow notation so that i could use the data of our original sBST
        //then add it into the empty BST i created. I then can ensure the traversal is 
        //working as intended
        sBST.Iterate((data) => addInto.Add(data), TRAVERSALORDER.IN_ORDER);

        
        Assert.AreEqual("Apple", addInto.ElementAt(0));
        Assert.AreEqual("Banana", addInto.ElementAt(1));
        Assert.AreEqual("Kiwi", addInto.ElementAt(2));
        Assert.AreEqual("Mango", addInto.ElementAt(3));
        Assert.AreEqual("Pear", addInto.ElementAt(4));
        Assert.AreEqual("Plum", addInto.ElementAt(5));
        
    }

    
     //Unsure if the postorder from class is broken or what but the order seems off
    [Test]
    public void TestPostOrderTraversal()
    {
        //create an empty BST
        BST<string> addInto = new BST<string>();

        // I used arrow notation so that i could use the data of our original sBST
        //then add it into the empty BST i created. 

        sBST.Iterate((data) => addInto.Add(data), TRAVERSALORDER.POST_ORDER);
        Assert.AreEqual("Kiwi", addInto.ElementAt(0));
        Assert.AreEqual("Banana", addInto.ElementAt(1));
        Assert.AreEqual("Apple", addInto.ElementAt(2));
        Assert.AreEqual("Plum", addInto.ElementAt(3));
        Assert.AreEqual("Pear", addInto.ElementAt(4));
        Assert.AreEqual("Mango", addInto.ElementAt(5));
    }
    







    [Test]
    //This will test the creation of the LL and ensure the largest element is 
    //in position 0 and the smallest element is in the final position
    public void TestCreatingDescendingLL()
    {
        Lists.LinkedList<string> list = sBST.ToLinkedList();
        Assert.AreEqual(6, list.Count);
        Assert.AreEqual("Plum", list.ElementAt(0));
        Assert.AreEqual("Pear", list.ElementAt(1));
        Assert.AreEqual("Mango", list.ElementAt(2));
        Assert.AreEqual("Kiwi", list.ElementAt(3));
        Assert.AreEqual("Banana", list.ElementAt(4));
        Assert.AreEqual("Apple", list.ElementAt(5));

    }

    //this test simply creates an empty tree and then calls our ToLinkedList() method
    //The list is created but it returns an empty list that has a count of 0. 
    [Test]
    public void TestCreatingEmptyLL()
    {
        BST<string> tree = new BST<string>();
        Lists.LinkedList<string> list = tree.ToLinkedList();
        Assert.AreEqual(0, list.Count);
    }

   

    //This method simply tests that an empty tree will create an empty list
    [Test]
    public void TestEmptyBST()
    {
        BST<string> emptyTree = new BST<string>();
        Lists.LinkedList<string> emptyList = emptyTree.ToLinkedList();
        Assert.AreEqual(0, emptyList.Count);
    }

    //This tests a bst with only one element in it being converted into a linked list
    [Test]
    public void TestBSTWithOneElement()
    {
        BST<string> oneElementBST = new BST<string>();
        oneElementBST.Add("OneThing");
        Lists.LinkedList<string> oneList = oneElementBST.ToLinkedList();
        Assert.AreEqual(1, oneList.Count);
        Assert.AreEqual("OneThing", oneList.ElementAt(0));
    }

    [Test]
    public void TestBSTBredthFirstEnumeration()
    {
        IEnumerator<string> myEnum = sBST.GetBreadthFirstEnumerator();
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Mango", myEnum.Current);
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Apple", myEnum.Current);
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Plum", myEnum.Current);
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Banana", myEnum.Current);
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Pear", myEnum.Current);
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Kiwi", myEnum.Current);
        Assert.AreEqual(false, myEnum.MoveNext());
    }

    [Test]
    public void TestBSTDepthFirstEnumeration()
    {
        IEnumerator<string> myEnum = sBST.GetEnumerator();
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Mango", myEnum.Current);
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Apple", myEnum.Current);
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Banana", myEnum.Current);
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Kiwi", myEnum.Current);
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Plum", myEnum.Current);
        Assert.AreEqual(true, myEnum.MoveNext());
        Assert.AreEqual("Pear", myEnum.Current);
        Assert.AreEqual(false , myEnum.MoveNext());
    }

    [Test]
    public void TestBSTRemove()
    {
        Assert.AreEqual(true, sBST.Remove("Pear"));
        Assert.AreEqual(5, sBST.Count);
        Assert.AreEqual(false, sBST.Remove("Peach"));
        Assert.AreEqual(5, sBST.Count);
        Assert.AreEqual(true, sBST.Remove("Mango"));
        Assert.AreEqual(4, sBST.Count);
        try
        {
            sBST.Find("Mango");
            Assert.Fail();
        }
        catch (ApplicationException ex) 
        {
            Assert.AreEqual("Mango was not found in tree", ex.Message);
        }
        Assert.AreEqual(true, stuBST.Remove(new Student(null, 501, null)));
        Assert.AreEqual(4, stuBST.Count);
    }

    [Test]
    public void TestBSTHeight()
    {
        Assert.AreEqual(3, sBST.Height());
        Assert.AreEqual(2, stuBST.Height());
        stuBST.Add(new Student("Max", 200, "max@school.ca"));
        Assert.AreEqual(3, stuBST.Height());
    }

    [Test]
    public void TestBSTClone()
    {
        BST<Student> clone = (BST<Student>)stuBST.Clone();
        Student fred = clone.Find(new Student(null, 501, null));
        Assert.AreEqual("Fred", fred.Name);
        Assert.AreEqual("Fred@hotmail.com", fred.Email);
        Student jane = clone.Find(new Student(null, 648, null));
        Assert.AreEqual("Jane", jane.Name);
        Assert.AreEqual("jane@gmail.com", jane.Email);
    }

    [Test]
    public void TestBSTFind()
    {
        Student fred = stuBST.Find(new Student(null, 501, null));
        Assert.AreEqual("Fred", fred.Name);
        Assert.AreEqual("Fred@hotmail.com", fred.Email);
        Student jane = stuBST.Find(new Student(null, 648, null));
        Assert.AreEqual("Jane", jane.Name);
        Assert.AreEqual("jane@gmail.com", jane.Email);
    }

    [Test]
    public void TestBSTCreation()
    {
        BST<string> bst = new BST<string>();
    }

    [Test]
    public void TestBSTAdd()
    {
        BST<string> bst = new BST<string>();
        Assert.AreEqual(0, bst.Count);
        bst.Add("Mango");
        bst.Add("Apple");
        bst.Add("Banana");
        bst.Add("Plum");
        bst.Add("Kiwi");
        bst.Add("Pear");
        Assert.AreEqual(6, bst.Count);
    }

    [Test]
    public void TestBSTClear()
    {
        Assert.AreEqual(6, sBST.Count);
        sBST.Clear();
        Assert.AreEqual(0, sBST.Count);
    }

    [Test]
    public void TestFindSmallest()
    {
        Assert.AreEqual("Apple", sBST.FindSmallest());
        sBST.Add("Acai");
        Assert.AreEqual("Acai", sBST.FindSmallest());

    }

    [Test]
    public void TestFindLargest()
    {
        Assert.AreEqual("Plum", sBST.FindLargest());
        sBST.Add("Strawberry");
        Assert.AreEqual("Strawberry", sBST.FindLargest());

    }

    [Test]
    public void TestBSTIterator()
    {
        Console.WriteLine("Pre-Order Traversal");
        aList = new ArrayList<string>();

        sBST.Iterate(WriteData, TRAVERSALORDER.PRE_ORDER);
        Assert.AreEqual("Mango", aList.ElementAt(0));
        Assert.AreEqual("Apple", aList.ElementAt(1));
        Assert.AreEqual("Banana", aList.ElementAt(2));
        Assert.AreEqual("Kiwi", aList.ElementAt(3));
        Assert.AreEqual("Plum", aList.ElementAt(4));
        Assert.AreEqual("Pear", aList.ElementAt(5));

        Console.WriteLine("In-Order Traversal");
        aList = new ArrayList<string>();

        sBST.Iterate(WriteData, TRAVERSALORDER.IN_ORDER);
        Assert.AreEqual("Apple", aList.ElementAt(0));
        Assert.AreEqual("Banana", aList.ElementAt(1));
        Assert.AreEqual("Kiwi", aList.ElementAt(2));
        Assert.AreEqual("Mango", aList.ElementAt(3));
        Assert.AreEqual("Pear", aList.ElementAt(4));
        Assert.AreEqual("Plum", aList.ElementAt(5));

        Console.WriteLine("Post-Order Traversal");
        aList = new ArrayList<string>();

        sBST.Iterate(WriteData, TRAVERSALORDER.POST_ORDER);
        Assert.AreEqual("Kiwi", aList.ElementAt(0));
        Assert.AreEqual("Banana", aList.ElementAt(1));
        Assert.AreEqual("Apple", aList.ElementAt(2));
        Assert.AreEqual("Pear", aList.ElementAt(3));
        Assert.AreEqual("Plum", aList.ElementAt(4));
        Assert.AreEqual("Mango", aList.ElementAt(5));

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

