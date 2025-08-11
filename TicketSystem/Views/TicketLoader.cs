namespace TicketSystem.Views;

internal class TicketLoader
{
    public static async Task GetSaveFiles()
    {
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json");
        List<List<Ticket>> allFound = new List<List<Ticket>>();

        foreach (var file in files)
        {
            List<Ticket> tickets = await FileReader.ReadFromJsonAsync(file);
            if (tickets.Count > 0)
            {
                allFound.Add(tickets);
            }
        }

        if (allFound.Count > 0)
        {
            List<string> fileNames = files.Select(Path.GetFileName).ToList();
            await LoadFilesScreen(allFound, fileNames);
        }
        else
        {
            // return as we have no tickets to load, so the program will "start fresh"
            return;
        }
    }

    public static async Task LoadFilesScreen(List<List<Ticket>> allFound, List<string> fileNames)
    {
        Console.Clear();
        
        ConsoleHelpers.HashLineDarkBlue(); // ###
        ConsoleHelpers.WriteWithRainbow("Load Tickets from File");
        ConsoleHelpers.HashLineDarkBlue(); // ###
        
        ConsoleHelpers.WriteLineWithColor("Found the following files with ticket data:", ConsoleColor.Green);
        for (int i = 0; i < allFound.Count; i++)
        {
            ConsoleHelpers.WriteLineWithColor($"{i + 1}. {allFound[i].Count} tickets found in file {fileNames[i]}", ConsoleColor.DarkYellow);
        }
        
        ConsoleHelpers.HashLineDarkBlue(); // ###
        
        Console.Write("Enter the number of the file to load or 'c' to continue without loading a savefile: ");
        string input = Console.ReadLine();
        
        if (input.ToLower() == "c")
        {
            return;
        }
        if (int.TryParse(input, out int fileIndex) && fileIndex > 0 && fileIndex <= allFound.Count)
        {
            TicketManager.tickets = allFound[fileIndex - 1];
            return;
        }
        else
        {
            Console.WriteLine("Invalid input. Press any key to return to the main menu.");
        }
    }
}
