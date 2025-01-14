using System.Collections;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace Task_21
{
    public class MyComparator<T> : Comparer<T> where T : IComparable
    {
        public override int Compare(T? x, T? y)
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
                    if (comparer.Compare(AddVal.Key, root.Key) < 0)
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



    internal class Program
    {
        static void Main(string[] args)
        {
            MyTreeMap<int, int> myTreeMap = new MyTreeMap<int, int>();
            myTreeMap.Put(15, 1);
            myTreeMap.Put(9, 2);
            myTreeMap.Put(25, 3);
            myTreeMap.Put(7, 4);

            myTreeMap.Put(13, 5);
            myTreeMap.Put(16, 6);
            myTreeMap.Put(18, 7);
            myTreeMap.Put(4, 8);

            myTreeMap.Put(8, 9);
            myTreeMap.Put(3, 10);
            myTreeMap.Put(2, 11);
            myTreeMap.Put(1, 12);
            MyTreeMap<int, int> secTree = myTreeMap.TailMap(9);
            secTree.PrintTree();
        }
    }
}