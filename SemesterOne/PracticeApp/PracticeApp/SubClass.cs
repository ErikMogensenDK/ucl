using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeApp;
public class SubClass : BaseClass
{
    public SubClass()
    {
        Console.WriteLine($"Subclass: I was created!");
    }
    public void SubClassMethod()
    {
        Console.WriteLine($"MySubClassMethod was called");
    }
}