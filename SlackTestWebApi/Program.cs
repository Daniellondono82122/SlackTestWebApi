using SlackTestWebApi.Services.Hubs;
using SlackTestWebApi.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddScoped<ISlackService, SlackService>();
builder.Services.AddScoped<IEventsService, EventsService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<SlackHub>("/SlackHub");
app.Run();
