namespace TicketSystem;

internal class TicketSaver
{
    public static async Task SaveTicketsToFileScreen()
    {
        Console.Clear();
        ConsoleHelpers.HashLineDarkBlue();
        Console.WriteLine("Save Tickets to File");
        ConsoleHelpers.HashLineDarkBlue();
        Console.Write("Enter file path (or press Enter for default 'tickets.json'): ");
        string? filePath = Console.ReadLine();
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = "tickets.json"; // default if no path is provided
        }
        await TicketManager.SaveTicketsToFileAsync(filePath);
        Console.WriteLine($"Tickets saved to {filePath}.");
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }
}
