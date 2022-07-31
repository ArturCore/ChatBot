using ChatBot.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

ChatBotService chatBot = new ChatBotService();

using var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>()
};
chatBot.GetBotClient().StartReceiving(
    updateHandler: chatBot.HandleUpdateAsync,
    pollingErrorHandler: chatBot.HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

app.UseSwagger();
app.MapGet("CheckBot", () => chatBot.CheckBot());
app.UseSwaggerUI();
app.Run();