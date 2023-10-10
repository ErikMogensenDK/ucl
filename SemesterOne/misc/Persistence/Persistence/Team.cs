using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Persistence
{
    public class Team
    {
        string _name;
        public string Name { get => _name; set => _name = value; }
        List<string>? _persons;

        public List<string> Persons { get => _persons; set => _persons = value; }

        public Team(List<string> persons, string name)
        {
            Persons = persons;
            Name = name;
        }
        public Team() :this(null, "")
        {
            Persons = new();
        }

        public void SaveTeam(string savePath)
        {
            var myWriter = new StreamWriter(savePath);
            myWriter.WriteLine($"!{_name}");
            foreach (string person in _persons)
                myWriter.WriteLine(person);
            myWriter.Close();
        }
        public void LoadTeam(string savePath)
        {
            var myReader = new StreamReader(savePath);
            string? myString = myReader.ReadLine();
            while (myString != null)
            {
                if (myString.StartsWith('!'))
                    Name = myString;
                else
                    Persons.Add(myString);
                myString = myReader.ReadLine();
            }
            myReader.Close();
        }
    }
}