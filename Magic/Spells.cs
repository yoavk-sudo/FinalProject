using FinalProject.Elements;

namespace FinalProject.Magic
{
    internal static class Spells
    {
        public static bool IsSpellTerminate = false;
        static FireBall _fire = new();
        static Heal _heal = new();
        static Teleport _teleport = new();
        static Lightning _lightning = new();
        readonly public static dynamic[] SpellsArray = new dynamic[] { _fire , _heal, _lightning, _teleport };
        public static ConsoleColor SpellColor(char symbol)
        {
            foreach (var item in SpellsArray)
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
            foreach(dynamic spell in SpellsArray)
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
            foreach (var spell in SpellsArray)
            {
                if (spell.Charge < 2)
                {
                    spell.Charge++;
                }
            }
            Inventory.InventoryDisplay();
        }
        public static async Task<int> MoveSpell(dynamic spell, int[] coordinates, string direction, Player player)
        {
            int x = 0, y = 0;
            int spd = (int)Math.Round(1000 * spell.Speed);
            for (int i = 0; i < spell.Range; i++)
            {
                if (IsSpellTerminate)
                {
                    await TerminateSpell(coordinates, x, y);
                    return 2;
                }
                if (i == 0) Thread.Sleep(spd);
                LockMethods.SetCursorLockAndOneSpace(coordinates, x, y);
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
                    if (EnemyList.EnemyByCoordinates(coordinates) is var enWithin && enWithin != null)
                    {
                        if(player.DealDamage(enWithin, spell.Power + player.Damage)) return 1;
                        if (!enWithin.IsAlive()) return 0;
                        EnemyList.AddEnemiesToMoveList(enWithin, player);
                    }
                    else if (EnemyList.EnemyByCoordinates(inFront) is var enemyFront && enemyFront != null)
                    {
                        player.DealDamage(enemyFront, spell.Power + player.Damage);
                        if (!enemyFront.IsAlive()) return 0;
                        EnemyList.AddEnemiesToMoveList(enemyFront, player);
                    }
                }
                if (Map.WhatIsInNextTile(coordinates, XY) != 0) return -1;
                SetCursorLockAndDrawSpell(spell, coordinates, x, y);
                Thread.Sleep(spd);
            }
            LockMethods.SetCursorLockAndOneSpace(coordinates, x, y);
            return 1;
        }
        public static void ResetSpellAcquisition()
        {
            for (int i = 0; i < SpellsArray.Length; i++)
            {
                SpellsArray[i].IsAcquired = false;
            }
        }
        public static void TeleportSpell(Player player, int x, int y)
        {
            if (Map.WhatIsInNextTile(player.Coordinates, new int[] { x, y }) != 0) return;
            LockMethods.SetCursorLockAndOneSpace(player.Coordinates);
            char avatar = Player.Avatar;
            if (x > 0)
            {
                for (int i = 0; i < Teleport.Range; i++)
                {
                    if (Map.WhatIsInNextTile(player.Coordinates, new int[] { 1, 0 }) != 0) break; //stop early if hit wall
                    player.Coordinates[0]++;
                }
                if (avatar == '▲') avatar = '►';
            }
            else if (x < 0)
            {
                for (int i = Teleport.Range; i > 0; i--)
                {
                    if (Map.WhatIsInNextTile(player.Coordinates, new int[] { -1, 0 }) != 0) break; //stop early if hit wall
                    player.Coordinates[0]--;
                    if (avatar == '▲') avatar = '◄';
                }
            }
            else if (y > 0) 
            {
                for (int i = 0; i < Teleport.Range; i++)
                {
                    if (Map.WhatIsInNextTile(player.Coordinates, new int[] { 0, 1 }) != 0) break; //stop early if hit wall
                    player.Coordinates[1]++;
                    if (avatar == '▲') avatar = '▼';
                }
            }
            else if (y < 0) 
            {
                for (int i = Teleport.Range; i > 0; i++)
                {
                    if (Map.WhatIsInNextTile(player.Coordinates, new int[] { 0, -1 }) != 0) break; //stop early if hit wall
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
        private static async Task TerminateSpell(int[] coordinates, int x, int y)
        {
            LockMethods.SetCursorLockAndOneSpace(coordinates); //Erase Spell
            LockMethods.SetCursorLockAndOneSpace(coordinates, x, y);
            IsSpellTerminate = false;
            return;
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
    }
}
