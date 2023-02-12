using FinalProject.Keys;
using FinalProject.Magic;

namespace FinalProject
{
    internal static class Log
    {
        static string[] logList = {"","","","",""};
        const int STARTPOS = 60;
        const int LASTCHAR = 57;
        const int MAXLENGTH = 50;
        static Dictionary<string, int> _controls = new Dictionary<string, int>();
        static public void PrintMessage(string msg, ConsoleColor color)
        {
            lock (LockMethods.ActionLock)
            {
                int position;
                for (int i = logList.Length; i >= 2; i--)
                {
                    position = 6 - logList.Length + i;
                    logList[i -1] = logList[i - 2];
                    Console.SetCursorPosition(STARTPOS, position);
                    for (int j = 0; j < LASTCHAR; j++)
                    {
                        Console.Write(' ');
                    }
                    Console.SetCursorPosition(STARTPOS, position);
                    Console.WriteLine(logList[i - 1]);
                }
                Console.ForegroundColor = color;
                position = 2;
                logList[0] = msg;
                Console.SetCursorPosition(STARTPOS, position);
                for (int i = 0; i < logList[1].Length; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(STARTPOS, position);
                Console.WriteLine(logList[0]);
                Console.ResetColor();
            }
        }
        public static void SetControlsDictionary()
        {
            _controls.Add($"{Controls.KeyLayout["up"]}: move up", 2);
            _controls.Add($"{Controls.KeyLayout["left"]}: move left", 3);
            _controls.Add($"{Controls.KeyLayout["down"]}: move down", 4);
            _controls.Add($"{Controls.KeyLayout["right"]}: move right", 5);
            _controls.Add($"{Controls.KeyLayout["interact"]}: interact", 6);
            _controls.Add("   or hit enemies", 7);
            _controls.Add($"{Controls.KeyLayout["potion"]}: use potion", 8);
            _controls.Add($"{Controls.KeyLayout["fireball"]}: fireball", 9);
            _controls.Add($"{Controls.KeyLayout["heal"]}: heal", 10);
            _controls.Add($"{Controls.KeyLayout["lightning"]}: lightning", 11);
            _controls.Add($"{Controls.KeyLayout["teleport"]}: teleport", 12);
            PrintControls();
            PrintMessage("Entered floor 1...", ConsoleColor.DarkYellow);
            Console.ResetColor();
        }
        public static void PrintControls()
        {
            lock (LockMethods.ActionLock)
            {
                foreach (var item in _controls)
                {
                    Console.SetCursorPosition(STARTPOS - 20, item.Value);
                    if (item.Value == 9 && Spells.SpellsArray[0].IsAcquired == false) continue;
                    if (item.Value == 10 && Spells.SpellsArray[1].IsAcquired == false) continue;
                    if (item.Value == 11 && Spells.SpellsArray[2].IsAcquired == false) continue;
                    if (item.Value == 12 && Spells.SpellsArray[3].IsAcquired == false) continue;
                    if (item.Value == 6 || item.Value == 7) Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(item.Key);
                    Console.ResetColor();
                    Console.SetCursorPosition(STARTPOS - 2, item.Value);
                    Console.Write("¦");
                }
            }
        }
        public static void ClearControlsLayout()
        {
            _controls.Clear();
        }
        public static void ClearLog()
        {
            for (int i = 0; i < logList.Length; i++)
            {
                logList[i] = "";
            }
        }
    }
}