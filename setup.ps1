# Ensure script stops on first error
$ErrorActionPreference = "Stop"

Write-Host "🔍 Checking for .NET SDK..."
if (-not (Get-Command "dotnet" -ErrorAction SilentlyContinue)) {
    Write-Error "❌ .NET SDK is not installed."
    exit 1
}

Write-Host "📦 Restoring NuGet packages..."
dotnet restore

Write-Host "🛠️ Installing dotnet-ef as local tool (if not already)..."
dotnet new tool-manifest -o . -f > $null
dotnet tool install dotnet-ef --version 9.0.7 --global --ignore-failed-sources

Write-Host "🔧 Applying migrations to seed the database..."
dotnet ef migrations add InitialCreate --project .\PublicLibraryService.Infrastructure --startup-project .\PublicLibraryService.API --context PublicLibraryDbContext --output-dir Migrations
dotnet ef database update --project .\PublicLibraryService.Infrastructure --startup-project .\PublicLibraryService.API

Write-Host "`n✅ Setup complete. You can now run the API."
