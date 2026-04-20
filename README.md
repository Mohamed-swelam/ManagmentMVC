# ManagementMVC (ASP.NET Core .NET 10)

An ASP.NET Core **MVC** application (with Razor views) for managing an ITI-style academic domain: **Students**, **Departments**, **Courses**, **Instructors**, and the relationships between them. The project also includes **ASP.NET Core Identity** for authentication/authorization (roles + user accounts).

## Tech stack

- **.NET:** 10
- **Web:** ASP.NET Core MVC (Razor views)
- **Database:** SQL Server + Entity Framework Core (code-first migrations)
- **Auth:** ASP.NET Core Identity (`ApplicationUser`, roles)
- **Mapping:** Mapster

## Solution structure

Main project: `lab1/lab1.csproj`

Key folders/files:

- `lab1/Program.cs` – application startup, DI registration, middleware pipeline, routing, roles seeding.
- `lab1/Data/AppDbContext.cs` – EF Core DbContext.
- `lab1/Models/` – domain entities (e.g., `Student`, `Department`, `Course`, `Instructor`, relation tables) + `ApplicationUser`.
- `lab1/Controllers/` – MVC controllers:
  - `StudentController`, `DepartmentController`, `CourseController`, `InstructorController`
  - relationship controllers: `StudCourseController`, `InsCourseController`
  - auth: `AccountController`
- `lab1/Repositories/` + `lab1/Interfaces/IRepositories/` – repository pattern (`IGenericRepo<T>` + feature repos).
- `lab1/ViewModels/` – view models / DTOs for UI screens.
- `lab1/Views/` – Razor views and shared layouts.
- `lab1/Migrations/` – EF Core migrations.
- `lab1/MiddleWares/` – custom middleware (error handling, optional logging).
- `lab1/Helper/DbInitializer.cs` – seeds Identity roles.
- `lab1/appsettings.json` – configuration, including the SQL Server connection string.

## Features

- CRUD for:
  - Students
  - Departments
  - Courses
  - Instructors
- Manage many-to-many relationships:
  - Student ↔ Course (`Stud_Course`) with grade editing
  - Instructor ↔ Course (`Ins_Course`) with hours editing
- Identity authentication:
  - Register / Login / Logout
  - Role seeding (e.g., `Admin`, `User`)

## Getting started

### Prerequisites

- Visual Studio 2026 (or later) with the **ASP.NET and web development** workload
- SQL Server (LocalDB or a full SQL Server instance)

### Configure database

Update the connection string in `lab1/appsettings.json` if needed:

- `ConnectionStrings:DefaultConnection`

### Apply migrations

From the Package Manager Console (or terminal), run EF migrations:

- `Update-Database`

Or using the .NET CLI:

- `dotnet ef database update --project lab1/lab1.csproj`

### Run

- Visual Studio: set `lab1` as startup project and press **F5**
- CLI: `dotnet run --project lab1/lab1.csproj`

Default route is configured to:

- `/Student/Index`

## Authentication notes

- Identity is configured in `lab1/Program.cs` via `AddIdentity<ApplicationUser, IdentityRole>()`.
- Middleware order should include:
  - `app.UseAuthentication();`
  - `app.UseAuthorization();`
- Roles are seeded during startup using `Helper/DbInitializer.SeedRoles(...)`.

## Common development tasks

- Add a new entity: create model in `lab1/Models/`, update `AppDbContext`, add migration, update database.
- Add a new screen: add controller action in `lab1/Controllers/` + Razor view in `lab1/Views/`.
- Add repo logic: add interface to `lab1/Interfaces/IRepositories/` and implementation to `lab1/Repositories/`, register in `Program.cs`.
