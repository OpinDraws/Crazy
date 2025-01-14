using System;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using Task20;
namespace Task_20
{
    internal class Program
    {
        enum TypeEnum
        {
            Integer,
            Double,
            Float
        }
        static void Main(string[] args)
        {
            Regex regex;
            MyHashMap<string, Tuple<TypeEnum, string>> myHashMap = new MyHashMap<string, Tuple<TypeEnum, string>>();

            string path = "input.txt";
            string line = "";
            StreamReader streamReader = new StreamReader(path);
            line = streamReader.ReadLine();
            try
            {
                while (line != null)
                {
                    regex = new Regex(@"(double|int|float) \S* ?(?:=) ?(\S)+?(?=;)");
                    var str = regex.Matches(line);
                    if (str.Count() > 0)
                    {
                        foreach (Match symbols in str)
                        {
                            Regex numbers = new Regex(@"(\d(,|\.)?([^\D]*)$)");
                            Regex Type = new Regex(@"\w*(?= )");
                            Regex Name = new Regex(@"(?<= )\w+");
                            Match answerNum = numbers.Match(symbols.Value);

                            Match answerType = Type.Match(symbols.Value);
                            Match answerName = Name.Match(symbols.Value);


                            if (answerNum.Success && answerName.Success && answerType.Success)
                            {
                                string number = answerNum.Value;
                                double result;
                                if (double.TryParse(number, out result))
                                    if (result % 1 != 0 && answerType.Value == "int")
                                    {
                                        Console.WriteLine($"Несоответствие типов в строке {symbols.Value}");
                                    }
                                    else
                                    {
                                        TypeEnum e;
                                        if (answerType.Value == "int")
                                            e = TypeEnum.Integer;
                                        else if (answerType.Value == "float")
                                            e = TypeEnum.Float;
                                        else
                                            e = TypeEnum.Double;


                                        Tuple<TypeEnum, string> tuple = new Tuple<TypeEnum, string>(e, number);
                                        if (myHashMap.Size() == 0)
                                            myHashMap.Put(answerName.Value, tuple);
                                        else
                                        {
                                            if (myHashMap.ContainsKey(answerName.Value))
                                                Console.WriteLine($"Попытка переопределить переменную {answerName.Value} значением {tuple}");
                                            else
                                            {
                                                myHashMap.Put(answerName.Value, tuple);

                                            }

                                        }


                                    }

                            }

                            else
                                Console.WriteLine($"Строка {symbols} не корректна");

                        }
                    }
                    else
                    {
                        Regex wrong = new Regex(@"(.*?;)|(.*$)|(.*? )");
                        MatchCollection matchCollection = wrong.Matches(line);
                        foreach (Match match in matchCollection)
                        {
                            if (match.Value != " " && match.Value != "")
                                Console.WriteLine($"Строка {match.Value} не корректна определена");

                        }

                    }
                    line = streamReader.ReadLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            path = "answer.txt";
            StreamWriter streamWriter = new StreamWriter(path);
            string[] Keys = myHashMap.KeySet();
            try
            {
                foreach (string el in Keys)
                {
                    Tuple<TypeEnum, string> pair = myHashMap.Get(el);
                    string answer = pair.Item1 + "=> " + el + $"({pair.Item2})";
                    streamWriter.WriteLine(answer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                streamReader.Close();
                streamWriter.Close();
            }
            streamWriter.Close();
            streamReader.Close();
        }
    }
}