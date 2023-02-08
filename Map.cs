﻿using FinalProject.Elements;
using FinalProject.Keys;
using FinalProject.Menus;
using System.Numerics;

namespace FinalProject
{
    internal static class Map
    {
        static int _lvlNum = 1;
        static string LevelPath = MainMenu.Path + "\\Level_0" + LevelNumber + ".txt";
        public static int LevelNumber {
            get { return _lvlNum; }
            set {
                _lvlNum = value;
                LevelPath = MainMenu.Path + "\\Level_0" + _lvlNum + ".txt";
            } 
        }
        static char[] _bgElemnts = { ' ', '|','-', '╔', '╚', '╗', '╝', '▲' };
        public static int[,] MapCol = CollisionMap(LevelPath);
        public static int LowestTile = File.ReadAllLines(LevelPath).GetLength(0);
        public static bool IsAlive = true;
        private static bool _isAlreadyRunning = false;

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
                        Console.Write(Player.Avatar);
                        Console.ResetColor();
                        continue;
                    }
                    if (!_bgElemnts.Contains(tile)) CharacterToLogic(tile);
                    else Console.Write(tile);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            if (_isAlreadyRunning) return;
            while (IsAlive)
            {
                _isAlreadyRunning = true;
                InputStream.Interperter(player);
            }
            LevelNumber = 1;
            GameOver.GameOverDisplay();
        }
        public static async void PrintShop(Player player)
        {
            PrintUI(player);
            string[] lines = File.ReadAllLines(LevelPath);
            Console.SetCursorPosition(0, 0);
            foreach (string line in lines)
            {
                foreach (char tile in line)
                {
                    if (tile == '►')
                    {
                        player.Coordinates[0] = Console.GetCursorPosition().Left;
                        player.Coordinates[1] = Console.GetCursorPosition().Top;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('►');
                        Console.ResetColor();
                        continue;
                    }
                    if (!_bgElemnts.Contains(tile)) CharacterToLogic(tile);
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(tile);
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
        private static void PrintUI(Player player)
        {
            HUD.DisplayHUD(player);
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
            int[,] collision = new int[maxLength, height];
            foreach(string line in File.ReadAllLines(levelName))
            {
                if (j >= height) break;
                foreach(char tile in line)
                {
                    if (i >= maxLength) break;
                    int[] cor = { i, j };
                    if (tile == ' ' || tile == '▲' || tile == 'E' || tile == '#' || tile == '►') ZeroCoordinate(collision, cor);//collision[i, j] = 0;
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
            Log.PrintMessage("Entered floor " + LevelNumber + "...", ConsoleColor.DarkYellow);
            Log.PrintControls();
            if (LevelNumber != 6)
            {
                LowestTile = File.ReadAllLines(LevelPath).GetLength(0);
                MapCol = CollisionMap(LevelPath);
                PrintMap(player);
            }
            else ShopLevel(player);
        }

        private static void ShopLevel(Player player)
        {
            LevelPath = MainMenu.Path + "\\Shop.txt";
            LowestTile = File.ReadAllLines(LevelPath).GetLength(0);
            MapCol = CollisionMap(LevelPath);
            PrintShop(player);
            LevelPath = MainMenu.Path + "\\Level_06.txt";
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
        private static void UpdateLevelPath()
        {
            _lvlNum++;
            LevelPath = "Level_0" + LevelNumber + ".txt";
        }
        private static void CharacterToLogic(char ele)
        {
            switch (ele)
            {
                case 'ð': //enemy
                    Console.ForegroundColor = ConsoleColor.Red;
                    Enemy en = Enemy.CreateEnemy("troll");
                    en.Coordinates[0] = Console.CursorLeft;
                    en.Coordinates[1] = Console.CursorTop;
                    break;
                case '¤': //enemy
                    Console.ForegroundColor = ConsoleColor.Red;
                    en = Enemy.CreateEnemy("bat");
                    en.Coordinates[0] = Console.CursorLeft;
                    en.Coordinates[1] = Console.CursorTop;
                    ele = Enemy.Avatar;
                    break;
                case '¶': //key
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    ElementsList.AddToList(ele);
                    Console.Write(ele);
                    Console.ResetColor(); 
                    return;
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
                case '§': //teleport spell
                case 'î': //Superior wand
                case 'Õ': //Health gem
                case 'ƒ': //Swift amulet
                    Console.ForegroundColor = ConsoleColor.DarkYellow; 
                    ElementsList.AddToList(ele);
                    break;
            }
            //if (LevelNumber == 6) Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(ele);
            Console.ResetColor();
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
        public static void ClearCollisionMap()
        {
            for (int i = 0; i < MapCol.GetLength(0); i++)
            {
                for (int j = 0; j < MapCol.GetLength(1); j++)
                {
                    ZeroCoordinate(MapCol, new int[] { i, j });
                }
            }
        }
    }
}
