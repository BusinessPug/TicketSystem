using System.Threading.Tasks;

namespace TicketSystem.Views;

internal class DeleteTicket
{
    public static async Task DeleteTicketScreen()
    {
        Console.Clear();
        
        ConsoleHelpers.HashLineDarkBlue(); // ###
        ConsoleHelpers.WriteWithRainbow("Delete Ticket");
        ConsoleHelpers.HashLineDarkBlue(); // ###
        
        var tickets = TicketManager.GetTickets();
        
        if (tickets.Count == 0)
        {
            Console.WriteLine("No tickets available to delete.");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            return;
        }
        
        for (int i = 0; i < tickets.Count; i++)
        {
            var ticket = tickets[i];
            Console.WriteLine($"{i + 1}. {ticket.Title} - {(ticket.IsClosed ? "Closed" : "Open")}");
        }
        
        Console.Write("Enter the ticket number to delete or 'b' to go back: ");
        string input = Console.ReadLine();
        
        if (input.ToLower() == "b")
        {
            return;
        }
        if (int.TryParse(input, out var ticketNumber) && ticketNumber > 0 && ticketNumber <= tickets.Count)
        {
            TicketManager.DeleteTicket(ticketNumber - 1);
            Console.WriteLine("Ticket deleted successfully. Going back to the main menu...");
            await Task.Delay(3000);
        }
        else
        {
            Console.WriteLine("Invalid input. Press any key to return to the main menu.");
            Console.ReadKey();
        }
    }
}
