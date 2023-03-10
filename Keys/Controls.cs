namespace FinalProject.Keys
{
    internal class Controls
    {
        public static Dictionary<string, char> KeyLayout = new Dictionary<string, char>();
        private static string _defaultFile = "Default_Controls.txt";
        private static string _customFile = "Custom_Controls.txt";
        public static void Setup()
        {
            CreateDefaultControls();
            string[] controls = File.ReadAllLines(_defaultFile);
            if (File.Exists(_customFile))
            {
                //checks if the custom file has all needed keys
                if(File.ReadAllLines(_customFile).Length == File.ReadAllLines(_defaultFile).Length)
                    controls = File.ReadAllLines(_customFile);
            }
            foreach(string control in controls)
            {
                KeyLayout.Add(control.Split(' ')[0], control.Last());
            }
        }
        public static void CreateDefaultControls()
        {
            if (File.Exists(_defaultFile)) return;
            File.AppendAllLines(_defaultFile, new string[] 
            { 
                "up w",
                "right d",
                "down s",
                "left a",
                "interact e",
                "fireball v",
                "heal u",
                "lightning m",
                "teleport t",
                "potion p"
            });
        }
        public static void NewControls()
        {
            try
            {
                Console.WriteLine("Enter a key for moving up (default \'w\'):");
                char up = char.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Enter a key for moving right (default \'d\'):");
                char right = char.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Enter a key for moving down (default \'s\'):");
                char down = char.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Enter a key for moving left (default \'a\'):");
                char left = char.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Enter a key for interacting (default \'e\'):");
                char act = char.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Enter a key for using fireball (default \'v\'):");
                char fireball = char.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Enter a key for using heal (default \'u\'):");
                char heal = char.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Enter a key for using lightning (default \'m\'):");
                char lightning = char.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Enter a key for using teleport (default \'t\'):");
                char teleport = char.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Enter a key for using potion (default \'p\'):");
                char potion = char.Parse(Console.ReadLine());
                HashSet<char> inputs = new HashSet<char>{ up, right, down, left, act, fireball, heal, lightning, teleport, potion };
                if (inputs.Count != File.ReadAllLines(_defaultFile).Length)
                {
                    Console.WriteLine("\nYou have repeated keys. \nPlease enter enter new, non-repeating keys:");
                    NewControls();
                    return;
                }
                string[] lines =
                {
                    "up " + up,
                    "right " + right,
                    "down " + down,
                    "left " + left,
                    "interact " + act,
                    "fireball " + fireball,
                    "heal " + heal,
                    "lightning " + lightning,
                    "teleport " + teleport,
                    "potion " + potion
                };
                File.AppendAllLines(_customFile, lines);
                KeyLayout.Clear();
                Console.Clear();
            }
            catch (FormatException)
            {
                Console.WriteLine("The input was not a single character.");
                NewControls();
                return;
            }
        }
    }
}
