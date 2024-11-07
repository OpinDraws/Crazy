using MyStack;
using System.Xml.Linq;

internal class Program
{
    static void Main(string[] args)
    {

       string[] snape = (string.Join(" ", args)).Split(' ');
        string equation = snape[0];
        ReversePolishNotation polk = new ReversePolishNotation();
        string[] vari = new string[snape.Length-1];
        for (int i = 1;i < snape.Length; i++)
        {
            vari[i-1] = snape[i];
        }
  
        double res = polk.Result(equation, vari);
        Console.WriteLine(res);
    }

}
