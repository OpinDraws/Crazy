using ObjectSort;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayGeneric
{
    public class Arrays
    {
        ArraySort SortObject = new ArraySort();
 

        public void RandShuffle(int[] array)
        {
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(0, 1000);


            }


        }

        public void RandSubarray(int[] array)
        {
            Random random = new Random();
            int size = random.Next(0, 3);
            size=(int)Math.Pow(10, size);
            int startRand=0;
            for (int i = 0, j=0; i<array.Length; i++, j++)
            {
                if (j<size-1) {
                    array[i] = random.Next(startRand, 1000);
                    startRand = array[i];
                }
                if (j == size-1)
                {
                    array[i] = random.Next(startRand, 1000);
                    startRand = 0;
                    j = 0;
                }
            }
            
        }

        public void RandSortA(int[] array)
        {
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(0, 1000);
            }
            Array.Sort(array);
            int countPer = rand.Next(1, 500);
            for (int i = 0;i < countPer; i++)
            {
                int per1 = rand.Next(0, array.Length);
                int per2=rand.Next(0, array.Length);
                int temp = array[per1];
                array[per1] = array[per2];
                array[per2] = temp;
            }
        }

        public void SameElInArray(int[] array)
        {
            Random rand = new Random();
            int same = rand.Next(0, 1000);
            for (int i = 0; i < array.Length; i++)
            {
                int chance = rand.Next(0, 2);
                if (chance == 0)
                {
                    array[i] = rand.Next(0, 1000);
                }
                else array[i] = same;
            }
        }
        public int[] Generaction(int index, int size)
        {
            int[] array = new int[size];
            switch (index) {
                case 0: { RandShuffle(array); break; }
                case 1: { RandSubarray(array); break; }
                case 2: { RandSortA(array); break; }
                case 3: { SameElInArray(array); break; }
            }
            return array;
        }
    }

  

}