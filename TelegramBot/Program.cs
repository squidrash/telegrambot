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
                Console.WriteLine("Начало");
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
                    // Эхо бот 
                    //Console.WriteLine($"Update id: {r.Update_id}");
                    //Console.WriteLine($"Chat id: {r.Message.Chat.Id}");
                    //Console.WriteLine($"First name : {r.Message.From.First_name}");
                    //Console.WriteLine($"Last name: {r.Message.From.Last_name}");
                    //Console.WriteLine($"Text: {r.Message.Text}");
                    //var id = r.Message.Chat.Id;
                    //var text = r.Message.Text;
                    //var req = new RestRequest($"sendMessage?chat_id={id}&text=ты написал: {text}");
                    //clientTelegram.Post(req);
                    //city = text;

                    var id = r.Message.Chat.Id;
                    city = r.Message.Text;
                    var lang_code = "ru";
                    var APIKeyWeather = "a5c2b591223f038ff62ea42e92f43c74";
                    var getWeather = new RestRequest($"weather?q={city}&appid={APIKeyWeather}&units=metric&lang={lang_code}", DataFormat.Json);
                    try
                    {
                        var responseWeather = clientWeather.Get(getWeather);
                        OpenWeather openWeather = JsonConvert.DeserializeObject<OpenWeather>(responseWeather.Content);
                        var main_weather = $"{openWeather.Weather[0].Description}\n";
                        var temp = $"Текущая температура: {openWeather.MainW.Temp}\n";
                        var temp_feel = $"Ощущается как: {openWeather.MainW.Feels_like}\n"; 
                        var temp_max = $"Максимальная температура: {openWeather.MainW.Temp_max}\n";
                        var temp_min = $"Минимальная температура:{openWeather.MainW.Temp_min}\n";
                        var humidity = $"Влажность: {openWeather.MainW.Humidity}%\n";
                        var wind_speed = $"Скорость ветра: {openWeather.Wind.Speed} м/с\n";
                        var basicWeatherIndicators = main_weather + temp + temp_feel + temp_max + temp_min + humidity + wind_speed;
                        var req = new RestRequest($"sendMessage?chat_id={id}&text={basicWeatherIndicators}");
                        clientTelegram.Post(req);


                        //string[] basicWeatherIndicators = new string[] { temp, temp_feel, temp_max, temp_min };
                        //string value = String.Join(" ", basicWeatherIndicators);
                        Console.WriteLine(basicWeatherIndicators);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"Неверно указан город {e.Message}");
                    }
                }
                if (telegram.Result.Length == 0)
                {
                    offset = 0;
                }
                else
                {
                    offset = telegram.Result[^1].Update_id + 1;
                }
                Console.WriteLine("Конец");
                
                //Thread.Sleep(1000);
                //Console.ReadLine();
            }
        }
        public class OpenWeather
        {
            public Coord Coord { get; set; }
            public Weather[] Weather { get; set; }
            public string Base { get; set; }
            [JsonProperty("main")]
            public MainW MainW { get; set; }
            public int Visibility { get; set; }
            public Wind Wind { get; set; }
            public Rain Rain { get; set; }
            public Snow Snow { get; set; }
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
            public double Sea_level { get; set; }
            public double Grnd_level { get; set; }
        }
        public class Wind
        {
            public double Speed { get; set; }
            public int Deg { get; set; }
            public double Gust { get; set; }
        }
        public class Rain
        {
            [JsonProperty("1h")]
            public double OneH { get; set; }
            [JsonProperty("3h")]
            public double ThreeH { get; set; }
        }
        public class Snow
        {
            [JsonProperty("1h")]
            public double OneH { get; set; }
            [JsonProperty("3h")]
            public double ThreeH { get; set; }
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
