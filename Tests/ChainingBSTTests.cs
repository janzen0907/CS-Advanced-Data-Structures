using System;
using System.Collections.Generic;
using Lists;
using Tests;
using BinarySearchTree;
using HashTables;
using System.IO;

namespace Tests;


public class ChainingBSTTests
{
    ChainingBST<int, string> chainingBST;
    ChainingBST<Person, Person> peopleBST;
    //NOTE CHANGE THIS BACK AT SCHOOL TO ONE DRIVE LOCATION
    //Home
    ArrayList<Person> persons = loadPeopleFromFile(@"C:\Users\dieds\OneDrive - Saskatchewan Polytechnic\COSC 286\COSC286\tenthousandpeople.csv");
    //School
    //ArrayList<Person> persons = loadPeopleFromFile(@"C:\Users\janzen0907\OneDrive - Saskatchewan Polytechnic\COSC 286\COSC286\tenthousandpeople.csv");

    [SetUp]
    public void Setup()
    {
        chainingBST = new ChainingBST<int, string>();
        peopleBST = new ChainingBST<Person, Person>();
    }

    [Test]
    public void TestLoadPeopleFromFile()
    {
        foreach (Person person in persons)
        {
            Console.WriteLine(person);
        }
    }

    [Test]
    public void TestChainingHTAdd()
    {
        testHTAdd(chainingBST);
    }

    [Test]
    public void TestChainingRemove()
    {
        testHTRemove(chainingBST);
    }

    [Test]
    public void TestChainingHTGet()
    {
        testHTGet(chainingBST);
    }

    [Test]
    public void TestChainingHTEnumeration()
    {
        testEnumerator(new ChainingBST<int, Person>());
    }

    [Test]
    public void TestToString()
    {
        testHTToString(chainingBST);
    }


    private void testHTToString(A_HashTable<int, string> ht)
    {
        ht.Add(1, "Bob");
        string expectedResult = " Key: 1 Value: Bob";
        string result = ht.ToString();
        Assert.AreEqual(expectedResult, result);
        ht.Add(2, "Tom");
        string expectedResult2 = " Key: 1 Value: Bob Key: 2 Value: Tom";
        string result2 = ht.ToString();
        Assert.AreEqual(expectedResult2, result2);
        ht.Add(3, "Bill");
        string expectedResult3 = " Key: 1 Value: Bob Key: 2 Value: Tom Key: 3 Value: Bill";
        string result3 = ht.ToString();
        Assert.AreEqual(expectedResult2, result2);
    }



    private void testHTAdd(A_HashTable<int, string> ht)
    {

        Assert.AreEqual(0, ht.Count);
        ht.Add(123, "Fred");
        ht.Add(456, "Ahmed");
        ht.Add(350, "John");
        ht.Add(730, "Hu");
        Assert.AreEqual(4, ht.Count);
        //If we try to add something with a duplicate key we should expect
        //an exception
        Assert.Throws(typeof(ApplicationException), () => ht.Add(123, "Duplicate"));
        Assert.AreEqual(4, ht.Count);
    }

    private void testHTRemove(A_HashTable<int, string> ht)
    {
        ht.Add(999, "Fred");
        ht.Add(1, "Nancy");
        ht.Add(-5, "Hu");
        ht.Add(99, "Ahmed");
        ht.Add(0, "Tom");
        Assert.AreEqual(5, ht.Count);
        Assert.AreEqual("Nancy", ht.Remove(1));
        Assert.AreEqual(4, ht.Count);
        Assert.AreEqual("Hu", ht.Remove(-5));
        Assert.AreEqual(3, ht.Count);
        Assert.AreEqual("Ahmed", ht.Remove(99));
        Assert.AreEqual(2, ht.Count);
        Assert.Throws(typeof(ApplicationException), () => ht.Remove(-5));
        Assert.AreEqual("Fred", ht.Remove(999));
        Assert.AreEqual(1, ht.Count);
        Assert.AreEqual("Tom", ht.Remove(0));
        Assert.AreEqual(0, ht.Count);
        ht.Add(0, "Tom");
        ht.Add(99, "Ahmed");
        ht.Add(-5, "Hu");
        ht.Add(1, "Nancy");
        ht.Add(999, "Fred");
        Assert.AreEqual("Hu", ht.Get(-5));
        Assert.AreEqual("Fred", ht.Get(999));
    }

    private void testHTGet(A_HashTable<int, string> ht)
    {
        //Using the keys we should be able to get the value that
        //was originally put in
        testHTAdd(ht);
        Assert.AreEqual("Fred", ht.Get(123));
        Assert.AreEqual("Ahmed", ht.Get(456));
        Assert.AreEqual("John", ht.Get(350));
        Assert.AreEqual("Hu", ht.Get(730));
        //If the value is not in there we will throw an error
        Assert.Throws(typeof(ApplicationException), () => ht.Get(999));
    }

    private void testEnumerator(A_HashTable<int, Person> ht)
    {
        //Keep track of the SIN's we're adding
        ArrayList<int> alSins = new ArrayList<int>();
        Random rnd = new Random(9999);

        //Randomly generate 500 sins and add them to the hash table
        for (int i = 0; i < 500; i++)
        {
            //Generate a random SIN, wrap it in a person
            int iSin = rnd.Next(999999999);
            alSins.Add(iSin);
            ht.Add(iSin, new Person(iSin, "First", "Last", "Email"));
        }
        Assert.AreEqual(500, alSins.Count);

        //Now that we have 500 people in our hashtable, lets enumerate
        //through them all
        //As we find them, cross them off our list
        foreach (Person p in ht)
        {
            //alSins.Remove(p.SIN);
            Assert.IsTrue(alSins.Remove(p.SIN));
        }
        //Once were done we should have gone through every person
        //so the al should be empty
        Assert.AreEqual(0, alSins.Count);
        //alSins.Clear();
    }



    public static ArrayList<Person> loadPeopleFromFile(string filename)
    {
        ArrayList<Person> al = new ArrayList<Person>();

        //I can enumerate the lines of a file
        IEnumerable<string> lines = File.ReadLines(filename);
        foreach (string line in lines)
        {
            //String has a split method we can use to split it up
            string[] sArray = line.Split(",");
            //Transform string into an integer
            int iSin = Int32.Parse(sArray[3]);
            Person p = new Person(iSin, sArray[0], sArray[1], sArray[2]);
            al.Add(p);
        }
        return al;
    }

}
