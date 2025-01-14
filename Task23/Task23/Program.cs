using System.Drawing;
using System.Runtime.ExceptionServices;
using Task23;
namespace Task_23
{
    internal class Program
    {
        public class MyHashSet<T> where T : IComparable
        {
            private MyHashMap<T, object> map;
            public MyHashSet()
            {
                map = new MyHashMap<T, object>();

            }


            public MyHashSet(T[] a)
            {
                foreach (T el in a)
                    map.Put(el, false);
            }


            public MyHashSet(int cap, float loadFactor)
            {
                map = new MyHashMap<T, object>(cap, loadFactor);


            }


            public MyHashSet(int cap)
            {
                map = new MyHashMap<T, object>(cap);


            }


            public void Add(T e)
            {
                map.Put((T)e, false);


            }

            public void AddAll(T[] a)
            {
                foreach (T el in a)
                    map.Put(el, false);
            }


            public void Clear()
            {
                map.Clear();

            }

            public bool Contains(object o)
            {
                T e = (T)o;
                return map.ContainsKey(e);

            }


            public bool ContainsAll(T[] a)
            {
                foreach (T el in a)
                {
                    if (!map.ContainsKey(el))
                        return false;
                }
                return true;
            }


            public bool IsEmpty()
            {
                return map.IsEmpty();

            }


            public void Remove(object o)
            {
                T e = (T)o;
                map.RemoveKey(e);

            }
            public void Remove(T[] a)
            {
                foreach (T e in a)
                    map.RemoveKey(e);

            }


            public void RetainAll(T[] a)
            {
                T[] keys = map.KeySet();
                foreach (T e in keys)
                {
                    if (!a.Contains<T>(e))
                        map.RemoveKey(e);
                }
            }


            public int Size() => map.Size();


            public T[] ToArray()
            {

                T[] returnArray = map.KeySet();
                return returnArray;
            }


            public T[] ToArray(T[] a)
            {
                if (a == null)
                    return ToArray();
                T[] returnArray = new T[a.Length + map.Size()];
                int index = 0;
                for (int i = 0; i < a.Length; i++)
                    returnArray[index++] = a[i];
                T[] devArray = map.KeySet();
                foreach (T el in devArray)
                    returnArray[index++] = el;
                return returnArray;
            }


            public T First()
            {
                if (map.IsEmpty())
                    return default(T);
                T[] array = map.KeySet();
                T min = array[0];
                foreach (T el in array)
                {
                    if (min.CompareTo(el) > 0)
                        min = el;
                }
                return min;
            }


            public T Last()
            {
                if (map.IsEmpty())
                    return default(T);
                T[] array = map.KeySet();
                T max = array[0];
                foreach (T el in array)
                {
                    if (max.CompareTo(el) < 0)
                        max = el;
                }
                return max;
            }


            public MyHashSet<T> Subset(T from, T toEl)
            {
                MyHashSet<T> SubSet = new MyHashSet<T>();
                T[] array = map.KeySet();
                foreach (T el in array)
                {
                    if (el.CompareTo(from) >= 0 && el.CompareTo(toEl) <= 0)
                        SubSet.Add(el);
                }
                return SubSet;

            }


            public MyHashSet<T> HeadSet(T toElement)
            {
                MyHashSet<T> SubSet = new MyHashSet<T>();
                T[] array = map.KeySet();
                foreach (T el in array)
                {
                    if (el.CompareTo(toElement) < 0)
                        SubSet.Add(el);
                }
                return SubSet;


            }

            public MyHashSet<T> TailSet(T fromElement)
            {
                MyHashSet<T> SubSet = new MyHashSet<T>();
                T[] array = map.KeySet();
                foreach (T el in array)
                {
                    if (el.CompareTo(fromElement) > 0)
                        SubSet.Add(el);
                }
                return SubSet;
            }
        }





        static void Main(string[] args)
        {
            MyHashSet<string> set = new MyHashSet<string>();
            string[] a = { "a", "b", "c", "d" };
            set.AddAll(a);
            set.Remove("d");

            string[] arr = set.ToArray();
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");
            //MyHashSet<int> set = new MyHashSet<int>();

            //set.Add(7);set.Add(2);set.Add(3);
            //int[] a = {17,24,12};


            //set.AddAll(a);


            //MyHashSet<int> sub = set.HeadSet(100);

            //int[] arr = sub.ToArray();
            //for (int i = 0; i < arr.Length; i++)
            //    Console.Write(arr[i] + " ");
        }
    }
}