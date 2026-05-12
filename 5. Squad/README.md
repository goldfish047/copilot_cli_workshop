# Squad

## What is Squad?

Squad is a team of agents that work on your behalf to conduct specializations like: DevOps, Testing, DB optimization, etc. 

This is an experimental project and may change over time.

The GitHub repo for Squad is at [https://github.com/bradygaster/squad](url)

Note the following about Squad:
1. Squad works with GitHub Copilot
2. You need to have **Node.js** and **npm** (version 5.2.0 or higher) installed on your computer in order to setup Squad.
3. Using Squad can result in the consumption of much tokens.

## Let's take it for a spin

In the *Chinook.Web* folder created in tutorial number 2 (CRUD App), install Squad by typing the follwoing terminal window command:

```bash
npm install -g @bradygaster/squad-cli
```

Next, initialize Squad with:

```bash
squad init
```

You must be logged into GitHub in order to use Squad. Type the following command to login into GitHub:

```bash
gh auth login
```

Start a GitHub Copilot CLI session by typing the following terminal window command:

```bash
copilot
```

We will choose the Squad agent to help us improve the Chinook.Web app. In the input field, type the following command to select an agent:

```
/agent
```

