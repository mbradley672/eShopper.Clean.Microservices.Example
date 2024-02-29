FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/basket/Basket.API/Basket.API.csproj", "src/services/basket/Basket.API/"]
COPY ["src/services/basket/Basket.Application/Basket.Application.csproj", "src/services/basket/Basket.Application/"]
COPY ["src/services/basket/Basket.Core/Basket.Core.csproj", "src/services/basket/Basket.Core/"]
COPY ["src/services/basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "src/services/basket/Basket.Infrastructure/"]
RUN dotnet restore "src/services/basket/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/src/services/basket/Basket.API"
RUN dotnet build "Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]
