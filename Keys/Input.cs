namespace FinalProject.Keys
{
    internal static class InputStream
    {
        static char up = Controls.KeyLayout["up"];
        static char left = Controls.KeyLayout["left"];
        static char down = Controls.KeyLayout["down"];
        static char right = Controls.KeyLayout["right"];
        static char fireball = Controls.KeyLayout["fireball"];
        static char lightning = Controls.KeyLayout["lightning"];
        static private string KeyInput
        {
            get { return Console.ReadKey(true).Key.ToString().ToLower(); } // get's player input, and turns it into lower case
        }
        static public void Interperter(Player player)
        {
            
            char input = KeyInput[0];
            if(input == up) player.Move('w');
            if(input == left) player.Move('a');
            if(input == down) player.Move('s');
            if(input == right) player.Move('d');
            if(input == fireball) player.CastSpell('v');
            if(input == lightning) player.CastSpell('m');
            if(input == (char)ConsoleKey.Escape) Console.WriteLine("esc");////////////////////////////
        }
    }
}
