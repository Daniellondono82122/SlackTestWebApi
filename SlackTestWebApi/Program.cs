using SlackTestWebApi.Services.Hubs;
using SlackTestWebApi.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option => {
    option.AddPolicy("cors", policy => {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
    });
});
builder.Services.AddSignalR(opt => { opt.EnableDetailedErrors = true; });
builder.Services.AddScoped<ISlackService, SlackService>();
builder.Services.AddScoped<IEventService, EventService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("cors");
app.UseAuthorization();
app.MapControllers();
app.MapHub<SlackHub>("/slackhub");
app.Run();


