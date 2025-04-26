# Etapa base para rodar o app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo
COPY ./MyGestor.Api/MyGestor.Api.csproj ./MyGestor.Api/
COPY ./MyGestor.Domain/MyGestor.Domain.csproj ./MyGestor.Domain/
COPY ./MyGestor.Application/MyGestor.Application.csproj ./MyGestor.Application/
COPY ./MyGestor.Infrastructure/MyGestor.Infrastructure.csproj ./MyGestor.Infrastructure/


WORKDIR /src/MyGestor.Api
RUN dotnet restore


COPY . .

# Build
RUN dotnet build "MyGestor.Api.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "MyGestor.Api.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyGestor.Api.dll"]
