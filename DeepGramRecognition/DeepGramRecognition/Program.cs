using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace DeepGramRecognition
{
    class Program
    {
        private static string[] keyPhrases = new string[] {"Address","Account","User Name","Password","Email" };
        private static string[] numbersFromZeroToNine = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };
        private static string[] numbersFromTenToNineteen = new string[] { "eleven","twelve","thirteen","fourteen","fifteen","sixteen","seventeen","eighteen", "nineteen" };
        private static string[] numbersFromTwentyToNinetySpecial =new string[] {"twenty", "thirty","forty","fifty","sixty","seventy","eighty", "ninety" };
        private static List<string> numbersFromTwentyToNinety = new List<string>();
        static void Main(string[] args)
        {
            ComputeNumbersFromTweentyToNinety();
            SearchForKeyPhrases();
            SearchForNumbers();
            
            Console.ReadLine();

        }

        private static void ComputeNumbersFromTweentyToNinety()
        {
            foreach (var tens in numbersFromTwentyToNinetySpecial) {

                foreach (var units in numbersFromZeroToNine) {

                    if (units != "zero") { numbersFromTwentyToNinety.Add(tens+units); }
                }
            }
        }

        private static void SearchForKeyPhrases() {

            foreach (var keyPhrase in keyPhrases) {

                Query(keyPhrase);

            }


        }

        private static void SearchForNumbers() {

            foreach (var number in numbersFromZeroToNine) {

                Query(number);
            }

            foreach (var number in numbersFromTenToNineteen)
            {

                Query(number);
            }

            foreach (var number in numbersFromTwentyToNinetySpecial)
            {

                Query(number);
            }

            foreach (var number in numbersFromTwentyToNinety)
            {

                Query(number);
            }
        }

        private static async void Query(string phrase)
        {
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var query = new Query{ Action = "object_search",
                                   UserID = "1490353809-d12ce9d4-3f11-4761-b73a-ea1f8d750e18-337908021386290231342747324852",
                                   ContentID = "1490350738-832c508c-53e9-49e0-abf4-97dba1fba4ad-9808201848",
                                   QueryText =phrase,
                                   Snippet =true,
                                   Filter = new Filter{ NMax=10,Pmin=0.9},
                                   Sort ="P"
                                  };
            // Serialize our concrete class into a JSON String
            var stringQuery = await Task.Run(() => JsonConvert.SerializeObject(query));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringQuery, Encoding.UTF8, "application/json");
            
            // Do the actual request and await the response
            var httpResponse = await httpClient.PostAsync("http://api.deepgram.com", httpContent);

            // If the response contains content we want to read it!
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                dynamic jObj = (JObject)JsonConvert.DeserializeObject(responseContent);


                if (jObj["startTime"].Count>0)
                {
                    Console.WriteLine(TimeSpan.FromSeconds(Math.Truncate(Convert.ToDouble(jObj["startTime"][0]))) + " " + phrase);

                }

                // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
            }


        }
    }
}
