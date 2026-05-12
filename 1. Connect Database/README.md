# Tutorial 1 — Connect to a Database

In this tutorial, you will use `GitHub Copilot CLI` to connect to and explore the `Chinook` database (a sample database that represents a digital music store).

## What is the Chinook Database?

The Chinook database models a digital media store, similar to an old iTunes store. It contains real music data and includes 11 tables:

| Table | Description |
|---|---|
| `Artist` | Music artists |
| `Album` | Albums released by artists |
| `Track` | Individual songs, including price and duration |
| `Genre` | Music genres (Rock, Jazz, Pop, etc.) |
| `MediaType` | Format of the track (MP3, AAC, etc.) |
| `Playlist` | Named playlists |
| `PlaylistTrack` | Tracks belonging to each playlist |
| `Customer` | Store customers |
| `Employee` | Store employees and their reporting structure |
| `Invoice` | Customer purchases |
| `InvoiceLine` | Individual line items on each invoice |

## Connecting to the Database

Download the Chinook.sqlite database file here 👉[click me to download](https://github.com/goldfish047/copilot_cli_workshop/releases/download/Chinook.sqlite/Chinook.sqlite)









Connect to the `Chinook.sqlite` database in this folder using the connection string `DataSource=Chinook.sqlite;Cache=Shared;`. 

```
List all the tables.
```

```
Display data in the Genre table.
```

```
Analyse the data in the database and provide me with some interesting insights.
```
