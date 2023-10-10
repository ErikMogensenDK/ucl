using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataHandler
    {
        private string _dataFileName;

        public string DataFileName { get => _dataFileName; }
        public DataHandler(string dataFileName)
        {
            _dataFileName = dataFileName;
        }
        public void SavePerson(Person person)
        {
            var myWriter = new StreamWriter(_dataFileName);
            myWriter.WriteLine(person.MakeTitle());
            myWriter.Close();
        }
        public void SavePerson(Person person, StreamWriter myWriter)
        {
            myWriter.WriteLine(person.MakeTitle());
        }
        public Person LoadPerson()
        {
            var myReader = new StreamReader(_dataFileName);
            string myString = myReader.ReadLine();
            myReader.Close();
            Person myPerson = GeneratePersonFromString(myString);
            return myPerson;
        }
        public Person LoadPerson(StreamReader myReader) 
        {
            string? myString = myReader.ReadLine();
            Person? myPerson = null;
            if (myString != null)
                myPerson = GeneratePersonFromString(myString);
            return myPerson;
        }

        public void SavePersons(Person[] persons)
        {
            var myWriter = new StreamWriter(_dataFileName);
            myWriter.Close();
            myWriter = new StreamWriter(_dataFileName, true);
            foreach (Person person in persons)
                //SavePerson(person, myWriter);
                myWriter.WriteLine(person.MakeTitle());
            myWriter.Close();
        }
        public Person[] LoadPersons()
        {
            int lines = TotalLines(_dataFileName);
            var myReader = new StreamReader(_dataFileName);

            Person myPerson = new();
            List<Person> myPersonsList = new();
            for (int i = 0; i < lines; i++)
            {
                myPerson = LoadPerson(myReader);
                myPersonsList.Add(myPerson);
            }
            Person[] myPersonsArray = myPersonsList.ToArray();
            myReader.Close();
            return myPersonsArray;
        }
        public Person GeneratePersonFromString(string myString)
        {
            string[] myStringArray = myString.Split(";");
            string name = myStringArray[0];
            DateTime birthDate = DateTime.Parse(myStringArray[1]);
            double height = Convert.ToDouble(myStringArray[2]);
            bool isMarried = Convert.ToBoolean(myStringArray[3]);
            int noOfChildren = Convert.ToInt32(myStringArray[4]);
            var myPerson = new Person(name, birthDate, height, isMarried, noOfChildren);
            return myPerson;
        }
        int TotalLines(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                int i = 0;
                while (r.ReadLine() != null) { i++; }
                return i;
            }
        }
    }
}