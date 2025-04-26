# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Aqui o caminho é direto
COPY ["MyGestor.Api.csproj", "./"]
RUN dotnet restore "./MyGestor.Api.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet build "MyGestor.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyGestor.Api.csproj" -c Release -o /app/publish

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyGestor.Api.dll"]
