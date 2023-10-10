using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class Person
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == "")
                    throw new Exception("Invalid input for the name variable - name cannot be an empty string");
                else
                    _name = value;
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (value.Year < 1900) 
                    throw new Exception("Birthdate year must be later than 1900");
                else
                    _birthDate = value;
            }
        }

        private double _height;

        public double Height
        {
            get => _height;
            set
            {
                if (value <= 0)
                    throw new Exception("Height must be larger than 0");
                else
                    _height = value;
            }
        }

        private bool _isMarried;

        public bool IsMarried { get => _isMarried; set => _isMarried = value; }

        private int _noOfChildren;
        public int NoOfChildren { 
            get => _noOfChildren;
            set
            {
                if (value < 0)
                    throw new Exception("Number of children must be larger than 0");
                else
                    _noOfChildren = value;
            }
        }

        public Person(string name, DateTime birthDate, double height, bool isMarried, int noOfChildren)
        {
            Name = name;            
            BirthDate = birthDate;
            Height = height;
            IsMarried = isMarried;
            NoOfChildren = noOfChildren;
        }
        public Person(string name, DateTime birthDate, double height, bool isMarried) : this(name, birthDate, height, isMarried, 0)
        {
        }
        public Person()
        {
            _name = "name";            
            _birthDate = DateTime.Now;
            _height = 0;
            _isMarried = false;
            _noOfChildren = 0;
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