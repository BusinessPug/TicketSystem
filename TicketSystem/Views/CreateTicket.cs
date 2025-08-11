namespace TicketSystem.Views;

internal class CreateTicket
{
    public static void CreateTicketScreen()
    {
        if (TicketManager.tickets.Count >= 5)
        {
            Console.Clear();

            ConsoleHelpers.HashLineDarkBlue(); // ###
            ConsoleHelpers.WriteLineWithColor("Ticket limit reached. Please delete or close a ticket before creating a new one.", ConsoleColor.Red);
            ConsoleHelpers.HashLineDarkBlue(); // ###
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        
        ConsoleHelpers.HashLineDarkBlue(); // ###
        ConsoleHelpers.WriteWithRainbow("Create Ticket");
        ConsoleHelpers.HashLineDarkBlue(); // ###
        
        Console.Write("Title: ");
        string title = Console.ReadLine();
        
        Console.Write("Description: ");
        string description = Console.ReadLine();
        
        TicketManager.CreateTicket(title, description);
    }
}
