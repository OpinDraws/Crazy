using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task20
{
    public class MyLinkedList<T>
    {
        LinkedListElement<T>? first;
        LinkedListElement<T>? last;
        int size;

        public MyLinkedList()
        {
            first = null;
            last = null;
            size = 0;
        }

        public MyLinkedList(T[] a)
        {
            first = new LinkedListElement<T>();
            last = new LinkedListElement<T>();
            first.Value = a[0];
            last = first;
            size++;
            for (int i = 1; i < a.Length; i++)
            {
                Add(a[i]);

            }
        }

        public void Add(T el)
        {
            if (first == null)
            {
                first = new LinkedListElement<T>();
                first.Value = el;

                last = first;
                size++;
                return;
            }
            LinkedListElement<T> newEl = new LinkedListElement<T>();
            newEl.Value = el;

            newEl.prev = last;
            last.next = newEl;

            last = newEl;

            size++;
        }


        public void AddAll(T[] a)
        {
            foreach (T el in a)
                Add(el);

        }

        public T Get(int index)
        {
            int curIndex = 0;
            if (index >= size)
                throw new IndexOutOfRangeException();
            if (index == size - 1)
                return last.Value;
            if (index == 0)
                return first.Value;
            LinkedListElement<T>? iterator = first;
            while (curIndex != index)
            {
                iterator = iterator.next;
                curIndex++;
            }
            return iterator.Value;
        }


        public void Clear()
        {
            first = null;
            last = first;
        }


        public bool Contains(object o)
        {
            LinkedListElement<T>? iterator = first;
            while (iterator != null)
            {
                if (iterator.Value.Equals((T)o))
                    return true;
                iterator = iterator.next;

            }
            return false;
        }


        public bool ContainsAll(T[] a)
        {
            foreach (T el in a)
            {
                if (Contains(el) == false)
                    return false;

            }
            return true;

        }


        public bool IsEmpty() => size == 0;


        public void Remove(object o)
        {
            LinkedListElement<T>? iterator = first;
            if (first.Value.Equals((T)o))
            {
                first = first.next;
                return;
            }
            if (last.Value.Equals((T)o))
            {
                last = last.prev;
                return;

            }


            while (iterator.next != null)
            {
                if (iterator.next.Value.Equals((T)o))
                {
                    iterator.next = iterator.next.next;
                }
                else
                    iterator = iterator.next;
            }


        }


        public void RemoveAll(T[] a)
        {
            foreach (T el in a)
                Remove(el);
        }


        public void RetainAll(T[] a)
        {
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                T el = Get(index);
                if (Contains(el) == false)
                {
                    Remove(el);
                }
                else
                    index++;
            }
            bool Contains(T el)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (el.Equals(a[i]))
                        return true;
                }
                return false;
            }


        }


        public int Size() => size;


        public T[] ToArray()
        {
            T[] retArray = new T[size];
            for (int i = 0; i < size; i++)
                retArray[i] = Get(i);
            return retArray;
        }


        public T[] ToArray(T[]? a)
        {
            if (a == null)
                return ToArray();


            T[] retArray = new T[size + a.Length];
            int index = 0;
            for (; index < a.Length; index++)
                retArray[index] = a[index];
            for (int i = 0; i < size; i++)
            {
                retArray[index] = Get(i);
                index++;

            }
            return retArray;

        }


        public void Add(int index, T el)
        {
            if (first == null)
            {
                first = new LinkedListElement<T>();
                first.Value = el;

                last = first;
                size++;
                return;
            }
            if (index == 0)
            {
                LinkedListElement<T> iter = new LinkedListElement<T>();
                iter.Value = el;
                iter.next = first;
                first.prev = iter;
                first = iter;
                return;
            }
            if (index == size - 1)
            {
                LinkedListElement<T> iter = new LinkedListElement<T>();
                iter.Value = el;
                iter.prev = last;
                last.next = iter;
                last = iter;
                return;
            }
            int current = 0;
            LinkedListElement<T> iters = new LinkedListElement<T>();
            iters = first;
            while (current + 1 != index)
            {
                iters = iters.next; current++;
            }
            if (current + 1 == index)
            {
                LinkedListElement<T> nextEl = new LinkedListElement<T>();
                nextEl.Value = el;
                nextEl.prev = iters;
                nextEl.next = iters.next;

                iters.next.prev = nextEl;
                iters.next = nextEl;
            }
        }


        public void AddAll(int index, T[] a)
        {
            for (int i = a.Length - 1; i >= 0; i--)
            {
                Add(index, a[i]);

            }


        }


        public int IndexOf(object o)
        {
            T el = (T)o;
            int index = 0;


            LinkedListElement<T> iter = new LinkedListElement<T>();
            iter = first;
            while (iter != null)
            {
                if (iter.Value.Equals(el))
                    return index;
                iter = iter.next;
                index++;
            }
            return -1;
        }


        public int LastIndexOf(object o)
        {
            T el = (T)o;
            int index = size - 1;


            LinkedListElement<T> iter = new LinkedListElement<T>();
            iter = last;
            while (iter != null)
            {
                if (iter.Value.Equals(el))
                    return index;
                iter = iter.prev;
                index--;
            }
            return -1;


        }


        public T Remove(int index)
        {
            if (index >= size)
                throw new IndexOutOfRangeException();
            int currentIndex = 0;

            if (index == 0)
            {
                if (size != 1)
                {
                    T el = first.Value;

                    first = first.next;
                    first.prev = null;
                    size -= 1;
                    return el;
                }
                first = null;
                last = null;
                size--;
                return default(T);
            }


            if (index == size - 1)
            {
                if (size != 1)
                {
                    T el = last.Value;
                    last = last.prev;
                    last.next = null;
                    size -= 1;
                    return el;
                }
                first = null;
                last = null;
                size--;
                return default(T);
            }


            LinkedListElement<T> iter = new LinkedListElement<T>();
            iter = first;
            while (currentIndex != index)
            {
                iter = iter.next;
                currentIndex++;
            }
            if (currentIndex == index)
            {
                iter.prev.next = iter.next;
                iter.next.prev = iter.prev;
                size -= 1;
                return iter.Value;
            }

            return default(T);
        }


        public void Set(int index, T e)
        {
            if (index >= size)
                throw new IndexOutOfRangeException();
            if (index == 0)
            {
                first.Value = e;
                return;
            }
            if (index == size - 1)
            {
                last.Value = e;
                return;
            }
            int id = 0;
            LinkedListElement<T> iter = new LinkedListElement<T>();
            iter = first;
            while (id != index)
            {
                iter = iter.next;
                id++;
            }
            iter.Value = e;
        }


        public T[] SubList(int fromindex, int toindex)
        {
            if (fromindex > toindex || fromindex < 0 || toindex >= size)
                throw new IndexOutOfRangeException();
            T[] RetArray = new T[toindex - fromindex + 1];
            int currentIndex = 0;
            LinkedListElement<T> iter = new LinkedListElement<T>();
            iter = first;
            while (currentIndex != fromindex)
            {
                iter = iter.next;
                currentIndex++;
            }
            int retIndex = 0;
            while (currentIndex <= toindex)
            {
                RetArray[retIndex] = iter.Value;
                retIndex++;
                currentIndex++;
                iter = iter.next;
            }
            return RetArray;
        }


        public T Element()
        {
            if (first == null)
                throw new IndexOutOfRangeException();
            return first.Value;
        }




        public void Print()
        {
            LinkedListElement<T>? iterator = first;
            while (iterator != null)
            {
                Console.WriteLine($"{iterator.Value}");
                iterator = iterator.next;

            }
        }


        public T Peek()
        {
            if (first == null)
                return default(T);
            return first.Value;
        }


        public void AddFirst(T el)
        {
            Add(0, el);
        }

        public void AddLast(T el)
        {
            Add(el);
        }
        public T Pool()
        {
            if (first == null)
                throw new IndexOutOfRangeException();
            T el = first.Value;
            first = first.next;
            first.prev = null;
            size -= 1;
            return el;
        }

        public T GetFirst()
        {
            if (first == null)
                throw new IndexOutOfRangeException();
            return first.Value;

        }


        public T GetLast()
        {
            if (last == null)
                throw new IndexOutOfRangeException();
            return last.Value;

        }

        public bool OfferFirst(T obj)
        {

            LinkedListElement<T>? iterator = new LinkedListElement<T>();
            iterator.Value = obj;
            iterator.next = first;
            iterator.prev = null;
            first.prev = iterator;
            first = iterator;
            return true;
        }


        public bool OfferLast(T obj)
        {

            LinkedListElement<T>? iterator = new LinkedListElement<T>();
            iterator.Value = obj;
            iterator.next = null;
            iterator.prev = last;
            last.next = iterator;
            last = iterator;
            return true;
        }


        public void Push(T obj)
        {
            AddFirst(obj);
        }


        public T PeekFirst()
        {
            if (size == 0)
                return default(T);
            return first.Value;

        }


        public T PeekLast()
        {
            if (size == 0)
                return default(T);
            return first.Value;
        }


        public T PollFirst()
        {
            if (size == 1)
            {
                T e = first.Value;
                first = null;
                last = null;
                size = 0;
                return e;
            }
            if (size == 0)
                return default(T);
            T el = first.Value;
            first = first.next;
            first.prev = null;
            size--;
            return el;
        }


        public T PollLast()
        {
            if (size == 1)
            {
                T e = first.Value;
                first = null;
                last = null;
                size = 0;
                return e;
            }
            if (size == 0)
                return default(T);
            T el = last.Value;
            last = last.prev;
            last.next = null;
            size--;
            return el;
        }

        public T Pop()
        {
            if (size == 1)
            {
                first = null;
                size = 0;
            }
            if (first == null)
                throw new IndexOutOfRangeException();
            T el = first.Value;

            first = first.next;
            first.prev = null;
            size -= 1;
            return el;
        }


        public T RemoveLast()
        {
            T el = last.Value;
            last = last.prev;
            last.next = null; size--;
            return el;
        }


        public T RemoveFirst()
        {
            if (size == 1)
            {
                first = null;
                size = 0;
            }
            T el = first.Value;
            first = first.next;
            last.prev = null; size--;
            return el;
        }


        public bool RemoveLastOccurence(object obj)
        {
            T el = (T)obj;
            if (size == 1 && first.Value.Equals(el))
            {
                first = null;
                size--;
                return true;
            }
            if (last.Value.Equals(el))
            {
                last = last.prev;
                last.next = null;
                size--;
                return true;
            }

            LinkedListElement<T>? iterator = last;
            while (iterator != null)
            {
                if (iterator.Value.Equals(el))
                {
                    if (iterator != first)
                    {
                        iterator.prev.next = iterator.next;
                        iterator.next.prev = iterator.prev;

                    }
                    else
                    {
                        first = first.next;
                        first.prev = null;
                    }
                    size--;
                    return true;
                }
                iterator = iterator.prev;

            }
            return false;
        }


        public bool RemoveFirstOccurence(object obj)
        {
            if (size == 1 && first.Value.Equals((T)obj))
            {
                size--; return true;
            }
            if (first.Value.Equals(obj))
            {
                first = first.next;
                first.prev = null;
                return true;
            }
            T el = (T)obj;
            LinkedListElement<T>? iterator = first;
            while (iterator != null)
            {
                if (iterator.Value.Equals(el))
                {
                    if (iterator != last)
                    {
                        iterator.prev.next = iterator.next;
                        iterator.next.prev = iterator.prev;

                    }
                    else
                    {
                        last = last.prev;
                        last.next = null;
                    }
                    size--;
                    return true;
                }
                iterator = iterator.next;

            }
            return false;



        }

        class LinkedListElement<T>
        {
            public LinkedListElement<T>? next = null;
            public LinkedListElement<T>? prev = null;
            public T Value;
        }
    }

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







        void CheckPut(int index, MyLinkedList<Tuple<K, T>>[] table, K key, T value)
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


    }
}
