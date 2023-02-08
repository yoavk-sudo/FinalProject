using FinalProject.Keys;
using FinalProject.Menus;

namespace FinalProject
{
    internal static class CharacterCreation
    {
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
            Map.MapCol = Map.CollisionMap(MainMenu.Path + "\\Level_01.txt");
            Player player = new(name);
            //Save.SaveGame(player);
            Console.Clear();
            Controls.Setup();
            Log.SetControlsDictionary();
            Map.PrintMap(player);
            return player;
        }
    }
}
