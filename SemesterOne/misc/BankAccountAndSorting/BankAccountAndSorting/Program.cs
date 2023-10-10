using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace BankAccountAndSorting;

class Program
{
    static void Main(string[] args)
    {
        int myInt = 5;
        int[] arrayValue = new int[1];
        arrayValue[0] = 5;

        Console.WriteLine($"myInt (before) = {myInt}");
        ChangeValue(myInt);
        ChangeValue(myInt);
        ChangeValue(myInt);
        ChangeValue(myInt);
        ChangeValue(myInt);
        ChangeValue(myInt);
        Console.WriteLine($"myInt (after) = {myInt}");


        Console.WriteLine($"myArrayInt (before) = {arrayValue[0]}");
        ChangeValueArray(arrayValue);
        ChangeValueArray(arrayValue);
        ChangeValueArray(arrayValue);
        ChangeValueArray(arrayValue);
        ChangeValueArray(arrayValue);
        Console.WriteLine($"myArrayInt (after) = {arrayValue[0]}");

        var myPerson = new Person();
        Console.WriteLine($"My persons integer before: {myPerson.myPublicInt}");
        ChangeValuePerson(myPerson);
        Console.WriteLine($"My persons integer after: {myPerson.myPublicInt}");




        static void ChangeValue(int someInt)
        {
            someInt += 5;
        }
        static void ChangeValueArray(int[] someIntArray)
        {
            someIntArray[0] +=5 ;
        }
        static void ChangeValuePerson(Person myPerson)
        {
            myPerson.myPublicInt += 5;
        }
    }
}

