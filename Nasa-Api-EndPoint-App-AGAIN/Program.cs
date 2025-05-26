using Microsoft.OpenApi.Models;
using Nasa_Api_EndPoint_App_AGAIN.Client;
using Nasa_Api_EndPoint_App_AGAIN.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<INasaApiClient, NasaApiClient>();
builder.Services.AddScoped<IAsteroidService, AsteroidService>();

builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Asteroids API",
        Version = "v1",
        Description = "Return Top 3 Most Dangerous Asteroids According to NASA."
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asteroids API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();