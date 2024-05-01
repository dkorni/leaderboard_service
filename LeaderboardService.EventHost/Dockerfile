FROM mcr.microsoft.com/dotnet/aspnet AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LeaderboardService.EventHost/LeaderboardService.EventHost.csproj", "LeaderboardService.EventHost/"]
RUN dotnet restore "LeaderboardService.EventHost/LeaderboardService.EventHost.csproj"
COPY . .
WORKDIR "/src/LeaderboardService.EventHost"
RUN dotnet build "LeaderboardService.EventHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LeaderboardService.EventHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LeaderboardService.EventHost.dll"]
