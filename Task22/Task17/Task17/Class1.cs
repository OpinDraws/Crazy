using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task17
{
    public class MyComparator<T> : Comparer<T> where T : IComparable
    {
        public override int Compare(T x, T y)
        {
            return x.CompareTo(y);
            throw new NotImplementedException();

        }
    }



    public class MyTreeMap<K, T> where K : IComparable
    {
        IComparer comparer;
        public TreeElement root = null;
        int size;

        public MyTreeMap()
        {

            comparer = new MyComparator<K>();
            size = 0;
        }


        public MyTreeMap(IComparer comp)
        {
            comparer = comp;
        }

        public void Clear()
        {
            root = null;
            size = 0;
        }


        public void Put(K key, T value)
        {
            if (root == null)
            {
                root = new TreeElement();
                root.Key = key;
                root.Value = value;
                size++;
                return;
            }
            TreeElement el = new TreeElement();
            el.Key = key;
            el.Value = value;
            TreeAdd(root, el);
            size++;
            void TreeAdd(TreeElement root, TreeElement AddVal)
            {
                int result = comparer.Compare(AddVal.Key, root.Key);
                while (true)
                {
                    if (comparer.Compare(AddVal.Key, root.Key) <= 0)
                    {
                        if (root.left == null)
                        {
                            root.left = AddVal;
                            break;

                        }
                        else root = root.left;
                    }
                    else if (comparer.Compare(AddVal.Key, root.Key) > 0)
                    {
                        if (root.right == null)
                        {
                            root.right = AddVal;
                            break;

                        }
                        else root = root.right;
                    }
                    else if (root.Key.Equals(AddVal.Key))
                        root.Key = AddVal.Key;
                }
            }
        }


        public bool ContainsKey(object key)
        {
            T newkey = (T)key;
            TreeElement copyRoot = root;
            while (copyRoot != null)
            {
                if (comparer.Compare(newkey, copyRoot.Key) < 0)
                {
                    copyRoot = copyRoot.left;


                }
                else if (comparer.Compare(newkey, copyRoot.Key) > 0)
                    copyRoot = copyRoot.right;
                else if (comparer.Compare(newkey, copyRoot.Key) == 0)
                    return true;
            }
            return false;
        }


        public bool ContainsValue(object value)
        {
            T val = (T)value;
            TreeElement copyRoot = root;
            if (comparer.Compare(copyRoot.Value, val) == 0)
                return true;
            Queue<TreeElement> queue = new Queue<TreeElement>();
            queue.Enqueue(copyRoot);
            while (queue.Count != 0)
            {
                TreeElement elFromqueue = queue.Dequeue();
                if (comparer.Compare(elFromqueue.Value, val) == 0)
                    return true;
                if (elFromqueue.left != null)
                    queue.Enqueue(elFromqueue.left);
                if (elFromqueue.right != null)
                    queue.Enqueue(elFromqueue.right);


            }
            return false;
        }


        public Tuple<K, T>[] EntrySet()
        {
            Tuple<K, T>[] retArray = new Tuple<K, T>[size];
            int index = 0;
            TreeElement copy = root;
            Queue<TreeElement> queue = new Queue<TreeElement>();
            queue.Enqueue(copy);
            while (queue.Count != 0)
            {
                TreeElement elFromqueue = queue.Dequeue();
                Tuple<K, T> pair = new Tuple<K, T>(elFromqueue.Key, elFromqueue.Value);
                retArray[index++] = pair;
                if (elFromqueue.left != null)
                    queue.Enqueue(elFromqueue.left);
                if (elFromqueue.right != null)
                    queue.Enqueue(elFromqueue.right);


            }
            return retArray;
        }


        public T Get(object key)
        {
            T el = (T)key;
            TreeElement copy = root;
            if (copy.Key.Equals(el))
                return copy.Value;

            while (copy != null)
            {
                if (comparer.Compare(key, copy.Key) < 0)
                {
                    copy = copy.left;

                }
                else if (comparer.Compare(key, copy.Key) > 0)
                    copy = copy.right;
                else if (comparer.Compare(key, copy.Key) == 0)
                    return copy.Value;


            }
            return default(T);
        }


        public K FirstKey()
        {
            if (root != null)
                return root.Key;
            return default(K);
        }


        public void Remove(K key)
        {
            if (root.Key.Equals(key) && root.right == null && root.left == null)
            {
                root = null;
                size = 0;
                return;
            }


            TreeElement copy = root;
            TreeElement prev = root;


            if (comparer.Compare(key, copy.Key) < 0)
                copy = root.left;


            else if (comparer.Compare(key, copy.Key) > 0)
                copy = root.right;




            while (copy != null)
            {
                if (copy.Key.Equals(key))
                {
                    if (copy.left == null && copy.right == null)
                    {
                        if (prev.left == copy)
                            prev.left = null;
                        else
                            prev.right = null;
                        size--;
                        return;
                    }

                    if (copy.left != null && copy.right == null || copy.right != null && copy.left == null)
                    {
                        if (copy.left != null)
                        {
                            copy.Value = copy.left.Value;
                            copy.Key = copy.left.Key;
                            copy.right = copy.left.right;
                            copy.left = copy.left.left;



                        }
                        else if (copy.right != null)
                        {
                            copy.Value = copy.right.Value;
                            copy.Key = copy.right.Key;
                            copy.left = copy.right.left;
                            copy.right = copy.right.right;





                        }
                        size--;
                        return;
                    }

                    if (copy.left != null && copy.right != null)
                    {
                        if (copy.right.left == null)
                        {
                            copy.Value = copy.right.Value;
                            copy.Key = copy.right.Key;
                            copy.right = copy.right.right;


                        }
                        else if (copy.right.left != null)
                        {
                            TreeElement copyOfRight = copy.right;
                            TreeElement leftPath = copy.right.left;
                            while (leftPath.left != null)
                            {
                                copyOfRight = leftPath.left;
                                leftPath = leftPath.left;
                            }
                            copy.Key = leftPath.Key;
                            copy.Value = leftPath.Value;

                            if (leftPath.left == null && leftPath.right == null)
                                copyOfRight.left = null;
                            if (leftPath.right != null)
                            {
                                leftPath.Key = leftPath.right.Key;
                                leftPath.Value = leftPath.right.Value;
                                leftPath.left = leftPath.right.left;
                                leftPath.right = leftPath.right.right;
                            }


                        }
                        size--;
                        return;
                    }
                }
                else if (comparer.Compare(key, copy.Key) < 0)
                {
                    prev = copy;
                    copy = copy.left;
                }
                else if (comparer.Compare(key, copy.Key) > 0)
                {
                    prev = copy;
                    copy = copy.right;
                }




            }
        }


        public Tuple<K, T> LowerEntry(K key)
        {
            foreach (Tuple<K, T> i in BFS(key))
            {
                if (comparer.Compare(i.Item1, key) < 0)
                    return i;
            }
            return default(Tuple<K, T>);
        }
        public int Size() => size;



        public Tuple<K, T> FloorEntry(K key)
        {
            foreach (Tuple<K, T> el in BFS(key))
                if (comparer.Compare(el.Item1, key) <= 0)
                    return el;
            return default(Tuple<K, T>);


        }


        public Tuple<K, T> HigherEntry(K key)
        {
            TreeElement copy = root;
            Queue<TreeElement> queue = new Queue<TreeElement>();
            queue.Enqueue(copy);
            while (queue.Count > 0)
            {
                copy = queue.Dequeue();
                if (comparer.Compare(copy.Key, key) > 0)
                {
                    Tuple<K, T> pair = new Tuple<K, T>(copy.Key, copy.Value);
                    return pair;
                }
                if (copy.left != null)
                    queue.Enqueue(copy.left);
                if (copy.right != null)
                    queue.Enqueue(copy.right);
            }
            return default(Tuple<K, T>);
        }


        public Tuple<K, T> CeilingEntry(K key)
        {
            foreach (Tuple<K, T> el in BFS(key))
                if (comparer.Compare(el.Item1, key) >= 0)
                    return el;
            return default(Tuple<K, T>);


        }


        public K LowerKey(K key)
        {
            foreach (Tuple<K, T> el in BFS(key))
                if (comparer.Compare(el.Item1, key) < 0)
                    return el.Item1;
            return default(K);
        }


        public K FloorKey(K key)
        {
            foreach (Tuple<K, T> el in BFS(key))
                if (comparer.Compare(el.Item1, key) <= 0)
                    return el.Item1;
            return default(K);
        }


        public K HigherKey(K key)
        {
            foreach (Tuple<K, T> el in BFS(key))
                if (comparer.Compare(el.Item1, key) > 0)
                    return el.Item1;
            return default(K);
        }


        public K CeilingKey(K key)
        {
            foreach (Tuple<K, T> el in BFS(key))
                if (comparer.Compare(el.Item1, key) >= 0)
                    return el.Item1;
            return default(K);
        }

        public void PrintTree()
        {
            TreeElement treeElement = root;
            Print(treeElement);
            void Print(TreeElement root)
            {
                if (root == null)
                    return;
                else
                {
                    Console.WriteLine(root.Key);
                    Print(root.left);
                    Print(root.right);
                }
            }
        }



        public MyTreeMap<K, T> HeadMap(K end)
        {
            MyTreeMap<K, T> TreeReturn = new MyTreeMap<K, T>();
            Queue<TreeElement> queue = new Queue<TreeElement>();
            TreeElement copy = root;
            if (comparer.Compare(end, copy.Key) < 0)
                copy = copy.left;
            else if (comparer.Compare(end, copy.Key) > 0)
                copy = copy.right;

            if (copy == null)
                return TreeReturn;
            queue.Enqueue(copy);
            while (queue.Count > 0)
            {
                TreeElement branch = queue.Dequeue();
                if (comparer.Compare(branch.Key, end) < 0)
                {
                    TreeReturn.Put(branch.Key, branch.Value);

                }
                if (branch.right != null)
                    queue.Enqueue(branch.right);
                if (branch.left != null)
                    queue.Enqueue(branch.left);

            }
            return TreeReturn;

        }


        public MyTreeMap<K, T> SubMap(K start, K end)
        {
            if (comparer.Compare(start, end) >= 0)
            {
                throw new IndexOutOfRangeException();
            }
            MyTreeMap<K, T> TreeReturn = new MyTreeMap<K, T>();

            TreeElement copy = root;
            if (comparer.Compare(start, copy.Key) == 0)
                copy = root.right;
            else if (comparer.Compare(end, copy.Key) <= 0)
                copy = root.left;
            if (copy == null)
                return TreeReturn;

            Queue<TreeElement> queue = new Queue<TreeElement>();
            queue.Enqueue(copy);
            while (queue.Count > 0)
            {
                copy = queue.Dequeue();
                if (comparer.Compare(start, copy.Key) <= 0 && comparer.Compare(end, copy.Key) > 0)
                    TreeReturn.Put(copy.Key, copy.Value);
                if (copy.right != null)
                    queue.Enqueue(copy.right);
                if (copy.left != null)
                    queue.Enqueue(copy.left);
            }


            return TreeReturn;
        }


        public MyTreeMap<K, T> TailMap(K start)
        {
            MyTreeMap<K, T> returnTree = new MyTreeMap<K, T>();

            Queue<TreeElement> queue = new Queue<TreeElement>();
            TreeElement copy = root;


            queue.Enqueue(copy);
            while (queue.Count > 0)
            {
                TreeElement branch = queue.Dequeue();
                if (comparer.Compare(branch.Key, start) > 0)
                {
                    returnTree.Put(branch.Key, branch.Value);

                }
                if (branch.right != null)
                    queue.Enqueue(branch.right);
                if (branch.left != null)
                    queue.Enqueue(branch.left);

            }
            return returnTree;

        }




        private IEnumerable<Tuple<K, T>> BFS(K key)
        {
            TreeElement copy = root;
            Queue<TreeElement> queue = new Queue<TreeElement>();
            queue.Enqueue(copy);
            while (queue.Count > 0)
            {
                copy = queue.Dequeue();

                Tuple<K, T> pair = new Tuple<K, T>(copy.Key, copy.Value);
                yield return pair;
                if (copy.left != null)
                    queue.Enqueue(copy.left);
                if (copy.right != null)
                    queue.Enqueue(copy.right);
            }
        }



        public class TreeElement
        {
            public TreeElement left = null;
            public TreeElement right = null;
            public T Value;
            public K Key;
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
        public T Get(K key)
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

        public K[] GetKeySet()
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

    public class MyLinkedList<T>
    {
        private class ListEl<T>
        {
            internal protected ListEl<T> next = null;
            internal protected ListEl<T> prev = null;
            internal protected T value = default(T);
        }

        ListEl<T> first = null;
        ListEl<T> last = null;
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
            ListEl<T> iterator = first;
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
            ListEl<T> iterator = first;
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
            ListEl<T> iterator = first;
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
            ListEl<T> iterator = first;
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

            ListEl<T> iterator = new ListEl<T>();
            iterator.value = obj;
            iterator.next = first;
            iterator.prev = null;
            first.prev = iterator;
            first = iterator;
            return true;
        }


        public bool OfferLast(T obj)
        {

            ListEl<T> iterator = new ListEl<T>();
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

            ListEl<T> iterator = last;
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
            ListEl<T> iterator = first;
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
