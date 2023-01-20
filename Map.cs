using FinalProject.Elements;
using FinalProject.Keys;
using FinalProject.Magic;

namespace FinalProject
{
    internal static class Map
    {
        public static int[,] MapCol = CollisionMap(CharacterCreation.LevelPath);
        /*
         * Create 2 maps, 1 visible, the second is a 2 dimensional array
         * the array will represent whether the contents of each tile is traversable
         * i.e. walls will be '0', or non-traversable
         * Enemies will also have an exception for being non-traversable through their own coordinates
         * Enemies also can't traverse through walls etc.
        */

        static public void PrintMap(Player player, string levelName)
        {
            char[] bgElemnts = { ' ', '|','-', '╔', '╚', '╗', '╝', '▲' };
            HUD.DisplayHUD(player);
            Spells.DisplaySpells();
            string[] lines = File.ReadAllLines(levelName);
            foreach (string line in lines)
            {
                foreach (char tile in line)
                {
                    if (tile == '▲')
                    {
                        player.Coordinates[0] = Console.GetCursorPosition().Left;
                        player.Coordinates[1] = Console.GetCursorPosition().Top;
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    if(tile == '¤')
                    {
                        Enemy en = Enemy.CreateEnemy();
                        en.Coordinates[0] = 1;
                        en.Coordinates[1] = 1;
                        en.StartAsyncTask();
                    }
                    else if (tile == '±' || tile == '╩') Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    else if (tile == '¶' || tile == '█') Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (tile == '¡') Console.ForegroundColor = ConsoleColor.DarkCyan;
                    else if (tile == '¤') Console.ForegroundColor = ConsoleColor.Red;
                    foreach(char bgElement in bgElemnts)
                    {
                        if (tile != bgElement) ElementsList.CreateElement(tile);
                    }
                    Console.Write(tile);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(player.Coordinates[0], player.Coordinates[1]);
            while (true)
            {
                InputStream.Interperter(player);
                Console.SetCursorPosition(0, 20);
            }
        }

        static public int[,] CollisionMap(string levelName)
        {
            int maxLength = 0;
            int i = 0, j = 0;
            int height = File.ReadAllLines(levelName).GetLength(0);
            foreach (string line in File.ReadAllLines(levelName))
            {
                if(maxLength < line.Length) maxLength = line.Length;
            }
            int[,] collision = new int[maxLength, height];////////////////////////////////////////
            foreach(string line in File.ReadAllLines(levelName))
            {
                if (j >= height) break;
                foreach(char tile in line)
                {
                    if (i >= maxLength) break;
                    if(tile == ' ' || tile == '▲' || tile == 'E')
                        collision[i, j] = 0;
                    else
                        collision[i, j] = 1;
                    i++;
                }
                j++;
                i = 0;
            }
            return collision;
        }
    }
}
