namespace FinalProject
{
    internal static class HUD
    {
        const int STARTINGPOS = 55;
        static public void DisplayHUD(Player player)
        {
            lock (LockMethods.ActionLock)
            {
                PrintBorders();
                int x = STARTINGPOS + 3;
                int y = 14;
                string exp = (player.Experience / player.ExpLvlCap * 100).ToString("0");
                PrintHudLine($"{player.PlayerName}", x, ref y, ConsoleColor.White);
                PrintHudLine($"HP:           {player.HP}", x, ref y, ConsoleColor.Green);
                PrintHudLine($"MP:           {player.MP}", x, ref y, ConsoleColor.Blue);
                PrintHudLine($"Power:        {player.Damage}", x, ref y, ConsoleColor.Magenta);
                PrintHudLine($"Defense:      {player.Armor.ArmorDef}", x, ref y, ConsoleColor.Cyan);
                PrintHudLine($"Evasion:      {player.Evasion}", x, ref y, ConsoleColor.White);
                PrintHudLine($"Gold:         {player.Gold}", x, ref y, ConsoleColor.Yellow);
                PrintHudLine($"Level:        {player.Level}", x, ref y, ConsoleColor.DarkYellow);
                PrintHudLine($"EXP:          {exp}%", x, ref y, ConsoleColor.Cyan);
                Console.ResetColor();
            }
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
            Console.SetCursorPosition(STARTINGPOS, 12);
            Console.Write("╔═════════════════════╗");
            for (int i = 0; i < 12; i++)
            {
                Console.SetCursorPosition(STARTINGPOS, 13 + i);
                Console.Write("║                     ║");
            }
            Console.SetCursorPosition(STARTINGPOS, 24);
            Console.Write("╚═════════════════════╝");
        }
    }
}
