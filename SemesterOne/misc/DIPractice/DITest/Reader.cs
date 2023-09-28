using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DITest
{
    public class Reader : IReader
    {
        public char Read()
        {
            char myChar = Console.ReadLine().ToCharArray()[0];

            return myChar;
        }
    }
}