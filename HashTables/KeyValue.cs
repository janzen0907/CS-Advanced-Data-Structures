using System;


namespace HashTables;

public class KeyValue<K, V> : IComparable<KeyValue<K, V>>
    where K : IComparable<K>
{
    K kKey;
    V vValue;

    public KeyValue(K key, V value)
    {
        this.kKey = key;
        this.vValue = value;
    }

    public K Key { get { return kKey; } }
    public V Value { get { return vValue; } }

    public int CompareTo(KeyValue<K, V> other)
    {
        return this.Key.CompareTo(other.Key);
    }

    public override bool Equals(object obj)
    {
        //Casting an object to our keyvalue
        KeyValue<K, V> kv = (KeyValue<K, V>)obj;
        return this.Key.CompareTo(kv.Key) == 0;

    }


}