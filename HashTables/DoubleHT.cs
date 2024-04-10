using System;
using System.Collections.Generic;


namespace HashTables;

public class DoubleHT<K,V>: A_OpenAddressing<K,V>
    where K : IComparable<K>
{
    protected override int GetIncrement(int iAttempt, K key)
    {
        int hk = Math.Abs(key.GetHashCode());
        int increment = (1 + hk % (HTSize - 1)) * iAttempt;
        return increment;
    }
}
