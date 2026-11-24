# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:11.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:11.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Order/Presentation/MultiShop.Order/MultiShop.Order.csproj","Services/Order/Presentation/MultiShop.Order/"]
COPY ["Services/Order/Core/MultiShop.Order.Application/MultiShop.Order.Application.csproj","Services/Order/Core/MultiShop.Order.Application/"]
COPY ["Services/Order/Core/MultiShop.Order.Domain/MultiShop.Order.Domain.csproj","Services/Order/Core/MultiShop.Order.Domain/"]
COPY ["Services/Order/Infrastructure/MultiShop.Order.Persistence/MultiShop.Order.Persistence.csproj","Services/Order/Infrastructure/MultiShop.Order.Persistence/"]
RUN dotnet restore "./Services/Order/Presentation/MultiShop.Order/MultiShop.Order.csproj"
COPY . .
WORKDIR "/src/Services/Order/Presentation/MultiShop.Order"
RUN dotnet build "./MultiShop.Order.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MultiShop.Order.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MultiShop.Order.dll"]