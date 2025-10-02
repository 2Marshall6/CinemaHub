# Этап runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем csproj для restore
COPY AuthenticationAndAuthorization/AuthenticationAndAuthorization.csproj ./AuthenticationAndAuthorization/
COPY AuthenticationAndAuthorization/BusinessLogic/BusinessLogic.csproj ./BusinessLogic/
COPY AuthenticationAndAuthorization/DataAccounts/DataAccounts.csproj ./DataAccounts/
COPY AuthenticationAndAuthorization/MigrationApp/MigrationApp.csproj ./MigrationApp/

# Восстанавливаем зависимости
RUN dotnet restore "./AuthenticationAndAuthorization/AuthenticationAndAuthorization.csproj"

# Копируем весь код
COPY . .

# Собираем проект
WORKDIR "/src/AuthenticationAndAuthorization"
RUN dotnet build "./AuthenticationAndAuthorization.csproj" -c Release -o /app/build

# Публикуем проект
RUN dotnet publish "./AuthenticationAndAuthorization.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationAndAuthorization.dll"]
