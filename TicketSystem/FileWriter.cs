namespace TicketSystem;

internal class FileWriter
{
    public static void WriteToJson(List<Ticket> tickets, string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = "tickets.json"; // default if no path is provided
        }
        try
        {
            string json = System.Text.Json.JsonSerializer.Serialize(tickets);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }

    public static void WriteToTxt(List<Ticket> tickets, string? filePath)
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
                    writer.WriteLine($"Title: {ticket.Title}");
                    writer.WriteLine($"Description: {ticket.Description}");
                    writer.WriteLine($"Is Closed: {ticket.IsClosed}");
                    writer.WriteLine(new string('-', 20));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }

    public static void WriteToTxtWithoutStreamWriter(List<Ticket> tickets, string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = "tickets_weird.txt"; // default if no path is provided
        }
        foreach(var ticket in tickets)
        {
            try
            {
                File.AppendAllText(filePath, $"Title: {ticket.Title}\n");
                File.AppendAllText(filePath, $"Description: {ticket.Description}\n");
                File.AppendAllText(filePath, $"Is Closed: {ticket.IsClosed}\n");
                File.AppendAllText(filePath, new string('-', 20) + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
    }
}
