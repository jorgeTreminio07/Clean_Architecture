# Clean Architecture Web API Template - ASP.NET Core 9
## 👤 Author
- Jorge Eduardo Treminio Cruz
- 📧 Email: eduardotreminio10@gmail.com
- Linkedln: https://www.linkedin.com/in/eduardo-treminio-b02b81323/

![.NET](https://img.shields.io/badge/.NET%209-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-336791?style=for-the-badge&logo=postgresql&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=for-the-badge&logo=nuget&logoColor=white)
![MediatR](https://img.shields.io/badge/MediatR-FF6C37?style=for-the-badge&logo=nuget&logoColor=white)
![AutoMapper](https://img.shields.io/badge/AutoMapper-FF5733?style=for-the-badge&logo=nuget&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![BCrypt](https://img.shields.io/badge/BCrypt-00BFFF?style=for-the-badge&logo=lock&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-4CAF50?style=for-the-badge&logo=archlinux&logoColor=white)
![CQRS](https://img.shields.io/badge/CQRS-FF9800?style=for-the-badge&logo=databricks&logoColor=white)
![Redis Ready](https://img.shields.io/badge/Cache%20Ready%20(Redis)-DC382D?style=for-the-badge&logo=redis&logoColor=white)

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
