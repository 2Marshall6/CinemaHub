FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем файлы решения и проектов
COPY AuthenticationAndAuthorization/AuthenticationAndAuthorization.sln ./
COPY AuthenticationAndAuthorization/AuthenticationAndAuthorization/AuthenticationAndAuthorization.csproj ./AuthenticationAndAuthorization/
COPY AuthenticationAndAuthorization/BusinessLogic/BusinessLogic.csproj ./BusinessLogic/
COPY AuthenticationAndAuthorization/DataAccounts/DataAccounts.csproj ./DataAccounts/

# Восстанавливаем зависимости
RUN dotnet restore "AuthenticationAndAuthorization/AuthenticationAndAuthorization.csproj"

# Копируем все остальные файлы
COPY AuthenticationAndAuthorization/ .

# Билдим проект
WORKDIR "/src/AuthenticationAndAuthorization"
RUN dotnet build "AuthenticationAndAuthorization.csproj" -c $BUILD_CONFIGURATION -o /app/build /p:UseAppHost=false

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuthenticationAndAuthorization.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationAndAuthorization.dll"]
