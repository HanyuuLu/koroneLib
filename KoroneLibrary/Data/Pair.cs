using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoroneLibrary.Data
{
    public class Pair<K,V>:ICloneable
    {
        public Pair(K key,V value) { Key = key; Value = value; }
        public Pair() { }
        public K Key { get; set; }
        public V Value { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public Pair<K, V> DeepClone()
        {
            return new Pair<K, V> {
                Key = Key,
                Value = Value
            };
        }

    }
}
