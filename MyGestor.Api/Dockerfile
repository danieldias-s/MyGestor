# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os csproj individualmente
COPY MyGestor.Api/MyGestor.Api.csproj MyGestor.Api/
COPY MyGestor.Application/MyGestor.Application.csproj MyGestor.Application/
COPY MyGestor.Domain/MyGestor.Domain.csproj MyGestor.Domain/
COPY MyGestor.Infrastructure/MyGestor.Infrastructure.csproj MyGestor.Infrastructure/

# Restaura as dependências
RUN dotnet restore MyGestor.Api/MyGestor.Api.csproj

# Agora copiar o restante dos arquivos
COPY MyGestor.Api/. MyGestor.Api/
COPY MyGestor.Application/. MyGestor.Application/
COPY MyGestor.Domain/. MyGestor.Domain/
COPY MyGestor.Infrastructure/. MyGestor.Infrastructure/

# Build
WORKDIR /src/MyGestor.Api
RUN dotnet build MyGestor.Api.csproj -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish MyGestor.Api.csproj -c Release -o /app/publish

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyGestor.Api.dll"]
