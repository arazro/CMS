using ContentService.Application;
using ContentService.Infrastructure;
using ContentService.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContentService.API.Extensions;
using ContentService.Application.Interfaces;
using ContentService.Domain.Interfaces;

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
builder.Services.AddScoped<IContentService, ContentService.Application.Services.ContentService>();
builder.Services.AddScoped<IContentRepository, ContentService.Infrastructure.Repositories.ContentRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomSwagger();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();
app.UseCors("AllowAll");
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
