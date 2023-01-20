namespace FinalProject
{
    internal static class Log
    {
        static string[] logList = {"","","","",""};
        // need to make an array\list of strings, LIFO
        static public void PrintMessage(string msg, ConsoleColor color) 
        {
            int position;
            for (int i = logList.Length; i >= 2; i--)
            {
                //Console.ForegroundColor = color;
                position = 10 - logList.Length + i;
                logList[i -1] = logList[i - 2];
                Console.SetCursorPosition(70, position);
                for (int j = 0; j < logList[i-1].Length * 2; j++)
                {
                    Console.Write(' ');
                }
                Console.SetCursorPosition(70, position);
                Console.WriteLine(logList[i - 1]);
            }
            Console.ForegroundColor = color;
            position = 10 - logList.Length;
            logList[0] = msg;
            Console.SetCursorPosition(70, position + 1);
            for (int i = 0; i < logList[1].Length; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(70, position + 1);
            Console.WriteLine(logList[0]);
            Console.ResetColor();
        }
    }
}