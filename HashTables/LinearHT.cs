using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables;

public class LinearHT<K, V> : A_OpenAddressing<K, V>
    where K : IComparable<K>
{
    //The value of "c" - hw many steps ahead in the table to look
    private int iIncrement = 1;

    protected override int GetIncrement(int iAttempt, K key)
    {
        return iIncrement * iAttempt;
    }
}
