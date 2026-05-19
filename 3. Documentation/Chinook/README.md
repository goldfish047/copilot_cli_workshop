# Chinook.Web

A minimal ASP.NET Core Razor Pages sample app that connects to the Chinook SQLite sample database to demonstrate reading and managing music-store data (Genres, Artists, Albums, Tracks, Customers, Invoices).

## Project description

Chinook.Web is intended for learning and quick experimentation with Razor Pages and EF Core against a local SQLite dataset. It includes simple CRUD pages for the Genre entity and shows how to wire a DbContext to a SQLite file.

## Tech stack

- .NET 10.0 (TargetFramework: net10.0)
- ASP.NET Core Razor Pages
- Entity Framework Core 8 (Microsoft.EntityFrameworkCore.Sqlite)
- SQLite (file-based Chinook sample DB)
- C#

## Project layout (key files)

- Chinook.Web.sln
- Chinook.Web/ (project root)
  - Chinook.Web.csproj
  - Program.cs                       - app startup and middleware
  - appsettings.json
  - appsettings.Development.json     - development connection string
  - Pages/                           - Razor Pages (Index, Privacy, Genres pages)
    - Genres/ (Index, Create, Edit, Details, Delete)
    - Shared/ (_Layout, validation partials)
  - Data/
    - ChinookContext.cs              - EF DbContext mapping
    - Chinook.sqlite                  - (sample DB present in this repo)
  - Models/
    - Genre.cs
  - wwwroot/                         - static assets

Note: The repository currently includes Data\Chinook.sqlite. Consider removing it from source control for production or sensitive data.

## Configuration

Program.cs reads the connection string named "DefaultConnection" from configuration and registers the DbContext:

```csharp
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
```

Example appsettings.Development.json (recommended path if DB placed in Data/):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Data\\Chinook.sqlite;Cache=Shared;"
  }
}
```

Use double backslashes in JSON on Windows or a relative path as shown above. `;Cache=Shared;` helps avoid "database is locked" when multiple connections are used.

## Prerequisites

- .NET 10 SDK (install from https://dotnet.microsoft.com)
- Copy or download the Chinook SQLite database file into `Chinook.Web\Data\Chinook.sqlite` (if not present)
- Optional: DB Browser for SQLite to inspect the DB

## Quick start

1. Open a terminal and change to the project folder:

```powershell
cd "C:\Users\jonat\copilot workshop\chinook 1\Chinook.Web"
```

2. (Optional) Trust dev HTTPS certificate if not already trusted:

```powershell
dotnet dev-certs https --trust
```

3. Restore and build:

```powershell
dotnet restore
dotnet build
```

4. Run the app:

```powershell
dotnet run
```

5. Open the URL printed in the console (typically https://localhost:5001) and navigate to /Genres.

## Notes & best practices

- Development secrets: keep connection strings out of committed appsettings.json — use User Secrets or environment variables.
- Authentication/Authorization: sample pages do not enforce auth. Add authentication and protect write operations for production.
- Do not commit production databases. If the sample DB is unnecessary in the repo, remove it and add `Data\\Chinook.sqlite` to .gitignore.
- Use EF migrations cautiously with file-based SQLite; for read-only samples, migrations may not be necessary.

## Troubleshooting

- "SQLite database is locked": add `;Cache=Shared;` to the connection string and ensure only one writer at a time.
- Cannot open DB: verify the path in DefaultConnection and filesystem permissions.

## License

Add project license information here.

---

If anything in this README should be tailored to your environment, tell me what to change and I can update it.