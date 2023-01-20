namespace FinalProject
{
    internal class Inventory
    {
        List<object> items;
        public Inventory()
        {

        }
        public static string[] InventoryDisplay()
        {
            int x = 5, y = 20;
            int size = 5;
            string[] slots = new string[size];
            for (int i = 0; i < size; i++)
            {

                Console.SetCursorPosition(x, y);
                Console.Write("---");
                Console.SetCursorPosition(x - 1, y + 1);
                Console.Write("| ");
                Console.Write("¡");
                Console.Write(" |");
                // Console.SetCursorPosition(x, y + 1);
                //Console.Write(" ");
                Console.SetCursorPosition(x, y + 2);
                Console.Write("---");
                Console.SetCursorPosition(x + 2, y);
                x += 5;
                //Console.SetCursorPosition(0, 0);
            }
            return slots;
        }
    }
}
