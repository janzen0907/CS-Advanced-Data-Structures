using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HashTables;
using Lists;
using Tests;

namespace HashTableTests;

public class HashTableTests
{ 

    ChainingHT<int, string> chainingHT;
    ChainingHT<Person, Person> peopleCHT;
    //Home
    ArrayList<Person> persons = loadPeopleFromFile(@"C:\Users\dieds\OneDrive - Saskatchewan Polytechnic\COSC 286\COSC286\tenthousandpeople.csv");
    //School
    //ArrayList<Person> persons = loadPeopleFromFile(@"C:\Users\janzen0907\OneDrive - Saskatchewan Polytechnic\COSC 286\COSC286\tenthousandpeople.csv");
    LinearHT<int, string> linearHT;
    LinearHT<Person, Person> peopleLHT;


    [SetUp]
    public void Setup()

    {
        chainingHT = new ChainingHT<int, string>();
        peopleCHT = new ChainingHT<Person, Person>();
        linearHT = new LinearHT<int, string>();
        peopleLHT = new LinearHT<Person, Person> ();
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
    public void TestChainingHTConsructor()
    {
        ChainingHT<int, string> chaining = new ChainingHT<int, string>();
    }
    

    [Test]
    public void TestChainingHTAdd()
    {
        testHTAdd(chainingHT);
    }

    [Test]
    public void TestChainingHTClear()
    {
        testHTAdd(chainingHT);
        Assert.AreEqual(4, chainingHT.Count);
        chainingHT.Clear();
        Assert.AreEqual(0, chainingHT.Count);
    }

    [Test]
    public void testChainingHTLotsOfPeople()
    {
        testHTAddLotsOfPeople(peopleCHT);
        Assert.AreEqual(10000, peopleCHT.Count);
        testHTGetLotsOfPeople(peopleCHT);
    }

    [Test]
    public void TestChainingHTEnumeration()
    {
        testEnumerator(new ChainingHT<int, Person>());
    }

    [Test]
    public void TestChainingRemove()
    {
        testHTRemove(new ChainingHT<int, string>());
    }

    [Test]
    public void TestChainingHTKeys()
    {
        testHTKeys(new ChainingHT<int, string>());
    }

    [Test]
    public void TestChainingHTGet()
    {
        testHTGet(chainingHT);
    }

    //Linear HashTable Tests Being here
    [Test]
    public void TestLinearHTConsructor()
    {
        LinearHT<int, string> chaining = new LinearHT<int, string>();
    }


    [Test]
    public void TestLinearHTAdd()
    {
        testHTAdd(linearHT);
    }

    [Test]
    public void TestLinearHTClear()
    {
        testHTAdd(linearHT);
        Assert.AreEqual(4, linearHT.Count);
        linearHT.Clear();
        Assert.AreEqual(0, linearHT.Count);

    }

    [Test]
    public void testLinearHTLotsOfPeople()
    {
        testHTAddLotsOfPeople(peopleLHT);
        Assert.AreEqual(10000, peopleLHT.Count);
        testHTGetLotsOfPeople(peopleLHT);
    }

    [Test]
    public void TestLinearHTEnumeration()
    {
        testEnumerator(new LinearHT<int, Person>());
    }

    [Test]
    public void TestLinearRemove()
    {
        testHTRemove(new LinearHT<int, string>());
    }

    [Test]
    public void TestLinearHTKeys()
    {
        testHTKeys(new LinearHT<int, string>());
    }

    [Test]
    public void TestLinearHTGet()
    {
        testHTGet(linearHT);
    }
    
    //Test Quadratic Hashtables
    [Test]
    public void TestQuadraticHTLotsOfPeople()
    {
        QuadraticHT<Person, Person> peopleQHT = new QuadraticHT<Person, Person>();
        testHTAddLotsOfPeople(peopleQHT);
        testHTGetLotsOfPeople(peopleQHT);
    }

    //Test Double Hashtables
    [Test]
    public void TestDoubleHTLotsOfPeople()
    {
        DoubleHT<Person, Person> peopleDHT = new DoubleHT<Person, Person>();
        testHTAddLotsOfPeople(peopleDHT);
        testHTGetLotsOfPeople(peopleDHT);
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

    private void testEnumerator(A_HashTable<int, Person> ht)
    {
        //Keep track of the SIN's we're adding
        ArrayList<int> alSins = new ArrayList<int>();
        Random rnd = new Random(9999);

        //Randomly generate 500 sins and add them to the hash table
        for(int i=0; i<500; i++)
        {
            //Generate a random SIN, wrap it in a person
            int iSin = rnd.Next(999999999);
            alSins.Add(iSin);
            ht.Add(iSin, new Person(iSin, "First", "Last", "Email"));
        }

        //Now that we have 500 people in our hashtable, lets enumerate
        //through them all
        //As we find them, cross them off our list
        foreach (Person p in ht )
        {
            Assert.IsTrue(alSins.Remove(p.SIN));
        }
        //Once were done we should have gone through every person
        //so the al should be empty
        Assert.AreEqual(0, alSins.Count);
    }

    private void testHTAddLotsOfPeople(A_HashTable<Person, Person> ht)
    {
        int iNumAdded = 0;
        long start = Environment.TickCount;
        
        foreach(Person person in persons)
        {
            ht.Add(person, person);
            iNumAdded++;

        }
        long time = Environment.TickCount - start;
        Console.WriteLine("Added {0} entries in {1} ms, with {2} collissions",
            iNumAdded, time, ht.NumCollisions);
    }

    
    private void testHTGetLotsOfPeople(A_HashTable<Person, Person> ht)
    {
        long start = Environment.TickCount;
        foreach(Person p in persons) 
        { 
            Person pFromHt = ht.Get(p);
            Assert.AreEqual(p.Email, pFromHt.Email);
        }
        long totalTime = Environment.TickCount - start; 
        Console.WriteLine("Getting {0} peope took {1} ms",
            persons.Count, totalTime);
    }

    private void testHTKeys(A_HashTable<int, string> ht)
    {
        ht.Add(7, "Seven");
        ht.Add(1, "One");
        ht.Add(99, "Ninety-Nine");
        ht.Add(999999, "Big Number");
        ht.Add(23, "Twenty-Three");
        ht.Add(-14, "Negative Fourteen");
        ht.Add(0, "Zero");
        //Get the sorted rray of keys from the keys propert
        int[] aInts = ht.Keys;
        Assert.AreEqual(7, aInts.Length);
        //Let's make sure teh list is sorted
        Assert.AreEqual(-14, aInts[0]);
        Assert.AreEqual(0, aInts[1]);
        Assert.AreEqual(1, aInts[2]);
        Assert.AreEqual(7, aInts[3]);
        Assert.AreEqual(23, aInts[4]);
        Assert.AreEqual(99, aInts[5]);
        Assert.AreEqual(999999, aInts[6]);
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
