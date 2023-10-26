using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeApp
{
    public class SubSubClass: SubClass
    {
        public SubSubClass(): base()
        {
        }
        public void MySubSubClassMethod()
        {
            base.BaseClassMethod();
            base.SubClassMethod();
        }
    }
}