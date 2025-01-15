FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Backend/Microservices/Community/NetSpace.Community.Api/NetSpace.Community.Api.csproj", "src/Backend/Microservices/Community/NetSpace.Community.Api/"]
RUN dotnet restore "./src/Backend/Microservices/Community/NetSpace.Community.Api/NetSpace.Community.Api.csproj"
COPY . .
WORKDIR "/src/src/Backend/Microservices/Community/NetSpace.Community.Api"
RUN dotnet build "./NetSpace.Community.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetSpace.Community.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetSpace.Community.Api.dll"]