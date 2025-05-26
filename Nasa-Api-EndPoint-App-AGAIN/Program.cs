using Microsoft.OpenApi.Models;
using Nasa_Api_EndPoint_App_AGAIN.Client;
using Nasa_Api_EndPoint_App_AGAIN.Services;

// Application bootstrap and configuration for the Asteroids API.
//
// This code sets up the ASP.NET Core Web API application.
// It configures services, dependency injection, HTTP client factory,
// and Swagger for API documentation.
//
// The architecture follows best practices by:
// - Using DI to inject dependencies like INasaApiClient and IAsteroidService,
//   promoting loose coupling and easier testing.
// - Adding HttpClient through the IHttpClientFactory to efficiently manage HTTP connections.
// - Registering Swagger for interactive API docs, which enhances developer experience and API usability.
//
// The modular design allows for easy extension and maintenance of the application.
var builder = WebApplication.CreateBuilder(args);

// Add controllers for handling API endpoints.
builder.Services.AddControllers();

// Register scoped services for dependency injection:
// - INasaApiClient handles communication with NASA's external API.
// - IAsteroidService encapsulates business logic related to asteroids.
builder.Services.AddScoped<INasaApiClient, NasaApiClient>();
builder.Services.AddScoped<IAsteroidService, AsteroidService>();

// Add HttpClient support for making HTTP requests (used by NasaApiClient).
builder.Services.AddHttpClient();

// Enable API explorer and configure Swagger generation for automatic API documentation.
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

// Build the application.
var app = builder.Build();

// Conditionally enable Swagger UI only in Development environment for security and performance reasons.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asteroids API v1");
    });
}

// Enforce HTTPS redirection for improved security.
app.UseHttpsRedirection();

// Enable authorization middleware (placeholder for future use if needed).
app.UseAuthorization();

// Map controller routes to handle incoming requests.
app.MapControllers();

// Run the application and start listening for requests.
app.Run();
