using TicketSystem.Snake;

namespace TicketSystem.Views;

public class MainMenu
{
    public static async Task Menu()
    {
        Console.Clear();
        while (true)
        {
            Console.Clear();
            
            ConsoleHelpers.HashLineDarkBlue(); // ###
            ConsoleHelpers.WriteWithRainbow("Main Menu");
            ConsoleHelpers.HashLineDarkBlue(); // ###
            
            ConsoleHelpers.WriteLineWithColor("1. Create Ticket", ConsoleColor.Green);
            ConsoleHelpers.WriteLineWithColor("2. View Tickets", ConsoleColor.Blue);
            ConsoleHelpers.WriteLineWithColor("3. Close Ticket", ConsoleColor.Yellow);
            ConsoleHelpers.WriteLineWithColor("4. Delete Ticket", ConsoleColor.Red);
            ConsoleHelpers.WriteLineWithColor("5. Save Tickets to File", ConsoleColor.Cyan);
            ConsoleHelpers.WriteLineWithColor("6. Exit", ConsoleColor.Magenta);
            
            ConsoleHelpers.HashLineDarkBlue(); // ###
            
            Console.Write("Please select an option (1-6): ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateTicket.CreateTicketScreen();
                    break;
                case "2":
                    AllTickets.ViewTicketsScreen();
                    break;
                case "3":
                    await CloseTicket.CloseTicketScreen();
                    break;
                case "4":
                    await DeleteTicket.DeleteTicketScreen();
                    break;
                case "5":
                    await TicketSaver.SaveTicketsToFileScreen();
                    break;
                case "6":
                    Exit.ExitScreen();
                    return;
                case "7":
                    await SnakeLoader.LoadSnakeGame();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
}