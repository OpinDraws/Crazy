using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18
{
    public class MyLinkedList<T>
    {
        private class ListEl<T>
        {
            internal protected ListEl<T>? next = null;
            internal protected ListEl<T>? prev = null;
            internal protected T? value = default(T?);
        }

        ListEl<T>? first = null;
        ListEl<T>? last = null;
        int size;

        public MyLinkedList()
        {

            size = 0;
        }

        public MyLinkedList(T[] a)
        {
            first = new ListEl<T>();
            last = new ListEl<T>();
            first.value = a[0];
            size++;
            last = first;
            for (int i = 1; i < a.Length; i++)
            {
                Add(a[i]);

            }
        }
        public void Add(T e)
        {
            if (first == null)
            {
                first = new ListEl<T>();
                first.value = e;
                last = first;
                return;
            }
            ListEl<T> newEl = new ListEl<T>();
            newEl.value = e;
            newEl.prev = last;
            last.next = newEl;
            last = newEl;
            size++;
        }
        public void AddAll(T[] a)
        {
            foreach (T e in a)
                Add(e);
        }

        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }
        public bool Contains(object o)
        {
            ListEl<T>? iterator = first;
            while (iterator != null)
            {
                if (iterator.value.Equals((T)o))
                    return true;
                iterator = iterator.next;

            }
            return false;
        }

        public bool ContainsAll(T[] a)
        {
            foreach (T e in a)
            {
                if (Contains(e) == false)
                    return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            if (size == 0) return true;
            return false;
        }

        public void Remove(object o)
        {
            if (first.value.Equals((T)o))
            {
                first = first.next;
                size--;
                return;
            }
            ListEl<T>? iterator = first;
            while (iterator != null)
            {
                if (iterator.value.Equals((T)o))
                {
                    iterator.prev.next = iterator.next;
                    size--;
                    return;
                }
                iterator = iterator.next;

            }
        }
        public void RemoveAll(T[] a)
        {
            foreach (T e in a)
                Remove((object)e);
        }

        public void RetainAll(T[] a)
        {
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                T e = Get(index);
                if (Contains(e) == false)
                {
                    Remove(e);
                }
                else
                    index++;
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
        public T[] ToArray(T[] a)
        {
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
            if (index < size)
            {
                if (index == 0)
                {
                    ListEl<T> iter = new ListEl<T>();
                    iter.value = el;
                    iter.next = first;
                    first.prev = iter;
                    first = iter;
                    return;
                }
                if (index == size - 1)
                {
                    ListEl<T> iter = new ListEl<T>();
                    iter.value = el;
                    iter.prev = last;
                    last.next = iter;
                    last = iter;
                    return;
                }
                int current = 0;
                ListEl<T> iters = new ListEl<T>();
                iters = first;
                while (current + 1 != index)
                {
                    iters = iters.next; current++;
                }
                if (current + 1 == index)
                {
                    ListEl<T> nextEl = new ListEl<T>();
                    nextEl.value = el;
                    nextEl.prev = iters;
                    nextEl.next = iters.next;

                    iters.next.prev = nextEl;
                    iters.next = nextEl;
                }
            }
        }
        public void AddAll(int index, T[] a)
        {
            for (int i = a.Length - 1; i >= 0; i--)
            {
                Add(index, a[i]);

            }


        }

        public T Get(int index)
        {
            int curIndex = 0;
            if (index >= size)
                throw new IndexOutOfRangeException();
            if (index == size - 1)
                return last.value;
            if (index == 0)
                return first.value;
            ListEl<T>? iterator = first;
            while (curIndex != index)
            {
                iterator = iterator.next;
                curIndex++;
            }
            return iterator.value;
        }

        public int IndexOf(object o)
        {
            T el = (T)o;
            int index = 0;


            ListEl<T> iter = new ListEl<T>();
            iter = first;
            while (iter != null)
            {
                if (iter.value.Equals(el))
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


            ListEl<T> iter = new ListEl<T>();
            iter = last;
            while (iter != null)
            {
                if (iter.value.Equals(el))
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
                T el = first.value;

                first = first.next;
                first.prev = null;
                size -= 1;
                return el;
            }


            if (index == size - 1)
            {
                T el = last.value;
                last = last.prev;
                last.next = null;
                size -= 1;
                return el;
            }


            ListEl<T> iter = new ListEl<T>();
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
                return iter.value;
            }

            return default(T);
        }


        public void Set(int index, T e)
        {
            if (index >= size)
                throw new IndexOutOfRangeException();
            if (index == 0)
            {
                first.value = e;
                return;
            }
            if (index == size - 1)
            {
                last.value = e;
                return;
            }
            int id = 0;
            ListEl<T> iter = new ListEl<T>();
            iter = first;
            while (id != index)
            {
                iter = iter.next;
                id++;
            }
            iter.value = e;
        }


        public T[] SubList(int fromindex, int toindex)
        {
            if (fromindex > toindex || fromindex < 0 || toindex >= size)
                throw new IndexOutOfRangeException();
            T[] RetArray = new T[toindex - fromindex + 1];
            int currentIndex = 0;
            ListEl<T> iter = new ListEl<T>();
            iter = first;
            while (currentIndex != fromindex)
            {
                iter = iter.next;
                currentIndex++;
            }
            int retIndex = 0;
            while (currentIndex <= toindex)
            {
                RetArray[retIndex] = iter.value;
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
            return first.value;
        }




        public void Print()
        {
            ListEl<T>? iterator = first;
            while (iterator != null)
            {
                Console.WriteLine($"{iterator.value}");
                iterator = iterator.next;

            }
        }


        public T Peek()
        {
            if (first == null)
                return default(T);
            return first.value;
        }


        public void AddFirst(T el)
        {
            Add(0, el);
        }

        public void AddLast(T el)
        {
            Add(size - 1, el);
        }
        public T Pool()
        {
            if (first == null)
                throw new IndexOutOfRangeException();
            T el = first.value;
            first = first.next;
            first.prev = null;
            size -= 1;
            return el;
        }

        public T GetFirst()
        {
            if (first == null)
                throw new IndexOutOfRangeException();
            return first.value;

        }


        public T GetLast()
        {
            if (last == null)
                throw new IndexOutOfRangeException();
            return last.value;

        }

        public bool OfferFirst(T obj)
        {

            ListEl<T>? iterator = new ListEl<T>();
            iterator.value = obj;
            iterator.next = first;
            iterator.prev = null;
            first.prev = iterator;
            first = iterator;
            return true;
        }


        public bool OfferLast(T obj)
        {

            ListEl<T>? iterator = new ListEl<T>();
            iterator.value = obj;
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
            return first.value;

        }


        public T PeekLast()
        {
            if (size == 0)
                return default(T);
            return last.value;
        }


        public T PollFirst()
        {
            if (size == 1)
            {
                first = null;
                last = null;
                size = 0;
            }
            if (size == 0)
                return default(T);
            T el = first.value;
            first = first.next;
            first.prev = null;
            size--;
            return el;
        }


        public T PollLast()
        {
            if (size == 1)
            {
                first = null;
                last = null;
                size = 0;
            }
            if (size == 0)
                return default(T);
            T el = last.value;
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
            T el = first.value;

            first = first.next;
            first.prev = null;
            size -= 1;
            return el;
        }


        public T RemoveLast()
        {
            T el = last.value;
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
            T el = first.value;
            first = first.next;
            last.prev = null; size--;
            return el;
        }

        public bool RemoveLastOccurence(object obj)
        {
            T el = (T)obj;
            if (size == 1 && first.value.Equals(el))
            {
                first = null;
                size--;
                return true;
            }
            if (last.value.Equals(el))
            {
                last = last.prev;
                last.next = null;
                size--;
                return true;
            }

            ListEl<T>? iterator = last;
            while (iterator != null)
            {
                if (iterator.value.Equals(el))
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
            if (size == 1 && first.value.Equals((T)obj))
            {
                size--; return true;
            }
            if (first.value.Equals(obj))
            {
                first = first.next;
                first.prev = null;
                return true;
            }
            T el = (T)obj;
            ListEl<T>? iterator = first;
            while (iterator != null)
            {
                if (iterator.value.Equals(el))
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
    }
}
