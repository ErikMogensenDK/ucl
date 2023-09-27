using System.Reflection.Metadata.Ecma335;

namespace DependencyInjection;

partial class Program
{
    class SomeTestClass : ISomeTestClass
    {
        private int _userId;

        public int UserId { get => _userId; set => _userId = value; }

        private readonly IDataService _dataService;
        public SomeTestClass (IDataService dataService)
        {
            _dataService = dataService;
        }

        public void TestMethod()
        {
            Console.WriteLine("SOMETESTCLASS was reached!!! SUCCESS");
            _dataService.PrintMyInts();
        }
    }
}
