#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Microservice.Data.API/Microservice.Data.API.csproj", "src/Microservice.Data.API/"]
COPY ["src/Microservice.Data.Application/Microservice.Data.Application.csproj", "src/Microservice.Data.Application/"]
COPY ["src/Microservice.Data.Domain/Microservice.Data.Domain.csproj", "src/Microservice.Data.Domain/"]
COPY ["src/Microservice.Data.Persistence/Microservice.Data.Persistence.csproj", "src/Microservice.Data.Persistence/"]
COPY ["src/Microservice.Data.Infrastructure/Microservice.Data.Infrastructure.csproj", "src/Microservice.Data.Infrastructure/"]
RUN dotnet restore "src/Microservice.Data.API/Microservice.Data.API.csproj"
COPY . .
WORKDIR "/src/src/Microservice.Data.API"
RUN dotnet build "Microservice.Data.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Data.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Data.API.dll"]