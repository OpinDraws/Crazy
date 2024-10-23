internal class Project
{
    public class MyArrayList<T>
    {
        private T[] array;
        private int size;
        public MyArrayList()
        {
            this.array = new T[1];
        }
        public MyArrayList(T[] a)
        {
            int n = a.Length;
            this.array = new T[n];
            this.size = n;
            for (int i = 0; i < n; i++)
            {
                this.array[i] = a[i];
            }
        }
        public MyArrayList(int capacity)
        {
            this.array = new T[capacity];
            this.size = 0;
        }
        public void Add(T e)
        {

            if (array.Length > size) { array[size] = e; size++; }
            else
            {
                T[] temp = new T[(int)(array.Length * 1.5)];
                for (int i = 0; i < array.Length; i++)
                {
                    temp[i] = array[i];
                }
                temp[size] = e;
                size++;
                this.array = temp;
            }
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
        }
        public void Add(T e, int index)
        {
            if (array.Length > size)
            {

                for (int j = index; j < size; j++)
                {
                    array[j + 1] = array[j];
                }
                array[index] = e;
                size++;
            }
            else
            {
                T[] temp = new T[(int)(array.Length * 1.5)];
                for (int i = 0; i < array.Length; i++)
                {
                    temp[i] = array[i];
                }
                for (int j = index; j < size; j++)
                {
                    temp[j + 1] = temp[j];
                }

                temp[index] = e;
                size++;
                this.array = temp;
            }
        }
        public void AddAll(T[] e, int index)
        {
            int step = e.Length;
            if (array.Length > size + step)
            {

                for (int j = index; j < size; j++)
                {
                    array[j + step] = array[j];
                }
                for (int j = index; j < index + step; j++)
                {
                    array[j] = e[j];
                    size++;
                }

            }
            else
            {
                T[] temp = new T[(int)(array.Length * 1.5)];
                for (int i = 0; i < array.Length; i++)
                {
                    temp[i] = array[i];
                }
                for (int j = index + step; j < size; j++)
                {
                    temp[j + step] = temp[j];
                }

                for (int j = index; j < index + step; j++)
                {
                    temp[j] = e[j - step];
                    size++;
                }

                this.array = temp;
            }
        }
        public T Get(int index)
        {
            return array[index];
        }
        public void ShowList()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{array[i]} ");
            }
        }
        public object IndexOf(object o)
        {
            for (int i = 0; i < size; i++)
            {

                if (array[i].Equals(o))
                {
                    return (object)i;
                }
            }
            return -1;
        }
        public int LastIndexOf(object o)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (array[i].Equals(o))
                {
                    return i;
                }
            }
            return -1;
        }
        public T RemoveReturn(int index)
        {
            T ret = array[index];
            for (int i = index; i < size; i++)
            {
                for (int j = i; j < size - 1; j++)
                {
                    array[j] = array[j + 1];
                }
                size--;
            }
            return ret;
        }
        public void Set(int index, T e)
        {
            array[(int)index] = e;
        }
        public T[] Sublist(int indexA, int indexB)
        {
            T[] helpArray = new T[indexB - indexA + 1];
            for (int j = 0, i = indexA; i <= indexB; j++, i++)
            {
                helpArray[j] = array[i];
            }
            return helpArray;
        }
        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                array[i] = (T)default;
            }
            size = 0;
        }
        public bool Contains(object val)
        {
            for (int i = 0; (i < size); i++)
            {
                if (array[i].Equals(val) == val.Equals(val)) return true;
            }
            return false;
        }
        public bool ContainsAll(T[] a)
        {
            int counter = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < a.Length; j++) { if (array[i].Equals(a[j]) == a[j].Equals(a[j])) counter++; }
            }
            if (counter == a.Length) return true;
            return false;
        }
        public bool IsEmpty()
        {
            if (size == 0) return true;
            return false;
        }
        public void RemoveOnly(object val)
        {
            for (int i = 0; i < size; i++)
            {
                if (array[i].Equals(val) == val.Equals(val))
                {
                    for (int j = i; j < size - 1; j++)
                    {
                        array[j] = array[j + 1];
                    }
                    size--;
                    return;
                }
            }
        }
        public void Remove(object val)
        {
            for (int i = 0; i < size; i++)
            {
                if (array[i].Equals(val))
                {
                    for (int j = i; j < size - 1; j++)
                    {
                        array[j] = array[j + 1];
                    }
                    size--;
                }
            }
        }
        public void RemoveAll(T[] a)
        {
            int k = 0;
            for (int i = 0; i < size; i++)
            {

                for (int j = 0; j < a.Length; j++)
                {

                    if (array[i].Equals(a[j]))
                    {
                        for (int l = i; l < size; l++)
                        {
                            array[l] = array[l + 1];
                        }
                        k++;
                        size--;
                        if (k == a.Length) return;
                    }
                }
            }

        }
        public void RetainAll(T[] a)
        {
            for (int i = 0; i < size; i++)
            {
                bool flag = false;
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j].Equals(a[j]) == a[i].Equals(a[j])) { flag = true; break; }
                }
                if (!flag)
                {
                    for (int j = i; j < size - 1; j++)
                    {
                        array[j] = array[j + 1];
                    }
                    size--;
                }
            }
        }
        public int Size()
        {
            return (int)size;
        }

        public T[] toArray()
        {
            return array;
        }
        public T[] toArray(T[] a)
        {
            T[] help = new T[a.Length];
            int k = 0;
            for (int i = 0; i < size; i++)
            {
                bool flag = false;
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j].Equals(a[j]) == a[i].Equals(a[j])) { a[k] = a[j]; k++; break; }
                }
            }
            return help;
        }

        public T FirstElement()
        {
            return array[0];
        }
        public T LastElement()
        {
            return array[size - 1];
        }
        public void RemoveElementAt(int o)
        {
            for (int i = 0; i < size; i++)
            {
                if (o == i)
                {
                    for (int j = i; j < size - 1; j++)
                    {
                        array[j] = array[j + 1];
                    }
                    size--;
                    return;
                }
            }
        }
        public void RemoveElementAt(int o, int a)
        {
            for (int i = 0; i < size; i++)
            {
                if (o <= i && i <= a)
                {
                    for (int j = i; j < size - 1; j++)
                    {
                        array[j] = array[j + 1];
                    }
                    size--;
                    return;
                }
            }
        }

    }

    static void Main(string[] a)
    {
        MyArrayList<int> List = new MyArrayList<int>(10);
        List.Add(1);
        List.Add(4);
        List.Add(0);
        List.ShowList();
        Console.WriteLine($"{List.Size()}");
        List.Clear();
        List.Add(1);
        List.Add(1);
        List.Add(1);
        List.Add(1);
        List.ShowList();
        Console.WriteLine($"{List.Size()}");
        int[] array = { 2, 1, 6, 4 };
        List.Print();
        Console.WriteLine($"Символ по индексу 2: {List.Get(2)}");
        Console.WriteLine($"Индекс первой 1: {List.LastIndexOf(1)}");
        int[] Help = (int[])List.Sublist(1, 3);
        Console.WriteLine($"Результат сублиста с 1 по 3: ");
        for (int i = 0; i < Help.Length; i++)
        { Console.WriteLine(Help[i]); }
        List.RemoveOnly(1);
        Console.WriteLine("Удалим единицу 1 раз");
        List.Print();
        Console.WriteLine("Добавим к списку массив array");
        List.AddAll(array, 0);
        List.Print();
        Console.WriteLine("Удалим весь массив help");
        List.RemoveAll(Help);
        List.Print();




    }
}