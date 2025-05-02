FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore "./FailureApi.csproj"
RUN dotnet build --no-restore "./FailureApi.csproj" -c $BUILD_CONFIGURATION  

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish --no-build "./FailureApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FailureApi.dll"]