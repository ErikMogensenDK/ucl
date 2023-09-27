using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testprojekt
{
    public class Person
    {
        public string Initials;
        public string Name;
        public Person(string initials, string name)
        {
            Initials = initials;            
            Name = name;
        }

    }
}