using System;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Spectre.Console;

class FluxSettings
{
    public string Theme { get; set; } = "classic";
    public string ModelName { get; set; } = "llama3.2";
}

class Program
{
    static FluxSettings AppSettings = LoadSettings();

    static void Main()
    {
        if (IsWin1Theme())
        {
            RestartWin1BootSequence();
        }
        else
        {
            DrawBanner();
        }

        ShowBootMessage();

        while (true)
        {
            Console.Write("FLUX> ");
            string command = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

            if (command == "help")
            {
                ShowHelp();
            }
            else if (command == "info")
            {
                ShowInfo();
            }
            else if (command == "clear")
            {
                Console.Clear();
                DrawBanner();
                ShowBootMessage();
            }
            else if (command == "clock")
            {
                RunClock();
            }
            else if (command == "settings" || command == "theme")
            {
                RunThemeCommand();
            }
            else if (command == "calc")
            {
                RunCalculator();
            }
            else if (command == "scicalc")
            {
                RunScientificCalculator();
            }
            else if (command == "note")
            {
                RunNotepad();
            }
            else if (command == "notes")
            {
                ShowSavedNotes();
            }
            else if (command == "delete")
            {
                DeleteStoredNote();
            }
            else if (command.StartsWith("delete note "))
            {
                DeleteStoredNote(command.Substring("delete note ".Length));
            }
            else if (command.StartsWith("delete "))
            {
                DeleteStoredNote(command.Substring("delete ".Length));
            }
            else if (command == "open")
            {
                RunOpenFile();
            }
            else if (command == "view")
            {
                RunOpenFile();
            }
            else if (command == "view storage")
            {
                ShowStorageInfo();
            }
            else if (command == "chat")
            {
                RunChat();
            }
            else if (command == "draw")
            {
                RunDrawTool();
            }
            else if (command == "yt")
            {
                ShowChannelInfo();
            }
            else if (command == "desktop")
            {
                RunDesktopShell();
            }
            else if (command == "exit")
            {
                AnsiConsole.MarkupLine("\n[red]The session was stopped.[/]");
                break;
            }
            else if (command != string.Empty)
            {
                AnsiConsole.MarkupLine($"\n[red]Error:[/] what is '[yellow]{command}[/]'? Type [green]'help'[/] for available commands.");
            }
        }
    }

    static void DrawBanner()
    {
        if (IsWin1Theme())
        {
            AnsiConsole.Write(new Panel("[white]FLUX 1.0.0[/]")
                .Header("[black]Windows 1.0 Theme[/]")
                .BorderColor(Color.Blue)
                .Expand());
            Console.WriteLine("A simple old-school shell look is active...at least we tried to make it..\n");
            return;
        }

        AnsiConsole.Write(
            new FigletText("FLUX")
                .Centered()
                .Color(Color.Magenta1));
        Console.WriteLine("1.0.0");
        Console.WriteLine("using .NET 10\n");
    }

    static void RestartWin1BootSequence()
    {
        Console.Clear();
        Console.WriteLine("Restarting FLUX...");
        Console.WriteLine("Loading Windows 1.0 shell...");
        Console.Clear();
        ApplyThemeAppearance();
        DrawBanner();
    }

    static void ShowBootMessage()
    {
        if (IsWin1Theme())
        {
            AnsiConsole.MarkupLine("\n[blue]FLUX has been loaded.[/]");
            AnsiConsole.MarkupLine($"[cyan]Current theme:[/] [yellow]{AppSettings.Theme}[/]");
            AnsiConsole.MarkupLine("[cyan]Ready for input. Type 'help' for a list of commands.[/]\n");
            return;
        }

        AnsiConsole.MarkupLine("\n[green]FLUX has been loaded.[/]");
        AnsiConsole.MarkupLine($"[cyan]Current theme:[/] [yellow]{AppSettings.Theme}[/]");
        AnsiConsole.MarkupLine("[cyan]Ready for input. Type 'help' for a list of commands.[/]\n");
    }

    static void ShowHelp()
    {
        if (IsWin1Theme())
        {
            AnsiConsole.Write(new Panel("[white]FLUX Command List[/]").BorderColor(Color.Blue));
        }
        else
        {
            AnsiConsole.Write(new Panel("[green]FLUX Command List[/]").BorderColor(Color.Green));
        }

        AnsiConsole.MarkupLine("[cyan]help[/]  - Shows this menu");
        AnsiConsole.MarkupLine("[cyan]info[/]  - Displays OS version details");
        AnsiConsole.MarkupLine("[cyan]clear[/] - Clears the current window screen");
        AnsiConsole.MarkupLine("[cyan]clock[/] - Shows the current date and time");
        AnsiConsole.MarkupLine("[cyan]theme[/] - Opens the FLUX theme picker");
        AnsiConsole.MarkupLine("[cyan]calc[/]  - Opens a simple calculator");
        AnsiConsole.MarkupLine("[cyan]scicalc[/] - Opens a scientific calculator");
        AnsiConsole.MarkupLine("[cyan]note[/]  - Opens a simple writing tool");
        AnsiConsole.MarkupLine("[cyan]notes[/] - Shows saved notes from storage");
        AnsiConsole.MarkupLine("[cyan]delete[/] - Deletes a saved note by name");
        AnsiConsole.MarkupLine("[cyan]open[/]  - Views a file from inside FLUX");
        AnsiConsole.MarkupLine("[cyan]view[/]  - Alias for open file preview");
        AnsiConsole.MarkupLine("[cyan]view storage[/] - Lists the FLUX storage folder and its paths");
        AnsiConsole.MarkupLine("[cyan]draw[/]  - Opens a ASCII drawing tool");
        AnsiConsole.MarkupLine("[cyan]chat[/]  - Opens a local FLUX assistant");
        AnsiConsole.MarkupLine("[cyan]yt[/]    - The developer's YouTube channel info");
        AnsiConsole.MarkupLine("[cyan]desktop[/] - Opens a desktop-style picker");
        AnsiConsole.MarkupLine("[cyan]exit[/]  - Stops the session\n");
    }

    static void ShowInfo()
    {
        AnsiConsole.MarkupLine("\n[cyan]FLUX Operating System v1.0.0[/]");
        AnsiConsole.MarkupLine("[yellow]Kernel Core:[/] .NET 10.0 Standard Architecture");
        AnsiConsole.MarkupLine("[yellow]Developer:[/] itoldyounow");
        AnsiConsole.MarkupLine("[yellow]Channel:[/] itoldyounow on YouTube");
        AnsiConsole.MarkupLine($"[yellow]Theme:[/] {AppSettings.Theme}\n");
    }

    static void RunThemeCommand()
    {
        Console.Clear();
        ApplyThemeAppearance();

        if (IsWin1Theme())
        {
            AnsiConsole.Write(new Panel("[white]FLUX Theme[/]").BorderColor(Color.Blue));
        }
        else
        {
            AnsiConsole.Write(new Panel("[cyan]FLUX Theme[/]").BorderColor(Color.Cyan));
        }

        AnsiConsole.MarkupLine($"[yellow]Current theme:[/] [green]{AppSettings.Theme}[/]");
        Console.Write("Choose a theme [classic/win1]: ");
        string input = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();

        if (!string.IsNullOrWhiteSpace(input))
        {
            if (input == "classic" || input == "win1")
            {
                AppSettings.Theme = input;
                SaveSettings(AppSettings);
                AnsiConsole.MarkupLine($"\n[green]Theme saved:[/] [yellow]{AppSettings.Theme}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("\n[red]Unknown theme choice.[/] Use [green]classic[/] or [green]win1[/].");
            }
        }

        AnsiConsole.MarkupLine("\nPress [green]any key[/] to return to FLUX...");
        Console.ReadKey(true);

        Console.Clear();
        if (IsWin1Theme())
        {
            RestartWin1BootSequence();
        }
        else
        {
            DrawBanner();
        }

        ShowBootMessage();
    }

    static bool IsWin1Theme()
    {
        return string.Equals(AppSettings.Theme, "win1", StringComparison.OrdinalIgnoreCase);
    }

    static void ApplyThemeAppearance()
    {
        if (IsWin1Theme())
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            return;
        }

        Console.ResetColor();
    }

    static FluxSettings LoadSettings()
    {
        string storageDirectory = Path.Combine(Environment.CurrentDirectory, "storage");
        Directory.CreateDirectory(storageDirectory);

        string settingsPath = Path.Combine(storageDirectory, "settings.json");
        if (!File.Exists(settingsPath))
        {
            FluxSettings defaultSettings = new FluxSettings();
            SaveSettings(defaultSettings);
            return defaultSettings;
        }

        try
        {
            string json = File.ReadAllText(settingsPath);
            FluxSettings? loadedSettings = JsonSerializer.Deserialize<FluxSettings>(json);
            return loadedSettings ?? new FluxSettings();
        }
        catch
        {
            return new FluxSettings();
        }
    }

    static void SaveSettings(FluxSettings settings)
    {
        string storageDirectory = Path.Combine(Environment.CurrentDirectory, "storage");
        Directory.CreateDirectory(storageDirectory);

        string settingsPath = Path.Combine(storageDirectory, "settings.json");
        string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(settingsPath, json);
    }

    static void RunClock()
    {
        DateTime now = DateTime.Now;

        AnsiConsole.Write(new Panel("[cyan]FLUX Clock[/]").BorderColor(Color.Cyan));
        AnsiConsole.MarkupLine($"[yellow]Current Date:[/] {now:dddd, MMMM dd, yyyy}");
        AnsiConsole.MarkupLine($"[yellow]Current Time:[/] {now:HH:mm:ss}");
        AnsiConsole.MarkupLine("\nPress [green]enter[/] to return to the command prompt...");
        Console.ReadLine();
    }

    static void RunCalculator()
    {
        Console.Clear();
        AnsiConsole.Write(new Panel("[green]FLUX Standard Calculator Subsystem[/]").BorderColor(Color.Green));

        try
        {
            double num1 = AnsiConsole.Ask<double>("Enter first number: ");
            string op = AnsiConsole.Ask<string>("Enter operator ([blue]+, -, *, /[/]): ");
            double num2 = AnsiConsole.Ask<double>("Enter second number: ");

            double result = 0;
            if (op == "+") result = num1 + num2;
            else if (op == "-") result = num1 - num2;
            else if (op == "*") result = num1 * num2;
            else if (op == "/") result = num1 / num2;

            AnsiConsole.MarkupLine($"\n[green]Result: {num1} {op} {num2} = {result}[/]\n");
        }
        catch
        {
            AnsiConsole.MarkupLine("[red]Error: those aren't numbers..[/]");
        }

        AnsiConsole.MarkupLine("Press [green]any key[/] to close tool...");
        Console.ReadKey(true);
    }

    static void RunScientificCalculator()
    {
        Console.Clear();
        AnsiConsole.Write(new Panel("[magenta]Scientific Calculator[/]").BorderColor(Color.Magenta1));

        var mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose operation:")
                .AddChoices(new[] { "sin", "cos", "abs", "pow", "bin" }));

        try
        {
            if (mode == "sin" || mode == "cos")
            {
                double degrees = AnsiConsole.Ask<double>("Enter angle in degrees: ");
                double radians = degrees * (Math.PI / 180.0);
                double result = mode == "sin" ? Math.Sin(radians) : Math.Cos(radians);
                AnsiConsole.MarkupLine($"[magenta]Result: {mode}({degrees}°) = {result}[/]");
            }
            else if (mode == "abs")
            {
                double num = AnsiConsole.Ask<double>("Enter a number: ");
                AnsiConsole.MarkupLine($"[magenta]Absolute value: {Math.Abs(num)}[/]");
            }
            else if (mode == "pow")
            {
                double baseNum = AnsiConsole.Ask<double>("Enter base number: ");
                double exp = AnsiConsole.Ask<double>("Enter exponent power: ");
                AnsiConsole.MarkupLine($"[magenta]Result: {baseNum} ^ {exp} = {Math.Pow(baseNum, exp)}[/]");
            }
            else if (mode == "bin")
            {
                int num = AnsiConsole.Ask<int>("Enter integer to convert: ");
                AnsiConsole.MarkupLine($"[magenta]Binary Value: {Convert.ToString(num, 2)}[/]");
            }
        }
        catch
        {
            AnsiConsole.MarkupLine("[red]Error: Math engine parse failure.[/]");
        }

        AnsiConsole.MarkupLine("\nPress [green]any key[/] to close tool...");
        Console.ReadKey(true);
    }

    static void RunNotepad()
    {
        Console.Clear();
        AnsiConsole.Write(new Panel("[yellow]FLUX Text Writer Workspace[/]").BorderColor(Color.Yellow));
        Console.Write("Give this note a title: ");
        string title = (Console.ReadLine() ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(title))
        {
            title = "untitled";
        }

        Console.Write("Write text: ");
        string note = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(note))
        {
            AnsiConsole.MarkupLine("\n[red]No text was saved because the note was empty.[/]");
            AnsiConsole.MarkupLine("Press [green]any key[/] to exit notepad...");
            Console.ReadKey(true);
            return;
        }

        string storageDirectory = Path.Combine(Environment.CurrentDirectory, "storage", "notes");
        string noteFolder = Path.Combine(storageDirectory, SanitizePathSegment(title));
        string noteFile = Path.Combine(noteFolder, "note.txt");

        Directory.CreateDirectory(noteFolder);
        File.WriteAllText(noteFile, note + Environment.NewLine);

        AnsiConsole.MarkupLine($"\n[green]Success![/] Note saved to: [yellow]{noteFile}[/]\n");
        AnsiConsole.MarkupLine("Press [green]any key[/] to exit notepad...");
        Console.ReadKey(true);
    }

    static void ShowSavedNotes()
    {
        string storageDirectory = Path.Combine(Environment.CurrentDirectory, "storage", "notes");

        if (!Directory.Exists(storageDirectory))
        {
            AnsiConsole.MarkupLine("\n[red]No saved notes found.[/]");
            return;
        }

        string[] noteFolders = Directory.GetDirectories(storageDirectory);
        if (noteFolders.Length == 0)
        {
            AnsiConsole.MarkupLine("\n[red]No saved notes found.[/]");
            return;
        }

        foreach (string noteFolder in noteFolders)
        {
            string noteFile = Path.Combine(noteFolder, "note.txt");
            if (!File.Exists(noteFile))
            {
                continue;
            }

            string title = Path.GetFileName(noteFolder);
            string noteText = File.ReadAllText(noteFile).Trim();

            var panel = new Panel(noteText)
                .Header(title)
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Yellow);

            AnsiConsole.Write(panel);
        }
    }

    static void DeleteStoredNote(string? suppliedName = null)
    {
        string storageDirectory = Path.Combine(Environment.CurrentDirectory, "storage", "notes");

        if (!Directory.Exists(storageDirectory))
        {
            AnsiConsole.MarkupLine("\n[red]No saved notes found.[/]");
            return;
        }

        string title = suppliedName?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.Write("Enter the note title to delete: ");
            title = (Console.ReadLine() ?? string.Empty).Trim();
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            AnsiConsole.MarkupLine("\n[red]No note title was provided.[/]");
            return;
        }

        string noteFolder = Path.Combine(storageDirectory, SanitizePathSegment(title));
        string noteFile = Path.Combine(noteFolder, "note.txt");

        if (!File.Exists(noteFile))
        {
            AnsiConsole.MarkupLine($"\n[red]No saved note named:[/] [yellow]{title}[/]");
            return;
        }

        File.Delete(noteFile);
        Directory.Delete(noteFolder, false);

        AnsiConsole.MarkupLine($"\n[green]Deleted note:[/] [yellow]{title}[/]");
    }

    static void RunOpenFile()
    {
        Console.Write("Enter file path to preview: ");
        string input = (Console.ReadLine() ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            AnsiConsole.MarkupLine("\n[red]No file path was entered.[/]");
            return;
        }

        string resolvedPath = ResolvePath(input);
        if (!File.Exists(resolvedPath))
        {
            AnsiConsole.MarkupLine($"\n[red]File not found:[/] [yellow]{resolvedPath}[/]");
            return;
        }

        string content = File.ReadAllText(resolvedPath);
        string[] lines = content.Replace("\r\n", "\n").Split('\n');

        AnsiConsole.Write(new Panel(string.Join(Environment.NewLine, lines.Take(20)))
            .Header(Path.GetFileName(resolvedPath))
            .Border(BoxBorder.Rounded)
            .BorderColor(Color.Cyan));
    }

    static void RunDrawTool()
    {
        Console.Clear();
        AnsiConsole.Write(new Panel("[cyan]FLUX ASCII Drawing Tool[/]").BorderColor(Color.Cyan));
        Console.WriteLine("Use arrow keys to move the cursor, press a character to draw, press space to erase, press 's' to save, and press 'q' to quit.");
        Console.Write("Name this drawing: ");
        string drawingName = (Console.ReadLine() ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(drawingName))
        {
            drawingName = "untitled";
        }

        drawingName = SanitizePathSegment(drawingName);

        int width = 12;
        int height = 8;
        int cursorX = 0;
        int cursorY = 0;
        char[,] canvas = new char[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                canvas[y, x] = ' ';
            }
        }

        while (true)
        {
            RenderDrawCanvas(canvas, cursorX, cursorY);

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.LeftArrow) cursorX = Math.Max(0, cursorX - 1);
            else if (key.Key == ConsoleKey.RightArrow) cursorX = Math.Min(width - 1, cursorX + 1);
            else if (key.Key == ConsoleKey.UpArrow) cursorY = Math.Max(0, cursorY - 1);
            else if (key.Key == ConsoleKey.DownArrow) cursorY = Math.Min(height - 1, cursorY + 1);
            else if (key.Key == ConsoleKey.Spacebar) canvas[cursorY, cursorX] = ' ';
            else if (key.Key == ConsoleKey.S) 
            {
                string storageDirectory = Path.Combine(Environment.CurrentDirectory, "storage");
                string drawingsDirectory = Path.Combine(storageDirectory, "drawings");
                string drawingFolder = Path.Combine(drawingsDirectory, drawingName);
                Directory.CreateDirectory(drawingFolder);

                string drawingFile = Path.Combine(drawingFolder, "drawing.txt");
                File.WriteAllText(drawingFile, BuildAsciiCanvas(canvas));

                AnsiConsole.MarkupLine($"\n[green]Saved drawing to:[/] [yellow]{drawingFile}[/]");
                Console.WriteLine("Press any key to return to FLUX...");
                Console.ReadKey(true);
                return;
            }
            else if (key.Key == ConsoleKey.Q)
            {
                return;
            }
            else if (!char.IsControl(key.KeyChar))
            {
                canvas[cursorY, cursorX] = key.KeyChar;
            }
        }
    }

    static void RenderDrawCanvas(char[,] canvas, int cursorX, int cursorY)
    {
        Console.SetCursorPosition(0, 5);
        string art = BuildAsciiCanvas(canvas);
        string[] lines = art.Split(Environment.NewLine);

        for (int i = 0; i < lines.Length; i++)
        {
            Console.Write(new string(' ', 80));
            Console.SetCursorPosition(0, 5 + i);
            Console.Write(lines[i]);
        }

        Console.SetCursorPosition(cursorX, 5 + cursorY);
        Console.Write("^");
        Console.SetCursorPosition(0, 5 + canvas.GetLength(0) + 2);
    }

    static string BuildAsciiCanvas(char[,] canvas)
    {
        int height = canvas.GetLength(0);
        int width = canvas.GetLength(1);
        string[] lines = new string[height];

        for (int y = 0; y < height; y++)
        {
            char[] row = new char[width];
            for (int x = 0; x < width; x++)
            {
                row[x] = canvas[y, x];
            }

            lines[y] = new string(row);
        }

        return string.Join(Environment.NewLine, lines);
    }

    static void ShowStorageInfo()
    {
        string storageDirectory = Path.Combine(Environment.CurrentDirectory, "storage");

        if (!Directory.Exists(storageDirectory))
        {
            AnsiConsole.MarkupLine("\n[red]The FLUX storage folder does not exist yet.[/]");
            return;
        }

        string[] directories = Directory.GetDirectories(storageDirectory, "*", SearchOption.AllDirectories);
        string[] files = Directory.GetFiles(storageDirectory, "*", SearchOption.AllDirectories);

        if (directories.Length == 0 && files.Length == 0)
        {
            AnsiConsole.MarkupLine("\n[red]The FLUX storage folder exists, but it is empty.[/]");
            return;
        }

        AnsiConsole.MarkupLine("\n[cyan]FLUX storage folder contents:[/]");

        foreach (string directory in directories)
        {
            AnsiConsole.MarkupLine($"- [yellow]{directory}[/]");
        }

        foreach (string file in files)
        {
            AnsiConsole.MarkupLine($"- [yellow]{file}[/]");
        }
    }

    static string SanitizePathSegment(string value)
    {
        foreach (char invalid in Path.GetInvalidFileNameChars())
        {
            value = value.Replace(invalid, '_');
        }

        return string.IsNullOrWhiteSpace(value) ? "untitled" : value;
    }

    static string ResolvePath(string input)
    {
        if (Path.IsPathRooted(input))
        {
            return input;
        }

        string combined = Path.Combine(Environment.CurrentDirectory, input);
        return Path.GetFullPath(combined);
    }

    static void RunChat()
    {
        Console.WriteLine("FLUX Local Assistant");
        Console.WriteLine("Type 'exit' to leave chat.");

        while (true)
        {
            Console.Write("Chat> ");
            string input = (Console.ReadLine() ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nChat closed.");
                return;
            }

            string reply = GetChatReply(input);
            Console.WriteLine($"\nFLUX Assistant: {reply}");
        }
    }

    static string GetChatReply(string input)
    {
        if (TryGenerateModelReply(input, out string modelReply))
        {
            return modelReply;
        }

        return GetLocalReply(input);
    }

    static bool TryGenerateModelReply(string input, out string reply)
    {
        reply = string.Empty;

        try
        {
            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(8);

            var payload = new
            {
                model = AppSettings.ModelName,
                prompt = $"You are FLUX, a friendly local shell assistant. Answer plainly and briefly.\n\nUser: {input}\n\nAnswer:",
                stream = false
            };

            string json = JsonSerializer.Serialize(payload);
            using StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("http://localhost:11434/api/generate", content).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            string body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            using JsonDocument document = JsonDocument.Parse(body);
            if (document.RootElement.TryGetProperty("response", out JsonElement responseElement))
            {
                reply = responseElement.GetString()?.Trim() ?? string.Empty;
                return !string.IsNullOrWhiteSpace(reply);
            }
        }
        catch
        {
            return false;
        }

        return false;
    }

    static string GetLocalReply(string input)
    {
        string lower = NormalizeConversationText(input);

        if (TryEvaluateSimpleMath(input, out string mathAnswer))
        {
            return mathAnswer;
        }

        if (lower == "k" || lower == "okay" || lower == "alright")
        {
            return "Got it. What would you like FLUX to do next?";
        }

        if (lower.Contains("you know what") || lower.Contains("ykw"))
        {
            return "I’m following you. Ask me about notes, storage, commands, desktop, or the system.";
        }

        if (lower.Contains("i do not know") || lower.Contains("idk"))
        {
            return "That’s okay — FLUX can still help with notes, commands, storage, desktop mode, and basic math.";
        }

        if (lower.Contains("bruh") || lower.Contains("bro"))
        {
            return "I’m here. What would you like to do in FLUX?";
        }

        if (lower.Contains("to be honest") || lower.Contains("tbh"))
        {
            return "Fair enough — tell me what you need from FLUX and I’ll help.";
        }

        if (IsSystemQuestion(lower))
        {
            return GetSystemInfoReply();
        }

        if (lower.Contains("hello") || lower.Contains("hi"))
        {
            return "Hello! I’m FLUX’s local assistant. Ask me about notes, help, desktop, or commands.";
        }

        if (lower.Contains("read") && lower.Contains("file"))
        {
            return "I can read from the FLUX storage folder: " + Path.Combine(Environment.CurrentDirectory, "storage");
        }

        if ((lower.Contains("read") && (lower.Contains("note") || lower.Contains("storage"))) || lower.Contains("show notes"))
        {
            return ReadStorageNotes();
        }

        if (lower.Contains("note") || lower.Contains("notes"))
        {
            return "Use the 'note' command to save a note, and the 'notes' command to view saved notes.";
        }

        if (lower.Contains("help"))
        {
            return "Use the 'help' command to see the full FLUX command list.";
        }

        if (lower.Contains("system") || lower.Contains("os") || lower.Contains("environment"))
        {
            return GetSystemInfoReply();
        }

        if (lower.Contains("desktop"))
        {
            return "The 'desktop' command opens the lightweight desktop-style launcher.";
        }

        if (lower.Contains("clear"))
        {
            return "Use the 'clear' command if you want to clear the terminal screen.";
        }

        if (lower.Contains("what can you do") || lower.Contains("commands"))
        {
            return "I can help explain the FLUX shell, notes, desktop, help, and the basic command flow.";
        }

        return "I’m in fallback mode right now. I can answer FLUX-specific questions, and simple arithmetic can be handled by the 'calc' command. A local model server on http://localhost:11434/api/generate is needed for normal conversational AI replies.";
    }

    static bool IsSystemQuestion(string normalizedText)
    {
        return normalizedText.Contains("system")
            || normalizedText.Contains("what is this thing")
            || normalizedText.Contains("what is this")
            || normalizedText.Contains("what is this system")
            || normalizedText.Contains("what is the system")
            || normalizedText.Contains("environment")
            || normalizedText.Contains("os");
    }

    static string NormalizeConversationText(string input)
    {
        string normalized = input.ToLowerInvariant();

        string[] replacements = new[]
        {
            "ykw", "you know what",
            "idk", "i do not know",
            "bruh", "bruh",
            "tbh", "to be honest",
            "u", "you",
            "r", "are",
            "pls", "please",
            "omw", "on my way"
        };

        for (int i = 0; i < replacements.Length; i += 2)
        {
            normalized = normalized.Replace(replacements[i], replacements[i + 1]);
        }

        normalized = new string(normalized.Where(ch => char.IsLetterOrDigit(ch) || char.IsWhiteSpace(ch)).ToArray());
        normalized = string.Join(" ", normalized.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));

        return normalized;
    }

    static bool TryEvaluateSimpleMath(string input, out string answer)
    {
        answer = string.Empty;

        string cleaned = input;
        foreach (char invalid in Path.GetInvalidFileNameChars())
        {
            cleaned = cleaned.Replace(invalid, ' ');
        }

        cleaned = cleaned.Replace("what is", "")
                         .Replace("calculate", "")
                         .Replace("plus", "+")
                         .Replace("minus", "-")
                         .Replace("times", "*")
                         .Replace("divided by", "/")
                         .Replace("multiply", "*")
                         .Replace("subtract", "-")
                         .Replace("add", "+")
                         .Replace("equals", "");

        cleaned = new string(cleaned.Where(ch => char.IsDigit(ch) || "+-*/(). ".Contains(ch)).ToArray()).Trim();

        if (string.IsNullOrWhiteSpace(cleaned) || cleaned.Count(ch => char.IsDigit(ch)) < 1)
        {
            return false;
        }

        try
        {
            double result = Convert.ToDouble(new DataTable().Compute(cleaned, string.Empty));
            answer = $"The value is {result}.";
            return true;
        }
        catch
        {
            return false;
        }
    }

    static string ReadStorageNotes()
    {
        string storageDirectory = Path.Combine(Environment.CurrentDirectory, "storage");

        if (!Directory.Exists(storageDirectory))
        {
            return "I checked the FLUX storage folder and it does not exist yet.";
        }

        string[] directories = Directory.GetDirectories(storageDirectory, "*", SearchOption.AllDirectories);
        string[] files = Directory.GetFiles(storageDirectory, "*", SearchOption.AllDirectories);

        if (directories.Length == 0 && files.Length == 0)
        {
            return "The FLUX storage folder exists, but it does not contain any files yet.";
        }

        string summary = "I found these storage entries in the FLUX storage folder:\n";
        foreach (string directory in directories)
        {
            summary += "- " + directory + Environment.NewLine;
        }

        foreach (string file in files)
        {
            summary += "- " + file + Environment.NewLine;
        }

        string notesDirectory = Path.Combine(storageDirectory, "notes");
        if (Directory.Exists(notesDirectory))
        {
            string[] noteFolders = Directory.GetDirectories(notesDirectory);
            foreach (string noteFolder in noteFolders.Take(3))
            {
                string noteFile = Path.Combine(noteFolder, "note.txt");
                if (File.Exists(noteFile))
                {
                    string[] lines = File.ReadAllLines(noteFile);
                    if (lines.Length > 0)
                    {
                        summary += Environment.NewLine + "Saved note preview for " + Path.GetFileName(noteFolder) + ":\n" + string.Join(Environment.NewLine, lines.Take(3));
                    }
                }
            }
        }

        return summary;
    }

    static string GetSystemInfoReply()
    {
        string os = Environment.OSVersion.ToString();
        string user = Environment.UserName;
        string cwd = Environment.CurrentDirectory;
        string runtime = Environment.Version.ToString();

        return $"FLUX is running in '{cwd}'. The current user is '{user}'. Operating system: '{os}'. Runtime version: '{runtime}'.";
    }

    static void RunLaunchCommand()
    {
        Console.Write("Enter launch mode [app/file]: ");
        string mode = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();

        if (mode != "app" && mode != "file")
        {
            AnsiConsole.MarkupLine("\n[red]Use either 'app' or 'file'.[/]");
            return;
        }

        Console.Write("Enter the target: ");
        string target = (Console.ReadLine() ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(target))
        {
            AnsiConsole.MarkupLine("\n[red]No target was entered.[/]");
            return;
        }

        if (mode == "app")
        {
            if (TryLaunchKnownCommand(target))
            {
                AnsiConsole.MarkupLine($"\n[green]Launching app:[/] [yellow]{target}[/]");
                return;
            }

            AnsiConsole.MarkupLine($"\n[red]Could not launch app:[/] [yellow]{target}[/] because it was not found in the current PATH.");
            return;
        }

        if (TryOpenFileWithDefaultApp(target))
        {
            AnsiConsole.MarkupLine($"\n[green]Opening file:[/] [yellow]{target}[/]");
            return;
        }

        AnsiConsole.MarkupLine($"\n[red]Could not open file:[/] [yellow]{target}[/]");
    }

    static bool TryLaunchKnownCommand(string command)
    {
        string? pathVar = Environment.GetEnvironmentVariable("PATH");
        if (string.IsNullOrWhiteSpace(pathVar))
        {
            return false;
        }

        foreach (string rawPath in pathVar.Split(Path.PathSeparator))
        {
            if (string.IsNullOrWhiteSpace(rawPath))
            {
                continue;
            }

            string candidate = Path.Combine(rawPath, command);
            if (!File.Exists(candidate))
            {
                continue;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = candidate,
                    UseShellExecute = true
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        return false;
    }

    static bool TryOpenFileWithDefaultApp(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return false;
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });

            return true;
        }
        catch
        {
            return false;
        }
    }

    static void ShowChannelInfo()
    {
        try
        {
            AnsiConsole.Write(new Panel("[yellow]itoldyounow - YouTube Channel[/]").BorderColor(Color.Yellow));
            AnsiConsole.MarkupLine("A space built for gaming, coding, and tech experiments.");
            AnsiConsole.MarkupLine("[cyan]so go check it out at https://www.youtube.com/@itoldyounow[/]");
            AnsiConsole.MarkupLine("\nType [green]help[/] or [green]desktop[/] to continue using FLUX.");
        }
        catch
        {
            Console.WriteLine("\nThe channel info screen could not be displayed properly. Try again.");
        }
    }

    static void RunDesktopShell()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select a system tool to execute (Use Arrow Keys):[/]")
                    .AddChoices(new[]
                    {
                        "Help",
                        "Info",
                        "Theme",
                        "Clock",
                        "Standard Calculator",
                        "Scientific Calculator",
                        "Notepad",
                        "Write Note",
                        "Saved Notes",
                        "View File",
                        "View Storage",
                        "Chat",
                        "ASCII Draw",
                        "YouTube Info",
                        "System Information",
                        "Back to Commands"
                    }));

            if (choice == "Help")
            {
                ShowHelp();
            }
            else if (choice == "Info")
            {
                ShowInfo();
            }
            else if (choice == "Theme")
            {
                RunThemeCommand();
            }
            else if (choice == "Clock")
            {
                RunClock();
            }
            else if (choice == "Standard Calculator")
            {
                RunCalculator();
            }
            else if (choice == "Scientific Calculator")
            {
                RunScientificCalculator();
            }
            else if (choice == "Notepad")
            {
                RunNotepad();
            }
            else if (choice == "Write Note")
            {
                RunNotepad();
            }
            else if (choice == "Saved Notes")
            {
                ShowSavedNotes();
            }
            else if (choice == "View File")
            {
                RunOpenFile();
            }
            else if (choice == "View Storage")
            {
                ShowStorageInfo();
            }
            else if (choice == "Chat")
            {
                RunChat();
            }
            else if (choice == "ASCII Draw")
            {
                RunDrawTool();
            }
            else if (choice == "YouTube Info")
            {
                ShowChannelInfo();
            }
            else if (choice == "System Information")
            {
                AnsiConsole.MarkupLine("\n[cyan]FLUX OS Desktop Style 1.0.0[/]");
                AnsiConsole.MarkupLine("Running Spectre.Console UI and .NET Architecture.");
                AnsiConsole.MarkupLine("Press [green]enter[/] to return to the desktop workspace...");
                Console.ReadLine();
            }
            else if (choice == "Back to Commands")
            {
                return;
            }
        }
    }

}