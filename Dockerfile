# Use a imagem oficial do SDK do .NET para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia todos os arquivos do projeto
COPY CadastroContatos.sln .
COPY src/ src/

# Restaura as dependências
RUN dotnet restore

# Publica o projeto
WORKDIR /app/src/CadastroContatos.Producer.API
RUN dotnet publish -c Release -o out

# Usa a imagem do runtime do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app/src/CadastroContatos.Producer.API/out .

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "CadastroContatos.Producer.API.dll"]