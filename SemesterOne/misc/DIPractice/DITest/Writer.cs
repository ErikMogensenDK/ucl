using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DITest
{
    public class Writer : IWriter
    {
        public void Write()
        {
            Console.WriteLine("Something was written");
        }
    }
}