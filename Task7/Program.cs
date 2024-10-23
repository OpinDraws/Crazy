using System.ComponentModel.Design;

class IPv4
{
    static public List<string> VectorRead()
    {
        StreamReader read = new StreamReader("input.txt");
        int i = 0;
        List<string> result = new List<string>();
        string line;
        while ((line = read.ReadLine()) != null)
        {
            string[] help = line.Split(" ");
            for (int j = 0; j < help.Length; j++) { 
                string test = CheckIP(help[j]);
                if (test != "") { if (TestCopy(result, test))result.Add(test); }
            }
        }
        return result;

    }

    static public string CheckIP(string ip)
    {
       
        string result="";
        string[] help = ip.Split(".");
        if (help.Length != 4) { return ""; }
        int check = 3;
        for (int i = 0; i < help.Length; i++)
        {
            try
            {
                if (Convert.ToInt32(help[i]) > 0 && Convert.ToInt32(help[i]) < 256) { if (i != 3) result += help[i] + "."; else result += help[i]; }
                else return "";
            }
            catch { return ""; }
        }
        for (int i =1; i < help.Length; i++)
        {
            if (help[i] == help[i - 1]) check--;
        }
        if (check == 0) return "";
        return result;
    }

    static public bool TestCopy(List<string> vector, string check)
    {
        for (int i=0; i < vector.Count; i++)
        {
            
                if (vector[i] == check) { return false; }

        }
        return true;
    }

    static void Main(string[] args)
    {
       List<string> vector = VectorRead();
        using StreamWriter sw = new StreamWriter("output.txt");
        for (int i=0; i < vector.Count; i++)
        {
            sw.WriteLine($"{vector[i]}");
        }
    }
}