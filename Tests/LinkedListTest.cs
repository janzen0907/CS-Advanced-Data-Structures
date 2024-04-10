
using Lists;
using System.Collections;
using System.Collections.Generic;

namespace ListTests;
public class LinkedListTests
{

    Lists.LinkedList<string> myList;

    [SetUp]
    public void SetUp()
    {
        myList = new Lists.LinkedList<string>();
        myList.Add("Apple");
        myList.Add("Cherry");
        myList.Add("Mango");
    }

    [Test]
    public void TestLinkedListAddAndCreate()
    {
        Lists.LinkedList<string> myAL = new Lists.LinkedList<string>();
        myAL.Add("Cherry");
        myAL.Add("Peach");
        Assert.AreEqual(2, myAL.Count);
    }

    [Test]
    public void TestElementAt()
    {
        Assert.AreEqual("Apple", myList.ElementAt(0));
        Assert.AreEqual("Mango", myList.ElementAt(2));
    }

    [Test]
    public void TestClear()
    {
        Assert.AreEqual(3, myList.Count);
        myList.Clear();
        Assert.AreEqual(0, myList.Count);
    }

    [Test]
    public void TestInsert()
    {
        Assert.AreEqual(3, myList.Count);
        myList.Insert(1, "Kiwi");
        Assert.AreEqual(4, myList.Count);
        Assert.AreEqual("Kiwi", myList.ElementAt(1));
        Assert.AreEqual("Cherry", myList.ElementAt(2));     
    }

    [Test]
    public void TestRemove()
    {
        Assert.AreEqual(3, myList.Count);
        Assert.AreEqual(true, myList.Remove("Cherry"));
        Assert.AreEqual(2, myList.Count);
        Assert.AreEqual(false, myList.Remove("Pineapple"));
        Assert.AreEqual(2, myList.Count);
        Assert.AreEqual(true, myList.Remove("Apple"));
        Assert.AreEqual(true, myList.Remove("Mango"));
        Assert.AreEqual(0, myList.Count);
        Assert.AreEqual(false, myList.Remove("Bananna"));
    }

    [Test]
    public void TestRemoveAtMultiple()
    {
        Assert.AreEqual(3, myList.Count);
        Assert.AreEqual("Mango", myList.RemoveAt(2));
        Assert.AreEqual(2, myList.Count);
        Assert.AreEqual("Apple", myList.RemoveAt(0));
        Assert.AreEqual(1, myList.Count);
    }


    [Test]
    public void TestRemoveAtHead()
    {
        Assert.AreEqual(3, myList.Count);
        Assert.AreEqual("Apple", myList.RemoveAt(0));
        Assert.AreEqual(2, myList.Count);
    }

    [Test]
    public void TestRemoveAtLastElement()
    {
        Assert.AreEqual(3, myList.Count);
        Assert.AreEqual("Mango", myList.RemoveAt(2));
        Assert.AreEqual(2, myList.Count);
    }

    [Test]
    public void TestRemoveAtMiddle()
    {
        Assert.AreEqual(3, myList.Count);
        Assert.AreEqual("Cherry", myList.RemoveAt(1));
        Assert.AreEqual(2, myList.Count);
        
    }

    [Test]
    public void TestReplaceAt()
    {
        Assert.AreEqual(3, myList.Count);
        Assert.AreEqual("Cherry", myList.ElementAt(1));
        myList.ReplaceAt(1, "Kiwi");
        Assert.AreEqual(3, myList.Count);
        Assert.AreEqual("Kiwi", myList.ElementAt(1));
        myList.ReplaceAt(2, "Pumpkin");
        Assert.AreEqual("Pumpkin", myList.ElementAt(2));
        Assert.AreEqual(3, myList.Count);
        myList.ReplaceAt(0, "Orange");
        Assert.AreEqual("Orange", myList.ElementAt(0));
    }

    [Test]
    public void TestGetEnumerator()
    {
        IEnumerator<string> myEnum = myList.GetEnumerator();
        myEnum.Reset();
        Assert.AreEqual(myEnum.MoveNext(), true);
        Assert.AreEqual(myEnum.Current, "Apple");
        Assert.AreEqual(myEnum.MoveNext(), true);
        Assert.AreEqual(myEnum.Current, "Cherry");
        Assert.AreEqual(myEnum.MoveNext(), true);
        Assert.AreEqual(myEnum.Current, "Mango");
        Assert.AreEqual(myEnum.MoveNext(), false);
        Assert.AreEqual(myEnum.Current, "Mango");
        myEnum.Reset();
        Assert.AreEqual(myEnum.MoveNext(), true);
        Assert.AreEqual(myEnum.Current, "Apple");

    }
}
