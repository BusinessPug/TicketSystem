namespace TicketSystem;

internal class AllTickets
{
    public static void ViewTicketsScreen()
    {
        Console.Clear();
        ConsoleHelpers.HashLineDarkBlue();
        Console.WriteLine("Ticket Overview");
        ConsoleHelpers.HashLineDarkBlue();
        var tickets = TicketManager.GetTickets();
        if (tickets.Count == 0)
        {
            Console.WriteLine("No tickets available.");
            ConsoleHelpers.HashLineDarkBlue();
            Console.WriteLine("Press any key to return to the main menu.");
        }
        else
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                var ticket = tickets[i];
                Console.WriteLine($"{i + 1}. {ticket.Title} - {(ticket.IsClosed ? "Closed" : "Open")}");
            }
            ConsoleHelpers.HashLineDarkBlue();
            Console.Write("Enter the ticket number to view details or 'b' to go back: ");
            string input = Console.ReadLine();
            if (input.ToLower() == "b")
            {
                return;
            }
            if (int.TryParse(input, out var ticketNumber))
            {
                ViewTicketDetailsScreen(ticketNumber - 1);
            }
            else
            {
                Console.WriteLine("Invalid input. Press any key to return to the main menu.");
                Console.ReadKey();
            }
        }
    }

    public static void ViewTicketDetailsScreen(int index)
    {
        Console.Clear();
        var tickets = TicketManager.GetTickets();
        if (index < 0 || index >= tickets.Count)
        {
            Console.WriteLine("Invalid ticket number.");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            return;
        }
        var ticket = tickets[index];
        ConsoleHelpers.HashLineDarkBlue();
        Console.WriteLine("Ticket Details");
        ConsoleHelpers.HashLineDarkBlue();
        Console.WriteLine($"Title: {ticket.Title}");
        Console.WriteLine($"Description: {ticket.Description}");
        Console.WriteLine($"Is Closed: {ticket.IsClosed}");
        ConsoleHelpers.HashLineDarkBlue();
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }
}
