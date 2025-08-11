namespace TicketSystem;

internal class ConsoleHelpers
{
    public static void WriteLineWithColor(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteWithRainbow(string message)
    {
        string[] colors = new string[]
        {
            "Red", "Yellow", "Green", "Cyan", "Blue", "Magenta"
        };
        for (int i = 0; i < message.Length; i++)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colors[i % colors.Length]);
            Console.Write(message[i]);
        }
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void HashLineDarkBlue() => WriteLineWithColor("####################", ConsoleColor.DarkBlue);
}
