using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables;

public class PrimeNumber
{
    //We'll store a list of pre-computed prime numbers
    int[] iPrimes = {5, 11, 19, 41, 79, 163, 317, 641, 1201, 2399, 4801,
        9733, 50021, 100003, 200003, 4000009, 1600033};

    //Keep track of which prime number we gave out last
    int iCurrent = -1;

    public int GetNextPrime()
    {
        iCurrent++;
        if(iCurrent >= iPrimes.Length)
        {
            throw new ApplicationException("The table size has exceeded 1600033");
        }
        return iPrimes[iCurrent];
    }
}
