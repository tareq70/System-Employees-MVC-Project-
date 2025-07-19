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
EmployeeSystem/
├── Controllers/
├── Models/
├── Views/
│ ├── Shared/
│ ├── Employee/
│ └── Department/
├── DataAccess/
├── Services/
├── wwwroot/
└── Program.cs / Startup.cs
