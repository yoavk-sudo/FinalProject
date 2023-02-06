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
                    if (items[i] == '¡') Console.ForegroundColor = ConsoleColor.Cyan;
                    if (items[i] == 'Â') Console.ForegroundColor = ConsoleColor.Cyan;
                    if (items[i] == 'ß') Console.ForegroundColor = ConsoleColor.Green;
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
                case 'ß': //HP Potion
                    items[7] = 'ß';
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
