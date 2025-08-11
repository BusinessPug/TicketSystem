namespace TicketSystem;

internal class TicketSystemRunner
{
    private readonly TicketSystemRunner _ticketSystem;
    private TicketSystemRunner()
    {
        _ticketSystem = this;
    }

    public static TicketSystemRunner Instance { get; } = new TicketSystemRunner();
    public TicketManager TicketManager { get; } = new TicketManager(new List<Ticket>());

    public void Run()
    {
        // This method can be used to run the ticket system, e.g., by providing a menu or command line interface.
        // For now, we will just print a message.
        Console.WriteLine("Ticket System is running. Use TicketManager to manage tickets.");
    }
}
