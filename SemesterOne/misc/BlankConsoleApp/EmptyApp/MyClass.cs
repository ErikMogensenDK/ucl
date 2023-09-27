using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyApp
{
    public class MyClass
    {
        private string  _name;
        public string Name 
        {
            get { return _name; }
            set
            {
                if (value == "testname")
                {
                    _name = "OOOOh you got that shitty testname, lol";
                }
            }
        }
        
        public int Age {get; }

        public MyClass(int age)
        {
            Age = age;
        }

        public void PrintNameAndAge()
        {
            Console.WriteLine($"my age was {Age} and my name is {_name}");
        }
    }
}