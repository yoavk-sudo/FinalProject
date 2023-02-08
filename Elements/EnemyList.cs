using System;
using System.Numerics;

namespace FinalProject.Elements
{
    internal static class EnemyList
    {
        static List<Enemy> _enemies = new List<Enemy>();
        static HashSet<Enemy> _movingEnemies = new HashSet<Enemy>();
        static public Enemy? EnemyByCoordinates(int[] cor)
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy.Coordinates.SequenceEqual(cor)) return enemy;
            }
            return null;
        }
        static public void AddToList(Enemy enemy)
        {
            _enemies.Add(enemy);
        }
        static public void RemoveFromList(Enemy enemy)
        {
            LockMethods.SetCursorLockAndOneSpace(enemy.Coordinates);
            Map.ZeroCoordinate(Map.MapCol, enemy.Coordinates);
            enemy.HP = 0;
            _enemies.Remove(enemy);
            _movingEnemies.Remove(enemy);
        }
        public static void TerminateAllEnemies()
        {
            int num = _enemies.Count;
            for (int i = 0; i < num; i++)
            {
                RemoveFromList(_enemies[0]);
            }
        }
        public static async void AddEnemiesToMoveList(Enemy enemy, Player player)
        {
            _movingEnemies.Add(enemy);
            AsyncActivateEnemy(enemy, player);
        }

        private static async void AsyncActivateEnemy(Enemy enemy, Player player)
        {
            while (enemy.IsAlive())
            {
                int x = Math.Abs(enemy.Coordinates[0] - player.Coordinates[0]);
                int y = Math.Abs(enemy.Coordinates[1] - player.Coordinates[1]);
                if (x == 0 && y == Enemy.MELEERANGE)
                {
                    Enemy.EnemyAttack(enemy, player);
                    await Task.Delay(2500);
                }
                else if (x == Enemy.MELEERANGE && y == 0)
                {
                    Enemy.EnemyAttack(enemy, player);
                    await Task.Delay(2500);
                }
                else
                {
                    EnemyMove(enemy, player);
                    await Task.Delay(1500);
                }
            }
        }

        private static void EnemyMove(Enemy enemy, Player player)
        {
            PrintEnemyAtNewLocation(enemy, player.Coordinates);
        }

        private static void PrintEnemyAtNewLocation(Enemy enemy, int[] coordinates)
        {
            if (coordinates[0] < enemy.Coordinates[0])
            {
                if (Map.WhatIsInNextTile(enemy.Coordinates, new int[]{-1, 0}) == 1) return;
                LockMethods.SetCursorLockAndOneSpace(enemy.Coordinates);
                LockPrintEnemy(enemy, -1, 0);
                return;
            }
            else if (coordinates[0] > enemy.Coordinates[0])
            {
                if (Map.WhatIsInNextTile(enemy.Coordinates, new int[]{1, 0}) == 1) return;
                LockMethods.SetCursorLockAndOneSpace(enemy.Coordinates);
                LockPrintEnemy(enemy, 1, 0);
                return;
            }
            // equal on x axis, now checking and moving on y axis
            else if (coordinates[1] < enemy.Coordinates[1])
            {
                if (Map.WhatIsInNextTile(enemy.Coordinates, new int[]{0, -1}) == 1) return;
                LockMethods.SetCursorLockAndOneSpace(enemy.Coordinates);
                LockPrintEnemy(enemy, 0, -1);
                return;
            }
            else if (coordinates[1] > enemy.Coordinates[1])
            {
                if (Map.WhatIsInNextTile(enemy.Coordinates, new int[]{0, 1}) == 1) return;
                LockMethods.SetCursorLockAndOneSpace(enemy.Coordinates);
                LockPrintEnemy(enemy, 0, 1);
                return;
            }
        }

        private static void LockPrintEnemy(Enemy enemy, int x, int y)
        {
            lock (LockMethods.ActionLock)
            {
                Map.ZeroCoordinate(Map.MapCol, enemy.Coordinates);
                enemy.Coordinates[0] = enemy.Coordinates[0] + x;
                enemy.Coordinates[1] = enemy.Coordinates[1] + y;
                Console.SetCursorPosition(enemy.Coordinates[0], enemy.Coordinates[1]);
                Console.ForegroundColor = ConsoleColor.Red;
                if(enemy.Type == "bat") Console.Write(Enemy.Avatar);
                else if (enemy.Type == "troll") Console.Write(Enemy.TrollAvatar);
                Map.OneCoordinate(Map.MapCol, enemy.Coordinates);
                Console.ResetColor();
            }
        }

        public static void ActivateEnemiesInRange(Player player)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                int x = Math.Abs(_enemies[i].Coordinates[0] - player.Coordinates[0]);
                int y = Math.Abs(_enemies[i].Coordinates[1] - player.Coordinates[1]);
                if (x <= Enemy.MAXRANGE && y <= Enemy.MAXRANGE)
                {
                    AddEnemiesToMoveList(_enemies[i], player);
                }
            }
        }
    }
}
