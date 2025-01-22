# Utiliser l'image officielle .NET pour le runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Utiliser l'image SDK pour construire l'application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier les fichiers de solution et les projets
COPY ["HealthIndicators.sln", "./"]
COPY ["HealthIndicators/HealthIndicators.csproj", "HealthIndicators/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]

# Tests
# COPY ["Tests/Tests.csproj", "Tests/"]

# Restaurer les dépendances
RUN dotnet restore "./HealthIndicators.sln"

# Copier le reste des fichiers
COPY . .

# Construire l'application
RUN dotnet build "HealthIndicators.sln" -c Release -o /app/build

# Publier l'application
FROM build AS publish
RUN dotnet publish "HealthIndicators/HealthIndicators.csproj" -c Release -o /app/publish

# Créer l'image finale
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthIndicators.dll"]
