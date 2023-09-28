using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class Person
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        
        private DateTime _birthDate;
        public DateTime BirthDate{ get => _birthDate; set => _birthDate = value; }

        private double _height;

        public double Height { get => _height; set => _height = value; }

        private bool _isMarried;

        public bool IsMarried { get => _isMarried; set => _isMarried = value; }

        private int _noOfChildren;
        public int NoOfChildren { get => _noOfChildren; set => _noOfChildren = value;}

        public Person(string name, DateTime birthDate, double height, bool isMarried, int noOfChildren)
        {
            _name = name;            
            _birthDate = birthDate;
            _height = height;
            _isMarried = isMarried;
            _noOfChildren = noOfChildren;
        }

        public string MakeTitle()
        {
            string someString="";

            someString += _name;
            someString += ";" + _birthDate.ToString();
            someString += ";" + _height.ToString();
            someString += ";" + _isMarried.ToString();
            someString += ";" + _noOfChildren.ToString();

            return someString;
        }
    }
}