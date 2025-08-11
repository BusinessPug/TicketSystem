namespace TicketSystem;

internal class TicketManager
{
    private List<Ticket> tickets;

    public TicketManager(List<Ticket> tickets)
    {
        this.tickets = tickets;
    }

    public void CreateTicket(string title, string description, bool isClosed = false)
    {
        Ticket newTicket = new Ticket(title, description, isClosed);
        tickets.Add(newTicket);
    }

    public List<Ticket> ViewTickets()
    {
        // We return a copy of the list to prevent external modification (so it's a mutable copy, not a reference)
        // The mutability in this instance will not be used, but i stand by sending the copy
        return new List<Ticket>(tickets);
    }

    public void CloseTicket(int index)
    {
        if (index >= 0 && index < tickets.Count)
            tickets[index] = tickets[index] with { IsClosed = true };
        else
            throw new ArgumentOutOfRangeException("Invalid ticket index.");
    }

    public void DeleteTicket(int index)
    {
        if (index >= 0 && index < tickets.Count)
            tickets.RemoveAt(index);

        else
            throw new ArgumentOutOfRangeException("Invalid ticket index.");
    }

    public void SaveTicketsToFile(string? filePath)
    {
        FileWriter.WriteToJson(tickets, filePath);
    }

    public void SortTicketsByStatus()
    {
        List<Ticket> newTickets = new List<Ticket>();
        for (int i = 0; i < tickets.Count; i++)
        {
            if (tickets[i].IsClosed)
            {
                newTickets.Add(tickets[i]);
            }
            else
            {
                newTickets.Insert(0, tickets[i]);
            }
        }

        // Expanded from what i would've done in the real world, with linq:
        // tickets = tickets.OrderBy(ticket => ticket.IsClosed).ToList();
    }
}