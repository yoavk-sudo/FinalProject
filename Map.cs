using FinalProject.Elements;
using FinalProject.Keys;
using FinalProject.Magic;
using FinalProject.Menus;

namespace FinalProject
{
    internal static class Map
    {
        static int _levelNum = 1;
        static string LevelPath = MainMenu.Path + "\\Level_0" + _levelNum + ".txt";
        static char[] _bgElemnts = { ' ', '|','-', '╔', '╚', '╗', '╝', '▲' };
        public static int[,] MapCol = CollisionMap(LevelPath);
        public static int LowestTile = File.ReadAllLines(LevelPath).GetLength(0);
        public static bool IsAlive = true;
        public static int LevelNumber
        {
            get { return _levelNum; }
        }

        /*
         * Create 2 maps, 1 visible, the second is a 2 dimensional array
         * the array will represent whether the contents of each tile is traversable
         * i.e. walls will be '1', or non-traversable
         * Enemies will also have an exception for being non-traversable through their own coordinates
         * Enemies also can't traverse through walls etc.
        */

        public static async void PrintMap(Player player)
        {
            PrintUI(player);
            string[] lines = File.ReadAllLines(LevelPath);
            Console.SetCursorPosition(0, 0);
            foreach (string line in lines)
            {
                foreach (char tile in line)
                {
                    if (tile == '▲')
                    {
                        player.Coordinates[0] = Console.GetCursorPosition().Left;
                        player.Coordinates[1] = Console.GetCursorPosition().Top;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(Player.avatar);
                        Console.ResetColor();
                        continue;
                    }
                    if (!_bgElemnts.Contains(tile)) CharacterToLogic(tile);
                    else Console.Write(tile);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            while (IsAlive)
            {
                InputStream.Interperter(player);
            }
        }
        private static void PrintUI(Player player)
        {
            HUD.DisplayHUD(player);
            Spells.DisplaySpells();
            Inventory.InventoryDisplay();
            if (!Timer.IsTimerActive)
            {
                Timer.AsyncTimer();
                Timer.IsTimerActive = true;
            }
        }
        public static int[,] CollisionMap(string levelName)
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
                    int[] cor = { i, j };
                    if (tile == ' ' || tile == '▲' || tile == 'E' || tile == '#') ZeroCoordinate(collision, cor);//collision[i, j] = 0;
                    else OneCoordinate(collision, cor);//collision[i, j] = 1;
                    i++;
                }
                j++;
                i = 0;
            }
            return collision;
        }
        public static void LoadNextLevel(Player player)
        {
            Console.Clear();
            UpdateLevelPath();
            ElementsList.RemoveAllElements();
            LowestTile = File.ReadAllLines(LevelPath).GetLength(0);
            Log.PrintMessage("Entered floor " + LevelNumber + "...", ConsoleColor.DarkYellow);
            Log.PrintControls();
            if (LevelPath != (Directory.GetCurrentDirectory() + "\\Shop.txt"))
            {
                MapCol = CollisionMap(LevelPath);
                PrintMap(player);
            }
            else ShopLevel();
        }

        private static void ShopLevel()
        {
            throw new NotImplementedException();
        }

        public static int WhatIsInNextTile(int[] coordinates, int[] direction)
        {
            try
            {
                return MapCol[coordinates[0] + direction[0], coordinates[1] + direction[1]];
            }
            catch (System.IndexOutOfRangeException)
            {
                Console.WriteLine("2");
            }
            return -1;
        }
        public static void ZeroCoordinate(int[,] map, int[] cor)
        {
            if (cor == null) return; //Object doesn't exist in element dictionary, therefore cor is null
            map[cor[0], cor[1]] = 0;
        }
        public static void OneCoordinate(int[,] map, int[] cor)
        {
            map[cor[0], cor[1]] = 1;
        }
        public static void TwoCoordinate(int[,] map, int[] cor)
        {
            map[cor[0], cor[1]] = 2;
        }
        private static void CharacterToLogic(char ele)
        {
            switch (ele)
            {
                case 'ð': //enemy
                    Console.ForegroundColor = ConsoleColor.Red;
                    Enemy en = Enemy.CreateEnemy();
                    en.Coordinates[0] = Console.CursorLeft;
                    en.Coordinates[1] = Console.CursorTop;
                    break;
                case '¤': //enemy
                    Console.ForegroundColor = ConsoleColor.Red;
                    en = Enemy.CreateEnemy();
                    en.Coordinates[0] = Console.CursorLeft;
                    en.Coordinates[1] = Console.CursorTop;
                    ele = Enemy.avatar;
                    break;
                case '¶': //key
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    ElementsList.AddToList(ele);
                    break;
                case '█': //door
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    ElementsList.AddToList(ele);
                    break;
                case '¥': //gate
                case '±': //gate2
                case '╩': //lever
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    ElementsList.AddToList(ele);
                    break;
                case '¡': //wand
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    ElementsList.AddToList(ele);
                    break;
                case '▄': //chest
                case '▀': //chest2
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    ElementsList.AddToList(ele);
                    break;
                case '#': //trap
                    Console.ForegroundColor = ConsoleColor.Black;
                    ElementsList.AddToList(ele);
                    break;
                case 'ß': //HP Potion
                    Console.ForegroundColor = ConsoleColor.Green;
                    ElementsList.AddToList(ele);
                    break;
                case 'Â': //Armor
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    ElementsList.AddToList(ele);
                    break;
            }
            Console.Write(ele);
            Console.ResetColor();
        }
        private static void UpdateLevelPath()
        {
            _levelNum++;
            LevelPath = "Level_0" + _levelNum + ".txt";
        }
    }
}
