using ChatBot.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

ChatBotService chatBot = new ChatBotService();

app.UseSwagger();
app.MapGet("CheckBot", () => chatBot.CheckBot());
app.UseSwaggerUI();
app.Run();