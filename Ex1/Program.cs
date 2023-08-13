using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Text.RegularExpressions;


namespace Ex1
{
    class MathOp
    {
        private double op1;
        private double ?op2;
        private string operation;
       public  MathOp(string operation, double op1, double? op2 =null)
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
                    result = op1+op2.GetValueOrDefault();
                    break;
                case "-":
                        result = op1 - op2.GetValueOrDefault();
                    break;
                case "*":
                    result = op1 * op2.GetValueOrDefault();
                    break;
                case "/":
                    result = op1 / op2.GetValueOrDefault();
                    break;

                case "^":
                    result = Math.Pow( op1, op2.GetValueOrDefault()) ;
                    break;
                case "root":
                    result = Math.Pow(op1, 1/op2.GetValueOrDefault());
                    break;
                case "sqrt":
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
            Console.WriteLine("- Please Enter The Equation like this : [ OP1 operand OP2 ]");
            Console.WriteLine("- e.g. 10 + 20");
            Console.WriteLine("- Operators Allowed are [ + , - , * , / , ^ , sqrt (with 1 operand) , root ]");
            string input = Console.ReadLine();

            MathOp MathObj = new MathOp("sqrT",4);
            Console.WriteLine(MathObj);
            Console.ReadKey();
        }

        
    }
}
