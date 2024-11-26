using System.Drawing;
using System.Numerics;
using Task11;

public class Programm
{
    public class MyPriorityQueue<T> where T : IComparable
    {
        MyHeap<T> queue;
        private int size;
        private IComparer<T> comparer = new MyComparer<T>();

        public int Size() { return size; }
        public MyPriorityQueue(T[] a)
        {
            queue = new MyHeap<T>(a);
        }


        public MyPriorityQueue(int capacity)
        {
            queue = new MyHeap<T>(capacity);
        }


        public MyPriorityQueue(int capacity, IComparer<T> comp)
        {
            queue = new MyHeap<T>(capacity);
            size = 0;
            comparer = comp;
        }


        public MyPriorityQueue(MyPriorityQueue<T> c)
        {
            queue = new MyHeap<T>();
            T[] a = c.ToArray();
            for (int i = 0; i < a.Length; i++)
                queue.Add(a[i]);
            size = queue.Count;
        }

        public void Add(T e)
        {

            queue.Add(e);
            size++;
        }
        public void Clear()
        {
            queue = new MyHeap<T>();
            size = 0;
        }
        public void AddAll(T[] a)
        {
            MyHeap<T> array = new MyHeap<T>(a);
            queue.Add(array);
        }

        public T[] ToArray() { return queue.ToArray(); }

        public bool Contains(object o)
        {
            T oConverter = (T)o;
            foreach (T e in queue)
                if (oConverter.Equals(e))
                    return true;

            return false;
        }

        public bool ContainsAll(T[] a)
        {
            foreach (T e in a)
            {
                bool flag = false;
                foreach (T t in queue)
                    if (t.Equals(e) == true)
                        flag = true;
                if (!flag) return false;
            }
            return true;
        }

        public void Remove(T e)
        {
            queue.Remove(e);
        }
        public void RemoveAll(T[] a) {  queue.RemoveAll(a); }

        public bool IsEmpty()
        {
            if (size == 0) return true;
            return false;
        }

        public void RetainAll(T[] a)
        {
            foreach(T el in queue) {
                bool flag = false; 
                foreach (T e in a)
                {
                    if (el.CompareTo(e) == 0) {flag = true; break;}
                }
                if (!flag) Remove(el);
            }
        }

        public T Element()
        {
            return queue.GetMax();
        }
        public T? Peek()
        {
            if (size == 0) return default(T);
            return queue.GetMax();
        }

        public T Pool()
        {
           return queue.RemoveMax();
        }
        public void Print()
        {
            queue.Print();
        }
    }
    static void Main()
    {
        int[] mas = { 4, 3, 1, 7, 5 };
        char[] mas2 = { 'a', 'b', 'c' };
        MyPriorityQueue<char> Mpq = new MyPriorityQueue<char>(mas2);
        MyPriorityQueue<int> Mp = new MyPriorityQueue<int>(mas);
        Mpq.Print();
        Console.WriteLine();



        Mp.Print();

    }
}