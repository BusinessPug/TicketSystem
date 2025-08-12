using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Snake
{
    internal class SnakeGame
    {
        // P/Invoke for asynchronous key state detection
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        // the user32 virtual key codes for arrow keys and escape. refer to https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        private const int VK_UP = 0x26;
        private const int VK_DOWN = 0x28;
        private const int VK_LEFT = 0x25;
        private const int VK_RIGHT = 0x27;
        private const int VK_ESCAPE = 0x1B;

        // Grid settings (will be set based on user input)
        private static int GridWidth = 80;
        private static int GridHeight = 25;
        private static char[,] grid;

        // Game settings
        private static int refreshRate = 100; // in milliseconds

        // Snake properties
        private static int snakeLength = 5;
        private static int snakeHeadX;
        private static int snakeHeadY;
        private static List<(int X, int Y)> snakeBody = new List<(int, int)>();

        // Food properties
        private static int foodX;
        private static int foodY;
        private static int foodEaten = 0;
        private static Random random = new Random();

        // Current direction; defaults to right.
        private static ConsoleKey currentKey = ConsoleKey.RightArrow;

        public static async Task Start()
        {
            // Prompt user for grid dimensions and refresh rate.
            Console.Clear();
            Console.CursorVisible = false;
            Console.Write($"Enter grid width ({GridWidth}): ");
            if (!int.TryParse(Console.ReadLine(), out GridWidth))
            {
                GridWidth = 80;
            }
            Console.Write($"Enter grid height ({GridHeight}): ");
            if (!int.TryParse(Console.ReadLine(), out GridHeight))
            {
                GridHeight = 25;
            }
            Console.Write($"Enter refresh rate in milliseconds (e.g., {refreshRate}): ");
            if (!int.TryParse(Console.ReadLine(), out refreshRate))
            {
                refreshRate = 100;
            }

            // Resize the console buffer and window if needed.
            try
            {
                Console.SetWindowSize(Math.Min(GridWidth, Console.LargestWindowWidth), Math.Min(GridHeight + 3, Console.LargestWindowHeight));
                Console.SetBufferSize(Math.Max(Console.BufferWidth, GridWidth), Math.Max(Console.BufferHeight, GridHeight + 3));
            }
            catch { }

            grid = new char[GridWidth, GridHeight];

            // Initialize snake starting position at the center-ish.
            snakeHeadX = GridWidth / 2;
            snakeHeadY = GridHeight / 2;
            snakeLength = 5;
            snakeBody.Clear();
            // Create an initial snake body extending to the left.
            for (int i = 0; i < snakeLength; i++)
            {
                snakeBody.Add((snakeHeadX - i, snakeHeadY));
            }

            InitializeGrid();
            PlaceFood();
            DrawGrid();
            Console.ReadKey(true);
            Thread.Sleep(150); // extra delay to ensure previous key states clear (necessary from testing).
            await GameLoop();
            await Clean();
            Console.CursorVisible = true;
        }

        private static void InitializeGrid()
        {
            // Fill grid with spaces.
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    grid[x, y] = ' ';
                }
            }
            // Clear any previous food.
            grid[foodX, foodY] = ' ';
        }

        private static void PlaceFood()
        {
            do
            {
                foodX = random.Next(0, GridWidth);
                foodY = random.Next(0, GridHeight);
            } while (IsSnakePosition(foodX, foodY));
            // Food is represented by an asterisk.
            grid[foodX, foodY] = '*';
        }

        private static bool IsSnakePosition(int x, int y)
        {
            foreach (var part in snakeBody)
            {
                if (part.X == x && part.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        private static void DrawGrid()
        {
            // Reset cursor to top of console.
            Console.SetCursorPosition(0, 0);
            StringBuilder sb = new StringBuilder();

            // Draw top border.
            sb.AppendLine(new string('#', GridWidth + 2));

            // Draw grid rows with side borders.
            for (int y = 0; y < GridHeight; y++)
            {
                sb.Append('#'); // left border
                for (int x = 0; x < GridWidth; x++)
                {
                    bool drawn = false;
                    // Draw snake head.
                    if (snakeHeadX == x && snakeHeadY == y)
                    {
                        sb.Append('O');
                        drawn = true;
                    }
                    else
                    {
                        // Draw snake body.
                        foreach (var part in snakeBody)
                        {
                            if (!(snakeHeadX == part.X && snakeHeadY == part.Y) && part.X == x && part.Y == y)
                            {
                                sb.Append('o');
                                drawn = true;
                                break;
                            }
                        }
                    }
                    // Draw food if not drawn by snake.
                    if (!drawn)
                    {
                        sb.Append(grid[x, y]);
                    }
                }
                sb.AppendLine("#"); // right border
            }

            // Draw bottom border.
            sb.AppendLine(new string('#', GridWidth + 2));

            sb.Append($"Food eaten: {foodEaten}");
            Console.Write(sb.ToString());
        }

        private static async Task GameLoop()
        {
            bool exitGame = false;
            while (!exitGame)
            {
                ConsoleKey? polledKey = PollKey();
                if (polledKey.HasValue)
                {
                    if (polledKey.Value == ConsoleKey.Escape)
                    {
                        exitGame = true;
                        continue;
                    }
                    else if (!IsOppositeDirection(currentKey, polledKey.Value))
                    {
                        currentKey = polledKey.Value;
                    }
                }

                MoveSnake(currentKey);

                // Check if food is eaten.
                if (snakeHeadX == foodX && snakeHeadY == foodY)
                {
                    foodEaten++;
                    snakeLength++;
                    snakeBody.Insert(0, (snakeHeadX, snakeHeadY));
                    // Remove eaten food.
                    grid[foodX, foodY] = ' ';
                    PlaceFood();
                }
                else
                {
                    snakeBody.Insert(0, (snakeHeadX, snakeHeadY));
                    while (snakeBody.Count > snakeLength)
                    {
                        snakeBody.RemoveAt(snakeBody.Count - 1);
                    }
                }

                // Check collision with self (game over).
                for (int i = 1; i < snakeBody.Count; i++)
                {
                    if (snakeBody[i].X == snakeHeadX && snakeBody[i].Y == snakeHeadY)
                    {
                        exitGame = true;
                        Console.SetCursorPosition(0, GridHeight - 4);
                        Console.WriteLine("Game Over! You collided with yourself.");
                        Console.WriteLine("Press any button to return to the actual task at hand");
                        WaitForFreshKeypress();
                        break;
                    }
                }

                DrawGrid();
                Thread.Sleep(refreshRate);
            }
        }

        static void WaitForFreshKeypress()
        {
            // Drain the buffer so we don't consume a previously pressed key
            while (Console.KeyAvailable)
                Console.ReadKey(true);

            // block until the *next* key the user presses
            Console.ReadKey(true);
        }


        private static ConsoleKey? PollKey()
        {
            // Poll keys using GetAsyncKeyState.
            // Priority order: Escape, Up, Down, Left, Right.
            // 0x8000 indicates the key is currently pressed.
            if ((GetAsyncKeyState(VK_ESCAPE) & 0x8000) != 0)
                return ConsoleKey.Escape;
            if ((GetAsyncKeyState(VK_UP) & 0x8000) != 0)
                return ConsoleKey.UpArrow;
            if ((GetAsyncKeyState(VK_DOWN) & 0x8000) != 0)
                return ConsoleKey.DownArrow;
            if ((GetAsyncKeyState(VK_LEFT) & 0x8000) != 0)
                return ConsoleKey.LeftArrow;
            if ((GetAsyncKeyState(VK_RIGHT) & 0x8000) != 0)
                return ConsoleKey.RightArrow;
            return null;
        }

        private static void MoveSnake(ConsoleKey key)
        {
            // Determine new head position based on current key press.
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    snakeHeadY = Math.Max(0, snakeHeadY - 1);
                    break;
                case ConsoleKey.DownArrow:
                    snakeHeadY = Math.Min(GridHeight - 1, snakeHeadY + 1);
                    break;
                case ConsoleKey.LeftArrow:
                    snakeHeadX = Math.Max(0, snakeHeadX - 1);
                    break;
                case ConsoleKey.RightArrow:
                    snakeHeadX = Math.Min(GridWidth - 1, snakeHeadX + 1);
                    break;
            }
        }

        // Helper method to prevent direct reversal of direction (you can't bite your own neck, so why should the snake?).
        private static bool IsOppositeDirection(ConsoleKey current, ConsoleKey newKey)
        {
            return (current == ConsoleKey.UpArrow && newKey == ConsoleKey.DownArrow) ||
                   (current == ConsoleKey.DownArrow && newKey == ConsoleKey.UpArrow) ||
                   (current == ConsoleKey.LeftArrow && newKey == ConsoleKey.RightArrow) ||
                   (current == ConsoleKey.RightArrow && newKey == ConsoleKey.LeftArrow);
        }
        
        static async Task Clean()
        {
            Console.SetWindowSize(200, 200);
            Console.SetBufferSize(200, 200);
            Console.Clear();
        }
    }
}