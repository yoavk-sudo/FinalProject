using System.Linq;

namespace FinalProject.Elements
{
    internal static class ElementsList
    {
        static Dictionary<char, int[]> ElementList = new Dictionary<char, int[]>();
        static void LockWriteElementAtCoordinates(char ele, int[] cor, ConsoleColor color)
        {
            lock(LockMethods.ActionLock)
            {
                Console.SetCursorPosition(cor[0], cor[1]);
                Console.ForegroundColor = color;
                Console.Write(ele);
                Console.ResetColor();
                Map.OneCoordinate(Map.MapCol, cor);
            }
        }
        private static bool ContainsCoordinates(int[] cor)
        {
            foreach(int[] value in ElementList.Values)
            {
                if (value.SequenceEqual(cor))
                {
                    return true;
                }
            }
            return false;
        }
        private static char GetKeyByValue(int[] cor)
        {
            foreach (var kvp in ElementList)
            {
                if (kvp.Value.SequenceEqual(cor))
                {
                    return kvp.Key;
                }
            }
            return ' ';
        }
        public static char ElementByCoordinates(int[] cor)
        {
            if (!ContainsCoordinates(cor)) return ' ';//return ' ';
            return GetKeyByValue(cor);
        }
        public static void AddToList(char element)
        {
            int[] cor = { Console.GetCursorPosition().Left, Console.GetCursorPosition().Top };
            ElementList.Add(element, cor);
        }
        public static void RemoveFromList(char element)
        {
            ElementList.Remove(element);
        }
        public static void RemoveAllElements()
        {
            ElementList.Clear();
        }
        static void RemoveFromWorld(char element)
        {
            int[] cor = ElementList.FirstOrDefault(y => y.Key == element).Value;
            if(!(element == '█')) Map.ZeroCoordinate(Map.MapCol, cor);
            else Map.TwoCoordinate(Map.MapCol, cor);
            LockMethods.SetCursorLockAndOneSpace(cor);
            RemoveFromList(element);
        }
        static public void InteractWithElement(int[] cor, Player player)
        {
            char element = ElementByCoordinates(cor);
            switch (element)
            {
                case '¶': //key
                    RemoveFromWorld(element);
                    RemoveFromWorld('█');
                    Log.PrintMessage("Somewhere a door has been unlocked...", ConsoleColor.Cyan);
                    break;
                case '╩': //lever
                    RemoveFromWorld(element);
                    LockWriteElementAtCoordinates('╦', cor, ConsoleColor.DarkMagenta);
                    cor = ElementList.FirstOrDefault(x => x.Key == '±').Value;
                    LockMethods.SetCursorLockAndOneSpace(cor);
                    RemoveFromWorld('±');
                    RemoveFromWorld('¥');
                    Log.PrintMessage("After pulling the lever, a gate has opened", ConsoleColor.Magenta);
                    //Play SFX?
                    break;
                case '¡': //wand
                    RemoveFromWorld(element);
                    Inventory.AddToInventory('¡');//Add to inventory
                    Log.PrintMessage("Got a simple wand!", ConsoleColor.Green); 
                    break;
                case '#': //trap
                    Log.PrintMessage("You screamed in pain as a row of spikes pierce your feet", ConsoleColor.Red);
                    break;
                case '▄':
                    RemoveFromWorld(element);
                    player.GetGold(ChestContentsGenerator());
                    //LockMethods.SetCursorLockAndOneSpace(cor);
                    break;
                case 'ß': //HP Potion
                    RemoveFromWorld(element);
                    Inventory.AddToInventory('ß');//Add to inventory
                    Log.PrintMessage("Got an HP potion!", ConsoleColor.Green);
                    break;
                case 'Â':
                    RemoveFromWorld(element);
                    Inventory.AddToInventory('Â');//Add to inventory
                    Log.PrintMessage("Got a cape! Got 2 additional defense", ConsoleColor.Green); break;
                default:
                    break;
            }
        }
        static int ChestContentsGenerator()
        {
            int gold = Map.LevelNumber * 10 * Random.Shared.Next(1, 6);
            return gold;
        }
    }
}