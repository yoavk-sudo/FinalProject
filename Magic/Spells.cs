namespace FinalProject.Magic
{
    internal static class Spells
    {
        static FireBall fire = new();
        static Lightning lightning = new();
        static Heal heal = new();
        static int x = 5, y = 17;
        readonly public static dynamic[] spells = new dynamic[] { fire , lightning, heal};
        private static void SetCursorZeros()
        {
            Console.SetCursorPosition(0, 0);
        }
        private static void SetCursorLockAndAction(dynamic spell)
        {
            lock (LockMethods.ActionLock) DrawSpellSlots(spell);
        }
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
        public static void DisplaySpells()
        {
            x = 5; y = 17;
            foreach (var spell in spells)
            {
                SetCursorLockAndAction(spell);
                x += 5;
                Console.ResetColor();
            }
            SetCursorZeros();
        }
        private static void DrawSpellSlots(dynamic spell)
        {
            Console.SetCursorPosition(x, y);
            switch (spell.Charge)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 1:
                    Console.ForegroundColor = spell.LightCol;
                    break;
                case 2:
                    Console.ForegroundColor = spell.Col;
                    break;
            }
            Console.WriteLine(spell.Symbol);
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
        public static async Task ClearSpellSlate()
        {
            SetCursorLockAndClearSlate();
        }
        public static async Task<int> MoveSpell(dynamic spell, int[] coordinates, string direction)
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
                if (Map.WhatIsInNextTile(coordinates, XY) != 0) return -1;
                SetCursorLockAndDrawSpell(spell, coordinates, x, y);
                Thread.Sleep(spd);
            }
            LockMethods.SetCursorLockAndOneSpace(coordinates, x, y);
            return 1;
        }
        public static void SpellHit() //maybe private?
        {
        }
        private static void SpellDelete()
        {

        }
    }
}
