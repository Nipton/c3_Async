using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client
{
    public class Message
    {
        public string Name { get; set; } = "User";
        public string? Text { get; set; }
        public DateTime Date { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public static Message? FromJson(string json)
        {
            return JsonSerializer.Deserialize<Message>(json);
        }
        public override string ToString()
        {
            return $"[{Date.ToShortTimeString()}] {Name}: {Text}";
        }
    }
}
