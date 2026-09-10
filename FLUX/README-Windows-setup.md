# FLUX Setup Guide for Windows

FLUX is a project for fun 'cus i couldn't find anything better to do and i don't think you'll find it useful in anyway, but uhm, yeah. it's meant to be an OS and has turned out pretty good so far. oh yeah this guide is for version 1.0.0.

## Requirements

- The .NET 10 SDK
- PowerShell, Command Prompt, or Windows Terminal
- The project files in this folder:
  - `FLUX.csproj`
  - `Program.cs`
  - the NuGet package `Spectre.Console`, referenced in `FLUX.csproj`

## Install .NET 10

Install the .NET 10 SDK from Microsoft's official site if it is not already installed.

Verify it with:

```powershell
dotnet --version
```

You should see a .NET 10 version number.

## Setup commands

```text
+----------------------------------+
| PowerShell / Windows Terminal    |
|                                  |
| cd C:\path\to\FLUX*              |
| dotnet run                       |
+----------------------------------+
```
(*Listen, I'm not a windows user but if cd C:FLUX works(probably not), then.. uhm, good for you i guess..)

## FLUX commands

Once FLUX is running, use these built-in commands:

- `help`
- `info`
- `clear`
- `clock`
- `calc`
- `scicalc`
- `note`
- `yt`
- `desktop`
- `exit`

## Notes

- The `note` command saves notes to a `storage\notes.txt` file.
- `clear` is the explicit screen reset command.
- The app is meant to be easy to understand and run from a standard terminal.
