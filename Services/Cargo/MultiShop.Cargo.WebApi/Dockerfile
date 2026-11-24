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
COPY ["Services/Cargo/MultiShop.Cargo.WebApi/MultiShop.Cargo.WebApi.csproj","Services/Cargo/MultiShop.Cargo.WebApi/"]
COPY ["Services/Cargo/MultiShop.Cargo.Repositories/MultiShop.Cargo.Repositories.csproj","Services/Cargo/MultiShop.Cargo.Repositories/"]
COPY ["Services/Cargo/MultiShop.Cargo.Domain/MultiShop.Cargo.Domain.csproj","Services/Cargo/MultiShop.Cargo.Domain/"]
COPY ["Services/Cargo/MultiShop.Cargo.Services/MultiShop.Cargo.Services.csproj","Services/Cargo/MultiShop.Cargo.Services/"]
RUN dotnet restore "./Services/Cargo/MultiShop.Cargo.WebApi/MultiShop.Cargo.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/Cargo/MultiShop.Cargo.WebApi"
RUN dotnet build "./MultiShop.Cargo.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MultiShop.Cargo.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MultiShop.Cargo.WebApi.dll"]