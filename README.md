# Employee Management System

## Overview
The Employee Management System is a web application built with ASP.NET MVC. It enables HR and administrators to manage employee data efficiently, including personal information, department assignment, roles, and more.

---

## Key Features
- Create, Read, Update, Delete (CRUD) operations for employees
- Department management
- Role management
- Authentication and authorization (Admin, HR)
- Search and filter functionality
- User-friendly and responsive interface using Bootstrap

---

## Technologies Used
- ASP.NET MVC (C#)
- Entity Framework Core (Code First)
- SQL Server
- AutoMapper
- ASP.NET Identity (for Authentication)
- Bootstrap 5

---

## Architecture
- **Presentation Layer**: MVC Controllers + Razor Views
- **Business Logic Layer**: Services and Interfaces
- **Data Access Layer**: EF Core Repositories
- Design patterns used: MVC, Repository Pattern, Unit of Work, Dependency Injection

---

## Project Structure

```
EmployeeSystem/
├── Core/                            # Contains core logic (interfaces, models, DTOs)
│   ├── Interfaces/                 # Service and repository interfaces
│   ├── DTOs/                       # Data Transfer Objects
│   └── Models/                     # Domain models (e.g., Employee, Department)

├── Infrastructure/                 # Database access and implementation
│   ├── Data/                       # EF Core DbContext and seeding
│   │   └── ApplicationDbContext.cs
│   ├── Repositories/              # Repository implementations
│   └── Migrations/                # EF Core migration files

├── Services/                       # Business logic and service classes
│   └── EmployeeService.cs

├── Presentation/                   # UI and API layers
│   ├── Controllers/               # API Controllers (e.g., EmployeeController)
│   └── Views/                     # Razor Views
│       ├── Shared/
│       ├── Employee/
│       └── Department/

├── wwwroot/                        # Static files (CSS, JS, Images)

├── appsettings.json                # Application configuration
├── Program.cs                      # App entry point
└── Startup.cs                      # Middleware and service configuration
```

