using FinalProject.Magic;

namespace FinalProject
{
    internal class Inventory
    {
        static char[] items = {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '};
        public static string[] InventoryDisplay()
        {
            int x = 5, y = Map.LowestTile + 3;
            int size = items.Length;
            string[] slots = new string[size];
            for (int i = 0; i < size; i++)
            {
                lock (LockMethods.ActionLock)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("---");
                    Console.SetCursorPosition(x - 1, y + 1);
                    Console.Write("| ");
                    if (items[i] == 'o') Console.ForegroundColor = Spells.SpellColor('o');
                    else if (items[i] == '+') Console.ForegroundColor = Spells.SpellColor('+');
                    else if (items[i] == '╬') Console.ForegroundColor = Spells.SpellColor('╬');
                    else if (items[i] == '§') Console.ForegroundColor = Spells.SpellColor('§');
                    else if (items[i] == '¡') Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (items[i] == 'Â') Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (items[i] == 'ß') Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(items[i]);
                    Console.ResetColor();
                    Console.Write(" |");
                    Console.SetCursorPosition(x, y + 2);
                    Console.Write("---");
                    Console.SetCursorPosition(x + 2, y);
                    x += 6;
                }
                if (i == 3)
                {
                    y += 4;
                    x -= 24;
                }
            }
            return slots;
        }
        public static void AddToInventory(char item)
        {
            switch (item)
            {
                case 'o': //fireball
                    items[0] = 'o';
                    InventoryDisplay();
                    break;
                case '+': //hell
                    items[1] = '+';
                    InventoryDisplay();
                    break;
                case '╬': //lightning
                    items[2] = '╬';
                    InventoryDisplay();
                    break;
                case '§': //teleport
                    items[3] = '§';
                    InventoryDisplay();
                    break;
                case '¡': //wand
                    items[4] = '¡';
                    InventoryDisplay();
                    break;
                case 'Â': //armor
                    items[5] = 'Â';
                    InventoryDisplay();
                    break;
                case 'ß': //HP Potion
                    items[7] = 'ß';
                    InventoryDisplay();
                    break;
                default:
                    break;
            }
        }
        public static void RemoveFromInventory(char item)
        {
            int i = Array.IndexOf(items, item);
            if (i == -1) return; //return instead of using != so we don't waste time printing the inventory
            items[i] = ' ';
            InventoryDisplay();
        }
        public static bool IsInInventory(char item)
        {
            return items.Contains(item);
        }
    }
}
