using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting;

public class DotNetSort<T> : A_Sorter<T> where T : IComparable<T>
{
   
    public DotNetSort(T[] array): base(array) { }

    public override void Sort()
    {
        System.Array.Sort(Array);
    }
}
