using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11
{
    public class MyComparer<T> : Comparer<T> where T : IComparable
    {
        public override int Compare(T? x, T? y)
        {
            return x.CompareTo(y);
        }
    }
    internal class MyHeap<T> where T : IComparable
    {
        private T[] array;
        private int count;

        public MyHeap() { 
            this.array = new T[0];
            count = 0;
        }
        public MyHeap(int capacity)
        {
            this.array = new T[capacity];
            count = 0;
        }
        public MyHeap(T[] array)
        {
            if (array == null) { Console.WriteLine("Lol,this empety"); this.array = new T[10]; }
            else
            {
                this.array = (T[])array.Clone();
                count = array.Length;

            }
            OrderInHeap();
        }
        public int Count { get => count; }

        public void Print()
        {

            MyHeap<T> help = new MyHeap<T>(array);
            for (int i = 0; i < count; i++)
            {
                Console.Write(help.RemoveMax());
                Console.Write(' ');
            }
            Console.WriteLine();
        }

        private void Swap(int a, int b)
        {
            T t = array[a];
            array[a] = array[b];
            array[b] = t;
        }
        private void Emerge(int indexA)
        {
            if (indexA != 0)
            {
                int indexB = (indexA - 1) / 2;
                while (indexA >= 0)
                {

                    if (indexA.CompareTo(indexB) > 0)
                    {
                        Swap(indexA, indexB);
                        indexA = indexB;
                        indexB = (indexA + 1) / 2;
                    }
                    else break;
                }
            }
        }

        private void Heapify(int valueIndex)
        {
            while (true)
            {
                int leftChildIndex = valueIndex * 2 + 1;
                int rightChildIndex = valueIndex * 2 + 2;
                int largestItemIndex = valueIndex;

                if (leftChildIndex < count && array[leftChildIndex].CompareTo(array[largestItemIndex]) > 0)
                    largestItemIndex = leftChildIndex;

                if (rightChildIndex < count && array[rightChildIndex].CompareTo(array[largestItemIndex]) > 0)
                    largestItemIndex = rightChildIndex;

                if (largestItemIndex == valueIndex) break;

                Swap(valueIndex, largestItemIndex);

                valueIndex = largestItemIndex;
            }

        }

        public void Add(T value)
        {
            if (count == array.Length)
            {
                ExceptionMemory();
            }
            array[count++] = value;
            Emerge(count - 1);
        }

        public T RemoveMax()
        {
            if (count == 0) throw new IndexOutOfRangeException();

            T removedItem = array[0];
            if (count > 1)
            {
                array[0] = array[count - 1];
                array[count - 1] = default;
                if (count > 1) Heapify(0);
            }
            else array[0] = default;
            count--;
            return removedItem;
        }

        public T GetMax() => array[0];

        private void OrderAll()
        {
            for (int i = count / 2; i >= 0; i--) Heapify(i);
        }

        public void Add(MyHeap<T> heap)
        {
            T[] array = heap.ToArray();
            foreach (T item in array) Add(item);
        }

        private void ExceptionMemory()
        {
            T[] help = new T[(int)(array.Length * 1.5)];
            Array.Copy(array, help, array.Length);
            array = help;
        }

        public void KeyChange(int index, T val)
        {
            array[index] = val;
            Emerge(index);

        }

        private void OrderInHeap()
        {
            for (int i = count / 2; i >= 0; i--) Heapify(i);

        }

        public T[] ToArray()
        {
            T[] newItemData = new T[count];
            Array.Copy(array, newItemData, count);
            return newItemData;
        }

        public override string ToString()
            => $"[{string.Join(", ", ToArray())}]";
        public IEnumerator GetEnumerator()
        {
            return array.GetEnumerator();   
        }
        public void Remove(T item)
        {
            for(int i=0; i<count; i++)
           
                if (array[i].CompareTo(item) > 0) {
                    array[i] = array[count - 1];
                    array[count - 1] = default(T);
                    count--;

                }
            OrderAll();
        }

        public void RemoveAll(T[] a)
        {
            foreach(T item in a)
            {
                Remove(item);
            }
            OrderAll();
        }
    }
}

