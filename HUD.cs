using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace FinalProject
{
    internal static class HUD
    {
        static public void DisplayHUD(Player player)
        {
            PrintBorders();
            int x = 75;
            int y = 14;
            PrintHudLine($"{player.PlayerName}", x, ref y, ConsoleColor.White);
            PrintHudLine($"HP:           {player.HP}", x, ref y, ConsoleColor.Green);
            PrintHudLine($"MP:           {player.MP}", x, ref y, ConsoleColor.Blue);
            PrintHudLine($"Power:        {player.Damage}", x, ref y, ConsoleColor.Magenta);
            PrintHudLine($"Evasion:      {player.Evasion}", x, ref y, ConsoleColor.White);
            PrintHudLine($"Gold:         {player.Gold}", x, ref y, ConsoleColor.Yellow);
            PrintHudLine($"Level:        {player.Level}", x, ref y, ConsoleColor.DarkYellow);
            PrintHudLine($"EXP:          {player.Experience}%", x, ref y, ConsoleColor.Cyan);
            Console.ResetColor();
            Inventory.InventoryDisplay();
            Console.SetCursorPosition(0, 0);
        }
        private static void PrintHudLine(string stat, int x, ref int y, ConsoleColor colour)
        {
            Console.SetCursorPosition(x, y);
            for (int i = 0; i < stat.Length + 1; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = colour;
            Console.WriteLine(stat);
            y++;
        }
        private static void PrintBorders()
        {
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.SetCursorPosition(72, 12);
            Console.Write("╔════════════════════╗");
            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition(72, 13 + i);
                Console.Write("║                    ║");
            }
            Console.SetCursorPosition(72, 23);
            Console.Write("╚════════════════════╝");
        }
        public static void UpdateHUD(Player player, Stats stat)
        {
            //PrintHudLine($"HP:           {player.HP}", 75, (int)stat, ConsoleColor.Green);
            Console.SetCursorPosition(25, (int)stat);
            //player.GetType().GetProperties().
            object statValue = player.GetType().GetProperty((stat).ToString()).GetValue(player);
            string line = $"{stat}:           {statValue}";
            for (int i = 0; i <= line.Length + 6; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(25, (int)stat);
            Console.WriteLine($"{stat}:           {statValue}");
        }
        public enum Stats
        {
            HP = 14,
            MP,
            Power,
            Evasion,
            Gold,
            Level,
            EXP
        }
    }
}
