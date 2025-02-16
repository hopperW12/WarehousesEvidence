FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WarehousesEvidence.Web/WarehousesEvidence.Web.csproj", "WarehousesEvidence.Web/"]
RUN dotnet restore "WarehousesEvidence.Web/WarehousesEvidence.Web.csproj"
COPY . .
WORKDIR "/src/WarehousesEvidence.Web"
RUN dotnet build "WarehousesEvidence.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WarehousesEvidence.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WarehousesEvidence.Web.dll"]
