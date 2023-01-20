using FinalProject.Keys;
using FinalProject.Menus;

namespace FinalProject
{
    internal static class CharacterCreation
    {
        public static string LevelPath = "Level_00.txt";
        static public Player NewCharacter()
        {
            Console.WriteLine("Create a new character (or type \"exit\" to return to main menu)");
            Console.WriteLine("Enter your name:");
            Console.WriteLine("~~~~~~~~~~~~~~~~");
            string name = Console.ReadLine();
            if(name.ToLower() == "exit")
            {
                Console.Clear();
                MainMenu.DisplayMainMenu();
                return null;
            }    
            Player player = new(name);
            Save.SaveGame(player);
            Console.Clear();
            Controls.Setup();
            Map.PrintMap(player, LevelPath);
            
            return player;
        }
    }
}
