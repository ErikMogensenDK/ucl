using System.Security.Cryptography.X509Certificates;

namespace HyggeMadProjekt;

class Program
{
    static void Main(string[] args)
    {
        List<Person> people = new() { new("Ebbe", 0), new("Sara", 2), new("Christian" ,4), new("Johanne", 1), new("Julie", 5), new("Erik", 3) };
        // tildel person til uge, hvis person ikke var på i forrige uge, og hvis de ikke skal gøre rent den uge!
        SetCleaningWeeks(people[0], 0);
        SetCleaningWeeks(people[1], 0);
        SetCleaningWeeks(people[2], 2);
        SetCleaningWeeks(people[3], 2);
        SetCleaningWeeks(people[4], 1);
        SetCleaningWeeks(people[5], 1);

        List<string> cookingList = SetCookingWeeks(people);
        foreach (string line in cookingList)
            Console.WriteLine(line);
        foreach (Person person in people)
        {
            Console.WriteLine(person.Name);
            for (int i = 0; i < person.CleaningWeeks.Count && i < person.CookingWeeks.Count; i++)
                Console.WriteLine($"cleaning:{person.CleaningWeeks[i]}, Cooking:{person.CookingWeeks[i]}");
        }

        

        static void SetCleaningWeeks(Person person, int displacement)
        {
            List<int> myList = new();
            for (int i = 1; i < 53; i++)
            {
                if ((i+displacement) % 3 == 0)
                    myList.Add(i);
            }
            person.CleaningWeeks = myList;
        }
        // cooking week rules:
        // person with highest timeSinceLastCook, gets current week as cooking week unless it is their cleaningweek
        static List<string> SetCookingWeeks(List<Person> people)
        {
            List<string> cookingList = new();
            Person cook = new("");
            for (int i = 1; i < 53; i++)
            {
                int longestTimeSinceLastCook = 0;
                foreach (var person in people)
                {
                    if (person.TimeSinceLastCook > longestTimeSinceLastCook)
                    {
                        longestTimeSinceLastCook = person.TimeSinceLastCook;
                    }
                }
                Console.WriteLine($"Successfully set longest time to {longestTimeSinceLastCook}");
                Console.WriteLine($"I was {i}");
                bool cookingUnassigned = true;
                while (cookingUnassigned)
                {
                    foreach (Person person in people)
                    {
                        Console.WriteLine($"Person = {person.Name}, i = {i}, {!person.CleaningWeeks.Contains(i)}");
                        if (person.TimeSinceLastCook == longestTimeSinceLastCook && !person.CleaningWeeks.Contains(i))
                        {
                            person.CookingWeeks.Add(i);
                            person.TimeSinceLastCook = -1;
                            cookingList.Add($"Uge {i}: {person.Name}");
                            cookingUnassigned = false;
                            break;
                        }
                    }
                    foreach (Person person in people)
                    {
                        person.TimeSinceLastCook++;
                    }
                }
            }
            return cookingList;
        }
    }
}
