
namespace MenuApp;

class Program
{
    static void Main(string[] args)
    {
        MenuService myMenuService = new();

        string SelectedOption = myMenuService.ExecuteMainMenuFlow();

    }
}
