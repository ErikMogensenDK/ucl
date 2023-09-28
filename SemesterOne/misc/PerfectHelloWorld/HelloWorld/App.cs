using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldLibrary.BusinessLogic;

namespace HelloWorld
{
    public class App
    {
        private readonly IMessages _messages;

        public App(IMessages messages)
        {
            _messages = messages;
        }

        public void Run(string[] args)
        {
            string lang = "en";

            string message = _messages.Greeting(lang);

            Console.WriteLine(message);
        }

    }
}