using System;

namespace JsonResponse
{
    public class TelegramB
    {
        public TelegramB()
        {
            var result = new Result();
        }
        public bool ok { get; set; }
        public Result[] result { get; set; }
    }
    public class Result
    {
        public Result()
        {
            Message message = new Message();
        }
        public int update_id { get; set; }
        public Message message { get; set; }
    }
    public class Message
    {
        public Message()
        {
            From from = new From();
            Chat chat = new Chat();
        }
        public int message_id { get; set; }
        public From from { get; set; }
        public Chat chat { get; set; }
        public long date { get; set; }
        public string text { get; set; }

    }
    public class From
    {
        public long id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string language_code { get; set; }

    }
    public class Chat
    {
        public long id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string type { get; set; }

    }
}
