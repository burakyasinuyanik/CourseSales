# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CourseSales.File.Api/CourseSales.File.Api.csproj", "CourseSales.File.Api/"]
COPY ["CourseSales.Bus/CourseSales.Bus.csproj", "CourseSales.Bus/"]
COPY ["CourseSales.Shared/CourseSales.Shared.csproj", "CourseSales.Shared/"]
RUN dotnet restore "./CourseSales.File.Api/CourseSales.File.Api.csproj"
COPY . .
WORKDIR "/src/CourseSales.File.Api"
RUN dotnet build "./CourseSales.File.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CourseSales.File.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CourseSales.File.Api.dll"]