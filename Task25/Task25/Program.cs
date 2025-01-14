using System.Collections.Generic;
using Task25;
namespace Task_25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyHashSet<string> set = new MyHashSet<string>();
            string path = "input.txt";
            StreamReader stream = new StreamReader(path);
            string line = "";
            line = stream.ReadLine();
            while (line != null)
            {

                string word = "";
                bool flag = false;
                foreach (char sym in line)
                {
                    if (Char.IsLetter(sym))
                    {
                        word += sym.ToString().ToLower();
                        flag = true;
                    }
                    else
                    {
                        if (flag)
                        {
                            set.Add(word);
                            word = "";
                            flag = false;
                        }
                    }
                }

                line = stream.ReadLine();
            }
            string[] strings = set.ToArray();
            foreach (string s in strings)
            {
                Console.WriteLine(s);
            }

        }



    }
}