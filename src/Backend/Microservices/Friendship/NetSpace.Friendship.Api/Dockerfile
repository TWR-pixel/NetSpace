FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Backend/Microservices/Friendship/NetSpace.Friendship.Api/NetSpace.Friendship.Api.csproj", "src/Backend/Microservices/Friendship/NetSpace.Friendship.Api/"]
RUN dotnet restore "./src/Backend/Microservices/Friendship/NetSpace.Friendship.Api/NetSpace.Friendship.Api.csproj"
COPY . .
WORKDIR "/src/src/Backend/Microservices/Friendship/NetSpace.Friendship.Api"
RUN dotnet build "./NetSpace.Friendship.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetSpace.Friendship.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetSpace.Friendship.Api.dll"]