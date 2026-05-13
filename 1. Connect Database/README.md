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

1. Download the `Chinook.sqlite` database file here 👉[Click to download](https://github.com/goldfish047/copilot_cli_workshop/releases/download/Chinook.sqlite/Chinook.sqlite)

2. Create a folder named `Chinook` and place the downloaded `Chinook.sqlite` file inside it.

3. Open your terminal, navigate to the `Chinook` folder, and launch `GitHub Copilot CLI`, by typing in:

```bash
copilot
```

5. Wait for the Copilot CLI interface to load. You should see the prompt ready for input.

6. Once inside Copilot CLI, type the following prompt and press `ENTER` to establish a connection to the Chinook database. This tells Copilot which database file to use and how to access it.
   ```
   Connect to the Chinook.sqlite database using the connection string DataSource=Chinook.sqlite;Cache=Shared;
   ```
7. You should see Copilot confirm the connection. If you are asked to trust files in the current folder or asked to run commands, press ENTER to confirm Yes.

## Exploring the Database

Once connected, try the following prompts one at a time. After each one, take a moment to look at the results before moving on.

**List all tables in the database:**
```
List all the tables in a table format
```

**See what is inside a table:**
```
Display data in the Genre table.
```

**Ask for insights:**
```
Analyse the data in the database and provide me with some interesting insights.
```
