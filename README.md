# Clean Architecture Web API Template - ASP.NET Core 9

A **Clean Architecture** Web API template for developers who don't want to reinvent the wheel and want to speed up project development.

## Features

- **JWT Bearer Authentication**  
- **Password Hashing**  
- **CRUD for Users**  
- **CRUD for Roles**  
- **Role Permissions**  
- **CRUD for Backups** (local files)  
- **Cache Management** (can be migrated to Redis)  
- **Local File Storage**

### Test Endpoints

- Many-to-many relationship: `Students - Classes`  
- One-to-many relationship: `Employees - Offices`  

### Design Patterns Applied

- CQRS  
- Mediator  
- Repository  
- Specification  
- Dependency Injection  

### Libraries Used

- MediatR  
- AutoMapper  
- Ardalis.Result  
- Ardalis.Specification  
- Bcrypt  

### Database

- PostgreSQL  
- Entity Framework Core  
- Fluent API  

### Others

- **CORS** support

## Prerequisites

1. **Add the connection string** in `app.development.json` so the API can connect to the database.  
2. **Add PostgreSQL commands to your PATH** (`pg_dump`, `psql`, etc.) to allow executing backups from the API.  

## Running the API

To run the API locally, execute:

```bash
dotnet run --project MyApp.Api
