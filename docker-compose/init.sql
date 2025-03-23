-- docker-compose/init.sql

-- Cria o banco de dados se não existir
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CadastroContatosDb')
BEGIN
    CREATE DATABASE CadastroContatosDb;
END
GO

-- Usa o banco de dados
USE CadastroContatosDb;
GO

-- Cria a tabela Contatos
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Contatos')
BEGIN
CREATE TABLE Contatos (
                          Id INT PRIMARY KEY IDENTITY(1,1),
                          Nome NVARCHAR(100) NOT NULL,
                          Email NVARCHAR(100) NOT NULL,
                          Telefone NVARCHAR(20) NOT NULL,
                          DataNascimento DATE NOT NULL
);
END
GO