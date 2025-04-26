# Etapa base: imagem do ASP.NET Core para rodar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa build: imagem do SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MyGestor.Api/MyGestor.Api.csproj", "MyGestor.Api/"]
RUN dotnet restore "MyGestor.Api/MyGestor.Api.csproj"
COPY . .
WORKDIR "/src/MyGestor.Api"
RUN dotnet build "MyGestor.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyGestor.Api.csproj" -c Release -o /app/publish

# Final: copiar arquivos buildados para imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyGestor.Api.dll"]
