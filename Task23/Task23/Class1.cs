using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task23
{
    public class MyHashMap<K, T>
    {

        MyLinkedList<Tuple<K, T>>[] table;
        K s;

        BitArray chosen;
        int size;
        double loadFactor;
        public MyHashMap()
        {
            table = new MyLinkedList<Tuple<K, T>>[16];
            chosen = new BitArray(16);
            size = 0;
            loadFactor = 0.75;
        }

        public MyHashMap(int cap)
        {
            table = new MyLinkedList<Tuple<K, T>>[cap];
            chosen = new BitArray(cap);
            size = 0;
            loadFactor = 0.75;
        }


        public MyHashMap(int cap, float load)
        {
            table = new MyLinkedList<Tuple<K, T>>[cap];
            size = 0;
            if (load > 1.0)
                throw new Exception();
            loadFactor = load;
        }
        public T? Get(K key)
        {
            var el = table[Math.Abs((key.GetHashCode()) % table.Length)];

            for (int i = 0; i < el.Size(); i++)
            {
                Tuple<K, T> tup = el.Get(i);
                if (tup.Item1.Equals(key))
                    return tup.Item2;
            }
            return default(T);
        }

        public K[] KeySet()
        {
            K[] keysArray = new K[size];
            int currentIndex = 0;

            foreach (var entry in table)
            {
                if (entry != null)
                {
                    var list = entry;
                    for (int j = 0; j < list.Size(); j++)
                    {
                        keysArray[currentIndex++] = list.Get(j).Item1;
                    }
                }
            }

            return keysArray;
        }

        public bool IsEmpty() => size == 0;
        public bool HasValue(object value)
        {
            T element = (T)value;

            foreach (var entry in table)
            {
                if (entry != null)
                {
                    var list = entry;
                    for (int j = 0; j < list.Size(); j++)
                    {
                        if (list.Get(j).Item2.Equals(value))
                            return true;
                    }
                }
            }

            return false;
        }

        public void Put(K key, T value)
        {
            double loadRatio = (double)(size + 1) / table.Length;

            if (loadRatio >= loadFactor)
            {
                ReSize();
            }

            int index = Math.Abs(key.GetHashCode()) % table.Length;
            if (table[index] == null)
            {
                Tuple<K, T> valToAdd = new Tuple<K, T>(key, value);
                MyLinkedList<Tuple<K, T>> newKey = new MyLinkedList<Tuple<K, T>>();
                newKey.Add(valToAdd);
                table[index] = newKey;
            }
            else
            {
                var Element = table[index];
                K ListKey;
                var s = Element.Size();
                for (int i = 0; i < Element.Size(); i++)
                {
                    ListKey = Element.Get(i).Item1;
                    if (ListKey.GetHashCode() == key.GetHashCode())
                    {
                        if (ListKey.Equals(key))
                        {
                            Element.Set(i, new Tuple<K, T>(ListKey, value));
                            return;
                        }
                    }
                }
                Element.AddLast(new Tuple<K, T>(key, value));

            }
            size += 1;
        }

        public Tuple<K, T>[] EntrySet()
        {
            Tuple<K, T>[] entriesArray = new Tuple<K, T>[size];
            int currentIndex = 0;

            foreach (var entry in table)
            {
                if (entry != null)
                {
                    var list = entry;
                    for (int j = 0; j < list.Size(); j++)
                    {
                        entriesArray[currentIndex++] = list.Get(j);
                    }
                }
            }

            return entriesArray;
        }

        public void RemoveKey(K key)
        {
            int index = Math.Abs(key.GetHashCode()) % table.Length;

            if (index >= table.Length)
                throw new IndexOutOfRangeException();

            if (table[index] != null)
            {
                var list = table[index];
                for (int j = 0; j < list.Size(); j++)
                {
                    if (list.Get(j).Item1.Equals(key))
                    {
                        list.Remove(j);
                        size--;
                        break; // Можно выйти из цикла после удаления
                    }
                }
            }
        }
        public bool ContainsKey(object Key)
        {
            K key = (K)Key;
            if (table[Math.Abs((key.GetHashCode()) % table.Length)] == null)
                return false;
            else
            {
                MyLinkedList<Tuple<K, T>> list = table[Math.Abs((key.GetHashCode()) % table.Length)];
                for (int i = 0; i < list.Size(); i++)
                {
                    if (list.Get(i).Item1.Equals(key))
                        return true;
                }
            }
            return false;
        }
        public int Size() => size;
        public void ReSize()
        {
            MyLinkedList<Tuple<K, T>>[] newArray = new MyLinkedList<Tuple<K, T>>[table.Length * 3];
            size = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    MyLinkedList<Tuple<K, T>> val = table[i];
                    while (val.Size() > 0)
                    {
                        Tuple<K, T> pair = val.PollFirst();
                        int index = Math.Abs(pair.Item1.GetHashCode()) % newArray.Length;
                        CheckPut(index, newArray, pair.Item1, pair.Item2);

                    }

                }

            }
            table = newArray;
        }







        public void CheckPut(int index, MyLinkedList<Tuple<K, T>>[] table, K key, T value)
        {
            if (table[index] == null)
            {
                Tuple<K, T> valToAdd = new Tuple<K, T>(key, value);
                MyLinkedList<Tuple<K, T>> newKey = new MyLinkedList<Tuple<K, T>>();
                newKey.Add(valToAdd);
                table[index] = newKey;
            }
            else
            {
                var Element = table[index];
                K ListKey;
                var s = Element.Size();
                for (int i = 0; i < Element.Size(); i++)
                {
                    ListKey = Element.Get(i).Item1;
                    if (ListKey.GetHashCode() == key.GetHashCode())
                    {
                        if (ListKey.Equals(key))
                        {
                            Element.Set(i, new Tuple<K, T>(ListKey, value));
                            return;
                        }
                    }
                }
                Element.AddLast(new Tuple<K, T>(key, value));

            }
            size += 1;
        }

        public void Clear()
        {
            Array.Clear(table);
        }
    }
}
