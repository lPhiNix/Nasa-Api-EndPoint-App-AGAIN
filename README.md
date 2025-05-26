# NASA Asteroids API AGAIN (.NET Version)

## Overview

This project is an ASP.NET Core Web API that interacts with NASA's Near Earth Object Web Service (NeoWs) to retrieve information about asteroids approaching Earth. It fetches data from the NASA API, processes it, and exposes endpoints to deliver structured and simplified asteroid information.

This application mirrors the functionality of the original Java-based version, but is implemented in C# using .NET technologies and best practices.

---

## Features

-  Fetch asteroid data for a configurable number of upcoming days (1–7 days).
    
-  Simplified asteroid data output including name, average diameter (in km), relative speed, close approach date, and orbiting celestial body.
    
-  Endpoint to retrieve the top 3 largest _potentially hazardous_ asteroids during the given period.
    
-  Clean architecture with separation of concerns and dependency injection.
    
-  Easily testable thanks to interface-based service design and HttpClient abstraction.
    
-  API documentation with integrated Swagger UI.
    

---

## Architecture & Design

###  Layered and modular architecture

- **NasaApiClient**: Handles external API requests using `HttpClient`. Builds dynamic URLs and deserializes JSON responses.
    
- **AsteroidService**: Applies business logic, such as filtering hazardous asteroids, calculating average diameters, parsing speed, and sorting results.
    
- **AsteroidsController**: Defines HTTP endpoints and validates input parameters.
    
- **DTOs**: Plain data transfer objects that represent structured input/output formats.
    
- **Utility Class**: `DiameterCalculator` calculates the average diameter from NASA's range values.
    

###  Design Principles

- **Dependency Injection**: Applied throughout the app to support flexibility and testability.
    
- **Separation of Concerns**: Clear distinction between data access, logic, and presentation.
    
- **Culture-invariant parsing**: Ensures proper number parsing across different locales (important for decimal separators in velocity).
    
- **Swagger Integration**: For interactive API exploration and testing.
    

---

## Available Endpoints

|Endpoint|Description|Query Parameters|
|---|---|---|
|`GET /asteroids`|Returns top 3 largest potentially hazardous asteroids|`days` (int, 1–7)|

---

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
    
- NASA API Key _(free from [https://api.nasa.gov](https://api.nasa.gov))_
    
- Internet connection (to fetch data from NASA's API)
    

---

## How to Run

1. **Clone the repository**:
    
    ```bash
	git clone https://github.com/your-username/nasa-asteroids-api-dotnet.git 
	cd nasa-asteroids-api-dotnet
	```
    
2. **Update the API key**:
    
    - Open `NasaApiClient.cs` and replace `"DEMO_KEY"` with your actual NASA API key.
        
    - (Optional) Improve this by injecting configuration through `appsettings.json` or environment variables.
        
3. **Run the application**:
    
    ```bash
	dotnet run
	```
    
4. **Access the Swagger UI**:
    
    - Navigate to: [https://localhost:5001/swagger](https://localhost:5001/swagger) (or port used in your dev environment)
        
    - Try out the `GET /asteroids?days=3` endpoint interactively.
        

---

## Notes

- You can only request data for a maximum of 7 days in one call, as per NASA API limits.
    
- The application is built with extension and maintainability in mind.
    
- If you need to fetch additional metadata or change the business logic (e.g. hazard thresholds), simply modify the `AsteroidService`.