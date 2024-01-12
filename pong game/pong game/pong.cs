namespace Pong
{
    public class pong
    {

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