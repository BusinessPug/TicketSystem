using TicketSystem;

public static class Program
{
    public static void Main(string[] args)
    {
        TicketSystemRunner ticketSystem = TicketSystemRunner.Instance;
        ticketSystem.Run();
    }
}