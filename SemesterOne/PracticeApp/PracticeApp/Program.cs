namespace PracticeApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("input some expression:");
        string inputString = Console.ReadLine();
        int lengthOfString = inputString.Length;
        int index = 0;
        int output = 0;

        while (lengthOfString-2 > index)
        {
            string mySubstring = inputString.Substring(index, 3);
            int num1 = int.Parse(mySubstring.Substring(0,1));
            if (index == 0)
            {
                output = num1;
            }
            int num2 = int.Parse(mySubstring.Substring(2,1));
            string myOperator = mySubstring.Substring(1,1);

            if (myOperator == "+")
            {
                output += num2;
            }
            else if (myOperator == "-")
            {
                output -= num2;
            }
            else if (myOperator == "*")
            {
                output *= num2;
            }
            else if (myOperator == "/")
            {
                output /= num2;
            }

            index += 2;
        }
        Console.WriteLine(output);
        Console.ReadLine();
    }
}
