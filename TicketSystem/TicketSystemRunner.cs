namespace TicketSystem;

public class TicketSystemRunner
{
    public static async Task MainMenu()
    {
        Console.Clear();
        while (true)
        {
            Console.Clear();
            HashLine();
            Console.WriteLine("Main Menu");
            HashLine();
            Console.WriteLine("1. Create Ticket");
            Console.WriteLine("2. View Tickets");
            Console.WriteLine("3. Close Ticket");
            Console.WriteLine("4. Delete Ticket");
            Console.WriteLine("5. Save Tickets to File");
            Console.WriteLine("6. Exit");
            HashLine();
            Console.Write("Please select an option (1-6): ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateTicketScreen();
                    break;
                case "2":
                    ViewTicketsScreen();
                    break;
                case "3":
                    await CloseTicketScreen();
                    break;
                case "4":
                    DeleteTicketScreen();
                    break;
                case "5":
                    await SaveTicketsToFileScreen();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    public static async void GetSaveFile()
    {
        // Checks if there are output files in the current directory, and if there are, then ask the user if they want to load a file from there. it should
        // go through all json files in the current directory, and if there are any that contain ticket data, then it will be loaded into a list, and displayed
        // to the user, so they can choose which one to load, if any.
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json");
        List<List<Ticket>> allFound = new List<List<Ticket>>();

        foreach (var file in files)
        {
            List<Ticket> tickets = await FileReader.ReadFromJsonAsync(file);
            if (tickets.Count > 0)
            {
                allFound.Add(tickets);
            }
        }

        if (allFound.Count > 0)
        {
            LoadFilesScreen(allFound);
        }
        else
        {
            return;
        }
    }

    public static void LoadFilesScreen(List<List<Ticket>> allFound)
    {
        Console.Clear();
        HashLine();
        Console.WriteLine("Load Tickets from File");
        HashLine();
        Console.WriteLine("Found the following files with ticket data:");
        for (int i = 0; i < allFound.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {allFound[i].Count} tickets found in file {i + 1}");
            Console.WriteLine(allFound[i]);
        }
        HashLine();
        Console.Write("Press any key to return to the main menu.");
        Console.ReadKey();
    }




    // Views will be SOC'ed out from this class, but for now they will be here.
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
        HashLine();
        Console.WriteLine("Create Ticket");
        HashLine();
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Description: ");
        string description = Console.ReadLine();
        TicketManager.CreateTicket(title, description);
    }

    public static void ViewTicketsScreen()
    {
        Console.Clear();
        HashLine();
        Console.WriteLine("Ticket Overview");
        HashLine();
        var tickets = TicketManager.GetTickets();
        if (tickets.Count == 0)
        {
            Console.WriteLine("No tickets available.");
            HashLine();
            Console.WriteLine("Press any key to return to the main menu.");
        }
        else
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                var ticket = tickets[i];
                Console.WriteLine($"{i + 1}. {ticket.Title} - {(ticket.IsClosed ? "Closed" : "Open")}");
            }
            HashLine();
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
        HashLine();
        Console.WriteLine("Ticket Details");
        HashLine();
        Console.WriteLine($"Title: {ticket.Title}");
        Console.WriteLine($"Description: {ticket.Description}");
        Console.WriteLine($"Is Closed: {ticket.IsClosed}");
        HashLine();
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }

    public static async Task CloseTicketScreen()
    {
        Console.Clear();
        HashLine();
        Console.WriteLine("Close Ticket");
        HashLine();
        var tickets = TicketManager.GetTickets();
        if (tickets.Count == 0)
        {
            Console.WriteLine("No tickets available to close.");
            HashLine();
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            return;
        }
        for (int i = 0; i < tickets.Count; i++)
        {
            var ticket = tickets[i];
            Console.WriteLine($"{i + 1}. {ticket.Title} - {(ticket.IsClosed ? "Closed" : "Open")}");
        }
        HashLine();
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
            HashLine();
            Console.WriteLine("Invalid input. Press any key to return to the main menu.");
            Console.ReadKey();
        }
    }

    public static void DeleteTicketScreen()
    {
        Console.Clear();
        HashLine();
        Console.WriteLine("Delete Ticket");
        HashLine();
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
            Console.WriteLine("Ticket deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid input. Press any key to return to the main menu.");
        }
        Console.ReadKey();
    }

    public static async Task SaveTicketsToFileScreen()
    {
        Console.Clear();
        HashLine();
        Console.WriteLine("Save Tickets to File");
        HashLine();
        Console.Write("Enter file path (or press Enter for default 'tickets.json'): ");
        string? filePath = Console.ReadLine();
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = "tickets.json"; // default if no path is provided
        }
        await TicketManager.SaveTicketsToFileAsync(filePath);
        Console.WriteLine($"Tickets saved to {filePath}.");
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }

    public static void HashLine() => Console.WriteLine("####################");
}