Projeto: API de Clientes e Carros com Gateway Ocelot

Este repositório contém uma solução completa composta por:

Uma API REST para gestão de Clientes e Carros em .NET 8

Um Gateway Ocelot que atua como API Gateway

Um container SQL Server para persistência dos dados

Um docker-compose.yml para orquestração dos serviços

📁 Estrutura do Projeto

/
├── docker-compose.yml
├── CarClientApi/
│   ├── Controllers/
│   ├── Models/
│   ├── Repositories/
│   ├── Data/
│   ├── Program.cs
│   └── appsettings.json
├── CarrosGateway/
│   ├── ocelot.json
│   ├── Program.cs
│   └── launchSettings.json

🚀 1. API (.NET 8 - CarClientApi)

Uma API simples com:

CRUD de Clientes e Carros

Relacionamento 1:N (um Cliente possui vários Carros)

EF Core com SQL Server

Swagger para testes

Padrão Repository com Generic Repository

✅ Endpoints diretos da API (porta 5000)

GET /api/cliente

POST /api/cliente

PUT /api/cliente/{id}

DELETE /api/cliente/{id}

GET /api/carro

🛡️ 2. Gateway Ocelot (CarrosGateway)

Um microserviço que utiliza o Ocelot como API Gateway. Encaminha chamadas HTTP para a API principal.

📂 Configuração (ocelot.json)

Redireciona /gateway/cliente → http://carros-api:80/api/cliente

Redireciona /gateway/carro → http://carros-api:80/api/carro

✅ Endpoints via Gateway (porta 6000)

GET /gateway/cliente

POST /gateway/cliente

GET /gateway/carro

📃 3. SQL Server (Docker)

Banco de dados containerizado com volume persistente.

Imagem: mcr.microsoft.com/mssql/server:2022-latest

Usuário: sa

Senha: LvSagrado@12345

Volume: sqlserver_data

🚧 4. docker-compose

Facilita a orquestração de todos os serviços:

docker-compose up --build

🌐 Serviços levantados:

SQL Server (porta 1433)

API em .NET (porta 5000)

Gateway Ocelot (porta 6000)

🔄 5. Primeiros Passos

Certifica-te que tens o Docker instalado e a porta 1433 livre

Executa:

docker-compose up --build

Abre no navegador:

https://localhost:5000/swagger (API)

https://localhost:6000/gateway/cliente (Gateway)

Usa Postman ou Swagger para testar os endpoints

⚙️ 6. Migrações EF Core (caso necessário)

Se estiveres a trabalhar localmente, executa:

dotnet ef migrations add InitialCreate

dotnet ef database update

👤 Autor

Leonildo Vivaldo Mazenda

Para dúvidas, melhorias ou sugestões, sinta-se à vontade para abrir uma issue ou pull request.

