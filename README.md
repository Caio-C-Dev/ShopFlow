# ShopFlow API

A production-ready e-commerce REST API built with **ASP.NET Core 9**, following **Clean Architecture** principles with CQRS, JWT authentication, and Docker support.

---

## Architecture

The solution is organized into 5 projects following the **Dependency Rule** — dependencies always point inward toward the Domain layer:

```
ShopFlow.Domain          → Entities, Value Objects, Interfaces, Events, Enums
ShopFlow.Application     → Use Cases (Commands/Queries), Validators, Behaviors
ShopFlow.Infrastructure  → EF Core, Repositories, JWT, ASP.NET Identity
ShopFlow.API             → Controllers, Middlewares, Program.cs
ShopFlow.Tests           → Unit Tests
```

```
Domain ← Application ← Infrastructure ← API
                ↑
              Tests
```

The compiler enforces this rule — `Application` cannot import EF Core, and `Domain` cannot import anything.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 9 |
| ORM | Entity Framework Core 9 |
| Database | SQL Server |
| Authentication | ASP.NET Identity + JWT Bearer |
| CQRS | MediatR |
| Validation | FluentValidation |
| Testing | xUnit + NSubstitute + FluentAssertions |
| Containerization | Docker + docker-compose |
| CI | GitHub Actions |

---

## Features

- **Clean Architecture** with strict dependency rules enforced by the compiler
- **CQRS** with MediatR — Commands and Queries separated by intent
- **JWT Authentication** with ASP.NET Identity — register, login, role-based authorization
- **Automatic validation** via FluentValidation Pipeline Behavior — handlers only run with valid data
- **Global exception handling** middleware returning structured error responses
- **Value Objects** — `Money` type for price with currency, preventing invalid operations
- **Entity encapsulation** — state changes only through domain methods
- **EF Core** with owned types, configurations, and automatic migrations on startup
- **Swagger UI** with Bearer token support
- **Docker** — single command to run the full stack

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Running with Docker

```bash
git clone https://github.com/Caio-C-Dev/ShopFlow.git
cd ShopFlow
docker-compose up --build
```

The API will be available at `http://localhost:8080/swagger`

SQL Server runs on port `1434` — no local SQL Server installation required.

### Running locally

```bash
# Update connection string in ShopFlow.API/appsettings.json
dotnet restore
dotnet run --project ShopFlow.API
```

---

## API Endpoints

### Authentication

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/api/Auth/register` | Create account and get JWT | Public |
| POST | `/api/Auth/login` | Login and get JWT | Public |

### Catalog

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/api/Catalog` | Create a product | Required |
| GET | `/api/Catalog/{id}` | Get product by ID | Public |

---

## Authentication Flow

```
POST /api/Auth/register
  Body: { nomeCompleto, email, password }
  Response: { token: "eyJ..." }

POST /api/Catalog
  Header: Authorization: Bearer eyJ...
  Body: { nome, descricao, preco, moeda, estoque }
  Response: 201 Created
```

---

## Project Structure

```
ShopFlow.Application/
├── Catalog/
│   ├── Commands/CriarProduto/
│   │   ├── CriarProdutoCommand.cs
│   │   ├── CriarProdutoCommandHandler.cs
│   │   └── CriarProdutoValidator.cs
│   └── Queries/GetProduto/
│       ├── GetProdutoQuery.cs
│       ├── GetProdutoQueryHandler.cs
│       └── ProdutoResponse.cs
├── Identity/
│   ├── Commands/Register/
│   ├── Commands/Login/
│   └── IAuthService.cs
└── Common/
    └── Behaviors/ValidationBehavior.cs

ShopFlow.Domain/
├── Entities/       → Produto, Pedido, ItemPedido
├── ValueObjects/   → Money
├── Interfaces/     → IProdutoRepository, IPedidoRepository
├── Events/
└── Enums/          → StatusPedido
```

---

## Running Tests

```bash
dotnet test
```

```
Total: 3 | Passed: 3 | Failed: 0
```

Tests use **NSubstitute** for faking dependencies — no database required.

---

## CI/CD

GitHub Actions runs on every push to `main`:

1. Restore dependencies
2. Build in Release mode
3. Run all tests

A failing test blocks the pipeline.

---

## Design Decisions

**Why MediatR?** Decouples controllers from handlers. Adding a new use case means creating one folder — no controller changes needed.

**Why `Money` as a Value Object instead of `decimal`?** Prevents summing currencies with different units (BRL + USD). Centralizes monetary rules in one place.

**Why interfaces in Domain, not Infrastructure?** The Domain defines what it needs (`IProdutoRepository`). Infrastructure obeys. Swapping SQL Server for MongoDB only changes Infrastructure.

**Why `private set` on all Entity properties?** State changes only through domain methods (`DecrementarEstoque`, `Concluir`). Prevents invalid state from outside.

---

## License

MIT
