using FinalProject.Keys;

namespace FinalProject.Menus
{
    internal static class MainMenu
    {
        static private bool _save = true;
        const int STARTIGNPOS = 55;
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
            Console.SetCursorPosition(STARTIGNPOS, 8);
            Console.WriteLine("1. New Game");
            if (!File.Exists("Saves.txt"))
            {
                _save = false;
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            Console.SetCursorPosition(STARTIGNPOS, 9);
            Console.WriteLine("2. Load Game");
            Console.ResetColor();
            Console.SetCursorPosition(STARTIGNPOS, 10);
            Console.WriteLine("3. Settings");
            Console.SetCursorPosition(STARTIGNPOS, 11);
            Console.WriteLine("4. Synopsis");
            Console.SetCursorPosition(STARTIGNPOS, 12);
            Console.WriteLine("5. Credits");
            Console.SetCursorPosition(STARTIGNPOS, 13);
            Console.WriteLine("6. Exit Game");
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
            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    CharacterCreation.NewCharacter();
                    return;
                case 2:
                    if (!_save)
                    {
                        Audio.playAudio("Error.wav");
                        Console.Clear();
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("No existing save file found");
                        DisplayMainMenu();
                        return;
                    }
                    break;
                case 3:
                    Console.Clear();
                    SettingsMenu();
                    DisplayMainMenu();
                    return;
                case 4:
                    Console.Clear();
                    Console.SetCursorPosition(STARTIGNPOS, 3);
                    Console.WriteLine("Synopsis:");
                    Console.SetCursorPosition(0, 5);
                    Console.WriteLine("" +
                        "As an admirer of magic, you have decided to enroll in the best magic school in the world:\n" +
                        "School for Talented Intellectuals Learning Teleportation, Arcane and Nature magic.\n" +
                        "Or TILTAN for short.\n\n" +
                        "After a grueling but incredibly enjoyable first semester, you find yourself before your first exam in PROGRAMMING.\n" +
                        "PROGRAMMING of course is also an acronym for something quite magical,\n" +
                        "but you already forgot what it stands for.\n" +
                        "For PROGRAMMING, you have arrived at the entrance of a cave. Your teacher speaks...\n\n" +
                        "\"Before you lies the Cave of Knowledge. Within lie many a treasure, challenges and StackOverflow trolls.\n" +
                        "Enter the cave and brave the dangers within, and you will exit as wizards in your own right.\n" +
                        "Good luck.\"\n\n" +
                        "Hearing the encouraging words, you steel yourself before entering the cave and beginning your adventure..."
                        );
                    Console.ReadKey();
                    Console.Clear();
                    DisplayMainMenu(); 
                    return;
                case 5:
                    Console.Clear();
                    Console.SetCursorPosition(STARTIGNPOS, 3);
                    Console.WriteLine("CREDITS\n" +
                        "\t\tGame Designer:\n\n\t\t\tYoav Kendler\n\n" +
                        "\t\tLead Developer:\n\n\t\t\tYoav Kendler\n\n" +
                        "\t\tLead Art:\n\n\t\t\tYoav Kendler (and various ascii art websites found online)\n\n" +
                        "\t\tMusic:\n\n\t\t\tConsole.Beep()\n\n" +
                        "\t\tLecturer:\n\n\t\t\tDor Ben Dor");
                    Console.ReadKey();
                    Console.Clear();
                    DisplayMainMenu();
                    return;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    DisplayMainMenu();
                    return;
            }
        }

        private static void SettingsMenu()
        {
            Console.WriteLine("1. Change player avatar");
            Console.WriteLine("2. Change enemy avatar");
            Console.WriteLine("3. Change Controls");
            Console.WriteLine("4. Reset Controls to default");
            Console.WriteLine("5. Return to main menu");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    Console.Clear();
                    PlayerAvatarMenu();
                    DisplayMainMenu();
                    break;
                case 2:
                    Console.Clear();
                    EnemyAvatarMenu();
                    DisplayMainMenu();
                    break;
                case 3:
                    Console.Clear();
                    Controls.NewControls();
                    Console.Clear();
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
        }

        private static void PlayerAvatarMenu()
        {
            char[] avatars = { '▲', '@', '©', '☻', '☺' };
            Console.WriteLine("Change the enemy avatar to 1 of the following:");
            Console.WriteLine("1. ▲ ▼ ◄ ►(Default)");
            Console.WriteLine("2. @");
            Console.WriteLine("3. ©");
            Console.WriteLine("4. ☻");
            Console.WriteLine("5. ☺");
            Console.WriteLine("The direction you are facing is important for various actions, so the default option is recommended.");
            bool isValid = int.TryParse(Console.ReadLine(), out int choice);
            if (!isValid || choice <= 0 || choice > 5)
            {
                Console.Clear();
                Console.WriteLine("Choice was invalid");
                Player.avatar = '▲';
                DisplayMainMenu();
                return;
            }
            Player.avatar = avatars[choice - 1];
            Console.Clear();
        }

        private static void EnemyAvatarMenu()
        {
            char[] avatars = { '¤', '¢', '*', '&', 'x', 'Þ' };
            Console.WriteLine("Change the enemy avatar to 1 of the following:");
            Console.WriteLine("1. ¤ (Default)");
            Console.WriteLine("2. ¢");
            Console.WriteLine("3. *");
            Console.WriteLine("4. &");
            Console.WriteLine("5. ×");
            Console.WriteLine("6. Þ");
            bool isValid = int.TryParse(Console.ReadLine(), out int choice);
            if (!isValid || choice <= 0 || choice > 6)
            {
                Console.Clear();
                Console.WriteLine("Choice was invalid");
                Enemy.avatar = '¤'; 
                DisplayMainMenu();
                return;
            }
            Enemy.avatar = avatars[choice - 1];
            Console.Clear();
        }
    }
}
