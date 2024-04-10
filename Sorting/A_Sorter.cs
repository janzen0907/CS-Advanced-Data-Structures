using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting;

public abstract class A_Sorter<T> where T : IComparable<T>
{
    //The array we will store
    private T[] array;

    public T[] Array { get { return array; } }

    public A_Sorter(T[] array) 
    {
        this.array = array;
    }

    //this is the whole purpsoe of a sorting class
    public abstract void Sort();    

    protected void Swap(int first, int second)
    {
        T temp = Array[first];
        Array[first] = Array[second];
        Array[second] = temp;
    }

    public int Length { get { return array.Length; } }
   
    public T this[int index]
    {
        get { return array[index]; }
        set { Array[index] = value; }
    }

    public string ToString()
    {
        StringBuilder sb = new StringBuilder("[");
        foreach(T tVal in array )
        {
            sb.Append(tVal);
            sb.Append(", ");
        }

        if(sb.Length > 1)
        {
            sb.Remove(sb.Length - 2, 2);
        }
        sb.Append("]");
        return sb.ToString();
    }

}
