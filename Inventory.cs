namespace FinalProject
{
    internal class Inventory
    {
        static char[] items = {' ', ' ', ' ', ' ', ' '};
        public Inventory()
        {

        }
        public static string[] InventoryDisplay()
        {
            int x = 5, y = Map.LowestTile + 3;
            int size = 5;
            string[] slots = new string[size];
            for (int i = 0; i < size; i++)
            {
                lock (LockMethods.ActionLock)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("---");
                    Console.SetCursorPosition(x - 1, y + 1);
                    Console.Write("| ");
                    if (items[i] == 'ß') Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(items[i]);
                    Console.ResetColor();
                    Console.Write(" |");
                    Console.SetCursorPosition(x, y + 2);
                    Console.Write("---");
                    Console.SetCursorPosition(x + 2, y);
                    x += 6;
                }
            }
            return slots;
        }
        public static void AddToInventory(char item)
        {
            switch (item)
            {
                case 'ß': //HP Potion
                    items[4] = 'ß';
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
