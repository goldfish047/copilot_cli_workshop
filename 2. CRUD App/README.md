# CRUD App

<!--
Connect to the `Chinook.sqlite` database in this folder using the connection string: `DataSource=Chinook.sqlite;Cache=Shared;`.

Create a simple ASP.NET Razor Pages web application using .NET version 10.0 in a folder named `Chinook.Web` that works with the chinook database named `Chinook.sqlite`. Do the following:

- data that currently exists in the `Chinook.sqlite` should not be overwritten or lost
- move the database file into the `/Data` dirtectory of the razor pages project
- place the connection string of the application into the `appsettings.Development.json` file
- make sure to create a suitable `.gitignore` file and add to it file name `appsettings.Development.json`
- use Entity Framework
- build all the crud pages for the Genre table
- add a link to the Genre list on the main menu of the application
- change the bootstrap theme so it uses the `sketchy` theme from https://bootswatch.com/sketchy/
-->


# Tutorial 2 — Build a CRUD App

In this tutorial, you will use `GitHub Copilot CLI` to build a simple ASP.NET Razor Pages web application that reads and manages data from the Chinook database. You will do this step by step, one prompt at a time.

> [!NOTE]
> Before you start, make sure you have completed Tutorial 1 and have already run the following prompt inside Copilot CLI:
> ```
> Connect to the Chinook.sqlite database in this folder using the connection string: DataSource=Chinook.sqlite;Cache=Shared;
> ```
> If you have already done this, you can skip it and continue below.

---

## Prompt 1 — Create the Project

Type the following prompt inside Copilot CLI and press `ENTER`:
```
Create a simple ASP.NET Razor Pages web application using .NET 10 in a folder named Chinook.Web. Do not add any database or authentication yet.
```
> [!TIP]
> Wait for Copilot to finish completely before moving on to the next prompt. Rushing to the next step before Copilot is done is the most common cause of errors in this tutorial.

---

## Prompt 2 — Connect the Database

> [!WARNING]
> The following prompt connects to your existing Chinook.sqlite database. Do not modify or delete the database file while Copilot is running, as this may cause your data to be lost.
```
Add Entity Framework Core SQLite to Chinook.Web. Connect it to the existing Chinook.sqlite in the parent folder using connection string "DataSource=../Chinook.sqlite;Cache=Shared;" in appsettings.Development.json. Do not overwrite or delete any existing data.
```
> [!TIP]
> To verify the app is running correctly, open a **new terminal window**, navigate to the `Chinook.Web` folder, and run:
> ```bash
> dotnet watch
> ```
> Your browser should open automatically.

---

## Prompt 3 — Scaffold CRUD Pages
```
Create a Genre model that matches the existing Genre table in Chinook.sqlite. The SQLite database uses singular table names (e.g. Genre, not Genres), so the model must include a [Table("Genre")] attribute from System.ComponentModel.DataAnnotations.Schema to prevent Entity Framework Core from pluralizing the table name. Scaffold full CRUD Razor Pages for Genre. Add a Genre link to the main navigation menu.
```
> [!TIP]
> Go back to the terminal running `dotnet watch`. Your app should reload automatically. Navigate to the `/Genres` page in your browser. You should see a list of genres loaded from the Chinook database.

---

## Prompt 4 — Apply the Theme
```
Replace the default Bootstrap CSS in _Layout.cshtml with the Bootswatch Sketchy theme CDN link from https://bootswatch.com/sketchy/
```
Your app should now look noticeably different (hand-drawn style buttons and a unique font). Refresh `http://localhost:5000/Genres` to see the new theme applied.

---

## Prompt 5 — Run the app [!!!NOT WORKINGG half the timne!!! (only works on home page and cannot run genere page) so maybe need to run dotnet watch in other terminal]
```
Run the app 
```

If everything is working, you should see the Genre list page populated with data from the Chinook database. Try adding, editing, and deleting a genre to confirm the full CRUD functionality is working.
