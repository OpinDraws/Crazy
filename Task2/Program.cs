using System;
internal class CombplexNumber
{
    public struct ComplexNumber
    {

        private double x;
        private double y;

        public void Create(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            if (y >= 0) return $"{x}+{y}i";
            return $"{x}-{(-1) * y}i";
        }

        public void Mult(ComplexNumber factor)
        {

            this.x = x * factor.x - y * factor.y;
            this.y = y * factor.x + x * factor.y;


        }
        public void Divine(ComplexNumber factor)
        {


            this.x = (x * factor.x - (-y * factor.y)) / (Math.Pow(factor.x, 2) + Math.Pow(factor.y, 2));
            this.y = (y * factor.x + x * (-factor.y)) / (Math.Pow(factor.x, 2) + Math.Pow(factor.y, 2));

        }

        public double Argument()
        {
            if (x != 0)
            { return Math.Atan(y / x); }
            else return Math.Atan(y);
        }
        public void AddNum(ComplexNumber term)
        {

            this.x = x + term.x;
            this.y = y + term.y;

        }
        public void SubNum(ComplexNumber subtrahend)
        {

            this.x = x - subtrahend.x;
            this.y = y - subtrahend.y;

        }

        public double Module()
        {
            return Math.Sqrt(x * x + y * y);
        }

        double Imaginary()
        {
            return y;
        }
        double Material()
        {
            return y;
        }


    }


    public static void Main(string[] rack)
    {
        ComplexNumber xy = new ComplexNumber();
        ComplexNumber app = new ComplexNumber();

        bool f = true;
        while (f)
        {
            Console.WriteLine("Возможные действия в данном проекте:");
            Console.WriteLine("1 - создание комплексного числа");
            Console.WriteLine("2 - вывод комплексного числа, что сейчас есть");
            Console.WriteLine("3 - сложение другого комплексного числа к тому, что есть");
            Console.WriteLine("4 - вычитание другого комплексного числа от того, что есть");
            Console.WriteLine("5 - вывод аргумента данного комлпексного числа");
            Console.WriteLine("6 - вывод модуля данного комлпексного числа");

            Console.WriteLine("7 - умножение другого комплексного числа на то, что есть");
            Console.WriteLine("8 - деление другого комплексного числа на то, что есть");
            Console.WriteLine("Q - выход из проекта");
            string action = (string)Console.ReadLine();
            double x, y;
            switch (action)
            {
                case "1":
                    Console.Write("Введите вещественную часть: ");
                    x = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите мнимую часть: ");
                    y = Convert.ToDouble(Console.ReadLine());


                    xy.Create(x, y);
                    Console.WriteLine($"Создано число: {xy}");

                    break;
                case "2":
                    Console.WriteLine($"Ваше число: {xy}");
                    break;
                case "3":
                    Console.Write($"Введите вещественную часть числа, что хотите сложить: ");

                    x = Convert.ToDouble(Console.ReadLine());
                    Console.Write($"Введите мнимую часть числа, что хотите сложить: ");

                    y = Convert.ToDouble(Console.ReadLine());
                    app.Create(x, y);
                    xy.AddNum(app);
                    Console.WriteLine($"Ваш результат: {xy}");

                    break;
                case "4":


                    Console.Write("Введите вещественную часть числа, что хотите вычисть: ");
                    x = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите мнимую часть числа, что хотите вычисть: ");
                    y = Convert.ToDouble(Console.ReadLine());
                    app.Create(x, y);
                    xy.SubNum(app);
                    Console.WriteLine($"Ваш результат: {xy}");
                    break;
                case "5":
                    Console.WriteLine($"Аргумент вашего числа: {xy.Argument()}");
                    break;
                case "6":
                    Console.WriteLine($"Модуль вашего числа: {xy.Module()}");
                    break;
                case "7":
                    Console.Write("Введите вещественную часть числа, на которое хотите умножить: ");
                    x = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите мнимую часть числа, на которое хотите умножить: ");
                    y = Convert.ToDouble(Console.ReadLine());
                    app.Create(x, y);
                    xy.Mult(app);
                    Console.WriteLine($"Ваш результат: {xy}");
                    break;
                case "8":
                    Console.Write("Введите вещественную часть числа, на которое хотите поделить: ");
                    x = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите мнимую часть числа, на которое хотите поделить: ");
                    y = Convert.ToDouble(Console.ReadLine());
                    app.Create(x, y);
                    xy.Divine(app);
                    Console.WriteLine($"Ваш результат: {xy}");
                    break;
                case "Q":
                    Console.WriteLine("Удачи");
                    f = false;
                    break;
                case "q":
                    Console.WriteLine("Удачи");
                    f = false;
                    break;
                default:

                    break;
            }
            Console.WriteLine("");

        }
    }
}