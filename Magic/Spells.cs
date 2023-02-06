using FinalProject.Elements;

namespace FinalProject.Magic
{
    internal static class Spells
    {
        static FireBall fire = new();
        static Heal heal = new();
        static Teleport teleport = new();
        static Lightning lightning = new();
        static int x = 5, y = 17;
        readonly public static dynamic[] spells = new dynamic[] { fire , heal, lightning, teleport };

        private static void SetCursorLockAndClearSlate()
        {
            lock (LockMethods.ActionLock)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("                             ");
            }
        }
        private static void SetCursorLockAndDrawSpell(dynamic spell, int[] coordinates, int x, int y)
        {
            lock (LockMethods.ActionLock)
            {
                Console.SetCursorPosition(coordinates[0] + x, coordinates[1] + y);
                Console.ForegroundColor = spell.Col;
                Console.WriteLine(spell.Symbol);
                Console.ResetColor();
            }
        }
        public static ConsoleColor SpellColor(char symbol)
        {
            foreach (var item in spells)
            {
                if (item.Symbol == symbol && item.Charge == 2) return item.Col;
                if (item.Symbol == symbol && item.Charge == 1) return item.LightCol;
                if (item.Symbol == symbol && item.Charge == 0) return ConsoleColor.White;
            }
            return ConsoleColor.Black;
        }
        public static int FindSpell(Type spellType)
        {
            int i = 0;
            foreach(dynamic spell in spells)
            {
                if(spell.GetType() == spellType)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
        public static void RechargeSpells()
        {
            foreach (var spell in spells)
            {
                if (spell.Charge < 2)
                {
                    spell.Charge++;
                }
            }
        }
        public static async Task<int> MoveSpell(dynamic spell, int[] coordinates, string direction, Player player)
        {   
            int x = 0, y = 0;
            int spd = (int)Math.Round(1000 * spell.Speed);
            for (int i = 0; i < spell.Range; i++)
            {
                if(i == 0) Thread.Sleep(spd);
                LockMethods.SetCursorLockAndOneSpace(coordinates, x, y);// SetCursorLockAndOneSpace(coordinates, x, y);
                switch (direction)
                {
                    case "up":
                        y -= 1;
                        break;
                    case "right":
                        x += 1;
                        break;
                    case "down":
                        y += 1;
                        break;
                    case "left":
                        x -= 1;
                        break;
                    default:
                        break;
                }
                int[] XY = { x, y };
                int[] inFront = { coordinates[0] + x, coordinates[1] + y };
                lock (LockMethods.ActionLock)
                {
                    if (EnemyList.EnemyByCoordinates(inFront) is var enemy && enemy != null)
                    {
                        player.DealDamage(enemy, inFront, spell.Power);
                        if (!enemy.IsAlive()) return 0;
                        EnemyList.AddEnemiesToMoveList(enemy, player);
                    }
                }
                if (Map.WhatIsInNextTile(coordinates, XY) != 0) return -1;
                SetCursorLockAndDrawSpell(spell, coordinates, x, y);
                Thread.Sleep(spd);
            }
            LockMethods.SetCursorLockAndOneSpace(coordinates, x, y);
            return 1;
        }
        public static void TeleportSpell(Player player, int x, int y)
        {
            if (Map.WhatIsInNextTile(player.Coordinates, new int[] { x, y }) == 1) return;
            LockMethods.SetCursorLockAndOneSpace(player.Coordinates);
            char avatar = Player.Avatar;
            if (x > 0)
            {
                for (int i = 0; i < Teleport.Range; i++)
                {
                    if (Map.WhatIsInNextTile(player.Coordinates, new int[] { 1, 0 }) == 1) break;
                    player.Coordinates[0]++;
                }
                if (avatar == '▲') avatar = '►';
            }
            else if (x < 0)
            {
                for (int i = Teleport.Range; i > 0; i--)
                {
                    if (Map.WhatIsInNextTile(player.Coordinates, new int[] { -1, 0 }) == 1) break;
                    player.Coordinates[0]--;
                    if (avatar == '▲') avatar = '◄';
                }
            }
            else if (y > 0) 
            {
                for (int i = 0; i < Teleport.Range; i++)
                {
                    if (Map.WhatIsInNextTile(player.Coordinates, new int[] { 0, 1 }) == 1) break;
                    player.Coordinates[1]++;
                    if (avatar == '▲') avatar = '▼';
                }
            }
            else if (y < 0) 
            {
                for (int i = Teleport.Range; i > 0; i++)
                {
                    if (Map.WhatIsInNextTile(player.Coordinates, new int[] { 0, -1 }) == 1) break;
                    player.Coordinates[1]--;
                }
            }
            lock (LockMethods.ActionLock)
            {
                Console.SetCursorPosition(player.Coordinates[0], player.Coordinates[1]); //position at new coordinates
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(avatar); //print player at new coordinates
                Console.ResetColor();
            }
        }
    }
}
