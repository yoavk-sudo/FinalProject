using FinalProject.Elements;

namespace FinalProject.Menus
{
    internal class WinScreen
    {
        private static string _creditsText = "CREDITS\n" +
                        "\t\tGame Designer:\n\n\t\t\tYoav Kendler\n\n" +
                        "\t\tLead Developer:\n\n\t\t\tYoav Kendler\n\n" +
                        "\t\tLead Art:\n\n\t\t\tYoav Kendler (and various ascii art websites found online)\n\n" +
                        "\t\tMusic:\n\n\t\t\tConsole.Beep()\n\n" +
                        "\t\tNarrative:\n\n\t\t\tYoav Kendler\n\n" +
                        "\t\tLecturer:\n\n\t\t\tDor Ben Dor";
        const int STARTIGNPOS = 55;
        public static void WinGame(Player player)
        {
            Map.IsAlive = false;
            Console.ReadKey(true);
            Console.Clear();
            GameOver.ClearAllDataStructures();
            Console.SetCursorPosition(55, 2);
            Console.WriteLine("You Win!");
            Console.SetCursorPosition(55, 4);
            Console.WriteLine("Score: " + player.Gold * Player.Diff + " :)");
            Console.ReadKey(true);
            CreditsPage();
            System.Environment.Exit(0);
        }
        public static void CreditsPage()
        {
            Console.Clear();
            Console.SetCursorPosition(STARTIGNPOS, 2);
            Console.WriteLine(_creditsText);
            Console.ReadKey();
        }
        public static void SpawnCrown()
        {
            lock (LockMethods.ActionLock)
            {
                int[] cor = new int[] { 7, 1 };
                Console.SetCursorPosition(cor[0], cor[1]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine('W');
                Console.ResetColor();
                ElementsList.AddToList('W', cor);
                Map.OneCoordinate(Map.MapCol, cor);
            }
        }
    }
}
