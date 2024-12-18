ARG BUILD_CONFIGURATION=Release

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the *.csproj files.
COPY ./calculator .
COPY ./equations .

# Copy and publish the application and its libraries.
COPY . .
RUN dotnet restore ./calculator/Circuit.Calculator.csproj
WORKDIR "/src/."
RUN dotnet build ./calculator/Circuit.Calculator.csproj -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish

RUN dotnet publish ./calculator/Circuit.Calculator.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#RUN dotnet publish ./calculator/Circuit.Calculator.csproj -c $BUILD_CONFIGURATION -o /equations --no-restore

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Circuit.Calculator.dll"]