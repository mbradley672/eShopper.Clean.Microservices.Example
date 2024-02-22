FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/catalog/Catalog.API/Catalog.API.csproj", "src/services/catalog/Catalog.API/"]
COPY ["src/services/catalog/Catalog.Application/Catalog.Application.csproj", "src/services/catalog/Catalog.Application/"]
COPY ["src/services/catalog/Catalog.Core/Catalog.Core.csproj", "src/services/catalog/Catalog.Core/"]
COPY ["src/services/catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "src/services/catalog/Catalog.Infrastructure/"]
RUN dotnet restore "src/services/catalog/Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/src/services/catalog/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
