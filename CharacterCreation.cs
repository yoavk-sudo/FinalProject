using FinalProject.Keys;
using FinalProject.Menus;

namespace FinalProject
{
    internal static class CharacterCreation
    {
        public static Player? NewCharacter()
        {
            Console.WriteLine("Create a new character (or type \"exit\" to return to main menu)");
            Console.WriteLine("Enter your name:");
            Console.WriteLine("~~~~~~~~~~~~~~~~");
            string name = Console.ReadLine();
            if (name.ToLower() == "exit")
            {
                Console.Clear();
                MainMenu.DisplayMainMenu();
                return null;
            }
            Player player = new(name);
            SetUpGame(player);
            return player;
        }

        private static void SetUpGame(Player player)
        {
            Map.MapCol = Map.CollisionMap(MainMenu.Path + "\\Levels\\Level_01.txt");
            Console.Clear();
            Controls.Setup();
            Log.SetControlsDictionary();
            Map.PrintMap(player);
        }
    }
}
