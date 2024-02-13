FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UM.TasksAPI/UM.TasksAPI.csproj", "UM.TasksAPI/"]
RUN dotnet restore "UM.TasksAPI/UM.TasksAPI.csproj"
COPY . .
WORKDIR "/src/UM.TasksAPI"
RUN dotnet build "UM.TasksAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UM.TasksAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UM.TasksAPI.dll"]
