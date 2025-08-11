namespace TicketSystem;

public class TicketManager
{
    public static List<Ticket> tickets = new List<Ticket>();

    public static void CreateTicket(string title, string description, bool isClosed = false)
    {
        Ticket newTicket = new Ticket(title, description, isClosed);
        tickets.Add(newTicket);
    }

    public static List<Ticket> GetTickets()
    {
        SortTicketsByStatus();
        return new List<Ticket>(tickets);
    }

    public static void CloseTicket(int index)
    {
        if (index >= 0 && index < tickets.Count)
            tickets[index] = tickets[index] with { IsClosed = true };
        else
            throw new ArgumentOutOfRangeException("Invalid ticket index.");
    }

    public static void DeleteTicket(int index)
    {
        if (index >= 0 && index < tickets.Count)
            tickets.RemoveAt(index);

        else
            throw new ArgumentOutOfRangeException("Invalid ticket index.");
    }

    public static async Task SaveTicketsToFileAsync(string? filePath)
    {
        await FileWriter.WriteToJsonAsync(tickets, filePath);
    }

    public static void SortTicketsByStatus()
    {
        List<Ticket> newTickets = new List<Ticket>();
        for (int i = 0; i < tickets.Count; i++)
        {
            if (tickets[i].IsClosed)
                newTickets.Add(tickets[i]);

            else
                newTickets.Insert(0, tickets[i]);
        }

        // Expanded from what i would've done in the real world, with linq:
        // tickets = tickets.OrderBy(ticket => ticket.IsClosed).ToList();
    }
}