FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Api/", "Api/"]
COPY ["Console/", "Console/"]
RUN dotnet restore "./Api/Api.csproj"
RUN dotnet build --no-restore "./Api/Api.csproj" -c $BUILD_CONFIGURATION
RUN dotnet build --no-restore "./Console/Console.csproj" -c $BUILD_CONFIGURATION

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish --no-build "./Api/Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish
RUN dotnet publish --no-build "./Console/Console.csproj" -c $BUILD_CONFIGURATION -o /app/console_publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /app/console_publish .
ENTRYPOINT ["dotnet", "Api.dll"]