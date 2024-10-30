FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UxTracker.Api/UxTracker.Api.csproj", "UxTracker.Api/"]
COPY ["UxTracker.Core/UxTracker.Core.csproj", "UxTracker.Core/"]
COPY ["UxTracker.Infra/UxTracker.Infra.csproj", "UxTracker.Infra/"]
RUN dotnet restore "UxTracker.Api/UxTracker.Api.csproj"
COPY . .
WORKDIR "/src/UxTracker.Api"
RUN dotnet build "UxTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UxTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/wwwroot/ConsentTerms

RUN chmod -R 755 /app/wwwroot/ConsentTerms
RUN chmod -R 777 /app/wwwroot/ConsentTerms

ENTRYPOINT ["dotnet", "UxTracker.Api.dll"]
