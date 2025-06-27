namespace RPG_Wiki_WebApp.Models.Chat
{
    public class ChatMessage
    {
        public string Nickname { get; set; }
        public string Message { get; set; }
        public DateTime SentTime { get; set; } = DateTime.Now;
    }
}
