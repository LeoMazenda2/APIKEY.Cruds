# 🚀 API de Clientes e Carros com Gateway Ocelot

## 🔎 Descrição do Projeto
Este repositório contém uma solução base de microsserviços para gestão de **Clientes** e **Carros**, composta por:

- **API RESTful** em .NET 8  
  - CRUD de Clientes e Carros  
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
  - Sobe o SQL Server, a API e o Gateway  
  - Portas padrão:  
    - SQL Server → `1433`  
    - API .NET 8   → `5000`  
    - Gateway Ocelot → `6000`  

---
## ⚙️ Como Rodar

1. **Clone o repositório**  
   ```bash
   git clone https://github.com/SEU_USUARIO/APIKEY.Crudes.git
   cd APIKEY.Crudes

1. **Container em docker**
- Basta rodar o comando a baixo 
   ```bash 
   docker-compose up --build -d

---
## 🛠️ Tecnologias
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [Entity Framework Core](https://docs.microsoft.com/ef/core)  
- [Ocelot API Gateway](https://ocelot.readthedocs.io/)  
- [Docker & Docker Compose](https://docs.docker.com/)  

---

## 📁 Estrutura do Projeto
```text
.
├── docker-compose.yml
├── CarClientApi
│   ├── Controllers
│   │   ├── ClienteController.cs
│   │   └── CarroController.cs
│   ├── Models
│   │   ├── Cliente.cs
│   │   └── Carro.cs
│   ├── Repositories
│   │   ├── Interfaces
│   │   └── Implementations
│   ├── Data
│   │   └── AppDbContext.cs
│   ├── Program.cs
│   └── appsettings.json
└── CarrosGateway
    ├── ocelot.json
    ├── Program.cs
    ├── CarrosGateway.csproj
    └── Properties
        └── launchSettings.json
