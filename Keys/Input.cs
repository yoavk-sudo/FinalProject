using FinalProject.Menus;
using System.Diagnostics;

namespace FinalProject.Keys
{
    internal static class InputStream
    {
        static char _up = Controls.KeyLayout["up"];
        static char _left = Controls.KeyLayout["left"];
        static char _down = Controls.KeyLayout["down"];
        static char _right = Controls.KeyLayout["right"];
        static char _interact = Controls.KeyLayout["interact"];
        static char _fireball = Controls.KeyLayout["fireball"];
        static char _heal = Controls.KeyLayout["heal"];
        static char _lightning = Controls.KeyLayout["lightning"];
        static char _teleport = Controls.KeyLayout["teleport"];
        static char _potion = Controls.KeyLayout["potion"];
        static private string KeyInput
        {
            get 
            {
                return Console.ReadKey(true).Key.ToString().ToLower();
            } // get's player input, and turns it into lower case
        }
        static public async void Interperter(Player player)
        {
            char input = KeyInput[0];
            if(input == _up) _ = player.Move('w');
            else if(input == _left) _ = player.Move('a');
            else if (input == _down) _ = player.Move('s');
            else if (input == _right) _ = player.Move('d');
            else if (input == _interact) player.Interact();
            else if (input == _fireball) _ = player.CastSpell('v');
            else if (input == _heal) _ = player.CastSpell('u');
            else if (input == _lightning) _ = player.CastSpell('m');
            else if (input == _teleport) _ = player.CastSpell('t');
            else if (input == _potion) player.UsePotion();
            else if (input == (char)ConsoleKey.Escape) Console.WriteLine("esc");////////////////////////////
        }
    }
}
