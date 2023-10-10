using System.Runtime.InteropServices;

namespace Pengeskab;

class Program
{
    static void Main(string[] args)
    {
        State myState = State.Locked;
        StateHelper myStateHelper = new();
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Safe is: {myState}");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1: Lock the safe");
            Console.WriteLine("2: UnLock the safe");
            Console.WriteLine("3: Open the safe");
            Console.WriteLine("4: Close the safe");
            string option = Console.ReadLine();
            switch(option)
            {
                case "1":
                {
                    myState = myStateHelper.Lock(myState);
                    break;
                }
                case "2":
                {
                    myState = myStateHelper.Unlock(myState);
                    break;
                }
                case "3":
                {
                    myState = myStateHelper.Open(myState);
                    break;
                }
                case "4":
                {
                    myState = myStateHelper.Close(myState);
                    break;
                }

            }
        }
    }
}
