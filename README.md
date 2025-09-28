# Clean Architecture Web API Template - ASP.NET Core 9
## 👤 Author
- Jorge Eduardo Treminio Cruz
- 📧 Email: eduardotreminio10@gmail.com
- Linkedln: https://www.linkedin.com/in/eduardo-treminio-b02b81323/

![.NET](https://img.shields.io/badge/.NET%209-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-336791?style=for-the-badge&logo=postgresql&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=for-the-badge&logo=nuget&logoColor=white)

A **Clean Architecture** Web API template for developers who want to speed up development without reinventing the wheel.

---

## 🚀 Features

- **JWT Bearer Authentication**
- **Password Hashing**
- **CRUD for Users**
- **CRUD for Roles**
- **Role Permissions**
- **CRUD for Backups** (local files)
- **Cache Management** (can be migrated to Redis)
- **Local File Storage**

### 🔗 Test Endpoints

- Many-to-many relationship: `Students - Classes`
- One-to-many relationship: `Employees - Offices`

### 🏗 Design Patterns Applied

- CQRS
- Mediator
- Repository
- Specification
- Dependency Injection

### 📚 Libraries Used

- MediatR
- AutoMapper
- Ardalis.Result
- Ardalis.Specification
- Bcrypt

### 🗄 Database

- PostgreSQL
- Entity Framework Core
- Fluent API

### ⚙️ Others

- **CORS** support

---

## ⚡ Getting Started

### ✅ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [PostgreSQL](https://www.postgresql.org/)

### 🔧 Setup

1. Clone this repository:
2. Add your database connection string in:
   app.development.json
   Example:
   
   ```bash
   {
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Database=MyDb;Username=postgres;Password=yourpassword"
    }
   }

4. Make sure PostgreSQL commands (pg_dump, psql, etc.) are added to your system PATH for backup support.
   
5. perform a database migration with the command
    ```bash
   dotnet ef migrations add <MigrationName>
6. apply the migration to your database:
   ```bash
   dotnet ef database update
7. ▶️ Running the API
  ```bash
  dotnet run --project MyApp.Api
