using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DITest
{
    public class WriterTwo: IWriter
    {
        public void Write()                
        {
            var myWriter = new StreamWriter("Text.txt");
            myWriter.WriteLine("Somewhitn was written somewhere else");
            myWriter.Close();
        }
    }
}