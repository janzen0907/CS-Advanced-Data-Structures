using Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SortingTests;

public class SortingTests
{
    int[] aInt;
    int[] randomInts;
    int[] mostlyRandomInts;
    int numItems = 50000;

    [SetUp]
    public void SetUp()
    {
        aInt = new int[10];
        aInt[0] = 7;
        aInt[1] = 1;
        aInt[2] = 5;
        aInt[3] = 8;
        aInt[4] = 2;
        aInt[5] = 7;
        aInt[6] = 4;
        aInt[7] = 3;
        aInt[8] = 0;
        aInt[9] = 9;
        mostlyRandomInts = createMostlyRandomInts(numItems);
        randomInts = createRandomInts(numItems);
    }

    [Test]
    public void TestHeapSorter()
    {
        HeapSorter<int> sorter = new HeapSorter<int>(aInt);
        Console.WriteLine("QuickSort:");
        Console.WriteLine("Before:");
        Console.WriteLine(sorter.ToString());
        sorter.Sort();
        Console.WriteLine("After:");
        Console.WriteLine(sorter.ToString());
        AssertSortedInt(sorter.Array);
        sorter = new HeapSorter<int>(randomInts);
        Console.WriteLine("Sorting " + numItems + " random ints took " + TimeSort(sorter) + "ms.");
        AssertSortedInt(sorter.Array);
        sorter = new HeapSorter<int>(mostlyRandomInts);
        Console.WriteLine("Sorting " + numItems + " mostly random ints took " + TimeSort(sorter) + "ms.");
        AssertSortedInt(sorter.Array);
    }

    [Test]
    public void TestHeapSorterHeapify()
    {

        HeapSorter<int> hs = new HeapSorter<int>(aInt);
        hs.Heapify();
        for(int i = 0; i< hs.Length; i++)
        {
            Console.WriteLine(hs[i]);
        }
        Assert.AreEqual(9, hs[0]);
        Assert.AreEqual(8, hs[1]);
        Assert.AreEqual(7, hs[2]);
        Assert.AreEqual(7, hs[3]);
        Assert.AreEqual(2, hs[4]);
        Assert.AreEqual(5, hs[5]);
        Assert.AreEqual(4, hs[6]);
        Assert.AreEqual(3, hs[7]);
        Assert.AreEqual(0, hs[8]);
        Assert.AreEqual(1, hs[9]);
        

    }


    
    //[Test]
   public void TestQuickSortPartition()
   {
       int[] aInt = { 9, 4, 3, 6, 10, 2, 7, 5 };
       QuickSorter<int> qs = new QuickSorter<int>(aInt);
       int index = qs.partition(0, 6, 5);
       Console.WriteLine("After partition:");
       foreach (int i in qs.Array) Console.WriteLine(i);
       Console.WriteLine("Index where the two pointers met was " + index);
       qs = new QuickSorter<int>(new int[]{9, 10, 11, 99 });
       index = qs.partition(0, 2, 1);
       Console.WriteLine("After partition:");
       foreach (int i in qs.Array) Console.WriteLine(i);
       Console.WriteLine("Index where the two pointers met was " + index);
       index = qs.partition(0, 2, 99);
       Console.WriteLine("After partition:");
       foreach (int i in qs.Array) Console.WriteLine(i);
       Console.WriteLine("Index where the two pointers met was " + index);
   }

    [Test]
    public void TestQuickSortMedianOfThree()
    {
        QuickSorter<int> sorter = new QuickSorterMedianOfThree<int>(aInt);
        Console.WriteLine("QuickSort:");
        Console.WriteLine("Before:");
        Console.WriteLine(sorter.ToString());
        sorter.Sort();
        Console.WriteLine("After:");
        Console.WriteLine(sorter.ToString());
        AssertSortedInt(sorter.Array);
        sorter = new QuickSorterMedianOfThree<int>(randomInts);
        Console.WriteLine("Sorting " + numItems + " random ints took " + TimeSort(sorter) + "ms.");
        AssertSortedInt(sorter.Array);
        sorter = new QuickSorterMedianOfThree<int>(mostlyRandomInts);
        Console.WriteLine("Sorting " + numItems + " mostly random ints took " + TimeSort(sorter) + "ms.");
        AssertSortedInt(sorter.Array);
    }

    [Test]
    public void TestQuickSort()
    {
        QuickSorter<int> sorter = new QuickSorter<int>(aInt);
        Console.WriteLine("QuickSort:");
        Console.WriteLine("Before:");
        Console.WriteLine(sorter.ToString());
        sorter.Sort();
        Console.WriteLine("After:");
        Console.WriteLine(sorter.ToString());
        AssertSortedInt(sorter.Array);
        sorter = new QuickSorter<int>(randomInts);
        Console.WriteLine("Sorting " + numItems + " random ints took " + TimeSort(sorter) + "ms.");
        AssertSortedInt(sorter.Array);
        sorter = new QuickSorter<int>(mostlyRandomInts);
        Console.WriteLine("Sorting " + numItems + " mostly random ints took " + TimeSort(sorter) + "ms.");
        AssertSortedInt(sorter.Array);
    }

    [Test]
    public void TestInsertionSort()
    {
        InsertionSorter<int> sorter = new InsertionSorter<int>(aInt);
        Console.WriteLine("Insertion sort");
        Console.WriteLine("Before");
        sorter.Sort();
        Console.Write("After: ");
        Console.WriteLine(sorter.ToString());
        AssertSortedInt(sorter.Array);
        sorter = new InsertionSorter<int>(randomInts);
        Console.WriteLine("Sorting " + numItems + "random ints took " + TimeSort(sorter) + "ms");
        AssertSortedInt(sorter.Array);
        sorter = new InsertionSorter<int>(mostlyRandomInts);
        Console.WriteLine("Sorting " + numItems + "mostly random ints took " + TimeSort(sorter) + "ms");
        AssertSortedInt(sorter.Array);

    }

    //[Test]
    public void TestBubbleSort()
    {
        BubbleSort<int> sorter = new BubbleSort<int>(aInt);
        Console.WriteLine("Bubblesort");
        Console.WriteLine("Before");
        sorter.Sort();
        Console.Write("After: ");
        Console.WriteLine(sorter.ToString());
        AssertSortedInt(sorter.Array);
        sorter = new BubbleSort<int>(randomInts);
        Console.WriteLine("Sorting " + numItems + "random ints took " + TimeSort(sorter) + "ms");
        AssertSortedInt(sorter.Array);
        sorter = new BubbleSort<int>(mostlyRandomInts);
        Console.WriteLine("Sorting " + numItems + "mostly random ints took " + TimeSort(sorter) + "ms");
        AssertSortedInt(sorter.Array);

    }

    [Test]
    public void TestDotNetSort()
    {
        DotNetSort<int> sorter = new DotNetSort<int>(aInt);
        sorter.Sort();
        Console.WriteLine(sorter.ToString());
        AssertSortedInt(sorter.Array);

    }


    public void AssertSortedInt(int[] ints)
    {
        for(int i=0; i<ints.Length -1; i++)
        {
            Assert.IsTrue(ints[i] <= ints[i+1]);
        }
    }

    public int[] createRandomInts(int size)
    {
        int[] ints = new int[size];
        Random rnd = new Random();
        for(int i =0; i< size; i++)
        {
            ints[i] = rnd.Next(size);
        }
        return ints;
    }

    public int[] createMostlyRandomInts(int size) 
    {
        int[] ints = new int[size];
        Random rnd = new Random();
        for (int i = 0; i < size; i++)
        {
            if(rnd.Next(10) == 0)
            {
                ints[i] = rnd.Next(size);
            }
            else
            {
                ints[i] = i;
            }
        }
        return ints;
    }

    public double TimeSort(A_Sorter<int> sorter)
    {
        double start = Environment.TickCount;
        sorter.Sort();
        return Environment.TickCount - start;
    }
}
