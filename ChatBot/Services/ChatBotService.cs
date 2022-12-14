using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace ChatBot.Services
{
    public class ChatBotService
    {
        TelegramBotClient client { get; }
        public ChatBotService()
        {
            var token = Environment.GetEnvironmentVariable("TelegramToken");
            client = new TelegramBotClient(token);
        }

        public TelegramBotClient GetBotClient()
        {
            return client;
        }

        public async Task<string> CheckBot()
        {
            var me = await client.GetMeAsync();

            return $"Hello, World! I am user {me.Id} and my name is {me.FirstName}.";
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You said: \"" + messageText + "\"\nBot is in development. Thanks for testing!",
                cancellationToken: cancellationToken);
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
