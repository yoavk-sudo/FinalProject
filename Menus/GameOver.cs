using FinalProject.Elements;
using FinalProject.Keys;

namespace FinalProject.Menus
{
    internal static class GameOver
    {
        public static void PressAnyKeyScreen()
        {
            ClearAllDataStructures();
            Console.Clear();
            lock (LockMethods.ActionLock)
            {
                Console.WriteLine("Press any key to continue");
            } 
        }
        public static void GameOverDisplay(Player? player)
        {
            Console.Clear();
            if (File.Exists(MainMenu.Path + "\\Saves.txt")) File.Delete(MainMenu.Path + "\\Saves.txt");
            lock (LockMethods.ActionLock)
            {
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
                Console.CursorVisible = true;
                Map.LevelNumber = 1;
                GameOverChoice(player);
            }
        }

        private static void ClearAllDataStructures()
        {
            Controls.KeyLayout.Clear();
            Log.ClearControlsLayout();
            Log.ClearLog();
            ElementsList.RemoveAllElements();
            EnemyList.TerminateAllEnemies();
            Map.ClearCollisionMap();
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
