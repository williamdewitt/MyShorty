# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/MyShorty.Api/MyShorty.Api.csproj", "MyShorty.Api/"]
RUN dotnet restore "MyShorty.Api/MyShorty.Api.csproj"
COPY ["src/MyShorty.Api/", "MyShorty.Api/"]
WORKDIR /src/MyShorty.Api
RUN dotnet build "MyShorty.Api.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "MyShorty.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyShorty.Api.dll"]