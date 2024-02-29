FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/discount/Discount.API/Basket.API.csproj", "src/services/discount/Discount.API/"]
COPY ["src/services/discount/Discount.Application/Basket.Application.csproj", "src/services/discount/Discount.Application/"]
COPY ["src/services/discount/Discount.Core/Basket.Core.csproj", "src/services/basket/Discount.Core/"]
COPY ["src/services/discount/Discount.Infrastructure/Basket.Infrastructure.csproj", "src/services/discount/Discount.Infrastructure/"]
RUN dotnet restore "src/services/discount/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/src/services/discount/Discount.API"
WORKDIR "/src/src/services/discount/Discount.API"
RUN dotnet build "Discount.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Discount.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.API.dll"]
