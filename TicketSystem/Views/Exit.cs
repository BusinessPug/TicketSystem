namespace TicketSystem.Views;

internal class Exit
{
    public static void ExitScreen()
    {
        Console.Clear();
        
        ConsoleHelpers.HashLineDarkBlue(); // ###
        ConsoleHelpers.WriteWithRainbow("Exit Ticket System");
        ConsoleHelpers.HashLineDarkBlue(); // ###

        Console.Write("Would you like to save the current tickets before exiting? (y/N): ");
        string input = Console.ReadLine();
        if (input.ToLower() == "y")
        {
            TicketSaver.SaveTicketsToFileScreen();
        }
    }
}
