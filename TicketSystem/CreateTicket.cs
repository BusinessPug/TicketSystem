namespace TicketSystem;

internal class CreateTicket
{
    public static void CreateTicketScreen()
    {
        if (TicketManager.tickets.Count >= 5)
        {
            Console.Clear();
            Console.WriteLine("Ticket limit reached. Please delete or close a ticket before creating a new one.");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        ConsoleHelpers.HashLineDarkBlue();
        Console.WriteLine("Create Ticket");
        ConsoleHelpers.HashLineDarkBlue();
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Description: ");
        string description = Console.ReadLine();
        TicketManager.CreateTicket(title, description);
    }
}
