using FinalProject.Menus;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        //Console.CursorVisible= false;////////

        //Console.WriteLine(Console.GetCursorPosition().Left);

        // At the start of the game, display main menu -> calls for player's choice
        MainMenu.DisplayMainMenu();
    }
}