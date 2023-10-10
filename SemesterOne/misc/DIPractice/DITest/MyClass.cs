using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DITest
{
    public class MyClass : IMyClass
    {
        public void MyMethod()
        {
            Console.WriteLine("Successfuly called MyMethod");
        }
    }
}