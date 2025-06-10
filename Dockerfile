FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution file nếu có
COPY ["TeamUp.sln", "./"]

# Copy từng project
COPY ["TeamUp.API/TeamUp.API.csproj", "TeamUp.API/"]
COPY ["TeamUp.Services/TeamUp.Services.csproj", "TeamUp.Services/"]
COPY ["TeamUp.Contract.Services/TeamUp.Contract.Services.csproj", "TeamUp.Contract.Services/"]
COPY ["TeamUp.Repositories/TeamUp.Repositories.csproj", "TeamUp.Repositories/"]
COPY ["TeamUp.Contract.Repositories/TeamUp.Contract.Repositories.csproj", "TeamUp.Contract.Repositories/"]
COPY ["TeamUp.Core/TeamUp.Core.csproj", "TeamUp.Core/"]
COPY ["TeamUp.ModelViews/TeamUp.ModelViews.csproj", "TeamUp.ModelViews/"]
COPY ["TeamUp.WorkerService/TeamUp.WorkerService.csproj", "TeamUp.WorkerService/"]

# Restore solution (nếu có file sln)
RUN dotnet restore "TeamUp.sln"

# Sau khi restore thành công, copy toàn bộ source code
COPY . .

WORKDIR "/src/TeamUp.API"
RUN dotnet build "TeamUp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TeamUp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamUp.API.dll"]
