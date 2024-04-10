
using Lists;
using System.Collections;
using System.Collections.Generic;

namespace ListTests;
public class ArrayListTests
{

    ArrayList<string> myList;

    [SetUp]
    public void SetUp()
    {
        myList = new ArrayList<string>();
        myList.Add("Apple");
        myList.Add("Cherry");
        myList.Add("Mango");
    }

    [Test]
    public void TestArrayListAddAndCreate()
    {
        ArrayList<string> myAL = new ArrayList<string>();
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
    }

    [Test]
    public void TestRemoveAt()
    {
        Assert.AreEqual(3, myList.Count);
        Assert.AreEqual("Cherry", myList.RemoveAt(1));
        Assert.AreEqual(2, myList.Count);
    }

    [Test]
    public void TestReplaceAt()
    {
        Assert.AreEqual(3, myList.Count);
        Assert.AreEqual("Cherry", myList.ReplaceAt(1, "Kiwi"));
        Assert.AreEqual(3, myList.Count);
        Assert.AreEqual("Kiwi", myList.ElementAt(1));
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
