using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeApp
{
    public class BaseClass
    {
        public BaseClass()
        {
            Console.WriteLine($"Baseclass: I was Created!");
        }
        public void BaseClassMethod()
        {
            Console.WriteLine($"MyText from the baseclass!");
        }
    }
}