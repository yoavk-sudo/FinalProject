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
                Console.ReadKey(true);
                Console.CursorVisible = true;
            }
        }

        public static void ClearAllDataStructures()
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
    }
}
