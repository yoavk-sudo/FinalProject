namespace FinalProject.Menus
{
    internal static class GameOver
    {
        static public void GameOverDisplay(Player? player)
        {
            player = null;
            Map.MapCol = null;
            Console.Clear();
            if (File.Exists(MainMenu.Path + "\\Saves.txt")) File.Delete(MainMenu.Path + "\\Saves.txt");
            Console.SetCursorPosition(52, 2);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("You Died");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("************");
            Console.ResetColor();
            Thread.Sleep(200);
            Console.SetCursorPosition(50, 5);
            Console.WriteLine("1. Main Menu");
            Console.SetCursorPosition(50, 6);
            Console.WriteLine("2. Exit Game");
            Console.CursorVisible= true;
            GameOverChoice(player);
        }
        static public void GameOverChoice(Player? player)
        {
            bool isValid = int.TryParse(Console.ReadLine(), out int choice);
            if (!isValid)
            {
                Console.Clear();
                GameOverDisplay(player);
                return;
            }
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Map.IsAlive= true;
                    MainMenu.DisplayMainMenu();
                    return;
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    GameOverDisplay(player);
                    return;
            }
        }
    }
}
