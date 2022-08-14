#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Microservice.Data.API/Microservice.Data.API.csproj", "Microservice.Data.API/"]
COPY ["Microservice.Data.Infrastructure/Microservice.Data.Infrastructure.csproj", "Microservice.Data.Infrastructure/"]
COPY ["Microservice.Data.Persistence/Microservice.Data.Persistence.csproj", "Microservice.Data.Persistence/"]
COPY ["Microservice.Data.Domain/Microservice.Data.Domain.csproj", "Microservice.Data.Domain/"]
COPY ["Microservice.Data.Application/Microservice.Data.Application.csproj", "Microservice.Data.Application/"]

RUN dotnet restore "Microservice.Data.API/Microservice.Data.API.csproj"
COPY . .
WORKDIR "/src/Microservice.Data.API"
RUN dotnet build "Microservice.Data.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Data.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Data.API.dll"]