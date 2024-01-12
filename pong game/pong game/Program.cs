using System;
using System.Diagnostics;
using System.Threading;

namespace Pong
{
    public class Program
    {
        private const int FieldLength = 50;
        private const int FieldWidth = 15;
        private const char FieldTile = '#';
        private const int RacketLength = 3;
        private const char RacketLine = '|';
        private const char BallTile = 'O';
        private const int ScoreToWin = 10;
        private const int BallSpeed = 100;

        private static int leftRacketHeight = 0;
        private static int rightRacketHeight = 0;
        private static int ballX = FieldLength / 2;
        private static int ballY = FieldWidth / 2;
        private static bool isBallGoingDown = true;
        private static bool isBallGoingRight = true;
        private static int leftPlayerPoints = 0;
        private static int rightPlayerPoints = 0;
        private static int scoreboardX = FieldLength / 2 - 2;
        private static int scoreboardY = FieldWidth + 1;

        private static Stopwatch stopwatch = new Stopwatch();

        public static void Main()
        {
            InitializeGame();
            stopwatch.Start();

            while (leftPlayerPoints < ScoreToWin && rightPlayerPoints < ScoreToWin)
            {
                PrintGame();
                MoveBall();
                CheckBallCollision();
                HandlePlayerInput();
            }

            stopwatch.Stop();
            DisplayWinner();
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
            Console.ReadKey();
        }

        private static void InitializeGame()
        {
            Console.CursorVisible = false;
            ClearScreen();
        }

        private static void ClearScreen()
        {
            Console.Clear();
            DrawBorders();
        }

        private static void DrawBorders()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string(FieldTile, FieldLength));

            Console.SetCursorPosition(0, FieldWidth + 1);
            Console.WriteLine(new string(FieldTile, FieldLength));
        }

        private static void PrintGame()
        {
            Console.SetCursorPosition(0, 0);
            DrawRackets();
            DrawBall();
            DrawScoreboard();
        }

        private static void DrawRackets()
        {
            for (int i = 0; i < RacketLength; i++)
            {
                Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
                Console.WriteLine(RacketLine);
                Console.SetCursorPosition(FieldLength - 1, i + 1 + rightRacketHeight);
                Console.WriteLine(RacketLine);
            }
        }

        private static void DrawBall()
        {
            Console.SetCursorPosition(ballX, ballY);
            Console.WriteLine(BallTile);
        }

        private static void DrawScoreboard()
        {
            Console.SetCursorPosition(scoreboardX, scoreboardY);
            Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
        }

        private static void MoveBall()
        {
            Thread.Sleep(BallSpeed);

            Console.SetCursorPosition(ballX, ballY);
            Console.Write(" ");

            ballY += isBallGoingDown ? 1 : -1;
            ballX += isBallGoingRight ? 1 : -1;
        }

        private static void CheckBallCollision()
        {
            if (ballY == 1 || ballY == FieldWidth)
            {
                isBallGoingDown = !isBallGoingDown;
            }

            if (ballX == 1)
            {
                HandleLeftPlayerCollision();
            }

            if (ballX == FieldLength - 2)
            {
                HandleRightPlayerCollision();
            }
        }

        private static void HandleLeftPlayerCollision()
        {
            if (ballY >= leftRacketHeight + 1 && ballY <= leftRacketHeight + RacketLength)
            {
                isBallGoingRight = !isBallGoingRight;
            }
            else
            {
                HandleScoreAndReset(true);
            }
        }

        private static void HandleRightPlayerCollision()
        {
            if (ballY >= rightRacketHeight + 1 && ballY <= rightRacketHeight + RacketLength)
            {
                isBallGoingRight = !isBallGoingRight;
            }
            else
            {
                HandleScoreAndReset(false);
            }
        }

        private static void HandleScoreAndReset(bool leftPlayerScored)
        {
            if (leftPlayerScored)
            {
                leftPlayerPoints++;
            }
            else
            {
                rightPlayerPoints++;
            }

            ballY = FieldWidth / 2;
            ballX = FieldLength / 2;

            ClearScreen();

            if (leftPlayerPoints == ScoreToWin || rightPlayerPoints == ScoreToWin)
            {
                DisplayWinner();
                Environment.Exit(0);
            }
        }

        private static void HandlePlayerInput()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (rightRacketHeight > 0) rightRacketHeight--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (rightRacketHeight < FieldWidth - RacketLength - 1) rightRacketHeight++;
                        break;

                    case ConsoleKey.W:
                        if (leftRacketHeight > 0) leftRacketHeight--;
                        break;

                    case ConsoleKey.S:
                        if (leftRacketHeight < FieldWidth - RacketLength - 1) leftRacketHeight++;
                        break;
                }
            }
        }

        private static void DisplayWinner()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            if (rightPlayerPoints == ScoreToWin)
            {
                Console.WriteLine("Right player won!");
            }
            else
            {
                Console.WriteLine("Left player won!");
            }
        }
    }
}