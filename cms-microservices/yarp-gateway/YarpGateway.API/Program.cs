using Yarp.ReverseProxy.Configuration;
using YarpGateway.API.Providers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<Yarp.ReverseProxy.Configuration.IProxyConfigProvider, YarpGateway.API.Providers.InMemoryConfigProvider>();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


builder.Services.AddReverseProxy();

var app = builder.Build();
app.UseCors("AllowAll");

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("http://localhost:44271/swagger/v1/swagger.json", "Content Service");
    options.SwaggerEndpoint("http://localhost:44272/swagger/v1/swagger.json", "User Service");
    options.RoutePrefix = "swagger";
});

app.MapReverseProxy();
app.MapGet("/", () => "YARP Gateway with dynamic config is running!");

app.Run();
