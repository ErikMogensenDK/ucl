using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using HelloWorldLibrary.Models;
namespace HelloWorldLibrary.BusinessLogic
{
    public class Messages : IMessages
    {
        public string Greeting(string language)
        {
            string output = LookUpCustomText(nameof(Greeting), language);
            return output;
        }

        private string LookUpCustomText(string key, string language)
        {
            System.Text.Json.JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                List<CustomText>? messageSets = JsonSerializer
                    .Deserialize<List<CustomText>>
                    (
                        File.ReadAllText("CustomText.json"), options
                    );

                CustomText? messages = messageSets?.Where(x => x.Language == language).First();
                if (messages == null)
                    throw new NullReferenceException("Language was not found in json file");
                return messages.Translations[key];

            }
            catch (Exception ex)
            {
                // should log this!
                throw;
            }

        }

    }
}