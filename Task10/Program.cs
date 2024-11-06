internal class Program
{

    static void Main()
    {
        int[] a = {3, 2,5, 3,2, 1, 4, 11};
        Heap<int> heap = new Heap<int>(a);
        
        heap.Print();
        Console.WriteLine("Максимальный элемент: ");
        Console.WriteLine(heap.GetMax());

        Console.WriteLine("Добавим массив {7, 6, 8}, сделав из него кучу");
        int[] b = {7, 6, 8};
        Heap<int> heap1 = new Heap<int>(b);
        heap.Add(heap1);
        heap.Print();
        Console.WriteLine("Добавим элемент 10, сделав из него кучу");
        heap.Add(10);
        heap.Print();
        Console.WriteLine("Удалим 2 максимальных элементов");
        heap.RemoveMax();
        heap.RemoveMax();
        heap.Print();
        Console.WriteLine("При этом сама куча сейчас выглядит так, если идти по индексам: ");
        Console.WriteLine(heap);
        Console.WriteLine("Поменяем значение кучи, что находится на 3 индексе: ");
        heap.KeyChange(3, 9);
        heap.Print();
    }
    public class Heap<T> where T: IComparable
    {
        private T[] array;
        private int count;

        public Heap(T[] array)
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
          
            Heap<T> help = new Heap<T>(array);
            for (int i = 0;i<count; i++)
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
                       indexA= indexB;
                        indexB=(indexA + 1) / 2;
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

        public void Add(Heap<T> heap)
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
    }
}