using System;
using System.Collections.Generic;


namespace Tests;

public class Person : IComparable<Person>
{
    int iSin = 0;
    string sFirst = "";
    string sLast = "";
    string sEmail = "";

    public Person(int iSin, string sFirstName, string sLastName, string sEmail)
    { 
        this.iSin = iSin;
        this.sFirst = sFirstName;
        this.sLast = sLastName;
        this.sEmail = sEmail;   
    }

    public int SIN { get => iSin; }
    public string FirstName { get => sFirst;}
    public string LastName { get => sLast;}
    public string Email { get => sEmail;}

    public override string ToString()
    {
        return LastName + ", " + FirstName + " (" + SIN + ")";

    }

    //We will compare people on the bassis of their sin
    public int CompareTo(Person person)
    {
        return this.SIN.CompareTo(person.SIN);  
    }

    //Two people are going to be equal if they have the same sings
    public override bool Equals(object obj)
    {
        Person person = (Person)obj;
        return this.CompareTo(person) == 0; 
    }

    /// <summary>
    /// Only overrid this method if you know what you are doing!
    /// But if you do know what you're doing - if you know your programs data
    /// you might be able to write a better hash function then the default
    /// 
    /// A good hash function will generate values that represent all the indicies in the 
    /// table array. not just a subset. The actual hash function takes into account
    /// the table size, this hashcode function just turns the object into an integer
    /// 
    /// So a good GetHashCode function will have every integer have an equal chance
    /// of being generated.
    /// 
    /// Some hash functions will work well with some keys but not others
    /// Some hash function will work well with some table sizes but not others
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        //Let's turn the SIN into a string of charecters
        char[] cChars = SIN.ToString().ToCharArray();
        int iHashCode = 0;
        int iPrime = 31;
        //Common way to compute this is go through our array of values
        for(int i=0; i<cChars.Length; i++)
        {
            //This is a bad hash code
            //Each SIN is 9 digits. Each ASCII digit has a value from 48 to 57
            //That meants teh sum of digits is going from 9 * 48 at the low end
            //to 9 * 57 at the high end - only 81 possible hashcodes!
            //iHashCode += (int)cChars[i];
            //This technique multiple each ascii value by a power of 9 
            //according to its position in the SIN, For this particular set
            //of data, we get a much better distribution
            iHashCode += ((int) cChars[i]) * (int) Math.Pow(9, i);
            //A common general purpose hashing algorithim is to take a prime number,
            //multiple it by the existing hashcode, then add on the current charecter
            //iHashCode = (iPrime * iHashCode) + cChars[i];
        }

        //Pseudo random numbers generate good hash code, BUT are usually to slow
        //Random rnd = new Random(SIN);
        //return rnd.Next(Int32.MaxValue);


        return iHashCode;
    }



}
