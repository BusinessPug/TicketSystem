namespace TicketSystem.Snake;

internal class SnakeLoader
{
    public static SnakeLoader Instance { get; private set; }
    public SnakeLoader()
    {
        Instance = this;
    }

    public static async Task LoadSnakeGame()
    {
        Console.Clear();
        await SnakeGame.Start();
    }
}
