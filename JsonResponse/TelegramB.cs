using System;
using System.Collections.Generic;

namespace JsonResponse
{
    public class TelegramB
    {
        public bool Ok { get; set; }
        public List<Result> Result { get; set; }
    }
    public class Result
    {
        public int Update_id { get; set; }
        public Message Message { get; set; }
    }
    public class Message
    {
        public int Message_id { get; set; }
        public From From { get; set; }
        public Chat Chat { get; set; }
        public long Date { get; set; }
        public string Text { get; set; }

    }
    public class From
    {
        public long Id { get; set; }
        public bool Is_bot { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
        public string Language_code { get; set; }

    }
    public class Chat
    {
        public long Id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
        public string Type { get; set; }

    }
}
