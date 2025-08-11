namespace TicketSystem.Views;

internal class AllTickets
{
    public static void ViewTicketsScreen()
    {
        Console.Clear();

        ConsoleHelpers.HashLineDarkBlue(); // ###
        ConsoleHelpers.WriteWithRainbow("Ticket Overview");
        ConsoleHelpers.HashLineDarkBlue(); // ###
        
        var tickets = TicketManager.GetTickets();
        
        if (tickets.Count == 0)
        {
            Console.WriteLine("No tickets available.");
            ConsoleHelpers.HashLineDarkBlue(); // ###
            Console.WriteLine("Press any key to return to the main menu.");
        }
        else
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                var ticket = tickets[i];
                if (i % 2 == 0)
                {
                    ConsoleHelpers.WriteLineWithColor($"{i + 1}. {ticket.Title} - {(ticket.IsClosed ? "Closed" : "Open")}", ConsoleColor.DarkGreen);
                }
                else
                {
                    ConsoleHelpers.WriteLineWithColor($"{i + 1}. {ticket.Title} - {(ticket.IsClosed ? "Closed" : "Open")}", ConsoleColor.DarkCyan);
                }
            }
        
            ConsoleHelpers.HashLineDarkBlue(); // ###
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
                ConsoleHelpers.WriteLineWithColor("Invalid input. Press any key to return to the main menu.", ConsoleColor.Red);
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
        
        ConsoleHelpers.HashLineDarkBlue(); // ###
        ConsoleHelpers.WriteWithRainbow("Ticket Details");
        ConsoleHelpers.HashLineDarkBlue(); // ###
        
        ConsoleHelpers.WriteLineWithColor($"Title: {ticket.Title}", ConsoleColor.Green);
        ConsoleHelpers.WriteLineWithColor($"Description: {ticket.Description}", ConsoleColor.Blue);
        ConsoleHelpers.WriteLineWithColor($"Is Closed: {ticket.IsClosed}", ConsoleColor.Yellow);
        
        ConsoleHelpers.HashLineDarkBlue(); // ###
        
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }
}
