FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY AuthenticationAndAuthorization/AuthenticationAndAuthorization.sln ./
COPY AuthenticationAndAuthorization/AuthenticationAndAuthorization/AuthenticationAndAuthorization.csproj ./AuthenticationAndAuthorization/
COPY AuthenticationAndAuthorization/BusinessLogic/BusinessLogic.csproj ./BusinessLogic/
COPY AuthenticationAndAuthorization/DataAccounts/DataAccounts.csproj ./DataAccounts/
RUN dotnet restore "./AuthenticationAndAuthorization/AuthenticationAndAuthorization.csproj"
COPY . .
WORKDIR "/src/AuthenticationAndAuthorization"
RUN dotnet build "./AuthenticationAndAuthorization.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuthenticationAndAuthorization.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationAndAuthorization.dll"]
