using System;
using System.Collections.Generic;

using DataStructuresCommon;
namespace Lists;

public abstract class A_List<T> : A_Collection<T>, I_List<T> where T: IComparable<T>
{
    public abstract void Insert(int index, T data);
    public abstract T RemoveAt(int index);
    public abstract T ReplaceAt(int index, T data);

    public virtual int IndexOf(T data)
    {
        throw new NotImplementedException();
    }

    //Some stuff we'll already be able to implement just becasue we know we're
    //planning on implementing the Enumerator in our children this may not be the most efficient, so make sure to mark it
    //virtual so our children can override
    public virtual T ElementAt(int index)
    {
        //Default operator gets a default of the Type - if T is an int,
        //you'll get 0, if it is a bool you'll get false, if it is a reference type you'll get null, etc
        T tReturn = default(T);

        //keep track of how many times we've looped
        int count = 0;

        //Check bounds
        if (index < 0 || index >= this.Count)
        {
            // throw an exceotion if we're out of bounds
            throw new IndexOutOfRangeException("invalid index" + index);
        }
        
        IEnumerator<T> myEnum = GetEnumerator();
        myEnum.Reset(); //Move the enumerator before the start

        //Loop while there are more data items and not at the current index
        while(myEnum.MoveNext() && count != index)
        {
            count++;
        }

        //Once the count equals the index, we know we're at the right index
        tReturn = myEnum.Current;

        return tReturn;

        //lookups in array are done in constant time
        //If were looking for the third item it would be O1
    }

}