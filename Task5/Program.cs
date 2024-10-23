public class sdas
{
    static List<string> DeleteRetry(List<string> array)
    {
        List<string> list = new List<string>();
        for (int i = 0; i < array.LongCount(); i++)
        {
            bool flag = true;
            for (int j = 0; j < list.LongCount(); j++)
            {
                if (array[i].ToLower() == list[j].ToLower()) { flag = false; }
            }
            if (flag) { list.Add(array[i]); }
        }
        return list;
    }
    static public string Reading(StreamReader reat)
    {
        char[] prob = { ' ', '\n' };
        string itText = reat.ReadToEnd();
        string[] itArray = itText.Split(prob, StringSplitOptions.RemoveEmptyEntries);
        string result = "";
        int count = 0;
        for (int i = 0; i < itArray.Length; i++)
        {
            string sresult = "";
            string req = itArray[i];
            if (req[0] == '<')
            {
                sresult += '<';
                int j;
                if (req[1] == '/') { j = 2; sresult += '/'; }
                else j = 1;
                if ((req[j] >= 'A' && req[j] <= 'Z') || (req[j] >= 'a' && req[j] <= 'z'))
                {
                    sresult += req[j];
                    j++;
                    for (; j < req.Length - 1; j++)
                    {
                        if ((req[j] >= '0' && req[j] <= '9') || (req[j] >= 'A' && req[j] <= 'Z') || (req[j] >= 'a' && req[j] <= 'z')) { sresult += req[j]; }
                        else { sresult = ""; break; }
                    }
                    if (req[req.Length - 1] != '>') { sresult = ""; }
                    else { sresult += ">"; }
                }
            }
            if (sresult != "") result = result + " " + sresult;
        }
        return result;
    }

    static void Main(string[] arg)
    {
        StreamReader sr = new StreamReader("input.txt");
        List<string> list = new List<string>();
        sdas sdas = new sdas();
        list.AddRange(Reading(sr).Split(" ", StringSplitOptions.RemoveEmptyEntries));
        list = DeleteRetry(list);
        foreach (string s in list)
        {
            Console.WriteLine(s);
        }
    }
}