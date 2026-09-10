# FLUX Setup Guide

FLUX is a project for fun 'cus i couldn't find anything better to do and i don't think you'll find it useful in anyway, but uhm, yeah. it's meant to be an OS and has turned out pretty good so far. oh yeah this guide is for version 1.0.0.

## Requirements

- .NET 10 SDK
- A terminal such as Terminal, iTerm2, or VS Code terminal
- The project files in this folder:
  - `FLUX.csproj`
  - `Program.cs`
  - the NuGet package `Spectre.Console`

## Install .NET 10

On macOS, you can install the .NET SDK from Microsoft's website or use a package manager if you already have one configured.

After installation, verify it works:

```bash
dotnet --version
```

You should see a .NET 10 version.

## Setup commands

```text
+----------------------------------+
| Bash / Terminal                  |
|                                  |
| cd /Path/to/FLUX Folder*         |
| dotnet run                       |
+----------------------------------+
```(*Alternatively, you can just type 'cd FLUX' (cus i could on my mac))

## Other cool stuff

- The `note` command saves text into a `storage/notes.txt` file.
- `clear` is the explicit screen-reset command.
- This project is a beginner-friendly terminal shell and is designed to stay simple.
