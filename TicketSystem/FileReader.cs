namespace TicketSystem;

internal class FileReader
{
    public static async Task<List<Ticket>> ReadFromJsonAsync(string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = "tickets.json";
        }
        
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return new List<Ticket>();
            }
            string json = await File.ReadAllTextAsync(filePath);
            return System.Text.Json.JsonSerializer.Deserialize<List<Ticket>>(json) ?? new List<Ticket>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading from file: {ex.Message}");
            return new List<Ticket>();
        }
    }
}
