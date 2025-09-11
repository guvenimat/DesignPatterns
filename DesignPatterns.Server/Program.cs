using DesignPatterns.Server.Modules;

var builder = WebApplication.CreateBuilder(args);

// Configure all application services
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure application pipeline
app.UseCustomMiddlewares()
   .ConfigureApplicationPipeline();

app.Run();
