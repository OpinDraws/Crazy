using MyStack;
using System.Xml.Linq;

internal class Program
{
    static void Main(string[] args)
    {

        string equation = string.Join(",", args);
        ReversePolishNotation polk = new ReversePolishNotation();   
        double res = polk.Result(equation);
        Console.WriteLine(res);
    }

    public class ReversePolishNotation
    {

        public double Result(string equation)
        {
            string polk = ConvertNote(equation);
            Console.WriteLine(polk);
            return Calculator(polk);
        }

        private static string ConvertNote(string equation)
        {
            string result = "";
            MyStack<string> oper = new MyStack<string>();
            for (int i = 0; i < equation.Length; i++)
            {
                if (Char.IsDigit(equation[i]))
                {

                    result += PopNumber(equation, ref i) + " ";
                }
                else if (Char.IsLetter(equation[i]))
                {
                    string name = PopText(equation, ref i);
                    if (Weight(name) != 0)
                    {
                        while (!oper.Empty() && (Weight(oper.Peek()) >= Weight(name)))
                        {
                            result += oper.Peek() + " ";
                            oper.Pop();
                        }
                        oper.Push(name);
                    }
                }
                else if (Weight(Convert.ToString(equation[i])) != 0)
                {
                    string name = Convert.ToString(equation[i]);
                    while (!oper.Empty() && (Weight(oper.Peek()) >= Weight(name)))
                    {
                        result += oper.Peek() + " ";
                        oper.Pop();
                    }

                    oper.Push(name);
                }

                else if (equation[i] == '(' && equation[i + 1] == '-' && i + 2 < equation.Length)
                {

                    if (Char.IsDigit(equation[i + 2]))
                    {
                        i += 2;
                        string name = PopNumber(equation, ref i);
                        result += "!" + name + " ";
                        oper.Push("(");
                    }
                }
                else if (equation[i] == '(')
                {
                    oper.Push("(");
                }
                else if (equation[i] == ')')
                {
                    while (oper.Peek() != "(")
                    {
                        result += oper.Peek() + " ";
                        oper.Pop();
                    }
                    oper.Pop();
                }
                else if ((equation[i] == '/' && equation[i + 1] == '/'))
                {
                    string name = "//";
                    i += 1;
                    while (!oper.Empty() && (Weight(oper.Peek()) >= Weight(name)))
                    {
                        result += oper.Peek() + " ";
                        oper.Pop();
                    }
                    oper.Push(name);
                }

            }
            while (!oper.Empty())
            {
                result += oper.Peek() + " ";
                oper.Pop();
            }
            return result;
        }

        private static string PopNumber(string expr, ref int pos)
        {
            string output = "";
            for (; pos < expr.Length; pos++)
            {
                if (Char.IsDigit(expr[pos]) || expr[pos] == ',')
                {
                    output += expr[pos];
                }
                else
                {
                    pos--;
                    break;
                }
            }
            return output;
        }

        private static string PopText(string expr, ref int pos)
        {
            string output = "";
            for (; pos < expr.Length; pos++)
            {
                if (Char.IsLetter(expr[pos]) || expr[pos] == '/')
                {
                    output += expr[pos];
                }
                else
                {
                    pos--;
                    break;
                }
            }
            return output;
        }

        private static double Calculator(string note)
        {
            string result = "";
            MyStack<double> numbers = new MyStack<double>();
            for (int i = 0; i < note.Length; i++)
            {
                if (note[i] != ' ')
                {
                    if (Char.IsDigit(note[i]))
                    {
                        double val = Convert.ToDouble(PopNumber(note, ref i));
                        numbers.Push(val);
                    }
                    else if (Weight(Convert.ToString(note[i])) != 0)
                    {
                        {
                            string oper = Convert.ToString(note[i]);
                            numbers.Push(Count(oper, numbers));
                        }
                    }
                    else if (note[i] == '/' && note[i + 1] == '/')
                    {
                        i += 1;
                        numbers.Push(Count("//", numbers));
                    }
                    else if (note[i] == '!')
                    {
                        i += 1;
                        double val = Convert.ToDouble(PopNumber(note, ref i));
                        numbers.Push(-val);
                    }
                }

            }

            return numbers.Peek();
        }

        static public int Weight(string opertator)
        {
            switch (opertator)
            {
                case "+":
                    return 2;
                case "-":
                    return 2;
                case "*":
                case "/":
                case "//":
                    return 3;
                case "^":
                    return 4;
                case "sqrt":
                case "ln":
                case "cos":
                case "sin":
                case "tg":
                case "ctg":
                case "abs":
                case "log":
                case "min":
                case "max":
                case "mod":
                case "exp":
                case "trunc":
                    return 5;
                case "%":
                    return 6;
                default: return 0;
            }
        }

        private static double Count(string oper, MyStack<double> numbers)
        {
            switch (oper)
            {
                case "+":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();
                        double y = numbers.Peek();
                        numbers.Pop();
                        return x + y;

                    }

                case "-":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();
                        double y = numbers.Peek();
                        numbers.Pop(); return x - y;
                    }
                case "*":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();
                        double y = numbers.Peek();
                        numbers.Pop();
                        return x * y;

                    }
                case "/":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();
                        double y = numbers.Peek();
                        numbers.Pop();
                        if (y != 0)
                        {
                            return x / y;

                        }
                        else throw new Exception("Деление на ноль"); ;

                    }
                case "^":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();
                        double y = numbers.Peek();
                        numbers.Pop();
                        return Math.Pow(x, y);

                    }
                case "//":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();
                        double y = numbers.Peek();
                        numbers.Pop();
                        return Math.Floor(x / y);

                    }
                case "%":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();
                        double y = numbers.Peek();
                        numbers.Pop();
                        return (int)x % (int)y;

                    }
                case "abs":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();
                        return Math.Abs(x);
                    }
                case "sqrt":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();

                        return Math.Sqrt(x);
                    }
                case "ln":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();

                        return Math.Log(x);
                    }
                case "lg":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();

                        return Math.Log10(x);
                    }
                case "sin":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();

                        return Math.Sin(x);
                    }
                case "cos":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();

                        return Math.Cos(x);
                    }
                case "tg":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();

                        return Math.Tan(x);
                    }
                case "ctg":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();

                        return 1 / Math.Tan(x);
                    }
                case "exp":
                    return Math.E;
                case "e":
                    return Math.E;
                case "max":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();
                        double y = numbers.Peek();
                        numbers.Pop();
                        if (x > y) { return x; }
                        else return y;
                    }
                case "min":
                    {
                        double x = numbers.Peek();
                        numbers.Pop();
                        double y = numbers.Peek();
                        numbers.Pop();
                        if (x > y) { return y; }
                        else return x;
                    }
                default:
                    Console.WriteLine($"Введите переменную: {oper}");
                    double val = Convert.ToDouble(Console.ReadLine());
                    return val;

            }
        }
    }
}