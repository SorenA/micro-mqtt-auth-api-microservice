FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/MicroMQTT.Microservice.AuthAPI/MicroMQTT.Microservice.AuthAPI.csproj", "src/MicroMQTT.Microservice.AuthAPI/"]
RUN dotnet restore "src/MicroMQTT.Microservice.AuthAPI/MicroMQTT.Microservice.AuthAPI.csproj"
COPY . .
WORKDIR "/src/src/MicroMQTT.Microservice.AuthAPI"
RUN dotnet build "MicroMQTT.Microservice.AuthAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroMQTT.Microservice.AuthAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroMQTT.Microservice.AuthAPI.dll"]