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
            var myWriter = new StreamWriter(_dataFileName, true);
            myWriter.Write(person.MakeTitle());
            myWriter.Close();
        }
        public Person LoadPerson()
        {
            var myReader = new StreamReader(_dataFileName);
            string myString = myReader.ReadLine();
            myReader.Close();
            string[] myStringArray = myString.Split(";");
            string name = myStringArray[0];
            DateTime birthDate = DateTime.Parse(myStringArray[1]);
            double height = Convert.ToDouble(myStringArray[2]);
            bool isMarried = Convert.ToBoolean(myStringArray[3]);
            int noOfChildren = Convert.ToInt32(myStringArray[4]);
            var myPerson = new Person(name, birthDate, height, isMarried, noOfChildren);
            return myPerson;
        }
    }
}