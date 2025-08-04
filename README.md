# 📚 PublicLibraryService

A modern, modular ASP.NET Core Web API project built using Clean Architecture principles for managing a public library system. The system tracks books, borrowers, lending activity, and supports queries like book borrowing history, reading rate estimation, and top borrowers.

## 🏗 Architecture

This project follows **Clean Architecture** using:

- **Domain Layer** — Core business models and interfaces
- **Application Layer** — CQRS + MediatR for command/query separation
- **Infrastructure Layer** — SQLite DB, Repositories, EF Core, Logging (Serilog)
- **API Layer** — Versioned REST endpoints with Swagger support

✅ 1. Install EF Tools (if not already installed)
dotnet tool install --global dotnet-ef

✅ 2. Add a Migration
Navigate to the API project directory (where PublicLibraryService.API.csproj is located):

cd PublicLibraryService/PublicLibraryService.API
dotnet ef migrations add InitialCreate

This will generate migration files in the Migrations/ folder.

✅ 3. Apply the Migration (Create the SQLite DB)
bash
Copy
Edit
dotnet ef database update

**PowerShell Automation with setup.ps1**
You can run this with a PowerShell script placed at the root of your solution.

📄 setup.ps1
