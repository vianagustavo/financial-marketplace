

# - Financial Marketplace API

# Sumário

1. Tecnologias Utilizadas
2. Inicializando
3. Gerando e Implementando Migrations (Entity Framework)
4. Documentação da API
5. Autor

## Tecnologias Utilizadas

- [C#](https://dotnet.microsoft.com/pt-br/languages/csharp)
- [.NET](https://dotnet.microsoft.com/pt-br/)
- [Entity Framework](https://learn.microsoft.com/pt-br/ef/)
- [CockroachDB](https://www.cockroachlabs.com/)
- [Swagger](https://swagger.io/)

## Inicializando

- Clonar o repositório
- Buildando a aplicação: `dotnet restore && dotnet build`
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

**Para medida de testes da aplicação, foram criados Roles padronizadas e um Usuário Admin através de migrations**

## Documentação da API

Todos os endpoints desenvolvidos na API foram documentados utilizando Swagger e podem ser acessados:

- Localmente: `http://localhost:5153/swagger/index.html`

|  Verbo   |                    Endpoint                     |                 Descrição                  |     Acessível à:      |
| :------- | :---------------------------------------------: | :----------------------------------------: | :-------------------: |
| `POST`   |                  `/users`              |              Criação de novo usuário                |          -            |
| `GET`    |                  `/users`              |              Listagem de usuários                   |       Usuários        |
| `GET`    |                 `/users/{id}`          |                 Busca de usuário                    |       Usuários        |
| `DELETE` |                  `/users`              |                Deleta um usuário                    |       Usuários        |
| `PATCH`  |              `/users/{id}/role`        |              Altera role de usuário                 |    Operacional/Admin  |
| `PATCH`  |              `/users/password`         |               Criação de senha de usuário           |          -            |
| `POST`   |            `/users/reset-password`     |                Reseta senha de usuário              |          -            |
| `POST`   |                `/auth/login`           |                  Login de usuários                  |          -            |
| `POST`   |                `/auth/refresh`         |                  Reseta token de login              |          -            |
| `PATCH`  |             `/accounts/funds`          |               Adiciona saldo à conta                |   Operacional/Cliente |
| `PATCH`  |   `/accounts/product/{productId}/buy`  |            Realiza compra de um produto             |        Cliente        |
| `PATCH`  |   `/accounts/product/{productId}/sell` |            Realiza venda de um produto              |        Cliente        |
| `GET`    |           `/accounts/transactions`     |            Lista todas transações da conta          |        Cliente        |
| `POST`   |                 `/products`            |               Criação de um novo produto            |    Operacional/Admin  |
| `GET`    |                 `/products`            |                 Listagem de produtos                |       Usuários        |
| `PATCH`  |               `/products/{id}`         |                Alteração de um produto              |    Operacional/Admin  |

## Autor

- **Gustavo Ferreira Viana**