using System.Runtime.InteropServices;

namespace DependencyInjection;


partial class Program
{
    class DataService : IDataService
    {
        private List<int> myInts = new();
        public DataService()
        {
            var rng = new Random();
            for (int i = 0; i < 10; i++)
            {
                myInts.Add(rng.Next(100));
            }
        }
        public void PrintMyInts()
        {
            for (int i = 0; i < myInts.Count; i++)
            {
                Console.WriteLine($"Int{i} was {myInts[i]}");
            }
        }
    }
}
