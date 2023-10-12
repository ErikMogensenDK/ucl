namespace EvacuationProject.UI
{
    public class View
    {
        public string? Title { get; private set; }
        public string? Body { get; private set; }
        //public Dictionary<int, string?> Options { get; private set; }
        //public Dictionary<int, Dictionary<int, string>> Options{ get; private set; }
        public Dictionary<int, string> Options { get; private set; }
        public string? Prompt { get; private set; }
        private bool validInput = false;
        public List<string>? Prompts {get; private set;}

        public View(
            string? title = null, 
            string? body = null, 
            string? prompt = null, 
            Dictionary<int, string> validInputOptions = null,
            List<string> prompts = null)
        {
            Title = title;
            Body = body;
            Options = validInputOptions;
            if (prompt == null)
                prompt = "";
            Prompt = prompt;
            Options = validInputOptions;
            if (Options != null)
                Options.Add(Options.Count +1, "Afslut");
            if (prompts == null)
                Prompts = new() {Prompt};
            else 
                Prompts = prompts;

        }

        public string? GetInput()
        {
            string input;
            // options == null accepts any input
            if (Options == null)
            {
                input = Console.ReadLine();
                validInput = true;
                return input;
            }
            input = Console.ReadLine();
            if (int.TryParse(input, out int index))
            {
                try
                {
                    validInput = true;
                    return Options[index];
                }
                catch
                {
                    validInput = false;
                    Console.WriteLine("Fejl - ugyldigt input. Tryk enter for at prøve igen.");
                    Console.ReadLine();
                    return "";
                }
            }
                    Console.WriteLine("Fejl - ugyldigt input. Tryk enter for at prøve igen.");
                    Console.ReadLine();
            return null;
        }
        public void Display(string prompt)
        {
            Console.Clear();
            if (Title != null)
                Console.WriteLine(Title);
            if (Body != null)
                Console.WriteLine(Body);
            if (Options != null)
            {
                for (int i = 1; i < Options.Count +1; i++)
                {
                    if (i<10)
                        Console.WriteLine($"{i}.  {Options[i]}");
                    else
                        Console.WriteLine($"{i}. {Options[i]}");
                }
            }
            if (prompt != null)
                Console.WriteLine(prompt);
            Console.Write("Dit input: ");
        }

        public string Run()
        {
            string myInput = "";

            for (int i = 0; i < Prompts.Count; i++)
            {
                validInput = false;
                while (!validInput)
                {
                    Display(Prompts[i]);
                    myInput += GetInput();
                    if (i > 0)
                        myInput += ",";
                }
            }
            return myInput;
        }
    }
}