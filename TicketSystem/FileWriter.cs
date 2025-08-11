namespace TicketSystem;

internal class FileWriter
{
    public static async Task WriteToJsonAsync(List<Ticket> tickets, string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = "tickets.json"; // default if no path is provided
        }
        try
        {
            if (!filePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                filePath += ".json";
            }
            File.Delete(filePath); // delete the file if it exists, to ensure we write a fresh file
            string json = System.Text.Json.JsonSerializer.Serialize(tickets);
            await File.WriteAllTextAsync(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }

    // Not used. but shows how i would write to a txt file instead of a json file.
    public static async Task WriteToTxtAsync(List<Ticket> tickets, string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = "tickets.txt"; // default if no path is provided
        }
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var ticket in tickets)
                {
                    await writer.WriteLineAsync($"Title: {ticket.Title}");
                    await writer.WriteLineAsync($"Description: {ticket.Description}");
                    await writer.WriteLineAsync($"Is Closed: {ticket.IsClosed}");
                    await writer.WriteLineAsync(new string('-', 20));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }

    // Not used, but shows how i would write to a txt file without using StreamWriter.
    public static async Task WriteToTxtWithoutStreamWriterAsync(List<Ticket> tickets, string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = "tickets_nostream.txt"; // default if no path is provided
        }
        foreach(var ticket in tickets)
        {
            try
            {
                await File.AppendAllTextAsync(filePath, $"Title: {ticket.Title}\n");
                await File.AppendAllTextAsync(filePath, $"Description: {ticket.Description}\n");
                await File.AppendAllTextAsync(filePath, $"Is Closed: {ticket.IsClosed}\n");
                await File.AppendAllTextAsync(filePath, new string('-', 20) + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
    }
}
