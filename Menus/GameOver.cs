using FinalProject.Elements;
using FinalProject.Keys;
using FinalProject.Magic;

namespace FinalProject.Menus
{
    internal static class GameOver
    {
        public static void PressAnyKeyScreen()
        {
            Console.Clear();
            lock (LockMethods.ActionLock)
            {
                ClearAllDataStructures();
                Console.Clear();
                Console.WriteLine("Press any key to continue");
            } 
        }
        public static void GameOverDisplay()
        {
            Console.Clear();
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
                //Map.LevelNumber = 1;
                Map.IsAlive= true;
                GameOverChoice();
            }
        }

        private static void ClearAllDataStructures()
        {
            Log.ClearControlsLayout();
            Log.ClearLog();
            Spells.ResetSpellAcquisition();
            Timer.IsTimerActive = false;
            Controls.KeyLayout.Clear();
            ElementsList.RemoveAllElements();
            EnemyList.TerminateAllEnemies();
            Inventory.RemoveAllFromInventory();
            Map.ClearCollisionMap();
        }

        static public void GameOverChoice()
        {
            bool isValid = int.TryParse(Console.ReadLine(), out int choice);
            if (!isValid)
            {
                Console.Clear();
                GameOverDisplay();
                return;
            }
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    MainMenu.DisplayMainMenu();
                    return;
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    GameOverDisplay();
                    return;
            }
        }
    }
}
