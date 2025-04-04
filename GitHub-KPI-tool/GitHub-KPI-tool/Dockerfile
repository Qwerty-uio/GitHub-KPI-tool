FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GitHub-KPI-tool/GitHub-KPI-tool.csproj", "GitHub-KPI-tool/"]
RUN dotnet restore "GitHub-KPI-tool/GitHub-KPI-tool.csproj"
COPY . .
WORKDIR "/src/GitHub-KPI-tool"
RUN dotnet build "GitHub-KPI-tool.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GitHub-KPI-tool.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GitHub-KPI-tool.dll"]
