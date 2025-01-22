# Use the official .NET image for the runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["HealthIndicators/HealthIndicators.sln", "./"]
COPY ["HealthIndicators/HealthIndicators/HealthIndicators.csproj", "HealthIndicators/"]
COPY ["HealthIndicators/Business/Business.csproj", "Business/"]
COPY ["HealthIndicators/Common/Common.csproj", "Common/"]
COPY ["HealthIndicators/DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["HealthIndicators/Tests/Tests.csproj", "Tests/"]

# Restore dependencies
RUN dotnet restore "./HealthIndicators.sln"

# Copy the remaining files
COPY . .

# Build the application
RUN dotnet build "HealthIndicators.sln" -c Release

# Publish the application
RUN dotnet publish "HealthIndicators/HealthIndicators/HealthIndicators.csproj" -c Release -o /app/publish

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HealthIndicators.dll"]