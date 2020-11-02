using System;
using System.Threading;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonResponse;
using System.Collections.Generic;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            int offset = 0;
            string city = null;
            while (true)
            {
                var clientTelegram = new RestClient("https://api.telegram.org/bot1190000239:AAHgqTwRrLoHIAgbawXWtH-ZElKh4llIaLQ");
                // подсказка api.openweathermap.org/data/2.5/weather?q={city name}&appid={API key}
                var clientWeather = new RestClient("https://api.openweathermap.org/data/2.5");
                
                var getMe = new RestRequest("getMe", DataFormat.Json);
                var getUpdates = new RestRequest("getUpdates", DataFormat.Json);
                var getUpdatesOffset = new RestRequest($"getUpdates?offset={offset}", DataFormat.Json);


                var response = clientTelegram.Get(getMe);
                var response2 = clientTelegram.Get(getUpdates);
                var response3 = clientTelegram.Get(getUpdatesOffset);

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                TelegramB telegram = JsonConvert.DeserializeObject<TelegramB>(response3.Content);

                foreach (var r in telegram.Result)
                {
                    Console.WriteLine($"Update id: {r.Update_id}");
                    Console.WriteLine($"Chat id: {r.Message.Chat.Id}");
                    Console.WriteLine($"First name : {r.Message.From.First_name}");
                    Console.WriteLine($"Last name: {r.Message.From.Last_name}");
                    Console.WriteLine($"Text: {r.Message.Text}");
                    var id = r.Message.Chat.Id;
                    var text = r.Message.Text;
                    var req = new RestRequest($"sendMessage?chat_id={id}&text=ты написал: {text}");
                    clientTelegram.Post(req);
                    city = text;
                    //var APIKeyWeather = "a5c2b591223f038ff62ea42e92f43c74";
                    //var getWeather = new RestRequest($"weather?q={city}&appid={APIKeyWeather}", DataFormat.Json);
                    //var responseWeather = clientWeather.Get(getWeather);
                    //OpenWeather openWeather = JsonConvert.DeserializeObject<OpenWeather>(responseWeather.Content);
                    //Console.WriteLine(openWeather.MainW.Temp);
                    
                }
                try
                {
                    //Console.WriteLine(telegram.Result[^1].Update_id);
                    offset = telegram.Result[^1].Update_id + 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    offset = 0;
                }
                //Thread.Sleep(1000);
                //Console.ReadLine();
            }
        }
        public class OpenWeather
        {
            public Coord Coord { get; set; }
            public List<Weather> Weather { get; set; }
            public string Base { get; set; }
            [JsonProperty("main")]
            public MainW MainW { get; set; }
            public int Visibility { get; set; }
            public Wind Wind { get; set; }
            public Rain Rain { get; set; }
            public Clouds Clouds { get; set; }
            public int Dt { get; set; }
            public Sys Sys { get; set; }
            public int Timezone { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public int Cod { get; set; }
        }
        public class Coord
        {
            public double Lon { get; set; }
            public double Lat { get; set; }
        }
        public class Weather
        {
            public int Id { get; set; }
            public string Main { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }
        public class MainW
        {
            public double Temp { get; set; }
            public double Feels_like { get; set; }
            public double Temp_min { get; set; }
            public double Temp_max { get; set; }
            public double Pressure { get; set; }
            public double Humidity { get; set; }
        }
        public class Wind
        {
            public double Speed { get; set; }
            public int Deg { get; set; }
        }
        public class Rain
        {
            [JsonProperty("1h")]
            public double OneH { get; set; }
        }
        public class Clouds
        {
            public int All { get; set; }
        }
        public class Sys
        {
            public int Type { get; set; }
            public int Id { get; set; }
            public string Country { get; set; }
            public int Sunrise { get; set; }
            public int Sunset { get; set; }
        }
    }
}
