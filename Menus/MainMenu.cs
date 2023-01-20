using FinalProject.Keys;

namespace FinalProject.Menus
{
    internal static class MainMenu
    {
        static private bool _save = true;
        static public void DisplayMainMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(50, 1);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.SetCursorPosition(50, 2);
            Console.WriteLine("* THE MAGICIAN'S QUEST *");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.ResetColor();
            Console.WriteLine();
            Console.SetCursorPosition(55, 8);
            Console.WriteLine("1. New Game");
            if (!File.Exists("Saves.txt"))
            {
                _save = false;
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            Console.SetCursorPosition(55, 9);
            Console.WriteLine("2. Load Game");
            Console.ResetColor();
            Console.SetCursorPosition(55, 10);
            Console.WriteLine("3. Settings");
            Console.SetCursorPosition(55, 11);
            Console.WriteLine("4. Synopsis");
            Console.SetCursorPosition(55, 12);
            Console.WriteLine("5. Exit Game");
            MainMenuChoice();
        }
        static public void MainMenuChoice()
        {
            Console.WriteLine();
            bool isValid = int.TryParse(Console.ReadLine() ,out int choice);
            if(!isValid)
            {
                Console.Clear();
                DisplayMainMenu();
                return;
            }
            //int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    CharacterCreation.NewCharacter();
                    break;
                case 2:
                    if (!_save)
                    {
                        Audio.playAudio("Error.wav");
                        Console.Clear();
                        Console.SetCursorPosition(0, 15);
                        Console.WriteLine("No existing save file found");
                        DisplayMainMenu();
                        //Console.Clear();
                        //Console.WriteLine("Didn't find saves on local machine");
                        //DisplayMainMenu();
                        break;
                    }
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("1. Change player avatar");
                    Console.WriteLine("2. Change ???");
                    Console.WriteLine("3. Change Controls");
                    Console.WriteLine("4. Reset Controls to default");
                    Console.WriteLine("5. Return to main menu");
                    switch(int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            Console.Clear();
                            Controls.NewControls();
                            DisplayMainMenu();
                            break;
                        case 4:
                            File.Delete("Custom_Controls.txt");
                            Console.Clear();
                            DisplayMainMenu();
                            break;
                        case 5:
                            Console.Clear();
                            DisplayMainMenu();
                            break;
                        default:
                            Console.Clear();
                            DisplayMainMenu();
                            break;
                    }
                    Controls.NewControls();
                    DisplayMainMenu();
                    break;
                case 4:
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    DisplayMainMenu();
                    return;
            }
        }
    }
}
