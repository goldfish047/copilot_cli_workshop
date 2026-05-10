# CRUD App

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
