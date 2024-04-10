using System;
using System.Collections.Generic;


namespace HashTables;

public class QuadraticHT<K, V> : A_OpenAddressing<K, V>
    where K : IComparable<K>
{
    double c1 = 0.5;
    double c2 = 0.5;

    public QuadraticHT()
    {
        //Should not be higher than 0.5
        this.dLoadFactorLimit = 0.5;
    }
    protected override int GetIncrement(int iAttempt, K key)
    {
        //h(k, i) = (h(k) + c1*i + c2*i2) mod n
         int increment = (int)(c1 * iAttempt + c2 * Math.Pow(iAttempt, 2));
        return increment ;
    }
}
