using DesignPatterns.Client.Modules;

var builder = WebApplication.CreateBuilder(args);

// Configure all client services
builder.Services.AddClientServices();

var app = builder.Build();

// Configure client application pipeline
app.ConfigureClientPipeline();

app.Run();
