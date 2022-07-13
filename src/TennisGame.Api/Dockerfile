FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .
#COPY ["src/TennisGame.Api/TennisGame.Api.csproj", "./TennisGame.Api/"]
#RUN dotnet restore "./TennisGame.Api/TennisGame.Api.csproj"
#COPY . .
WORKDIR "/app/src/TennisGame.Api"
RUN dotnet restore

RUN dotnet build "./TennisGame.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./TennisGame.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TennisGame.Api.dll"]
