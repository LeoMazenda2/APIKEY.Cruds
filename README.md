## 🔎 Descrição do Projeto

Este repositório contém uma solução completa de microsserviços para gestão de **Clientes** e **Carros**, composta por:

- **API RESTful** em .NET 8  
  - CRUD de Clientes e Carros  
  - Relacionamento 1:N (um Cliente pode ter vários Carros)  
  - Entity Framework Core com SQL Server  
  - Swagger (OpenAPI) para documentação e testes  

- **API Gateway** com Ocelot  
  - Roteamento de todas as chamadas via `/gateway/*`  
  - Base para autenticação, versionamento e rate-limiting no futuro  

- **Banco de Dados** em container Docker  
  - SQL Server 2022  
  - Volume nomeado para persistência de dados  
  - Configuração pronta para `docker-compose`  

- **Orquestração** com Docker Compose  
  - Sob demanda, sobe o SQL Server, a API e o Gateway  
  - Porta padrão:  
    - SQL Server → `1433`  
    - API .NET 8    → `5000`  
    - Gateway Ocelot → `6000`  

---

### 🛠️ Tecnologias

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Ocelot API Gateway](https://ocelot.readthedocs.io/)
- [Docker & Docker Compose](https://docs.docker.com/)

