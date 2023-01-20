namespace FinalProject.Menus
{
    internal static class GameOver
    {
        static public void GameOverDisplay()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Game Over");
            Console.WriteLine("*********");
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.WriteLine("1. Main Menu");
            Console.WriteLine("2. Exit Game");
            GameOverChoice();
        }
        static public void GameOverChoice()
        {
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    MainMenu.DisplayMainMenu();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    GameOverChoice();
                    break;
            }
        }
    }
}
