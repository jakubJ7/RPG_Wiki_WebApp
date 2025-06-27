using Microsoft.AspNetCore.SignalR;

namespace RPG_Wiki_WebApp.Models.Chat
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string nickname, string message)
        {
            var chatMessage = new ChatMessage
            {
                Nickname = nickname,
                Message = message
            };

            await Clients.All.SendAsync("ReceiveMessage", chatMessage.Nickname, chatMessage.SentTime.ToString("HH:mm"), chatMessage.Message);
        }
    }
}
