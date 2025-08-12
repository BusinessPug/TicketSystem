using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace TicketSystem.Snake
{
    internal class SnakeGame
    {
        [DllImport("user32.dll")] private static extern short GetAsyncKeyState(int vKey);

        private const int VK_UP = 0x26, VK_DOWN = 0x28, VK_LEFT = 0x25, VK_RIGHT = 0x27, VK_ESCAPE = 0x1B;

        private static int GridWidth = 80;
        private static int GridHeight = 25;
        private static char[,] grid;

        private static int refreshRate = 100;

        private static int snakeLength = 5;
        private static int snakeHeadX;
        private static int snakeHeadY;
        private static readonly List<(int X, int Y)> snakeBody = new();

        private static int foodX;
        private static int foodY;
        private static int foodEaten = 0;
        private static readonly Random random = new();


        private static ConsoleKey currentKey = ConsoleKey.RightArrow;

        public static async Task Start()
        {
            Console.Clear();
            Console.CursorVisible = false;

            Console.Write($"Enter grid width ({GridWidth}): ");
            if (!int.TryParse(Console.ReadLine(), out GridWidth)) GridWidth = 80;

            Console.Write($"Enter grid height ({GridHeight}): ");
            if (!int.TryParse(Console.ReadLine(), out GridHeight)) GridHeight = 25;

            Console.Write($"Enter refresh rate in milliseconds (e.g., {refreshRate}): ");
            if (!int.TryParse(Console.ReadLine(), out refreshRate)) refreshRate = 100;

            grid = new char[GridWidth, GridHeight];

            snakeHeadX = GridWidth / 2;
            snakeHeadY = GridHeight / 2;
            snakeLength = 5;
            snakeBody.Clear();
            for (int i = 0; i < snakeLength; i++)
                snakeBody.Add((snakeHeadX - i, snakeHeadY));

            InitializeGrid();
            PlaceFood();
            ZoomToScale();
            DrawGrid();
            Console.ReadKey(true);
            Thread.Sleep(150);
            await GameLoop();
            await Clean();
            Console.CursorVisible = true;
        }

        private static void InitializeGrid()
        {
            for (int x = 0; x < GridWidth; x++)
                for (int y = 0; y < GridHeight; y++)
                    grid[x, y] = ' ';
            grid[foodX, foodY] = ' ';
        }

        private static void PlaceFood()
        {
            do
            {
                foodX = random.Next(0, GridWidth);
                foodY = random.Next(0, GridHeight);
            } while (IsSnakePosition(foodX, foodY));
            grid[foodX, foodY] = '*';
        }

        private static bool IsSnakePosition(int x, int y)
        {
            foreach (var part in snakeBody)
                if (part.X == x && part.Y == y) return true;
            return false;
        }

        private static void ZoomToScale()
        {
            // using send input with the combinations CTRL+SW_UP and CTRL+SW_DWN we zoom and check the contents until the window is displaying the grid height, and not exceeding the grid width
        }

        private static void DrawGrid()
        {
            Console.SetCursorPosition(0, 0);
            var sb = new StringBuilder();

            sb.AppendLine(new string('#', GridWidth + 2));

            for (int y = 0; y < GridHeight; y++)
            {
                sb.Append('#');
                for (int x = 0; x < GridWidth; x++)
                {
                    bool drawn = false;

                    if (snakeHeadX == x && snakeHeadY == y)
                    {
                        sb.Append('O');
                        drawn = true;
                    }
                    else
                    {
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

                    if (!drawn) sb.Append(grid[x, y]);
                }
                sb.AppendLine("#");
            }

            sb.AppendLine(new string('#', GridWidth + 2));
            sb.Append($"Food eaten: {foodEaten}");
            Console.Write(sb.ToString());
        }

        private static async Task GameLoop()
        {
            bool exitGame = false;

            while (!exitGame)
            {
                var polledKey = PollKey();
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

                if (snakeHeadX == 0 || snakeHeadX >= GridWidth ||
                    snakeHeadY == 0 || snakeHeadY >= GridHeight)
                {
                    DrawGrid();
                    exitGame = true;
                    Console.SetCursorPosition(0, Math.Max(0, GridHeight - 4));
                    Console.WriteLine("Game Over! You hit the wall.");
                    Console.WriteLine("Press any button to return to the actual task at hand");
                    WaitForFreshKeypress();
                    break;
                }

                if (snakeHeadX == foodX && snakeHeadY == foodY)
                {
                    foodEaten++;
                    snakeLength++;
                    snakeBody.Insert(0, (snakeHeadX, snakeHeadY));
                    grid[foodX, foodY] = ' ';
                    PlaceFood();
                }
                else
                {
                    snakeBody.Insert(0, (snakeHeadX, snakeHeadY));
                    while (snakeBody.Count > snakeLength)
                        snakeBody.RemoveAt(snakeBody.Count - 1);
                }

                for (int i = 1; i < snakeBody.Count; i++)
                {
                    if (snakeBody[i].X == snakeHeadX && snakeBody[i].Y == snakeHeadY)
                    {
                        exitGame = true;
                        Console.SetCursorPosition(0, Math.Max(0, GridHeight - 4));
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

        private static void WaitForFreshKeypress()
        {
            while (Console.KeyAvailable) Console.ReadKey(true);
            Console.ReadKey(true);
        }

        private static ConsoleKey? PollKey()
        {
            if ((GetAsyncKeyState(VK_ESCAPE) & 0x8000) != 0) return ConsoleKey.Escape;
            if ((GetAsyncKeyState(VK_UP) & 0x8000) != 0) return ConsoleKey.UpArrow;
            if ((GetAsyncKeyState(VK_DOWN) & 0x8000) != 0) return ConsoleKey.DownArrow;
            if ((GetAsyncKeyState(VK_LEFT) & 0x8000) != 0) return ConsoleKey.LeftArrow;
            if ((GetAsyncKeyState(VK_RIGHT) & 0x8000) != 0) return ConsoleKey.RightArrow;
            return null;
        }

        private static void MoveSnake(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: snakeHeadY = Math.Max(0, snakeHeadY - 1); break;
                case ConsoleKey.DownArrow: snakeHeadY = Math.Min(GridHeight - 1, snakeHeadY + 1); break;
                case ConsoleKey.LeftArrow: snakeHeadX = Math.Max(0, snakeHeadX - 1); break;
                case ConsoleKey.RightArrow: snakeHeadX = Math.Min(GridWidth - 1, snakeHeadX + 1); break;
            }
        }

        private static bool IsOppositeDirection(ConsoleKey current, ConsoleKey newKey)
        {
            return (current == ConsoleKey.UpArrow && newKey == ConsoleKey.DownArrow) ||
                   (current == ConsoleKey.DownArrow && newKey == ConsoleKey.UpArrow) ||
                   (current == ConsoleKey.LeftArrow && newKey == ConsoleKey.RightArrow) ||
                   (current == ConsoleKey.RightArrow && newKey == ConsoleKey.LeftArrow);
        }

        static async Task Clean()
        {
            try
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);

                currentKey = ConsoleKey.RightArrow;
            }
            catch { }
        }
    }
}