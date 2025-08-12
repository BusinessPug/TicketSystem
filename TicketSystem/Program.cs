using System.Runtime.InteropServices;
using TicketSystem.Views;

public static class Program
{
    // Structure used by GetWindowRect
    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

    [DllImport("user32.dll")]
    private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

    private const int SW_MAXIMIZE = 3;

    public static async Task Main(string[] args)
    {
        if (OperatingSystem.IsWindows())
        {
            ResizeConsoleWindow();
        }

        await TicketLoader.GetSaveFiles();
        await MainMenu.Menu();
    }

    private static void ResizeConsoleWindow()
    {
        try
        {
            // Get the handle of the console window
            IntPtr consoleWindowHandle = GetForegroundWindow();

            // Maximize the console window
            ShowWindow(consoleWindowHandle, SW_MAXIMIZE);

            // Get the screen size
            if (GetWindowRect(consoleWindowHandle, out Rect screenRect))
            {
                // Resize and reposition the console window to fill the screen
                int width = screenRect.Right - screenRect.Left;
                int height = screenRect.Bottom - screenRect.Top;
                MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width, height, true);
            }
        }
        catch
        {
            // Handle any exceptions silently to avoid crashing
        }
    }
}