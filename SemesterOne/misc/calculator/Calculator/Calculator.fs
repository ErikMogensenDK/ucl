using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        public int Add(int num1, int num2)        
        {
            return num1 + num2;
        }
        public int Subtract(int num1, int num2)        
        {
            return num1 - num2;
        }
        public double Divide(int num1, int num2)        
        {
            double num1Double = Convert.ToDouble(num1);
            double num2Double = Convert.ToDouble(num2);
            return num1Double / num2Double;
        }
        public int Multiply(int num1, int num2)        
        {
            return num1 * num2;
        }
    }
}