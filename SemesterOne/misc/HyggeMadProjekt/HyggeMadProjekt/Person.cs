using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyggeMadProjekt
{
    public class Person
    {
        private string _name;

        private int _dinnerCount;
        private List<int> _cleaningWeeks;
        private List<int> _cookingWeeks;
        private int _timeSinceLastCook;

        public string Name { get => _name; set => _name = value; }
        public int DinnerCount { get => _dinnerCount; set => _dinnerCount = value; }
        public List<int> CleaningWeeks { get => _cleaningWeeks; set => _cleaningWeeks = value; }
        public List<int> CookingWeeks {get => _cookingWeeks; set => _cookingWeeks = value; }
        public int TimeSinceLastCook {get => _timeSinceLastCook; set => _timeSinceLastCook = value;}

        public Person(string name, int timeSinceLastCook=6)
        {
            Name = name;
            DinnerCount = 0;
            TimeSinceLastCook = timeSinceLastCook; 
            CookingWeeks = new();
            CleaningWeeks = new();
        }
    }
}