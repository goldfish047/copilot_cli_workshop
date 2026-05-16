# Tutorial 3 — Documentation

In this tutorial, you will use `GitHub Copilot CLI` to add data to your database, generate project documentation, perform a security audit, and plan what comes next.

---

## Prompt 1 — Generate Fake Data

By now you may have already tried adding, editing, and deleting genres directly from the app in Tutorial 2. In this prompt, we will ask Copilot to insert fake data into the database directly 

Type the following prompt inside Copilot CLI and press `ENTER`:

```
Insert 10 fake sample genres into the Genre table in Chinook.sqlite
```

Once Copilot is done, navigate to the `/Genres` page in your browser to confirm the new genres have been added. Feel free to play around — try editing or deleting some of the new entries to see the full CRUD functionality in action.

> [!TIP]
> Make sure your other terminal is still running `dotnet watch` so the app stays live while you work in Copilot CLI.

---

## Prompt 2 — Generate Project Documentation

Now that the app is working and has data in it, let's ask Copilot to document the whole project automatically.

```
Generate a README.md file for my Chinook.Web ASP.NET Razor Pages project that connects to the Chinook SQLite database. Include a project description, tech stack, folder structure, and setup instructions.
```

Once Copilot is done, open the generated `README.md` file from your `Chinook.Web` folder and review it. You should see a full description of everything you built across all three tutorials.

---

## Prompt 3 — Security Audit

So now you have a README documents of what your app does, but what about what it doesn't do? Let's ask Copilot to review the project for potential security risks and save the findings to a separate file.

```
Review the Chinook.Web project for any security vulnerabilities or risks and document the findings in a SECURITY.md file
```

Open the generated `SECURITY.md` file and read through the findings. You may be surprised by how many things Copilot picks up on in a simple app!

---

## Prompt 4 — Plan Version 2

Now that the project is documented and audited, let's ask Copilot what we could do next. You can use Copilot to brainstorm ideas for improvements, suggest new features to add, or plan out what a more complete version of this app would look like down the line.

```
Based on the Chinook.Web project, suggest what version 2 could look like. What features would you add, how long would it take, and what would the tech stack look like?
```

---
