#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Priemka.API/Priemka.API.csproj", "Priemka.API/"]
COPY ["Priemka.Application/Priemka.Application.csproj", "Priemka.Application/"]
COPY ["Priemka.Domain/Priemka.Domain.csproj", "Priemka.Domain/"]
COPY ["Priemka.Infrastructure/Priemka.Infrastructure.csproj", "Priemka.Infrastructure/"]
RUN dotnet restore "./Priemka.API/Priemka.API.csproj"
COPY . .
WORKDIR "/src/Priemka.API"
RUN dotnet build "./Priemka.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Priemka.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Priemka.API.dll"]