using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EvacuationProject.UI
{
    public class View
    {
        public string? Title{ get; private set; }
        public string? Body { get; private set; }
        //public Dictionary<int, string?> Options{ get; private set; }
        public List<KeyValuePair<int, string>> Options{ get; private set; }
        public string? Prompt { get; private set; }
        private bool validInput = false;

        public View(string? title = null, string? body = null, string? prompt = null, List<KeyValuePair<int, string?>> validInputOptions = null)
        {
            Title = title;
            Body = body;
            Prompt = prompt;
            Options = validInputOptions;
            if (Options != null)
            {
                KeyValuePair<int, string> exitOption = new(0, "Afslut");
                Options.Add(exitOption);
            }
        }

        public KeyValuePair<int, string>? GetInput()
        {
            // options == null accepts any input
            if (Options == null)
            {
                validInput=true;
                return null; 
            }

            int keyOfChosenOption;
            string input = Console.ReadLine();
            int.TryParse(input, out keyOfChosenOption);
            try
            {
                validInput = true;
                return Options[keyOfChosenOption];
            }
            catch
            {
                validInput = false;
                Console.WriteLine("Fejl - ugyldigt input. Tryk enter for at prøve igen.");
                Console.ReadLine();
                return null;
            }
        }
        public void Display()
        {
            Console.Clear();
            if (Title != null)
                Console.WriteLine(Title);
            if (Body != null)
                Console.WriteLine(Body);
            if (Options != null)
            {
                for (int i = 1; i < Options.Count; i++) 
                {
                    Console.WriteLine($"{i}. {Options[i]}");
                }
                Console.WriteLine($"{0}. {Options[0]}");
            }
            if (Prompt != null)
                Console.WriteLine(Prompt);
                Console.Write("Dit input: ");
        }

        public KeyValuePair<int, string>? Run()
        {
            validInput = false;
            KeyValuePair<int, string>? myInput = new(); 
            while (!validInput)
            {
                Display();
                myInput = GetInput();
            }
            return myInput;
        }
    }
}