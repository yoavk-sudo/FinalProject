using FinalProject.Elements;

namespace FinalProject.Menus
{
    internal class WinScreen
    {
        const int STARTIGNPOS = 55;
        private readonly static string _creditsText = "CREDITS\n" +
                        "\t\tGame Designer:\n\n\t\t\tYoav Kendler\n\n" +
                        "\t\tLead Developer:\n\n\t\t\tYoav Kendler\n\n" +
                        "\t\tLead Art:\n\n\t\t\tYoav Kendler\n\n" +
                        "\t\tMusic:\n\n\t\t\tpixabay.com, Console.Beep()\n\n" +
                        "\t\tNarrative:\n\n\t\t\tYoav Kendler\n\n" +
                        "\t\tLecturer:\n\n\t\t\tDor Ben Dor";
        public static void WinGame(Player player)
        {
            Map.IsAlive = false;
            GameOver.ClearAllDataStructures();
            Console.Clear();
            Console.ReadKey(true);
            Audio.WinMusic();
            Console.SetCursorPosition(STARTIGNPOS, 2);
            Console.WriteLine("You Win!");
            Console.SetCursorPosition(STARTIGNPOS, 4);
            Console.WriteLine("Score: " + player.Gold * Player.Diff * 100 + " :)");
            Console.ReadKey(true);
            CreditsPage();
            Environment.Exit(0);
        }
        public static void CreditsPage()
        {
            Console.Clear();
            Console.SetCursorPosition(STARTIGNPOS, 2);
            Console.WriteLine(_creditsText);
            Console.ReadKey(true);
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
