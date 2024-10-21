using System.Collections.Generic;
using System;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Numerics;


namespace ObjectSort
{
    public class ArraySort
    {
        public void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b; b = temp;
        }
        public void BubbleSort(int[] array)
        {
            int capacity = array.Length;
            for (int i = 0; i < capacity; i++)
            {
                for (int j = 0; j < capacity - 1; j++)
                {
                    if (array[j] > array[j + 1]) { Swap(ref array[j], ref array[j + 1]); }
                }
            }
            capacity = capacity - 1;
        }

        public void ShakerSort(int[] array)
        {
            bool check = false;
            int end = array.Length - 1;
            int begin = 0;
            while (begin <= end && check == false)
            {
                check = true;
                for (int i = begin; i < end; i++)
                {
                    if (array[i] > array[i + 1]) { Swap(ref array[i], ref array[i + 1]); check = false; }
                }
                end -= 1;
                for (int i = end; i > begin; i--)
                {
                    if (array[i] < array[i - 1]) { Swap(ref array[i], ref array[i - 1]); check = false; }
                }
                begin += 1;
            }
        }

        public void CombSort(int[] array)
        {
            int capacity = array.Length;
            int step = capacity - 1;
            double coefficient = 1.25;
            while (step > 0)
            {

                for (int i = 0; i + step < array.Length; i += 1)
                {
                    if (array[i] > array[i + step]) { Swap(ref array[i], ref array[i + step]); }
                }
                step = (int)(step / coefficient);
            }

        }

        public void InsertionSort(int[] array)
        {
            int i, j, temp;
            for (i = 1; i < array.Length; i++)
            {
                temp = array[i];
                for (j = i - 1; j >= 0; j--)
                {
                 
                    array[j + 1] = array[j];
                    array[j] = temp;
                }
            }
        }

        public void ShellSort(int[] array)
        {
            int capacity = array.Length;
            int step = (array.Length - 1) / 2;
            while (step >= 1)
            {
                for (int i = 0 + step; i < capacity - 1; i++)
                {
                    int j = i;
                    int diff = j - step;
                    while (diff >= 0 && array[diff] > array[j])
                    {
                        Swap(ref array[diff], ref array[j]);
                        j = diff;
                        diff = j - step;
                    }
                }
                step /= 2;
            }
        }

        public void TreeSort(int[] array)
        {
            int capacity = array.Length;
            Tree tree = new Tree();
            tree.val = array[0];
            for (int i = 1; i < capacity; i++)
            {
                Tree.Manure(tree, array[i]);
            }

            int j = 0;
            ArraySwapTree(array, tree);
            void ArraySwapTree(int[] array2, Tree tree2)
            {
                if (tree2 != null)
                {
                    ArraySwapTree(array2, tree2.left);
                    array2[j] = tree2.val;
                    j++;
                    ArraySwapTree(array2, tree2.right);
                }
            }
        }

        public void DwarfSort(int[] array)
        {
            int capacity = array.Length;
            int i = 0;
            int j = 1;
            while (i < capacity)
            {
                if (i == 0 || array[(i - 1)] <= array[i]) i++;
                else { Swap(ref array[i - 1], ref array[i]); i--; }
            }
        }

        public void SelectionSort(int[] array)
        {
            int capacity = array.Length;
            for (int i = 0; i < capacity; i++)
            {
                int minVal = array[i];
                int index = i;
                for (int j = i + 1; j < capacity; j++)
                {
                    if (array[j] < minVal) { minVal = array[j]; index = j; }
                }
                Swap(ref array[i], ref array[index]);
            }

        }

        public void QuickSort(int[] array, int fBegin, int fEnd)
        {

            int mid = array[(fBegin + fEnd) / 2];
            int end = fEnd;
            int begin = fBegin;
            while (begin <= end)
            {
                while (array[begin] < mid) { begin++; }
                while (array[end] > mid) { end--; }
                if (begin <= end) { Swap(ref array[begin], ref array[end]); begin++; end--; }
            }
            if (fBegin < end) { QuickSort(array, fBegin, end); }
            if (begin < fEnd) { QuickSort(array, begin, fEnd); }
        }

        public static int GetMaxVal(int[] array, int size)
        {
            var maxVal = array[0];
            for (int i = 1; i < size; i++)
                if (array[i] > maxVal)
                    maxVal = array[i];
            return maxVal;
        }
        public void CountingSort(int[] array)
        {
            var min = array[0];
            var max = array[0];
            foreach (int element in array)
            {
                if (element > max)
                {
                    max = element;
                }
                else if (element < min)
                {
                    min = element;
                }
            }
            var correctionFactor = min != 0 ? -min : 0;
            max += correctionFactor;

            var count = new int[max + 1];
            for (var i = 0; i < array.Length; i++)
            {
                count[array[i] + correctionFactor]++;
            }

            var index = 0;
            for (var i = 0; i < count.Length; i++)
            {
                for (var j = 0; j < count[i]; j++)
                {
                    array[index] = i - correctionFactor;
                    index++;
                }
            }
        }

        static void Merge(int[] a, int l, int m, int r)
        {
            int i, j, k;

            int n1 = m - l + 1;
            int n2 = r - m;

            int[] left = new int[n1 + 1];
            int[] right = new int[n2 + 1];

            for (i = 0; i < n1; i++)
            {
                left[i] = a[l + i];
            }

            for (j = 1; j <= n2; j++)
            { 
            
                right[j - 1] = a[m + j];
            }

            left[n1] = int.MaxValue;
            right[n2] = int.MaxValue;

            i = 0;
            j = 0;

            for (k = l; k <= r; k++)
            {
                if (left[i] < right[j])
                {
                    a[k] = left[i];
                    i = i + 1;
                }
                else
                {
                    a[k] = right[j];
                    j = j + 1;
                }
            }
        }
        public void MergeSort(int[] a, int l, int r)
        {
            int q;

            if (l < r)
            {
                q = (l + r) / 2;
                MergeSort(a, l, q);
                MergeSort(a, q + 1, r);
                Merge(a, l, q, r);
            }
        }

        public void RadixSort(int[] array)
        {
            int[] help = new int[array.Length];
            int radix = 2;
            int b = 32;
            int[] count = new int[1 << radix];
            int[] pref = new int[1 << radix];
            int groups = 8;
            int mask = (1 << radix) - 1;

            for (int c = 0, shift = 0; c < groups; c++, shift += radix)
            {
                for (int j = 0; j < count.Length; j++)
                    count[j] = 0;


                for (int i = 0; i < array.Length; i++)
                {
                    int test = array[i];
                    var first = array[i] >> shift;
                    var num = first & mask;
                    count[num]++;
                }
                pref[0] = 0;
                for (int i = 1; i < count.Length; i++)
                    pref[i] = pref[i - 1] + count[i - 1];


                for (int i = 0; i < array.Length; i++)
                {
                    var first = array[i] >> shift;
                    var num = first & mask;
                    help[pref[num]++] = array[i];
                }

                help.CopyTo(array, 0);
            }
        }
        public int[] BitonicSort(ref int[] array)
        {


            int n = array.Length;
            int k, j, l, i, temp;
            int cond = n & (n - 1);
            if (cond != 0)
            {
                int pow = 1;
                int sn = n;
                while (sn != 0)
                {
                    sn /= 2;
                    pow *= 2;
                }
                n = pow;
            }
            int[] newArray = new int[n];

            for (int it = 0; it < newArray.Length; it++)
            {
                if (it < array.Length)
                {
                    newArray[it] = array[it];

                }
                else newArray[it] = -1;
            }
            for (k = 2; k <= n; k *= 2)
            {
                for (j = k / 2; j > 0; j /= 2)
                {
                    for (i = 0; i < n; i++)
                    {
                        l = i ^ j;
                        if (l > i)
                        {
                            if (((i & k) == 0) && (newArray[i] > newArray[l]) || (((i & k) != 0) && (newArray[i] < newArray[l])))
                            {
                                Swap(ref newArray[i], ref newArray[l]);

                            }
                        }
                    }
                }
            }

            return newArray;
        }

        public void HeapSort(ref int[] array)
        {
            Heap sort = new Heap();
            sort.Sort(array);
        }

        public void SortChose(int st,int[] array)
        {
            switch (st)
            {
                case 0: { this.BubbleSort(array); break; }
                case 1: { this.InsertionSort(array); break; }
                case 2: { this.SelectionSort(array); break; }
                case 3: { this.ShakerSort(array); break; }
                case 4: { this.DwarfSort(array); break; }
                case 5: { this.BitonicSort(ref array); break; }
                case 6: { this.ShellSort(array); break; }
                case 7: { this.TreeSort(array); break; }
                case 8: { this.CombSort(array); break; }
                case 9: { this.HeapSort(ref array); break; }
                case 10: { this.QuickSort(array, 0, array.Length - 1); break; }
                case 11: { this.MergeSort(array, 0, array.Length-1); break; }
                case 12: { this.CountingSort(array); break; }
                case 13: { this.RadixSort(array); break; }
            }

        }

    


    public class Tree
        {
            public int val;
            public Tree left = null;
            public Tree right = null;

            public static void Manure(Tree root, int val)
            {

                if (root.val > val)
                {
                    if (root.left == null)
                    {
                        Tree Branch = new Tree();
                        Branch.val = val;
                        root.left = Branch;
                    }
                    else Manure(root.left, val);
                }
                else
                {
                    if (root.right == null)
                    {
                        Tree Branch = new Tree();
                        Branch.val = val;
                        root.right = Branch;
                    }
                    else Manure(root.right, val);
                }
            }

        }


        public class Heap
        {
            public void Sort(int[] arr)
            {
                int n = arr.Length;
                for (int i = n / 2 - 1; i >= 0; i--)
                    heapify(arr, n, i);
                for (int i = n - 1; i >= 0; i--)
                {
                    int temp = arr[0];
                    arr[0] = arr[i];
                    arr[i] = temp;
                    heapify(arr, i, 0);
                }
            }
            void heapify(int[] arr, int n, int i)
            {
                int largest = i;
                int l = 2 * i + 1;
                int r = 2 * i + 2;
                if (l < n && arr[l] > arr[largest])
                    largest = l;
                if (r < n && arr[r] > arr[largest])
                    largest = r;
                if (largest != i)
                {
                    int swap = arr[i];
                    arr[i] = arr[largest];
                    arr[largest] = swap;
                    heapify(arr, n, largest);
                }
            }
            static void printArray(int[] arr)
            {
                int n = arr.Length;
                for (int i = 0; i < n; ++i)
                    Console.Write(arr[i] + " ");
                Console.Read();
            }
            
        }
    }
 }
