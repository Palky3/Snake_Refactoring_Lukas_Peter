using System;
using System.Collections.Generic;
using System.Threading;
using static System.Console;

namespace Snake
{
    class Program
    {
        static int screenWidth = 32;
        static int screenHeight = 16;
        static int score = 5;
        static bool gameOver = false;

        enum Movement
        {
            Right,
            Left,
            Up,
            Down
        }

        static Movement actualMove = Movement.Right;

        static Random random = new Random();

        static List<int> snakeXPositions = new List<int>();
        static List<int> snakeYPositions = new List<int>();

        static int berryX;
        static int berryY;

        static void Main(string[] args)
        {
            InitializeGame();
            while (!gameOver)
            {
                Draw();
                GetUserInput();
                MoveSnake();
                CheckCollision();
                Thread.Sleep(200);
            }
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
        }

        static void InitializeGame()
        {
            WindowHeight = screenHeight;
            WindowWidth = screenWidth;
            InitializeSnake();
            PlaceBerry();
        }

        static void InitializeSnake()
        {
            snakeXPositions.Clear();
            snakeYPositions.Clear();
            int startX = screenWidth / 2;
            int startY = screenHeight / 2;
            for (int i = 0; i < score; i++)
            {
                snakeXPositions.Add(startX - i);
                snakeYPositions.Add(startY);
            }
        }

        static void Draw()
        {
            Console.Clear();
            DrawWalls();
            DrawSnake();
            DrawBerry();
        }

        static void DrawWalls()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, screenHeight - 1);
                Console.Write("■");
            }
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("■");
            }
        }

        static void DrawSnake()
        {
            // Draw snake body in green
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 1; i < snakeXPositions.Count; i++)
            {
                Console.SetCursorPosition(snakeXPositions[i], snakeYPositions[i]);
                Console.Write("■");
            }
            // Draw snake head in red
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(snakeXPositions[0], snakeYPositions[0]);
            Console.Write("■");
        }

        static void DrawBerry()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(berryX, berryY);
            Console.Write("■");
        }

        static void PlaceBerry()
        {
            berryX = random.Next(1, screenWidth - 2);
            berryY = random.Next(1, screenHeight - 2);
        }

        static void GetUserInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        actualMove = Movement.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        actualMove = Movement.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        actualMove = Movement.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        actualMove = Movement.Right;
                        break;
                }
            }
        }

        static void MoveSnake()
        {
            int newX = snakeXPositions[0];
            int newY = snakeYPositions[0];
            switch (actualMove)
            {
                case Movement.Up:
                    newY--;
                    break;
                case Movement.Down:
                    newY++;
                    break;
                case Movement.Left:
                    newX--;
                    break;
                case Movement.Right:
                    newX++;
                    break;
            }
            snakeXPositions.Insert(0, newX);
            snakeYPositions.Insert(0, newY);
            if (newX == berryX && newY == berryY)
            {
                score++;
                PlaceBerry();
            }
            else
            {
                snakeXPositions.RemoveAt(snakeXPositions.Count - 1);
                snakeYPositions.RemoveAt(snakeYPositions.Count - 1);
            }
        }

        static void CheckCollision()
        {
            int headX = snakeXPositions[0];
            int headY = snakeYPositions[0];
            if (headX == 0 || headX == screenWidth - 1 || headY == 0 || headY == screenHeight - 1)
                gameOver = true;
            for (int i = 1; i < snakeXPositions.Count; i++)
            {
                if (headX == snakeXPositions[i] && headY == snakeYPositions[i])
                {
                    gameOver = true;
                    break;
                }
            }
        }
    }
}
