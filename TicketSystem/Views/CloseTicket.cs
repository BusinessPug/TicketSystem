namespace TicketSystem.Views;

internal class CloseTicket
{
    public static async Task CloseTicketScreen()
    {
        Console.Clear();
        
        ConsoleHelpers.HashLineDarkBlue(); // ###
        ConsoleHelpers.WriteWithRainbow("Close Ticket");
        ConsoleHelpers.HashLineDarkBlue(); // ###
        
        var tickets = TicketManager.GetTickets();
        
        if (tickets.Count == 0)
        {
            Console.WriteLine("No tickets available to close.");
            ConsoleHelpers.HashLineDarkBlue(); // ###
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            return;
        }
        
        for (int i = 0; i < tickets.Count; i++)
        {
            var ticket = tickets[i];
            Console.WriteLine($"{i + 1}. {ticket.Title} - {(ticket.IsClosed ? "Closed" : "Open")}");
        }
        
        ConsoleHelpers.HashLineDarkBlue(); // ###
        Console.Write("Enter the ticket number to close or 'b' to go back: ");
        string input = Console.ReadLine();
        
        if (input.ToLower() == "b")
        {
            return;
        }
        if (int.TryParse(input, out var ticketNumber) && ticketNumber > 0 && ticketNumber <= tickets.Count)
        {
            TicketManager.CloseTicket(ticketNumber - 1);
            Console.WriteLine("Ticket closed successfully. Redirecting you back to the main menu...");
            await Task.Delay(3000); // Wait for 3 seconds
        }
        else
        {
            ConsoleHelpers.HashLineDarkBlue(); // ###
            Console.WriteLine("Invalid input. Press any key to return to the main menu.");
            Console.ReadKey();
        }
    }
}
