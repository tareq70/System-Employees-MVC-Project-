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

## Project Structure (3-Tier Architecture)

```
EmployeeSystem/
├── Presentation/                   # UI Layer (Handles user interaction)
│   ├── Controllers/               # API or MVC Controllers
│   └── Views/                     # Razor Views (if applicable)

├── BusinessLogic/                 # Business Logic Layer (Core application logic)
│   ├── Interfaces/               # Service Interfaces
│   ├── Services/                 # Service Implementations
│   └── DTOs/                     # Data Transfer Objects

├── DataAccess/                    # Data Access Layer (Database interaction)
│   ├── Models/                   # Domain Models (e.g., Employee, Department)
│   ├── Context/                  # EF Core DbContext
│   └── Repositories/            # Repository Interfaces & Implementations

├── wwwroot/                       # Static web assets (CSS, JS, Images)

├── appsettings.json               # Configuration file
├── Program.cs                     # App entry point
└── Startup.cs                     # Services & middleware configuration
```
