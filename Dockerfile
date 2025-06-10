# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TeamUp.API/TeamUp.API.csproj", "TeamUp.API/"]
COPY ["TeamUp.Services/TeamUp.Services.csproj", "TeamUp.Services/"]
COPY ["TeamUp.Contract.Services/TeamUp.Contract.Services.csproj", "TeamUp.Contract.Services/"]
COPY ["TeamUp.Repositories/TeamUp.Repositories.csproj", "TeamUp.Repositories/"]
COPY ["TeamUp.Contract.Repositories/TeamUp.Contract.Repositories.csproj", "TeamUp.Contract.Repositories/"]
COPY ["TeamUp.Core/TeamUp.Core.csproj", "TeamUp.Core/"]
COPY ["TeamUp.ModelViews/TeamUp.ModelViews.csproj", "TeamUp.ModelViews/"]
COPY ["TeamUp.WorkerService/TeamUp.WorkerService.csproj", "TeamUp.WorkerService/"]
RUN dotnet restore "./TeamUp.API/TeamUp.API.csproj"
COPY . .
WORKDIR "/src/TeamUp.API"
RUN dotnet build "./TeamUp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TeamUp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamUp.API.dll"]