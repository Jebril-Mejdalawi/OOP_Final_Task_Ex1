using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Text.RegularExpressions;
using System.IO;


namespace Ex1
{
    class MathOp
    {
        private double op1;
        private double? op2;
        private string operation;

        public MathOp(string operation, double op1, double? op2 = null)
        {
            this.operation = operation;
            this.op1 = op1;
            this.op2 = op2;
        }

        public double OP1
        {
            set { op1 = value; }
            get { return op1; }
        }

        public double OP2
        {
            set { op2 = value; }
            get { return op2.GetValueOrDefault(); }
        }

        public string Operation
        {
            set { operation = value; }
            get { return operation; }
        }

        public double calculation()
        {
            double result = 0;
            switch (operation.ToLower())
            {
                case "+":
                    result = op1 + op2.GetValueOrDefault();
                    break;
                case "-":
                    result = op1 - op2.GetValueOrDefault();
                    break;
                case "*":
                    result = op1 * op2.GetValueOrDefault();
                    break;
                case "/":
                    if (op2 == 0)
                        throw new DivideByZeroException("Division by zero is not allowed.");
                    result = op1 / op2.GetValueOrDefault();
                    break;

                case "^":
                    result = Math.Pow(op1, op2.GetValueOrDefault());
                    break;
                case "root":
                    if (op2 % 2 == 0 && op1 < 0)
                        throw new ArithmeticException("Cannot find even root of a negative number.");
                    result = Math.Pow(op1, 1 / op2.GetValueOrDefault());
                    break;
                case "sqrt":
                    if (op1 < 0)
                        throw new ArithmeticException("Cannot find the square root of a negative number.");
                    result = Math.Sqrt(op1);
                    break;
                default:
                    Console.WriteLine("This operation is not supported");
                    break;
            }
            return result;
        }

        public override string ToString() { return $"{op1} {operation} {op2} = {calculation()}"; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //I made a loop so the user can enter as many mathmatical expressions as he want
            while (true)
            {
                try
                {
                    Console.WriteLine("- Please Enter The Equation like this : [ OP1 operand OP2 ]");
                    Console.WriteLine("- e.g. 10 + 20 , e.g. 4 sqrt");
                    Console.WriteLine("- Operators Allowed are [ + , - , * , / , ^ , sqrt (with 1 operand) , root ]");
                    Console.Write("\n\n\n----->\t");
                    string input = Console.ReadLine();
                    //I used a regular expression to fetch the operands and operator from one line of user input
                    string reg = @"^(-?\d+(\.\d+)?)\s*(\+|\-|\*|\/|\^|\bROOT\b|\bSQRT\b)\s*(-?\d+(\.\d+)?)?$";
                    Match m = Regex.Match(input, reg, RegexOptions.IgnoreCase);

                    if (m.Success)
                    {
                        if (!double.TryParse(m.Groups[1].Value, out double number1))
                        {
                            throw new FormatException();
                        }
                        string operation = m.Groups[3].Value;
                        double? number2 = null;
                        if (m.Groups[4].Value != "" && double.TryParse(m.Groups[4].Value, out double parsedNum2))
                        {
                            number2 = parsedNum2;
                        }
                        else if (m.Groups[4].Value != "")
                        {
                            throw new FormatException();
                        }

                        Console.WriteLine($"Number1: {number1}");
                        Console.WriteLine($"Operation: {operation}");
                        Console.WriteLine($"Number2: {number2}");
                        MathOp MathObj = new MathOp(operation, number1, number2);
                        Console.WriteLine(MathObj);
                        Console.WriteLine("\n\n\n\n --------------------------------------------------------\n\n\n\n");
                        //break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid expression. Enter the Expression Again.");
                    }
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArithmeticException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error in number format. Please enter valid numbers.");
                }
            }
            Console.ReadKey();
        }
    }
}
