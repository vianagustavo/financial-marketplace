

# - Financial Marketplace API

# Sumário

1. Tecnologias Utilizadas
2. Inicializando
3. Gerando e Implementando Migrations (Entity Framework)
4. Testes Automatizados

## Tecnologias Utilizadas

- [C#](https://dotnet.microsoft.com/pt-br/languages/csharp)
- [.NET](https://dotnet.microsoft.com/pt-br/)
- [Entity Framework](https://learn.microsoft.com/pt-br/ef/)
- [CockroachDB](https://www.cockroachlabs.com/)
- [Xunit](https://xunit.net/)
- [Test Containers](https://testcontainers.com/)

## Inicializando

- Clonar o repositório
- Buildando a aplicação: `dotnet build`
- Criando um arquivo `.env` a partir da cópia do arquivo `.env.example`: `cp .env.example .env`
- Subindo o banco localmente usando _Docker Compose_: `docker-compose up`
- Executando a aplicação localmente: `dotnet run --project ./FinancialMarketplace.Api`

## Gerando e Implementando Migrations (Entity Framework)

Todos os arquivos relacionados à conexão com o banco de dados e o Entity Framework se encontram no projeto FinancialMarketplace.Infrastructure

Para criar uma nova migration:

```
$ dotnet ef migrations add NomeDaMigration -p FinancialMarketplace.Infrastructure/ -s FinancialMarketplace.Api

> OBS: O nome da migration deve estar em PascalCase

```

Atualizando o banco manualmente:

```
$ dotnet ef database update -p FinancialMarketplace.Infrastructure/ -s FinancialMarketplace.Api

```

## Testes Automatizados

Para rodar os testes disponíves da aplicação:

```
$ dotnet test --project ./FinancialMarketplace.Test

```
