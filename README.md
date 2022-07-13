# Tennis game interview in ASP.NET Core

<br/>

This is a solution template for creating a Tennis game interview with ASP.NET Core using a Clean Architecture.

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/)
* [XUnit.net](https://xunit.net/), [NSubstitute](https://nsubstitute.github.io/), & [AutoFixture](https://github.com/AutoFixture/AutoFixture)

## Overview

### TennisGame.Domain

This will contain all entities, enums, interfaces, types and logic specific to the domain layer.

### TennisGame.Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. For this layer I used MediatR for handled Commands and Queries to the application.

### TennisGame.Infrastructure

This layer contains classes for accessing external resources, in this case, access to the data base context. These classes implements the contracts defined in the domain layer.

### TennisGame.Api

This layer is a rest api entrypoint using ASP.NET Core 6. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection.

<br />

### Configuring the sample to use SQL Server

1. By default, the project uses a real database. If you want an in memory database, you can add in `appsettings.json`

    ```json
   {
       "UseOnlyInMemoryDatabase": true
   }

    ```

2. Ensure your connection strings in `appsettings.json` point to a local SQL Server instance.
3. Ensure the tool EF was already installed. You can find some help [here](https://docs.microsoft.com/ef/core/miscellaneous/cli/dotnet)

    ```
    dotnet tool update --global dotnet-ef
    ```

4. Open a command prompt in the TennisGame.Api folder and execute the following commands:

    ```
    dotnet restore
    dotnet tool restore
    dotnet ef database update -p ../TennisGame.Infrastructure/Infrastructure.csproj -s TennisGame.Api.csproj
    ```
   
5. Run the application.
    ```
   dotnet run
    ```


## Running the sample using Docker

You can run the interview by running these commands from the root folder (where the .sln file is located):

```
docker-compose build
docker-compose up
```
