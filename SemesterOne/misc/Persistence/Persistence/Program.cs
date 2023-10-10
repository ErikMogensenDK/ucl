using System.Reflection;

namespace Persistence;

class Program
{
    static void Main(string[] args)
    {
        var myPerson = new Person("navn", DateTime.Now, 150, false, 5);
        var myPerson2 = new Person("NytNavn", DateTime.Now, 150, false, 5);
        var myPerson3 = new Person("NytNavnMere", DateTime.Now, 150, false, 5);
        List<Person> myPersonList = new() {myPerson, myPerson2, myPerson3};
        string title = myPerson.MakeTitle();
        Console.WriteLine(title);
        List<string> personNameList = new(){myPerson.Name, myPerson2.Name, myPerson3.Name};
        var myTeam = new Team(personNameList, "ThebestTeamINTheWorld");
        myTeam.SaveTeam("SometextFile.txt");
        Team newTeam = new();
        newTeam.LoadTeam("SometextFile.txt");
        Console.WriteLine(newTeam.Name);
        Console.WriteLine(newTeam.Persons[0]);
        Console.WriteLine(newTeam.Persons[1]);
        Console.WriteLine(newTeam.Persons[2]);

    }
}
