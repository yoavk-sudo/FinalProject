using FinalProject.Magic;

namespace FinalProject
{
    internal class Inventory
    {
        static char[] _items = {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '};
        public static string[] InventoryDisplay()
        {
            int x = 5, y = Map.LowestTile + 3;
            int size = _items.Length;
            string[] slots = new string[size];
            for (int i = 0; i < size; i++)
            {
                lock (LockMethods.ActionLock)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("---");
                    Console.SetCursorPosition(x - 1, y + 1);
                    Console.Write("| ");
                    if (_items[i] == 'o') Console.ForegroundColor = Spells.SpellColor('o');
                    else if (_items[i] == '+') Console.ForegroundColor = Spells.SpellColor('+');
                    else if (_items[i] == '╬') Console.ForegroundColor = Spells.SpellColor('╬');
                    else if (_items[i] == '§') Console.ForegroundColor = Spells.SpellColor('§');
                    else if (_items[i] == '¡') Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (_items[i] == 'î') Console.ForegroundColor = ConsoleColor.DarkYellow;
                    else if (_items[i] == 'Â') Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (_items[i] == 'Æ') Console.ForegroundColor = ConsoleColor.DarkYellow;
                    else if (_items[i] == 'ß') Console.ForegroundColor = ConsoleColor.Green;
                    else if (_items[i] == 'M') Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(_items[i]);
                    Console.ResetColor();
                    Console.Write(" |");
                    Console.SetCursorPosition(x, y + 2);
                    Console.Write("---");
                    Console.SetCursorPosition(x + 2, y);
                    x += 6;
                }
                if (i == 3) // new row after 4 items
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
                    _items[0] = 'o';
                    InventoryDisplay();
                    break;
                case '+': //hell
                    _items[1] = '+';
                    InventoryDisplay();
                    break;
                case '╬': //lightning
                    _items[2] = '╬';
                    InventoryDisplay();
                    break;
                case '§': //teleport
                    _items[3] = '§';
                    InventoryDisplay();
                    break;
                case '¡': //wand
                    _items[4] = '¡';
                    InventoryDisplay();
                    break;
                case 'î': //Superior wand
                    RemoveFromInventory('¡');
                    _items[4] = 'î';
                    InventoryDisplay();
                    break;
                case 'Â': //armor
                    _items[5] = 'Â';
                    InventoryDisplay();
                    break;
                case 'Æ': //armor
                    _items[5] = 'Æ';
                    InventoryDisplay();
                    break;
                case 'M': //manaGen
                    _items[6] = 'M';
                    InventoryDisplay();
                    break;
                case 'ß': //HP Potion
                    _items[7] = 'ß';
                    InventoryDisplay();
                    break;
                default:
                    break;
            }
            InventoryDisplay();
        }
        public static void RemoveFromInventory(char item)
        {
            int i = Array.IndexOf(_items, item);
            if (i == -1) return; //return instead of using != so we don't waste time printing the inventory
            _items[i] = ' ';
            InventoryDisplay();
        }
        public static void RemoveAllFromInventory()
        {
            foreach (var item in _items)
            {
                RemoveFromInventory(item);
            }
        }
        public static bool IsInInventory(char item)
        {
            return _items.Contains(item);
        }
    }
}
