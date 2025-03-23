# Use the official .NET image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY CadastroContatos.sln .
COPY CadastroContatos.Producer.API/CadastroContatos.Producer.API.csproj ./CadastroContatos.Producer.API/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/CadastroContatos.Producer.API
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/CadastroContatos.Producer.API/out .
ENTRYPOINT ["dotnet", "CadastroContatos.Producer.API.dll"]