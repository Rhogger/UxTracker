FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 8080
EXPOSE 8081

COPY ["UxTracker.Web/UxTracker.Web.csproj", "UxTracker.Web/"]
COPY ["UxTracker.Core/UxTracker.Core.csproj", "UxTracker.Core/"]
RUN dotnet restore "UxTracker.Web/UxTracker.Web.csproj"
COPY . .
WORKDIR "/src/UxTracker.Web"
ARG BUILD_CONFIGURATION=Release
RUN dotnet build "UxTracker.Web.csproj" -c $BUILD_CONFIGURATION -o "/app/build"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UxTracker.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#FROM nginx:alpine AS final
#WORKDIR /usr/share/nginx/html
#COPY --from=publish /app/publish/wwwroot .
#COPY nginx.conf /etc/nginx/nginx.conf
#
#RUN mkdir -p /app/wwwroot/images
#
#RUN chmod -R 755 /app/wwwroot/images

EXPOSE 80
#ENTRYPOINT ["nginx", "-g", "daemon off;"]