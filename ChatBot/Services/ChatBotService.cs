using Telegram.Bot;

namespace ChatBot.Services
{
    public class ChatBotService
    {
        readonly TelegramBotClient client;
        public ChatBotService()
        {
            var token = Environment.GetEnvironmentVariable("TelegramToken");
            client = new TelegramBotClient(token);
        }
        public async Task<string> CheckBot()
        {
            var me = await client.GetMeAsync();

            return $"Hello, World! I am user {me.Id} and my name is {me.FirstName}.";
        }
    }
}
