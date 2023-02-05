﻿using FinalProject.Keys;

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
            PrintControls();
            PrintMessage("Entered floor 1...", ConsoleColor.DarkYellow);
        }
        public static void PrintControls()
        {
            lock (LockMethods.ActionLock)
            {
                foreach (var item in _controls)
                {
                    Console.SetCursorPosition(STARTPOS - 20, item.Value);
                    Console.Write(item.Key);
                    Console.SetCursorPosition(STARTPOS - 2, item.Value);
                    Console.Write("¦");
                }
            }
        }
    }
}