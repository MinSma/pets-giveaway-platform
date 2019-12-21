# Pets Giveaway Platform (PGP)

## Getting Started

Use these instructions to get the required understanding to integrate into the project flow as smoothly as possible.

## Environment Setup

- Download [Node.js v10.15.0](https://nodejs.org/en/download) or older.
- Download [.NET Core 2.2 SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2).

## Project startup

- Move to PGP.WebUI/ClientApp using `cd PGP.WebUI/ClientApp` and install npm packages using `npm install` command.
- Move back to base folder using `cd ../..` and use `npm start` command.

## Database startup

- Select PGP.WebUI project as Startup project.
- Select PGP.Persistence project as Default project.
- Run `update-database` command inside Package Manager Console or `dotnet ef update-database` command inside console.
