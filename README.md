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

EmployeeSystem  
│  
├── Controllers/              # Contains all controller classes (e.g., EmployeeController)  
├── Models/                   # Contains domain models (e.g., Employee, Department)  
├── Views/                    # Contains Razor views  
│   ├── Shared/               # Layout files and shared views  
│   ├── Employee/             # Views related to Employee module  
│   └── Department/           # Views related to Department module  
├── DataAccess/               # EF Core DbContext and configurations  
├── Services/                 # Business logic and service implementations  
├── wwwroot/                  # Static files like CSS, JS, images  
├── appsettings.json          # Application configuration file  
├── Program.cs                # Main entry point of the application  
└── Startup.cs                # Configures services and middleware  

