using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HelloWorldLibrary.Models
{
    public class CustomText
    {
        public string Language { get; set; }
        public Dictionary<string, string> Translations { get; set; }
    }
}