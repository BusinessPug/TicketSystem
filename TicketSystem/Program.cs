using TicketSystem;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await TicketLoader.GetSaveFiles();
        await MainMenu.Menu();
    }
}