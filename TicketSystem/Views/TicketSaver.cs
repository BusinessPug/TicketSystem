namespace TicketSystem.Views;

internal class TicketSaver
{
    public static async Task SaveTicketsToFileScreen()
    {
        Console.Clear();
        
        ConsoleHelpers.HashLineDarkBlue(); // ###
        ConsoleHelpers.WriteWithRainbow("Save Tickets to File");
        ConsoleHelpers.HashLineDarkBlue(); // ###
        
        Console.Write("Enter file path (or press Enter for default 'tickets.json'): ");
        string? filePath = Console.ReadLine();
        
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = "tickets.json"; // default if no path is provided
        }
        
        await TicketManager.SaveTicketsToFileAsync(filePath);
        
        Console.WriteLine($"Tickets saved to {filePath}.");
        await Task.Delay(2000); // Wait for 2 seconds to let the user see the message
    }
}
