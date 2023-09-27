using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testprojekt
{
    public class Database
    {
        public List<Person> Employees;
        public Database()
        {
            Employees = new List<Person>();
        }
        public void AddEmployee(Person myPerson)
        {
            Employees.Add(myPerson);
        }

        public Employee GetEmployee(string name)
        {
            for (int i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].Name == name)
                {
                    Console.WriteLine("Erik was in the database");
                }
                else
                {
                    Console.WriteLine("Erik was not found in the database");
                }
            }
        }
    }
}