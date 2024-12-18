FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ./calculator .
COPY ./equations .
COPY . .

# MsBuild stage
RUN dotnet restore ./calculator/Circuit.Calculator.csproj
WORKDIR "/src/."
RUN dotnet build "./calculator/Circuit.Calculator.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish ./calculator/Circuit.Calculator.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN dotnet publish "./calculator/Circuit.Calculator.csproj" -c debug -o /app/publish --no-restore

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV DOTNET_AttachStdout=1
ENV DOTNET DOTNET_CLI_TELEMETRY_OPTOUT=1
ENTRYPOINT ["dotnet", "Circuit.Calculator.dll"]
CMD ["-u"]