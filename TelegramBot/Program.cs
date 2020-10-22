using System;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonResponse;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var client = new RestClient("https://api.telegram.org/bot1190000239:AAHgqTwRrLoHIAgbawXWtH-ZElKh4llIaLQ");

                Console.WriteLine("1");
                var request1 = new RestRequest("getMe", DataFormat.Json);
                Console.WriteLine("2");
                var request2 = new RestRequest("getUpdates", DataFormat.Json);
                
                var response = client.Get(request1);
                var response2 = client.Get(request2);
                
                //var res = client.Post(req);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                var b = JsonConvert.DeserializeObject(response.Content);
                
                

                Console.WriteLine(b);

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                var c = JsonConvert.DeserializeObject(response2.Content);
                Console.WriteLine(c.GetType());


                TelegramB telegram = JsonConvert.DeserializeObject<TelegramB>(response2.Content);
                Console.WriteLine(telegram.result[telegram.result.Length-1].message.chat.id);
                Console.WriteLine(telegram.result[telegram.result.Length - 1].message.text);

                //Console.WriteLine(telegram.result[telegram.result.Length - 1]);
                

                //var id = telegram.result[telegram.result.Length - 1].message.chat.id;
                //var text = telegram.result[telegram.result.Length - 1].message.text;
                //var req = new RestRequest($"sendMessage?chat_id={id}&text=ты написал: {text}");

                //client.Post(req);



                //Console.WriteLine(c);
                //Console.WriteLine(response2.Request);

                Console.WriteLine("тест");
                Console.ReadLine();
            }
        }
    }
}
