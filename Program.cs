var builder = WebApplication.CreateBuilder(args);

// Usar Startup.cs
var startup = new WebApiProyect.Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment);

app.Run();
