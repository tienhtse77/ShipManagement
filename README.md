# AE Backend Code Challenge Solution Readme
This solution is comprised of REST APIs that enable users to manage ships and find the closest port to a given ship with estimated arrival time based on the ship's geolocation and velocity.

## Technologies Used
* C#
* .NET 6
* Entity Framework Core with Fluent API
* MediatR for CQRS pattern
* FluentValidation for request validation
* Swagger for API documentation
## Setup
To set up and run the project, please follow these steps:

1. Clone the repository from GitHub
2. Open the solution file (AE.Backend.CodeChallenge.sln) in Visual Studio or your preferred IDE.
3. Build the solution to restore the NuGet packages.
4. Update the database connection string in the appsettings.json file to point to a local or remote MySQL Server instance.
5. Run the following commands in the Package Manager Console to apply the database migrations and seed the initial port data: Update-Database
6. Run the application using IIS Express or your preferred web server.
7. Access the API documentation using the Swagger UI, which can be found at /swagger/index.html.
## API Endpoints
The solution includes the following API endpoints:

### Ships
- `GET /api/ships`: Retrieves a list of all ships.
- `GET /api/ships/{id}`: Retrieves a specific ship by ID.
- `POST /api/ships`: Creates a new ship.
- `PUT /api/ships/{id}`: Updates an existing ship by ID.
### Ports
- `GET /api/ports/closest`: Retrieves the closest port to a given ship with estimated arrival time based on the ship's geolocation and velocity.
## Assumptions
The velocity is calculated in kilometers per hour (km/h) and the distance between ship and port is also in kilometers (km).
Port data is initially seeded using a pre-defined list of ports.
No REST API is required for creating ports. Port data can only be seeded via database migrations.
The API does not handle authentication and authorization. These can be added as per requirements.
## Next Steps
- Add Docker support for easier deployment
- Add unit tests and integration tests to improve code quality and reliability
- Create a CI/CD workflow to automatically build, test, and deploy the application.
